using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HBS_Shared;
using System.Windows.Forms.DataVisualization.Charting;

namespace HBS
{
    public partial class ColdBlastUsrCtrl : UserControl
    {
        #region Variables
        public bool IsFinish { get; set; }
        public string Column0Name { get; set; }
        public string Column1Name { get; set; }
        public string Column2Name { get; set; }
        public string Column3Name { get; set; }

        Point? prevPosition = null;

        ToolTip tooltip = new ToolTip();

        private List<CGas> Gases = new List<CGas>();

        #endregion


        #region Functions
        public ColdBlastUsrCtrl()
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

        public void SetChart2()
        {
            List<double> time_Air = GetData(0, dgvO2FlowOperatingConditions);
            List<double> flowRate_Air = GetData(1, dgvO2FlowOperatingConditions);
            List<double> temperature_Air = GetData(2, dgvO2FlowOperatingConditions);
            List<double> pressure_Air = GetData(3, dgvO2FlowOperatingConditions);

            List<double> time_O2 = GetData(0, dgvAirFlowOperatingConditions);
            List<double> flowRate_O2 = GetData(1, dgvAirFlowOperatingConditions);
            List<double> temperature_O2 = GetData(2, dgvAirFlowOperatingConditions);
            List<double> pressure_O2 = GetData(3, dgvAirFlowOperatingConditions);


            SetChart2(chtFlowRate, "Air Flow Rate", "O2 FlowRate", time_Air, flowRate_Air, time_O2, flowRate_O2, Color.Red, Color.Blue);
            SetChart2(chtTemperature, "Air Temperature", "O2 Temperature", time_Air, temperature_Air, time_O2, temperature_O2, Color.Red, Color.Blue);
            SetChart2(chtPressure, "Air Pressure", "O2 Pressure", time_Air, pressure_Air, time_O2, pressure_O2, Color.Red, Color.Blue);

            //chtFlowRate.ChartAreas[0].AxisX.Minimum = time_Air[3];
            //chtFlowRate.ChartAreas[0].AxisX.Maximum = time_Air[time_Air.Count-3];

            //chtPressure.ChartAreas[0].AxisX.Minimum = time_Air[3];
            //chtPressure.ChartAreas[0].AxisX.Maximum = time_Air[time_Air.Count - 3];
        }

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

        private void SetChart2(System.Windows.Forms.DataVisualization.Charting.Chart chart, string name1, string name2,
            List<double> x1, List<double> y1, List<double> x2, List<double> y2, Color color1, Color color2)
        {
            chart.Series.Clear();

            chart.ChartAreas[0].AxisX.IsMarginVisible = false;

            chart.Series.Add(name1);
            chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart.Series[0].Color = color1;
            chart.Series[0].BorderWidth = 2;
            chart.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            chart.Series[0].MarkerColor = color1;
            chart.Series[0].MarkerSize = 5;

            chart.Series.Add(name2);
            chart.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart.Series[1].Color = color2;
            chart.Series[1].BorderWidth = 2;
            chart.Series[1].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            chart.Series[1].MarkerColor = color2;
            chart.Series[1].MarkerSize = 5;

            int min1 = Math.Min(x1.Count, y1.Count);
            
            for (int i = 0; i < min1; i++)
                chart.Series[0].Points.AddXY(x1[i], y1[i]);

            int min2 = Math.Min(x2.Count, y2.Count);
            
            for (int i = 0; i < min2; i++)
                chart.Series[1].Points.AddXY(x2[i], y2[i]);
        }

        private void dgvColdBlast_Resize(object sender, EventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            DataGridViewMethods.ResizeGasComposition(dataGridView);
        }
        /// <summary>
        /// Functional method for reading csv file data.
        /// </summary>
        /// <param name="dataGridView"></param>
        public void ReadCsvFile(DataGridView dataGridView, OpenFileDialog openFileDialog)
        {
            ST_UD ud = ST_UD.GetInstance();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<string> contents = File.ReadAllLines(openFileDialog1.FileName).ToList();
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

                SetChart2();
            }
        }

        /// <summary>
        /// Functional method for writing datagridview values to csv file data.
        /// </summary>
        public void WriteCsvFile(DataGridView dataGridView, SaveFileDialog saveFileDialog)
        {
            List<string> contents = new List<string>();

            string headText = string.Empty;
            for (int j = 0; j < dataGridView.ColumnCount; j++)
                if (j == dataGridView.ColumnCount - 1)
                    headText += dataGridView.Columns[j].HeaderText;
                else
                    headText += dataGridView.Columns[j].HeaderText + ",";

            contents.Add(headText);

            for (int i = 0; i < dataGridView.RowCount - 1; i++)
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

        #endregion


        #region Event Functions
        private void dgvAirFlowOperatingConditions_Resize(object sender, EventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            DataGridViewMethods.ResizeAirFlowOperatingCondition(dataGridView);
        }

        private void dgvO2FlowOperatingConditions_Resize(object sender, EventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            DataGridViewMethods.ResizeO2FlowOperatingCondition(dataGridView);
        }
        
        private void ColdBlastUsrCtrl_Load(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            CGas Air = ud.ColdBlast.Air;

            CGas o2 = ud.ColdBlast.O2;

            DataGridView dataGridView1 = dgvColdBlast;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;


            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                CGas.Composition comp = DataGridViewMethods.GetCompositionOnDataGridView(dataGridView1, i);
                dataGridView1[1, i].Value = CGas.MolarMass[comp].ToString("#0.00");
                dataGridView1[2, i].Value = (Air.MoleFraction[comp] * 100.0).ToString("#0.00");
                dataGridView1[3, i].Value = (o2.MoleFraction[comp] * 100.0).ToString("#0.00");
            }

            DataGridView dataGridView2 = dgvAirFlowOperatingConditions;

            for (int i = 3; i < ud.ColdBlast.AirFlowOperatingConditions.Count-2; i++)
            {
                CTime time = ud.ColdBlast.AirFlowOperatingConditions[i].Time;
                CFlowRate flowRate = ud.ColdBlast.AirFlowOperatingConditions[i].FlowRate;
                CTemperature temperature = ud.ColdBlast.AirFlowOperatingConditions[i].Temperature;
                CPressure pressure = ud.ColdBlast.AirFlowOperatingConditions[i].Pressure;
                dataGridView2.Rows.Add(new object[] { time[CTime.Unit.min], flowRate[CFlowRate.Unit.CMH], temperature[CTemperature.Unit.C], pressure[CPressure.Unit.bar] });
            }

            DataGridView dataGridView3 = dgvO2FlowOperatingConditions;
            for (int i = 3; i < ud.ColdBlast.O2FlowOperatingConditions.Count-2; i++)
            {
                CTime time = ud.ColdBlast.O2FlowOperatingConditions[i].Time;
                CFlowRate flowRate = ud.ColdBlast.O2FlowOperatingConditions[i].FlowRate;
                CTemperature temperature = ud.ColdBlast.O2FlowOperatingConditions[i].Temperature;
                CPressure pressure = ud.ColdBlast.O2FlowOperatingConditions[i].Pressure;
                dataGridView3.Rows.Add(new object[] { time[CTime.Unit.min], flowRate[CFlowRate.Unit.CMH], temperature[CTemperature.Unit.C], pressure[CPressure.Unit.bar] });
            }

            SetChart2();

            CTime ReliefTime = ud.ColdBlast.ReliefTime;
            CPressure EqualizingPressureAir = ud.ColdBlast.Air_EqualizingPressure;
            CPressure ReliefPresssureAir = ud.ColdBlast.Air_ReliefPressure;

            CTime EqualizingTime = ud.ColdBlast.EqualizingTime;
            CPressure EqualizingPressureO2 =ud.ColdBlast.O2_EqualizingPressure;
            CPressure ReliefPresssureO2 = ud.ColdBlast.O2_ReliefPressure;


            txtAirPressureRelief.Text = "" + ReliefPresssureAir[CPressure.Unit.bar];
            txtAirPressureEqualizing.Text = "" + EqualizingPressureAir[CPressure.Unit.bar];
            txtTimeRelief.Text = "" + ReliefTime[CTime.Unit.min];


            txtO2PressureRelief.Text = "" + ReliefPresssureO2[CPressure.Unit.bar];
            txtO2PressureEqualizing.Text = "" + EqualizingPressureO2[CPressure.Unit.bar];
            txtTimeEqualizing.Text = "" + EqualizingTime[CTime.Unit.min];
        }

        private void btnOpenFileAir_Click(object sender, EventArgs e)
        {
            ReadCsvFile(dgvColdBlast, openFileDialog1);
        }

        private void btnSaveFileAir_Click(object sender, EventArgs e)
        {
            WriteCsvFile(dgvColdBlast, saveFileDialog1);
        }

        private void btnOpenFileO2_Click(object sender, EventArgs e)
        {
            ReadCsvFile(dgvColdBlast, openFileDialog2);
        }

        private void SaveFileO2_Click(object sender, EventArgs e)
        {
            WriteCsvFile(dgvColdBlast, saveFileDialog2);
        }

        public void SetDataFromFile(string FilePath)
        {
            ST_UD ud = ST_UD.GetInstance();

            ud.ColdBlast = new ST_UD.ColdBlastDataType(FilePath);
            SetDataFromUd();
        }

        public void SetDataFromUd()
        {
            ST_UD ud = ST_UD.GetInstance();

            CGas Air = ud.ColdBlast.Air;
            CGas o2 = ud.ColdBlast.O2;

            this.dgvAirFlowOperatingConditions.DataSource = null;
            this.dgvAirFlowOperatingConditions.Rows.Clear();
            this.dgvO2FlowOperatingConditions.DataSource = null;
            this.dgvO2FlowOperatingConditions.Rows.Clear();

            DataGridView dataGridView1 = dgvColdBlast;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;


            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                CGas.Composition comp = DataGridViewMethods.GetCompositionOnDataGridView(dataGridView1, i);
                dataGridView1[1, i].Value = CGas.MolarMass[comp].ToString("#0.00");
                dataGridView1[2, i].Value = (Air.MoleFraction[comp] * 100.0).ToString("#0.00");
                dataGridView1[3, i].Value = (o2.MoleFraction[comp] * 100.0).ToString("#0.00");
            }

            DataGridView dataGridView2 = dgvAirFlowOperatingConditions;

            for (int i = 3; i < ud.ColdBlast.AirFlowOperatingConditions.Count - 2; i++)
            {
                CTime time = ud.ColdBlast.AirFlowOperatingConditions[i].Time;
                CFlowRate flowRate = ud.ColdBlast.AirFlowOperatingConditions[i].FlowRate;
                CTemperature temperature = ud.ColdBlast.AirFlowOperatingConditions[i].Temperature;
                CPressure pressure = ud.ColdBlast.AirFlowOperatingConditions[i].Pressure;
                dataGridView2.Rows.Add(new object[] { time[CTime.Unit.min], flowRate[CFlowRate.Unit.CMH], temperature[CTemperature.Unit.C], pressure[CPressure.Unit.bar] });
            }

            DataGridView dataGridView3 = dgvO2FlowOperatingConditions;
            for (int i = 3; i < ud.ColdBlast.O2FlowOperatingConditions.Count - 2; i++)
            {
                CTime time = ud.ColdBlast.O2FlowOperatingConditions[i].Time;
                CFlowRate flowRate = ud.ColdBlast.O2FlowOperatingConditions[i].FlowRate;
                CTemperature temperature = ud.ColdBlast.O2FlowOperatingConditions[i].Temperature;
                CPressure pressure = ud.ColdBlast.O2FlowOperatingConditions[i].Pressure;
                dataGridView3.Rows.Add(new object[] { time[CTime.Unit.min], flowRate[CFlowRate.Unit.CMH], temperature[CTemperature.Unit.C], pressure[CPressure.Unit.bar] });
            }

            SetChart2();

            CTime ReliefTime = ud.ColdBlast.ReliefTime;
            CPressure EqualizingPressureAir = ud.ColdBlast.Air_EqualizingPressure;
            CPressure ReliefPresssureAir = ud.ColdBlast.Air_ReliefPressure;

            CTime EqualizingTime = ud.ColdBlast.EqualizingTime;
            CPressure EqualizingPressureO2 = ud.ColdBlast.O2_EqualizingPressure;
            CPressure ReliefPresssureO2 = ud.ColdBlast.O2_ReliefPressure;


            txtAirPressureRelief.Text = "" + ReliefPresssureAir[CPressure.Unit.bar];
            txtAirPressureEqualizing.Text = "" + EqualizingPressureAir[CPressure.Unit.bar];
            txtTimeRelief.Text = "" + ReliefTime[CTime.Unit.min];


            txtO2PressureRelief.Text = "" + ReliefPresssureO2[CPressure.Unit.bar];
            txtO2PressureEqualizing.Text = "" + EqualizingPressureO2[CPressure.Unit.bar];
            txtTimeEqualizing.Text = "" + EqualizingTime[CTime.Unit.min];
        }

        private void btnLoadDefault_Click(object sender, EventArgs e)
        {
            SetDataFromFile(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);
        }
        #endregion


        #region updateUDdata
        private void UpdateUDGasData(DataGridView dataGridView)
        {
            ST_UD ud = ST_UD.GetInstance();

            int iCol = dataGridView.CurrentCell.ColumnIndex;
            List<double> value = GetData(iCol, dataGridView);

            if (iCol == 2)
                ud.ColdBlast.Air.MoleFraction = new CGas.Fraction(value);
            else if (iCol == 3)
                ud.ColdBlast.O2.MoleFraction = new CGas.Fraction(value);
            else
                return;
        }

        private void UpdateUDAirOperatingCondition(DataGridView dataGridView)
        {
            ST_UD ud = ST_UD.GetInstance();
            int iCol = dataGridView.CurrentCell.ColumnIndex;
            List<double> value = GetData(iCol, dataGridView);

            if (iCol == 0)
                ud.ColdBlast.Air_Time = value;
            else if (iCol == 1)
                ud.ColdBlast.Air_FlowRate = value;
            else if (iCol == 2)
                ud.ColdBlast.Air_Temperature = value;
            else if (iCol == 3)
                ud.ColdBlast.Air_Pressure = value;
            else
                return;
        }

        private void UpdateUDO2OperatingCondition(DataGridView dataGridView)
        {
            ST_UD ud = ST_UD.GetInstance();
            int iCol = dataGridView.CurrentCell.ColumnIndex;
            List<double> value = GetData(iCol, dataGridView);

            if (iCol == 0)
                ud.ColdBlast.O2_Time = value;
            else if (iCol == 1)
                ud.ColdBlast.O2_FlowRate = value;
            else if (iCol == 2)
                ud.ColdBlast.O2_Temperature = value;
            else if (iCol == 3)
                ud.ColdBlast.O2_Pressure = value;
            else
                return;
        }
        #endregion


        #region Chart ToolTip

        private void chtFlowRate_MouseMove(object sender, MouseEventArgs e)
        {
            ShowToolTipAtPoint(sender, e, chtFlowRate, "0.00");
        }

        private void chtTemperature_MouseMove(object sender, MouseEventArgs e)
        {
            ShowToolTipAtPoint(sender, e, chtTemperature, "0.00");
        }

        private void chtPressure_MouseMove(object sender, MouseEventArgs e)
        {
            ShowToolTipAtPoint(sender, e, chtPressure, "0.00");
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

        #endregion


        #region Validating/Validated functions
        private void dgvAirFlowOperatingConditions_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
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

        private void dgvO2FlowOperatingConditions_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
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

        private void dgvColdBlast_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
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

        private void txtTimeRelief_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if (!CValidityCheck.IsPositiveNumber(d, true, e))
                return;
        }

        private void txtAirPressureRelief_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;
        }

        private void txtO2PressureRelief_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;
        }
     
        private void txtTimeEqualizing_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if (!CValidityCheck.IsPositiveNumber(d, true, e))
                return;
        }

        private void txtAirPressureEqualizing_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;
        }

        private void txtO2PressureEqualizing_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;
        }

        private void dgvAirFlowOperatingConditions_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            SetChart2();
            UpdateUDAirOperatingCondition(dgvAirFlowOperatingConditions);
        }

        private void dgvO2FlowOperatingConditions_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            SetChart2();
            UpdateUDO2OperatingCondition(dgvO2FlowOperatingConditions);
        }

        private void dgvColdBlast_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            UpdateUDGasData(dgvColdBlast);
        }

        private void txtTimeRelief_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            CTime timeRelief = int.Parse(txtTimeRelief.Text);
            ud.ColdBlast.ReliefTime = timeRelief[CTime.Unit.s];
        }

        private void txtAirPressureRelief_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            CPressure AirReliefPressure = int.Parse(txtAirPressureRelief.Text);
            ud.ColdBlast.Air_ReliefPressure = AirReliefPressure[CPressure.Unit.Pa];
        }

        private void txtO2PressureRelief_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            CPressure O2PressureRelief = int.Parse(txtO2PressureRelief.Text);
            ud.ColdBlast.O2_ReliefPressure = O2PressureRelief[CPressure.Unit.Pa];
        }

        private void txtTimeEqualizing_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            CTime timeEqualizing = int.Parse(txtTimeEqualizing.Text);
            ud.ColdBlast.EqualizingTime= timeEqualizing[CTime.Unit.s];
        }

        private void txtAirPressureEqualizing_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            CPressure AirEqualizingPressure = int.Parse(txtAirPressureEqualizing.Text);
            ud.ColdBlast.Air_EqualizingPressure = AirEqualizingPressure[CPressure.Unit.Pa];
        }

        private void txtO2PressureEqualizing_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            CPressure O2EqualizingPressure = int.Parse(txtO2PressureEqualizing.Text);
            ud.ColdBlast.O2_EqualizingPressure = O2EqualizingPressure[CPressure.Unit.Pa];
        }
    }
    #endregion
}
