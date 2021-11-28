using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.ComponentModel;

namespace HBS_Shared
{
    public partial class ST_UD
    {
        public M_GasCalculationDataType M_GasCalculation { get; set; }

        public class M_GasCalculationDataType
        {
            [Category("Gas Components"),
            Description("Gas obtained during the combustion of coke in blast furnaces.")]
            public CGas BFG { get; set; }

            [Category("Gas Components"),
            Description("[Coke Oven Gas] Gas composition obtained from coking coals.")]
            public CGas COG { get; set; }

            [Category("Gas Components"),
            Description("Arbitrary gas added during combustion [LPG, etc].")]
            public CGas XGas { get; set; }

            [Category("Gas Components"),
            Description("Mole/Mass fraction determined by the mixing mole ratio of combusted gas.")]
            public CGas MGas { get; set; }

            [Category("Mixing Mole Ratio"),
            Description("Molar Ratio of BFG constituting M-Gas")]
            public double X_BFG { get; set; }

            [Category("Mixing Mole Ratio"),
           Description("Molar Ratio of COG constituting M-Gas")]
            public double X_COG { get; set; }

            [Category("Mixing Mole Ratio"),
           Description("Molar Ratio of XGas constituting M-Gas")]
            public double X_XGas { get; set; }

            public M_GasCalculationDataType()
            {

            }
            public M_GasCalculationDataType(string filePath)
            {
                FileInfo fileInfo = new FileInfo(filePath);

                if (!fileInfo.Exists)
                    throw CException.Show(CException.Type.NoFile);

                XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);
                XmlNodeList xmlMGasCalculation = CFileIO.GetXmlSubNodeList(xmlAll, "MGasCalculation");

                X_BFG = CFileIO.GetXmlValueAsDouble(xmlMGasCalculation, "X_BFG");
                X_COG = CFileIO.GetXmlValueAsDouble(xmlMGasCalculation, "X_COG");
                X_XGas = CFileIO.GetXmlValueAsDouble(xmlMGasCalculation, "X_XGas");

                XmlNodeList xmlBfg = CFileIO.GetXmlSubNodeList(xmlMGasCalculation, "BFG");
                XmlNodeList xmlCog = CFileIO.GetXmlSubNodeList(xmlMGasCalculation, "COG");
                XmlNodeList xmlXGas = CFileIO.GetXmlSubNodeList(xmlMGasCalculation, "XGas");
                XmlNodeList xmlMGas = CFileIO.GetXmlSubNodeList(xmlMGasCalculation, "MGas");

                BFG = CGas.GetGasFromXmlNodeList(xmlBfg);
                COG = CGas.GetGasFromXmlNodeList(xmlCog);
                XGas = CGas.GetGasFromXmlNodeList(xmlXGas);
                MGas = CGas.GetGasFromXmlNodeList(xmlMGas);
            }

            public List<string> GetMGasDataToList()
            {
                List<string> contents = new List<string>();

                contents.Add(@"    <BFG>");
                contents.Add(@"      <CH4>" + BFG.MoleFraction[CGas.Composition.CH4] + @"</CH4>");
                contents.Add(@"      <C2H4>" + BFG.MoleFraction[CGas.Composition.C2H4] + @"</C2H4>");
                contents.Add(@"      <C2H6>" + BFG.MoleFraction[CGas.Composition.C2H6] + @"</C2H6>");
                contents.Add(@"      <C3H8>" + BFG.MoleFraction[CGas.Composition.C3H8] + @"</C3H8>");
                contents.Add(@"      <CO>" + BFG.MoleFraction[CGas.Composition.CO] + @"</CO>");
                contents.Add(@"      <CO2>" + BFG.MoleFraction[CGas.Composition.CO2] + @"</CO2>");
                contents.Add(@"      <NH3>" + BFG.MoleFraction[CGas.Composition.NH3] + @"</NH3>");
                contents.Add(@"      <H2>" + BFG.MoleFraction[CGas.Composition.H2] + @"</H2>");
                contents.Add(@"      <O2>" + BFG.MoleFraction[CGas.Composition.O2] + @"</O2>");
                contents.Add(@"      <N2>" + BFG.MoleFraction[CGas.Composition.N2] + @"</N2>");
                contents.Add(@"      <Ar>" + BFG.MoleFraction[CGas.Composition.Ar] + @"</Ar>");
                contents.Add(@"      <H2O>" + BFG.MoleFraction[CGas.Composition.H2O] + @"</H2O>");
                contents.Add(@"    </BFG>");

                contents.Add(@"    <COG>");
                contents.Add(@"      <CH4>" + COG.MoleFraction[CGas.Composition.CH4] + @"</CH4>");
                contents.Add(@"      <C2H4>" + COG.MoleFraction[CGas.Composition.C2H4] + @"</C2H4>");
                contents.Add(@"      <C2H6>" + COG.MoleFraction[CGas.Composition.C2H6] + @"</C2H6>");
                contents.Add(@"      <C3H8>" + COG.MoleFraction[CGas.Composition.C3H8] + @"</C3H8>");
                contents.Add(@"      <CO>" + COG.MoleFraction[CGas.Composition.CO] + @"</CO>");
                contents.Add(@"      <CO2>" + COG.MoleFraction[CGas.Composition.CO2] + @"</CO2>");
                contents.Add(@"      <NH3>" + COG.MoleFraction[CGas.Composition.NH3] + @"</NH3>");
                contents.Add(@"      <H2>" + COG.MoleFraction[CGas.Composition.H2] + @"</H2>");
                contents.Add(@"      <O2>" + COG.MoleFraction[CGas.Composition.O2] + @"</O2>");
                contents.Add(@"      <N2>" + COG.MoleFraction[CGas.Composition.N2] + @"</N2>");
                contents.Add(@"      <Ar>" + COG.MoleFraction[CGas.Composition.Ar] + @"</Ar>");
                contents.Add(@"      <H2O>" + COG.MoleFraction[CGas.Composition.H2O] + @"</H2O>");
                contents.Add(@"    </COG>");

                contents.Add(@"    <XGas>");
                contents.Add(@"      <CH4>" + XGas.MoleFraction[CGas.Composition.CH4] + @"</CH4>");
                contents.Add(@"      <C2H4>" + XGas.MoleFraction[CGas.Composition.C2H4] + @"</C2H4>");
                contents.Add(@"      <C2H6>" + XGas.MoleFraction[CGas.Composition.C2H6] + @"</C2H6>");
                contents.Add(@"      <C3H8>" + XGas.MoleFraction[CGas.Composition.C3H8] + @"</C3H8>");
                contents.Add(@"      <CO>" + XGas.MoleFraction[CGas.Composition.CO] + @"</CO>");
                contents.Add(@"      <CO2>" + XGas.MoleFraction[CGas.Composition.CO2] + @"</CO2>");
                contents.Add(@"      <NH3>" + XGas.MoleFraction[CGas.Composition.NH3] + @"</NH3>");
                contents.Add(@"      <H2>" + XGas.MoleFraction[CGas.Composition.H2] + @"</H2>");
                contents.Add(@"      <O2>" + XGas.MoleFraction[CGas.Composition.O2] + @"</O2>");
                contents.Add(@"      <N2>" + XGas.MoleFraction[CGas.Composition.N2] + @"</N2>");
                contents.Add(@"      <Ar>" + XGas.MoleFraction[CGas.Composition.Ar] + @"</Ar>");
                contents.Add(@"      <H2O>" + XGas.MoleFraction[CGas.Composition.H2O] + @"</H2O>");
                contents.Add(@"    </XGas>");

                contents.Add(@"    <MGas>");
                contents.Add(@"      <CH4>" + MGas.MoleFraction[CGas.Composition.CH4] + @"</CH4>");
                contents.Add(@"      <C2H4>" + MGas.MoleFraction[CGas.Composition.C2H4] + @"</C2H4>");
                contents.Add(@"      <C2H6>" + MGas.MoleFraction[CGas.Composition.C2H6] + @"</C2H6>");
                contents.Add(@"      <C3H8>" + MGas.MoleFraction[CGas.Composition.C3H8] + @"</C3H8>");
                contents.Add(@"      <CO>" + MGas.MoleFraction[CGas.Composition.CO] + @"</CO>");
                contents.Add(@"      <CO2>" + MGas.MoleFraction[CGas.Composition.CO2] + @"</CO2>");
                contents.Add(@"      <NH3>" + MGas.MoleFraction[CGas.Composition.NH3] + @"</NH3>");
                contents.Add(@"      <H2>" + MGas.MoleFraction[CGas.Composition.H2] + @"</H2>");
                contents.Add(@"      <O2>" + MGas.MoleFraction[CGas.Composition.O2] + @"</O2>");
                contents.Add(@"      <N2>" + MGas.MoleFraction[CGas.Composition.N2] + @"</N2>");
                contents.Add(@"      <Ar>" + MGas.MoleFraction[CGas.Composition.Ar] + @"</Ar>");
                contents.Add(@"      <H2O>" + MGas.MoleFraction[CGas.Composition.H2O] + @"</H2O>");
                contents.Add(@"    </MGas>");

                contents.Add(@"    <X_BFG>" + X_BFG + @"</X_BFG>");
                contents.Add(@"    <X_COG>" + X_COG + @"</X_COG>");
                contents.Add(@"    <X_XGas>" + X_XGas + @"</X_XGas>");

                return contents;
            }
        }
    }
}
