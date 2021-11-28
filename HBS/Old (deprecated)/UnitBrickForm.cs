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
    public partial class UnitBrickForm : Form
    {
        public double L { get; set; }
        public double D { get; set; }
        public double N { get; set; }
        public bool IsFinish { get; set; }

        public UnitBrickForm()
        {
            InitializeComponent();
            IsFinish = false;
        }

        public void SetAllDataFromUd()
        {
            txtChracteristicLengthOfUnitBrick.Text = L.ToString("#00.0");
            txtDiameterOfUnitHole.Text = D.ToString("#00.0");
            txtTheNumberOfUnitBricks.Text = N.ToString("#00");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            L = double.Parse(txtChracteristicLengthOfUnitBrick.Text);
            D = double.Parse(txtDiameterOfUnitHole.Text);
            N = double.Parse(txtTheNumberOfUnitBricks.Text);

            DialogResult = DialogResult.OK;
        }
    }
}
