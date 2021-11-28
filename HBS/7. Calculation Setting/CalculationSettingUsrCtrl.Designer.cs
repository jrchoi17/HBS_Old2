
namespace HBS
{
    partial class CalculationSettingUsrCtrl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbUseConvectiveHeatLoss = new System.Windows.Forms.CheckBox();
            this.lblAmbientTemperature = new System.Windows.Forms.Label();
            this.txtAmbientTemperature = new System.Windows.Forms.TextBox();
            this.txtConvectionHeatTransferCoefficient = new System.Windows.Forms.TextBox();
            this.lblConvectionHeatTransferCoefficient = new System.Windows.Forms.Label();
            this.gbProcessSettings = new System.Windows.Forms.GroupBox();
            this.lblCurrentCycle2 = new System.Windows.Forms.Label();
            this.txtNumberOfProcesses = new System.Windows.Forms.TextBox();
            this.txtCurrentCycle = new System.Windows.Forms.TextBox();
            this.txtProcessTime = new System.Windows.Forms.TextBox();
            this.lblCurrentCycle = new System.Windows.Forms.Label();
            this.lblNumberOfProcesses = new System.Windows.Forms.Label();
            this.lblProcessTime = new System.Windows.Forms.Label();
            this.gbTimeSettings = new System.Windows.Forms.GroupBox();
            this.lblCurrentTime2 = new System.Windows.Forms.Label();
            this.txtCurrentTime = new System.Windows.Forms.TextBox();
            this.txtTimeInterval = new System.Windows.Forms.TextBox();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.lblTimeInterval = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.gbProcessSettings.SuspendLayout();
            this.gbTimeSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbUseConvectiveHeatLoss);
            this.groupBox1.Controls.Add(this.lblAmbientTemperature);
            this.groupBox1.Controls.Add(this.txtAmbientTemperature);
            this.groupBox1.Controls.Add(this.txtConvectionHeatTransferCoefficient);
            this.groupBox1.Controls.Add(this.lblConvectionHeatTransferCoefficient);
            this.groupBox1.Location = new System.Drawing.Point(23, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 230);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ambient Conditions";
            // 
            // cbUseConvectiveHeatLoss
            // 
            this.cbUseConvectiveHeatLoss.AutoSize = true;
            this.cbUseConvectiveHeatLoss.Checked = true;
            this.cbUseConvectiveHeatLoss.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseConvectiveHeatLoss.Location = new System.Drawing.Point(27, 53);
            this.cbUseConvectiveHeatLoss.Name = "cbUseConvectiveHeatLoss";
            this.cbUseConvectiveHeatLoss.Size = new System.Drawing.Size(215, 24);
            this.cbUseConvectiveHeatLoss.TabIndex = 3;
            this.cbUseConvectiveHeatLoss.Text = "Use Convective Heat Loss";
            this.cbUseConvectiveHeatLoss.UseVisualStyleBackColor = true;
            this.cbUseConvectiveHeatLoss.CheckedChanged += new System.EventHandler(this.cbUseConvectiveHeatLoss_CheckedChanged);
            // 
            // lblAmbientTemperature
            // 
            this.lblAmbientTemperature.AutoSize = true;
            this.lblAmbientTemperature.Location = new System.Drawing.Point(26, 168);
            this.lblAmbientTemperature.Name = "lblAmbientTemperature";
            this.lblAmbientTemperature.Size = new System.Drawing.Size(186, 20);
            this.lblAmbientTemperature.TabIndex = 2;
            this.lblAmbientTemperature.Text = "Ambient Temperature [C]";
            // 
            // txtAmbientTemperature
            // 
            this.txtAmbientTemperature.Location = new System.Drawing.Point(406, 165);
            this.txtAmbientTemperature.Name = "txtAmbientTemperature";
            this.txtAmbientTemperature.Size = new System.Drawing.Size(100, 26);
            this.txtAmbientTemperature.TabIndex = 1;
            this.txtAmbientTemperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtConvectionHeatTransferCoefficient
            // 
            this.txtConvectionHeatTransferCoefficient.Location = new System.Drawing.Point(406, 114);
            this.txtConvectionHeatTransferCoefficient.Name = "txtConvectionHeatTransferCoefficient";
            this.txtConvectionHeatTransferCoefficient.Size = new System.Drawing.Size(100, 26);
            this.txtConvectionHeatTransferCoefficient.TabIndex = 1;
            this.txtConvectionHeatTransferCoefficient.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblConvectionHeatTransferCoefficient
            // 
            this.lblConvectionHeatTransferCoefficient.AutoSize = true;
            this.lblConvectionHeatTransferCoefficient.Location = new System.Drawing.Point(23, 117);
            this.lblConvectionHeatTransferCoefficient.Name = "lblConvectionHeatTransferCoefficient";
            this.lblConvectionHeatTransferCoefficient.Size = new System.Drawing.Size(338, 20);
            this.lblConvectionHeatTransferCoefficient.TabIndex = 0;
            this.lblConvectionHeatTransferCoefficient.Text = "Convection Heat Transfer Coefficient [W/m2-K]";
            // 
            // gbProcessSettings
            // 
            this.gbProcessSettings.Controls.Add(this.lblCurrentCycle2);
            this.gbProcessSettings.Controls.Add(this.txtNumberOfProcesses);
            this.gbProcessSettings.Controls.Add(this.txtCurrentCycle);
            this.gbProcessSettings.Controls.Add(this.txtProcessTime);
            this.gbProcessSettings.Controls.Add(this.lblCurrentCycle);
            this.gbProcessSettings.Controls.Add(this.lblNumberOfProcesses);
            this.gbProcessSettings.Controls.Add(this.lblProcessTime);
            this.gbProcessSettings.Location = new System.Drawing.Point(23, 267);
            this.gbProcessSettings.Name = "gbProcessSettings";
            this.gbProcessSettings.Size = new System.Drawing.Size(530, 230);
            this.gbProcessSettings.TabIndex = 1;
            this.gbProcessSettings.TabStop = false;
            this.gbProcessSettings.Text = "Process Settings";
            // 
            // lblCurrentCycle2
            // 
            this.lblCurrentCycle2.AutoSize = true;
            this.lblCurrentCycle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentCycle2.Location = new System.Drawing.Point(25, 74);
            this.lblCurrentCycle2.Name = "lblCurrentCycle2";
            this.lblCurrentCycle2.Size = new System.Drawing.Size(321, 15);
            this.lblCurrentCycle2.TabIndex = 2;
            this.lblCurrentCycle2.Text = "(If you have no set value, 0 is recommended the first time.)";
            // 
            // txtNumberOfProcesses
            // 
            this.txtNumberOfProcesses.Location = new System.Drawing.Point(402, 102);
            this.txtNumberOfProcesses.Name = "txtNumberOfProcesses";
            this.txtNumberOfProcesses.Size = new System.Drawing.Size(100, 26);
            this.txtNumberOfProcesses.TabIndex = 1;
            this.txtNumberOfProcesses.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCurrentCycle
            // 
            this.txtCurrentCycle.Location = new System.Drawing.Point(402, 51);
            this.txtCurrentCycle.Name = "txtCurrentCycle";
            this.txtCurrentCycle.Size = new System.Drawing.Size(100, 26);
            this.txtCurrentCycle.TabIndex = 1;
            this.txtCurrentCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtProcessTime
            // 
            this.txtProcessTime.Location = new System.Drawing.Point(402, 153);
            this.txtProcessTime.Name = "txtProcessTime";
            this.txtProcessTime.Size = new System.Drawing.Size(100, 26);
            this.txtProcessTime.TabIndex = 1;
            this.txtProcessTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCurrentCycle
            // 
            this.lblCurrentCycle.AutoSize = true;
            this.lblCurrentCycle.Location = new System.Drawing.Point(24, 54);
            this.lblCurrentCycle.Name = "lblCurrentCycle";
            this.lblCurrentCycle.Size = new System.Drawing.Size(104, 20);
            this.lblCurrentCycle.TabIndex = 0;
            this.lblCurrentCycle.Text = "Current Cycle";
            // 
            // lblNumberOfProcesses
            // 
            this.lblNumberOfProcesses.AutoSize = true;
            this.lblNumberOfProcesses.Location = new System.Drawing.Point(24, 102);
            this.lblNumberOfProcesses.Name = "lblNumberOfProcesses";
            this.lblNumberOfProcesses.Size = new System.Drawing.Size(161, 20);
            this.lblNumberOfProcesses.TabIndex = 0;
            this.lblNumberOfProcesses.Text = "Number of Processes";
            // 
            // lblProcessTime
            // 
            this.lblProcessTime.AutoSize = true;
            this.lblProcessTime.Location = new System.Drawing.Point(26, 156);
            this.lblProcessTime.Name = "lblProcessTime";
            this.lblProcessTime.Size = new System.Drawing.Size(203, 20);
            this.lblProcessTime.TabIndex = 0;
            this.lblProcessTime.Text = "Process Time (1 cycle) [min]";
            // 
            // gbTimeSettings
            // 
            this.gbTimeSettings.Controls.Add(this.lblCurrentTime2);
            this.gbTimeSettings.Controls.Add(this.txtCurrentTime);
            this.gbTimeSettings.Controls.Add(this.txtTimeInterval);
            this.gbTimeSettings.Controls.Add(this.lblCurrentTime);
            this.gbTimeSettings.Controls.Add(this.lblTimeInterval);
            this.gbTimeSettings.Location = new System.Drawing.Point(23, 522);
            this.gbTimeSettings.Name = "gbTimeSettings";
            this.gbTimeSettings.Size = new System.Drawing.Size(530, 179);
            this.gbTimeSettings.TabIndex = 1;
            this.gbTimeSettings.TabStop = false;
            this.gbTimeSettings.Text = "Time Settings";
            // 
            // lblCurrentTime2
            // 
            this.lblCurrentTime2.AutoSize = true;
            this.lblCurrentTime2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentTime2.Location = new System.Drawing.Point(27, 71);
            this.lblCurrentTime2.Name = "lblCurrentTime2";
            this.lblCurrentTime2.Size = new System.Drawing.Size(331, 15);
            this.lblCurrentTime2.TabIndex = 2;
            this.lblCurrentTime2.Text = "(If you have no set value, 0.0 is recommended the first time.)";
            // 
            // txtCurrentTime
            // 
            this.txtCurrentTime.Location = new System.Drawing.Point(402, 51);
            this.txtCurrentTime.Name = "txtCurrentTime";
            this.txtCurrentTime.Size = new System.Drawing.Size(100, 26);
            this.txtCurrentTime.TabIndex = 1;
            this.txtCurrentTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTimeInterval
            // 
            this.txtTimeInterval.Location = new System.Drawing.Point(402, 102);
            this.txtTimeInterval.Name = "txtTimeInterval";
            this.txtTimeInterval.Size = new System.Drawing.Size(100, 26);
            this.txtTimeInterval.TabIndex = 1;
            this.txtTimeInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Location = new System.Drawing.Point(26, 51);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(137, 20);
            this.lblCurrentTime.TabIndex = 0;
            this.lblCurrentTime.Text = "Current Time [sec]";
            // 
            // lblTimeInterval
            // 
            this.lblTimeInterval.AutoSize = true;
            this.lblTimeInterval.Location = new System.Drawing.Point(26, 105);
            this.lblTimeInterval.Name = "lblTimeInterval";
            this.lblTimeInterval.Size = new System.Drawing.Size(136, 20);
            this.lblTimeInterval.TabIndex = 0;
            this.lblTimeInterval.Text = "Time Interval [sec]";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(425, 726);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(128, 66);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(643, 12);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 26);
            this.textBox5.TabIndex = 1;
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox5.Visible = false;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(643, 63);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 26);
            this.textBox6.TabIndex = 1;
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox6.Visible = false;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(643, 114);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 26);
            this.textBox7.TabIndex = 1;
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox7.Visible = false;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(643, 165);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(100, 26);
            this.textBox8.TabIndex = 1;
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox8.Visible = false;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(643, 216);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 26);
            this.textBox9.TabIndex = 1;
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox9.Visible = false;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(643, 267);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(100, 26);
            this.textBox10.TabIndex = 1;
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox10.Visible = false;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(643, 318);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(100, 26);
            this.textBox11.TabIndex = 1;
            this.textBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox11.Visible = false;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(643, 369);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(100, 26);
            this.textBox12.TabIndex = 1;
            this.textBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox12.Visible = false;
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(643, 420);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(100, 26);
            this.textBox13.TabIndex = 1;
            this.textBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox13.Visible = false;
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(643, 471);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(100, 26);
            this.textBox14.TabIndex = 1;
            this.textBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox14.Visible = false;
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(643, 522);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(100, 26);
            this.textBox15.TabIndex = 1;
            this.textBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox15.Visible = false;
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(643, 573);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(100, 26);
            this.textBox16.TabIndex = 1;
            this.textBox16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox16.Visible = false;
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(643, 624);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(100, 26);
            this.textBox17.TabIndex = 1;
            this.textBox17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox17.Visible = false;
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(643, 675);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(100, 26);
            this.textBox18.TabIndex = 1;
            this.textBox18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox18.Visible = false;
            // 
            // textBox19
            // 
            this.textBox19.Location = new System.Drawing.Point(643, 726);
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(100, 26);
            this.textBox19.TabIndex = 1;
            this.textBox19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox19.Visible = false;
            // 
            // textBox20
            // 
            this.textBox20.Location = new System.Drawing.Point(643, 828);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(100, 26);
            this.textBox20.TabIndex = 1;
            this.textBox20.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox20.Visible = false;
            // 
            // textBox21
            // 
            this.textBox21.Location = new System.Drawing.Point(643, 879);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(100, 26);
            this.textBox21.TabIndex = 1;
            this.textBox21.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox21.Visible = false;
            // 
            // textBox22
            // 
            this.textBox22.Location = new System.Drawing.Point(643, 777);
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(100, 26);
            this.textBox22.TabIndex = 1;
            this.textBox22.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox22.Visible = false;
            // 
            // textBox23
            // 
            this.textBox23.Location = new System.Drawing.Point(643, 930);
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new System.Drawing.Size(100, 26);
            this.textBox23.TabIndex = 1;
            this.textBox23.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox23.Visible = false;
            // 
            // CalculationSettingUsrCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.gbTimeSettings);
            this.Controls.Add(this.gbProcessSettings);
            this.Controls.Add(this.textBox23);
            this.Controls.Add(this.textBox19);
            this.Controls.Add(this.textBox22);
            this.Controls.Add(this.textBox16);
            this.Controls.Add(this.textBox13);
            this.Controls.Add(this.textBox21);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox18);
            this.Controls.Add(this.textBox15);
            this.Controls.Add(this.textBox12);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox20);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox17);
            this.Controls.Add(this.textBox14);
            this.Controls.Add(this.textBox11);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CalculationSettingUsrCtrl";
            this.Size = new System.Drawing.Size(1650, 980);
            this.Load += new System.EventHandler(this.CalculationSettingUsrCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbProcessSettings.ResumeLayout(false);
            this.gbProcessSettings.PerformLayout();
            this.gbTimeSettings.ResumeLayout(false);
            this.gbTimeSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblAmbientTemperature;
        private System.Windows.Forms.TextBox txtAmbientTemperature;
        private System.Windows.Forms.TextBox txtConvectionHeatTransferCoefficient;
        private System.Windows.Forms.Label lblConvectionHeatTransferCoefficient;
        private System.Windows.Forms.GroupBox gbProcessSettings;
        private System.Windows.Forms.Label lblProcessTime;
        private System.Windows.Forms.TextBox txtNumberOfProcesses;
        private System.Windows.Forms.TextBox txtProcessTime;
        private System.Windows.Forms.Label lblNumberOfProcesses;
        private System.Windows.Forms.GroupBox gbTimeSettings;
        private System.Windows.Forms.TextBox txtTimeInterval;
        private System.Windows.Forms.Label lblTimeInterval;
        private System.Windows.Forms.CheckBox cbUseConvectiveHeatLoss;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtCurrentCycle;
        private System.Windows.Forms.Label lblCurrentCycle;
        private System.Windows.Forms.Label lblCurrentCycle2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.TextBox textBox19;
        private System.Windows.Forms.TextBox textBox20;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.TextBox txtCurrentTime;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Label lblCurrentTime2;
    }
}
