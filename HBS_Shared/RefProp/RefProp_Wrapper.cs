//
// IRefProp64 - Builds the IRefProp64.dll .Net assembly DLL that allows C# programs to use the native RefProp RefPrp64.dll. 
//              C#/.Net programs must add the MCS namesspace and Reference IRefProp64.dll to the Visual Studio Project in order
//              to use IRefProp64.dll.
//
//
// Copyright 2017 Mill Creek Systems, Inc. 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files 
// (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do 
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

// Mill Creek Systems, Inc.
// 3233 N. Arlington Heights Rd., Ste 301B
// Arlington Heights, IL 60004 USA
//
// www.MillCreekSystems.com
// support@MillCreekSystems.com
// 847/590-5686
/*! \mainpage IRefProp64.dll
 *
 * \section intro_sec Introduction
 *
 * IRefProp is an effective C# Interface for REFPROP. It’s an open source project (is/will be hosted on github) that contains a static wrapper class (IRefProp64) allowing access to the REFPROP native-code methods such as SETPATHdll(…), SETUPdll(…), SATSPLNdll(…),………etc.
 *
 * \section install_sec Installation
 * Make sure IRefProp64.dll is installed on your machine.
 * \subsection step1 Remarks
 *  
 * Using IRefProp requires adding the IRefProp64.dll reference to the project. Once the reference is added, the wrapped REFPROP methods can be accessed as follows: MCS.iRefProp64.method_name() where MCS is the namespace and iRefProp64 is the name of the main static class. Ex: MCS.IRefProp64.SETPATHdll(hpath, ref size);
 *
 */
using System;
using System.Runtime.InteropServices;

namespace HBS_Shared
{
    public static class RefProp_Wrapper
    {
        private const string path = @"RefProp\REFPRP64.dll";
        /**************************************************************
         * Set the path where the fluid files are located.
         * The path does not need to contain the ending "\" and it can
         * point directly to the location where the DLL is stored if a
         * fluids subdirectory (with the corresponding fluid files) is
         * located there.            
         *************************************************************/
        [DllImport(path)]
        private static extern void SETPATHdll(string htype, ref long ln);
        /******************************************************************************
         * Define models and initialize arrays. A call to this subroutine is required
         *******************************************************************************/
        [DllImport(path, CharSet = CharSet.Ansi)]
        private static extern void SETUPdll
        (
            ref long nComps,                                          // (INPUT) number of components (1 for pure fluid) [integer]
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string hfld,    // (INPUT) list of file names specifying fluid/mixture components. Separated by pipes "|"
                                                                      //  e.g., METHANE|AMMONIA|ARGON
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string hfmix,   // (INPUT) mixture coefficients. File name containing coefficients for mixture model, if applicable
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string hrf,     // (INPUT) reference state for thermodynamic calculations [3 character string]
                                                                      //  'DEF' : default reference state as specified in fluid file is applied to each pure component
                                                                      //  'NBP' : h,s = 0 at pure component normal boiling point(s)
                                                                      //  'ASH' : h,s = 0 for sat liquid at -40 C (ASHRAE convention)
                                                                      //  'IIR' : h= 200, s = 1.0 for sat liq at 0 C (IIR convention)
                                                                      //  other choices are possible, but these require a separate call to SETREF
            ref long ierr,                                            // (OUTPUT) error flag: 
                                                                      //  0 = successful
                                                                      //  101 = error in opening file
                                                                      //  102 = error in file or premature end of file
                                                                      //  -103 = unknown model encountered in file
                                                                      //  104 = error in setup of model
                                                                      //  105 = specified model not found
                                                                      //  111 = error in opening mixture file
                                                                      //  112 = mixture file of wrong type
                                                                      //  114 = nc<>nc from setmod
                                                                      //  -117 = binary pair not found, all parameters will be estimated
                                                                      //  117 = no mixture data are available, mixture is outside the range of the model and calculations will not be made
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string herr,    // (OUTPUT) error string
            ref long hfldLen,
            ref long hfmixLen,
            ref long hrfLen,
            ref long herrLen);

        /***********************************************************
         * molecular weight for a mixture of specified composition
         ***********************************************************/
        [DllImport(path, CharSet = CharSet.Ansi)]
        private static extern void WMOLdll
        (
           [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] x,   // (INPUT) composition array [array of mol frac]
           ref double wm                                                        // (OUTPUT) molar mass [g/mol]
        );
        /***************************************************************************************
         * General Flash subroutine taking a temperature and pressure.
         * This routine accepts both single-phase and two-phase states as input.
         * If the the phase is know, the specialized routines are faster.
         **************************************************************************************/
        [DllImport(path, CharSet = CharSet.Ansi)]
        private static extern void TPFLSHdll
        (
            ref double t,                                                            // (INPUT) temperature [K]
            ref double p,                                                            // (INPUT) pressure [kPa]
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] x,       // (INPUT) overall (bulk) composition [array of mol frac]
            ref double d,                                                            // (OUTPUT) overall (bulk) molar density [mol/L]
            ref double Dl,                                                           // (OUTPUT) molar density of liquid phase [mol/L]
            ref double Dv,                                                           // (OUTPUT) molar density of the vapor phase [mol/L]
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] xliq,    // (OUTPUT) composition of liquid phase [array of mol frac]
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] xvap,    // (OUTPUT) composition of vapor phase [array of mol frac]
            ref double q,                                                            // (OUTPUT) vapor quality on a MOLAR basis [moles vapor/total moles]
                                                                                     //  q < 0 indicates subcooled (compressed) liquid
                                                                                     //  q = 0 indicates saturated liquid
                                                                                     //  q = 1 indicates saturated vapor
                                                                                     //  q > 1 indicates superheated vapor
                                                                                     //  q = 998 superheated vapor, but quality not defined (in most situations, t > Tc)
                                                                                     //  q = 999 indicates supercritical state (t > Tc) and (p > Pc)
            ref double ee,                                                           // (OUTPUT) overall (bulk) internal energy [J/mol]
            ref double h,                                                            // (OUTPUT) overall (bulk) enthalpy [J/mol]
            ref double ss,                                                           // (OUTPUT) overall (bulk) entropy [J/mol-K]
            ref double Cv,                                                           // (OUTPUT) isochoric (constant V) heat capacity [J/mol-K] 
            ref double Cp,                                                           // (OUTPUT) isobaric (constatnt p) heat capacity [J/mol-K] (not defined for 2-phase)
            ref double w,                                                            // (OUTPUT) speed of sound [m/s] (not defined for 2-phase)
            ref long ierr,                                                           // (OUTPUT) error flag
                                                                                     //  0 = all inputs within limits
                                                                                     //  <> 0 = one or more inputs outside limits
                                                                                     //  -1 = 1.5*tmax > t > tmax
                                                                                     //  1 = t < tmin or t > 1.5*tmax
                                                                                     //  2 = D > Dmax or D < 0
                                                                                     //  -4 = 2*pmax > p > pmax
                                                                                     //  4 = p < 0 or p > 2*pmax
                                                                                     //  8 = component composition < 0 or > 1 and/or composition sum < 0 or > 1
                                                                                     //  16 = p > pmelt
                                                                                     //  -16 = t < ttrp (important for water)
                                                                                     // If multiple inputs are outside limits, ierr = abs[sum(ierr)] with the sign determined by the most severe excursion
                                                                                     // (ierr < 0 indicates a warning -- results may be questionable, ierr > 0 indicates an error -- calculations impossible)
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string herr,                   // (OUTPUT) error string
            ref long ln
            );
        /*************************************************************************************************
         * iterate for saturated liquid and vapor states given pressure and the composition of one phase
         ************************************************************************************************/
        [DllImport(path, CharSet = CharSet.Ansi)]
        private static extern void SATPdll
        (
            ref double p,                                                           // (INPUT) pressure [kPa]
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] x,      // (INPUT) composition array [array of mol frac]
            ref long kph,                                                           // (INPUT) phase flag. see same parameter in SATPdll
            ref double t,                                                           // (OUTPUT) temperature [K]
            ref double Dl,                                                          // (OUTPUT) density of liquid [mol/L]
            ref double Dv,                                                          // (OUTPUT) density of vapor [mol/L]
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] xliq,   // (OUTPUT) liquid composition [array of mol frac]
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] xvap,   // (OUTPUT) vapor composition [array of mol frac]
            ref long ierr,                                                          // (OUTPUT) error flag:
                                                                                    //  0 = successful
                                                                                    //  2 = P<Ptp
                                                                                    //  4 = P< 0
                                                                                    //  8 = x out of range
                                                                                    //  12 = P and x out of range
                                                                                    //  140 = CRITP did not converge
                                                                                    //  141 = P> Pcrit
                                                                                    //  142 = TPRHO-liquid did not converge (pure fluid)
                                                                                    //  143 = TPRHO-vapor did not converge (pure fluid)
                                                                                    //  144 = pure fluid iteration did not converge
                                                                                    //  following 3 error codes are advisory--iteration will either converge on later guess or error out (ierr = 148)
                                                                                    //  -144 = Raoult's law (mixture initial guess) did  not converge
                                                                                    //  -145 = TPRHO did not converge for parent ph(mix)
                                                                                    //  -146 = TPRHO did not converge for incipient(mix)
                                                                                    //  -147 = composition iteration did not converge
                                                                                    //  148 = mixture iteration did not converge (error out from previous 3)
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string herr,                  // (OUTPUT) error string
            ref long herrLen
        );
        /******************************************************************
         * compute the transport properties of thermal conductivity and 
         * viscosity as functions of temperature, density, and composition
         *******************************************************************/
        [DllImport(path, CharSet = CharSet.Ansi)]
        private static extern void TRNPRPdll
        (
            ref double t,                                                           // (INPUT) temperature [K]
            ref double d,                                                           // (INPUT) molar density [mol/L]
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] x,      // (INPUT) composition array [mol frac]
            ref double eta,                                                         // (OUTPUT) viscosity [uPa.s]
            ref double tcx,                                                         // (OUTPUT) thermal conductivity [W/m.K]
            ref long ierr,                                                          // (OUTPUT) error flag:
                                                                                    //  0 = successful
                                                                                    //  -31 = temperature out of range for conductivity
                                                                                    //  -32 = density out of range for conductivity
                                                                                    //  -33 = T and D out of range for conductivity
                                                                                    //  -41 = temperature out of range for viscosity
                                                                                    //  -42 = density out of range for viscosity
                                                                                    //  -43 = T and D out of range for viscosity
                                                                                    //  -51 = T out of range for both visc and t.c.
                                                                                    //  -52 = D out of range for both visc and t.c.
                                                                                    //  -53 = T and/or D out of range for visc and t.c.
                                                                                    //  39 = model not found for thermal conductivity
                                                                                    //  40 = model not found for thermal conductivity or viscosity
                                                                                    //  49 = model not found for viscosity
                                                                                    //  50 = ammonia/water mixture (no properties calculated)
                                                                                    //  51 = exactly at t, rhoc for a pure fluid; k is infinite
                                                                                    //  -58,-59 = ECS model did not converge
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string herr,
            ref long herrLen
        );
        public static double GetVersion()
        {
            string hfld = "";
            string hfmix = "hmx.bnc" + new String(' ', 248);
            string hrf = "DEF";
            long iErr = 0;
            string herr = new String(' ', 255);
            long hfldLen = hfld.Length, hfmixLen = hfmix.Length, hrfLen = hrf.Length, herrLen = herr.Length;
            long numComps = -1;
            RefProp_Wrapper.SETUPdll(ref numComps, ref hfld, ref hfmix, ref hrf, ref iErr, ref herr, ref hfldLen, ref hfmixLen, ref hrfLen, ref herrLen);
            return Math.Round(iErr / 10000.0, 0);
        }
        public static double GetMolarMass(CGas.Composition species, string dbFilePath = @"RefProp\DatabaseFiles")
        {
            double molarmass = 0.0;
            string hfld = species.ToString();

            string hfmix = "hmx.bnc" + new String(' ', 248);
            string hrf = "DEF";
            long iErr = 0;
            string herr = new String(' ', 255);
            long hfldLen = hfld.Length, hfmixLen = hfmix.Length, hrfLen = hrf.Length, herrLen = herr.Length;

            long size = dbFilePath.Length;
            RefProp_Wrapper.SETPATHdll(dbFilePath, ref size);

            long numComps = 1;  // for pure composition
            RefProp_Wrapper.SETUPdll(ref numComps, ref hfld, ref hfmix, ref hrf, ref iErr, ref herr, ref hfldLen, ref hfmixLen, ref hrfLen, ref herrLen);
            RefProp_Wrapper.WMOLdll(null, ref molarmass);

            return molarmass;
        }
        public static double GetDensity(CGas.Composition species, double temperature, double pressure = 101325.0, string dbFilePath = @"RefProp\DatabaseFiles")
        {
            double density = 0.0, specificHeat = 0.0;
            GetDensityAndSpecificHeat(ref density, ref specificHeat, species, temperature, pressure, dbFilePath);
            return density;
        }
        public static double GetSpecificHeat(CGas.Composition species, double temperature, double pressure = 101325.0, string dbFilePath = @"RefProp\DatabaseFiles")
        {
            double density = 0.0, specificHeat = 0.0;
            GetDensityAndSpecificHeat(ref density, ref specificHeat, species, temperature, pressure, dbFilePath);
            return specificHeat;
        }
        private static void GetDensityAndSpecificHeat(ref double density, ref double specificHeat, CGas.Composition species, double temperature, double pressure = 101325.0, string dbFilePath = @"RefProp\DatabaseFiles")
        {
            pressure /= 1000.0; // unit converting from Pa to kPa
            double molarmass = 0.0;
            string hfld = species.ToString();

            double Dl = 0.0, Dv = 0.0, q = 0.0, ee = 0.0, hh = 0.0, ss = 0.0, cv = 0.0, w = 0.0;
            double[] x = new double[20], xliq = new double[20], xvap = new double[20];

            string hfmix = "hmx.bnc" + new String(' ', 248);
            string hrf = "DEF";
            long iErr = 0;
            string herr = new String(' ', 255);
            long hfldLen = hfld.Length, hfmixLen = hfmix.Length, hrfLen = hrf.Length, herrLen = herr.Length;

            long size = dbFilePath.Length;
            RefProp_Wrapper.SETPATHdll(dbFilePath, ref size);

            long numComps = 1;  // for pure composition
            RefProp_Wrapper.SETUPdll(ref numComps, ref hfld, ref hfmix, ref hrf, ref iErr, ref herr, ref hfldLen, ref hfmixLen, ref hrfLen, ref herrLen);
            RefProp_Wrapper.WMOLdll(null, ref molarmass);
            // RefProp_Wrapper.TPFLSHdll(ref tk, ref p, x, ref d, ref Dl, ref Dv, xliq, xvap, ref q, ref ee, ref hh, ref ss, ref cp, ref cv, ref w, ref iErr, ref herr, ref herrLen);
            RefProp_Wrapper.TPFLSHdll(ref temperature, ref pressure, x,
                ref density, ref Dl, ref Dv, xliq, xvap, ref q, ref ee, ref hh, ref ss, ref specificHeat, ref cv, ref w, ref iErr, ref herr, ref herrLen);
            
            //Conversion from mol/L -> g/L = kg/m3
            density *= molarmass;
            
            //Conversion from J/mol/K -> J/kg/K
            specificHeat *= 1000.0 / molarmass;
        }
        public static double GetThermalConductivity(CGas.Composition species, double temperature, double pressure = 101325.0, string dbFilePath = @"RefProp\DatabaseFiles")
        {
            double thermalConductivity = 0.0, dynamicViscosity = 0.0;
            GetThermalConductivityAndDynamicViscosity(ref thermalConductivity, ref dynamicViscosity, species, temperature, pressure, dbFilePath);
            return thermalConductivity;
        }
        public static double GetDynamicViscosity(CGas.Composition species, double temperature, double pressure = 101325.0, string dbFilePath = @"RefProp\DatabaseFiles")
        {
            double thermalConductivity = 0.0, dynamicViscosity = 0.0;
            GetThermalConductivityAndDynamicViscosity(ref thermalConductivity, ref dynamicViscosity, species, temperature, pressure, dbFilePath);
            return dynamicViscosity;
        }
        private static void GetThermalConductivityAndDynamicViscosity(ref double thermalConductivity, ref double dynamicViscosity, CGas.Composition species, double temperature, double pressure = 101325.0, string dbFilePath = @"RefProp\DatabaseFiles")
        {
            double density = 0.0, specificHeat = 0.0;
            pressure /= 1000.0; // unit converting from Pa to kPa
            double molarmass = 0.0;
            string hfld = species.ToString();

            double Dl = 0.0, Dv = 0.0, q = 0.0, ee = 0.0, hh = 0.0, ss = 0.0, cv = 0.0, w = 0.0;
            double[] x = new double[20], xliq = new double[20], xvap = new double[20];

            string hfmix = "hmx.bnc" + new String(' ', 248);
            string hrf = "DEF";
            long iErr = 0;
            string herr = new String(' ', 255);
            long hfldLen = hfld.Length, hfmixLen = hfmix.Length, hrfLen = hrf.Length, herrLen = herr.Length;

            long size = dbFilePath.Length;
            RefProp_Wrapper.SETPATHdll(dbFilePath, ref size);

            long numComps = 1;  // for pure composition
            RefProp_Wrapper.SETUPdll(ref numComps, ref hfld, ref hfmix, ref hrf, ref iErr, ref herr, ref hfldLen, ref hfmixLen, ref hrfLen, ref herrLen);
            RefProp_Wrapper.WMOLdll(null, ref molarmass);
            RefProp_Wrapper.TPFLSHdll(ref temperature, ref pressure, x,
                ref density, ref Dl, ref Dv, xliq, xvap, ref q, ref ee, ref hh, ref ss, ref specificHeat, ref cv, ref w, ref iErr, ref herr, ref herrLen);
            RefProp_Wrapper.TRNPRPdll(ref temperature, ref density, x, ref dynamicViscosity, ref thermalConductivity, ref iErr, ref herr, ref herrLen);
            dynamicViscosity *= 1.0e-6;
        }
    }
}
