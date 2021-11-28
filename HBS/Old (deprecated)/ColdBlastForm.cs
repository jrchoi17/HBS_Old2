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
    public partial class ColdBlastForm : Form
    {
        public bool IsFinish { get; set; }
        public CGas diOxide { get; set; }
        public CGas DryAir { get; set; }
        public ColdBlastForm()
        {
            InitializeComponent();

            CGas.Fraction MolarMass = CGas.MolarMass;
            foreach (CGas.Composition composition in CGas.GetCompositionList())
            {
                object[] rows = new object[] { composition, MolarMass[composition].ToString("#00.0"), null };
                dgvColdBlast.Rows.Add(rows);
            }
            IsFinish = false;
        }

        public void SetAlldataFromUd2(CGas o2, CGas dryAir)
        {
           diOxide = o2;
           DryAir = dryAir;
        }

        public void SetChart()
        {
            List<double> air_time = GetData(dgvAirFlowOperatingConditions, 0);
            List<double> air_flowRate = GetData(dgvAirFlowOperatingConditions, 1);
            List<double> air_temperature = GetData(dgvAirFlowOperatingConditions, 2);
            List<double> air_pressure = GetData(dgvAirFlowOperatingConditions, 3);

            List<double> o2_time = GetData(dgvO2FlowOperatingConditions, 0);
            List<double> o2_flowRate = GetData(dgvO2FlowOperatingConditions, 1);
            List<double> o2_temperature = GetData(dgvO2FlowOperatingConditions, 2);
            List<double> o2_pressure = GetData(dgvO2FlowOperatingConditions, 3);

            SetChart(chtFlowRate, "Air", air_time, air_flowRate, Color.Red,
                "O2", o2_time, o2_flowRate, Color.Blue);
            SetChart(chtTemperature, "Air", air_time, air_temperature, Color.Red,
                "O2", o2_time, o2_temperature, Color.Blue);
            SetChart(chtPressure, "Air", air_time, air_pressure, Color.Red,
                "O2", o2_time, o2_pressure, Color.Blue);
        }

        private List<double> GetData(DataGridView dataGridView, int iColumn)
        {
            List<double> value = new List<double>();

            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                object obj = dataGridView[iColumn, i].Value;
                if (obj != null)
                {
                    double d = 0.0;
                    if (double.TryParse(dataGridView[iColumn, i].Value.ToString(), out d))
                        value.Add(d);
                    else
                        break;
                }
            }

            return value;
        }

        private void SetChart(System.Windows.Forms.DataVisualization.Charting.Chart chart, 
            string name1, List<double> x1, List<double> y1, Color color1,
            string name2, List<double> x2, List<double> y2, Color color2)
        {
            chart.Series.Clear();
            chart.Legends.Clear();

            chart.Legends.Add("legend1");
            chart.Legends[0].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            chart.Legends[0].Alignment = StringAlignment.Center;

            chart.Series.Add(name1);
            chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart.Series[0].Color = color1;
            chart.Series[0].BorderWidth = 3;
            chart.Series[0].Legend = "legend1";

            int min1 = Math.Min(x1.Count, y1.Count);

            for (int i = 0; i < min1; i++)
                chart.Series[0].Points.AddXY(x1[i], y1[i]);

            chart.Series.Add(name2);
            chart.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart.Series[1].Color = color2;
            chart.Series[1].BorderWidth = 3;
            chart.Series[1].Legend = "legend1";

            int min2 = Math.Min(x2.Count, y2.Count);

            for (int i = 0; i < min2; i++)
                chart.Series[1].Points.AddXY(x2[i], y2[i]);
        }

        
        private void SetNormalizing()
        {
            if (diOxide != null)
            {
                diOxide.MoleFraction.Normalize();   
            }
             
            if (DryAir!= null)
            {
                DryAir.MoleFraction.Normalize();
            }
            SetChart();
        }
        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            double d = 0.0;

            if (e.FormattedValue.ToString() == string.Empty)
            {
                dataGridView[e.ColumnIndex, e.RowIndex].Value = null;
                return;
            }

            if (!double.TryParse(e.FormattedValue.ToString(), out d))
            {
                MessageBox.Show(HBS_Shared.Properties.Settings.Default.MSG_INPUT_NUMBER, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                e.Cancel = true;
                return;
            }

            if(e.ColumnIndex==0)
            {
                if (!CValidityCheck.IsPositiveNumber(d, true, e))
                    return;
            }

            if(e.ColumnIndex == 1)
            {
                if (!CValidityCheck.IsPositiveNumber(d, false, e))
                    return;
                else if (!CValidityCheck.IsInRangeFlowRate(e, 0, 1.0e7, d))
                    return;
            }

            if(e.ColumnIndex == 2)
            {
                if (!CValidityCheck.IsInRangeTemp(e, -273.0, 2000.0, d))
                    return;
            }

            if (e.ColumnIndex == 3)
            {
                if(!CValidityCheck.IsInRangePressure(e, -10332.0, 10332.0 * 50.0, d))
                    return;
            }

        }

        private void dgv_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            SetChart();
        }

        private void btnNormalizing_Click(object sender, EventArgs e)
        {
           SetNormalizing();
        }
    }
}
