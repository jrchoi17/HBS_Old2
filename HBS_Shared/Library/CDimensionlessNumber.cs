using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HBS_Shared
{
    public static class CDimensionlessNumber
    {
        public const double Re_c = 3000.0;
        public const double epsilson = 116.0e-6;

        /// <summary>
        /// Calculation of the Nusselt number.
        /// </summary>
        /// <param name="D">Diameter. Unit: m.</param>
        /// <param name="Re_D">Reynolds number.</param>
        /// <param name="Pr">Prandtl number.</param>
        /// <returns></returns>
        public static double Nu_D(double D, double Re_D, double Pr)
        {
            if (Re_D < Re_c)
                return 4.36;
            else
            {
                double ff = f(D, Re_D);

                return  ((ff / 8.0) * (Re_D - 1000.0) * Pr) / (1.0 + 12.7 * Math.Sqrt(ff / 8.0) * (Math.Pow(Pr, 2.0 / 3.0) - 1.0));
            }
        }

        /// <summary>
        /// Calculation of the Reynolds number.
        /// </summary>
        /// <param name="u">Velocity. Unit: m/s.</param>
        /// <param name="D">Diameter. Unit: m.</param>
        /// <param name="rho">Density. Unit: kg/m^3.</param>
        /// <param name="mu">Dynamic viscosity. Unit: Pa-s.</param>
        /// <returns></returns>
        public static double Re_D(double u, double D, double rho, double mu)
        {
            return rho * Math.Abs(u) * D / mu;
        }

        /// <summary>
        /// Calculation of Prandtl number.
        /// </summary>
        /// <param name="c_p">Specific heat. Unit: J/kg-K.</param>
        /// <param name="mu">Dynamic viscosity. Unit: Pa-s.</param>
        /// <param name="k">Thermal conductivity. Unit: W/m-K.</param>
        /// <returns></returns>
        public static double Pr(double c_p, double k, double mu)
        {
            return c_p * mu / k;
        }

        /// <summary>
        /// Darcy friction number.
        /// </summary>
        /// <param name="D">Diameter. Unit: m.</param>
        /// <param name="Re_D">Reynolds number.</param>
        /// <returns></returns>
        public static double f(double D, double Re_D)
        {
            if (Re_D <= Re_c)
                return 64.0 / Re_D;
            else
                return Math.Pow(-1.8 * Math.Log10(Math.Pow(epsilson / D / 3.7, 1.11)), -2.0);
        }
    }
}
