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
using System.IO;

namespace HBS
{
    public partial class BrickConfigurationUsrCtrl : UserControl
    {
        public double Length { get; set; }
        public double Diameter { get; set; }
        public double NumberOfBricks { get; set; }
        public bool IsFinish { get; set; }
        public string BrickName { get; set; }
        public string BrickHeight { get; set; }
        public string SelectedBrick { get; set; }
        public List<string> BrickNames { get; set; }
        public List<double> BrickHeights { get; set; }

        public BrickConfigurationUsrCtrl()
        {
            InitializeComponent();
            IsFinish = false;
        }


        public void SetAllDataFromUd()
        {
            //txtLength.Text = Length.ToString("#00.0");
            //txtDiameter.Text = Diameter.ToString("#00.0");
            //txtNumberOfBricks.Text = NumberOfBricks.ToString("#00");


            for (int i = 0; i < BrickNames.Count; i++)
                dgvBricks.Rows.Add(new object[] { i + 1, BrickNames[i], BrickHeights[i] });
        }

        private void UpdateUDData()
        {
            ST_UD ud = ST_UD.GetInstance();

            ud.BrickConfiguration.Length = double.Parse(txtLength.Text);
            ud.BrickConfiguration.Diameter = double.Parse(txtDiameter.Text);
            ud.BrickConfiguration.NumberofBricks = double.Parse(txtNumberOfBricks.Text);
            ud.PropertyGrid.SelectedObject = ud.BrickConfiguration;
        }

        private void UpdateIndex()
        {
            int index = dgvBricks.Rows.Count;
            for (int i = 0; i < index; i++)
                dgvBricks[0, i].Value = i;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            UpdateUDData();
        }

        private void btnOk2_Click(object sender, EventArgs e)
        {
            if (lbBricks.SelectedIndex < 0)
            {
                MessageBox.Show("Please select one brick at least.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SelectedBrick = lbBricks.Items[lbBricks.SelectedIndex].ToString();
            }

            int index = dgvBricks.SelectedCells[0].RowIndex;
            List<DataGridViewRow> rows = new List<DataGridViewRow>();

            for (int i = index + 1; i < dgvBricks.Rows.Count; i++)
                rows.Add(dgvBricks.Rows[i]);

            int count = dgvBricks.Rows.Count - index - 1;

            string brickName = SelectedBrick;

            for (int i = 0; i < count; i++)
                dgvBricks.Rows.RemoveAt(index + 1);

            dgvBricks.Rows.Insert(index + 1, new object[] { index + 1, brickName, BrickHeight });

            for (int i = 0; i < rows.Count; i++)
                dgvBricks.Rows.Add(rows[i]);


            UpdateIndex();
        }

        private void btnOk3_Click(object sender, EventArgs e)
        {
            List<string> brickName = new List<string>();
            List<double> brickHeight = new List<double>();

            for (int i = 0; i < dgvBricks.RowCount; i++)
            {
                brickName.Add(dgvBricks[1, i].Value.ToString());
                brickHeight.Add(double.Parse(dgvBricks[2, i].Value.ToString()) / 1000.0);
            }

            ST_UD ud = ST_UD.GetInstance();


            ud.BrickConfiguration.BrickNames = brickName;
            ud.BrickConfiguration.BrickHeights = brickHeight;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int index = dgvBricks.SelectedCells[0].RowIndex;
            List<DataGridViewRow> rows = new List<DataGridViewRow>();

            for (int i = index + 1; i < dgvBricks.Rows.Count; i++)
                rows.Add(dgvBricks.Rows[i]);

            int count = dgvBricks.Rows.Count - index - 1;

            AddBrickForm form = new AddBrickForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                string brickName = form.SelectedBrick;

                for (int i = 0; i < count; i++)
                    dgvBricks.Rows.RemoveAt(index + 1);

                dgvBricks.Rows.Insert(index + 1, new object[] { index + 1, brickName, null });

                for (int i = 0; i < rows.Count; i++)
                    dgvBricks.Rows.Add(rows[i]);
            }

            UpdateIndex();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int index = dgvBricks.SelectedCells[0].RowIndex;

            if (index == 0)
                return;

            DataGridViewRow row = dgvBricks.Rows[index];
            dgvBricks.Rows.RemoveAt(index);
            dgvBricks.Rows.Insert(index - 1, row);
            dgvBricks.CurrentCell = dgvBricks[0, index - 1];

            UpdateIndex();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int index = dgvBricks.SelectedCells[0].RowIndex;

            if (index == dgvBricks.RowCount - 1)
                return;

            DataGridViewRow row = dgvBricks.Rows[index];
            dgvBricks.Rows.RemoveAt(index);
            dgvBricks.Rows.Insert(index + 1, row);
            dgvBricks.CurrentCell = dgvBricks[0, index + 1];

            UpdateIndex();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            dgvBricks.Rows.RemoveAt(dgvBricks.SelectedCells[0].RowIndex);

            UpdateIndex();
        }

        private void BrickConfigurationUsrCtrl_Load(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();
            CLength UnitBrickHeight = ud.BrickConfiguration.Length;
            CLength UnitBrickDiameter = ud.BrickConfiguration.Diameter;

            txtLength.Text = UnitBrickHeight[CLength.Unit.mm].ToString("#0.0");
            txtDiameter.Text = UnitBrickDiameter[CLength.Unit.mm].ToString("#0.0");
            txtNumberOfBricks.Text = ud.BrickConfiguration.NumberofBricks.ToString();

            //BrickInfoForm Load
            //ListBox Load
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath + "\\Brick DB");
            List<FileInfo> fileInfos = directoryInfo.GetFiles("*.prop").ToList();

            for (int i = 0; i < fileInfos.Count; i++)
                lbBricks.Items.Add(Path.GetFileNameWithoutExtension(fileInfos[i].Name));

            //AddBrickForm Load
            ListBox listBox = lbBricks;
            DataGridView dataGridView1 = dgvBricks;
            for (int i = 0; i < ud.BrickConfiguration.BrickNames.Count; i++)
            {
                double index = i;
                string BrickName = ud.BrickConfiguration.BrickNames[i];
                CLength BrickHeight = ud.BrickConfiguration.BrickHeights[i];
                dataGridView1.Rows.Add(new object[] { index, BrickName, BrickHeight[CLength.Unit.mm] });
            }


        }

        private void dgvBricks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBricks.SelectedCells[0].RowIndex < 0)
                return;

            BrickInfoForm form = new BrickInfoForm();

            if (dgvBricks[2, dgvBricks.SelectedCells[0].RowIndex].Value == null)
                form.BrickHeight = 0.0;
            else
                form.BrickHeight = double.Parse(dgvBricks[2, dgvBricks.SelectedCells[0].RowIndex].Value.ToString());

            form.BrickName = dgvBricks[1, dgvBricks.SelectedCells[0].RowIndex].Value.ToString();

            if (form.ShowDialog() == DialogResult.OK)
            {
                dgvBricks[2, dgvBricks.SelectedCells[0].RowIndex].Value = form.BrickHeight;
            }
        }



        private void btnAddBrick_Click(object sender, EventArgs e)
        {
            string BrickName = txtName.Text;
            BrickHeight = txtHeight.Text;

            if (txtName.Text.Length != 0)
                lbBricks.Items.Add(BrickName);
            else
                MessageBox.Show("Please input the name of the brick");
            return;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtHeight.Text = "";
        }

        private void btnClear2_Click(object sender, EventArgs e)
        {
            if (lbBricks.SelectedIndex < 0)
            {
                MessageBox.Show("Please select one brick at least.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                lbBricks.Items.RemoveAt(lbBricks.SelectedIndex);
            }

        }

        public void SetDataFromFile(string filePath)
        {
            ST_UD ud = ST_UD.GetInstance();
            lbBricks.Items.Clear();
            dgvBricks.Rows.Clear();

            DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath + "\\Brick DB");
            List<FileInfo> fileInfos = directoryInfo.GetFiles("*.prop").ToList();

            ud.BrickConfiguration = new ST_UD.BrickConfigurationDataType(filePath);

            CLength UnitBrickHeight = ud.BrickConfiguration.Length;
            CLength UnitBrickDiameter = ud.BrickConfiguration.Diameter;

            txtLength.Text = UnitBrickHeight[CLength.Unit.mm].ToString("#0.0");
            txtDiameter.Text = UnitBrickDiameter[CLength.Unit.mm].ToString("#0.0");
            txtNumberOfBricks.Text = ud.BrickConfiguration.NumberofBricks.ToString();

            for (int i = 0; i < fileInfos.Count; i++)
                lbBricks.Items.Add(Path.GetFileNameWithoutExtension(fileInfos[i].Name));

            DataGridView dataGridView1 = dgvBricks;
            for (int i = 0; i < ud.BrickConfiguration.BrickNames.Count; i++)
            {
                double index = i;
                string BrickName = ud.BrickConfiguration.BrickNames[i];
                CLength BrickHeight = ud.BrickConfiguration.BrickHeights[i];
                dataGridView1.Rows.Add(new object[] { index, BrickName, BrickHeight[CLength.Unit.mm] });
            }
        }

        private void btnLoadDefault_Click(object sender, EventArgs e)
        {
            SetDataFromFile(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);
        }



        #region Validating/Validated functions
        private void dgvBricks_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
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

            if (e.ColumnIndex == 2)
            {
                if (!CValidityCheck.IsPositiveNumber(d, false, e))
                    return;
            }
        }

        private void txtLength_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if (!CValidityCheck.IsPositiveNumber(d, true, e))
                return;
        }

        private void txtDiameter_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if (!CValidityCheck.IsPositiveNumber(d, true, e))
                return;
        }

        private void txtNumberOfBricks_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if (!CValidityCheck.IsPositiveNumber(d, true, e))
                return;
        }

        private void txtHeight_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double d = 0.0;

            if (!CValidityCheck.IsNumber(textBox, e, out d))
                return;

            if (!CValidityCheck.IsPositiveNumber(d, true, e))
                return;
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            //no need to validate name
            return;
        }


        private void txtLength_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            ud.BrickConfiguration.Length = double.Parse(txtLength.Text);
            ud.PropertyGrid.SelectedObject = ud.BrickConfiguration;
        }

        private void txtDiameter_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            ud.BrickConfiguration.Diameter = double.Parse(txtDiameter.Text);
            ud.PropertyGrid.SelectedObject = ud.BrickConfiguration;
        }

        private void txtNumberOfBricks_Validated(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            ud.BrickConfiguration.NumberofBricks = double.Parse(txtNumberOfBricks.Text);
            ud.PropertyGrid.SelectedObject = ud.BrickConfiguration;
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
        public void UpdateUDBrickHeight(DataGridView dataGridView)
        {
            ST_UD ud = ST_UD.GetInstance();

            int iCol = dataGridView.CurrentCell.ColumnIndex;
            List<double> name = GetData(1, dataGridView);
            List<double> height = GetData(2, dataGridView);
            List<string> Name_real = new List<string>();

            for (int i = 0; i < name.Count; i++)
            {
               Name_real[i] = name[i].ToString();
            }

            if (iCol == 1)
                ud.BrickConfiguration.BrickNames = Name_real;
            else if (iCol == 2)
                ud.BrickConfiguration.BrickHeights = height;
            else
                return;
        } 
        private void dgvBricks_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            UpdateUDBrickHeight(dgvBricks);
        }
        #endregion

 
    }
}
