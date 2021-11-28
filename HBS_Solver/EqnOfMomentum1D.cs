using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS_Shared;

namespace HBS_Solver
{
    public class EqnOfMomentum1D
    {
        public static void Solving(CRegion region, ST_SD.FlowDirection dir)
        {
            ST_SD sd = ST_SD.GetInstance();
            List<CPseudoCell> pcells = region.PCells;
            List<CPseudoCell> pcells_0 = region.Region0.PCells;
            List<CCell> cells = region.Cells;
            double dF;

            switch(dir)
            {
                case ST_SD.FlowDirection.WestToEast:
                    for (int I = 1; I <= region.N - 1; I++)
                    {
                        dF = _dF(cells[I - 1], cells[I], pcells[I], pcells[I + 1], pcells_0[I]);
                        cells[I].p = cells[I - 1].A_I / cells[I].A_I * cells[I - 1].p - dF / cells[I].A_I;
                    }
                    break;
                case ST_SD.FlowDirection.EastToWest:
                    for (int I = region.N - 1; I >= 1; I--)
                    {
                        dF = _dF(cells[I - 1], cells[I], pcells[I], pcells[I + 1], pcells_0[I]);
                        cells[I - 1].p = cells[I].A_I / cells[I - 1].A_I * cells[I].p + dF / cells[I - 1].A_I;
                    }
                    break;
                default:
                    throw CException.Show(CException.Type.UnsupportedKeyword);
            }
        }

        /// <summary>
        /// Force difference term. Unit: N.
        /// </summary>
        /// <param name="cell_W">(I - 1)th cell</param>
        /// <param name="cell_E">(I)th cell</param>
        /// <param name="pcell_i">(i)th pcell</param>
        /// <param name="pcell_e">(i + 1)th pcell</param>
        /// <param name="pcell_i_0">(i)th previous time cell</param>
        /// <returns></returns>
        public static double _dF(CCell cell_W, CCell cell_E, CPseudoCell pcell_i, CPseudoCell pcell_e, CPseudoCell pcell_i_0)
        {
            ST_SD sd = ST_SD.GetInstance();
            double dF = 0.0;
            
            dF += (pcell_i.rho * pcell_i.u - pcell_i_0.rho * pcell_i_0.u) * pcell_i.DV / sd.Dt;
            dF += cell_E.F_1 * cell_E.u - cell_W.F_1 * cell_W.u;
            dF -= cell_W.D_1 * pcell_i.u + cell_E.D_1 * pcell_e.u - (cell_W.D_1 + cell_E.D_1) * pcell_e.u;
            dF -= pcell_i.S_U * pcell_e.u + pcell_i.S_B_1;

            return dF;
        }
    }
}
