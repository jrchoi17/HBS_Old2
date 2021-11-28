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
        public ColdBlastDataType ColdBlast { get; set; }

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
                XmlNodeList xmlEqualizingConditions = CFileIO.GetXmlSubNodeList(xmlColdBlast, "EqualizingConditions");

                List<List<double>> list_2D_Air = CFileIO.GetXml2DTableAsDoubleList(xmlAirFlowOperatingConditions);
                List<List<double>> list_2D_O2 = CFileIO.GetXml2DTableAsDoubleList(xmlO2FlowOperatingConditions);


                ReliefTime = Convert.ToDouble(CFileIO.GetXmlValueAsString(xmlReliefConditions, "ReliefTime"));
                Air_ReliefPressure = Convert.ToDouble(CFileIO.GetXmlValueAsString(xmlReliefConditions, "Air_ReliefPressure"));
                O2_ReliefPressure = Convert.ToDouble(CFileIO.GetXmlValueAsString(xmlReliefConditions, "O2_ReliefPressure"));

                EqualizingTime = Convert.ToDouble(CFileIO.GetXmlValueAsString(xmlEqualizingConditions, "EqualizingTime"));
                Air_EqualizingPressure = Convert.ToDouble(CFileIO.GetXmlValueAsString(xmlEqualizingConditions, "Air_EqualizingPressure"));
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
    }
}
