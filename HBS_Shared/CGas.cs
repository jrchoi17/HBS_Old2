using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HBS_Shared;

namespace HBS_Shared
{
    public class CGas
    {
        #region Enum
        /// <summary>
        /// Ienumerable interface for gas composition.
        /// </summary>
        public enum Composition { CH4 = 0, C2H4, C2H6, C3H8, CO, CO2, NH3, H2, O2, N2, Ar, H2O }
        #endregion


        
        #region Fraction informations
        /// <summary>
        /// Mole fraction.
        /// </summary> 
        public Fraction MoleFraction { get; set; }
        /// <summary>
        /// Mass fraction.
        /// </summary>
        public Fraction MassFraction { get; set; }

        /// <summary>
        /// Returns the molar mass values of gas compositions.
        /// </summary>
        public static Fraction MolarMass
        {
            get
            {
                Fraction molarMass = new Fraction();

                // data from NIST RefProp 10.0.0
                molarMass.Add(Composition.CH4, 16.0428);
                molarMass.Add(Composition.C2H4, 28.05376);
                molarMass.Add(Composition.C2H6, 30.06904);
                molarMass.Add(Composition.C3H8, 44.09562);
                molarMass.Add(Composition.CO, 28.0101);
                molarMass.Add(Composition.CO2, 44.0098);
                molarMass.Add(Composition.NH3, 17.03052);
                molarMass.Add(Composition.H2, 2.01588);
                molarMass.Add(Composition.O2, 31.9988);
                molarMass.Add(Composition.N2, 28.01348);
                molarMass.Add(Composition.Ar, 39.948);
                molarMass.Add(Composition.H2O, 18.015268);

                return molarMass;
            }
        }
        #endregion



        #region Fraction class functional methods
        public class Fraction : Dictionary<CGas.Composition, double>
        {
            public Fraction()
            {

            }

            /// <summary>
            /// Assigns double value to composition enum.
            /// </summary>
            /// <param name="value">double value of gas.</param>
            public Fraction(double value)
            {
                foreach (int i in Enum.GetValues(typeof(Composition)))
                    Add((Composition)Enum.ToObject(typeof(Composition), i), value);
            }

            public Fraction(List<double> value)
            {
                foreach (int i in Enum.GetValues(typeof(Composition)))
                    Add((Composition)Enum.ToObject(typeof(Composition), i), value[i]);
            }

            /// <summary>
            /// Adds key value pair to an entry in composition dictionary.
            /// </summary>
            /// <param name="dictionary"></param>
            public Fraction(Dictionary<Composition, double> dictionary)
            {
                foreach (KeyValuePair<Composition, double> entry in dictionary)
                    Add(entry.Key, entry.Value);
            }

            /// <summary>
            /// Adds key value pair to an entry in fraction.
            /// </summary>
            /// <param name="fraction"></param>
            public Fraction(Fraction fraction)
            {
                foreach (KeyValuePair<Composition, double> entry in fraction)
                    Add(entry.Key, entry.Value);
            }

            /// <summary>
            /// Stores content values from a file located at filepath.
            /// </summary>
            /// <param name="filePath">directory of file</param>
            public Fraction(string filePath)
            {
                FileInfo fileInfo = new FileInfo(filePath);

                if (!fileInfo.Exists)
                    throw new System.Exception();

                List<string> contents = File.ReadAllLines(filePath).ToList();

                foreach (string content in contents)
                {
                    List<string> columns = content.Split(',').ToList();
                    string strComp = columns[0], strValue = columns[1];

                    Composition comp = (Composition)Enum.Parse(typeof(Composition), strComp);
                    double value = double.Parse(strValue);
                    Add(comp, value);
                }
            }

            /// <summary>
            /// Adds composition and value pair from file to fraction.
            /// </summary>
            /// <param name="contents">list of contents</param>
            public Fraction(List<string> contents)
            {
                List<string> compositions = CFileIO.GetStringExcelFormatColumn(contents, "compositions");
                List<string> values = CFileIO.GetStringExcelFormatColumn(contents, "values");

                for (int i = 0; i < compositions.Count; i++)
                {
                    Composition comp = (Composition)Enum.Parse(typeof(Composition), compositions[i]);
                    double value = double.Parse(values[i]);
                    Add(comp, value);
                }
            }

            /// <summary>
            /// Adds dry air molar mass values to data grid view.
            /// </summary>
            /// <returns>Molar mass of air.</returns>
            public static Fraction DryAir()
            {
                Fraction air = new Fraction(0.0);

                air[Composition.N2] = 0.78084;    // N2 = 78.084%
                air[Composition.O2] = 0.20946;    // O2 = 20.946%
                air[Composition.Ar] = 0.009300;   // Ar = 0.930%
                air[Composition.CO2] = 0.00040;   // CO2 = 0.04%

                return air;
            }

            /// <summary>
            /// Returns the normalized values of mole/mass fraction values.
            /// </summary>
            /// <returns>Fraction.</returns>
            public Fraction GetNormalizedValues()
            {
                Fraction fraction = new Fraction();
                double sum = GetSumValues();

                if (sum == 0.0)
                    return new Fraction(0.0);

                foreach (KeyValuePair<Composition, double> entry in this)
                    fraction.Add(entry.Key, entry.Value / sum);

                return fraction;
            }

            /// <summary>
            /// Returns the mass fraction values of gas compositions.
            /// </summary>
            /// <returns>Fraction.</returns>
            public Fraction GetMassFraction()
            {
                Fraction massFraction = new Fraction();

                foreach (KeyValuePair<Composition, double> entry in this)
                    massFraction.Add(entry.Key, entry.Value * MolarMass[entry.Key]);

                return massFraction.GetNormalizedValues();
            }

            /// <summary>
            /// Returns the mole fraction values of gas compositions.
            /// </summary>
            /// <returns>Fraction.</returns>
            public Fraction GetMoleFraction()
            {
                Fraction moleFraction = new Fraction();

                foreach (KeyValuePair<Composition, double> entry in this)
                    moleFraction.Add(entry.Key, entry.Value / MolarMass[entry.Key]);

                return moleFraction.GetNormalizedValues();
            }

            /// <summary>
            /// Gets fraction values in descending order.
            /// </summary>
            /// <returns>Fraction.</returns>
            public Fraction GetDescending()
            {
                List<Composition> comp = new List<Composition>();
                List<double> value = new List<double>();
                List<double> score = new List<double>();
                List<int> indices = new List<int>();

                foreach (KeyValuePair<Composition, double> entry in this)
                {
                    comp.Add(entry.Key);
                    value.Add(entry.Value);
                    score.Add(entry.Value);
                }

                for (int i = 0; i < score.Count; i++)
                {
                    indices.Add(score.IndexOf(score.Max()));
                    score[indices[i]] = -1.0;
                }

                Fraction fraction = new Fraction();

                for (int i = 0; i < indices.Count; i++)
                    fraction.Add(comp[indices[i]], value[indices[i]]);

                return fraction;
            }
           
            /// <summary>
            /// Normalizes individual mole/mass fraction values and sums. 
            /// </summary>
            public void Normalize()
            {
                Fraction fraction = new Fraction();
                double sum = GetSumValues();

                if (sum == 0.0)
                    foreach (KeyValuePair<Composition, double> entry in this)
                        fraction.Add(entry.Key, 0.0);
                else
                    foreach (KeyValuePair<Composition, double> entry in this)
                        fraction.Add(entry.Key, entry.Value / sum);

                foreach (KeyValuePair<Composition, double> entry in fraction)
                    this[entry.Key] = fraction[entry.Key];
            }

            /// <summary>
            /// Gets the summation of mole/mass fraction values of each gas column.
            /// </summary>
            /// <returns>Summation of fraction</returns>
            public double GetSumValues()
            {
                double sum = 0.0;

                foreach (KeyValuePair<Composition, double> entry in this)
                    sum += entry.Value;

                return sum;
            }

            /// <summary>
            /// Functional method for returning fraction values above threshold value.
            /// </summary>
            /// <param name="d">Threshold Value</param>
            /// <returns>Fraction.</returns>
            public Fraction GetAboveThresholdValue(double d)
            {
                Fraction fraction = new Fraction();

                foreach (KeyValuePair<Composition, double> entry in this)
                {
                    if (entry.Value > d)
                        fraction.Add(entry.Key, entry.Value);
                }

                return fraction;
            }

            /// <summary>
            /// Functional method for returning fraction values below threshold value.
            /// </summary>
            /// <param name="d">Threshold value/</param>
            /// <returns>Fraction.</returns>
            public Fraction GetBelowThresholdValue(double d)
            {
                Fraction fraction = new Fraction();

                foreach (KeyValuePair<Composition, double> entry in this)
                {
                    if (entry.Value <= d)
                        fraction.Add(entry.Key, entry.Value);
                }

                return fraction;
            }
            
        }
        #endregion



        #region Functional methods
        /// <summary>
        /// Functional method that converts mole fraction to mass fraction.
        /// </summary>
        public void MoleFractionToMassFraction()
        {
            MassFraction = MoleFraction.GetMassFraction();
        }

        /// <summary>
        /// Functional method that converts mass fraction to mole fraction.
        /// </summary>
        public void MassFractionToMoleFraction()
        {
            MoleFraction = MassFraction.GetMoleFraction();
        }

        /// <summary>
        /// Functional method to get gas composition list.
        /// </summary>
        /// <returns>Composition List</returns>
        public static List<Composition> GetCompositionList()
        {
            List<Composition> species = new List<Composition>();
            foreach (int i in Enum.GetValues(typeof(Composition)))
                species.Add((Composition)Enum.ToObject(typeof(Composition), i));

            return species;
        }

        /// <summary>
        /// Functional method to get the number of composition.
        /// </summary>
        /// <returns>Length of composition.</returns>
        public static int GetNumbOfComposition()
        {
            return Enum.GetNames(typeof(Composition)).Length;
        }

        /// <summary>
        /// Functional method to get gas fraction values from Xml node list.
        /// </summary>
        /// <param name="nodeList"></param>
        /// <returns>CGas class</returns>
        public static CGas GetGasFromXmlNodeList(XmlNodeList nodeList)
        {
             CGas gas = new CGas();
            gas.MoleFraction = new CGas.Fraction(0.0);

            foreach (CGas.Composition comp in CGas.GetCompositionList())
            {
                double value = CFileIO.GetXmlValueAsDouble(nodeList, comp.ToString());
                gas.MoleFraction[comp] = value;
            }

            gas.MoleFraction.Normalize();
            gas.MoleFractionToMassFraction();

            return gas;
        }
        #endregion
    }
}