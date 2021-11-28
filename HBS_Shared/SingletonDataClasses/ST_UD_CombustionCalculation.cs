using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.ComponentModel;

namespace HBS_Shared
{
    public partial class ST_UD
    {
        public CombustionCalculationDataType CombustionCalculation { get; set; }

        public class CombustionCalculationDataType
        {
            [Category("Gas Components"),
            Description("Mole/Mass fraction determined by the mixing mole ratio of combusted gas.")]
            public CGas MGas { get; set; }

            [Category("Gas Components"),
            Description("Air composition for combustion")]
            public CGas Air { get; set; }

            [Category("Gas Components"),
            Description("Combusted gas composition for combustion")]
            public CGas CombustedGas { get; set; }

            [Category("Flow Rate [NCMH]"),
            Description("Operating mass flow rate of MGas [NCMH]")]
            public double MGas_FlowRate { get; set; }

            [Category("Flow Rate [NCMH]"),
            Description("Operating mass flow rate of Air [NCMH]")]
            public double Air_FlowRate { get; set; }

            [Category("Temperature [C]"),
            Description("Default operation temperature [C]")]
            public double MGas_Temperature { get; set; }

            [Category("Temperature [C]"),
            Description("Default operation temperature [C]")]
            public double Air_Temperature { get; set; }

            [Category("Pressure [mmAq]"),
            Description("Default operation pressure [mmAq]")]
            public double MGas_Pressure { get; set; }

            [Category("Pressure [mmAq]"),
            Description("Default operation pressure [mmAq]")]
            public double Air_Pressure { get; set; }

            public CombustionCalculationDataType()
            {

            }
            public CombustionCalculationDataType(string filePath)
            {
                FileInfo fileInfo = new FileInfo(filePath);

                if (!fileInfo.Exists)
                    throw CException.Show(CException.Type.NoFile);

                XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);
                XmlNodeList xmlMGasCalculation = CFileIO.GetXmlSubNodeList(xmlAll, "MGasCalculation");
                XmlNodeList xmlCombustionCalculation = CFileIO.GetXmlSubNodeList(xmlAll, "CombustionCalculation");
                XmlNodeList xmlOperatingCondition = CFileIO.GetXmlSubNodeList(xmlCombustionCalculation, "OperatingCondition");

                //Getting Operating Conditions
                MGas_FlowRate = CFileIO.GetXmlValueAsDouble(xmlOperatingCondition, "MGas_MassFlowRate");
                MGas_Temperature = CFileIO.GetXmlValueAsDouble(xmlOperatingCondition, "MGas_Temperature");
                MGas_Pressure = CFileIO.GetXmlValueAsDouble(xmlOperatingCondition, "MGas_Pressure");
                Air_FlowRate = CFileIO.GetXmlValueAsDouble(xmlOperatingCondition, "Air_MassFlowRate");
                Air_Temperature = CFileIO.GetXmlValueAsDouble(xmlOperatingCondition, "Air_Temperature");
                Air_Pressure = CFileIO.GetXmlValueAsDouble(xmlOperatingCondition, "Air_Pressure");

                XmlNodeList xmlAir = CFileIO.GetXmlSubNodeList(xmlCombustionCalculation, "Air");
                XmlNodeList xmlCombustedGas = CFileIO.GetXmlSubNodeList(xmlCombustionCalculation, "CombustedGas");
                XmlNodeList xmlMGas = CFileIO.GetXmlSubNodeList(xmlMGasCalculation, "MGas");

                Air = CGas.GetGasFromXmlNodeList(xmlAir);
                CombustedGas = CGas.GetGasFromXmlNodeList(xmlCombustedGas);
                MGas = CGas.GetGasFromXmlNodeList(xmlMGas);
            }

            public List<string> GetCombustionCalculationDataToList()
            {
                List<string> contents = new List<string>();

                contents.Add(@"    <OperatingCondition>");
                contents.Add(@"      <MGas_MassFlowRate>" + MGas_FlowRate + @"</MGas_MassFlowRate>");
                contents.Add(@"      <MGas_Temperature>" + MGas_Temperature + @"</MGas_Temperature>");
                contents.Add(@"      <MGas_Pressure>" + MGas_Pressure + @"</MGas_Pressure>");
                contents.Add(@"      <Air_MassFlowRate>" + Air_FlowRate + @"</Air_MassFlowRate>");
                contents.Add(@"      <Air_Temperature>" + Air_Temperature + @"</Air_Temperature>");
                contents.Add(@"      <Air_Pressure>" + Air_Pressure + @"</Air_Pressure>");
                contents.Add(@"    </OperatingCondition>");

                contents.Add(@"    <Air>");
                contents.Add(@"      <CH4>" + Air.MoleFraction[CGas.Composition.CH4] + @"</CH4>");
                contents.Add(@"      <C2H4>" + Air.MoleFraction[CGas.Composition.C2H4] + @"</C2H4>");
                contents.Add(@"      <C2H6>" + Air.MoleFraction[CGas.Composition.C2H6] + @"</C2H6>");
                contents.Add(@"      <C3H8>" + Air.MoleFraction[CGas.Composition.C3H8] + @"</C3H8>");
                contents.Add(@"      <CO>" + Air.MoleFraction[CGas.Composition.CO] + @"</CO>");
                contents.Add(@"      <CO2>" + Air.MoleFraction[CGas.Composition.CO2] + @"</CO2>");
                contents.Add(@"      <NH3>" + Air.MoleFraction[CGas.Composition.NH3] + @"</NH3>");
                contents.Add(@"      <H2>" + Air.MoleFraction[CGas.Composition.H2] + @"</H2>");
                contents.Add(@"      <O2>" + Air.MoleFraction[CGas.Composition.O2] + @"</O2>");
                contents.Add(@"      <N2>" + Air.MoleFraction[CGas.Composition.N2] + @"</N2>");
                contents.Add(@"      <Ar>" + Air.MoleFraction[CGas.Composition.Ar] + @"</Ar>");
                contents.Add(@"      <H2O>" + Air.MoleFraction[CGas.Composition.H2O] + @"</H2O>");
                contents.Add(@"    </Air>");

                contents.Add(@"    <CombustedGas>");
                contents.Add(@"      <CH4>" + CombustedGas.MoleFraction[CGas.Composition.CH4] + @"</CH4>");
                contents.Add(@"      <C2H4>" + CombustedGas.MoleFraction[CGas.Composition.C2H4] + @"</C2H4>");
                contents.Add(@"      <C2H6>" + CombustedGas.MoleFraction[CGas.Composition.C2H6] + @"</C2H6>");
                contents.Add(@"      <C3H8>" + CombustedGas.MoleFraction[CGas.Composition.C3H8] + @"</C3H8>");
                contents.Add(@"      <CO>" + CombustedGas.MoleFraction[CGas.Composition.CO] + @"</CO>");
                contents.Add(@"      <CO2>" + CombustedGas.MoleFraction[CGas.Composition.CO2] + @"</CO2>");
                contents.Add(@"      <NH3>" + CombustedGas.MoleFraction[CGas.Composition.NH3] + @"</NH3>");
                contents.Add(@"      <H2>" + CombustedGas.MoleFraction[CGas.Composition.H2] + @"</H2>");
                contents.Add(@"      <O2>" + CombustedGas.MoleFraction[CGas.Composition.O2] + @"</O2>");
                contents.Add(@"      <N2>" + CombustedGas.MoleFraction[CGas.Composition.N2] + @"</N2>");
                contents.Add(@"      <Ar>" + CombustedGas.MoleFraction[CGas.Composition.Ar] + @"</Ar>");
                contents.Add(@"      <H2O>" + CombustedGas.MoleFraction[CGas.Composition.H2O] + @"</H2O>");
                contents.Add(@"    </CombustedGas>");

                return contents;
            }
        }
    }
}
