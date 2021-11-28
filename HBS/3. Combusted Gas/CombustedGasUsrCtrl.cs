using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HBS_Shared;
using System.Windows.Forms.DataVisualization.Charting;

namespace HBS
{
    public partial class CombustedGasUsrCtrl : UserControl
    {
        Point? prevPosition = null;

        ToolTip tooltip = new ToolTip();

        #region Variables
        public bool IsFinish { get; set; }

        public enum FractionType { Mole = 0, Mass }
        public string Column0Name { get; set; }
        public string Column1Name { get; set; }
        public string Column2Name { get; set; }
        public string Column3Name { get; set; }

        private List<CGas> Gases = new List<CGas>();

        #endregion



        #region Constructor method
        /// <summary>
        /// Initializes dgvCombustedGas with composition and molar mass values.
        /// </summary>
        public CombustedGasUsrCtrl()
        {
            InitializeComponent();

            CGas.Fraction MolarMass = CGas.MolarMass;
            
            foreach (CGas.Composition composition in CGas.GetCompositionList())
            {
                object[] rows = new object[] { composition, MolarMass[composition].ToString("#00.0"), null };
                dgvCombustedGas.Rows.Add(rows);
            }
            IsFinish = false;
        }
        #endregion



        #region Functional method
        /// <summary>
        /// Functional method for setting chart.
        /// </summary>
        public void SetChart()
        {
            List<double> time = GetData(0, dgvFlowOperatingConditions);
            List<double> flowRate = GetData(1, dgvFlowOperatingConditions);
            List<double> temperature = GetData(2, dgvFlowOperatingConditions);
            List<double> pressure = GetData(3, dgvFlowOperatingConditions);

            SetChart(chtFlowRate, "Flow Rate", time, flowRate, Color.Black);
            SetChart(chtTemperature, "Temperature", time, temperature, Color.Red);
            SetChart(chtPressure, "Pressure", time, pressure, Color.Blue);
        }

        /// <summary>
        /// Functional method for reading csv file data.
        /// </summary>
        /// <param name="dataGridView"></param>
        public void ReadCsvFile(DataGridView dataGridView)
        { 
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> contents = File.ReadAllLines(openFileDialog.FileName).ToList();
                List<string> headText = contents[0].Split(',').ToList();

                if (contents.Count > dataGridView.RowCount + 1 || headText.Count > dataGridView.ColumnCount)
                {
                    MessageBox.Show(CException.InvalidColumnRow, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Column0Name = headText[0];
                Column1Name = headText[1];
                Column2Name = headText[2];
                Column3Name = headText[3];
                   
                for (int i = 0; i < contents.Count - 1; i++)
                {
                    List<string> columns = contents[i + 1].Split(',').ToList();

                    for (int j = 0; j < columns.Count; j++)
                        dataGridView[j, i].Value = columns[j];
                }

                SetChart();
            }
        }
       
        /// <summary>
        /// Functional method for writing datagridview values to csv file data.
        /// </summary>
        public void WriteCsvFile(DataGridView dataGridView)
        {
            List<string> contents = new List<string>();

            string headText = string.Empty;
            for (int j = 0; j < dataGridView.ColumnCount; j++)
                if (j == dataGridView.ColumnCount - 1)
                    headText += dataGridView.Columns[j].HeaderText;
                else
                    headText += dataGridView.Columns[j].HeaderText + ",";

            contents.Add(headText);

            for (int i = 0; i < dataGridView.RowCount-1; i++)
            {
                string cell = string.Empty;
                for (int j = 0; j < dataGridView.ColumnCount; j++)
                    if (j == dataGridView.ColumnCount - 1)
                        cell += dataGridView[j, i].Value.ToString();
                    else
                        cell += dataGridView[j, i].Value.ToString() + ",";
                contents.Add(cell);
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                File.WriteAllLines(saveFileDialog.FileName, contents);
        }

        /// <summary>
        /// Functional method for getting values from datagridview columns.
        /// </summary>
        /// <param name="iColumn">Column index.</param>
        /// <returns>List of values.</returns>
        private List<double> GetData(int iColumn, DataGridView dataGridView)
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

        /// <summary>
        /// Functional method for setting chart components.
        /// </summary>
        /// <param name="chart">Chart.</param>
        /// <param name="name">Title.</param>
        /// <param name="x">X-data.</param>
        /// <param name="y">Y-data.</param>
        /// <param name="color">Color of data.</param>
        private void SetChart(System.Windows.Forms.DataVisualization.Charting.Chart chart, string name,
            List<double> x, List<double> y, Color color)
        {
            chart.Series.Clear();

            chart.Series.Add(name);
            chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart.Series[0].Color = color;
            chart.Series[0].BorderWidth = 2;
            chart.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            chart.Series[0].MarkerSize = 5;

            int min = Math.Min(x.Count, y.Count);

            for (int i = 0; i < min; i++)
                chart.Series[0].Points.AddXY(x[i], y[i]);
        }

        private List<double> Listmultiply(List<double> list, double mult)
        {
           
            for (int i = 0; i < list.Count; i++)
            {
                list[i] *= mult;
            }

            return list;
        }

        private void UpdateUDGasData(DataGridView dataGridView)
        {
            ST_UD ud = ST_UD.GetInstance();

            int iCol = dataGridView.CurrentCell.ColumnIndex;
            List<double> value = GetData(iCol, dataGridView);
            Listmultiply(value, 0.01);

            if (iCol == 2)
                ud.CombustedGas.CombustedGas.MoleFraction = new CGas.Fraction(value);
            else
                return;
        }
        
        private void UpdateUDFlowOperatingCondition(DataGridView dataGridView)
        {
            ST_UD ud = ST_UD.GetInstance();
            int iCol = dataGridView.CurrentCell.ColumnIndex;
            List<double> value = GetData(iCol, dataGridView);
            
            if (iCol == 0)
                ud.CombustedGas.Time= value;
            else if (iCol == 1)
                ud.CombustedGas.FlowRate = value;
            else if (iCol == 2)
                ud.CombustedGas.Temperature = value;
            else if (iCol == 3)
                ud.CombustedGas.Pressure = value;
            else
                return;
        }
        #endregion



        #region Event method
        /// <summary>
        /// Event method for validating cell values of dgvFlowOperatingConditions.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvFlowOperatingConditions_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
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

            if (e.ColumnIndex == 0)
            {
                if (!CValidityCheck.IsPositiveNumber(d, true, e))
                    return;
            }

            if (e.ColumnIndex == 1)
            {
                if (!CValidityCheck.IsPositiveNumber(d, false, e))
                    return;
                else if (!CValidityCheck.IsInRangeFlowRate(e, 0, 1.0e7, d))
                    return;
            }

            if (e.ColumnIndex == 2)
            {
                if (!CValidityCheck.IsInRangeTemp(e, -273.0, 2000.0, d))
                    return;
            }

            if (e.ColumnIndex == 3)
            {
                if (!CValidityCheck.IsInRangePressure(e, -10332.0, 10332.0 * 50.0, d))
                    return;
            }
        }

        /// <summary>
        /// Event method for setting chart after validation.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvFlowOperatingConditions_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            SetChart();
            UpdateUDFlowOperatingCondition(dgvFlowOperatingConditions);
        }

        /// <summary>
        /// Event method for resizing dgvCombustedGas cell size.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvCombustedGas_Resize(object sender, EventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            DataGridViewMethods.ResizeGasComposition(dataGridView);
        }

        /// <summary>
        /// Event method for resizing dgvFlowOperatingConditions cell size.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvFlowOperatingConditions_Resize(object sender, EventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            DataGridViewMethods.ResizeFlowOperatingCondition(dataGridView);
            UpdateUDGasData(dgvCombustedGas);
        }

        public void SetDataFromFile(string filePath)
        {
            ST_UD ud = ST_UD.GetInstance();
            ud.CombustedGas = new ST_UD.CombustedGasDataType(filePath);
            CGas CombustedGas = ud.CombustedGas.CombustedGas;

            this.dgvFlowOperatingConditions.DataSource = null;
            this.dgvFlowOperatingConditions.Rows.Clear();

            DataGridView dataGridView1 = dgvCombustedGas;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                CGas.Composition comp = DataGridViewMethods.GetCompositionOnDataGridView(dataGridView1, i);
                dataGridView1[1, i].Value = CGas.MolarMass[comp].ToString("#0.00");
                dataGridView1[2, i].Value = (CombustedGas.MoleFraction[comp] * 100.0).ToString("#0.00");
            }

            DataGridView dataGridView2 = dgvFlowOperatingConditions;
            for (int i = 0; i < ud.CombustedGas.FlowOperatingCondition.Count; i++)
            {
                CTime time = ud.CombustedGas.FlowOperatingCondition[i].Time;
                CFlowRate flowRate = ud.CombustedGas.FlowOperatingCondition[i].FlowRate;
                CTemperature temperature = ud.CombustedGas.FlowOperatingCondition[i].Temperature;
                CPressure pressure = ud.CombustedGas.FlowOperatingCondition[i].Pressure;
                dataGridView2.Rows.Add(new object[] { time[CTime.Unit.min], flowRate[CFlowRate.Unit.CMH], temperature[CTemperature.Unit.C], pressure[CPressure.Unit.mmAq] });
            }

            SetChart();
        }

        /// <summary>
        /// Event method for loading default value of datagridviews.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void btnLoadDefault_Click(object sender, EventArgs e)
        {
            SetDataFromFile(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);
        }

        /// <summary>
        /// Event method for setting values to datagridview cells and charts.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void CombustedGasUsrCtrl_Load(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();
            CGas CombustedGas = ud.CombustedGas.CombustedGas;
            
            DataGridView dataGridView1 = dgvCombustedGas;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                CGas.Composition comp = DataGridViewMethods.GetCompositionOnDataGridView(dataGridView1, i);
                dataGridView1[1, i].Value = CGas.MolarMass[comp].ToString("#0.00");
                dataGridView1[2, i].Value = (CombustedGas.MoleFraction[comp] * 100.0).ToString("#0.00");
            }

            DataGridView dataGridView2 = dgvFlowOperatingConditions;
            for (int i = 0; i < ud.CombustedGas.FlowOperatingCondition.Count; i++)
            {
                CTime time = ud.CombustedGas.FlowOperatingCondition[i].Time;
                CFlowRate flowRate = ud.CombustedGas.FlowOperatingCondition[i].FlowRate;
                CTemperature temperature = ud.CombustedGas.FlowOperatingCondition[i].Temperature;
                CPressure pressure = ud.CombustedGas.FlowOperatingCondition[i].Pressure;
                dataGridView2.Rows.Add(new object[] { time[CTime.Unit.min], flowRate[CFlowRate.Unit.CMH], temperature[CTemperature.Unit.C], pressure[CPressure.Unit.mmAq] });
            }

            SetChart();
        }

        /// <summary>
        /// Event method for importing csv file data to dgvFlowOperatingConditions.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            ReadCsvFile(dgvFlowOperatingConditions);
        }

        /// <summary>
        /// Event method for exporting csv file data of dgvFlowOperatingConditions.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            WriteCsvFile(dgvFlowOperatingConditions);
        }
        #endregion

        private void dgvCombustedGas_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            double d = 0.0;

            if (e.ColumnIndex < 2)
                return;

            if (e.FormattedValue.ToString() == string.Empty)
            {
                dataGridView[e.ColumnIndex, e.RowIndex].Value = null;
                return;
            }

            if (!double.TryParse(e.FormattedValue.ToString(), out d))
            {
                MessageBox.Show(CException.NotNumber, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            if (d < 0)
            {
                MessageBox.Show(CException.NotPositiveNumber, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
        }

        private void dgvCombustedGas_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            UpdateUDGasData(dgvCombustedGas);
        }

        private void ShowToolTipAtPoint(object sender, MouseEventArgs e, Chart chart1, string format)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chart1.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);

            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var xVal = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var yVal = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                        // check if the cursor is really close to the point (2 pixels around the point)
                        if (Math.Abs(pos.X - xVal) < 5 && Math.Abs(pos.Y - yVal) < 5)
                        {
                            tooltip.Show("" + chart1.ChartAreas[0].AxisY.Title + " = " + prop.YValues[0].ToString(format) + "\n" + chart1.ChartAreas[0].AxisX.Title +
                            " = " + prop.XValue.ToString("0.0"), chart1, pos.X, pos.Y - 15);
                        }
                    }
                }
            }
        }

        private void chtFlowRate_MouseMove(object sender, MouseEventArgs e)
        {
            ShowToolTipAtPoint(sender, e, chtFlowRate, "0.00");
        }

        private void chtPressure_MouseMove(object sender, MouseEventArgs e)
        {
            ShowToolTipAtPoint(sender, e, chtPressure, "0.00");
        }

        private void chtTemperature_MouseMove(object sender, MouseEventArgs e)
        {
            ShowToolTipAtPoint(sender, e, chtTemperature, "0.00");
        }
        
       private double ListToDouble(List<double> value, int i)
        {
            return value[i];
        }
      
  
        public void SetNormalizing()
        {
            DataGridView dataGridView1 = dgvCombustedGas;

            CGas CombustedGas = new CGas();
            CombustedGas.MoleFraction = new CGas.Fraction(0.0);
            List<double> Gas = GetData(2, dataGridView1);

            foreach (CGas.Composition comp in CGas.GetCompositionList())
            {
                
                double value = ListToDouble(Gas, Convert.ToInt32(comp));

                CombustedGas.MoleFraction[comp] = value;
            }

            CombustedGas.MoleFraction.Normalize();
            CombustedGas.MoleFractionToMassFraction();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                CGas.Composition comp = DataGridViewMethods.GetCompositionOnDataGridView(dataGridView1, i);
                dataGridView1[2, i].Value = (CombustedGas.MoleFraction[comp] * 100.0).ToString("#0.00");
            }
        }
 
        private void Normalizing_Click(object sender, EventArgs e)
        {
            SetNormalizing();
        }
    }
}
