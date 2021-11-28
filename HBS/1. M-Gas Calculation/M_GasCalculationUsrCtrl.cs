using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using HBS_Shared;

namespace HBS
{
    public partial class M_GasCalculationUsrCtrl : UserControl
    {
        #region Variable
        public bool IsFinish { get; set; }

        #endregion



        #region Constructor methods
        public M_GasCalculationUsrCtrl()
        {
            InitializeComponent();
            
            gasCompUsrCtrl.Gas0Name = "BFG";
            gasCompUsrCtrl.Gas1Name = "COG";
            gasCompUsrCtrl.Gas2Name = "X-Gas";
            gasCompUsrCtrl.Gas10Name = "M-Gas";


            IsFinish = false;
            
        }

        public M_GasCalculationUsrCtrl(string filePath)
        {
            InitializeComponent();

            gasCompUsrCtrl.Gas0Name = "BFG";
            gasCompUsrCtrl.Gas1Name = "COG";
            gasCompUsrCtrl.Gas2Name = "X-Gas";
            gasCompUsrCtrl.Gas10Name = "M-Gas";

            IsFinish = false;

            SetDataFromFile(filePath);
        }
        #endregion


        #region Event methods
        private void M_GasCalculationUsrCtrl_Load(object sender, EventArgs e)
        {
            SetDataFromUd();
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
            gasCompUsrCtrl.SetChart(-1);
        }

        private void btnCalculation_Click(object sender, EventArgs e)
        {
            gasCompUsrCtrl.Calculate(txtBFG, txtCOG, txtXGas);
           
            MessageBox.Show("M-gas calculation complete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            gasCompUsrCtrl.SetChart(-1);
        }
        #endregion

        #region Event methods::Validating methods
        private void txtGasN_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if (!CValidityCheck.IsPositiveNumber(d, true, e))
                return;
        }
        #endregion

        #region Event methods::Validated methods
        private void txtBFG_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();
            TextBox textBox = (TextBox)sender;

            ud.M_GasCalculation.X_BFG = double.Parse(textBox.Text) / 100.0;
            ud.PropertyGrid.SelectedObject = ud.M_GasCalculation;
        }
        private void txtCOG_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();
            TextBox textBox = (TextBox)sender;

            ud.M_GasCalculation.X_COG = double.Parse(textBox.Text) / 100.0;
            ud.PropertyGrid.SelectedObject = ud.M_GasCalculation;
        }

        private void txtXGas_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();
            TextBox textBox = (TextBox)sender;

            ud.M_GasCalculation.X_XGas = double.Parse(textBox.Text) / 100.0;
            ud.PropertyGrid.SelectedObject = ud.M_GasCalculation;
        }
        #endregion
        #endregion

        #region public methods
        public void SetDataFromFile(string filePath)
        {
            ST_UD ud = ST_UD.GetInstance();

            ud.M_GasCalculation = new ST_UD.M_GasCalculationDataType(filePath);
            SetDataFromUd();
        }

        public void SetDataFromUd()
        {
            ST_UD ud = ST_UD.GetInstance();

            gasCompUsrCtrl.Init();
            gasCompUsrCtrl.SetInitialFractionType(GasCompUsrCtrl.FractionType.Mole);

            gasCompUsrCtrl.Add("BFG", ud.M_GasCalculation.BFG, doughnutCharUsrCtrl1);
            gasCompUsrCtrl.Add("COG", ud.M_GasCalculation.COG, doughnutCharUsrCtrl2);
            gasCompUsrCtrl.Add("X-Gas", ud.M_GasCalculation.XGas, doughnutCharUsrCtrl3);
            gasCompUsrCtrl.Add("M-Gas", ud.M_GasCalculation.MGas, doughnutCharUsrCtrl4);
            txtBFG.Text = (ud.M_GasCalculation.X_BFG * 100.0).ToString("#0.00");
            txtCOG.Text = (ud.M_GasCalculation.X_COG * 100.0).ToString("#0.00");
            txtXGas.Text = (ud.M_GasCalculation.X_XGas * 100.0).ToString("#0.00");
            gasCompUsrCtrl.SetInitialFractionType(GasCompUsrCtrl.FractionType.Mole);
            gasCompUsrCtrl.Finish();
            gasCompUsrCtrl.SetChart(-1);
        }
        #endregion
    }
}
