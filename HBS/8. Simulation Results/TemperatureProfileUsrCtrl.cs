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
    public partial class TemperatureProfileUsrCtrl : UserControl
    {
        public enum TemperatureType { Volume = 0, Surface }
        public TemperatureType Type { get; set; }

        public TemperatureProfileUsrCtrl()
        {
            InitializeComponent();
            txtCycle.Text = "0";
            txtElapsedTimePerCycle.Text = "0.0";
        }

        private void TemperatureProfileUsrCtrl_Load(object sender, EventArgs e)
        {
            ST_PD pd = ST_PD.GetInstance();
            
            trbCycle.Minimum = 0;
            trbCycle.Maximum = pd.GetLastCycle();
            trbCycle.TickFrequency = 1;
            trbCycle.Value = trbCycle.Maximum;
           
            trbElapsedTimePerCycle.Minimum = 0;
            trbElapsedTimePerCycle.TickFrequency = 1;
        }

        private void DrawGraph(int cycle, int timeStepPerCycle)
        {
            ST_PD pd = ST_PD.GetInstance();

            List<double> fluidTemperature = pd.GetTemperature(cycle, timeStepPerCycle, ST_PD.RegionType.Fluid, ST_PD.ComponentType.RegenerativeStage);
            List<double> solidTemperature = pd.GetTemperature(cycle, timeStepPerCycle, ST_PD.RegionType.Solid, ST_PD.ComponentType.RegenerativeStage);
            List<double> wallTemperature = pd.GetTemperature(cycle, timeStepPerCycle, ST_PD.RegionType.Wall, ST_PD.ComponentType.RegenerativeStage);

            List<double> length = pd.GetRelativePosition(ST_PD.ComponentType.RegenerativeStage);

            List<string> title = new List<string>();
            List<List<double>> data = new List<List<double>>();

            title.Add("T_Fluid");
            data.Add(fluidTemperature);
            title.Add("T_Solid");
            data.Add(solidTemperature);
            title.Add("T_Wall");
            data.Add(wallTemperature);

            DrawGraph(length, title, data);
        }

        private void DrawGraph(List<double> length, List<string> titles, List<List<double>> data)
        {
            chart.Series.Clear();

            for (int i = 0; i < data.Count; i++)
            {
                chart.Series.Add(titles[i]);
                chart.Series[i].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart.Series[i].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;


                for (int j = 0; j < data[0].Count; j++)
                    chart.Series[i].Points.AddXY(data[i][j], length[j]);
            }
        }

        private void trbCycle_ValueChanged(object sender, EventArgs e)
        {
            ST_PD pd = ST_PD.GetInstance();

            TrackBar trackBar = (TrackBar)sender;
            int cycle = trackBar.Value;

            txtCycle.Text = trbCycle.Value.ToString();
            trbElapsedTimePerCycle.Maximum = pd.GetTimeStepPerCycle(cycle).Max();
            trbElapsedTimePerCycle.Value = trbElapsedTimePerCycle.Maximum;

            if (trbElapsedTimePerCycle.Maximum == trbElapsedTimePerCycle.Value)
                DrawGraph(cycle, trbElapsedTimePerCycle.Value);

            //snaps trackbar to ticks
            if (trbCycle.Value % trbCycle.TickFrequency != 0)
            {
                trbCycle.Value = (trbCycle.Value / trbCycle.TickFrequency) * trbCycle.TickFrequency;
            }
        }

        private void trbElapsedTimePerCycle_ValueChanged(object sender, EventArgs e)
        {
            txtElapsedTimePerCycle.Text = trbElapsedTimePerCycle.Value.ToString();

            TrackBar trackBar = (TrackBar)sender;
            int cycle = trbCycle.Value;
            int timeStepPerCycle = trackBar.Value;

            if (trbElapsedTimePerCycle.Value % trbElapsedTimePerCycle.TickFrequency != 0)
            {
                trbElapsedTimePerCycle.Value = (trbElapsedTimePerCycle.Value / trbElapsedTimePerCycle.TickFrequency) * trbElapsedTimePerCycle.TickFrequency;
            }

            DrawGraph(cycle, timeStepPerCycle);
        }


        private void txtCycle_Validating(object sender, CancelEventArgs e)
        {
            
            double d = 0.0;
            
            if (!CValidityCheck.IsNumber(txtCycle, e, out d))
                return;

            if (!CValidityCheck.IsPositiveNumber(d, true, e))
                return;

            if (!CValidityCheck.IsInRangeCycle(e, 0, trbCycle.Maximum, d))
            {
                return;
            }
        }

        private void txtElapsedTimePerCycle_Validating(object sender, CancelEventArgs e)
        {
            double d = 0.0;
            if (!CValidityCheck.IsNumber(txtElapsedTimePerCycle, e, out d))
                return;

            if (!CValidityCheck.IsPositiveNumber(d, true, e))
                return;

            if (!CValidityCheck.IsInRangeCycle(e, 0, trbElapsedTimePerCycle.Maximum, d))
            {
                return;
            }
        }

        private void txtCycle_Validated(object sender, EventArgs e)
        {
            double value;

            if (double.TryParse(txtCycle.Text, out value))
            {
                int output = Convert.ToInt32(Math.Round(value));
                trbCycle.Value = output;
            }
            else
                trbElapsedTimePerCycle.Value = int.Parse(txtCycle.Text);
        }

        private void txtElapsedTimePerCycle_Validated(object sender, EventArgs e)
        {
            double value;

            if (double.TryParse(txtElapsedTimePerCycle.Text, out value))
            {
                int output = Convert.ToInt32(Math.Round(value));
                trbElapsedTimePerCycle.Value = output/10;
            }       
            else
                trbElapsedTimePerCycle.Value = int.Parse(txtElapsedTimePerCycle.Text);
        }

        private void trbCycle_Scroll(object sender, EventArgs e)
        {
            chContinueUpdatingTemperatureProfile.Checked = false;
        }

        private void trbElapsedTimePerCycle_Scroll(object sender, EventArgs e)
        {
            chContinueUpdatingTemperatureProfile.Checked = false;
        }

        private void chContinueUpdatingTemperatureProfile_CheckedChanged(object sender, EventArgs e)
        {
            if (chContinueUpdatingTemperatureProfile.Checked)
            {
                trbCycle.Value = trbCycle.Maximum;
                trbElapsedTimePerCycle.Value = trbElapsedTimePerCycle.Maximum;
                txtElapsedTimePerCycle.Text = trbElapsedTimePerCycle.Maximum.ToString();
            }
        }
    }
}
