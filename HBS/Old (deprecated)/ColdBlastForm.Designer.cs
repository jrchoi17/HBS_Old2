
namespace HBS
{
    partial class ColdBlastForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColdBlastForm));
            this.chtPressure = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chtFlowRate = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvColdBlast = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chtTemperature = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblFlowOperatingConditions = new System.Windows.Forms.Label();
            this.lblCombustedGas = new System.Windows.Forms.Label();
            this.dgvAirFlowOperatingConditions = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPressure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvO2FlowOperatingConditions = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNormalizing = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chtPressure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chtFlowRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColdBlast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chtTemperature)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAirFlowOperatingConditions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvO2FlowOperatingConditions)).BeginInit();
            this.SuspendLayout();
            // 
            // chtPressure
            // 
            chartArea1.Name = "ChartArea1";
            this.chtPressure.ChartAreas.Add(chartArea1);
            this.chtPressure.Location = new System.Drawing.Point(651, 349);
            this.chtPressure.Name = "chtPressure";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            this.chtPressure.Series.Add(series1);
            this.chtPressure.Size = new System.Drawing.Size(305, 300);
            this.chtPressure.TabIndex = 29;
            this.chtPressure.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "Pressure";
            this.chtPressure.Titles.Add(title1);
            // 
            // chtFlowRate
            // 
            chartArea2.Name = "ChartArea1";
            this.chtFlowRate.ChartAreas.Add(chartArea2);
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            this.chtFlowRate.Legends.Add(legend1);
            this.chtFlowRate.Location = new System.Drawing.Point(11, 349);
            this.chtFlowRate.Name = "chtFlowRate";
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chtFlowRate.Series.Add(series2);
            this.chtFlowRate.Size = new System.Drawing.Size(305, 300);
            this.chtFlowRate.TabIndex = 30;
            this.chtFlowRate.Text = "chart1";
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "Title1";
            title2.Text = "Flow Rate";
            this.chtFlowRate.Titles.Add(title2);
            // 
            // dgvColdBlast
            // 
            this.dgvColdBlast.AllowUserToAddRows = false;
            this.dgvColdBlast.AllowUserToDeleteRows = false;
            this.dgvColdBlast.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvColdBlast.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvColdBlast.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColdBlast.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column6,
            this.Column7});
            this.dgvColdBlast.Location = new System.Drawing.Point(11, 27);
            this.dgvColdBlast.Name = "dgvColdBlast";
            this.dgvColdBlast.RowHeadersVisible = false;
            this.dgvColdBlast.Size = new System.Drawing.Size(338, 287);
            this.dgvColdBlast.TabIndex = 22;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Species";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.HeaderText = "Molar Mass";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column6
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column6.HeaderText = "MF Air";
            this.Column6.Name = "Column6";
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column7
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column7.HeaderText = "MF O2";
            this.Column7.Name = "Column7";
            // 
            // chtTemperature
            // 
            chartArea3.Name = "ChartArea1";
            this.chtTemperature.ChartAreas.Add(chartArea3);
            this.chtTemperature.Location = new System.Drawing.Point(331, 349);
            this.chtTemperature.Name = "chtTemperature";
            series3.BorderWidth = 3;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Name = "Series1";
            this.chtTemperature.Series.Add(series3);
            this.chtTemperature.Size = new System.Drawing.Size(305, 300);
            this.chtTemperature.TabIndex = 31;
            this.chtTemperature.Text = "chart1";
            title3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title3.Name = "Title1";
            title3.Text = "Temperature";
            this.chtTemperature.Titles.Add(title3);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(355, 320);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 23);
            this.button3.TabIndex = 25;
            this.button3.Text = "Open File";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(856, 320);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 23);
            this.button4.TabIndex = 26;
            this.button4.Text = "Accept";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(461, 320);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 27;
            this.button1.Text = "Save File";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 320);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 23);
            this.button2.TabIndex = 28;
            this.button2.Text = "Load Default";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // lblFlowOperatingConditions
            // 
            this.lblFlowOperatingConditions.AutoSize = true;
            this.lblFlowOperatingConditions.Location = new System.Drawing.Point(355, 11);
            this.lblFlowOperatingConditions.Name = "lblFlowOperatingConditions";
            this.lblFlowOperatingConditions.Size = new System.Drawing.Size(145, 13);
            this.lblFlowOperatingConditions.TabIndex = 23;
            this.lblFlowOperatingConditions.Text = "Air Flow Operating Conditions";
            // 
            // lblCombustedGas
            // 
            this.lblCombustedGas.AutoSize = true;
            this.lblCombustedGas.Location = new System.Drawing.Point(11, 11);
            this.lblCombustedGas.Name = "lblCombustedGas";
            this.lblCombustedGas.Size = new System.Drawing.Size(54, 13);
            this.lblCombustedGas.TabIndex = 24;
            this.lblCombustedGas.Text = "Cold Blast";
            // 
            // dgvAirFlowOperatingConditions
            // 
            this.dgvAirFlowOperatingConditions.AllowUserToDeleteRows = false;
            this.dgvAirFlowOperatingConditions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAirFlowOperatingConditions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAirFlowOperatingConditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAirFlowOperatingConditions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.Column5,
            this.ColumnPressure});
            this.dgvAirFlowOperatingConditions.Location = new System.Drawing.Point(355, 27);
            this.dgvAirFlowOperatingConditions.Name = "dgvAirFlowOperatingConditions";
            this.dgvAirFlowOperatingConditions.RowHeadersVisible = false;
            this.dgvAirFlowOperatingConditions.Size = new System.Drawing.Size(600, 133);
            this.dgvAirFlowOperatingConditions.TabIndex = 21;
            this.dgvAirFlowOperatingConditions.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValidated);
            this.dgvAirFlowOperatingConditions.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            // 
            // Column3
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column3.HeaderText = "Time [min]";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column4.HeaderText = "Flow Rate [N㎥/hr]";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column5.HeaderText = "Temperature [℃]";
            this.Column5.Name = "Column5";
            // 
            // ColumnPressure
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnPressure.DefaultCellStyle = dataGridViewCellStyle9;
            this.ColumnPressure.HeaderText = "Pressure [bar]";
            this.ColumnPressure.Name = "ColumnPressure";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(355, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "O2 Flow Operating Conditions";
            // 
            // dgvO2FlowOperatingConditions
            // 
            this.dgvO2FlowOperatingConditions.AllowUserToDeleteRows = false;
            this.dgvO2FlowOperatingConditions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvO2FlowOperatingConditions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvO2FlowOperatingConditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvO2FlowOperatingConditions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgvO2FlowOperatingConditions.Location = new System.Drawing.Point(355, 181);
            this.dgvO2FlowOperatingConditions.Name = "dgvO2FlowOperatingConditions";
            this.dgvO2FlowOperatingConditions.RowHeadersVisible = false;
            this.dgvO2FlowOperatingConditions.Size = new System.Drawing.Size(600, 133);
            this.dgvO2FlowOperatingConditions.TabIndex = 21;
            this.dgvO2FlowOperatingConditions.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValidated);
            this.dgvO2FlowOperatingConditions.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn1.HeaderText = "Time [min]";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn2.HeaderText = "Flow Rate [N㎥/hr]";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn3.HeaderText = "Temperature [℃]";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewTextBoxColumn4.HeaderText = "Pressure [bar]";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // btnNormalizing
            // 
            this.btnNormalizing.Location = new System.Drawing.Point(118, 320);
            this.btnNormalizing.Name = "btnNormalizing";
            this.btnNormalizing.Size = new System.Drawing.Size(100, 23);
            this.btnNormalizing.TabIndex = 32;
            this.btnNormalizing.Text = "Normalizing";
            this.btnNormalizing.UseVisualStyleBackColor = true;
            this.btnNormalizing.Click += new System.EventHandler(this.btnNormalizing_Click);
            // 
            // ColdBlastForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 667);
            this.Controls.Add(this.btnNormalizing);
            this.Controls.Add(this.chtPressure);
            this.Controls.Add(this.chtFlowRate);
            this.Controls.Add(this.dgvColdBlast);
            this.Controls.Add(this.chtTemperature);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFlowOperatingConditions);
            this.Controls.Add(this.lblCombustedGas);
            this.Controls.Add(this.dgvO2FlowOperatingConditions);
            this.Controls.Add(this.dgvAirFlowOperatingConditions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColdBlastForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cold Blast";
            ((System.ComponentModel.ISupportInitialize)(this.chtPressure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chtFlowRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColdBlast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chtTemperature)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAirFlowOperatingConditions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvO2FlowOperatingConditions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chtPressure;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtFlowRate;
        private System.Windows.Forms.DataGridView dgvColdBlast;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtTemperature;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblFlowOperatingConditions;
        private System.Windows.Forms.Label lblCombustedGas;
        private System.Windows.Forms.DataGridView dgvAirFlowOperatingConditions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvO2FlowOperatingConditions;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPressure;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button btnNormalizing;
    }
}