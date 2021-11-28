using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS_Solver;
using HBS_Shared;
using System.Diagnostics;

namespace HBS_Solver_Console_Exe
{
    partial class Program
    {
        /*
        static void Main_for_Fluid_Solid()
        {
            ST_SD sd = ST_SD.GetInstance();

            /// Set data from xml file
            {
                string filePath = @"SdFiles\\1D_simple_fluid-solid.xml";

                // Time parameter
                sd.SetDataFromXml_TimeParameter(filePath);

                // Time step parameter
                sd.SetDataFromXml_TimeStepParameter(filePath);

                // Inner iteration parameter
                sd.SetDataFromXml_InnerIterationParameter(filePath);

                // Porous parameter
                sd.SetDataFromXml_PorousParameter(filePath);

                // Geometric parameter
                sd.SetDataFromXml_GeometricParameter(filePath);

                if (sd.AllRegions == null)
                    sd.AllRegions = new List<CRegion>();

                // Fluid region
                sd.SetDataFromXml_FluidRegion(filePath);

                // Solid region
                sd.SetDataFromXml_SolidRegion(filePath);
            }

            /// Initialization
            {
                // fluid region
                Initialization_FluidRegion_Old();

                // solid region
                Initialization_SolidRegion();

                // connect region each other
                sd.FluidRegion.SetRegionConnectivity(sd.AllRegions);
                sd.SolidRegion.SetRegionConnectivity(sd.AllRegions);

                // set a boundary condition of the temperature.
                sd.FluidRegion.SetWestBc_Temperature(1300.0); // When the initialization has occurred, please set the temperature only.
            }

            //for (int t = 0; t < ud.T_Max; t++)
            for (int t = 0; t < 120; t++)
            {
                // Update time
                sd.FluidRegion.UpdateTime();
                sd.SolidRegion.UpdateTime();

                // Inner iterations
                for (int i = 0; i < sd.I_Max; i++)
                {
                    SolveContinuityEquation_Old();

                    SolveMomenumEquation_Old();

                    /// Solve energy equation...
                    {
                        // set a boundary condition of the pressure.
                        sd.FluidRegion.SetWestBc_Temperature(1300.0);

                        // set a volumetric heat source and sink as 0 initially.
                        sd.FluidRegion.SetVolumetricHeatSource(0.0);

                        // get data before calculating the energy equation.
                        List<double> T_fluid_old = sd.FluidRegion.GetTemperature(CCell.CellType.Cell);
                        List<double> T_solid_old = sd.SolidRegion.GetTemperature(CCell.CellType.Cell);

                        // solve energy equation.
                        EqnOfEnergy1D.Fluid.SolveStgCellFromWest(sd.FluidRegion, sd.dt, EqnOfEnergy1D.DifferencingScheme.Hybrid);
                        EqnOfEnergy1D.Solid.SolveStgCell(sd.SolidRegion, sd.dt);

                        // wall temperature calculation
                        //EqnOfEnergy05D.CalculateWallTemperature(sd.FluidRegion, sd.SolidRegion);
                        EqnOfEnergy05D.CalculateWallTemperature_StgCell(sd.FluidRegion, sd.SolidRegion);

                        // update temperature values staggered cell -> cell.
                        sd.FluidRegion.UpdateTemperatureFromWest(CCell.CellType.Cell);
                        sd.SolidRegion.UpdateTemperatureFromWest(CCell.CellType.Cell);

                        // get data after calculating the energy equation.
                        List<double> T_fluid_new = sd.FluidRegion.GetTemperature(CCell.CellType.Cell);
                        List<double> T_solid_new = sd.SolidRegion.GetTemperature(CCell.CellType.Cell);

                        // get residual
                        double residual_fluid = EqnOfEnergy1D.Residual(T_fluid_old, T_fluid_new);
                        double residual_solid = EqnOfEnergy1D.Residual(T_solid_old, T_solid_new);

                        if (residual_fluid < ST_SD.epsilon_a && residual_solid < ST_SD.epsilon_a)
                        {
#if (DEBUG)
                            Debug.WriteLine("Time step = " + t.ToString() + " Inner iteration = " + i.ToString());
#endif
                            break;
                        }
                    }

                    /// Solve equation of states...
                    SolveEquationOfState();
                }

                WriteFilesForDebugging(t);
            } // end of time step
        } // end of time loop
        */
        /*static void SolveContinuityEquation_Old()
        {
            ST_SD sd = ST_SD.GetInstance();

            // set a boundary condition of the velocity.
            sd.FluidRegion.SetWestBc_Velocity(0.1);

            // solve continuity equation.
            EqnOfContinuity1D.SolveStgCellFromWest(sd.FluidRegion, sd.dt);

            // update velocity values cell -> staggered cell.
            //sd.FluidRegion.UpdateVelocityFromWest();
        }*/

        /*static void SolveMomenumEquation_Old()
        {
            ST_SD sd = ST_SD.GetInstance();

            // set a boundary condition of the pressure.
            sd.FluidRegion.SetWestBc_Pressure(0.0);

            // solve momentum equation.
            EqnOfMomentum1D.SolveStgCellFromWest(sd.FluidRegion, sd.dt);

            // update pressure values cell -> staggered cell.
            //sd.FluidRegion.UpdatePressureFromWest();
        }*/
    }
}
