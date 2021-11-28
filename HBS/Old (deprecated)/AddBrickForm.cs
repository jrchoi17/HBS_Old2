using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HBS
{
    public partial class AddBrickForm : Form
    {
        public string SelectedBrick { get; set; }

        public AddBrickForm()
        {
            InitializeComponent();
        }

        public void AddBrickForm_Load(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath + "\\Brick DB");
            List<FileInfo> fileInfos = directoryInfo.GetFiles("*.prop").ToList();

            for (int i = 0; i < fileInfos.Count; i++)
                lbBricks.Items.Add(Path.GetFileNameWithoutExtension(fileInfos[i].Name));
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lbBricks.SelectedIndex < 0)
            {
                MessageBox.Show("Please select one brick at least.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SelectedBrick = lbBricks.Items[lbBricks.SelectedIndex].ToString();
                DialogResult = DialogResult.OK;
            }
            
           
        }
    }
}
