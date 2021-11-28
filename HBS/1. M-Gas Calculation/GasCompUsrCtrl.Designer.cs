
namespace HBS
{
    partial class GasCompUsrCtrl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblUnit = new System.Windows.Forms.Label();
            this.dgvGasCompositions = new System.Windows.Forms.DataGridView();
            this.ColumnSpecies0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMolarMass0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSum = new System.Windows.Forms.DataGridView();
            this.ColumnSpecies1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMolarMass1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rbMassFraction = new System.Windows.Forms.RadioButton();
            this.rbMoleFraction = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGasCompositions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSum)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Comma-separated values  files (*.csv)|*.csv|All files (*.*)|*.*";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Comma-separated values  files (*.csv)|*.csv|All files (*.*)|*.*";
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblUnit.Location = new System.Drawing.Point(840, 0);
            this.lblUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(60, 20);
            this.lblUnit.TabIndex = 2;
            this.lblUnit.Text = "Unit: %";
            // 
            // dgvGasCompositions
            // 
            this.dgvGasCompositions.AllowUserToAddRows = false;
            this.dgvGasCompositions.AllowUserToDeleteRows = false;
            this.dgvGasCompositions.AllowUserToResizeColumns = false;
            this.dgvGasCompositions.AllowUserToResizeRows = false;
            this.dgvGasCompositions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGasCompositions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGasCompositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGasCompositions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSpecies0,
            this.ColumnMolarMass0});
            this.dgvGasCompositions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGasCompositions.Location = new System.Drawing.Point(0, 50);
            this.dgvGasCompositions.Margin = new System.Windows.Forms.Padding(0);
            this.dgvGasCompositions.MultiSelect = false;
            this.dgvGasCompositions.Name = "dgvGasCompositions";
            this.dgvGasCompositions.RowHeadersVisible = false;
            this.dgvGasCompositions.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvGasCompositions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvGasCompositions.Size = new System.Drawing.Size(900, 400);
            this.dgvGasCompositions.TabIndex = 0;
            this.dgvGasCompositions.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvGasCompositions_CellBeginEdit);
            this.dgvGasCompositions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGasCompositions_CellClick);
            this.dgvGasCompositions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGasCompositions_CellDoubleClick);
            this.dgvGasCompositions.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGasCompositions_CellEndEdit);
            this.dgvGasCompositions.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGasCompositions_CellValidated);
            this.dgvGasCompositions.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvGasCompositions_CellValidating);
            this.dgvGasCompositions.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvGasCompositions_ColumnAdded);
            this.dgvGasCompositions.SelectionChanged += new System.EventHandler(this.dgvGasCompositions_SelectionChanged);
            this.dgvGasCompositions.Leave += new System.EventHandler(this.dgvGasCompositions_Leave);
            this.dgvGasCompositions.Resize += new System.EventHandler(this.dgvGasCompositions_Resize);
            // 
            // ColumnSpecies0
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            this.ColumnSpecies0.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnSpecies0.HeaderText = "Species";
            this.ColumnSpecies0.Name = "ColumnSpecies0";
            this.ColumnSpecies0.ReadOnly = true;
            // 
            // ColumnMolarMass0
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            this.ColumnMolarMass0.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnMolarMass0.HeaderText = "Molar Mass [g/mol]";
            this.ColumnMolarMass0.Name = "ColumnMolarMass0";
            this.ColumnMolarMass0.ReadOnly = true;
            // 
            // dgvSum
            // 
            this.dgvSum.AllowUserToAddRows = false;
            this.dgvSum.AllowUserToDeleteRows = false;
            this.dgvSum.AllowUserToResizeColumns = false;
            this.dgvSum.AllowUserToResizeRows = false;
            this.dgvSum.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSum.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSum.ColumnHeadersVisible = false;
            this.dgvSum.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSpecies1,
            this.ColumnMolarMass1});
            this.dgvSum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSum.Location = new System.Drawing.Point(0, 450);
            this.dgvSum.Margin = new System.Windows.Forms.Padding(0);
            this.dgvSum.Name = "dgvSum";
            this.dgvSum.ReadOnly = true;
            this.dgvSum.RowHeadersVisible = false;
            this.dgvSum.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvSum.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSum.Size = new System.Drawing.Size(900, 35);
            this.dgvSum.TabIndex = 1;
            this.dgvSum.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSum_CellClick);
            this.dgvSum.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSum_CellDoubleClick);
            this.dgvSum.Resize += new System.EventHandler(this.dgvSum_Resize);
            // 
            // ColumnSpecies1
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            this.ColumnSpecies1.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnSpecies1.HeaderText = "Species";
            this.ColumnSpecies1.Name = "ColumnSpecies1";
            this.ColumnSpecies1.ReadOnly = true;
            // 
            // ColumnMolarMass1
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            this.ColumnMolarMass1.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColumnMolarMass1.HeaderText = "Molar Mass";
            this.ColumnMolarMass1.Name = "ColumnMolarMass1";
            this.ColumnMolarMass1.ReadOnly = true;
            // 
            // rbMassFraction
            // 
            this.rbMassFraction.AutoSize = true;
            this.rbMassFraction.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbMassFraction.Location = new System.Drawing.Point(713, 0);
            this.rbMassFraction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbMassFraction.Name = "rbMassFraction";
            this.rbMassFraction.Size = new System.Drawing.Size(127, 25);
            this.rbMassFraction.TabIndex = 1;
            this.rbMassFraction.Text = "Mass Fraction";
            this.rbMassFraction.UseVisualStyleBackColor = true;
            this.rbMassFraction.CheckedChanged += new System.EventHandler(this.rbMassFraction_CheckedChanged);
            // 
            // rbMoleFraction
            // 
            this.rbMoleFraction.AutoSize = true;
            this.rbMoleFraction.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbMoleFraction.Location = new System.Drawing.Point(590, 0);
            this.rbMoleFraction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbMoleFraction.Name = "rbMoleFraction";
            this.rbMoleFraction.Size = new System.Drawing.Size(123, 25);
            this.rbMoleFraction.TabIndex = 0;
            this.rbMoleFraction.Text = "Mole Fraction";
            this.rbMoleFraction.UseVisualStyleBackColor = true;
            this.rbMoleFraction.CheckedChanged += new System.EventHandler(this.rbMoleFraction_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvGasCompositions, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgvSum, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(900, 485);
            this.tableLayoutPanel1.TabIndex = 36;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 50);
            this.panel1.TabIndex = 36;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(900, 50);
            this.tableLayoutPanel2.TabIndex = 34;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbMoleFraction);
            this.panel3.Controls.Add(this.rbMassFraction);
            this.panel3.Controls.Add(this.lblUnit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 25);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(900, 25);
            this.panel3.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(900, 25);
            this.panel2.TabIndex = 0;
            // 
            // GasCompUsrCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "GasCompUsrCtrl";
            this.Size = new System.Drawing.Size(900, 485);
            this.Load += new System.EventHandler(this.GasCompUsrCtrl_Load);
            this.Resize += new System.EventHandler(this.GasCompUsrCtrl_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGasCompositions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSum)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.DataGridView dgvGasCompositions;
        private System.Windows.Forms.DataGridView dgvSum;
        private System.Windows.Forms.RadioButton rbMassFraction;
        private System.Windows.Forms.RadioButton rbMoleFraction;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSpecies0;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMolarMass0;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSpecies1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMolarMass1;
    }
}
