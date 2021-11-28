using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS_Shared
{
    public class CPseudoCell
    {
        #region Pseudo-cell information
        /// <summary>
        /// Pseudo cell index.
        /// </summary>
        public int ii { get; set; }
        #endregion


        #region Physical Parameters
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
        /// Area of west face. Unit: m^2.
        /// </summary>
        public double A_w { get; set; }

        /// <summary>
        /// Area of east face. Unit: m^2.
        /// </summary>
        public double A_e { get; set; }

        /// <summary>
        /// Area of connected face. Unit: m^2.
        /// </summary>
        public double A_c { get; set; }

        /// <summary>
        /// Volume of cell. Unit: m^3.
        /// </summary>
        public double DV
        {
            get
            {
                return DV_w + DV_e;
            }
        }

        /// <summary>
        /// Volume from west side cell to center of mass. Unit: m^3.
        /// </summary>
        public double DV_w { get; set; }

        /// <summary>
        /// Volume from east side cell to center of mass. Unit: m^3.
        /// </summary>
        public double DV_e { get; set; }
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



        #region Source term parameters
        /// <summary>
        /// Volumetric momentum source of velocity term, S_u. Unit: kg/m^3-s.
        /// </summary>
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
                return S_b_1 * DV;
            }
        }
        #endregion



        #region CFD parameters
        /// <summary>
        /// Diffusion flux. Unit: kg/s.
        /// </summary>
        public double D_1
        {
            get
            {
                return k * A_c / Dx_we;
            }
        }

        /// <summary>
        /// Diffusion flux. Unit: W/K.
        /// </summary>
        public double D_2
        {
            get
            {
                return k * A_c / Dx_we;
            }
        }

        /// <summary>
        /// Convection flux. Unit: kg/s.
        /// </summary>
        public double F_1
        {
            get
            {
                return rho * A_c * u;
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
        #endregion



        #region Constructor method
        public CPseudoCell(int i)
        {
            ii = i;
        }

        public CPseudoCell(CPseudoCell pcell)
        {

            ii = pcell.ii;
            u = pcell.u;
            p = pcell.p;
            T = pcell.T;
        }
        #endregion

    }
}
