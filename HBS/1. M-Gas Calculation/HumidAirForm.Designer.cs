
namespace HBS
{
    partial class HumidAirForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HumidAirForm));
            this.dgvGasCompositions = new System.Windows.Forms.DataGridView();
            this.ColumnSpecies0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMolarMass0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDryAir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnHumidAir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTemperature = new System.Windows.Forms.Label();
            this.txtTemperature = new System.Windows.Forms.TextBox();
            this.lblRelativeHumidity = new System.Windows.Forms.Label();
            this.txtRelativeHumidity = new System.Windows.Forms.TextBox();
            this.lblSatVaporDensity = new System.Windows.Forms.Label();
            this.txtSatVaporDensity = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCalculation = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGasCompositions)).BeginInit();
            this.SuspendLayout();
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGasCompositions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGasCompositions.ColumnHeadersHeight = 34;
            this.dgvGasCompositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvGasCompositions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSpecies0,
            this.ColumnMolarMass0,
            this.ColumnDryAir,
            this.ColumnHumidAir});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGasCompositions.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvGasCompositions.Location = new System.Drawing.Point(12, 32);
            this.dgvGasCompositions.MultiSelect = false;
            this.dgvGasCompositions.Name = "dgvGasCompositions";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGasCompositions.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvGasCompositions.RowHeadersVisible = false;
            this.dgvGasCompositions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvGasCompositions.Size = new System.Drawing.Size(400, 146);
            this.dgvGasCompositions.TabIndex = 0;
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
            // ColumnDryAir
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnDryAir.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnDryAir.HeaderText = "Dry Air";
            this.ColumnDryAir.Name = "ColumnDryAir";
            this.ColumnDryAir.ReadOnly = true;
            // 
            // ColumnHumidAir
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnHumidAir.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnHumidAir.HeaderText = "Humid Air";
            this.ColumnHumidAir.Name = "ColumnHumidAir";
            this.ColumnHumidAir.ReadOnly = true;
            // 
            // lblTemperature
            // 
            this.lblTemperature.AutoSize = true;
            this.lblTemperature.Location = new System.Drawing.Point(12, 188);
            this.lblTemperature.Name = "lblTemperature";
            this.lblTemperature.Size = new System.Drawing.Size(87, 13);
            this.lblTemperature.TabIndex = 1;
            this.lblTemperature.Text = "Temperature [℃]";
            // 
            // txtTemperature
            // 
            this.txtTemperature.Location = new System.Drawing.Point(311, 184);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(100, 20);
            this.txtTemperature.TabIndex = 2;
            this.txtTemperature.Text = "20";
            this.txtTemperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblRelativeHumidity
            // 
            this.lblRelativeHumidity.AutoSize = true;
            this.lblRelativeHumidity.Location = new System.Drawing.Point(13, 214);
            this.lblRelativeHumidity.Name = "lblRelativeHumidity";
            this.lblRelativeHumidity.Size = new System.Drawing.Size(106, 13);
            this.lblRelativeHumidity.TabIndex = 3;
            this.lblRelativeHumidity.Text = "Relative Humidity [%]";
            // 
            // txtRelativeHumidity
            // 
            this.txtRelativeHumidity.Location = new System.Drawing.Point(312, 210);
            this.txtRelativeHumidity.Name = "txtRelativeHumidity";
            this.txtRelativeHumidity.Size = new System.Drawing.Size(100, 20);
            this.txtRelativeHumidity.TabIndex = 4;
            this.txtRelativeHumidity.Text = "50";
            this.txtRelativeHumidity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSatVaporDensity
            // 
            this.lblSatVaporDensity.AutoSize = true;
            this.lblSatVaporDensity.Location = new System.Drawing.Point(13, 240);
            this.lblSatVaporDensity.Name = "lblSatVaporDensity";
            this.lblSatVaporDensity.Size = new System.Drawing.Size(129, 13);
            this.lblSatVaporDensity.TabIndex = 5;
            this.lblSatVaporDensity.Text = "Sat. Vapor Density [g/m3]";
            // 
            // txtSatVaporDensity
            // 
            this.txtSatVaporDensity.Location = new System.Drawing.Point(312, 236);
            this.txtSatVaporDensity.Name = "txtSatVaporDensity";
            this.txtSatVaporDensity.ReadOnly = true;
            this.txtSatVaporDensity.Size = new System.Drawing.Size(100, 20);
            this.txtSatVaporDensity.TabIndex = 6;
            this.txtSatVaporDensity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(312, 262);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(206, 262);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCalculation
            // 
            this.btnCalculation.Location = new System.Drawing.Point(100, 262);
            this.btnCalculation.Name = "btnCalculation";
            this.btnCalculation.Size = new System.Drawing.Size(100, 23);
            this.btnCalculation.TabIndex = 7;
            this.btnCalculation.Text = "Calculation";
            this.btnCalculation.UseVisualStyleBackColor = true;
            this.btnCalculation.Click += new System.EventHandler(this.btnCalculation_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Mole Fractions of Dry Air and Humid Air";
            // 
            // HumidAirForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 292);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCalculation);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtSatVaporDensity);
            this.Controls.Add(this.lblSatVaporDensity);
            this.Controls.Add(this.txtRelativeHumidity);
            this.Controls.Add(this.lblRelativeHumidity);
            this.Controls.Add(this.txtTemperature);
            this.Controls.Add(this.lblTemperature);
            this.Controls.Add(this.dgvGasCompositions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HumidAirForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Humid Air Module";
            this.Load += new System.EventHandler(this.HumidAirForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGasCompositions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGasCompositions;
        private System.Windows.Forms.Label lblTemperature;
        private System.Windows.Forms.TextBox txtTemperature;
        private System.Windows.Forms.Label lblRelativeHumidity;
        private System.Windows.Forms.TextBox txtRelativeHumidity;
        private System.Windows.Forms.Label lblSatVaporDensity;
        private System.Windows.Forms.TextBox txtSatVaporDensity;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCalculation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSpecies0;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMolarMass0;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDryAir;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnHumidAir;
        private System.Windows.Forms.Label label1;
    }
}