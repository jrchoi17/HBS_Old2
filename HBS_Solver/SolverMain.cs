using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using HBS_Shared;


namespace HBS_Solver
{
    public class SolverMain
    {
        public void Excute()
        {
            ST_SD sd = ST_SD.GetInstance();

            // set data from xml file
            sd.SetDataFromXml(@"SDFiles\\1D_fluid_solid_wall.xml");
            // set data from Ud
            //sd.SetDataFromUd();

            // initialization all data
            Initialization();

            for (int cycle = 1; cycle <= sd.C_max; cycle++)
            {
                sd.C += 1;

                /// Dual-time-iterative solving
                for (int timeStep = 1; timeStep <= sd.T_Max; timeStep++)
                {
                    sd.T = timeStep;
                    sd.t += sd.Dt;

                    // update time
                    UpdateTime();

                    // inner iterations
                    for (int iteration = 0; iteration < sd.I_Max; iteration++)
                    {
                        SolveEquationOfState();

                        SolveContinuityEquation();

                        SolveMomentumEquation();

                        if (SolveEnergyEquation() || iteration == sd.I_Max - 1)
                        {
                            Debug.WriteLine("cycle = " + cycle + ", t = " + timeStep);
                            Debug.WriteLine("inner iter = " + (iteration + 1));
                            break;
                        }
                    }

                    // write region data
                    WriteFiles(timeStep);
                }
            }

            Console.WriteLine("End of simulation.");
        }


        private static void Initialization()
        {
            ST_SD sd = ST_SD.GetInstance();

            // fluid region
            if (sd.FluidRegion != null)
                EqnOfState.UpdateAll(sd.FluidRegion);

            // solid region
            if (sd.SolidRegion != null)
                EqnOfState.UpdateAll(sd.SolidRegion);

            // wall region
            if (sd.WallRegion != null)
                EqnOfState.UpdateAll(sd.WallRegion);

            // connect region each other
            if (sd.FluidRegion != null)
            {
                // set a boundary condition of the temperature.
                // when the initialization has occurred, please set the temperature only.
                sd.FluidRegion.SetBcTemperature(1300.0, ST_SD.FlowDirection.WestToEast);
            }

            if (sd.FluidRegion != null && sd.SolidRegion != null)
            {
                sd.FluidRegion.SetRegionConnectivity(sd.AllRegions);
                sd.SolidRegion.SetRegionConnectivity(sd.AllRegions);
            }
            else if (sd.FluidRegion != null && sd.SolidRegion != null && sd.WallRegion != null)
            {
                sd.FluidRegion.SetRegionConnectivity(sd.AllRegions);
                sd.SolidRegion.SetRegionConnectivity(sd.AllRegions);
                sd.WallRegion.SetRegionConnectivity(sd.AllRegions);
            }
        }

        private static void UpdateTime()
        {
            ST_SD sd = ST_SD.GetInstance();

            if (sd.FluidRegion != null)
                sd.FluidRegion.UpdateTime();

            if (sd.SolidRegion != null)
                sd.SolidRegion.UpdateTime();

            if (sd.WallRegion != null)
                sd.WallRegion.UpdateTime();

            // clear values before starting the inner iterations
            if (sd.FluidRegion != null)
                sd.FluidRegion.SetVolumetricHeatSource(0.0);
            if (sd.SolidRegion != null)
                sd.SolidRegion.SetVolumetricHeatSource(0.0);
            if (sd.WallRegion != null)
                sd.WallRegion.SetVolumetricHeatSource(0.0);
        }

        private static void SolveContinuityEquation()
        {
            ST_SD sd = ST_SD.GetInstance();

            if (sd.FluidRegion != null)
            {
                sd.FluidRegion.SetDataToPseudoCellFromCell(CCell.DataOrder.rho);
                sd.FluidRegion.SetDataToPseudoCellFromCell(CCell.DataOrder.u);

                switch (sd.FlowDir)
                {
                    case ST_SD.FlowDirection.WestToEast:
                        sd.FluidRegion.SetBcVelocity(sd.Velocity_Now, ST_SD.FlowDirection.WestToEast);
                        EqnOfContinuity1D.Solving(sd.FluidRegion, ST_SD.FlowDirection.WestToEast);
                        break;
                    case ST_SD.FlowDirection.EastToWest:
                        sd.FluidRegion.SetBcVelocity(sd.Velocity_Now, ST_SD.FlowDirection.EastToWest);
                        EqnOfContinuity1D.Solving(sd.FluidRegion, ST_SD.FlowDirection.EastToWest);
                        break;
                    default:
                        throw CException.Show(CException.Type.UnsupportedKeyword);
                }


                /// East side


                sd.FluidRegion.SetDataToCellFromPseudoCell(CCell.DataOrder.u);
            }
        }

        private static void SolveMomentumEquation()
        {
            ST_SD sd = ST_SD.GetInstance();

            if (sd.FluidRegion != null)
            {
                sd.FluidRegion.SetDataToPseudoCellFromCell(CCell.DataOrder.mu);

                switch (sd.FlowDir)
                {
                    case ST_SD.FlowDirection.WestToEast:
                        sd.FluidRegion.SetBcPressure(sd.Pressure_Now, ST_SD.FlowDirection.WestToEast);
                        EqnOfMomentum1D.Solving(sd.FluidRegion, ST_SD.FlowDirection.WestToEast);
                        break;
                    case ST_SD.FlowDirection.EastToWest:
                        sd.FluidRegion.SetBcPressure(sd.Pressure_Now, ST_SD.FlowDirection.EastToWest);
                        EqnOfMomentum1D.Solving(sd.FluidRegion, ST_SD.FlowDirection.EastToWest);
                        break;
                    default:
                        throw CException.Show(CException.Type.UnsupportedKeyword);
                }

                sd.FluidRegion.SetDataToPseudoCellFromCell(CCell.DataOrder.p);
            }
        }

        private static bool SolveEnergyEquation()
        {
            ST_SD sd = ST_SD.GetInstance();

            List<double> T_fluid_old, T_solid_old, T_wall_old;
            List<double> T_fluid_new, T_solid_new, T_wall_new;
            double R_fluid = 1e+10, R_solid = 1e+10, R_wall = 1e+10;

            if (sd.FluidRegion != null)
            {
                // pseudo-cell data update
                sd.FluidRegion.SetDataToPseudoCellFromCell(CCell.DataOrder.c_p);
                sd.FluidRegion.SetDataToPseudoCellFromCell(CCell.DataOrder.k);

                // get data before calculating the energy equation.
                T_fluid_old = sd.FluidRegion.GetSelectedData(CCell.DataOrder.T);

                switch (sd.FlowDir)
                {
                    case ST_SD.FlowDirection.WestToEast:
                        sd.FluidRegion.SetBcTemperature(sd.Temperature_Now, ST_SD.FlowDirection.WestToEast);
                        EqnOfEnergy1D.Solving(sd.FluidRegion, EqnOfEnergy1D.DifferencingScheme.Hybrid, ST_SD.FlowDirection.WestToEast);
                        break;
                    case ST_SD.FlowDirection.EastToWest:
                        sd.FluidRegion.SetBcTemperature(sd.Temperature_Now, ST_SD.FlowDirection.EastToWest);
                        EqnOfEnergy1D.Solving(sd.FluidRegion, EqnOfEnergy1D.DifferencingScheme.Hybrid, ST_SD.FlowDirection.EastToWest);
                        break;
                    default:
                        throw CException.Show(CException.Type.UnsupportedKeyword);
                }

                // get data after calculating the energy equation.
                T_fluid_new = sd.FluidRegion.GetSelectedData(CCell.DataOrder.T);

                // get residual
                R_fluid = EqnOfEnergy1D.Residual(T_fluid_old, T_fluid_new);
            }

            if (sd.SolidRegion != null)
            {
                // pseudo-cell data update
                sd.SolidRegion.SetDataToPseudoCellFromCell(CCell.DataOrder.c_p);
                sd.SolidRegion.SetDataToPseudoCellFromCell(CCell.DataOrder.k);

                // get data before calculating the energy equation.
                T_solid_old = sd.SolidRegion.GetSelectedData(CCell.DataOrder.T);

                EqnOfEnergy1D.Solving(sd.SolidRegion);

                // get data after calculating the energy equation.
                T_solid_new = sd.SolidRegion.GetSelectedData(CCell.DataOrder.T);

                // get residual
                R_solid = EqnOfEnergy1D.Residual(T_solid_old, T_solid_new);
            }

            if (sd.WallRegion != null)
            {
                // pseudo-cell data update
                sd.WallRegion.SetDataToPseudoCellFromCell(CCell.DataOrder.c_p);
                sd.WallRegion.SetDataToPseudoCellFromCell(CCell.DataOrder.k);

                // get data before calculating the energy equation.
                T_wall_old = sd.WallRegion.GetSelectedData(CCell.DataOrder.T);

                EqnOfEnergy1D.Solving(sd.WallRegion);

                // get data after calculating the energy equation.
                T_wall_new = sd.WallRegion.GetSelectedData(CCell.DataOrder.T);

                // get residual
                R_wall = EqnOfEnergy1D.Residual(T_wall_old, T_wall_new);
            }

            double stoppingCriterion = ST_SD.epsilon_a;

            if (sd.FluidRegion != null && sd.SolidRegion == null && sd.WallRegion == null)
            {
                EqnOfEnergy05D.Solving(sd.FluidRegion);

                if (R_fluid < stoppingCriterion)
                {
                    Debug.WriteLine("R_fluid: " + R_fluid);
                    return true;
                }
            }


            if (sd.FluidRegion == null && sd.SolidRegion != null && sd.WallRegion == null)
            {
                EqnOfEnergy05D.Solving(sd.SolidRegion);

                if (R_solid < stoppingCriterion)
                {
                    Debug.WriteLine("R_solid: " + R_solid);
                    return true;
                }
            }


            if (sd.FluidRegion == null && sd.SolidRegion == null && sd.WallRegion != null)
            {
                EqnOfEnergy05D.Solving(sd.WallRegion);

                if (R_wall < stoppingCriterion)
                {
                    Debug.WriteLine("R_wall: " + R_wall);
                    return true;
                }
            }

            if (sd.FluidRegion != null && sd.SolidRegion != null && sd.WallRegion == null)
            {
                EqnOfEnergy05D.Solving(sd.FluidRegion, sd.SolidRegion);

                if (R_fluid < stoppingCriterion && R_solid < stoppingCriterion)
                {
                    Debug.WriteLine("R_fluid: " + R_fluid);
                    Debug.WriteLine("R_solid: " + R_solid);
                    return true;
                }
            }

            if (sd.FluidRegion != null && sd.SolidRegion == null && sd.WallRegion != null)
            {
                EqnOfEnergy05D.Solving(sd.FluidRegion, sd.WallRegion);

                if (R_fluid < stoppingCriterion && R_wall < stoppingCriterion)
                {
                    Debug.WriteLine("R_fluid: " + R_fluid);
                    Debug.WriteLine("R_wall: " + R_wall);
                    return true;
                }
            }


            if (sd.FluidRegion != null && sd.SolidRegion != null && sd.WallRegion != null)
            {
                EqnOfEnergy05D.Solving(sd.FluidRegion, sd.SolidRegion, sd.WallRegion);

                if (R_fluid < stoppingCriterion && R_solid < stoppingCriterion && R_wall < stoppingCriterion)
                {
                    Debug.WriteLine("R_fluid: " + R_fluid);
                    Debug.WriteLine("R_solid: " + R_solid);
                    Debug.WriteLine("R_wall: " + R_wall);
                    return true;
                }
            }

            return false;
        }

        private static void SolveEquationOfState()
        {
            ST_SD sd = ST_SD.GetInstance();

            // fluid region
            if (sd.FluidRegion != null)
                EqnOfState.UpdateAll(sd.FluidRegion);

            // solid region
            if (sd.SolidRegion != null)
                EqnOfState.UpdateAll(sd.SolidRegion);

            // wall region
            if (sd.WallRegion != null)
                EqnOfState.UpdateAll(sd.WallRegion);
        }

        static void WriteFiles(int timestep)
        {
            ST_SD sd = ST_SD.GetInstance();

            // fluid region
            if (sd.FluidRegion != null)
                sd.FluidRegion.WriteRegionTemperature("fluid_" + ((sd.C - 1) * sd.T_Max + timestep).ToString() + ".txt", sd.t);

            // solid region
            if (sd.SolidRegion != null)
                sd.SolidRegion.WriteRegionTemperature("solid_" + ((sd.C - 1) * sd.T_Max + timestep).ToString() + ".txt", sd.t);

            // wall region
            if (sd.WallRegion != null)
                sd.WallRegion.WriteRegionTemperature("wall_" + ((sd.C - 1) * sd.T_Max + timestep).ToString() + ".txt", sd.t);
        }

        private static void WriteFilesForDebugging(int t)
        {
            ST_SD sd = ST_SD.GetInstance();

            //if ((t + 1) % 60 == 0)
            {
                if (sd.FluidRegion != null)
                    sd.FluidRegion.WriteRegionContents("fluid_cell_" + (t + 1).ToString() + ".txt");

                if (sd.SolidRegion != null)
                    sd.SolidRegion.WriteRegionContents("solid_cell_" + (t + 1).ToString() + ".txt");
            }
        }
    }
}
