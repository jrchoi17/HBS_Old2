using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Windows.Forms;

namespace HBS_Shared
{
    public class ST_UD
    {
        public struct FlowOperatingConditionDataType
        {
            public double Time { get; set; }
            public double FlowRate { get; set; }
            public double Temperature { get; set; }
            public double Pressure { get; set; }
        }
        public enum FlowOperationConditionItem { Time = 0, FlowRate, Temperature, Pressure }

        public PropertyGrid PropertyGrid { get; set; }

        private static ST_UD _objThis;

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
        public M_GasCalculationDataType M_GasCalculation { get; set; }

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
                contents.Add(@"      <CH4>" +  Air.MoleFraction[CGas.Composition.CH4] + @"</CH4>");
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
        public CombustionCalculationDataType CombustionCalculation { get; set; }

        public class CombustedGasDataType
        {
            [Browsable(false)]
            public List<FlowOperatingConditionDataType> FlowOperatingCondition { get; set; }

            [Category("Flow Operating Conditions"),
            Description("Time [min]")]
            public List<double> Time { get; set; }

            [Category("Flow Operating Conditions"),
            Description("Flow Rate [Nm3/hr]")]
            public List<double> FlowRate { get; set; }

            [Category("Flow Operating Conditions"),
            Description("Temperature [C]")]
            public List<double> Temperature { get; set; }

            [Category("Flow Operating Conditions"),
            Description("Pressure [mmAq(g)]")]
            public List<double> Pressure { get; set; }

            [Category("Gas Components"),
            Description("Combusted gas composition for combustion")]
            public CGas CombustedGas { get; set; }

            public CombustedGasDataType()
            {

            }
            public CombustedGasDataType(string filePath)
            {
                FileInfo fileInfo = new FileInfo(filePath);

                if (!fileInfo.Exists)
                    throw CException.Show(CException.Type.NoFile);

                XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);
                XmlNodeList xmlCombustedGas = CFileIO.GetXmlSubNodeList(xmlAll, "CombustedGas");
                XmlNodeList xmlCombustedGasSub = CFileIO.GetXmlSubNodeList(xmlCombustedGas, "CombustedGas");
                XmlNodeList xmlFlowOperatingConditions = CFileIO.GetXmlSubNodeList(xmlCombustedGas, "FlowOperatingConditions");

                CombustedGas = CGas.GetGasFromXmlNodeList(xmlCombustedGasSub);

                List<List<double>> list_2D = CFileIO.GetXml2DTableAsDoubleList(xmlFlowOperatingConditions);



                FlowOperatingCondition = new List<FlowOperatingConditionDataType>();

                for (int i = 0; i < list_2D.Count; i++)
                {
                    Time = list_2D[i];
                    FlowRate = list_2D[i];
                    Temperature = list_2D[i];
                    Pressure = list_2D[i];

                    FlowOperatingConditionDataType flowOperatingCondition = new FlowOperatingConditionDataType();
                    flowOperatingCondition.Time = Time[(int)FlowOperationConditionItem.Time];
                    flowOperatingCondition.FlowRate = FlowRate[(int)FlowOperationConditionItem.FlowRate];
                    flowOperatingCondition.Temperature = Temperature[(int)FlowOperationConditionItem.Temperature];
                    flowOperatingCondition.Pressure = Pressure[(int)FlowOperationConditionItem.Pressure];

                    FlowOperatingCondition.Add(flowOperatingCondition);
                }
            }

            public List<string> GetCombustedGasDataToList()
            {
                List<string> contents = new List<string>();

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

                contents.Add(@"    <FlowOperatingConditions>");
                for (int i = 0; i < FlowOperatingCondition.Count; i++)
                {
                    contents.Add(@"      <Profile_" + i + @">");
                    contents.Add(@"        <Time>" + FlowOperatingCondition[i].Time + "</Time>");
                    contents.Add(@"        <FlowRate>" + FlowOperatingCondition[i].FlowRate + "</FlowRate>");
                    contents.Add(@"        <Temperature>" + FlowOperatingCondition[i].Temperature + "</Temperature>");
                    contents.Add(@"        <Pressure>" + FlowOperatingCondition[i].Pressure + "</Pressure>");
                    contents.Add(@"      </Profile_" + i + @">");
                }
                contents.Add(@"    </FlowOperatingConditions>");

                return contents;
            }
        }
        public CombustedGasDataType CombustedGas { get; set; }

        public class ColdBlastDataType
        {
            [Category("Gas Components"),
            Description("Cold blast gas composition")]
            public CGas Air { get; set; }

            [Category("Gas Components"),
            Description("Cold blast gas composition")]
            public CGas O2 { get; set; }

            [Category("Air Flow Operating Conditions"),
            Description("Time [min]")]
            public List<double> Air_Time { get; set; }

            [Category("Air Flow Operating Conditions"),
            Description("Flow Rate [Nm3/hr]")]
            public List<double> Air_FlowRate { get; set; }

            [Category("Air Flow Operating Conditions"),
            Description("Temperature [C]")]
            public List<double> Air_Temperature { get; set; }

            [Category("Air Flow Operating Conditions"),
            Description("Pressure [mmAq(g)]")]
            public List<double> Air_Pressure { get; set; }

            [Category("O2 Flow Operating Conditions"),
            Description("Time [min]")]
            public List<double> O2_Time { get; set; }

            [Category("O2 Flow Operating Conditions"),
            Description("Flow Rate [Nm3/hr]")]
            public List<double> O2_FlowRate { get; set; }

            [Category("O2 Flow Operating Conditions"),
            Description("Temperature [C]")]
            public List<double> O2_Temperature { get; set; }

            [Category("O2 Flow Operating Conditions"),
            Description("Pressure [mmAq(g)]")]
            public List<double> O2_Pressure { get; set; }

            [Browsable(false)]
            public List<FlowOperatingConditionDataType> AirFlowOperatingConditions { get; set; }
            
            [Browsable(false)]
            public List<FlowOperatingConditionDataType> O2FlowOperatingConditions { get; set; }

            [Category("Relief Pressure Condition"), 
            Description("Time [min]")]
            public double ReliefTime { get; set; }

            [Category("Relief Pressure Condition"),
            Description("Air Pressure [bar]")]
            public double Air_ReliefPressure { get; set; }

            [Category("Relief Pressure Condition"),
            Description("O2 Pressure [bar]")]
            public double O2_ReliefPressure { get; set; }

            [Category("Equalizing Pressure Condition"),
            Description("Time [min]")]
            public double EqualizingTime { get; set; }

            [Category("Equalizing Pressure Condition"),
            Description("Air Pressure [bar]")]
            public double Air_EqualizingPressure { get; set; }

            [Category("Equalizing Pressure Condition"),
            Description("O2 Pressure [bar]")]
            public double O2_EqualizingPressure { get; set; }

            public ColdBlastDataType()
            {

            }
            public ColdBlastDataType(string filePath)
            {
                FileInfo fileInfo = new FileInfo(filePath);

                if (!fileInfo.Exists)
                    throw CException.Show(CException.Type.NoFile);

                XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);
                XmlNodeList xmlColdBlast = CFileIO.GetXmlSubNodeList(xmlAll, "ColdBlast");
                XmlNodeList xmlAirFlowOperatingConditions = CFileIO.GetXmlSubNodeList(xmlColdBlast, "AirFlowOperatingConditions");
                XmlNodeList xmlO2FlowOperatingConditions = CFileIO.GetXmlSubNodeList(xmlColdBlast, "O2FlowOperatingConditions");
                XmlNodeList xmlReliefConditions = CFileIO.GetXmlSubNodeList(xmlColdBlast, "ReliefConditions");
                XmlNodeList xmlEqualizingConditions= CFileIO.GetXmlSubNodeList(xmlColdBlast, "EqualizingConditions");

                List<List<double>> list_2D_Air = CFileIO.GetXml2DTableAsDoubleList(xmlAirFlowOperatingConditions);
                List<List<double>> list_2D_O2 = CFileIO.GetXml2DTableAsDoubleList(xmlO2FlowOperatingConditions);


                ReliefTime = Convert.ToDouble(CFileIO.GetXmlValueAsString(xmlReliefConditions, "ReliefTime"));
                Air_ReliefPressure = Convert.ToDouble(CFileIO.GetXmlValueAsString(xmlReliefConditions, "Air_ReliefPressure"));
                O2_ReliefPressure = Convert.ToDouble(CFileIO.GetXmlValueAsString(xmlReliefConditions, "O2_ReliefPressure"));

                EqualizingTime = Convert.ToDouble(CFileIO.GetXmlValueAsString(xmlEqualizingConditions, "EqualizingTime"));
                Air_EqualizingPressure= Convert.ToDouble(CFileIO.GetXmlValueAsString(xmlEqualizingConditions, "Air_EqualizingPressure"));
                O2_EqualizingPressure = Convert.ToDouble(CFileIO.GetXmlValueAsString(xmlEqualizingConditions, "O2_EqualizingPressure"));
                


                AirFlowOperatingConditions = new List<FlowOperatingConditionDataType>();
                for (int i = 0; i < list_2D_Air.Count; i++)
                {
                    Air_Time = list_2D_Air[i];
                    Air_FlowRate = list_2D_Air[i];
                    Air_Temperature = list_2D_Air[i];
                    Air_Pressure = list_2D_Air[i];

                    FlowOperatingConditionDataType AirflowOperatingConditions = new FlowOperatingConditionDataType();
                    AirflowOperatingConditions.Time = Air_Time[(int)FlowOperationConditionItem.Time];
                    AirflowOperatingConditions.FlowRate = Air_FlowRate[(int)FlowOperationConditionItem.FlowRate];
                    AirflowOperatingConditions.Temperature = Air_Temperature[(int)FlowOperationConditionItem.Temperature];
                    AirflowOperatingConditions.Pressure = Air_Pressure[(int)FlowOperationConditionItem.Pressure];

                    AirFlowOperatingConditions.Add(AirflowOperatingConditions);
                }

                O2FlowOperatingConditions = new List<FlowOperatingConditionDataType>();
                for (int i = 0; i < list_2D_O2.Count; i++)
                {
                    O2_Time = list_2D_O2[i];
                    O2_FlowRate = list_2D_O2[i];
                    O2_Temperature = list_2D_O2[i];
                    O2_Pressure = list_2D_O2[i];

                    FlowOperatingConditionDataType O2flowOperatingConditions = new FlowOperatingConditionDataType();
                    O2flowOperatingConditions.Time = O2_Time[(int)FlowOperationConditionItem.Time];
                    O2flowOperatingConditions.FlowRate = O2_FlowRate[(int)FlowOperationConditionItem.FlowRate];
                    O2flowOperatingConditions.Temperature = O2_Temperature[(int)FlowOperationConditionItem.Temperature];
                    O2flowOperatingConditions.Pressure = O2_Pressure[(int)FlowOperationConditionItem.Pressure];

                    O2FlowOperatingConditions.Add(O2flowOperatingConditions);
                }

                Air = new CGas();
                Air.MoleFraction = CGas.Fraction.DryAir();
                Air.MassFraction = Air.MoleFraction.GetMassFraction();

                O2 = new CGas();
                O2.MoleFraction = new CGas.Fraction(0);
                O2.MoleFraction[CGas.Composition.O2] = 1.0;
                O2.MassFraction = O2.MoleFraction.GetMassFraction();
            }

            public List<string> GetColdBlastDataToList()
            {
                List<string> contents = new List<string>();

                contents.Add(@"    <AirFlowOperatingConditions>");
                for (int i = 0; i < AirFlowOperatingConditions.Count; i++)
                {
                    contents.Add(@"      <Profile_" + i + @">");
                    contents.Add(@"        <Time>" + AirFlowOperatingConditions[i].Time + @"</Time>");
                    contents.Add(@"        <FlowRate>" + AirFlowOperatingConditions[i].FlowRate + @"</FlowRate>");
                    contents.Add(@"        <Temperature>" + AirFlowOperatingConditions[i].Temperature + @"</Temperature>");
                    contents.Add(@"        <Pressure>" + AirFlowOperatingConditions[i].Pressure + @"</Pressure>");
                    contents.Add(@"      </Profile_" + i + @">");
                }
                contents.Add(@"    </AirFlowOperatingConditions>");

                contents.Add(@"    <O2FlowOperatingConditions>");
                for (int i = 0; i < O2FlowOperatingConditions.Count; i++)
                {
                    contents.Add(@"      <Profile_" + i + @">");
                    contents.Add(@"        <Time>" + O2FlowOperatingConditions[i].Time + @"</Time>");
                    contents.Add(@"        <FlowRate>" + O2FlowOperatingConditions[i].FlowRate + @"</FlowRate>");
                    contents.Add(@"        <Temperature>" + O2FlowOperatingConditions[i].Temperature + @"</Temperature>");
                    contents.Add(@"        <Pressure>" + O2FlowOperatingConditions[i].Pressure + @"</Pressure>");
                    contents.Add(@"      </Profile_" + i + @">");
                }
                contents.Add(@"      </O2FlowOperatingConditions>");

                contents.Add(@"    <ReliefConditions>");
                contents.Add(@"      <ReliefTime>" + ReliefTime + @"</ReliefTime>");
                contents.Add(@"      <Air_ReliefPressure>" + Air_ReliefPressure + @"</Air_ReliefPressure>");
                contents.Add(@"      <O2_ReliefPressure>" + O2_ReliefPressure + @"</O2_ReliefPressure>");
                contents.Add(@"    </ReliefConditions>");


                contents.Add(@"    <EqualizingConditions>");
                contents.Add(@"      <EqualizingTime>" + ReliefTime + @"</EqualizingTime>");
                contents.Add(@"      <Air_EqualizingPressure>" + Air_EqualizingPressure + @"</Air_EqualizingPressure>");
                contents.Add(@"      <O2_EqualizingPressure>" + O2_EqualizingPressure + @"</O2_EqualizingPressure>");
                contents.Add(@"    </EqualizingConditions>");

                return contents;
            }
        }
        public ColdBlastDataType ColdBlast { get; set; }

        public class BrickConfigurationDataType
        {
            [Category("Brick Configuration"),
            Description("Default length(mm), diameter(mm) and number of bricks")]
            public double Length { get; set; }

            [Category("Brick Configuration"),
            Description("Default length(mm), diameter(mm) and number of bricks")]
            public double Diameter { get; set; }

            [Category("Brick Configuration"),
            Description("Default length(mm), diameter(mm) and number of bricks")]
            public double NumberofBricks { get; set; }

            [Category("Brick Names"),
            Description("Available Brick Types")]

            public List<string> BrickNames { get; set; }

            [Category("Brick Heights"),
            Description("Height of Individual Brick")]
            public List<double> BrickHeights { get; set; }

            public BrickConfigurationDataType()
            {

            }
            public BrickConfigurationDataType(string filePath)
            {
                FileInfo fileInfo = new FileInfo(filePath);
               

                if (!fileInfo.Exists)
                    throw CException.Show(CException.Type.NoFile);

                XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);
                XmlNodeList xmlBrickConfiguration = CFileIO.GetXmlSubNodeList(xmlAll, "BrickConfiguration");
                XmlNodeList xmlBrickNames = CFileIO.GetXmlSubNodeList(xmlBrickConfiguration, "BrickNames");
                XmlNodeList xmlBrickHeights = CFileIO.GetXmlSubNodeList(xmlBrickConfiguration, "BrickHeights");
                XmlNodeList xmlUnitBrick = CFileIO.GetXmlSubNodeList(xmlBrickConfiguration, "UnitBrick");

                Length = CFileIO.GetXmlValueAsDouble(xmlUnitBrick, "Length");
                Diameter = CFileIO.GetXmlValueAsDouble(xmlUnitBrick, "Diameter");
                NumberofBricks = CFileIO.GetXmlValueAsInt(xmlUnitBrick, "NumberofBricks");
                BrickNames = CFileIO.GetXml1DTableAsStringList(xmlBrickNames);
                BrickHeights = CFileIO.GetXml1DTableAsDoubleList(xmlBrickHeights);     
            }

            public List<string> GetBrickConfigurationDataToList()
            {
                List<string> contents = new List<string>();
                contents.Add(@"    <BrickNames>");
                for (int i = 0; i < BrickNames.Count; i++)
                {
                    contents.Add(@"      <brick_" + (i+1) + @">" + BrickNames[i] + @"</brick_" + (i + 1) + @">");
                }
                contents.Add(@"    </BrickNames>");

                contents.Add(@"    <BrickHeights>");
                for (int i = 0; i < BrickHeights.Count; i++)
                {
                    contents.Add(@"      <brick_" + (i+1) + @">" + BrickHeights[i] + @"</brick_" + (i + 1) + @">");
                }
                contents.Add(@"    </BrickHeights>");

                contents.Add(@"    <UnitBrick>");
                contents.Add(@"      <Length>" + Length + @"</Length>");
                contents.Add(@"      <Diameter>" + Diameter + @"</Diameter>");
                contents.Add(@"      <NumberofBricks>" + NumberofBricks + @"</NumberofBricks>");
                contents.Add(@"    </UnitBrick>");
                return contents;
            }
        }
        public BrickConfigurationDataType BrickConfiguration { get; set; }

        public class CalculationSettingDataType
        {
            [Category("Ambient Conditions"),
            Description("Convection Heat Transfer Coefficients (W/m2K)")]
            public double ConvectionHeatTransferCoeffcient { get; set; }

            [Category("Ambient Conditions"),
            Description("AmbientTemperature (C)")]
            public double AmbientTemperature { get; set; }

            public CalculationSettingDataType()
            {

            }
            public CalculationSettingDataType(string filePath)
            {
                FileInfo fileInfo = new FileInfo(filePath);


                if (!fileInfo.Exists)
                    throw CException.Show(CException.Type.NoFile);

                XmlNodeList xmlAll = CFileIO.GetXmlAllNodeList(filePath);
                XmlNodeList xmlCalculationSetting = CFileIO.GetXmlSubNodeList(xmlAll, "CalculationSetting");

                ConvectionHeatTransferCoeffcient = CFileIO.GetXmlValueAsDouble(xmlCalculationSetting, "ConvectionHeatTransferCoefficient");
                AmbientTemperature = CFileIO.GetXmlValueAsDouble(xmlCalculationSetting, "AmbientTemperature") - 273.15;


            }
        }

        public CalculationSettingDataType CalculationSetting { get; set; }


        public ST_UD()
        {
            M_GasCalculation = new M_GasCalculationDataType();
            CombustionCalculation = new CombustionCalculationDataType();
            CombustedGas = new CombustedGasDataType();
            ColdBlast = new ColdBlastDataType();
            BrickConfiguration = new BrickConfigurationDataType();
            CalculationSetting = new CalculationSettingDataType();
        }

        public static ST_UD GetInstance()
        {
            if (_objThis == null)
                _objThis = new ST_UD();

            return _objThis;
        }

        }
}