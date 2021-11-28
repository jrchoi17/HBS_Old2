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
        public CalculationSettingDataType CalculationSetting { get; set; }

        public class CalculationSettingDataType
        {
            [Category("Ambient Conditions"),
            Description("Use Convective Heat Loss?")]
            public bool UseConvectiveHeatLoss { get; set; }

            [Category("Ambient Conditions"),
            Description("Convection Heat Transfer Coefficients (W/m2K)")]
            public double ConvectionHeatTransferCoeffcient { get; set; }

            [Category("Ambient Conditions"),
            Description("Ambient Temperature (C)")]
            public double AmbientTemperature { get; set; }

            [Category("Process Settings"),
            Description("Current Cycle. If you have no set value, 0 is recommended the first time.")]
            public int CurrentCycle { get; set; }
            
            [Category("Process Settings"),
            Description("Number of Processes for Soving.")]
            public int NumberOfProcesses { get; set; }

            [Category("Process Settings"),
            Description("Process Time per 1 Cycle (min)")]
            public double ProcessTime { get; set; }

            [Category("Time Settings"),
            Description("Current Time (sec). If you have no set value, 0.0 is recommended the first time.")]
            public double CurrentTime { get; set; }

            [Category("Time Settings"),
            Description("Time Interval (sec).")]
            public double TimeInterval { get; set; }

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

                UseConvectiveHeatLoss = CFileIO.GetxmlValueAsBool(xmlCalculationSetting, "UseConvectiveHeatLoss");
                ConvectionHeatTransferCoeffcient = CFileIO.GetXmlValueAsDouble(xmlCalculationSetting, "ConvectionHeatTransferCoefficient");
                AmbientTemperature = CFileIO.GetXmlValueAsDouble(xmlCalculationSetting, "AmbientTemperature") - 273.15;
                CurrentCycle = CFileIO.GetXmlValueAsInt(xmlCalculationSetting, "CurrentCycle");
                NumberOfProcesses = CFileIO.GetXmlValueAsInt(xmlCalculationSetting, "NumberOfProcesses");
                ProcessTime = CFileIO.GetXmlValueAsDouble(xmlCalculationSetting, "ProcessTime") / 60.0;
                CurrentTime = CFileIO.GetXmlValueAsDouble(xmlCalculationSetting, "CurrentTime");
                TimeInterval = CFileIO.GetXmlValueAsDouble(xmlCalculationSetting, "TimeInterval");
            }
        }
    }
}
