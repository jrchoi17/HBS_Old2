
namespace HBS
{
    partial class M_GasCalculationUsrCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCOG = new System.Windows.Forms.Label();
            this.lblXGas = new System.Windows.Forms.Label();
            this.lblBFG = new System.Windows.Forms.Label();
            this.txtBFG = new System.Windows.Forms.TextBox();
            this.txtCOG = new System.Windows.Forms.TextBox();
            this.txtXGas = new System.Windows.Forms.TextBox();
            this.btnCalculation = new System.Windows.Forms.Button();
            this.btnExportCsvFile = new System.Windows.Forms.Button();
            this.btnLoadDefault = new System.Windows.Forms.Button();
            this.btnNormalizing = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnImportCSVFile = new System.Windows.Forms.Button();
            this.gasCompUsrCtrl = new HBS.GasCompUsrCtrl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.doughnutCharUsrCtrl4 = new HBS.DoughnutCharUsrCtrl();
            this.doughnutCharUsrCtrl2 = new HBS.DoughnutCharUsrCtrl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbMixingMoleRatio = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.doughnutCharUsrCtrl3 = new HBS.DoughnutCharUsrCtrl();
            this.doughnutCharUsrCtrl1 = new HBS.DoughnutCharUsrCtrl();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbMixingMoleRatio.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCOG
            // 
            this.lblCOG.AutoSize = true;
            this.lblCOG.Location = new System.Drawing.Point(24, 66);
            this.lblCOG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCOG.Name = "lblCOG";
            this.lblCOG.Size = new System.Drawing.Size(45, 20);
            this.lblCOG.TabIndex = 11;
            this.lblCOG.Text = "COG";
            // 
            // lblXGas
            // 
            this.lblXGas.AutoSize = true;
            this.lblXGas.Location = new System.Drawing.Point(24, 102);
            this.lblXGas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblXGas.Name = "lblXGas";
            this.lblXGas.Size = new System.Drawing.Size(55, 20);
            this.lblXGas.TabIndex = 13;
            this.lblXGas.Text = "X-Gas";
            // 
            // lblBFG
            // 
            this.lblBFG.AutoSize = true;
            this.lblBFG.Location = new System.Drawing.Point(24, 30);
            this.lblBFG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBFG.Name = "lblBFG";
            this.lblBFG.Size = new System.Drawing.Size(43, 20);
            this.lblBFG.TabIndex = 9;
            this.lblBFG.Text = "BFG";
            // 
            // txtBFG
            // 
            this.txtBFG.Location = new System.Drawing.Point(748, 27);
            this.txtBFG.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBFG.Name = "txtBFG";
            this.txtBFG.Size = new System.Drawing.Size(110, 26);
            this.txtBFG.TabIndex = 10;
            this.txtBFG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBFG.Validating += new System.ComponentModel.CancelEventHandler(this.txtGasN_Validating);
            this.txtBFG.Validated += new System.EventHandler(this.txtBFG_Validated);
            // 
            // txtCOG
            // 
            this.txtCOG.Location = new System.Drawing.Point(748, 63);
            this.txtCOG.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCOG.Name = "txtCOG";
            this.txtCOG.Size = new System.Drawing.Size(110, 26);
            this.txtCOG.TabIndex = 12;
            this.txtCOG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCOG.Validating += new System.ComponentModel.CancelEventHandler(this.txtGasN_Validating);
            this.txtCOG.Validated += new System.EventHandler(this.txtCOG_Validated);
            // 
            // txtXGas
            // 
            this.txtXGas.Location = new System.Drawing.Point(748, 99);
            this.txtXGas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtXGas.Name = "txtXGas";
            this.txtXGas.Size = new System.Drawing.Size(110, 26);
            this.txtXGas.TabIndex = 14;
            this.txtXGas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtXGas.Validating += new System.ComponentModel.CancelEventHandler(this.txtGasN_Validating);
            this.txtXGas.Validated += new System.EventHandler(this.txtXGas_Validated);
            // 
            // btnCalculation
            // 
            this.btnCalculation.Location = new System.Drawing.Point(732, 204);
            this.btnCalculation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCalculation.Name = "btnCalculation";
            this.btnCalculation.Size = new System.Drawing.Size(150, 35);
            this.btnCalculation.TabIndex = 18;
            this.btnCalculation.Text = "Calculation";
            this.btnCalculation.UseVisualStyleBackColor = true;
            this.btnCalculation.Click += new System.EventHandler(this.btnCalculation_Click);
            // 
            // btnExportCsvFile
            // 
            this.btnExportCsvFile.Location = new System.Drawing.Point(175, 204);
            this.btnExportCsvFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExportCsvFile.Name = "btnExportCsvFile";
            this.btnExportCsvFile.Size = new System.Drawing.Size(150, 35);
            this.btnExportCsvFile.TabIndex = 17;
            this.btnExportCsvFile.Text = "Export CSV File";
            this.btnExportCsvFile.UseVisualStyleBackColor = true;
            this.btnExportCsvFile.Click += new System.EventHandler(this.btnExportCsvFile_Click);
            // 
            // btnLoadDefault
            // 
            this.btnLoadDefault.Location = new System.Drawing.Point(17, 159);
            this.btnLoadDefault.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLoadDefault.Name = "btnLoadDefault";
            this.btnLoadDefault.Size = new System.Drawing.Size(150, 35);
            this.btnLoadDefault.TabIndex = 15;
            this.btnLoadDefault.Text = "Load Default";
            this.btnLoadDefault.UseVisualStyleBackColor = true;
            this.btnLoadDefault.Click += new System.EventHandler(this.btnLoadDefault_Click);
            // 
            // btnNormalizing
            // 
            this.btnNormalizing.Location = new System.Drawing.Point(732, 159);
            this.btnNormalizing.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNormalizing.Name = "btnNormalizing";
            this.btnNormalizing.Size = new System.Drawing.Size(150, 35);
            this.btnNormalizing.TabIndex = 18;
            this.btnNormalizing.Text = "Normalizing";
            this.btnNormalizing.UseVisualStyleBackColor = true;
            this.btnNormalizing.Click += new System.EventHandler(this.btnNormalizing_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "gas";
            this.saveFileDialog.Filter = "Comma-separated values  files (*.csv)|*.csv|All files (*.*)|*.*";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "csv";
            this.openFileDialog.Filter = "Comma-separated values  files (*.csv)|*.csv|All files (*.*)|*.*";
            // 
            // btnImportCSVFile
            // 
            this.btnImportCSVFile.Location = new System.Drawing.Point(175, 159);
            this.btnImportCSVFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImportCSVFile.Name = "btnImportCSVFile";
            this.btnImportCSVFile.Size = new System.Drawing.Size(150, 35);
            this.btnImportCSVFile.TabIndex = 16;
            this.btnImportCSVFile.Text = "Import CSV File";
            this.btnImportCSVFile.UseVisualStyleBackColor = true;
            this.btnImportCSVFile.Click += new System.EventHandler(this.btnImportCSVFile_Click);
            // 
            // gasCompUsrCtrl
            // 
            this.gasCompUsrCtrl.BackColor = System.Drawing.Color.White;
            this.gasCompUsrCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gasCompUsrCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gasCompUsrCtrl.Gas0 = null;
            this.gasCompUsrCtrl.Gas0Name = null;
            this.gasCompUsrCtrl.Gas1 = null;
            this.gasCompUsrCtrl.Gas10 = null;
            this.gasCompUsrCtrl.Gas10Name = null;
            this.gasCompUsrCtrl.Gas1Name = null;
            this.gasCompUsrCtrl.Gas2 = null;
            this.gasCompUsrCtrl.Gas2Name = null;
            this.gasCompUsrCtrl.Location = new System.Drawing.Point(0, 0);
            this.gasCompUsrCtrl.Margin = new System.Windows.Forms.Padding(0);
            this.gasCompUsrCtrl.Name = "gasCompUsrCtrl";
            this.gasCompUsrCtrl.Size = new System.Drawing.Size(900, 490);
            this.gasCompUsrCtrl.TabIndex = 0;
            this.gasCompUsrCtrl.Type = HBS.GasCompUsrCtrl.FractionType.Mole;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 900F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1650, 980);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.doughnutCharUsrCtrl4, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.doughnutCharUsrCtrl2, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(1275, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(375, 980);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // doughnutCharUsrCtrl4
            // 
            this.doughnutCharUsrCtrl4.BackColor = System.Drawing.Color.White;
            this.doughnutCharUsrCtrl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doughnutCharUsrCtrl4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doughnutCharUsrCtrl4.Fraction = null;
            this.doughnutCharUsrCtrl4.Location = new System.Drawing.Point(0, 490);
            this.doughnutCharUsrCtrl4.Margin = new System.Windows.Forms.Padding(0);
            this.doughnutCharUsrCtrl4.Name = "doughnutCharUsrCtrl4";
            this.doughnutCharUsrCtrl4.Size = new System.Drawing.Size(375, 490);
            this.doughnutCharUsrCtrl4.TabIndex = 2;
            this.doughnutCharUsrCtrl4.ThresholdValue = 0D;
            // 
            // doughnutCharUsrCtrl2
            // 
            this.doughnutCharUsrCtrl2.BackColor = System.Drawing.Color.White;
            this.doughnutCharUsrCtrl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doughnutCharUsrCtrl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doughnutCharUsrCtrl2.Fraction = null;
            this.doughnutCharUsrCtrl2.Location = new System.Drawing.Point(0, 0);
            this.doughnutCharUsrCtrl2.Margin = new System.Windows.Forms.Padding(0);
            this.doughnutCharUsrCtrl2.Name = "doughnutCharUsrCtrl2";
            this.doughnutCharUsrCtrl2.Size = new System.Drawing.Size(375, 490);
            this.doughnutCharUsrCtrl2.TabIndex = 1;
            this.doughnutCharUsrCtrl2.ThresholdValue = 0D;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.gasCompUsrCtrl, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(900, 980);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbMixingMoleRatio);
            this.panel1.Controls.Add(this.btnLoadDefault);
            this.panel1.Controls.Add(this.btnCalculation);
            this.panel1.Controls.Add(this.btnNormalizing);
            this.panel1.Controls.Add(this.btnExportCsvFile);
            this.panel1.Controls.Add(this.btnImportCSVFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 490);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 490);
            this.panel1.TabIndex = 1;
            // 
            // gbMixingMoleRatio
            // 
            this.gbMixingMoleRatio.Controls.Add(this.lblBFG);
            this.gbMixingMoleRatio.Controls.Add(this.txtBFG);
            this.gbMixingMoleRatio.Controls.Add(this.txtXGas);
            this.gbMixingMoleRatio.Controls.Add(this.txtCOG);
            this.gbMixingMoleRatio.Controls.Add(this.lblCOG);
            this.gbMixingMoleRatio.Controls.Add(this.lblXGas);
            this.gbMixingMoleRatio.Location = new System.Drawing.Point(17, 13);
            this.gbMixingMoleRatio.Name = "gbMixingMoleRatio";
            this.gbMixingMoleRatio.Size = new System.Drawing.Size(865, 138);
            this.gbMixingMoleRatio.TabIndex = 4;
            this.gbMixingMoleRatio.TabStop = false;
            this.gbMixingMoleRatio.Text = "Mixing Mole Ratio";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.doughnutCharUsrCtrl3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.doughnutCharUsrCtrl1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(900, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(375, 980);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // doughnutCharUsrCtrl3
            // 
            this.doughnutCharUsrCtrl3.BackColor = System.Drawing.Color.White;
            this.doughnutCharUsrCtrl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doughnutCharUsrCtrl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doughnutCharUsrCtrl3.Fraction = null;
            this.doughnutCharUsrCtrl3.Location = new System.Drawing.Point(0, 490);
            this.doughnutCharUsrCtrl3.Margin = new System.Windows.Forms.Padding(0);
            this.doughnutCharUsrCtrl3.Name = "doughnutCharUsrCtrl3";
            this.doughnutCharUsrCtrl3.Size = new System.Drawing.Size(375, 490);
            this.doughnutCharUsrCtrl3.TabIndex = 1;
            this.doughnutCharUsrCtrl3.ThresholdValue = 0D;
            // 
            // doughnutCharUsrCtrl1
            // 
            this.doughnutCharUsrCtrl1.BackColor = System.Drawing.Color.White;
            this.doughnutCharUsrCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doughnutCharUsrCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doughnutCharUsrCtrl1.Fraction = null;
            this.doughnutCharUsrCtrl1.Location = new System.Drawing.Point(0, 0);
            this.doughnutCharUsrCtrl1.Margin = new System.Windows.Forms.Padding(0);
            this.doughnutCharUsrCtrl1.Name = "doughnutCharUsrCtrl1";
            this.doughnutCharUsrCtrl1.Size = new System.Drawing.Size(375, 490);
            this.doughnutCharUsrCtrl1.TabIndex = 0;
            this.doughnutCharUsrCtrl1.ThresholdValue = 0D;
            // 
            // M_GasCalculationUsrCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "M_GasCalculationUsrCtrl";
            this.Size = new System.Drawing.Size(1650, 980);
            this.Load += new System.EventHandler(this.M_GasCalculationUsrCtrl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.gbMixingMoleRatio.ResumeLayout(false);
            this.gbMixingMoleRatio.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblCOG;
        private System.Windows.Forms.Label lblXGas;
        private System.Windows.Forms.Label lblBFG;
        private System.Windows.Forms.TextBox txtBFG;
        private System.Windows.Forms.TextBox txtCOG;
        private System.Windows.Forms.TextBox txtXGas;
        private System.Windows.Forms.Button btnCalculation;
        private System.Windows.Forms.Button btnExportCsvFile;
        private System.Windows.Forms.Button btnLoadDefault;
        private System.Windows.Forms.Button btnNormalizing;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private GasCompUsrCtrl gasCompUsrCtrl;
        private System.Windows.Forms.Button btnImportCSVFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private DoughnutCharUsrCtrl doughnutCharUsrCtrl4;
        private DoughnutCharUsrCtrl doughnutCharUsrCtrl2;
        private DoughnutCharUsrCtrl doughnutCharUsrCtrl3;
        private DoughnutCharUsrCtrl doughnutCharUsrCtrl1;
        private System.Windows.Forms.GroupBox gbMixingMoleRatio;
    }
}
