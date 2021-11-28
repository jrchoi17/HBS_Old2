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
    public partial class ST_UD
    {
        #region Enum & Structure
        public struct FlowOperatingConditionDataType
        {
            public double Time { get; set; }
            public double FlowRate { get; set; }
            public double Temperature { get; set; }
            public double Pressure { get; set; }
        }
        public enum FlowOperationConditionItem { Time = 0, FlowRate, Temperature, Pressure }
        #endregion

        public PropertyGrid PropertyGrid { get; set; }

        private static ST_UD _objThis = null;

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

        public void SetDataToSD()
        {
            ST_SD sd = ST_SD.GetInstance();


        }
    }
}