using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using HBS_Shared;

namespace HBS
{
    public partial class DoughnutCharUsrCtrl : UserControl
    {
        public double ThresholdValue { get; set; }

        private bool _isInit = false;
        public CGas.Fraction Fraction { get; set; }

        public DoughnutCharUsrCtrl()
        {
            InitializeComponent();
        }
         public void SetChart(string title, CGas.Fraction fraction)
        {
            chtGas.Series.Clear();

            chtGas.Series.Add(title);
            chtGas.Series[0].ChartType = SeriesChartType.Doughnut;
            chtGas.Titles[0].Text = title;

            CGas.Fraction aboveFraction = fraction.GetDescending().GetAboveThresholdValue(0.05);

            foreach (KeyValuePair<CGas.Composition, double> entry in aboveFraction)
            {
                if (entry.Value > 0)
                {
                    DataPoint dataPoint = new DataPoint();
                    dataPoint.YValues = new double[] { entry.Value };
                    dataPoint.Label = entry.Key.ToString() + "(" + entry.Value.ToString("0.0%") + ")";
                    
                    chtGas.Series[0].Points.Add(dataPoint);
                }
            }
            // Sums all the remaining values

            CGas.Fraction belowFraction = fraction.GetDescending().GetBelowThresholdValue(ThresholdValue);
            DataPoint dataPoint2 = new DataPoint();
            double sum = 0.0;

            foreach (KeyValuePair<CGas.Composition, double> entry in belowFraction)
            {
                if (entry.Value > 0)
                {
                    dataPoint2.YValues = new double[] { entry.Value };
                }
            }
            for (int i = 0; i < dataPoint2.YValues.Length; i++)
            {
                sum = sum + dataPoint2.YValues[i];
                if (i == (dataPoint2.YValues.Length) - 1 && sum != 0)
                {
                    chtGas.Series[0].Points.Add(sum);
                    chtGas.Series[0].Points[chtGas.Series[0].Points.Count - 1].Label = "etc." + "(" + sum.ToString("0.00%") +")";
                }
            }
            chtGas.Series[0].IsValueShownAsLabel = true;          
        }

        public void SetListView()
        {
            lvGas.Items.Clear();

            CGas.Fraction belowFraction = Fraction.GetDescending().GetBelowThresholdValue(ThresholdValue);
            CGas.Composition[] keys = new CGas.Composition[belowFraction.Count];
            double[] values = new double[belowFraction.Count];

            belowFraction.Keys.CopyTo(keys, 0);
            belowFraction.Values.CopyTo(values, 0);

            for (int i = 0; i < belowFraction.Count; i++)
            {
                lvGas.Items.Add(keys[i].ToString());
                lvGas.Items[i].SubItems.Add((values[i] * 100).ToString("#0.00"));
            }
        }

        private void DoughnutCharUsrCtrl_SizeChanged(object sender, System.EventArgs e)
        {
            int width = (int)(lvGas.Width / 2.0) - 2;
            lvGas.Columns[0].Width = width - 40;
            lvGas.Columns[1].Width = width;

            if (_isInit)
                SetListView();
        }

        private void DoughnutCharUsrCtrl_Load(object sender, System.EventArgs e)
        {
            _isInit = true;
        }
    }
}
