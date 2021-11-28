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
    public partial class BrickInfoForm : Form
    {
        public string BrickName { get; set; }
        public double BrickHeight { get; set; }

        public BrickInfoForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            BrickHeight = double.Parse(txtHeight.Text);
            DialogResult = DialogResult.OK;
        }

        private void BrickInfoForm_Load(object sender, EventArgs e)
        {
            txtName.Text = BrickName;
            txtHeight.Text = BrickHeight.ToString();
        }
    }
}
