using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = CreateDynamicDataSet();
            ds.WriteXml(@"C:\Users\yongcheol\source\repos\HBS\Forge\data.xml");
        }

        private DataSet CreateDynamicDataSet()
        {

            DataSet ds = new DataSet("DS");
            ds.Namespace = "StdNamespace";
            DataTable stdTable = new DataTable("Student");
            DataColumn col1 = new DataColumn("Name");
            DataColumn col2 = new DataColumn("Address");
            stdTable.Columns.Add(col1);
            stdTable.Columns.Add(col2);
            ds.Tables.Add(stdTable);

            //Add student Data to the table  
            DataRow newRow; newRow = stdTable.NewRow();
            newRow["Name"] = "Mahesh Chand";
            newRow["Address"] = "Meadowlake Dr, Dtown";
            stdTable.Rows.Add(newRow);
            newRow = stdTable.NewRow();

            newRow["Name"] = "Mike Gold";
            newRow["Address"] = "NewYork";
            stdTable.Rows.Add(newRow);
            newRow = stdTable.NewRow();
            newRow["Name"] = "Mike Gold";
            newRow["Address"] = "New York";

            stdTable.Rows.Add(newRow);
            ds.AcceptChanges();
            return ds;

        }
    }
}
