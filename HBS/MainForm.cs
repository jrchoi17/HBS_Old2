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
using HBS_Solver;
using System.IO;
using System.Reflection;

namespace HBS
{
    public partial class MainForm : Form
    {
        private M_GasCalculationUsrCtrl _m_GasCalculationUsrCtrl;
        private CombustionCalculationUsrCtrl _combustionCalculationUsrCtrl;
        private CombustedGasUsrCtrl _combustedGasUsrCtrl;
        private ReferencePropertiesUsrCtrl _refPropertiesUsrCtrl = new ReferencePropertiesUsrCtrl();
        private ColdBlastUsrCtrl _coldBlastUsrCtrl;
        private BrickConfigurationUsrCtrl _brickConfigurationUsrCtrl;
        private CalculationSettingUsrCtrl _calculationSettingUsrCtrl;
        private PostProcessingUsrCtrl _postProcessingUsrCtrl;
        
        public MainForm()
        {
            InitializeComponent();

            _m_GasCalculationUsrCtrl = new M_GasCalculationUsrCtrl(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);
            _combustionCalculationUsrCtrl = new CombustionCalculationUsrCtrl();
            _combustedGasUsrCtrl = new CombustedGasUsrCtrl();
            _coldBlastUsrCtrl = new ColdBlastUsrCtrl();
            _brickConfigurationUsrCtrl = new BrickConfigurationUsrCtrl();
            _calculationSettingUsrCtrl = new CalculationSettingUsrCtrl();
            _postProcessingUsrCtrl = new PostProcessingUsrCtrl();
#if (DEBUG)
            mnDebug.Visible = true;
#else
            mnDebug.Visible = false;
#endif
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // ud setting
            ST_UD ud = ST_UD.GetInstance();

            // set user controls
            ud.PropertyGrid = propertyGrid;

            // set data from default ud file
            //ud.M_GasCalculation = new ST_UD.M_GasCalculationDataType(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);
            ud.CombustionCalculation = new ST_UD.CombustionCalculationDataType(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);
            ud.CombustedGas = new ST_UD.CombustedGasDataType(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);
            ud.ColdBlast = new ST_UD.ColdBlastDataType(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);
            ud.BrickConfiguration = new ST_UD.BrickConfigurationDataType(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);
            ud.CalculationSetting = new ST_UD.CalculationSettingDataType(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);

            // add controls to panel
            panel.Controls.Add(_m_GasCalculationUsrCtrl);
            panel.Controls.Add(_combustionCalculationUsrCtrl);
            panel.Controls.Add(_combustedGasUsrCtrl);
            panel.Controls.Add(_refPropertiesUsrCtrl);
            panel.Controls.Add(_coldBlastUsrCtrl);
            panel.Controls.Add(_brickConfigurationUsrCtrl);
            panel.Controls.Add(_calculationSettingUsrCtrl);

            SetSubControlsHidden(panel);

            // set version on the top.
            Text += Application.ProductVersion + ")";

            // set split container 50%, 50%.
            splitContainer1.SplitterDistance = (int)(splitContainer1.Height / 2.0);

            tvInformation.Visible = true;
            string date = DateTime.Now.ToString("yyMMdd");
            tvInformation.Nodes[0].Text += "defalut_" + date + ">";
            tvInformation.ExpandAll();

#if (DEBUG)
            tvInformation.SelectedNode = tvInformation.Nodes["ndSimulationResults"];
            tvInformation.SelectedNode = tvInformation.Nodes[0];

#endif
        }

        #region Menu event methods
        #region Menu event methods::Menu File
        // File > New Solution
        private void menuNewSolution_Click(object sender, EventArgs e)
        {
            NewSolutionForm form = new NewSolutionForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                tvInformation.Visible = true;
                tvInformation.Nodes[0].Text = "Project<" + form.SolutionName + ">";

                _m_GasCalculationUsrCtrl.SetDataFromFile(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);
                _combustionCalculationUsrCtrl.SetDataFromFile(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);
                _combustedGasUsrCtrl.SetDataFromFile(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);
                _coldBlastUsrCtrl.SetDataFromFile(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);
                _brickConfigurationUsrCtrl.SetDataFromFile(Application.StartupPath + Properties.Settings.Default.FILEPATH_DEFAULT_UD_XML);

                tvInformation.ExpandAll();
            }
        }

        // File > Open Solution
        private void menuOpenSolution_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                _m_GasCalculationUsrCtrl.SetDataFromFile(filePath);
                _combustionCalculationUsrCtrl.SetDataFromFile(filePath);
                _combustedGasUsrCtrl.SetDataFromFile(filePath);
                _coldBlastUsrCtrl.SetDataFromFile(filePath);
                _brickConfigurationUsrCtrl.SetDataFromFile(filePath);
            }
        }

        // File > Save Solution
        private void mnSaveSolution_Click(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                List<string> allContents = new List<string>();

                allContents.Add(@"<?xml version=""1.0"" encoding=""utf-8""?>");
                allContents.Add(@"<HBS_v1.1.0.3>");

                // contents of MGasCalculation
                allContents.Add("  <MGasCalculation>");
                List<string> MGasCalculationContent = ud.M_GasCalculation.GetMGasDataToList();
                for (int i = 0; i < MGasCalculationContent.Count; i++)
                    allContents.Add(MGasCalculationContent[i]);
                allContents.Add("  </MGasCalculation>");

                //contents of CombustionCalculation
                allContents.Add("  <CombustionCalculation>");
                List<string> CombustionCalculationContent = ud.CombustionCalculation.GetCombustionCalculationDataToList();
                for (int i = 0; i < CombustionCalculationContent.Count; i++)
                    allContents.Add(CombustionCalculationContent[i]);
                allContents.Add("  </CombustionCalculation>");

                //contents of CombustedGas
                allContents.Add("  <CombustedGas>");
                List<string> CombustedGasContent = ud.CombustedGas.GetCombustedGasDataToList();
                for (int i = 0; i < CombustedGasContent.Count; i++)
                    allContents.Add(CombustedGasContent[i]);
                allContents.Add("  </CombustedGas>");

                //contents of ColdBlast
                allContents.Add("  <ColdBlast>");
                List<string> ColdBlastContent = ud.ColdBlast.GetColdBlastDataToList();
                for (int i = 0; i < ColdBlastContent.Count; i++)
                    allContents.Add(ColdBlastContent[i]);
                allContents.Add("  </ColdBlast>");

                //contents of BrickConfiguration
                allContents.Add(@"  <BrickConfiguration>");
                List<string> BrickConfigurationContent = ud.BrickConfiguration.GetBrickConfigurationDataToList();
                for (int i = 0; i < BrickConfigurationContent.Count; i++)
                    allContents.Add(BrickConfigurationContent[i]);
                allContents.Add(@"  </BrickConfiguration>");

                allContents.Add(@"</HBS_v1.1.0.3>");

                File.WriteAllLines(filePath, allContents.ToArray());
            }
        }

        // File > Exit
        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Menu event method::Function
        // Function > M-Gas Calculation
        private void mnM_GasCalculation_Click(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            SetSubControlsHidden(panel);

            _m_GasCalculationUsrCtrl.Visible = true;
            _m_GasCalculationUsrCtrl.Dock = DockStyle.Fill;

            propertyGrid.SelectedObject = ud.M_GasCalculation;
            tvInformation.SelectedNode = tvInformation.Nodes[0].Nodes["ndM_GasCalculation"];
        }

        // Function > Combustion Calculation
        private void mnCombustionCalculation_Click(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            SetSubControlsHidden(panel);

            _combustionCalculationUsrCtrl.Visible = true;
            _combustionCalculationUsrCtrl.Dock = DockStyle.Fill;

            propertyGrid.SelectedObject = ud.CombustionCalculation;
            tvInformation.SelectedNode = tvInformation.Nodes[0].Nodes["ndCombustionCalculation"];
        }

        // Function > Combusted Gas
        private void mnCombustedGas_Click(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            SetSubControlsHidden(panel);

            _combustedGasUsrCtrl.Visible = true;
            _combustedGasUsrCtrl.Dock = DockStyle.Fill;

            propertyGrid.SelectedObject = ud.CombustedGas;
            tvInformation.SelectedNode = tvInformation.Nodes[0].Nodes["ndCombustedGas"];
        }

        // Function > Reference Properties
        private void mnReferenceProperties_Click(object sender, EventArgs e)
        {
            SetSubControlsHidden(panel);

            _refPropertiesUsrCtrl.Visible = true;
            _refPropertiesUsrCtrl.Dock = DockStyle.Fill;

            propertyGrid.SelectedObject = null;
            tvInformation.SelectedNode = tvInformation.Nodes[0].Nodes["ndReferenceProperties"];
        }

        // Function > Cold Blast
        private void mnColdBlast_Click(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            SetSubControlsHidden(panel);

            _coldBlastUsrCtrl.Visible = true;
            _coldBlastUsrCtrl.Dock = DockStyle.Fill;

            propertyGrid.SelectedObject = ud.ColdBlast;
            tvInformation.SelectedNode = tvInformation.Nodes[0].Nodes["ndColdBlast"];
        }

        // Function > Brick Configuration
        private void mnBrickConfiguration_Click(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();
            SetSubControlsHidden(panel);

            _brickConfigurationUsrCtrl.Visible = true;
            _brickConfigurationUsrCtrl.Dock = DockStyle.Fill;

            propertyGrid.SelectedObject = ud.BrickConfiguration;
            tvInformation.SelectedNode = tvInformation.Nodes[0].Nodes["ndBrickConfigurations"];
        }

        // Function > Caculation Setting
        private void mnCalculationSetting_Click(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();
            SetSubControlsHidden(panel);

            _calculationSettingUsrCtrl.Visible = true;
            _calculationSettingUsrCtrl.Dock = DockStyle.Fill;

            propertyGrid.SelectedObject = ud.CalculationSetting;
            tvInformation.SelectedNode = tvInformation.Nodes[0].Nodes["ndCalculationSettings"];
        }
        #endregion

        #region Menu event methods::Debug
        private void mnDebug_Click(object sender, EventArgs e)
        {
            ST_UD ud = ST_UD.GetInstance();

            // noting do to
        }
        #endregion

        #region Menu evenrt methods::Help
        private void mnHelp_Click(object sender, EventArgs e)
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\HelpV2";
            string helpLocation = Path.Combine(executableLocation, "HBS.chm");
            Help.ShowHelp(this, "File://" + helpLocation);
        }

        private void mnSendBugReport_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(String.Format("mailto:{0}?subject={1}&cc={2}&body={3}",
  @"jrchoi17@sogang.ac.kr", "Bug report (" + DateTime.Now.ToString("yyMMdd") + ")", "ycchoi@sogang.ac.kr", "To developers," + Environment.NewLine + Environment.NewLine)) ;
        }
        #endregion
        #endregion

        #region Control event methods
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView treeview = (TreeView)sender;
            TreeNode project = tvInformation.Nodes[0];

            ST_UD ud = ST_UD.GetInstance();

            if (e.Node == null)
            {
                foreach (Control control in panel.Controls)
                {
                    control.Dock = DockStyle.None;
                    control.Visible = false;
                }
            }
            else if (e.Node.Equals(treeview.Nodes[0])) { SetSubControlsHidden(panel); }
            else if (e.Node.Equals(project.Nodes["ndM_GasCalculation"])) { mnM_GasCalculation.PerformClick(); }
            else if (e.Node.Equals(project.Nodes["ndCombustionCalculation"])) { mnCombustionCalculation.PerformClick(); }
            else if (e.Node.Equals(project.Nodes["ndCombustedGas"])) { mnCombustedGas.PerformClick(); }
            else if (e.Node.Equals(project.Nodes["ndReferenceProperties"])) { mnReferenceProperties.PerformClick(); }
            else if (e.Node.Equals(project.Nodes["ndColdBlast"])) { mnColdBlast.PerformClick(); }
            else if (e.Node.Equals(project.Nodes["ndBrickConfigurations"])) { mnBrickConfiguration.PerformClick(); }
            else if (e.Node.Equals(project.Nodes["ndCalculationSettings"])) { mnCalculationSetting.PerformClick(); }

            else { SetSubControlsHidden(panel); }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
#if (!DEBUG)
            DialogResult dialogResult = MessageBox.Show("Do you want to save this setting as a file?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            
            if (dialogResult == DialogResult.Yes)
                mnSaveSolution.PerformClick();
            else if (dialogResult == DialogResult.Cancel)
                e.Cancel = true;
#endif
        }
#endregion


#region Private methods
        private void SetSubControlsHidden(Control control)
        {
            foreach (Control sub in control.Controls)
            {
                //sub.Dock = DockStyle.None;
                sub.Visible = false;
            }
        }
        #endregion


    }
}
