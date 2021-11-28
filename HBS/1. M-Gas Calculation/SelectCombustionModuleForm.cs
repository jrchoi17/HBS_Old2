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
    public partial class SelectCombustionModuleForm : Form
    {
        public Cantera.CalculationType Type { get; set; }
        public bool IsAuto { get; set; }
        public double FlameWidth { get; set; }

        public SelectCombustionModuleForm()
        {
            InitializeComponent();
        }

        private void SelectCombustionModuleForm_Load(object sender, EventArgs e)
        {
            rb0DCantera.Checked = true;
            cbSpecifiedValue.Checked = true;
            Type = Cantera.CalculationType.Sim0D;
            IsAuto = true;
        }

        private void rb0DCantera_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            GroupBox groupBox = gb1DCanteraParameter;

            groupBox.Enabled = !radioButton.Checked;
            Type = Cantera.CalculationType.Sim0D;
        }

        private void rb1D_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            GroupBox groupBox = gb1DCanteraParameter;

            groupBox.Enabled = radioButton.Checked;
            Type = Cantera.CalculationType.Sim1D;
        }

        private void cbSpecifiedValue_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            txtWidth.Enabled = checkBox.Checked;
            IsAuto = checkBox.Checked;
        }

        private void btnCalculation_Click(object sender, EventArgs e)
        {
            FlameWidth = double.Parse(txtWidth.Text);
            DialogResult = DialogResult.OK;
        }

        private void txtWidth_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if (!CValidityCheck.IsPositiveNumber(d, false, e))
                return;
        }
    }
}