
namespace HBS
{
    partial class TemperatureProfileUsrCtrl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chContinueUpdatingTemperatureProfile = new System.Windows.Forms.CheckBox();
            this.trbCycle = new System.Windows.Forms.TrackBar();
            this.lblCycle = new System.Windows.Forms.Label();
            this.trbElapsedTimePerCycle = new System.Windows.Forms.TrackBar();
            this.lblElapsedTimePerCycle = new System.Windows.Forms.Label();
            this.txtCycle = new System.Windows.Forms.TextBox();
            this.txtElapsedTimePerCycle = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbCycle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbElapsedTimePerCycle)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart
            // 
            chartArea3.AxisX.Maximum = 1600D;
            chartArea3.AxisX.Minimum = 200D;
            chartArea3.AxisY.IsReversed = true;
            chartArea3.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea3);
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend3.Name = "Legend1";
            this.chart.Legends.Add(legend3);
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Margin = new System.Windows.Forms.Padding(0);
            this.chart.Name = "chart";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Legend = "Legend1";
            series8.Name = "Series2";
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series9.Legend = "Legend1";
            series9.Name = "Series3";
            this.chart.Series.Add(series7);
            this.chart.Series.Add(series8);
            this.chart.Series.Add(series9);
            this.chart.Size = new System.Drawing.Size(642, 618);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart1";
            // 
            // chContinueUpdatingTemperatureProfile
            // 
            this.chContinueUpdatingTemperatureProfile.AutoSize = true;
            this.chContinueUpdatingTemperatureProfile.Checked = true;
            this.chContinueUpdatingTemperatureProfile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chContinueUpdatingTemperatureProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chContinueUpdatingTemperatureProfile.Location = new System.Drawing.Point(0, 618);
            this.chContinueUpdatingTemperatureProfile.Margin = new System.Windows.Forms.Padding(0);
            this.chContinueUpdatingTemperatureProfile.Name = "chContinueUpdatingTemperatureProfile";
            this.chContinueUpdatingTemperatureProfile.Size = new System.Drawing.Size(642, 30);
            this.chContinueUpdatingTemperatureProfile.TabIndex = 1;
            this.chContinueUpdatingTemperatureProfile.Text = "Continue Updating Temperature Profile";
            this.chContinueUpdatingTemperatureProfile.UseVisualStyleBackColor = true;
            this.chContinueUpdatingTemperatureProfile.CheckedChanged += new System.EventHandler(this.chContinueUpdatingTemperatureProfile_CheckedChanged);
            // 
            // trbCycle
            // 
            this.trbCycle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trbCycle.Location = new System.Drawing.Point(3, 677);
            this.trbCycle.Name = "trbCycle";
            this.trbCycle.Size = new System.Drawing.Size(636, 44);
            this.trbCycle.TabIndex = 2;
            this.trbCycle.Value = 1;
            this.trbCycle.Scroll += new System.EventHandler(this.trbCycle_Scroll);
            this.trbCycle.ValueChanged += new System.EventHandler(this.trbCycle_ValueChanged);
            // 
            // lblCycle
            // 
            this.lblCycle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCycle.Location = new System.Drawing.Point(0, 0);
            this.lblCycle.Name = "lblCycle";
            this.lblCycle.Size = new System.Drawing.Size(65, 26);
            this.lblCycle.TabIndex = 3;
            this.lblCycle.Text = "Cycle";
            this.lblCycle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trbElapsedTimePerCycle
            // 
            this.trbElapsedTimePerCycle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trbElapsedTimePerCycle.LargeChange = 10;
            this.trbElapsedTimePerCycle.Location = new System.Drawing.Point(3, 763);
            this.trbElapsedTimePerCycle.Name = "trbElapsedTimePerCycle";
            this.trbElapsedTimePerCycle.Size = new System.Drawing.Size(636, 44);
            this.trbElapsedTimePerCycle.SmallChange = 10;
            this.trbElapsedTimePerCycle.TabIndex = 2;
            this.trbElapsedTimePerCycle.Value = 1;
            this.trbElapsedTimePerCycle.Scroll += new System.EventHandler(this.trbElapsedTimePerCycle_Scroll);
            this.trbElapsedTimePerCycle.ValueChanged += new System.EventHandler(this.trbElapsedTimePerCycle_ValueChanged);
            // 
            // lblElapsedTimePerCycle
            // 
            this.lblElapsedTimePerCycle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblElapsedTimePerCycle.Location = new System.Drawing.Point(0, 0);
            this.lblElapsedTimePerCycle.Name = "lblElapsedTimePerCycle";
            this.lblElapsedTimePerCycle.Size = new System.Drawing.Size(174, 26);
            this.lblElapsedTimePerCycle.TabIndex = 3;
            this.lblElapsedTimePerCycle.Text = "Elapsed Time per Cycle";
            this.lblElapsedTimePerCycle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCycle
            // 
            this.txtCycle.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtCycle.Location = new System.Drawing.Point(542, 0);
            this.txtCycle.Name = "txtCycle";
            this.txtCycle.Size = new System.Drawing.Size(100, 26);
            this.txtCycle.TabIndex = 4;
            this.txtCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCycle.Validating += new System.ComponentModel.CancelEventHandler(this.txtCycle_Validating);
            this.txtCycle.Validated += new System.EventHandler(this.txtCycle_Validated);
            // 
            // txtElapsedTimePerCycle
            // 
            this.txtElapsedTimePerCycle.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtElapsedTimePerCycle.Location = new System.Drawing.Point(542, 0);
            this.txtElapsedTimePerCycle.Name = "txtElapsedTimePerCycle";
            this.txtElapsedTimePerCycle.Size = new System.Drawing.Size(100, 26);
            this.txtElapsedTimePerCycle.TabIndex = 4;
            this.txtElapsedTimePerCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtElapsedTimePerCycle.Validating += new System.ComponentModel.CancelEventHandler(this.txtElapsedTimePerCycle_Validating);
            this.txtElapsedTimePerCycle.Validated += new System.EventHandler(this.txtElapsedTimePerCycle_Validated);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.chart, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chContinueUpdatingTemperatureProfile, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.trbCycle, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.trbElapsedTimePerCycle, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(642, 810);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtElapsedTimePerCycle);
            this.panel2.Controls.Add(this.lblElapsedTimePerCycle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 734);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(642, 26);
            this.panel2.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCycle);
            this.panel1.Controls.Add(this.txtCycle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 648);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(642, 26);
            this.panel1.TabIndex = 3;
            // 
            // TemperatureProfileUsrCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TemperatureProfileUsrCtrl";
            this.Size = new System.Drawing.Size(642, 810);
            this.Load += new System.EventHandler(this.TemperatureProfileUsrCtrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbCycle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbElapsedTimePerCycle)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.CheckBox chContinueUpdatingTemperatureProfile;
        private System.Windows.Forms.TrackBar trbCycle;
        private System.Windows.Forms.Label lblCycle;
        private System.Windows.Forms.TrackBar trbElapsedTimePerCycle;
        private System.Windows.Forms.Label lblElapsedTimePerCycle;
        private System.Windows.Forms.TextBox txtCycle;
        private System.Windows.Forms.TextBox txtElapsedTimePerCycle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}
