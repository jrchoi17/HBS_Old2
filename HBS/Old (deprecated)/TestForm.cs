using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using HBS_Shared;

namespace HBS
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();

            ST_UD ud = ST_UD.GetInstance();
            ud.M_GasCalculation = new ST_UD.M_GasCalculationDataType(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);

        }

        private void TestForm_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}