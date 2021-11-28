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
    public partial class FractionForm : Form
    {
        public GasCompUsrCtrl.FractionType FractionType { get; set; }
        public bool Normalize { get; set; }

        public FractionForm()
        {
            InitializeComponent();
        }

        private void FractionForm_Load(object sender, EventArgs e)
        {
            Normalize = true;
        }

        private void rbMoleFraction_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMoleFraction.Checked)
                FractionType = GasCompUsrCtrl.FractionType.Mole;
        }

        private void rbMassFraction_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMassFraction.Checked)
                FractionType = GasCompUsrCtrl.FractionType.Mass;
        }

        private void cbNormalize_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            Normalize = checkBox.Checked;
        }
    }
}
