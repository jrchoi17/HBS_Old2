using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HBS_Shared;

namespace HBS
{
    public class DataGridViewMethods
    {
        #region DataGridView Resize Methods
        public static void ResizeGasComposition(DataGridView dataGridView)
        {
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            int dvgHeight = dataGridView.Height;
            int nCompositoin = CGas.GetNumbOfComposition();
            int headerHeight = dataGridView.ColumnHeadersHeight;
            int rowHeight = (dataGridView.Height - headerHeight) / (nCompositoin);

            foreach (DataGridViewRow dataGridViewRow in dataGridView.Rows)
                dataGridViewRow.Height = rowHeight;

            dataGridView.ColumnHeadersHeight = dvgHeight - nCompositoin * rowHeight;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        public static void ResizeFlowOperatingCondition(DataGridView dataGridView, int nRow = 12)
        {
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            int dvgHeight = dataGridView.Height;
            int headerHeight = dataGridView.ColumnHeadersHeight;
            int rowHeight = (dataGridView.Height - headerHeight) / (nRow);

            foreach (DataGridViewRow dataGridViewRow in dataGridView.Rows)
                dataGridViewRow.Height = rowHeight;

            dataGridView.ColumnHeadersHeight = dvgHeight - nRow * rowHeight;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        public static void ResizeAirFlowOperatingCondition(DataGridView dataGridView, int nRow = 6)
        {
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            int dvgHeight = dataGridView.Height;
            int headerHeight = dataGridView.ColumnHeadersHeight;
            int rowHeight = (dataGridView.Height - headerHeight) / (nRow);

            foreach (DataGridViewRow dataGridViewRow in dataGridView.Rows)
                dataGridViewRow.Height = rowHeight;

            dataGridView.ColumnHeadersHeight = dvgHeight - nRow * rowHeight;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        public static void ResizeO2FlowOperatingCondition(DataGridView dataGridView, int nRow = 6)
        {
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            int dvgHeight = dataGridView.Height;
            int headerHeight = dataGridView.ColumnHeadersHeight;
            int rowHeight = (dataGridView.Height - headerHeight) / (nRow);

            foreach (DataGridViewRow dataGridViewRow in dataGridView.Rows)
                dataGridViewRow.Height = rowHeight;

            dataGridView.ColumnHeadersHeight = dvgHeight - nRow * rowHeight;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }
        #endregion


        public static CGas.Composition GetCompositionOnDataGridView(DataGridView dataGridView, int iRow)
        {
            return (CGas.Composition)Enum.Parse(typeof(CGas.Composition), dataGridView[0, iRow].Value.ToString());
        }
    }
}
