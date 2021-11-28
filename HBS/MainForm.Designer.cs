
namespace HBS
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("M-Gas Calculation");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Combustion Calculation");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Combusted Gas");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Reference Properties");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Cold Blast");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Brick Configurations");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Calculation Settings");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Project<", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Simulation Results");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mnFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnNewSolution = new System.Windows.Forms.ToolStripMenuItem();
            this.mnOpenSolution = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnSaveSolution = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.mnM_GasCalculation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnCombustionCalculation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnCombustedGas = new System.Windows.Forms.ToolStripMenuItem();
            this.mnReferenceProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.mnColdBlast = new System.Windows.Forms.ToolStripMenuItem();
            this.mnBrickConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.mnCalculationSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutUsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnSendBugReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvInformation = new System.Windows.Forms.TreeView();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.panel = new System.Windows.Forms.Panel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnFile,
            this.mnFunction,
            this.simulationResultsToolStripMenuItem,
            this.mnHelp,
            this.mnDebug});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip.Size = new System.Drawing.Size(1513, 31);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // mnFile
            // 
            this.mnFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnNewSolution,
            this.mnOpenSolution,
            this.toolStripSeparator1,
            this.mnSaveSolution,
            this.toolStripSeparator2,
            this.mnExit});
            this.mnFile.Name = "mnFile";
            this.mnFile.Size = new System.Drawing.Size(64, 25);
            this.mnFile.Text = "File(&F)";
            // 
            // mnNewSolution
            // 
            this.mnNewSolution.Name = "mnNewSolution";
            this.mnNewSolution.Size = new System.Drawing.Size(202, 26);
            this.mnNewSolution.Text = "New Solution(&N)";
            this.mnNewSolution.Click += new System.EventHandler(this.menuNewSolution_Click);
            // 
            // mnOpenSolution
            // 
            this.mnOpenSolution.Name = "mnOpenSolution";
            this.mnOpenSolution.Size = new System.Drawing.Size(202, 26);
            this.mnOpenSolution.Text = "Open Solution(&O)";
            this.mnOpenSolution.Click += new System.EventHandler(this.menuOpenSolution_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
            // 
            // mnSaveSolution
            // 
            this.mnSaveSolution.Name = "mnSaveSolution";
            this.mnSaveSolution.Size = new System.Drawing.Size(202, 26);
            this.mnSaveSolution.Text = "Save Solution(&S)";
            this.mnSaveSolution.Click += new System.EventHandler(this.mnSaveSolution_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
            // 
            // mnExit
            // 
            this.mnExit.Name = "mnExit";
            this.mnExit.Size = new System.Drawing.Size(202, 26);
            this.mnExit.Text = "Exit(&X)";
            this.mnExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // mnFunction
            // 
            this.mnFunction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnM_GasCalculation,
            this.mnCombustionCalculation,
            this.mnCombustedGas,
            this.mnReferenceProperties,
            this.mnColdBlast,
            this.mnBrickConfiguration,
            this.mnCalculationSetting});
            this.mnFunction.Name = "mnFunction";
            this.mnFunction.Size = new System.Drawing.Size(104, 25);
            this.mnFunction.Text = "Function(&O)";
            // 
            // mnM_GasCalculation
            // 
            this.mnM_GasCalculation.Name = "mnM_GasCalculation";
            this.mnM_GasCalculation.Size = new System.Drawing.Size(266, 26);
            this.mnM_GasCalculation.Text = "M-Gas Calculation(&M)";
            this.mnM_GasCalculation.Click += new System.EventHandler(this.mnM_GasCalculation_Click);
            // 
            // mnCombustionCalculation
            // 
            this.mnCombustionCalculation.Name = "mnCombustionCalculation";
            this.mnCombustionCalculation.Size = new System.Drawing.Size(266, 26);
            this.mnCombustionCalculation.Text = "Combustion Calculation(&C)";
            this.mnCombustionCalculation.Click += new System.EventHandler(this.mnCombustionCalculation_Click);
            // 
            // mnCombustedGas
            // 
            this.mnCombustedGas.Name = "mnCombustedGas";
            this.mnCombustedGas.Size = new System.Drawing.Size(266, 26);
            this.mnCombustedGas.Text = "Combusted Gas(&G)";
            this.mnCombustedGas.Click += new System.EventHandler(this.mnCombustedGas_Click);
            // 
            // mnReferenceProperties
            // 
            this.mnReferenceProperties.Name = "mnReferenceProperties";
            this.mnReferenceProperties.Size = new System.Drawing.Size(266, 26);
            this.mnReferenceProperties.Text = "Reference Properties(&R)";
            this.mnReferenceProperties.Click += new System.EventHandler(this.mnReferenceProperties_Click);
            // 
            // mnColdBlast
            // 
            this.mnColdBlast.Name = "mnColdBlast";
            this.mnColdBlast.Size = new System.Drawing.Size(266, 26);
            this.mnColdBlast.Text = "Cold Blast(&O)";
            this.mnColdBlast.Click += new System.EventHandler(this.mnColdBlast_Click);
            // 
            // mnBrickConfiguration
            // 
            this.mnBrickConfiguration.Name = "mnBrickConfiguration";
            this.mnBrickConfiguration.Size = new System.Drawing.Size(266, 26);
            this.mnBrickConfiguration.Text = "Brick Configuration(&B)";
            this.mnBrickConfiguration.Click += new System.EventHandler(this.mnBrickConfiguration_Click);
            // 
            // mnCalculationSetting
            // 
            this.mnCalculationSetting.Name = "mnCalculationSetting";
            this.mnCalculationSetting.Size = new System.Drawing.Size(266, 26);
            this.mnCalculationSetting.Text = "Calculation Setting(&S)";
            this.mnCalculationSetting.Click += new System.EventHandler(this.mnCalculationSetting_Click);
            // 
            // simulationResultsToolStripMenuItem
            // 
            this.simulationResultsToolStripMenuItem.Name = "simulationResultsToolStripMenuItem";
            this.simulationResultsToolStripMenuItem.Size = new System.Drawing.Size(171, 25);
            this.simulationResultsToolStripMenuItem.Text = "Simulation Results(&R)";
            // 
            // mnHelp
            // 
            this.mnHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutUsToolStripMenuItem,
            this.toolStripSeparator3,
            this.mnSendBugReport});
            this.mnHelp.Name = "mnHelp";
            this.mnHelp.Size = new System.Drawing.Size(75, 25);
            this.mnHelp.Text = "Help(&H)";
            this.mnHelp.Click += new System.EventHandler(this.mnHelp_Click);
            // 
            // aboutUsToolStripMenuItem
            // 
            this.aboutUsToolStripMenuItem.Name = "aboutUsToolStripMenuItem";
            this.aboutUsToolStripMenuItem.Size = new System.Drawing.Size(215, 26);
            this.aboutUsToolStripMenuItem.Text = "About Us(&A)";
            this.aboutUsToolStripMenuItem.Click += new System.EventHandler(this.aboutUsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(212, 6);
            // 
            // mnSendBugReport
            // 
            this.mnSendBugReport.Name = "mnSendBugReport";
            this.mnSendBugReport.Size = new System.Drawing.Size(215, 26);
            this.mnSendBugReport.Text = "Send Bug Report(&E)";
            this.mnSendBugReport.Click += new System.EventHandler(this.mnSendBugReport_Click);
            // 
            // mnDebug
            // 
            this.mnDebug.Name = "mnDebug";
            this.mnDebug.Size = new System.Drawing.Size(68, 25);
            this.mnDebug.Text = "Debug";
            this.mnDebug.Click += new System.EventHandler(this.mnDebug_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.Location = new System.Drawing.Point(0, 31);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip.Size = new System.Drawing.Size(1513, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip.Location = new System.Drawing.Point(0, 836);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip.Size = new System.Drawing.Size(1513, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 56);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1513, 780);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvInformation);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(300, 780);
            this.splitContainer1.SplitterDistance = 307;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvInformation
            // 
            this.tvInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvInformation.Location = new System.Drawing.Point(0, 0);
            this.tvInformation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tvInformation.Name = "tvInformation";
            treeNode1.Name = "ndM_GasCalculation";
            treeNode1.Text = "M-Gas Calculation";
            treeNode2.Name = "ndCombustionCalculation";
            treeNode2.Text = "Combustion Calculation";
            treeNode3.Name = "ndCombustedGas";
            treeNode3.Text = "Combusted Gas";
            treeNode4.Name = "ndReferenceProperties";
            treeNode4.Text = "Reference Properties";
            treeNode5.Name = "ndColdBlast";
            treeNode5.Text = "Cold Blast";
            treeNode6.Name = "ndBrickConfigurations";
            treeNode6.Text = "Brick Configurations";
            treeNode7.Name = "ndCalculationSettings";
            treeNode7.Text = "Calculation Settings";
            treeNode8.Name = "ndProject";
            treeNode8.Text = "Project<";
            treeNode9.Name = "ndSimulationResults";
            treeNode9.Text = "Simulation Results";
            this.tvInformation.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9});
            this.tvInformation.Size = new System.Drawing.Size(300, 307);
            this.tvInformation.TabIndex = 2;
            this.tvInformation.Visible = false;
            this.tvInformation.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Enabled = false;
            this.propertyGrid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(300, 469);
            this.propertyGrid.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(303, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1207, 774);
            this.panel.TabIndex = 1;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.Filter = "Xml Files|*.xml|All Files|*.*";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xml";
            this.saveFileDialog.Filter = "Xml Files|*.xml|All Files|*.*";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1513, 858);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " HBS - Hot Blast Stove Simulator (";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem mnFile;
        private System.Windows.Forms.ToolStripMenuItem mnNewSolution;
        private System.Windows.Forms.ToolStripMenuItem mnOpenSolution;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnExit;
        private System.Windows.Forms.ToolStripMenuItem mnSaveSolution;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ToolStripMenuItem mnDebug;
        private System.Windows.Forms.TreeView tvInformation;
        private System.Windows.Forms.ToolStripMenuItem mnHelp;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem mnFunction;
        private System.Windows.Forms.ToolStripMenuItem mnM_GasCalculation;
        private System.Windows.Forms.ToolStripMenuItem mnCombustionCalculation;
        private System.Windows.Forms.ToolStripMenuItem mnCombustedGas;
        private System.Windows.Forms.ToolStripMenuItem mnReferenceProperties;
        private System.Windows.Forms.ToolStripMenuItem mnColdBlast;
        private System.Windows.Forms.ToolStripMenuItem mnBrickConfiguration;
        private System.Windows.Forms.ToolStripMenuItem mnCalculationSetting;
        private System.Windows.Forms.ToolStripMenuItem simulationResultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutUsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnSendBugReport;
    }
}