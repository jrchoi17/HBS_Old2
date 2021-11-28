using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS_Shared;

namespace HBS_Solver
{
    public class EqnOfState
    {
        public static void UpdateAll(CRegion region)
        {
            UpdateDensity(region);
            UpdateSpecificHeat(region);
            UpdateThermalConductivity(region);
            UpdateDynamicViscosity(region);
        }

        public static void UpdateDensity(CRegion region)
        {
            foreach (CCell cell in region.Cells)
                cell.rho = region.Property[cell.Prop].Density(cell.T);
        }

        public static void UpdateSpecificHeat(CRegion region)
        {
            foreach (CCell cell in region.Cells)
                cell.c_p = region.Property[cell.Prop].SpecificHeat(cell.T);
        }

        public static void UpdateThermalConductivity(CRegion region)
        {
            foreach (CCell cell in region.Cells)
                cell.k = region.Property[cell.Prop].ThermalConductivity(cell.T);
        }

        public static void UpdateDynamicViscosity(CRegion region)
        {
            foreach (CCell cell in region.Cells)
                cell.mu = region.Property[cell.Prop].DynamicViscosity(cell.T);
        }
    }
}
