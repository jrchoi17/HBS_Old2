using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateChartRealTime
{
    public partial class Form1 : Form
    {
        private List<double> time = null;
        private List<double> list = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            list = new List<double>();

            for (int i = 0; true; i++)
            {

                list.Add(Math.Sin(i / Math.PI));
            }
                
        }
    }
}
