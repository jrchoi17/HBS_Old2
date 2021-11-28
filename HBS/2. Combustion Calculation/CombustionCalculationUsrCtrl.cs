using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HBS_Shared;

namespace HBS
{
    public partial class CombustionCalculationUsrCtrl : UserControl
    {

        #region Variables
        public bool IsFinish { get; set; }

        #endregion



        #region Constructor method
        public CombustionCalculationUsrCtrl()
        {
            InitializeComponent();
            gasCompUsrCtrl.Gas0Name = "M-Gas";
            gasCompUsrCtrl.Gas1Name = "Combustion-Air";
            gasCompUsrCtrl.Gas2Name = null;
            gasCompUsrCtrl.Gas10Name = "Combusted-Gas";
            IsFinish = false;
        }

        public CombustionCalculationUsrCtrl(string filePath)
        {
            InitializeComponent();
            gasCompUsrCtrl.Gas0Name = "M-Gas";
            gasCompUsrCtrl.Gas1Name = "Combustion-Air";
            gasCompUsrCtrl.Gas2Name = null;
            gasCompUsrCtrl.Gas10Name = "Combusted-Gas";
            IsFinish = false;

            SetDataFromFile(filePath);
        }
        #endregion

        

        #region Event methods
        public void SetDataFromFile(string filePath)
        {
            ST_UD ud = ST_UD.GetInstance();

            ud.CombustionCalculation = new ST_UD.CombustionCalculationDataType(filePath);

            SetDataFromUd();
        }

        public void SetDataFromUd()
        {
            ST_UD ud = ST_UD.GetInstance();

            gasCompUsrCtrl.Init();
            gasCompUsrCtrl.SetInitialFractionType(GasCompUsrCtrl.FractionType.Mole);
            gasCompUsrCtrl.Add("M-Gas", ud.CombustionCalculation.MGas, doughnutCharUsrCtrl1);
            gasCompUsrCtrl.Add("Air", ud.CombustionCalculation.Air, doughnutCharUsrCtrl2);
            gasCompUsrCtrl.Add("Combusted Gas", ud.CombustionCalculation.CombustedGas, doughnutCharUsrCtrl3);
            txtM_gasFlowRate.Text = ud.CombustionCalculation.MGas_FlowRate.ToString("#0.00");
            txtM_gasTemperature.Text = ud.CombustionCalculation.MGas_Temperature.ToString("#0.00");
            txtM_gasPressure.Text = ud.CombustionCalculation.MGas_Pressure.ToString("#0.00");
            txtAirFlowRate.Text = ud.CombustionCalculation.Air_FlowRate.ToString("#0.00");
            txtAirTemperature.Text = ud.CombustionCalculation.Air_Temperature.ToString("#0.00");
            txtAirPressure.Text = ud.CombustionCalculation.Air_Pressure.ToString("#0.00");
            gasCompUsrCtrl.Finish();
            gasCompUsrCtrl.SetChart(-1);
        }


        #region Event methods::Button click methods
        private void btnLoadDefault_Click(object sender, EventArgs e)
        {
            SetDataFromFile(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);
        }

        private void btnImportCSVFile_Click(object sender, EventArgs e)
        {
            gasCompUsrCtrl.ReadCsvFile();
        }

        private void btnExportCsvFile_Click(object sender, EventArgs e)
        {
            gasCompUsrCtrl.WriteCsvFile();
        }

        private void btnNormalizing_Click(object sender, EventArgs e)
        {
            gasCompUsrCtrl.SetNormalizing();
        }

        private void btnHumidAir_Click(object sender, EventArgs e)
        {
            HumidAirForm form = new HumidAirForm();
            form.HumidAir = gasCompUsrCtrl.Gas1;

            if (form.ShowDialog() == DialogResult.OK)
            {
                gasCompUsrCtrl.Gas1 = form.HumidAir;
            }
        }

        private void btnCalculation_Click(object sender, EventArgs e)
        {
            SelectCombustionModuleForm form = new SelectCombustionModuleForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadingForm loading = new LoadingForm();
                loading.Show();

                Cantera cantera = new Cantera();
                cantera.Type = form.Type;
                cantera.IsAuto = form.IsAuto;
                cantera.FlameWidth = form.FlameWidth;

                cantera.T_mgas = double.Parse(txtM_gasTemperature.Text);
                cantera.T_air = double.Parse(txtAirTemperature.Text);
                cantera.p_mgas = double.Parse(txtM_gasPressure.Text);
                cantera.p_air = double.Parse(txtAirPressure.Text);
                cantera.Q_mgas = double.Parse(txtM_gasFlowRate.Text);
                cantera.Q_air = double.Parse(txtAirFlowRate.Text);
                cantera.M_Gas = gasCompUsrCtrl.Gas0;
                cantera.Air = gasCompUsrCtrl.Gas1;

                cantera.WritePyFile();
                cantera.ExcuteCantera();
                cantera.UpdateData();


                if (MessageBox.Show("Calculation complete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    loading.Dispose();

                if (!gbCombustionSimulationResults.Visible)
                    gbCombustionSimulationResults.Visible = true;

                gasCompUsrCtrl.Gas10 = cantera.Combusted_Gas;
                //gasCompUsrCtrl.UpdateAll();

                txtCombustedGasTemperature.Text = cantera.T_combustedgas.ToString("#00.0");
                txtCombustedGasPressure.Text = cantera.p_combustedgas.ToString("#00.0");
                txtCombustedGasCO.Text = cantera.X_CO_combustedgas.ToString("#00.0");
                txtCombustedGasNOx.Text = cantera.X_NOx_combustedgas.ToString("#00.0");
                // textbox update here!
                IsFinish = true;
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", Application.StartupPath + "\\results.txt");
        }
        #endregion


        private void txtN_FlowRate_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if (!CValidityCheck.IsPositiveNumber(d, false, e))
                return;
        }

        /// <summary>
        /// Event method for validating temperature value for m-gas and air.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void txtTemperatureInput_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if (!CValidityCheck.IsInRangeTemp(e, -273.0, 2000.0, d))
                return;
        }

        /// <summary>
        /// Event method for validating pressure value for m-gas and air.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void txtPressureInput_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if (!CValidityCheck.IsInRangePressure(e, -10332.0, 10332.0 * 50.0, d))
                return;
        }

        /// <summary>
        /// Event method for adding chart and gas composition values.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void gasCompUsrCtrl1_Load(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();


            gasCompUsrCtrl.Init();
            ud.CombustionCalculation.MGas = ud.M_GasCalculation.MGas;
                    

            gasCompUsrCtrl.SetInitialFractionType(GasCompUsrCtrl.FractionType.Mole);
            gasCompUsrCtrl.Add("M-Gas", ud.CombustionCalculation.MGas, doughnutCharUsrCtrl1);
            gasCompUsrCtrl.Add("Air", ud.CombustionCalculation.Air, doughnutCharUsrCtrl2);
            gasCompUsrCtrl.Add("Combusted Gas", ud.CombustionCalculation.CombustedGas, doughnutCharUsrCtrl3);
            txtM_gasFlowRate.Text = ud.CombustionCalculation.MGas_FlowRate.ToString("#0.00");
            txtM_gasTemperature.Text = ud.CombustionCalculation.MGas_Temperature.ToString("#0.00");
            txtM_gasPressure.Text = ud.CombustionCalculation.MGas_Pressure.ToString("#0.00");
            txtAirFlowRate.Text = ud.CombustionCalculation.Air_FlowRate.ToString("#0.00");
            txtAirTemperature.Text = ud.CombustionCalculation.Air_Temperature.ToString("#0.00");
            txtAirPressure.Text = ud.CombustionCalculation.Air_Pressure.ToString("#0.00");

            gasCompUsrCtrl.Finish();
        }
        
        /// <summary>
        /// Event method for updating ud data once gas flowrate text box is validated.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void txtM_gasFlowRate_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();
            TextBox textBox = (TextBox)sender;

            if (textBox.Equals(txtM_gasFlowRate))
            {
                ud.CombustionCalculation.MGas_FlowRate = double.Parse(textBox.Text);
                ud.PropertyGrid.SelectedObject = ud.CombustionCalculation;
            }
            else
                throw CException.Show(CException.Type.UnsupportedKeyword);
        }

        /// <summary>
        /// Event method for updating ud data once air flowrate text box is validated.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void txtAirFlowRate_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();
            TextBox textBox = (TextBox)sender;

            if (textBox.Equals(txtAirFlowRate))
            {
                ud.CombustionCalculation.Air_FlowRate = double.Parse(textBox.Text);
                ud.PropertyGrid.SelectedObject = ud.CombustionCalculation;
            }
            else
                throw CException.Show(CException.Type.UnsupportedKeyword);
        }

        /// <summary>
        /// Event method for updating ud data once gas temperature text box is validated.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void txtM_gasTemperature_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();
            TextBox textBox = (TextBox)sender;

            if (textBox.Equals(txtM_gasTemperature))
            {
                ud.CombustionCalculation.MGas_Temperature = double.Parse(textBox.Text);
                ud.PropertyGrid.SelectedObject = ud.CombustionCalculation;
            }
            else
                throw CException.Show(CException.Type.UnsupportedKeyword);
        }

        /// <summary>
        /// Event method for updating ud data once air temperature text box is validated.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void txtAirTemperature_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();
            TextBox textBox = (TextBox)sender;

            if (textBox.Equals(txtAirTemperature))
            {
                ud.CombustionCalculation.Air_Temperature = double.Parse(textBox.Text);
                ud.PropertyGrid.SelectedObject = ud.CombustionCalculation;
            }
            else
                throw CException.Show(CException.Type.UnsupportedKeyword);
        }

        /// <summary>
        /// Event method for updating ud data once M-gas pressure text box is validated.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void txtM_gasPressure_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();
            TextBox textBox = (TextBox)sender;

            if (textBox.Equals(txtM_gasPressure))
            {
                ud.CombustionCalculation.MGas_Pressure = double.Parse(textBox.Text);
                ud.PropertyGrid.SelectedObject = ud.CombustionCalculation;
            }
            else
                throw CException.Show(CException.Type.UnsupportedKeyword);
        }

        /// <summary>
        /// Event method for updating ud data once air pressure text box is validated.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void txtAirPressure_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();
            TextBox textBox = (TextBox)sender;

            if (textBox.Equals(txtAirPressure))
            {
                ud.CombustionCalculation.Air_Pressure = double.Parse(textBox.Text);
                ud.PropertyGrid.SelectedObject = ud.CombustionCalculation;
            }
            else
                throw CException.Show(CException.Type.UnsupportedKeyword);
        }
   
        /// <summary>
        /// Event method for updating ud data once gas composition value is validated.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
       
        #endregion
    }
}
