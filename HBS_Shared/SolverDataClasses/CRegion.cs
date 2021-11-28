using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace HBS_Shared
{
    public class CRegion
    {
        #region Enums
        /// <summary>
        /// Enum of region type.
        /// </summary>
        public enum RegionType { Fluid = 0, Solid, Wall }
        #endregion



        #region Region information
        /// <summary>
        /// Region type.
        /// </summary>
        public RegionType Type { get; set; }

        /// <summary>
        /// Number of cells.
        /// </summary>
        public int N { get; set; }
        #endregion



        #region Data structures
        /// <summary>
        /// Cell list.
        /// </summary>
        public List<CCell> Cells { get; set; }

        /// <summary>
        /// PseudeoCell list.
        /// </summary>
        public List<CPseudoCell> PCells { get; set; }

        /// <summary>
        /// Region at previous time step.
        /// </summary>
        public CRegion Region0 { get; set; }

        /// <summary>
        /// Material proeprty.
        /// </summary>
        public List<CMatProp> Property { get; set; }
        #endregion



        #region Exterior convection heat trasfer parameters
        /// <summary>
        /// Temperature of quiescent region. Unit: K.
        /// </summary>
        public double T_inf { get; set; }

        /// <summary>
        /// Convection heat transfer coefficient of quiescent region. Unit: W/m^2-K.
        /// </summary>
        public double h_inf { get; set; }
        #endregion



        #region Constructor methods
        /// <summary>
        /// Constructor method.
        /// </summary>
        /// <param name="ncell">Number of cells.</param>
        public CRegion(int ncell)
        {
            N = ncell;
            Cells = new List<CCell>();

            for (int i = 0; i < ncell; i++)
            {
                CCell cell = new CCell(i);
                //cell.Type = CCell.CellType.Cell;

                Cells.Add(cell);
            }

            Cells[0].IsFirst = true;
            Cells[ncell - 1].IsLast = true;
        }
        /// <summary>
        /// Constructor method for deep clone.
        /// </summary>
        /// <param name="region">Region class.</param>
        public CRegion(CRegion region)
        {
            // set region information.
            Type = region.Type;
            N = region.N;

            // set data structures.
            Cells = new List<CCell>();
            for (int I = 0; I < region.Cells.Count; I++)
            {
                CCell cell = new CCell(region.Cells[I]);
                Cells.Add(cell);
            }

            PCells = new List<CPseudoCell>();
            for (int i = 0; i < region.PCells.Count; i++)
            {
                CPseudoCell pseudoCell = new CPseudoCell(region.PCells[i]);
                PCells.Add(pseudoCell);
            }

            Region0 = region.Region0;
            if (region.Property != null)
            {
                Property = new List<CMatProp>();

                for (int i = 0; i < region.Property.Count; i++)
                    Property.Add(new CMatProp(region.Property[i]));
            }

        }
        #endregion

        public void SetDataToPseudoCellFromCell(CCell.DataOrder order)
        {
            for (int i = 0; i < N + 1; i++)
            {
                switch (order)
                {   
                    case CCell.DataOrder.u:
                        if (i == 0)
                            PCells[i].u = Cells[i].u;
                        else if (i == N)
                            PCells[i].u = Cells[i - 1].u;
                        else
                            PCells[i].u = (Cells[i - 1].DV_e * Cells[i - 1].u + Cells[i].DV_w * Cells[i].u) / (Cells[i - 1].DV_e + Cells[i].DV_w);
                        break;
                    case CCell.DataOrder.p:
                        if (i == 0)
                            PCells[i].p = Cells[i].p;
                        else if (i == N)
                            PCells[i].p = Cells[i - 1].p;
                        else
                            PCells[i].p = (Cells[i - 1].DV_e * Cells[i - 1].p + Cells[i].DV_w * Cells[i].p) / (Cells[i - 1].DV_e + Cells[i].DV_w);
                        break;
                    case CCell.DataOrder.rho:
                        if (i == 0)
                            PCells[i].rho = Cells[i].rho;
                        else if (i == N)
                            PCells[i].rho = Cells[i - 1].rho;
                        else
                            PCells[i].rho = (Cells[i - 1].DV_e * Cells[i - 1].rho + Cells[i].DV_w * Cells[i].rho) / (Cells[i - 1].DV_e + Cells[i].DV_w);
                        break;
                    case CCell.DataOrder.c_p:
                        if (i == 0)
                            PCells[i].c_p = Cells[i].c_p;
                        else if (i == N)
                            PCells[i].c_p = Cells[i - 1].c_p;
                        else
                            PCells[i].c_p = (Cells[i - 1].DV_e * Cells[i - 1].c_p + Cells[i].DV_w * Cells[i].c_p) / (Cells[i - 1].DV_e + Cells[i].DV_w);
                        break;
                    case CCell.DataOrder.k:
                        if (i == 0)
                            PCells[i].k = Cells[i].k;
                        else if (i == N)
                            PCells[i].k = Cells[i - 1].k;
                        else
                            PCells[i].k = (Cells[i - 1].DV_e * Cells[i - 1].k + Cells[i].DV_w * Cells[i].k) / (Cells[i - 1].DV_e + Cells[i].DV_w);
                        break;
                    case CCell.DataOrder.mu:
                        if (i == 0)
                            PCells[i].mu = Cells[i].mu;
                        else if (i == N)
                            PCells[i].mu = Cells[i - 1].mu;
                        else
                            PCells[i].mu = (Cells[i - 1].DV_e * Cells[i - 1].mu + Cells[i].DV_w * Cells[i].mu) / (Cells[i - 1].DV_e + Cells[i].DV_w);
                        break;
                }
            }
        }

        public void SetDataToCellFromPseudoCell(CCell.DataOrder order)
        {
            double phi_w, phi_e;

            for (int I = 0; I < N; I++)
            {
                double V_ce_w = PCells[I].DV_w;
                double V_cw_e = PCells[I + 1].DV_e;
                switch(order)
                {
                    case CCell.DataOrder.u:
                        phi_w = PCells[I].u;
                        phi_e = PCells[I + 1].u;
                        Cells[I].u = (phi_w * V_ce_w + phi_e * V_cw_e) / (V_ce_w + V_cw_e);
                        break;
                    default:
                        throw CException.Show(CException.Type.UnsupportedKeyword);
                }                
            }
        }


        /// <summary>
        /// Initialize region connectivities.
        /// </summary>
        /// <param name="regions">List of region.</param>
        public void SetRegionConnectivity(List<CRegion> regions)
        {
            // for cells...
            for (int i = 0; i < Cells.Count; i++)
            {
                CCell cell = Cells[i];
                if (cell.N_J < 0 || cell.N_I < 0)
                    cell.North = null;
                else
                    cell.North = regions[cell.N_J].Cells[cell.N_I];

                if (cell.S_J < 0 || cell.S_I < 0)
                    cell.South = null;
                else
                    cell.South = regions[cell.S_J].Cells[cell.S_I];
            }
        }

        /// <summary>
        /// Update time and clone class data to Region0.
        /// </summary>
        public void UpdateTime()
        {
            Region0 = new CRegion(this);
        }

         public void SetBcTemperature(double T, ST_SD.FlowDirection dir)
        {
            switch(dir)
            {
                case ST_SD.FlowDirection.WestToEast:
                    PCells[0].T = T;
                    break;
                case ST_SD.FlowDirection.EastToWest:
                    PCells[N].T = T;
                    break;
                default:
                    throw CException.Show(CException.Type.UnsupportedKeyword);
            }
        }

        public void SetBcVelocity(double u, ST_SD.FlowDirection dir)
        {
            switch(dir)
            {
                case ST_SD.FlowDirection.WestToEast:
                    PCells[0].u = u;
                    break;
                case ST_SD.FlowDirection.EastToWest:
                    PCells[N].u = u;
                    break;
                default:
                    throw CException.Show(CException.Type.UnsupportedKeyword);
            }
        }

        public void SetBcPressure(double p, ST_SD.FlowDirection dir)
        {
            switch(dir)
            {
                case ST_SD.FlowDirection.WestToEast:
                    PCells[0].p = p;
                    break;
                case ST_SD.FlowDirection.EastToWest:
                    PCells[N].p = p;
                    break;
                default:
                    throw CException.Show(CException.Type.UnsupportedKeyword);
            }
        }

        public void SetVolumetricHeatSource(double q3)
        {
            foreach (CCell cell in Cells)
                cell.q3 = q3;
        }

        public void SetCellParent()
        {
            for (int i = 0; i < N; i++)
                Cells[i].Parent = this;
        }

        public void SetPseudoCell()
        {
            PCells = new List<CPseudoCell>();

            for (int i = 0; i < N + 1; i++)
            {
                CPseudoCell pcell = new CPseudoCell(i);
                if (i == 0)
                {
                    pcell.Dx_w = 0.0;
                    pcell.Dx_e = Cells[i].Dx_w;
                    pcell.A_w = Cells[i].A_w;
                    pcell.A_e = Cells[i].A_I;
                    pcell.A_c = pcell.A_e;
                    pcell.DV_w = 0.0;
                    pcell.DV_e = Cells[i].DV_w;

                    PCells.Add(pcell);
                }
                else if (i == N)
                {
                    pcell.Dx_w = Cells[i - 1].Dx_e;
                    pcell.Dx_e = 0.0;
                    pcell.A_w = Cells[i - 1].A_I;
                    pcell.A_e = Cells[i - 1].A_e;
                    pcell.A_c = pcell.A_w;
                    pcell.DV_w = Cells[i - 1].DV_e;
                    pcell.DV_e = 0.0;

                    PCells.Add(pcell);
                }
                else
                {
                    pcell.Dx_w = Cells[i - 1].Dx_e;
                    pcell.Dx_e = Cells[i].Dx_w;
                    pcell.A_w = Cells[i - 1].A_I;
                    pcell.A_e = Cells[i].A_I;
                    pcell.A_c = Cells[i - 1].A_e;
                    pcell.DV_w = Cells[i - 1].DV_e;
                    pcell.DV_e = Cells[i].DV_w;

                    PCells.Add(pcell);
                }
            }
            
        }

        public List<double> GetSelectedData(CCell.DataOrder order)
        {
            List<double> data = new List<double>();

            switch (order)
            {
                case CCell.DataOrder.N_J:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].N_J);
                    break;
                case CCell.DataOrder.N_I:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].N_I);
                    break;
                case CCell.DataOrder.S_J:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].S_J);
                    break;
                case CCell.DataOrder.S_I:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].S_I);
                    break;
                case CCell.DataOrder.u:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].u);
                    break;
                case CCell.DataOrder.p:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].p);
                    break;
                case CCell.DataOrder.T:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].T);
                    break;
                case CCell.DataOrder.T_nw:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].T_n);
                    break;
                case CCell.DataOrder.dx_Iw:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].Dx_w);
                    break;
                case CCell.DataOrder.dx_Ie:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].Dx_e);
                    break;
                case CCell.DataOrder.dx_In:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].Dx_n);
                    break;
                case CCell.DataOrder.dx_Is:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].Dx_s);
                    break;
                case CCell.DataOrder.A_w:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].A_w);
                    break;
                case CCell.DataOrder.A_e:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].A_e);
                    break;
                case CCell.DataOrder.A_n:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].A_n);
                    break;
                case CCell.DataOrder.A_s:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].A_s);
                    break;
                case CCell.DataOrder.DV:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].DV);
                    break;
                case CCell.DataOrder.rho:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].rho);
                    break;
                case CCell.DataOrder.c_p:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].c_p);
                    break;
                case CCell.DataOrder.k:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].k);
                    break;
                case CCell.DataOrder.mu:
                    for (int I = 0; I < N; I++)
                        data.Add(Cells[I].mu);
                    break;
                default:
                    throw CException.Show(CException.Type.UnsupportedKeyword);
            }

            return data;
        }

        public void DebugWriteLine(CCell.DataOrder order, bool clipboard = true)
        {
            string contents = string.Empty;

            switch (order)
            {
                case CCell.DataOrder.u:
                    for (int I = 0; I < N; I++)
                    {
                        contents += Cells[I].u.ToString() + Environment.NewLine;
                        Debug.WriteLine(Cells[I].u);
                    }
                    break;
                case CCell.DataOrder.p:
                    for (int I = 0; I < N; I++)
                    {
                        contents += Cells[I].p.ToString() + Environment.NewLine;
                        Debug.WriteLine(Cells[I].p);
                    }
                    break;
                case CCell.DataOrder.T:
                    for (int I = 0; I < N; I++)
                    {
                        contents += Cells[I].T.ToString() + Environment.NewLine;
                        Debug.WriteLine(Cells[I].T);
                    }
                    break;
                case CCell.DataOrder.T_nw:
                    for (int I = 0; I < N; I++)
                    {
                        contents += Cells[I].T_n.ToString() + Environment.NewLine;
                        Debug.WriteLine(Cells[I].T_n);
                    }
                    break;
                case CCell.DataOrder.q3:
                    for (int I = 0; I < N; I++)
                    {
                        contents += Cells[I].q3.ToString() + Environment.NewLine;
                        Debug.WriteLine(Cells[I].q3);
                    }
                    break;
                case CCell.DataOrder.rho:
                    for (int I = 0; I < N; I++)
                    {
                        contents += Cells[I].rho.ToString() + Environment.NewLine;
                        Debug.WriteLine(Cells[I].rho);
                    }
                    break;
                case CCell.DataOrder.c_p:
                    for (int I = 0; I < N; I++)
                    {
                        contents += Cells[I].c_p.ToString() + Environment.NewLine;
                        Debug.WriteLine(Cells[I].c_p);
                    }
                    break;
                case CCell.DataOrder.k:
                    for (int I = 0; I < N; I++)
                    {
                        contents += Cells[I].k.ToString() + Environment.NewLine;
                        Debug.WriteLine(Cells[I].k);
                    }
                    break;
                case CCell.DataOrder.mu:
                    for (int I = 0; I < N; I++)
                    {
                        contents += Cells[I].mu.ToString() + Environment.NewLine;
                        Debug.WriteLine(Cells[I].mu);
                    }
                    break;
                case CCell.DataOrder.h_g:
                    for (int I = 0; I < N; I++)
                    {
                        contents += Cells[I].h_g.ToString() + Environment.NewLine;
                        Debug.WriteLine(Cells[I].h_g);
                    }
                    break;
                case CCell.DataOrder.epsilon_g:
                    for (int I = 0; I < N; I++)
                    {
                        contents += Cells[I].epsilon_g.ToString() + Environment.NewLine;
                        Debug.WriteLine(Cells[I].epsilon_g);
                    }
                    break;
            }
            

            if (clipboard)
            {
                //Clipboard.Clear();
                //Clipboard.SetText(contents);
            }
                

        }

        public List<CCell> GetPsedoCells()
        {
            List<CCell> cells = new List<CCell>();

            for (int i = 0; i < N - 1; i++)
            {
                CCell cell = new CCell(i);
                
                cell.A_w = (Cells[i].A_w + Cells[i].A_e) / 2.0;
                cell.A_e = (Cells[i + 1].A_w + Cells[i + 1].A_e) / 2.0;
                
                if (i == 0)
                {
                    cell.IsFirst = true;
                    cell.A_w = Cells[i].A_w;
                }
                else if (i == N - 2)
                {
                    cell.IsLast = true;
                    cell.A_e = Cells[i + 1].A_e;
                }

                cell.DV = CMatricReport.LengthAverageValue(Cells[i], Cells[i + 1], CCell.DataOrder.DV);
                cell.A_n = CMatricReport.LengthAverageValue(Cells[i], Cells[i + 1], CCell.DataOrder.A_n);
                cell.A_s = CMatricReport.LengthAverageValue(Cells[i], Cells[i + 1], CCell.DataOrder.A_s);
                cell.rho = CMatricReport.VolumeAverageValue(Cells[i], Cells[i + 1], CCell.DataOrder.rho);

                cells.Add(cell);
            }

            return cells;
        }

        /// <summary>
        /// Write all cell contents as a file (for debugging).
        /// </summary>
        /// <param name="type">Cell type.</param>
        /// <param name="fileName">File name.</param>
        public void WriteRegionContents(string fileName)
        {
            string dm = "\t";
            List<string> contents = new List<string>();
            contents.Add("Index" + dm +
                "RID_n" + dm + "CID_n" + dm + "RID_s" + dm + "CID_s" + dm +
                "u" + dm + "p" + dm + "T" + dm + "T_nw" + dm + "q3" + dm +
                "rho" + dm + "c_p" + dm + "k" + dm + "mu" + dm +
                "dx_w" + dm + "dx_e" + dm + "dx_n" + dm + "dx_s" + dm +
                "A_w" + dm + "A_e" + dm + "A_n" + dm + "A_s" + dm +
                "dV");
            for (int i = 0; i < Cells.Count; i++)
            {
                CCell c = Cells[i];
                contents.Add(c.II.ToString() + dm +
                    c.N_J.ToString() + dm + c.N_I.ToString() + dm + c.S_J.ToString() + dm + c.S_I.ToString() + dm +
                    c.u.ToString() + dm + c.p.ToString() + dm + c.T.ToString() + dm + c.T_n.ToString() + dm + c.q3.ToString() + dm +
                    c.rho.ToString() + dm + c.c_p.ToString() + dm + c.k.ToString() + dm + c.mu.ToString() + dm +
                    c.Dx_w.ToString() + dm + c.Dx_e.ToString() + dm + c.Dx_n.ToString() + dm + c.Dx_s.ToString() + dm +
                    c.A_w.ToString() + dm + c.A_e.ToString() + dm + c.A_n.ToString() + dm + c.A_s.ToString() + dm +
                    c.DV.ToString());
            }

            File.WriteAllLines(fileName, contents.ToArray());
        }

        public void WriteRegionTemperature(string fileName, double time)
        {
            ST_SD sd = ST_SD.GetInstance();
            List<string> temperature = new List<string>();
            temperature.Add(time.ToString() + " s");

            for (int i = 0; i < N; i++)
                temperature.Add(Cells[i].T.ToString());

            File.WriteAllLines(fileName, temperature.ToArray());
        }
    }
}
