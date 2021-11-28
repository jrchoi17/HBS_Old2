using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS_Shared;

namespace HBS_Solver
{
    public class EqnOfEnergy1D
    {
        public enum DifferencingScheme { Central = 0, Upwind, Hybrid }


        public static void Solving(CRegion region, DifferencingScheme scheme = DifferencingScheme.Hybrid, ST_SD.FlowDirection dir = ST_SD.FlowDirection.WestToEast)
        {
            switch (region.Type)
            {
                case CRegion.RegionType.Fluid:
                    FluidSolving(region, scheme, dir);
                    break;
                case CRegion.RegionType.Solid:
                case CRegion.RegionType.Wall:
                    SolidSolving(region);
                    break;
                default:
                    throw CException.Show(CException.Type.UnsupportedKeyword);
            }
        }

        private static void FluidSolving(CRegion region, DifferencingScheme scheme, ST_SD.FlowDirection dir)
        {
            ST_SD sd = ST_SD.GetInstance();

            double[] T_new = new double[region.N];

            double[] a_W = new double[region.N];
            double[] a_E = new double[region.N];
            double[] a_P = new double[region.N];
            double[] a_P_1 = new double[region.N];
            double[] a_P_0 = new double[region.N];

            double[] S_T = new double[region.N];
            double[] S_B = new double[region.N];

            double[] b = new double[region.N];

            for (int I = 0; I < region.N; I++)
            {
                CCell cell = region.Cells[I];
                CCell cell_0 = region.Region0.Cells[I];
                CPseudoCell pcell_w = region.PCells[I];
                CPseudoCell pcell_e = region.PCells[I + 1];

                // 1. a_W & a_E calculation
                switch (scheme)
                {
                    case DifferencingScheme.Central:
                        // most west-side boundary conditions
                        if (I == 0) 
                            a_W[I] = 0.0;
                        else
                            a_W[I] = pcell_w.D_2 + pcell_w.F_2 / 2.0;

                        // most east-side boundary conditions
                        if (I == region.N - 1)
                            a_E[I] = 0.0;
                        else
                            a_E[I] = pcell_e.D_2 - pcell_e.F_2 / 2.0;
                        break;
                    case DifferencingScheme.Upwind:
                        // most west-side boundary conditions
                        if (I == 0)
                            a_W[I] = 0.0;
                        else
                            a_W[I] = pcell_w.D_2 + Math.Max(pcell_w.F_2, 0.0);

                        // most east-side boundary conditions
                        if (I == region.N - 1)
                            a_E[I] = 0.0;
                        else
                            a_E[I] = pcell_e.D_2 + Math.Max(0.0, - pcell_e.F_2);
                        break;
                    case DifferencingScheme.Hybrid:
                        // most west-side boundary conditions
                        if (I == 0)
                            a_W[I] = 0.0;
                        else
                            a_W[I] = Math.Max(Math.Max(pcell_w.F_2, pcell_w.D_2 + pcell_w.F_2 / 2.0), 0.0);

                        // most east-side boundary conditions
                        if (I == region.N - 1)
                            a_E[I] = 0.0;
                        else
                            a_E[I] = Math.Max(Math.Max(- pcell_e.F_2, pcell_e.D_2 - pcell_e.F_2 / 2.0), 0.0);
                        break;
                    default:
                        throw CException.Show(CException.Type.UnsupportedKeyword);
                }

                // 2. a_P_1 & a_P_0 calculation
                a_P_1[I] = cell.rho * cell.c_p * cell.DV / sd.Dt;
                a_P_0[I] = cell_0.rho * cell_0.c_p * cell_0.DV / sd.Dt;
                double F_w, F_e;

                // 3. S_T & S_B calculation
                S_T[I] = - cell.A_n * cell.h_g - cell.A_n * ST_SD.Sigma * cell.epsilon_g * Math.Pow(cell.T, 3.0);
                S_B[I] = cell.A_n * cell.h_g * cell.T_n + cell.A_n * ST_SD.Sigma * cell.alpha_g * Math.Pow(cell.T_n, 4.0) - (cell.S_U * cell.u + cell.S_B_1) * Math.Abs(cell.u) - region.h_inf * cell.A_n * (cell.T_n - region.T_inf);

                // 4. F_w * F_e calculation
                F_w = pcell_w.F_2;
                F_e = pcell_e.F_2;

                // 5. a_P calculation
                if (I == 0)
                    a_P[I] = a_E[I] + a_P_1[I] - S_T[I] + F_e + pcell_w.k * pcell_w.A_c / cell.Dx_w;
                else if (I == region.N - 1)
                    a_P[I] = a_W[I] + a_P_1[I] - S_T[I] - F_w + pcell_e.k * pcell_e.A_c / cell.Dx_e;
                else
                    a_P[I] = a_W[I] + a_E[I] + a_P_1[I] - S_T[I] + F_e - F_w;

                // 6. b Calculation
                b[I] = a_P_0[I] * cell_0.T
                        + (cell_0.rho * Math.Pow(cell_0.u, 2.0) / 2.0 - cell.rho * Math.Pow(cell.u, 2.0) / 2.0 + cell.p - cell_0.p) * cell.DV / sd.Dt
                        + pcell_w.F_2 / pcell_w.c_p * Math.Pow(pcell_w.u, 2.0) / 2.0 - pcell_e.F_2 / pcell_e.c_p * Math.Pow(pcell_e.u, 2.0) / 2.0
                        + S_B[I];

                if (I == 0)
                {
                    switch (dir)
                    {
                        case ST_SD.FlowDirection.WestToEast:
                            b[I] += pcell_w.F_2 * pcell_w.T + pcell_w.k * pcell_w.A_c * pcell_w.T / cell.Dx_w;
                            break;
                        case ST_SD.FlowDirection.EastToWest:
                            a_P[I] += - F_w - pcell_w.k * pcell_w.A_c / cell.Dx_w;
                            break;
                        default:
                            throw CException.Show(CException.Type.UnsupportedKeyword);
                    }
                }
                else if (I == region.N - 1)
                {
                    switch (dir)
                    {
                        case ST_SD.FlowDirection.WestToEast:
                            a_P[I] += F_e - pcell_e.k * pcell_e.A_c / cell.Dx_e;
                            break;
                        case ST_SD.FlowDirection.EastToWest:
                            b[I] += - pcell_e.F_2 * pcell_e.T + pcell_e.k * pcell_e.A_c * pcell_e.T / cell.Dx_e;
                            break;
                        default:
                            throw CException.Show(CException.Type.UnsupportedKeyword);
                    }
                }
                else
                    b[I] += 0.0;
            }

            T_new = TDMA(a_P, a_E, a_W, b);

            for (int I = 0; I < region.N; I++)
            {
                CCell cell = region.Cells[I];
                cell.T = T_new[I];
                cell.a_W = a_W[I];
                cell.a_E = a_E[I];
                cell.a_P = a_P[I];
                cell.a_P_0 = a_P_1[I];
                cell.S_T = S_T[I];
                cell.S_B_2 = S_B[I];
                cell.b = b[I];
            }
        }

        private static void SolidSolving(CRegion region)
        {
            ST_SD sd = ST_SD.GetInstance();

            double[] T_new = new double[region.N];
            double[] a_W = new double[region.N];
            double[] a_E = new double[region.N];
            double[] a_P = new double[region.N];
            double[] a_P_1 = new double[region.N];
            double[] a_P_0 = new double[region.N];

            double[] S_T = new double[region.N];
            double[] S_B = new double[region.N];

            double[] b = new double[region.N];

            for (int I = 0; I < region.N; I++)
            {
                CCell cell = region.Cells[I];
                CCell cell_0 = region.Region0.Cells[I];
                CPseudoCell pcell_w = region.PCells[I];
                CPseudoCell pcell_e = region.PCells[I + 1];

                // 1. a_W & a_E calculation
                if (I == 0)
                {
                    a_W[I] = 0.0;
                    a_E[I] = pcell_e.D_2;
                }
                else if (I == region.N - 1)
                {
                    a_W[I] = pcell_w.D_2;
                    a_E[I] = 0.0;
                }
                else
                {
                    a_W[I] = pcell_w.D_2;
                    a_E[I] = pcell_e.D_2;
                }

                // 2. a_P_1 & a_P_0 calculation
                a_P_1[I] = cell.rho * cell.c_p * cell.DV / sd.Dt;
                a_P_0[I] = cell_0.rho * cell_0.c_p * cell_0.DV / sd.Dt;

                // 3. S_T & S_B calculation
                S_T[I] = -cell.k * cell.A_s / cell.Dx_s;
                S_B[I] = cell.k * cell.A_s / cell.Dx_s * cell.T_s - region.h_inf * cell.A_n * (cell.T_n - region.T_inf); //

                // 4. a_P calculation
                a_P[I] = a_W[I] + a_E[I] - S_T[I] + a_P_1[I];

                // 5. b calculation
                b[I] = a_P_0[I] * cell_0.T + S_B[I];
            }

            T_new = TDMA(a_P, a_E, a_W, b);

            for (int I = 0; I < region.N; I++)
            {
                CCell cell = region.Cells[I];
                cell.T = T_new[I];
                cell.a_W = a_W[I];
                cell.a_E = a_E[I];
                cell.a_P = a_P[I];
                cell.a_P_0 = a_P_1[I];
                cell.S_T = S_T[I];
                cell.S_B_2 = S_B[I];
                cell.b = b[I];
            }
        }
       

        /// <summary>
        /// Model method for solving the TriDiagonal Matrix Alorighm (TDMA).
        /// =   -   -
        /// A · φ = b
        /// rewriting the above equation with N * N matrix A, N * 1 matrix φ, and N * 1 matrix b in detail,
        /// ┌─────────────────────────────────────────────────────────────┐   ┌────────┐   ┌────────┐
        /// │ a_P(0)    a_E(0)                                            │   │  φ(0)  │   │  b(0)  │
        /// │ a_W(1)    a_P(1)    a_E(1)                                  │   │  φ(1)  │   │  b(1)  │
        /// │           a_W(2)    a_P(2)    a_E(2)                        │   │  φ(2)  │   │  b(2)  │
        /// │                 ...       ...       ...                     │ · │  ...   │ = │  ...   │
        /// │                     a_W(i)    a_P(i)    a_E(i)              │   │  φ(i)  │   │  b(i)  │
        /// │                           ...       ...       ...           │   │  ...   │   │  ...   │
        /// │                                         a_W(N-1)  a_P(N-1)  │   │ φ(N-1) │   │ b(N-1) │
        /// └─────────────────────────────────────────────────────────────┘   └────────┘   └────────┘
        /// rules you must follow
        ///    1. size of all arrays has to be N.
        ///    2. all components have to be positive.
        ///    3. a_W(0) = 0.0
        ///    4. a_E(N-1) = 0.0
        /// </summary>
        /// <param name="a_P">Array of a_P.</param>
        /// <param name="a_E">Array of a_E.</param>
        /// <param name="a_W">Array of a_W.</param>
        /// <param name="b">Array of b.</param>
        /// <returns>Array of φ.</returns>
        private static double[] TDMA(double[] a_P, double[] a_E, double[] a_W, double[] b)
        {
            // define data baskets.
            int nData = b.Length;
            double[] P = new double[nData];
            double[] Q = new double[nData];
            double[] phi = new double[nData];

            // do forward-substitution.
            P[0] = a_E[0] / a_P[0];
            Q[0] = b[0] / a_P[0];
            for (int i = 1; i < nData - 1; i++)
            {
                P[i] = (a_E[i]) / (a_P[i] - a_W[i] * P[i - 1]);
                Q[i] = (b[i] + a_W[i] * Q[i - 1]) / (a_P[i] - a_W[i] * P[i - 1]);
            }
            P[nData - 1] = (a_E[nData - 1]) / (a_P[nData - 1] - a_W[nData - 1] * P[nData - 1 - 1]);
            Q[nData - 1] = (b[nData - 1] + a_W[nData - 1] * Q[nData - 1 - 1]) / (a_P[nData - 1] - a_W[nData - 1] * P[nData - 1 - 1]);

            // do back-substitution.
            phi[nData - 1] = Q[nData - 1];
            for (int i = nData - 1 - 1; i >= 0; i--)
                phi[i] = P[i] * phi[i + 1] + Q[i];

            // return value.
            return phi;
        }

        /// <summary>
        /// Min/Max
        /// </summary>
        /// <param name="phi_old"></param>
        /// <param name="phi_new"></param>
        /// <returns></returns>
        public static double Residual(List<double> phi_old, List<double> phi_new)
        {
            List<double> dphi = new List<double>();

            for (int i = 0; i < phi_new.Count; i++)
                dphi.Add(Math.Abs((phi_old[i] - phi_new[i]) / phi_new[i]));

            return dphi.Max();
        }
    }
}
