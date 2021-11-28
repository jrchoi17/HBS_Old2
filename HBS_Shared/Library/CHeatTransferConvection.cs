using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS_Shared
{
    public class CHeatTransferConvection
    {
        /// <summary>
        /// Calculation of the convection heat transfer coefficient. Unit: W/m^2-K.
        /// </summary>
        /// <param name="cell">CCell class.</param>
        /// <param name="D">Diameter. Unit: m.</param>
        /// <returns></returns>
        public static double h(CCell cell, double D)
        {
            double u = cell.u;

            double rho = cell.rho;
            double c_p = cell.c_p;
            double k = cell.k;
            double mu = cell.mu;

            double Re_D = CDimensionlessNumber.Re_D(u, D, rho, mu);
            double Pr = CDimensionlessNumber.Pr(c_p, k, mu);

#if (DEBUG)
            double Nu_D = 4.36;
#else
            double Nu_D = CDimensionlessNumber.Nu_D(D, Re_D, Pr);
#endif
            return h(D, k, Nu_D);
        }

        /// <summary>
        /// Calculation of the convection heat transfer coefficient. Unit: W/m^2-K.
        /// </summary>
        /// <param name="D">Diameter. Unit: m.</param>
        /// <param name="k">Thermal conductivity. Unit: W/m-K.</param>
        /// <param name="Nu_D">Nusselt number.</param>
        /// <returns></returns>
        public static double h(double D, double k, double Nu_D)
        {
            return Nu_D * k / D;
        }
    }
}
