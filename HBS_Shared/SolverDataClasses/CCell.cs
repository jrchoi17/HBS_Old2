using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS_Shared
{
    public class CCell
    {
        #region Enums
        public enum CellZone { Fluid_Wall, Fluid_Solid_Wall }

        /// <summary>
        /// Enum
        /// </summary>
        /// 
        public enum XmlDataOrder
        {
          /*0        1    2    3    4     5  6  7  8    9    10    11    12    13    14   15   16   17   18*/
            N_J = 0, N_I, S_J, S_I, Prop, u, p, T, T_n, T_s, Dx_w, Dx_e, Dx_n, Dx_s, A_w, A_e, A_n, A_s, DV

        }

        public enum DataOrder
        {
            N_J = 0, N_I, S_J, S_I,
            u, p, T, T_nw, q3,
            dx_Iw, dx_Ie, dx_In, dx_Is, A_w, A_e, A_n, A_s, A_I, DV,
            rho, c_p, k, mu,
            h_g, epsilon_g
        }
        #endregion



        #region Cell information
        /// <summary>
        /// Cell zone.
        /// </summary>
        public CellZone Zone { get; set; }

        /// <summary>
        /// Parent region.
        /// </summary>
        public CRegion Parent { get; set; }

        /// <summary>
        /// Cell index.
        /// </summary>
        public int II { get; set; }

        /// <summary>
        /// Cell indexing flag. If this cell is the first cell, the value is true.
        /// </summary>
        public bool IsFirst { get; set; }

        /// <summary>
        /// Cell indexing flag. If this cell is the last cell, the value is true.
        /// </summary>
        public bool IsLast { get; set; }

        /// <summary>
        /// Index of north side region.
        /// </summary>
        public int N_J { get; set; }

        /// <summary>
        /// Index of north side cell.
        /// </summary>
        public int N_I { get; set; }

        /// <summary>
        /// Index of south side region.
        /// </summary>
        public int S_J { get; set; }

        /// <summary>
        /// Index of south side cell.
        /// </summary>
        public int S_I { get; set; }

        /// <summary>
        /// Index of property
        /// </summary>
        public int Prop { get; set; }
        #endregion



        #region Physical parameters
        /// <summary>
        /// Velocity. Unit: m/s.
        /// </summary>
        public double u { get; set; }

        /// <summary>
        /// Gage pressure. Unit: Pa.
        /// </summary>
        public double p { get; set; }

        /// <summary>
        /// Temperature. Unit: K.
        /// </summary>
        public double T { get; set; }

        /// <summary>
        /// Temperature of north side wall. Unit: K.
        /// </summary>
        public double _T_n = 0.0;
        public double T_n 
        {
            get
            {
                return _T_n;
            }
            set
            {
                _T_n = value;

                if (North != null)
                    North._T_s = value;
            }
        }

        /// <summary>
        /// Temperature of south side wall. Unit: K.
        /// </summary>
        private double _T_s = 0.0;
        public double T_s
        {
            get
            {
                return _T_s;
            }
            set
            {
                _T_s = value;

                if (South != null)
                    South._T_n = value;
            }
        }

        /// <summary>
        /// Volumetric heat source and heat sink. Unit: W/m^3.
        /// if it is a positive value, 
        /// that means heat source and 
        /// the cell temperature will be increase by the heat source.
        /// </summary>
        public double q3 { get; set; }

        /// <summary>
        /// Total enthalpy. Unit: J/kg.
        /// </summary>
        public double H
        {
            get
            {
                return c_p * T + Math.Pow(u, 2.0) / 2.0;
            }
        }

        /// <summary>
        /// Total internal energy. Unit: J/kg.
        /// </summary>
        public double E
        {
            get
            {
                return H - rho / p;
            }
        }
        #endregion



        #region Geometric parameters
        /// <summary>
        /// Distance between cell centroid and west face, Δx_w. Unit: m.
        /// </summary>
        public double Dx_w { get; set; }

        /// <summary>
        /// Distance between cell centroid and east face, Δx_e. Unit: m.
        /// </summary>
        public double Dx_e { get; set; }

        /// <summary>
        /// Distance between west face and east face, Δx_we. Unit: m.
        /// </summary>
        public double Dx_we
        {
            get
            {
                return Dx_w + Dx_e;
            }
        }

        /// <summary>
        /// Distance between cell centroid and north face, Δx_n. Unit: m.
        /// </summary>
        public double Dx_n { get; set; }

        /// <summary>
        /// Distance between cell centroid and south face, Δx_s. Unit: m.
        /// </summary>
        public double Dx_s { get; set; }

        /// <summary>
        /// Distance between north face and south face, Δx_ns. Unit: m.
        /// </summary>
        public double Dx_ns
        {
            get
            {
                return Dx_n + Dx_s;
            }
        }

        /// <summary>
        /// Area of west face. Unit: m^2.
        /// </summary>
        public double A_w { get; set; }

        /// <summary>
        /// Area of east face. Unit: m^2.
        /// </summary>
        public double A_e { get; set; }

        /// <summary>
        /// Area of north face. Unit: m^2.
        /// </summary>
        public double A_n { get; set; }

        /// <summary>
        /// Area of north face of west side to center of mass. Unit: m^2.
        /// </summary>
        public double A_n_Iw
        {
            get
            {
                return psi_w * A_n;
            }
        }

        /// <summary>
        /// Area of north face of east side to center of mass. Unit: m^2.
        /// </summary>
        public double A_n_Ie
        {
            get
            {
                return psi_e * A_n;
            }
        }

        /// <summary>
        /// Area of south face. Unit: m^2.
        /// </summary>
        public double A_s { get; set; }

        /// <summary>
        /// Area of south face of east side to center of mass. Unit: m^2.
        /// </summary>
        public double A_s_Iw
        {
            get
            {
                return psi_w * A_s;
            }
        }

        /// <summary>
        /// Area of south face of west side to center of mass. Unit: m^2.
        /// </summary>
        public double A_s_Ie
        {
            get
            {
                return psi_e * A_s;
            }
        }


        /// <summary>
        /// Area of centroid face. Unit: m^2.
        /// </summary>
        public double A_I
        {
            get
            {
                return psi_e * A_w + psi_w * A_e;
            }
        }

        /// <summary>
        /// Volume of cell. Unit: m^3.
        /// </summary>
        public double DV { get; set; }

        /// <summary>
        /// Volume of cell of west side to center of mass. Unit: m^3.
        /// </summary>
        public double DV_w
        {
            get
            {
                return (A_w + A_I) * psi_w / (A_w + A_e) * DV;
            }
        }

        /// <summary>
        /// Volume of cell of east side to center of mass. Unit: m^3.
        /// </summary>
        public double DV_e
        {
            get
            {
                return (A_e + A_I) * psi_e / (A_w + A_e) * DV;
            }
        }

        /// <summary>
        /// West side area ratio of cell.
        /// </summary>
        private double psi_w
        {
            get
            {
                return (A_w + 2.0 * A_e) / (3.0 * (A_w + A_e));
            }

        }

        /// <summary>
        /// East side area ratio of cell.
        /// </summary>
        private double psi_e
        {
            get
            {
                return (2.0 * A_w + A_e) / (3.0 * (A_w + A_e));
            }
        }
        #endregion



        #region Material properties
        /// <summary>
        /// Density. Unit: kg/m^3.
        /// </summary>
        public double rho { get; set; }

        /// <summary>
        /// Specific heat. Unit: J/kg-K.
        /// </summary>
        public double c_p { get; set; }

        /// <summary>
        /// Thermal conductivity. Unit: W/m-K.
        /// </summary>
        public double k { get; set; }

        /// <summary>
        /// Dynamic viscosity. Unit: Pa-s.
        /// </summary>
        public double mu { get; set; }
        #endregion



        #region Source term paramters
        public double S_u
        {
            get
            {
                ST_SD sd = ST_SD.GetInstance();

                double Re_D = CDimensionlessNumber.Re_D(u, sd.d, rho, mu);
                double f = CDimensionlessNumber.f(sd.d, Re_D);
                //return - f / sd.d * rho * u / 2.0;
                return 0.0;
            }
        }

        /// <summary>
        /// Momentum source of velocity term, S_U. Unit: kg/s.
        /// </summary>
        public double S_U
        {
            get
            {
                if (S_u * u > 0)
                    throw CException.Show(CException.Type.InvalidRange);

                return S_u * DV;
            }
        }

        /// <summary>
        /// Volumetric momentum source of constant term, S_b. Unit: N/m^3.
        /// </summary>
        public double S_b_1
        {
            get
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Momentum source of constant term, S_B. Unit: N.
        /// </summary>
        public double S_B_1
        {
            get
            {
                if (S_b_1 > 0)
                    throw CException.Show(CException.Type.InvalidRange);
                
                return S_b_1 * DV;
            }
        }

        /// <summary>
        /// Energy source of temperature term, S_T. unit: W/K.
        /// </summary>
        public double S_T { get; set; }

        /// <summary>
        /// Energy source of constant term, S_B, Unit: W.
        /// </summary>
        public double S_B_2 { get; set; }
        #endregion



        #region CFD parameters
        /// <summary>
        /// Diffusion flux. Unit: kg/s.
        /// </summary>
        public double D_1
        {
            get
            {
                return mu * A_I / Dx_we;
            }
        }

        /// <summary>
        /// Diffusion flux. Unit: W/K.
        /// </summary>
        public double D_2
        {
            get
            {
                return k * A_I / Dx_we;
            }
        }

        /// <summary>
        /// Convection flux. Unit: kg/s.
        /// </summary>
        public double F_1
        {
            get
            {
                return rho * A_I * u;
            }
        }

        /// <summary>
        /// Convection flux. Unit: W/K.
        /// </summary>
        public double F_2
        {
            get
            {
                return F_1 * c_p;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double a_P { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double a_P_0 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double a_W { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double a_E { get; set; }
        

        /// <summary>
        /// 
        /// </summary>
        public double b { get; set; }
        #endregion



        #region Operating conditions
        /// <summary>
        /// Convection heat transfer coefficient of gas side. Unit: W/m^2-K.
        /// </summary>
        public double h_g
        {
            get
            {
                ST_SD sd = ST_SD.GetInstance();
                switch (Parent.Type)
                {
                    case CRegion.RegionType.Fluid:
                        if (sd.SolidRegion == null)
                            return 0.0;
                        else
                            return CHeatTransferConvection.h(this, sd.d);
                    default:
                        return 0.0;
                }
            }
        }

        /// <summary>
        /// Emissivity of gas side. Unit: W/m^2-K^4.
        /// </summary>
        public double epsilon_g
        {
            get
            {
                ST_SD sd = ST_SD.GetInstance();
                switch (Parent.Type)
                {
                    case CRegion.RegionType.Fluid:
                        if (sd.SolidRegion == null)
                            return 0.0;
                        else
                        {
                            double X_CO2 = sd.CombustedGas.MoleFraction[CGas.Composition.CO2];
                            double X_H2O = sd.CombustedGas.MoleFraction[CGas.Composition.H2O];

                            return CHeatTransferRadiation.epsilon((p + 101325.0) / 101325.0, T, X_CO2, X_H2O, sd.L);
                        }
                            
                    default:
                            return 0.0;
                }
            }
        }

        /// <summary>
        /// Absoltivity of gas side. Unit: W/m^2-K^4.
        /// </summary>
        public double alpha_g
        {
            get
            {
                ST_SD sd = ST_SD.GetInstance();
                switch (Parent.Type)
                {
                    case CRegion.RegionType.Fluid:
                        if (sd.SolidRegion == null)
                            return 0.0;
                        else
                        {
                            double X_CO2 = sd.CombustedGas.MoleFraction[CGas.Composition.CO2];
                            double X_H2O = sd.CombustedGas.MoleFraction[CGas.Composition.H2O];

                            return CHeatTransferRadiation.alpha((p + 101325.0) / 101325.0, T, X_CO2, X_H2O, sd.L); ;
                        }
                    default:
                        return 0.0;
                }
            }
        }
        #endregion



        #region Connectivity
        /// <summary>
        /// West side cell.
        /// </summary>
        public CCell West { get; set; }

        /// <summary>
        /// East side cell.
        /// </summary>
        public CCell East { get; set; }

        /// <summary>
        /// North side cell.
        /// </summary>
        public CCell North { get; set; }

        /// <summary>
        /// South side cell.
        /// </summary>
        public CCell South { get; set; }
        #endregion


        #region Constructor methods
        /// <summary>
        /// Constructor method.
        /// </summary>
        /// <param name="index">Cell index. [0:region.N - 1]</param>
        public CCell(int I)
        {
            II = I;
            IsFirst = false;
            IsLast = false;
        }



        /// <summary>
        /// Constructor method for deep clone.
        /// </summary>
        /// <param name="cell">Cell class.</param>
        public CCell(CCell cell)
        {
            // set cell information.
            Parent = cell.Parent;
            //Type = cell.Type;
            II = cell.II;
            IsFirst = cell.IsFirst;
            IsLast = cell.IsLast;
            N_J = cell.N_J;
            N_I = cell.N_I;
            S_J = cell.S_J;
            S_I = cell.S_I;

            // set physical parameters.
            u = cell.u;
            p = cell.p;
            T = cell.T;
            T_n = cell.T_n;
            q3 = cell.q3;

            // set material properties.
            rho = cell.rho;
            c_p = cell.c_p;
            k = cell.k;
            mu = cell.mu;

            // set geometric parameters.
            Dx_w = cell.Dx_w;
            Dx_e = cell.Dx_e;
            Dx_n = cell.Dx_n;
            Dx_s = cell.Dx_s;
            A_w = cell.A_w;
            A_e = cell.A_e;
            A_n = cell.A_n;
            A_s = cell.A_s;
            DV = cell.DV;
        }
        #endregion

        #region public methods
        public List<CCell> GetSouthCells()
        {
            List<CCell> cells = new List<CCell>();
            
            int J = 0;
            cells.Add(this);

            while (true)
            {
                if (cells[J].North == null)
                    break;
                else
                {
                    cells.Add(cells[J].North);
                    J++;
                }
            }

            return cells;
        }
        #endregion
        #region private methods
        #endregion
    }
}
