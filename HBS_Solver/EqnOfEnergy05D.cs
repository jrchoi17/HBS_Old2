using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS_Shared;

namespace HBS_Solver
{
    public class EqnOfEnergy05D
    {
        public static void Solving(CRegion region)
        {
            ST_SD sd = ST_SD.GetInstance();

            switch (region.Type)
            {
                case CRegion.RegionType.Fluid:
                    SolvingFluidOnly(region);
                    break;
                case CRegion.RegionType.Solid:
                case CRegion.RegionType.Wall:
                    SolvingSolidOnly(region);
                    break;
                default:
                    throw CException.Show(CException.Type.UnsupportedKeyword);
            }
        }

        private static void SolvingFluidOnly(CRegion fluid)
        {
            for (int I = 0; I < fluid.N; I++)
            {
                CCell cell = fluid.Cells[I];

                if (cell.Parent.h_inf != 0.0)
                    CalTemperatureFluid_Inf(cell);
            }
        }

        private static void SolvingSolidOnly(CRegion solid)
        {
            for (int I = 0; I < solid.N; I++)
            {
                CCell cell = solid.Cells[I];

                if (solid.h_inf != 0)
                    CalTemperatureSolid_Inf(cell);
            }
        }

        public static void Solving(CRegion fluid, CRegion solid)
        {
            ST_SD sd = ST_SD.GetInstance();

            for (int I = 0; I < fluid.N; I++)
            {
                List<CCell> cells = fluid.Cells[I].GetSouthCells();
                CCell cell_f, cell_s;

                switch (cells.Count)
                {
                    case 1:     // fluid only
                        cell_f = cells[0];

                        if (fluid.h_inf == 0.0)
                            break;
                        else 
                        {
                            if (fluid.h_inf != 0.0)
                                CalTemperatureFluid_Inf(cell_f);
                            break;
                        }
                    case 2:     // fluid + solid
                        cell_f = cells[0];
                        cell_s = cells[1];

                        if (solid.h_inf == 0.0)   // fluid -> solid
                        {
                            if (cell_f.h_g == 0.0)
                                break;
                            else
                                CalTemperatureFluid_Solid(cell_f, cell_s);
                        }
                        else // fluid -> solid -> inf
                        {
                            if (cell_f.h_g == 0.0)
                                break;
                            else
                                CalTemperatureFluid_Solid_Inf(cell_f, cell_s);
                        }
                        break;
                    default:
                        throw CException.Show(CException.Type.UnsupportedKeyword);
                }
            }
        }

        public static void Solving(CRegion fluid, CRegion solid, CRegion wall)
        {
            ST_SD sd = ST_SD.GetInstance();

            for (int I = 0; I < fluid.N; I++)
            {
                List<CCell> cells = fluid.Cells[I].GetSouthCells();
                CCell cell_f, cell_s, cell_w;

                switch(cells.Count)
                {
                    case 1:     // fluid only
                        cell_f = cells[0];

                        if (fluid.h_inf == 0.0)
                            break;
                        else
                        {
                            if (fluid.h_inf != 0.0)
                                CalTemperatureFluid_Inf(cell_f);
                            break;
                        }
                    case 2: // fluid + solid (or wall)
                        cell_f = cells[0];
                        cell_s = cells[1];

                        if (cell_s.Parent.h_inf == 0.0)     // fluid -> solid
                        {
                            if (cell_f.h_g == 0.0)
                                break;
                            else
                                CalTemperatureFluid_Solid(cell_f, cell_s);
                        }
                        else    // fluid -> solid -> inf
                        {
                            if (cell_f.h_g == 0.0)
                                break;
                            else
                                CalTemperatureFluid_Solid_Inf(cell_f, cell_s);
                        }
                        break;
                    case 3: // fluid + solid + wall
                        cell_f = cells[0];
                        cell_s = cells[1];
                        cell_w = cells[2];

                        if (wall.h_inf == 0.0) // fluid -> solid -> wall
                        {
                            if (cell_f.h_g == 0.0)
                                break;
                            else
                                CalTemperatureFluid_Solid_Wall(cell_f, cell_s, cell_w);
                        }
                        else // fluid -> solid -> wall -> inf
                        {
                            if (cell_f.h_g == 0.0)
                                break;
                            else
                                CalTemperatureFluid_Solid_Wall_Inf(cell_f, cell_s, cell_w);
                        }
                        break;
                    default:
                        throw CException.Show(CException.Type.UnsupportedKeyword);
                }
            }
        }

        private static double R_cond(double k, double A, double Dx)
        {
            double denominator = k * A;

            if (denominator == 0.0)
                return 0.0;
            else
                return Dx / denominator;
        }

        private static double R_conv(double h, double A)
        {
            double denominator = h * A;

            if (denominator == 0.0)
                return double.PositiveInfinity;
            else
                return 1.0 / denominator;
        }

        private static double R_rad(double T, double T_w, double epsilon, double alpha)
        {
            double denominator = ST_SD.Sigma * (epsilon * Math.Pow(T, 4.0) - alpha * Math.Pow(T_w, 4.0));
            if (denominator == 0)
                return double.PositiveInfinity;
            else
                return (T - T_w) / denominator;
        }

        private static double R_conv_rad(double R_conv, double R_rad)
        {
            return 1.0 / (1.0 / R_conv + 1.0 / R_rad);
        }

        private static double R_cond_conv_rad(double R_cond, double R_conv, double R_rad)
        {
            return 1.0 / (1.0 / R_cond + 1.0 / R_conv + 1.0 / R_rad);
        }

        private static void CalTemperatureFluid_Inf(CCell cell)
        {
            CRegion fluid = cell.Parent;

            double R_f_cond = R_cond(cell.k, cell.A_n, cell.Dx_n);
            double R_f_conv = R_conv(cell.h_g, cell.A_n);
            double R_f_rad = R_rad(cell.T, cell.T_n, cell.epsilon_g, cell.alpha_g);
            double R_f_cond_conv_rad = R_cond_conv_rad(R_f_cond, R_f_conv, R_f_rad);
            double R_f_inf = R_conv(fluid.h_inf, cell.A_n);
            double R_tot = R_f_cond_conv_rad + R_f_inf;

            if (R_tot == double.PositiveInfinity)
                cell.q3 = 0.0;
            else
            {
                // store volumetric heat source (q''' [W/m^3]) to each cell.
                double q = (cell.T - fluid.T_inf) / R_tot;
                cell.q3 = -q / cell.DV;

                // calculate wall temperature(s)
                cell.T_s = cell.T;
                cell.T_n = fluid.T_inf + q * R_f_inf;
            }
        }

        private static void CalTemperatureSolid_Inf(CCell cell)
        {
            CRegion solid = cell.Parent;

            double R_s_cond = R_cond(cell.k, cell.A_n, cell.Dx_n);
            double R_s_inf = R_conv(solid.h_inf, cell.A_n);
            double R_tot = R_s_cond + R_s_inf;

            if (R_tot == double.PositiveInfinity)
                cell.q3 = 0.0;
            else
            {
                // store volumetric heat source (q''' [W/m^3]) to each cell.
                double q = (cell.T - solid.T_inf) / R_tot;
                cell.q3 = -q / cell.DV;

                // calculate wall temperature(s)
                cell.T_s = cell.T;
                cell.T_n = solid.T_inf + q * R_s_inf;
            }
        }

        private static void CalTemperatureFluid_Solid(CCell cell_f, CCell cell_s)
        {
            double R_f_conv = R_conv(cell_f.h_g, cell_f.A_n);
            double R_f_rad = R_rad(cell_f.T, cell_f.T_n, cell_f.epsilon_g, cell_f.alpha_g);
            double R_f_conv_rad = R_conv_rad(R_f_conv, R_f_rad);
            double R_s_cond1 = R_cond(cell_s.k, cell_s.A_s, cell_s.Dx_s);
            double R_tot = R_f_conv_rad + R_s_cond1;

            if (R_tot == double.PositiveInfinity)
            {
                cell_f.q3 = 0.0;
                cell_s.q3 = 0.0;
                return;
            }
            else
            {
                // store volumetric heat source (q''' [W/m^3]) to each cell.
                double q = (cell_f.T - cell_s.T) / R_tot;
                cell_f.q3 = -q / cell_f.DV;
                cell_s.q3 = q / cell_s.DV;

                // calculate wall temperature(s)
                cell_f.T_s = cell_f.T;
                cell_f.T_n = cell_s.T + q * R_s_cond1;
                cell_s.T_s = cell_f.T_n;
                cell_s.T_n = cell_s.T;
            }
        }

        private static void CalTemperatureFluid_Solid_Inf(CCell cell_f, CCell cell_s)
        {
            CRegion solid = cell_s.Parent;

            double R_f_conv = R_conv(cell_f.h_g, cell_f.A_n);
            double R_f_rad = R_rad(cell_f.T, cell_f.T_n, cell_f.epsilon_g, cell_f.alpha_g);
            double R_f_conv_rad = R_conv_rad(R_f_conv, R_f_rad);
            double R_s_cond1 = R_cond(cell_s.k, cell_s.A_s, cell_s.Dx_s);
            double R_s_cond2 = R_cond(cell_s.k, cell_s.A_n, cell_s.Dx_n);
            double R_s_inf = R_conv(solid.h_inf, cell_s.A_n);
            double R_tot = R_f_conv_rad + R_s_cond1 + R_s_cond2 + R_s_inf;

            if (R_tot == double.PositiveInfinity)
            {
                cell_f.q3 = 0.0;
                cell_s.q3 = 0.0;
            }
            else
            {
                // store volumetric heat source (q''' [W/m^3]) to each cell.
                double q = (cell_f.T - solid.T_inf) / R_tot;
                cell_f.q3 = -q / cell_f.DV;
                cell_s.q3 = q / cell_s.DV;

                // calculate wall temperature(s)
                cell_f.T_s = cell_f.T;
                cell_f.T_n = cell_s.T + q * R_s_cond1;
                cell_s.T_s = cell_f.T_n;
                cell_s.T_n = solid.T_inf + q * R_s_inf;
            }
        }

        private static void CalTemperatureFluid_Solid_Wall(CCell cell_f, CCell cell_s, CCell cell_w)
        {
            double R_f_conv = R_conv(cell_f.h_g, cell_f.A_n);
            double R_f_rad = R_rad(cell_f.T, cell_f.T_n, cell_f.epsilon_g, cell_f.alpha_g);
            double R_f_conv_rad = R_conv_rad(R_f_conv, R_f_rad);
            double R_s_cond1 = R_cond(cell_s.k, cell_s.A_s, cell_s.Dx_s);
            double R_s_cond2 = R_cond(cell_s.k, cell_s.A_n, cell_s.Dx_n);
            double R_w_cond1 = R_cond(cell_w.k, cell_w.A_s, cell_w.Dx_s);
            double R_tot = R_f_conv_rad + R_s_cond1 + R_s_cond2 + R_w_cond1;

            if (R_tot == double.PositiveInfinity)
            {
                cell_f.q3 = 0.0;
                cell_s.q3 = 0.0;
                cell_w.q3 = 0.0;
            }
            else
            {
                // store volumetric heat source (q''' [W/m^3]) to each cell.
                double q = (cell_f.T - cell_w.T) / R_tot;
                cell_f.q3 = -q / cell_f.DV;
                cell_s.q3 = q / cell_s.DV;
                cell_s.q3 = q / cell_w.DV;

                // calculate wall temperature(s)
                cell_f.T_s = cell_f.T;
                cell_f.T_n = cell_s.T + q * R_s_cond1;
                cell_s.T_s = cell_f.T_n;
                cell_s.T_n = cell_w.T + q * R_w_cond1;
                cell_w.T_s = cell_s.T_n;
                cell_w.T_n = cell_w.T;
            }
        }

        private static void CalTemperatureFluid_Solid_Wall_Inf(CCell cell_f, CCell cell_s, CCell cell_w)
        {
            CRegion wall = cell_w.Parent;

            double R_f_conv = R_conv(cell_f.h_g, cell_f.A_n);
            double R_f_rad = R_rad(cell_f.T, cell_f.T_n, cell_f.epsilon_g, cell_f.alpha_g);
            double R_f_conv_rad = R_conv_rad(R_f_conv, R_f_rad);
            double R_s_cond1 = R_cond(cell_s.k, cell_s.A_s, cell_s.Dx_s);
            double R_s_cond2 = R_cond(cell_s.k, cell_s.A_n, cell_s.Dx_n);
            double R_w_cond1 = R_cond(cell_w.k, cell_w.A_s, cell_w.Dx_s);
            double R_w_cond2 = R_cond(cell_w.k, cell_w.A_n, cell_w.Dx_n);
            double R_w_inf = R_conv(wall.h_inf, cell_w.A_n);
            double R_tot = R_f_conv_rad + R_s_cond1 + R_s_cond2 + R_w_cond1 + R_w_cond2 + R_w_inf;

            if (R_tot == double.PositiveInfinity)
            {
                cell_f.q3 = 0.0;
                cell_s.q3 = 0.0;
                cell_w.q3 = 0.0;
            }
            else
            {
                // store volumetric heat source (q''' [W/m^3]) to each cell.
                double q = (cell_f.T - wall.T_inf) / R_tot;
                cell_f.q3 = -q / cell_f.DV;
                cell_s.q3 = q / cell_s.DV;
                cell_s.q3 = q / cell_w.DV;

                // calculate wall temperature(s)
                cell_f.T_s = cell_f.T;
                cell_f.T_n = cell_s.T + q * R_s_cond1;
                cell_s.T_s = cell_f.T_n;
                cell_s.T_n = cell_w.T + q * R_w_cond1;
                cell_w.T_s = cell_s.T_n;
                cell_w.T_n = wall.T_inf + q * R_w_inf;
            }
        }
    }
}
