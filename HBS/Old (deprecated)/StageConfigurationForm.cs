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
    public partial class StageConfigurationForm : Form
    {
        public bool IsFinish { get; set; }
        public List<string> BrickNames { get; set; }
        public List<double> BrickHeights { get; set; }

        public StageConfigurationForm()
        {
            InitializeComponent();
            IsFinish = false;
        }

        public void SetAllDataFromUd()
        {
            for (int i = 0; i < BrickNames.Count; i++)
                dgvBricks.Rows.Add(new object[] { i + 1, BrickNames[i], BrickHeights[i] });
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int index = dgvBricks.SelectedCells[0].RowIndex;
            
            if (index == 0)
                return;

            DataGridViewRow row = dgvBricks.Rows[index];
            dgvBricks.Rows.RemoveAt(index);
            dgvBricks.Rows.Insert(index - 1, row);
            dgvBricks.CurrentCell= dgvBricks[0, index - 1];
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
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            dgvBricks.Rows.RemoveAt(dgvBricks.SelectedCells[0].RowIndex);
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

                dgvBricks.Rows.Insert(index + 1, new object[] { dgvBricks.Rows.Count + 1, brickName, null });

                for (int i = 0; i < rows.Count; i++)
                    dgvBricks.Rows.Add(rows[i]);
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
    }
}
