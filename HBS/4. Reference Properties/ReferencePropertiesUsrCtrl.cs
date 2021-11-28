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
    public partial class ReferencePropertiesUsrCtrl : UserControl
    {
        
        public bool IsFinish { get; set; }
        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();


        public ReferencePropertiesUsrCtrl()
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

        private List<double> GetTemperature()
        {
            int Points = 50;
            double Tbeg = 300 + 273.15;
            double Tend = 1300 + 273.15;
            List<double> Temperature = new List<double>();

            for (int i = 0; i <= Points; i++)
            {
                Temperature.Add(Tbeg + ((Tend - Tbeg) / Points) * i);
            }

            return Temperature;
        }
        private List<List<double>> GetDensity()
        {
            List<CGas.Composition> compositions = CGas.GetCompositionList();
            List<double> Temperature = GetTemperature();
            List<List<double>> Density = new List<List<double>>();

            for (int i = 0; i < compositions.Count; i++)
            {
                List<double> species = new List<double>();
                Density.Add(species);
                for (int j = 0; j < Temperature.Count; j++)
                {
                    Density[i].Add(RefProp_Wrapper.GetDensity(compositions[i], Temperature[j], 101325, Application.StartupPath + @"\RefProp\DatabaseFiles"));
                    if (Density[i][j] == -9999990.0)
                    {
                        if (j == 0)
                            Density[i][j] = 0.0;
                        else
                            Density[i][j] = Density[i][j - 1];
                    }
                }
            }

            return Density;
        }

        private List<List<double>> GetSpecificHeat()
        {
            List<CGas.Composition> compositions = CGas.GetCompositionList();
            List<double> Temperature = GetTemperature();
            List<List<double>> SpecificHeat = new List<List<double>>();

            for (int i = 0; i < compositions.Count; i++)
            {
                List<double> species = new List<double>();
                SpecificHeat.Add(species);
                for (int j = 0; j < Temperature.Count; j++)
                {
                    SpecificHeat[i].Add(RefProp_Wrapper.GetSpecificHeat(compositions[i], Temperature[j], 101325, Application.StartupPath + @"\RefProp\DatabaseFiles"));
                    if (SpecificHeat[i][j] == -9999990.0)
                    {
                        if (j == 0)
                            SpecificHeat[i][j] = 0.0;
                        else
                            SpecificHeat[i][j] = SpecificHeat[i][j - 1];
                    }
                }
            }

            return SpecificHeat;
        }

        private List<List<double>> GetThermalConductivity()
        {
            List<CGas.Composition> compositions = CGas.GetCompositionList();
            List<double> Temperature = GetTemperature();
            List<List<double>> ThermalConductivity = new List<List<double>>();

            for (int i = 0; i < compositions.Count; i++)
            {
                List<double> species = new List<double>();
                ThermalConductivity.Add(species);
                for (int j = 0; j < Temperature.Count; j++)
                {
                    ThermalConductivity[i].Add(RefProp_Wrapper.GetThermalConductivity(compositions[i], Temperature[j], 101325, Application.StartupPath + @"\RefProp\DatabaseFiles"));
                    if (ThermalConductivity[i][j] == -9999990.0)
                    {
                        if (j == 0)
                            ThermalConductivity[i][j] = 0.0;
                        else
                            ThermalConductivity[i][j] = ThermalConductivity[i][j-1];
                    }
                }
            }

            return ThermalConductivity;
        }

        private List<List<double>> GetDynamicViscosity()
        {
            List<CGas.Composition> compositions = CGas.GetCompositionList();
            List<double> Temperature = GetTemperature();
            List<List<double>> DynamicViscosity = new List<List<double>>();

            for (int i = 0; i < compositions.Count; i++)
            {
                List<double> species = new List<double>();
                DynamicViscosity.Add(species);
                for (int j = 0; j < Temperature.Count; j++)
                {
                    DynamicViscosity[i].Add(RefProp_Wrapper.GetDynamicViscosity(compositions[i], Temperature[j], 101325, Application.StartupPath + @"\RefProp\DatabaseFiles"));
                    if (DynamicViscosity[i][j] == -9.99999)
                    {
                        if (j == 0)
                            DynamicViscosity[i][j] = 0.0;
                        else
                            DynamicViscosity[i][j] = DynamicViscosity[i][j - 1];
                    }
                }
            }

            return DynamicViscosity;
        }
        
        private List<double> GetTotalDensity(List<List<double>> Density)
        {
            ST_UD ud = ST_UD.GetInstance();
            CGas CombustedGas = ud.CombustedGas.CombustedGas;
            List<double> MoleFraction = new List<double>();
            List<double> TotalDensity = new List<double>();
            List<double> PartialDensity = new List<double>();

            for (int i = 0; i < dgvCombustedGas.RowCount; i++)
            {
                CGas.Composition comp = DataGridViewMethods.GetCompositionOnDataGridView(dgvCombustedGas, i);
                MoleFraction.Add(CombustedGas.MoleFraction[comp]);
            }

            for (int i = 0; i < Density[0].Count; i++)
            {
                PartialDensity.Clear();
                double Sum = 0.0;
                for (int j = 0; j < Density.Count; j++)
                {
                    PartialDensity.Add(Density[j][i] * MoleFraction[j]);
                    Sum += PartialDensity[j];
                }
                TotalDensity.Add(Sum);
            }
            return TotalDensity;
        }

        private List<double> GetTotalSpecificHeat(List<List<double>> SpecificHeat)
        {
            ST_UD ud = ST_UD.GetInstance();
            CGas CombustedGas = ud.CombustedGas.CombustedGas;
            List<double> MassFraction = new List<double>();
            List<double> TotalSpecificHeat = new List<double>();
            List<double> PartialSpecificHeat = new List<double>();

            for (int i = 0; i < dgvCombustedGas.RowCount; i++)
            {
                CGas.Composition comp = DataGridViewMethods.GetCompositionOnDataGridView(dgvCombustedGas, i);
                MassFraction.Add(CombustedGas.MassFraction[comp]);
            }

            for (int i = 0; i < SpecificHeat[0].Count; i++)
            {
                PartialSpecificHeat.Clear();
                double Sum = 0.0;
                for (int j = 0; j < SpecificHeat.Count; j++)
                {
                    PartialSpecificHeat.Add(SpecificHeat[j][i] * MassFraction[j]);
                    Sum += PartialSpecificHeat[j];
                }
                TotalSpecificHeat.Add(Sum);
            }
            return TotalSpecificHeat;
        }

        private List<double> GetTotalThermalConductivity(List<List<double>> ThermalConductivity)
        {
            ST_UD ud = ST_UD.GetInstance();
            CGas CombustedGas = ud.CombustedGas.CombustedGas;
            List<double> MoleFraction = new List<double>();
            List<double> TotalThermalConductivity = new List<double>();
            List<double> PartialThermalConductivity = new List<double>();


            for (int i = 0; i < dgvCombustedGas.RowCount; i++)
            {
                CGas.Composition comp = DataGridViewMethods.GetCompositionOnDataGridView(dgvCombustedGas, i);
                MoleFraction.Add(CombustedGas.MoleFraction[comp]);
            }

            for (int i = 0; i < ThermalConductivity[0].Count; i++)
            {
                PartialThermalConductivity.Clear();
                double Sum = 0.0;
                for (int j = 0; j < ThermalConductivity.Count; j++)
                {
                    PartialThermalConductivity.Add(ThermalConductivity[j][i] * MoleFraction[j]);
                    Sum += PartialThermalConductivity[j];
                }
                TotalThermalConductivity.Add(Sum);
            }
            return TotalThermalConductivity;
        }

        private List<double> GetTotalDynamicViscosity(List<List<double>> DynamicViscosity)
        {
            ST_UD ud = ST_UD.GetInstance();
            CGas CombustedGas = ud.CombustedGas.CombustedGas;
            List<double> MoleFraction = new List<double>();
            List<double> TotalDynamicViscosity = new List<double>();
            List<double> PartialDynamicViscosity = new List<double>();


            for (int i = 0; i < dgvCombustedGas.RowCount; i++)
            {
                CGas.Composition comp = DataGridViewMethods.GetCompositionOnDataGridView(dgvCombustedGas, i);
                MoleFraction.Add(CombustedGas.MoleFraction[comp]);
            }

            for (int i = 0; i < DynamicViscosity[0].Count; i++)
            {
                PartialDynamicViscosity.Clear();
                double Sum = 0.0;
                for (int j = 0; j < DynamicViscosity.Count; j++)
                {
                    PartialDynamicViscosity.Add(DynamicViscosity[j][i] * MoleFraction[j]);
                    Sum += PartialDynamicViscosity[j];
                }
                TotalDynamicViscosity.Add(Sum);
            }
            return TotalDynamicViscosity;
        }

        private double GetTotalDensityMinMax(string value)
        {
            List<double> TotalDensity = GetTotalDensity(GetDensity());

            if (value == "max" || value == "Max")
            {
                if (TotalDensity[TotalDensity.Count - 1] > TotalDensity[0])
                    return TotalDensity[TotalDensity.Count - 1];
                else
                    return TotalDensity[0];
            }
            else if (value == "min" || value == "Min")
            {
                if (TotalDensity[TotalDensity.Count - 1] > TotalDensity[0])
                    return TotalDensity[0];
                else
                    return TotalDensity[TotalDensity.Count - 1];
            }
            else
            {
                return 0;
            }      
        }

        private double GetTotalSpecificHeatMinMax(string value)
        {
            List<double> TotalSpecificHeat = GetTotalSpecificHeat(GetSpecificHeat());

            if (value == "max" || value == "Max")
            {
                if (TotalSpecificHeat[TotalSpecificHeat.Count - 1] > TotalSpecificHeat[0])
                    return TotalSpecificHeat[TotalSpecificHeat.Count - 1];
                else
                    return TotalSpecificHeat[0];
            }
            else if (value == "min" || value == "Min")
            {
                if (TotalSpecificHeat[TotalSpecificHeat.Count - 1] > TotalSpecificHeat[0])
                    return TotalSpecificHeat[0];
                else
                    return TotalSpecificHeat[TotalSpecificHeat.Count - 1];
            }
            else
            {
                return 0;
            }
        }

        private double GetTotalThermalConductivityMinMax(string value)
        {
            List<double> TotalThermalConductivity = GetTotalThermalConductivity(GetThermalConductivity());

            if (value == "max" || value == "Max")
            {
                if (TotalThermalConductivity[TotalThermalConductivity.Count - 1] > TotalThermalConductivity[0])
                    return TotalThermalConductivity[TotalThermalConductivity.Count - 1];
                else
                    return TotalThermalConductivity[0];
            }
            else if (value == "min" || value == "Min")
            {
                if (TotalThermalConductivity[TotalThermalConductivity.Count - 1] > TotalThermalConductivity[0])
                    return TotalThermalConductivity[0];
                else
                    return TotalThermalConductivity[TotalThermalConductivity.Count - 1];
            }
            else
            {
                return 0;
            }
        }

        private double GetTotalDynamicViscosityMinMax(string value)
        {
            List<double> TotalDynamicViscosity = GetTotalDynamicViscosity(GetDynamicViscosity());

            if (value == "max" || value == "Max")
            {
                if (TotalDynamicViscosity[TotalDynamicViscosity.Count - 1] > TotalDynamicViscosity[0])
                    return TotalDynamicViscosity[TotalDynamicViscosity.Count - 1];
                else
                    return TotalDynamicViscosity[0];
            }
            else if (value == "min" || value == "Min")
            {
                if (TotalDynamicViscosity[TotalDynamicViscosity.Count - 1] > TotalDynamicViscosity[0])
                    return TotalDynamicViscosity[0];
                else
                    return TotalDynamicViscosity[TotalDynamicViscosity.Count - 1];
            }
            else
            {
                return 0;
            }
        }

        private void SetChart(System.Windows.Forms.DataVisualization.Charting.Chart chart, string name1, List<double> x1, List<double> y1, Color color1)
        {
            chart.Series.Clear();

            chart.ChartAreas[0].AxisX.IsMarginVisible = false;

            chart.Series.Add(name1);
            chart.Series[0].ChartType = SeriesChartType.Line;
            chart.Series[0].BorderWidth = 2;
            chart.Series[0].Color = color1;
            chart.Series[0].MarkerStyle = MarkerStyle.Circle;
            chart.Series[0].MarkerSize = 5;
            int min1 = Math.Min(x1.Count, y1.Count);

            for (int i = 0; i < min1; i++)
                chart.Series[0].Points.AddXY(x1[i], y1[i]);
        }

        private void LoadSavedGasData()
        {
            ST_UD ud = ST_UD.GetInstance();
            CGas CombustedGas = ud.CombustedGas.CombustedGas;
            List<double> Temperature = GetTemperature();
            List<double> TempCelsius = new List<double>();
            List<double> DynamicViscositymmAq = new List<double>();

            List<List<double>> Density = GetDensity();
            List<List<double>> SpecificHeat = GetSpecificHeat();
            List<List<double>> ThermalConductivity = GetThermalConductivity();
            List<List<double>> DynamicViscosity = GetDynamicViscosity();

            List<double> TotalDensity = GetTotalDensity(Density);
            List<double> TotalSpecificHeat = GetTotalSpecificHeat(SpecificHeat);
            List<double> TotalThermalConductivity = GetTotalThermalConductivity(ThermalConductivity);
            List<double> TotalDynamicViscosity = GetTotalDynamicViscosity(DynamicViscosity);

            for (int i = 0; i < Temperature.Count; i++)
            {
                CTemperature temp = Temperature[i];
                CPressure pressure = TotalDynamicViscosity[i];
                TempCelsius.Add(temp[CTemperature.Unit.C]);
                DynamicViscositymmAq.Add(pressure[CPressure.Unit.mmAq]);
            }

           

            DataGridView dataGridView1 = dgvCombustedGas;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;


            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                CGas.Composition comp = DataGridViewMethods.GetCompositionOnDataGridView(dataGridView1, i);
                dataGridView1[1, i].Value = CGas.MolarMass[comp].ToString("#0.00");
                dataGridView1[2, i].Value = (CombustedGas.MoleFraction[comp] * 100).ToString("#0.00");
                dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dataGridView1.Columns[2].ReadOnly = true;
            
            txtDensity.Text = "" + GetTotalDensityMinMax("max").ToString("0.000");
            txtSpecificHeat.Text = "" + GetTotalSpecificHeatMinMax("min").ToString("0.000");
            txtDynamicViscosity.Text = "" + GetTotalDynamicViscosityMinMax("min").ToString("0.0000000");
            txtThermalConductivity.Text = "" + GetTotalThermalConductivityMinMax("min").ToString("0.0000");

            SetChart(chtDensity, "Density", TempCelsius, TotalDensity, Color.Blue);
            SetChart(chtSpecificHeat, "Specific Heat", TempCelsius, TotalSpecificHeat, Color.Blue);
            SetChart(chtThermalConductivity, "Thermal Conductivitiy", TempCelsius, TotalThermalConductivity, Color.Blue);
            SetChart(chtDynamicViscosity, "Dynamic Viscosity", TempCelsius, TotalDynamicViscosity, Color.Blue);
        }

        private void dgvCombustedGas_Resize(object sender, EventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            DataGridViewMethods.ResizeGasComposition(dataGridView);
        }

        private void RefPropertiesUsrCtrl_Load(object sender, EventArgs e)
        {
            LoadSavedGasData();
        }
        private void btnUpdateChart_Click(object sender, EventArgs e)
        {
            LoadSavedGasData();
        }

        private void RefPropertiesUsrCtrl_Resize(object sender, EventArgs e)
        {
            LoadSavedGasData();
        }

        private void RefPropertiesUsrCtrl_VisibleChanged(object sender, EventArgs e)
        {
            LoadSavedGasData();
        }


        #region Mouse movement

        private void chtDensity_MouseMove(object sender, MouseEventArgs e)
        {
            ShowToolTipAtPoint(sender, e, chtDensity, "0.000");
        }

        private void chtSpecificHeat_MouseMove(object sender, MouseEventArgs e)
        {
            ShowToolTipAtPoint(sender, e, chtSpecificHeat, "0.000");
        }

        private void chtThermalConductivity_MouseMove(object sender, MouseEventArgs e)
        {
            ShowToolTipAtPoint(sender, e, chtThermalConductivity, "0.000");
        }

        private void chtDynamicViscosity_MouseMove(object sender, MouseEventArgs e)
        {
            ShowToolTipAtPoint(sender, e, chtDynamicViscosity, "E3");
        }

        private void ShowToolTip(object sender, MouseEventArgs e, Chart chart1, string format)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chart1.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint); // set ChartElementType.PlottingArea for full area, not only DataPoints
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint) // set ChartElementType.PlottingArea for full area, not only DataPoints
                {
                    var yVal = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);
                    var xVal = result.ChartArea.AxisX.PixelPositionToValue(pos.X);
                    tooltip.Show("" + chart1.ChartAreas[0].AxisY.Title + " = "  + ((double)yVal).ToString(format) + "\n" + chart1.ChartAreas[0].AxisX.Title + 
                        " = " + ((double)xVal).ToString("0.0"), chart1, pos.X, pos.Y - 15);
                }
            }
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

        private void GetPoint(Chart chart1, MouseEventArgs e)
        {
            HitTestResult result = chart1.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                var xVal = result.Series.Points[result.PointIndex].XValue;
                var yVal = result.Series.Points[result.PointIndex].YValues[0];
                result.Series.Points[result.PointIndex].MarkerColor = Color.Red;

                Clipboard.SetText("" + chart1.ChartAreas[0].AxisY.Title + "," + chart1.ChartAreas[0].AxisX.Title  + "\n" + yVal + "," + xVal);
                MessageBox.Show("Values Copied To Clipboard");
            }

        }

        private void chtDensity_MouseDown(object sender, MouseEventArgs e)
        {
            GetPoint(chtDensity, e);
        }

        private void chtSpecificHeat_MouseDown(object sender, MouseEventArgs e)
        {
            GetPoint(chtSpecificHeat, e);
        }

        private void chtThermalConductivity_MouseDown(object sender, MouseEventArgs e)
        {
            GetPoint(chtThermalConductivity, e);
        }

        private void chtDynamicViscosity_MouseDown(object sender, MouseEventArgs e)
        {
            GetPoint(chtDynamicViscosity, e);
        }

        #endregion


        private void txtTemp_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if(!CValidityCheck.IsInRangeTemp(e, 300.0, 1300.0, d))
                return;
        }

        private void txtDensityOutput_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if (!CValidityCheck.IsInRangeValue(e, GetTotalDensityMinMax("min"), GetTotalDensityMinMax("max"), d))
                return;
        }

        private void txtSpecificHeatOutput_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if(!CValidityCheck.IsInRangeValue(e, GetTotalSpecificHeatMinMax("min"), GetTotalSpecificHeatMinMax("max"), d))
                return;
        }

        private void txtThermalConductivityOutput_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if(!CValidityCheck.IsInRangeValue(e, GetTotalThermalConductivityMinMax("min"), GetTotalThermalConductivityMinMax("max"), d))
                return;
        }

        private void txtDynamicViscosity_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if (!CValidityCheck.IsInRangeDynamicViscosity(e, GetTotalDynamicViscosityMinMax("min"), GetTotalDynamicViscosityMinMax("max"), d))
                return;
        }


        private double GetNearest(List<double> list, double value)
        {
            double nearest = list[0];
            double currentdifference = Math.Abs(nearest - value);
       
            for (int i = 1; i < list.Count; i++)
            {
                double diff = Math.Abs(list[i] - value);
                if (diff < currentdifference)
                {
                    currentdifference = diff;
                    nearest = list[i];
                }
            }

            return nearest;
        }

        private double GetNearestTemp(List<double> list, double value)
        {
            double nearest = list[0];
            double newValue = value + 273.15;
            double currentdifference = Math.Abs(nearest - newValue);

            for (int i = 1; i < list.Count; i++)
            {
                double diff = Math.Abs(list[i] - newValue);
                if (diff < currentdifference)
                {
                    currentdifference = diff;
                    nearest = list[i];
                }
            }

            return nearest;
        }





        #region text value to trackbar

        private void txtTempDensity_Validated(object sender, EventArgs e)
        {
            List<double> Temperature = GetTemperature();
            List<double> TotalDensity = GetTotalDensity(GetDensity());

            trackbarTempDensity.Value = Temperature.IndexOf(GetNearestTemp(Temperature, Convert.ToDouble(txtTempDensity.Text)));

            trackbarDensity.Value = trackbarTempDensity.Value;
            txtTempDensity.Text = (Temperature[trackbarDensity.Value]-273.15).ToString("0.0");
            txtDensity.Text = TotalDensity[trackbarDensity.Value].ToString("0.###");
        } 

        private void txtTempSpecificHeat_Validated(object sender, EventArgs e)
        {
            List<double> Temperature = GetTemperature();
            List<double> TotalSpecificHeat = GetTotalSpecificHeat(GetSpecificHeat());

            trackbarTempSpecificHeat.Value = Temperature.IndexOf(GetNearestTemp(Temperature, Convert.ToDouble(txtTempSpecificHeat.Text)));

            trackbarSpecificHeat.Value = trackbarTempSpecificHeat.Value;
            txtSpecificHeat.Text = TotalSpecificHeat[trackbarSpecificHeat.Value].ToString("0.###");
        }

        private void txtTempThermalConductivity_Validated(object sender, EventArgs e)
        {
            List<double> Temperature = GetTemperature();
            List<double> TotalThermalConductivity = GetTotalThermalConductivity(GetThermalConductivity());

            trackbarTempThermalConductivity.Value = Temperature.IndexOf(GetNearestTemp(Temperature, Convert.ToDouble(txtTempThermalConductivity.Text)));

            trackbarThermalConductivity.Value = trackbarTempThermalConductivity.Value;
            txtThermalConductivity.Text = TotalThermalConductivity[trackbarThermalConductivity.Value].ToString("0.###");
        }

        private void txtTempDynamicViscosity_Validated(object sender, EventArgs e)
        {
            List<double> Temperature = GetTemperature();
            List<double> TotalDynamicViscosity = GetTotalDynamicViscosity(GetDynamicViscosity());

            trackbarTempDynamicViscosity.Value = Temperature.IndexOf(GetNearestTemp(Temperature, Convert.ToDouble(txtTempDynamicViscosity.Text)));

            trackbarDynamicViscosity.Value = trackbarTempDynamicViscosity.Value;
            txtDynamicViscosity.Text = TotalDynamicViscosity[trackbarDynamicViscosity.Value].ToString("0.######");
        }




        private void txtDensity_Validated(object sender, EventArgs e)
        {
            List<double> TotalDensity = GetTotalDensity(GetDensity());
            List<double> Temperature = GetTemperature();

            trackbarDensity.Value = TotalDensity.IndexOf(GetNearest(TotalDensity, Convert.ToDouble(txtDensity.Text)));

            trackbarTempDensity.Value = trackbarDensity.Value;
            txtDensity.Text = (TotalDensity[trackbarTempDensity.Value]).ToString("0.000");
            txtTempDensity.Text = (Temperature[trackbarTempDensity.Value]-273.15).ToString("0.0");
        }

        private void txtSpecificHeat_Validated(object sender, EventArgs e)
        {
            List<double> TotalSpecificHeat = GetTotalSpecificHeat(GetSpecificHeat());
            List<double> Temperature = GetTemperature();

            trackbarSpecificHeat.Value = TotalSpecificHeat.IndexOf(GetNearest(TotalSpecificHeat, Convert.ToDouble(txtSpecificHeat.Text)));

            trackbarTempSpecificHeat.Value = trackbarSpecificHeat.Value;
            txtSpecificHeat.Text = (TotalSpecificHeat[trackbarTempSpecificHeat.Value]).ToString("0.000");
            txtTempSpecificHeat.Text = (Temperature[trackbarTempSpecificHeat.Value] - 273.15).ToString("0.0");
        }

        private void txtThermalConductivity_Validated(object sender, EventArgs e)
        {
            List<double> TotalThermalConductivity = GetTotalThermalConductivity(GetThermalConductivity());
            List<double> Temperature = GetTemperature();

            trackbarThermalConductivity.Value = TotalThermalConductivity.IndexOf(GetNearest(TotalThermalConductivity, Convert.ToDouble(txtThermalConductivity.Text)));

            trackbarTempThermalConductivity.Value = trackbarThermalConductivity.Value;
            txtThermalConductivity.Text = (TotalThermalConductivity[trackbarTempThermalConductivity.Value]).ToString("0.000");
            txtTempThermalConductivity.Text = (Temperature[trackbarTempThermalConductivity.Value] - 273.15).ToString("0.0");
        }

        private void txtDynamicViscosity_Validated(object sender, EventArgs e)
        {
            List<double> TotalDynamicViscosity = GetTotalDynamicViscosity(GetDynamicViscosity());
            List<double> Temperature = GetTemperature();

            trackbarDynamicViscosity.Value = TotalDynamicViscosity.IndexOf(GetNearest(TotalDynamicViscosity, Convert.ToDouble(txtDynamicViscosity.Text)));

            trackbarTempDynamicViscosity.Value = trackbarDynamicViscosity.Value;
            txtDynamicViscosity.Text = (TotalDynamicViscosity[trackbarTempDynamicViscosity.Value]).ToString("0.0000000");
            txtTempDynamicViscosity.Text = (Temperature[trackbarTempDynamicViscosity.Value] - 273.15).ToString("0.0");
        }



        #endregion


        #region trackbar value to text 
        private void trackbarTempDensity_Scroll(object sender, EventArgs e)
        {
            List<double> TotalDensity = GetTotalDensity(GetDensity());

            txtTempDensity.Text = ((trackbarTempDensity.Value) * 20.0 + 300.0).ToString("0.0");
            trackbarDensity.Value = trackbarTempDensity.Value;
            txtDensity.Text = (TotalDensity[trackbarDensity.Value]).ToString("0.000");
        }

        private void trackbarTempSpecificHeat_Scroll(object sender, EventArgs e)
        {
            List<double> TotalSpecificHeat = GetTotalSpecificHeat(GetSpecificHeat());

            txtTempSpecificHeat.Text = ((trackbarTempSpecificHeat.Value) * 20.0 + 300.0).ToString("0.0");
            trackbarSpecificHeat.Value = trackbarTempSpecificHeat.Value;
            txtSpecificHeat.Text = (TotalSpecificHeat[trackbarSpecificHeat.Value]).ToString("0.000");
        }

        private void trackbarTempThermalConductivity_Scroll(object sender, EventArgs e)
        {
            List<double> TotalThermalConductivity = GetTotalThermalConductivity(GetThermalConductivity());

            txtTempThermalConductivity.Text = ((trackbarTempThermalConductivity.Value) * 20.0 + 300.0).ToString("0.0");
            trackbarThermalConductivity.Value = trackbarTempThermalConductivity.Value;
            txtThermalConductivity.Text = (TotalThermalConductivity[trackbarThermalConductivity.Value]).ToString("0.000");
        }

        private void trackbarTempDynamicViscosity_Scroll(object sender, EventArgs e)
        {
            List<double> TotalDynamicViscosity = GetTotalDynamicViscosity(GetDynamicViscosity());
            txtTempDynamicViscosity.Text = ((trackbarTempDynamicViscosity.Value) * 20.0 + 300.0).ToString("0.0");
            trackbarDynamicViscosity.Value = trackbarTempDynamicViscosity.Value;

            txtDynamicViscosity.Text = (TotalDynamicViscosity[trackbarDynamicViscosity.Value]).ToString("0.0000000");
        }

        private void trackbarDensity_Scroll(object sender, EventArgs e)
        {
            List<double> TotalDensity = GetTotalDensity(GetDensity());
            txtDensity.Text = (TotalDensity[trackbarDensity.Value]).ToString("0.000");
            trackbarTempDensity.Value = trackbarDensity.Value;

            txtTempDensity.Text = ((trackbarTempDensity.Value) * 20.0 + 300.0).ToString("0.0");
        }

        private void trackbarSpecificHeat_Scroll(object sender, EventArgs e)
        {
            List<double> TotalSpecificHeat = GetTotalSpecificHeat(GetSpecificHeat());
            txtSpecificHeat.Text = (TotalSpecificHeat[trackbarSpecificHeat.Value]).ToString("0.000");
            trackbarTempSpecificHeat.Value = trackbarSpecificHeat.Value;

            txtTempSpecificHeat.Text = ((trackbarTempSpecificHeat.Value) * 20.0 + 300.0).ToString("0.0");
        }

        private void trackbarThermalConductivity_Scroll(object sender, EventArgs e)
        {
            List<double> TotalThermalConductivity = GetTotalThermalConductivity(GetThermalConductivity());
            txtThermalConductivity.Text = (TotalThermalConductivity[trackbarThermalConductivity.Value]).ToString("0.000");
            trackbarTempThermalConductivity.Value = trackbarThermalConductivity.Value;

            txtTempThermalConductivity.Text = ((trackbarTempThermalConductivity.Value) * 20.0 + 300.0).ToString("0.0");

        }

        private void trackbarDynamicViscosity_Scroll(object sender, EventArgs e)
        {
            List<double> TotalDynamicViscosity = GetTotalDynamicViscosity(GetDynamicViscosity());
            txtDynamicViscosity.Text = (TotalDynamicViscosity[trackbarDynamicViscosity.Value]).ToString("0.0000000");
            trackbarTempDynamicViscosity.Value = trackbarDynamicViscosity.Value;

            txtTempDynamicViscosity.Text = ((trackbarTempDynamicViscosity.Value) * 20.0 + 300.0).ToString("0.0");
        }





        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            chtDensity.Series[0].MarkerColor = Color.Blue;
            chtSpecificHeat.Series[0].MarkerColor = Color.Blue;
            chtThermalConductivity.Series[0].MarkerColor = Color.Blue;
            chtDynamicViscosity.Series[0].MarkerColor = Color.Blue;
        }
    }


}

