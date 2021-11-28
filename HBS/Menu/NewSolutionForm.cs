using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HBS
{
    public partial class NewSolutionForm : Form
    {
        private string _solutionName = string.Empty;
        public string SolutionName
        {
            get
            {
                return _solutionName;
            }
        }

        public NewSolutionForm()
        {
            InitializeComponent();
        }

        private void NewForm_Load(object sender, EventArgs e)
        {
            string yy = DateTime.Now.ToString("yy");
            string MM = DateTime.Now.ToString("MM");
            string dd = DateTime.Now.ToString("dd");
            txtSolutionName.Text += yy + MM + dd;
        }

        private void txtSolutionName_Validating(object sender, CancelEventArgs e)
        {
            string content = ((TextBox)sender).Text.Trim().Replace(" ", string.Empty);
            double d = 0.0;

            if (content == string.Empty)
            {
                Global.ShowErrorMsgBox("The \"Solution name\" is null.");
                e.Cancel = true;
                return;
            }

            
            if (double.TryParse(content.Substring(0, 1), out d))
            {
                Global.ShowErrorMsgBox("The first character can NOT have a number.");
                e.Cancel = true;
                return;
            }

            var regexItem = new Regex("^[a-zA-Z0-9_]*$");
            foreach(char c in content)
            {
                if (!regexItem.IsMatch(c.ToString()))
                {
                    Global.ShowErrorMsgBox("Special character \"" + c.ToString() + "\" is NOT allowed.");
                    e.Cancel = true;
                    return;
                }
            }
        }

        

        private void txtSolutionName_Validated(object sender, EventArgs e)
        {
            _solutionName = ((TextBox)sender).Text.Trim();
        }
    }
}
