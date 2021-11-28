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

namespace HBS
{
    public partial class GasCompUsrCtrl : UserControl
    {
        #region Variables
        private const string _format = "#0.00";
        public enum FractionType { Mole = 0, Mass }
        public FractionType Type { get; set; }

        public CGas Gas0 { get; set; }
        public CGas Gas1 { get; set; }
        public CGas Gas2 { get; set; }
        public CGas Gas10 { get; set; }

        public string Gas0Name { get; set; }
        public string Gas1Name { get; set; }
        public string Gas2Name { get; set; }
        public string Gas10Name { get; set; }

        private List<CGas> Gases = new List<CGas>();
        private List<CheckBox> CheckBoxes = new List<CheckBox>();
        private List<DoughnutCharUsrCtrl> Charts = new List<DoughnutCharUsrCtrl>();
        private List<DataGridViewColumn> GasCompositionColumns = new List<DataGridViewColumn>();
        private List<DataGridViewColumn> SumColumns = new List<DataGridViewColumn>();

        private bool _isInitial = false;
        private bool _isBeingEdit = false;
        #endregion



        #region Constructor method
        public GasCompUsrCtrl()
        {
            InitializeComponent();

            foreach (CGas.Composition composition in CGas.GetCompositionList())
            {
                object[] rows1 = new object[] { composition, null };
                dgvGasCompositions.Rows.Add(rows1);
            }

            object[] rows2 = new object[] { "Sum", null };
            dgvSum.Rows.Add(rows2);
        }
        #endregion



        #region Event methods
        /// <summary>
        /// Event method when the form is loaded.
        /// </summary>
        /// <param name="sender">Ojbect.</param>
        /// <param name="e">Event Arguments</param>
        private void GasCompUsrCtrl_Load(object sender, EventArgs e)
        {
            // for 1st row...
            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Absolute;
            tableLayoutPanel1.RowStyles[0].Height = 50;

            int unitHeight = 100 / (CGas.GetNumbOfComposition() + 2);

            // for 2nd row...
            tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[1].Height = unitHeight * (CGas.GetNumbOfComposition() + 1);
            dgvGasCompositions_Resize(dgvGasCompositions, e);

            // for 3rd row...
            tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[2].Height = unitHeight;
            dgvSum_Resize(dgvSum, e);

            // clear selections...
            dgvGasCompositions_SelectionChanged(dgvGasCompositions, e);
            dgvSum_SelectionChanged(dgvSum, e);

            _isInitial = true;
        }
        /// <summary>
        /// Event method for setting chart when GasCompUsrCtrl is resized.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void GasCompUsrCtrl_Resize(object sender, EventArgs e)
        {
            if (_isInitial)
                SetChart(-1);
        }

        #region event methods for dvgGasComposition
        /// <summary>
        /// Event method when the datagridview is starting to edit.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvGasCompositions_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex < 2)
                return;

            DataGridView dataGridView = (DataGridView)sender;
            CGas.Composition comp = (CGas.Composition)Enum.Parse(typeof(CGas.Composition), dataGridView[0, e.RowIndex].Value.ToString());

            CGas gas = GetGas(dataGridView, e.ColumnIndex);
            CGas.Fraction fraction = GetFraction(gas);

            dataGridView[e.ColumnIndex, e.RowIndex].Value = fraction[comp] * 100.0;
            _isBeingEdit = true;
        }

        /// <summary>
        /// Event method to validate user input and gas value input.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvGasCompositions_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
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

            CGas.Composition comp = (CGas.Composition)Enum.Parse(typeof(CGas.Composition), dataGridView[0, e.RowIndex].Value.ToString());
            CGas gas = GetGas(dataGridView, e.ColumnIndex);
            CGas.Fraction fraction = GetFraction(gas);

            if (_isBeingEdit)
                fraction[comp] = d / 100.0;
           
        }

        /// <summary>
        /// Event method to resize datagridview cellsize and setting charts once the data is validated.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvGasCompositions_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            if (e.ColumnIndex < 2)
                return;

            DataGridView dataGridView = (DataGridView)sender;
            if (e.RowIndex < 0 || e.RowIndex > dataGridView.RowCount)
                return;

            CGas.Composition comp = (CGas.Composition)Enum.Parse(typeof(CGas.Composition), dataGridView[0, e.RowIndex].Value.ToString());
            CGas gas = GetGas(dataGridView, e.ColumnIndex);
            CGas.Fraction fraction = GetFraction(gas);

            if (_isBeingEdit)
            {
                dataGridView[e.ColumnIndex, e.RowIndex].Value = (fraction[comp] * 100.0).ToString("#0.00");

                SetDataGridViewSum(e.ColumnIndex);
                SetChart(e.ColumnIndex - 2);
            }

            dgvGasCompositions_Resize(sender, e);

            dgvSum_Resize(dgvSum, e);

            UpdateSelectedUDGasData();
        }



        /// <summary>
        /// Clears the selection of datagridview cell.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvGasCompositions_Leave(object sender, EventArgs e)
        {
            ((DataGridView)sender).ClearSelection();
        }

        /// <summary>
        /// Event method that stores editted value to datagridview cells after cell edidting ends.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvGasCompositions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 2)
                return;

            DataGridView dataGridView = (DataGridView)sender;

            double value = double.Parse(dataGridView[e.ColumnIndex, e.RowIndex].Value.ToString());
            dataGridView[e.ColumnIndex, e.RowIndex].Value = value.ToString("#0.00");

            _isBeingEdit = false;
        }

        /// <summary>
        /// Event method that clears the previous selection when selection is changed.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvGasCompositions_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            if (dataGridView.SelectedCells.Count == 1)
                if (dataGridView.SelectedCells[0].ColumnIndex < 2)
                    dataGridView.ClearSelection();
        }

        /// <summary>
        /// Event method that changes selection when cell is clicked.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvGasCompositions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvGasCompositions_SelectionChanged(sender, e);
        }

        /// <summary>
        /// Event method that changes selection when cell is double-clicked.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvGasCompositions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvGasCompositions_SelectionChanged(sender, e);
        }

        /// <summary>
        /// Event method that resizes the dgvGascomposition.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvGasCompositions_Resize(object sender, EventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            DataGridViewMethods.ResizeGasComposition(dataGridView);
        }

        /// <summary>
        /// Event method that resizes added column of dgvGasCompositions.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvGasCompositions_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgvGasCompositions_Resize(dgvGasCompositions, e);
        }
        #endregion

        #region event methods for dvgSum

        /// <summary>
        /// Event method that clears the previous selection when selection is changed.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvSum_SelectionChanged(object sender, EventArgs e)
        {
            ((DataGridView)sender).ClearSelection();
        }

        /// <summary>
        /// Event method that resizes the dgvSum.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvSum_Resize(object sender, EventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            int dvgHeight = dataGridView.Height;

            dataGridView.Rows[0].Height = dvgHeight;
        }

        /// <summary>
        /// Event method that changes selection when cell is clicked.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvSum_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSum_SelectionChanged(sender, e);
        }

        /// <summary>
        /// Event method that changes selection when cell is double-clicked.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void dgvSum_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSum_SelectionChanged(sender, e);
        }
        #endregion

        #region event methods for radio buttions
        /// <summary>
        /// Event method for setting dgvGascompositions with mole-fraction values, dgvSum with sum, and chart.  
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void rbMoleFraction_CheckedChanged(object sender, EventArgs e)
        {
            DataGridView dataGridView = dgvGasCompositions;

            if (!_isInitial)
                return;

            RadioButton radioButton = (RadioButton)sender;

            if (radioButton.Checked)
            {
                Type = FractionType.Mole;

                for (int j = 0; j < Gases.Count; j++)
                {
                    for (int i = 0; i < dataGridView.RowCount; i++)
                    {
                        CGas.Composition composition = GetRowGasCompositionFromRowHead(i);
                        dataGridView[j + 2, i].Value = (Gases[j].MoleFraction[composition] * 100.0).ToString(_format);
                    }
                }

                SetDataGridViewSum(-1);
                SetChart(-1);
            }
        }

        /// <summary>
        /// Event method for setting dgvGascompositions with mass-fraction values, dgvSum with sum, and chart.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void rbMassFraction_CheckedChanged(object sender, EventArgs e)
        {
            DataGridView dataGridView = dgvGasCompositions;

            if (!_isInitial)
                return;

            RadioButton radioButton = (RadioButton)sender;

            if (radioButton.Checked)
            {
                Type = FractionType.Mass;

                for (int j = 0; j < Gases.Count; j++)
                {
                    for (int i = 0; i < dataGridView.RowCount; i++)
                    {
                        CGas.Composition composition = GetRowGasCompositionFromRowHead(i);
                        dataGridView[j + 2, i].Value = (Gases[j].MassFraction[composition] * 100.0).ToString(_format);
                    }
                }
                SetDataGridViewSum(-1);
                SetChart(-1);
            }
        }
        #endregion

        #region event method(s) for check box(es)
        /// <summary>
        /// Event method to change visibility of gas columns.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Event arguments.</param>
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;

            for (int i = 0; i < CheckBoxes.Count; i++)
            {
                if (checkBox.Equals(CheckBoxes[i]))
                {
                    dgvGasCompositions.Columns["ColumnGas_" + CheckBoxes[i].Text].Visible = checkBox.Checked;
                    dgvSum.Columns["ColumnSum_" + CheckBoxes[i].Text].Visible = checkBox.Checked;
                }
            }

            bool checker = false;
            for (int i = 0; i < CheckBoxes.Count - 1; i++)
            {
                if (!checker && !CheckBoxes[i].Checked)
                {
                    checker = false;
                }
                else
                {
                    checker = true;
                    break;
                }
            }

            if (!checker)
            {
                MessageBox.Show(HBS_Shared.Properties.Settings.Default.MSG_CHECK_CHECKBOX, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkBox.Checked = true;
                return;
            }
        }
        #endregion



        #endregion



        #region Functional methods
        public void UpdateSelectedUDGasData()
        {
            ST_UD ud = ST_UD.GetInstance();
            int iCol = GetCurrentColumnIndex();

            List<double> value = GetData(iCol);

            for (int i = 0; i < value.Count; i++)
                value[i] /= 100;

            if (GetFractionType() == GasCompUsrCtrl.FractionType.Mole)
                if (iCol == 2)
                    ud.M_GasCalculation.BFG.MoleFraction = new CGas.Fraction(value);
                else if (iCol == 3)
                    ud.M_GasCalculation.COG.MoleFraction = new CGas.Fraction(value);
                else if (iCol == 4)
                    ud.M_GasCalculation.XGas.MoleFraction = new CGas.Fraction(value);
                else if (iCol == 5)
                    ud.M_GasCalculation.MGas.MoleFraction = new CGas.Fraction(value);
                else
                    return;
            else
                if (iCol == 2)
                    ud.M_GasCalculation.BFG.MassFraction = new CGas.Fraction(value);
                else if (iCol == 3)
                    ud.M_GasCalculation.COG.MassFraction = new CGas.Fraction(value);
                else if (iCol == 4)
                    ud.M_GasCalculation.XGas.MassFraction = new CGas.Fraction(value);
                else if (iCol == 5)
                    ud.M_GasCalculation.MGas.MassFraction = new CGas.Fraction(value);
                else
                    return;
        }

        public void UpdateUDGasData()
        {
            ST_UD ud = ST_UD.GetInstance();

            CGas BFG = GetGas(dgvGasCompositions, 2);
            ud.M_GasCalculation.BFG.MassFraction = BFG.MassFraction;
            ud.M_GasCalculation.BFG.MoleFraction = BFG.MoleFraction;

            CGas COG = GetGas(dgvGasCompositions, 3);
            ud.M_GasCalculation.COG.MassFraction = COG.MassFraction;
            ud.M_GasCalculation.COG.MoleFraction = COG.MoleFraction;

            CGas XGas = GetGas(dgvGasCompositions, 4);
            ud.M_GasCalculation.XGas.MassFraction = XGas.MassFraction;
            ud.M_GasCalculation.XGas.MoleFraction = XGas.MoleFraction;

            CGas MGas = GetGas(dgvGasCompositions, 5);
            ud.M_GasCalculation.MGas.MassFraction = MGas.MassFraction;
            ud.M_GasCalculation.MGas.MoleFraction = MGas.MoleFraction;

           
        }

        public FractionType GetFractionType()
        {
            if (rbMoleFraction.Checked)
                return FractionType.Mole;
            else if (rbMassFraction.Checked)
                return FractionType.Mass;
            else
                throw CException.Show(CException.Type.UnsupportedKeyword);
        }


        /// <summary>
        /// Functional method for normalizing mole/mass fraction values of dgvGasCompositions.
        /// </summary>
        public void SetNormalizing()
        {
            switch (dgvGasCompositions.ColumnCount)
            {
                case 4:
                    Gas0 = GetGas(dgvGasCompositions, 2);
                    Gas1 = null;
                    Gas2 = null;
                    Gas10 = GetGas(dgvGasCompositions, 3);
                    break;
                case 5:
                    Gas0 = GetGas(dgvGasCompositions, 2);
                    Gas1 = GetGas(dgvGasCompositions, 3);
                    Gas2 = null;
                    Gas10 = GetGas(dgvGasCompositions, 4);
                    break;
                case 6:
                    Gas0 = GetGas(dgvGasCompositions, 2);
                    Gas1 = GetGas(dgvGasCompositions, 3);
                    Gas2 = GetGas(dgvGasCompositions, 4);
                    Gas10 = GetGas(dgvGasCompositions, 5);
                    break;
            }

            FractionType type = FractionType.Mole;

            if (rbMoleFraction.Checked)
                type = FractionType.Mole;
            else if (rbMassFraction.Checked)
                type = FractionType.Mass;
            else
                throw CException.Show(CException.Type.UnsupportedKeyword);

            if (Gas0 != null)
            {
                Gas0.MoleFraction.Normalize();
                Gas0.MassFraction.Normalize();
            }

            if (Gas1 != null)
            {
                Gas1.MoleFraction.Normalize();
                Gas1.MassFraction.Normalize();
            }

            if (Gas2 != null)
            {
                Gas2.MoleFraction.Normalize();
                Gas2.MassFraction.Normalize();
            }

            if (Gas10 != null)
            {
                Gas10.MoleFraction.Normalize();
                Gas10.MassFraction.Normalize();
            }

            switch (dgvGasCompositions.ColumnCount)
            {
                case 4:
                    SetDataGridViewGasCompositions(type, Gas0, 2);
                    SetDataGridViewGasCompositions(type, Gas10, 3);
                    break;
                case 5:
                    SetDataGridViewGasCompositions(type, Gas0, 2);
                    SetDataGridViewGasCompositions(type, Gas1, 3);
                    SetDataGridViewGasCompositions(type, Gas10, 4);
                    break;
                case 6:
                    SetDataGridViewGasCompositions(type, Gas0, 2);
                    SetDataGridViewGasCompositions(type, Gas1, 3);
                    SetDataGridViewGasCompositions(type, Gas2, 4);
                    SetDataGridViewGasCompositions(type, Gas10, 5);
                    break;
            }

            SetDataGridViewSum(-1);
        }


        /// <summary>
        /// Functional method for setting chart based on gas fraction values.
        /// </summary>
        /// <param name="columnIndex">Column index. columnIndex = -1 means that update all chart.</param>
        public void SetChart(int columnIndex = -1)
        {
            switch (columnIndex)
            {
                case -1:
                    for (int i = 0; i < Gases.Count; i++)
                        SetChart(i);
                    break;

                default:
                    string chartTitle = dgvGasCompositions.Columns[columnIndex + 2].HeaderText;
                    SetChart(Gases[columnIndex], Charts[columnIndex], chartTitle, Type);
                    break;
            }
        }


        /// <summary>
        /// Functional method for setting one chart based on basic information.
        /// </summary>
        /// <param name="gas">CGas class.</param>
        /// <param name="chart">DoughnutCharUsrCtrl class.</param>
        /// <param name="chartTitle">Chart title.</param>
        /// <param name="type">Mole fraction or mass fraction.</param>
        private void SetChart(CGas gas, DoughnutCharUsrCtrl chart, string chartTitle, FractionType type)
        {
            if (gas == null || chart == null)
                return;

            string typeOfFraction = string.Empty;

            switch (type)
            {
                case FractionType.Mole:
                    typeOfFraction += " (Mole Fraction)";
                    break;
                case FractionType.Mass:
                    typeOfFraction += " (Mass Fraction)";
                    break;
                default:
                    throw CException.Show(CException.Type.UnsupportedKeyword);
            }

            chart.ThresholdValue = 0.05;

            chart.SetChart(chartTitle + typeOfFraction, GetFraction(gas));
            chart.Fraction = GetFraction(gas);
            chart.SetListView();
            chart.Update();
        }

        /// <summary>
        /// Functional method for returning the mole/mass fraction of gas type.
        /// </summary>
        /// <param name="gas">CGas class.</param>
        /// <returns></returns>
        private CGas.Fraction GetFraction(CGas gas)
        {
            switch (Type)
            {
                case FractionType.Mole:
                    return gas.MoleFraction;
                case FractionType.Mass:
                    return gas.MassFraction;
                default:
                    throw CException.Show(CException.Type.UnsupportedKeyword);
            }
        }

        /// <summary>
        /// Functional method for getting the iRowth composition name as string.
        /// </summary>
        /// <param name="iRow">row index</param>
        /// <returns></returns>
        private CGas.Composition GetRowGasCompositionFromRowHead(int iRow)
        {
            DataGridView dataGridView = dgvGasCompositions;
            return (CGas.Composition)Enum.Parse(typeof(CGas.Composition), dataGridView[0, iRow].Value.ToString());
        }

        /// <summary>
        /// Functional method for getting the column of a datagridview.
        /// </summary>
        /// <param name="dataGridView">dgvGasCompositions, dgvSums.</param>
        /// <param name="iCol">Column index of dataGridView.</param>
        /// <returns></returns>
        private DataGridViewColumn GetDataGridViewColumn(DataGridView dataGridView, int iCol)
        {
            string headText = dataGridView.Columns[iCol].HeaderText;

            if (dataGridView == dgvGasCompositions)
                return dataGridView.Columns["ColumnGas_" + headText];
            else if (dataGridView == dgvSum)
                return dataGridView.Columns["ColumnSum_" + headText];
            else
                throw CException.Show(CException.Type.UnsupportedKeyword);
        }

        /// <summary>
        /// Functional method for getting gases from coressponding datagridview columns.
        /// </summary>
        /// <param name="dataGridView">dgvGasCompositions.</param>
        /// <param name="iCol">Column index of dataGridView.</param>
        /// <returns></returns>
        public CGas GetGas(DataGridView dataGridView, int iCol)
        {
            return Gases[GetDataGridViewColumn(dataGridView, iCol).Index - 2];
        }

        /// <summary>
        /// Functional method for initializing table of gas by removing all columns and checkboxes in panel.
        /// </summary>
        public void Init()
        {
            // delete all columns in dvgGasComposition
            for (int i = 0; i < GasCompositionColumns.Count; i++)
                dgvGasCompositions.Columns.Remove(dgvGasCompositions.Columns[dgvGasCompositions.Columns.Count - 1]);

            GasCompositionColumns = new List<DataGridViewColumn>();

            // delete all columns in dvgSum
            for (int i = 0; i < SumColumns.Count; i++)
                dgvSum.Columns.Remove(dgvSum.Columns[dgvSum.Columns.Count - 1]);

            SumColumns = new List<DataGridViewColumn>();

            // delete all checkbox in panel
            List<CheckBox> checkBoxes = new List<CheckBox>();
            foreach (Control control in panel2.Controls)
                if (control.GetType() == typeof(CheckBox))
                    checkBoxes.Add((CheckBox)control);

            for (int i = 0; i < checkBoxes.Count; i++)
                checkBoxes[i].Dispose();

            Gases.Clear();

            CheckBoxes = new List<CheckBox>();

            _isInitial = true;
        }

        /// <summary>
        /// Functional method for setting initial fraction type, mole fraction or mass fraction.
        /// </summary>
        /// <param name="type">Fraction type.</param>
        public void SetInitialFractionType(FractionType type)
        {
            switch (type)
            {
                case FractionType.Mole:
                    rbMoleFraction.Checked = true;
                    break;
                case FractionType.Mass:
                    rbMassFraction.Checked = true;
                    break;
                default:
                    throw CException.Show(CException.Type.UnsupportedKeyword);
            }
        }

        /// <summary>
        /// Functional method for adding gas, graph components, and title to checkboxes.
        /// </summary>
        /// <param name="gasName">Gas name.</param>
        /// <param name="gas">CGas class.</param>
        /// <param name="chart">Chart Component.</param>
        public void Add(string gasName, CGas gas, DoughnutCharUsrCtrl chart)
        {
            // cell stype...
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // datagridview for dgvGasCompsition
            DataGridViewTextBoxColumn columnGas = new DataGridViewTextBoxColumn();

            columnGas.DefaultCellStyle = cellStyle;
            columnGas.HeaderText = gasName;
            columnGas.Name = "ColumnGas_" + gasName;

            dgvGasCompositions.Columns.Add(columnGas);
            GasCompositionColumns.Add(columnGas);

            /// datagridview for dgvSum
            DataGridViewTextBoxColumn columnSum = new DataGridViewTextBoxColumn();

            columnSum.DefaultCellStyle = cellStyle;
            columnSum.HeaderText = gasName;
            columnSum.Name = "ColumnSum_" + gasName;
            columnSum.ReadOnly = true;

            dgvSum.Columns.Add(columnSum);
            SumColumns.Add(columnSum);

            // checkbox...
            CheckBox checkBox = new CheckBox();
            checkBox.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left)));
            checkBox.AutoSize = true;
            checkBox.Checked = true;
            checkBox.CheckState = CheckState.Checked;
            checkBox.Dock = DockStyle.None;

            int xPixel = 0;
            foreach (CheckBox cb in CheckBoxes)
                xPixel += cb.Width;

            checkBox.Location = new Point(xPixel, 0);
            checkBox.Margin = new Padding(4, 5, 4, 5);
            checkBox.Name = "CheckBox_" + gasName;
            checkBox.Size = new Size(67, 24);
            checkBox.Text = gasName;
            checkBox.UseVisualStyleBackColor = true;
            checkBox.CheckedChanged += new EventHandler(this.checkBox_CheckedChanged);

            CheckBoxes.Add(checkBox);
            Gases.Add(gas);
            Charts.Add(chart);
            panel2.Controls.Add(checkBox);

            // add Gas data to data grid view...
            if (gas == null)
                return;

            DataGridView dataGridView = dgvGasCompositions;
            int iColumn = dataGridView.Columns[columnGas.Name].Index;
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                CGas.Composition comp = DataGridViewMethods.GetCompositionOnDataGridView(dataGridView, i);
                dataGridView[1, i].Value = CGas.MolarMass[comp].ToString(_format);
                dataGridView[iColumn, i].Value = (GetFraction(gas)[comp] * 100.0).ToString(_format);
            }

            dgvSum[iColumn, 0].Value = (GetFraction(gas).GetSumValues() * 100.0).ToString(_format);
        }

        /// <summary>
        /// Functional method for setting each column of dgvGasCompositions to its corresponding gas compositions.
        /// </summary>
        /// <param name="type">Fraction Type.</param>
        /// <param name="Gas">CGas class.</param>
        /// <param name="iCol">Column index of datagridview.</param>
        public void SetDataGridViewGasCompositions(FractionType type, CGas Gas, int iCol)
        {
            DataGridView dataGridView = dgvGasCompositions;

            
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                CGas.Composition composition = GetRowGasCompositionFromRowHead(i);
                    
                if (type == FractionType.Mass)
                {
                    dataGridView[iCol, i].Value = (Gas.MassFraction[composition] * 100.0).ToString(_format);
                }
                else
                {
                    dataGridView[iCol, i].Value = (Gas.MoleFraction[composition] * 100.0).ToString(_format);
                }
                    
            }
        }

        /// <summary>
        /// Functional method for summing the mole/mass fraction values of a given column.
        /// </summary>
        /// <param name="iCol">Column index of datagridview.</param>
        public void SetDataGridViewSum(int iCol)
        {
            switch (iCol)
            {
                case -1:
                    for (int i = 2; i < dgvSum.ColumnCount; i++)
                        SetDataGridViewSum(i);
                    break;
                default:
                    dgvSum[iCol, 0].Value = (GetFraction(Gases[iCol - 2]).GetSumValues() * 100.0).ToString(_format);
                    break;
            }
        }

        /// <summary>
        /// Functional method for calculating M-Gas fraction based on mixing mole ratio.
        /// </summary>
        /// <param name="text1">txtBFG.</param>
        /// <param name="text2">txtCOG.</param>
        /// <param name="text3">txtX-Gas.</param>
        public void Calculate(TextBox text1, TextBox text2, TextBox text3)
        {
            DataGridView dataGridView = dgvGasCompositions;
            FractionType type = new FractionType();

            CGas bfg = GetGas(dataGridView, 2);
            CGas cog = GetGas(dataGridView, 3);
            CGas x_gas = GetGas(dataGridView, 4);

            double X_bfg = double.Parse(text1.Text);
            double X_cog = double.Parse(text2.Text);
            double X_x_gas = double.Parse(text3.Text);
            double X_total = X_bfg + X_cog + X_x_gas;

            CGas m_gas = new CGas();
            m_gas.MoleFraction = new CGas.Fraction();

            foreach (KeyValuePair<CGas.Composition, double> entry in bfg.MoleFraction)
            {
                m_gas.MoleFraction[entry.Key] = (X_bfg / X_total) * bfg.MoleFraction[entry.Key] + 
                    (X_cog / X_total) * cog.MoleFraction[entry.Key] + 
                    (X_x_gas / X_total) * x_gas.MoleFraction[entry.Key];

                m_gas.MassFraction = m_gas.MoleFraction.GetMassFraction();
            }

            if (rbMoleFraction.Checked)
            {
                type = FractionType.Mole;
                SetDataGridViewGasCompositions(type, m_gas, 5);
            }
            else if (rbMassFraction.Checked)
            {
                type = FractionType.Mass;
                SetDataGridViewGasCompositions(type, m_gas, 5);
            }             
            else
                throw CException.Show(CException.Type.UnsupportedKeyword);
        }

        /// <summary>
        /// Functional method to read csv file for gas value input.
        /// </summary>
        public void ReadCsvFile()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> contents = File.ReadAllLines(openFileDialog.FileName).ToList();
                List<string> headText = contents[0].Split(',').ToList();

                CGas.Fraction fraction0 = new CGas.Fraction();
                CGas.Fraction fraction1 = new CGas.Fraction();
                CGas.Fraction fraction2 = new CGas.Fraction();
                CGas.Fraction fraction10 = new CGas.Fraction();

                if (contents.Count > dgvGasCompositions.RowCount + 1 || headText.Count > dgvGasCompositions.ColumnCount)
                {
                    MessageBox.Show(CException.InvalidColumnRow, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ColumnSpecies0.HeaderText = headText[0];
                ColumnMolarMass0.HeaderText = headText[1];

                switch (headText.Count)
                {
                    case 4:
                        Gas0 = GetGas(dgvGasCompositions, 2);
                        Gas10 = GetGas(dgvGasCompositions, 3);

                        Gas0Name = headText[2];
                        Gas1Name = null;
                        Gas2Name = null;
                        Gas10Name = headText[3];
                        break;
                    case 5:
                        Gas0 = GetGas(dgvGasCompositions, 2);
                        Gas1 = GetGas(dgvGasCompositions, 3);
                        Gas10 = GetGas(dgvGasCompositions, 4);

                        Gas0Name = headText[2];
                        Gas1Name = headText[3];
                        Gas2Name = null;
                        Gas10Name = headText[4];
                        break;
                    case 6:
                        Gas0 = GetGas(dgvGasCompositions, 2);
                        Gas1 = GetGas(dgvGasCompositions, 3);
                        Gas2 = GetGas(dgvGasCompositions, 4);
                        Gas10 = GetGas(dgvGasCompositions, 5);

                        Gas0Name = headText[2];
                        Gas1Name = headText[3];
                        Gas2Name = headText[4];
                        Gas10Name = headText[5];
                        break;
                }

               
                    

                FractionForm form = new FractionForm();

                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.FractionType == FractionType.Mole)
                        rbMoleFraction.Checked = true;
                    else if (form.FractionType == FractionType.Mass)
                        rbMassFraction.Checked = true;
                    else
                        throw CException.Show(CException.Type.UnsupportedKeyword);

                    for (int i = 0; i < contents.Count - 1; i++)
                    {
                        List<string> columns = contents[i + 1].Split(',').ToList();

                        for (int j = 0; j < columns.Count; j++)
                            dgvGasCompositions[j, i].Value = columns[j];
                    }

                    for (int i = 0; i < contents.Count - 1; i++)
                    {
                        List<string> columns = contents[i + 1].Split(',').ToList();
                        CGas.Composition comp = (CGas.Composition)Enum.Parse(typeof(CGas.Composition), columns[0]);

                        switch (headText.Count)
                        {
                            case 4:
                                fraction0[comp] = double.Parse(columns[2]) / 100.0;
                                fraction10[comp] = double.Parse(columns[3]) / 100.0;
                                break;
                            case 5:
                                fraction0[comp] = double.Parse(columns[2]) / 100.0;
                                fraction1[comp] = double.Parse(columns[3]) / 100.0;
                                fraction10[comp] = double.Parse(columns[4]) / 100.0;
                                break;
                            case 6:
                                fraction0[comp] = double.Parse(columns[2]) / 100.0;
                                fraction1[comp] = double.Parse(columns[3]) / 100.0;
                                fraction2[comp] = double.Parse(columns[4]) / 100.0;
                                fraction10[comp] = double.Parse(columns[5]) / 100.0;
                                break;
                        }
                    }

                    switch (headText.Count)
                    {
                        case 4:
                            if (form.Normalize)
                            {
                                fraction0.Normalize();
                                fraction10.Normalize();
                            }
                            break;
                        case 5:
                            if (form.Normalize)
                            {
                                fraction0.Normalize();
                                fraction1.Normalize();
                                fraction10.Normalize();
                            }
                            break;
                        case 6:
                            if (form.Normalize)
                            {
                                fraction0.Normalize();
                                fraction1.Normalize();
                                fraction2.Normalize();
                                fraction10.Normalize();
                            }
                            break;
                    }

                    switch (form.FractionType)
                    {
                        case FractionType.Mole:
                            switch (headText.Count)
                            {
                                case 4:
                                    Gas0.MoleFraction = fraction0;
                                    Gas10.MoleFraction = fraction10;

                                    Gas0.MassFraction = Gas0.MoleFraction.GetMassFraction();
                                    Gas10.MassFraction = Gas10.MoleFraction.GetMassFraction();
                                    break;
                                case 5:
                                    Gas0.MoleFraction = fraction0;
                                    Gas1.MoleFraction = fraction1;
                                    Gas10.MoleFraction = fraction10;

                                    Gas0.MassFraction = Gas0.MoleFraction.GetMassFraction();
                                    Gas1.MassFraction = Gas1.MoleFraction.GetMassFraction();
                                    Gas10.MassFraction = Gas10.MoleFraction.GetMassFraction();
                                    break;
                                case 6:
                                    Gas0.MoleFraction = fraction0;
                                    Gas1.MoleFraction = fraction1;
                                    Gas2.MoleFraction = fraction2;
                                    Gas10.MoleFraction = fraction10;

                                    Gas0.MassFraction = Gas0.MoleFraction.GetMassFraction();
                                    Gas1.MassFraction = Gas1.MoleFraction.GetMassFraction();
                                    Gas2.MassFraction = Gas2.MoleFraction.GetMassFraction();
                                    Gas10.MassFraction = Gas10.MoleFraction.GetMassFraction();
                                    break;
                            }
                            break;
                        case FractionType.Mass:
                            switch (headText.Count)
                            {
                                case 4:
                                    Gas0.MassFraction = fraction0;
                                    Gas10.MassFraction = fraction10;

                                    Gas0.MoleFraction = Gas0.MoleFraction.GetMoleFraction();
                                    Gas10.MoleFraction = Gas10.MoleFraction.GetMoleFraction();
                                    break;
                                case 5:
                                    Gas0.MassFraction = fraction0;
                                    Gas1.MassFraction = fraction1;
                                    Gas10.MassFraction = fraction10;

                                    Gas0.MoleFraction = Gas0.MoleFraction.GetMoleFraction();
                                    Gas1.MoleFraction = Gas1.MoleFraction.GetMoleFraction();
                                    Gas10.MoleFraction = Gas10.MoleFraction.GetMoleFraction();
                                    break;
                                case 6:
                                    Gas0.MassFraction = fraction0;
                                    Gas1.MassFraction = fraction1;
                                    Gas2.MassFraction = fraction2;
                                    Gas10.MassFraction = fraction10;

                                    Gas0.MoleFraction = Gas0.MoleFraction.GetMoleFraction();
                                    Gas1.MoleFraction = Gas1.MoleFraction.GetMoleFraction();
                                    Gas2.MoleFraction = Gas2.MoleFraction.GetMoleFraction();
                                    Gas10.MoleFraction = Gas10.MoleFraction.GetMoleFraction();
                                    break;
                            }
                            break;
                    }

                    SetDataGridViewSum(-1);
                    SetChart(-1);
                }
            }
        }

        /// <summary>
        /// Functional method to write csv file for gas value input.
        /// </summary>
        public void WriteCsvFile()
        {
            List<string> contents = new List<string>();

            string headText = string.Empty;
            for (int j = 0; j < dgvGasCompositions.ColumnCount; j++)
                if (j == dgvGasCompositions.ColumnCount - 1)
                    headText += dgvGasCompositions.Columns[j].HeaderText;
                else
                    headText += dgvGasCompositions.Columns[j].HeaderText + ",";

            contents.Add(headText);

            for (int i = 0; i < dgvGasCompositions.RowCount; i++)
            {
                string cell = string.Empty;
                for (int j = 0; j < dgvGasCompositions.ColumnCount; j++)
                    if (j == dgvGasCompositions.ColumnCount - 1)
                        cell += dgvGasCompositions[j, i].Value.ToString();
                    else
                        cell += dgvGasCompositions[j, i].Value.ToString() + ",";
                contents.Add(cell);
            }

            string sum = string.Empty;
            for (int j = 0; j < dgvSum.ColumnCount; j++)
                if (j == dgvSum.ColumnCount - 1)
                {
                    if (dgvSum[j, 0].Value == null)
                        sum += string.Empty;
                    else
                        sum += dgvSum[j, 0].Value.ToString();
                }
                else
                {
                    if (dgvSum[j, 0].Value == null)
                        sum += string.Empty;
                    else
                        sum += dgvSum[j, 0].Value.ToString() + ",";
                }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                File.WriteAllLines(saveFileDialog.FileName, contents);
        }

        /// <summary>
        /// Functional method for removing M-Gas checkbox
        /// </summary>
        public void Finish()
        {
            // last checkbox will be gone.
            CheckBoxes[CheckBoxes.Count - 1].Visible = false;
        }


        public List<double> GetData(int iColumn)
        {
            List<double> value = new List<double>();

            DataGridView dataGridView = dgvGasCompositions;

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

        public int GetCurrentColumnIndex()
        {
            int iCol = dgvGasCompositions.CurrentCell.ColumnIndex;
            return iCol;
        }

        #endregion
    }

}