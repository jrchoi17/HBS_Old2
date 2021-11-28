using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HBS
{
    public partial class AmbientConditionForm : Form
    {
        public bool IsFinish { get; set; }
        
        public double H_ext { get; set; }
        public double T_inf { get; set; }

        public AmbientConditionForm()
        {
            InitializeComponent();
            IsFinish = false;
        }

        public void SetAllDataFromUd()
        {
            txtConvectionHeatTransferCoefficient.Text = H_ext.ToString("#00.0");
            txtTemperature.Text = T_inf.ToString("#00.0");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            H_ext = double.Parse(txtConvectionHeatTransferCoefficient.Text);
            T_inf = double.Parse(txtTemperature.Text);

            DialogResult = DialogResult.OK;
        }
    }
}