using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS_Shared
{
    public class CMatProp
    {
        public enum MatPropMethod { Polynomial, RefProf }

        /// <summary>
        /// Density calculation method.
        /// </summary>
        public MatPropMethod DensityMethod { get; set; }

        /// <summary>
        /// Polynomial coefficients for the thermodynamic density calculation. Unit: kg/m^3.
        /// </summary>
        public List<double> DensityCoeff { get; set; }
        /// <summary>
        /// Maximum temperature bound for density. Unit: K.
        /// </summary>
        public double T_MaxForDensity { get; set; }
        /// <summary>
        /// Minimum temperature bound for density. Unit: K.
        /// </summary>
        public double T_MinForDensity { get; set; }

        /// <summary>
        /// Density calculation method.
        /// </summary>
        public MatPropMethod SpecificHeatMethod { get; set; }

        /// <summary>
        /// Polynomial coefficients for the thermodynamic specific heat calculation. Unit: J/kg-K.
        /// </summary>
        public List<double> SpecificHeatCoeff { get; set; }
        /// <summary>
        /// Maximum temperature bound for specific heat. Unit: K.
        /// </summary>
        public double T_MaxForSpecificHeat { get; set; }
        /// <summary>
        /// Minimum temperature bound for specific heat. Unit: K.
        /// </summary>
        public double T_MinForSpecificHeat { get; set; }

        /// <summary>
        /// Density calculation method.
        /// </summary>
        public MatPropMethod ThermalConductivityMethod { get; set; }

        /// <summary>
        /// Polynomial coefficients for the thermodynamic thermal conductivity calculation. Unit: W/m-K.
        /// </summary>
        public List<double> ThermalConductivityCoeff { get; set; }
        /// <summary>
        /// Maximum temperature bound for thermal conductivity. Unit: K.
        /// </summary>
        public double T_MaxForThermalConductivity { get; set; }
        /// <summary>
        /// Minimum temperature bound for thermal conductivity. Unit: K.
        /// </summary>
        public double T_MinForThermalConductivity { get; set; }

        /// <summary>
        /// Density calculation method.
        /// </summary>
        public MatPropMethod DynamicViscosityMethod { get; set; }

        /// <summary>
        /// Polynomial coefficients for the thermodynamic dynamic viscosity calculation. Unit: W/m-K.
        /// </summary>
        public List<double> DynamicViscosityCoeff { get; set; }
        /// <summary>
        /// Maximum temperature bound for dynamic viscosity. Unit: K.
        /// </summary>
        public double T_MaxForDynamicViscosity { get; set; }
        /// <summary>
        /// Minimum temperature bound for dynamic viscosity. Unit: K.
        /// </summary>
        public double T_MinForDynamicViscosity { get; set; }

        public CMatProp()
        {

        }

        public CMatProp(CMatProp prop)
        {
            // do deep clone for density.
            DensityCoeff = new List<double>();

            for (int i = 0; i < prop.DensityCoeff.Count; i++)
                DensityCoeff.Add(prop.DensityCoeff[i]);

            T_MaxForDensity = prop.T_MaxForDensity;
            T_MinForDensity = prop.T_MinForDensity;

            // do deep clone for specific heat.
            SpecificHeatCoeff = new List<double>();

            for (int i = 0; i < prop.SpecificHeatCoeff.Count; i++)
                SpecificHeatCoeff.Add(prop.SpecificHeatCoeff[i]);

            T_MaxForSpecificHeat = prop.T_MaxForSpecificHeat;
            T_MinForSpecificHeat = prop.T_MinForSpecificHeat;

            // do deep clone for thermal conductivity.
            ThermalConductivityCoeff = new List<double>();

            for (int i = 0; i < prop.ThermalConductivityCoeff.Count; i++)
                ThermalConductivityCoeff.Add(prop.ThermalConductivityCoeff[i]);

            T_MaxForThermalConductivity = prop.T_MaxForThermalConductivity;
            T_MinForThermalConductivity = prop.T_MinForThermalConductivity;

            // do deep clone for dynamic viscosity.
            DynamicViscosityCoeff = new List<double>();

            for (int i = 0; i < prop.DynamicViscosityCoeff.Count; i++)
                DynamicViscosityCoeff.Add(prop.DynamicViscosityCoeff[i]);

            T_MaxForDynamicViscosity = prop.T_MaxForDynamicViscosity;
            T_MinForDynamicViscosity = prop.T_MinForDynamicViscosity;
        }

        public double Density(double temperature)
        {
            return GetPolynomialValue(DensityCoeff, T_MaxForDensity, T_MinForDensity, temperature);
        }

        public double SpecificHeat(double temperature)
        {
            return GetPolynomialValue(SpecificHeatCoeff, T_MaxForSpecificHeat, T_MinForSpecificHeat, temperature);
        }

        public double ThermalConductivity(double temperature)
        {
            return GetPolynomialValue(ThermalConductivityCoeff, T_MaxForThermalConductivity, T_MinForThermalConductivity, temperature);
        }

        public double DynamicViscosity(double temperature)
        {
            return GetPolynomialValue(DynamicViscosityCoeff, T_MaxForDynamicViscosity, T_MinForDynamicViscosity, temperature);
        }

        private double GetPolynomialValue(List<double> coeff, double x_Max, double x_min, double x_0)
        {
            if (coeff == null)
                return double.NaN;

            double value = 0.0;
            if (x_0 < x_min)
                x_0 = x_min;

            if (x_0 > x_Max)
                x_0 = x_Max;

            for (int i = 0; i < coeff.Count; i++)
                value += coeff[i] * Math.Pow(x_0, (double)i);

            return value;
        }
    }
}
