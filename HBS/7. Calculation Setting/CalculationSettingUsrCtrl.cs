using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HBS_Shared;

namespace HBS
{
    public partial class CalculationSettingUsrCtrl : UserControl
    {
        public CalculationSettingUsrCtrl()
        {
            InitializeComponent();
        }

        public CalculationSettingUsrCtrl(string filePath)
        {
            InitializeComponent();

            UpdateFromFile(filePath);
        }

        public void UpdateFromFile(string filePath)
        {
            ST_UD ud = ST_UD.GetInstance();
            ud.CalculationSetting = new ST_UD.CalculationSettingDataType(filePath);

            UpdateFromUD();
        }

        public void UpdateFromUD()
        {
            ST_UD ud = ST_UD.GetInstance();
            ST_UD.CalculationSettingDataType dataType = ud.CalculationSetting;

            // Ambient Conditions
            cbUseConvectiveHeatLoss.Checked = dataType.UseConvectiveHeatLoss;
            txtConvectionHeatTransferCoefficient.Text = dataType.ConvectionHeatTransferCoeffcient.ToString("#0.00");
            txtAmbientTemperature.Text = dataType.AmbientTemperature.ToString("#0.00");
            
            // Process Settings
            txtCurrentCycle.Text = dataType.CurrentCycle.ToString();
            txtNumberOfProcesses.Text = dataType.NumberOfProcesses.ToString();
            txtProcessTime.Text = dataType.ProcessTime.ToString("#0.00");

            // Time Settings
            txtCurrentTime.Text = dataType.CurrentTime.ToString("#0.00");
            txtTimeInterval.Text = dataType.TimeInterval.ToString("#0.00");
        }

        private void cbUseConvectiveHeatLoss_CheckedChanged(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();
            CheckBox checkBox = (CheckBox)sender;

            ud.CalculationSetting.UseConvectiveHeatLoss = checkBox.Checked;
            txtConvectionHeatTransferCoefficient.Enabled = checkBox.Checked;
            txtAmbientTemperature.Enabled = checkBox.Checked;
        }

        private void CalculationSettingUsrCtrl_Load(object sender, EventArgs e)
        {
            UpdateFromUD();
        }
    }
}
