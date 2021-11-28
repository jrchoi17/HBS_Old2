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
        public CombustedGasDataType CombustedGas { get; set; }

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
    }
}
