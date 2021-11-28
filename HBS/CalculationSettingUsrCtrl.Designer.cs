
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
            this.lblConvectionHeatTransferCoefficient = new System.Windows.Forms.Label();
            this.txtlblConvectionHeatTransferCoefficient = new System.Windows.Forms.TextBox();
            this.lblAmbientTemperature = new System.Windows.Forms.Label();
            this.txtlblAmbientTemperature = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAmbientTemperature);
            this.groupBox1.Controls.Add(this.txtlblAmbientTemperature);
            this.groupBox1.Controls.Add(this.txtlblConvectionHeatTransferCoefficient);
            this.groupBox1.Controls.Add(this.lblConvectionHeatTransferCoefficient);
            this.groupBox1.Location = new System.Drawing.Point(23, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 118);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ambient Conditions";
            // 
            // lblConvectionHeatTransferCoefficient
            // 
            this.lblConvectionHeatTransferCoefficient.AutoSize = true;
            this.lblConvectionHeatTransferCoefficient.Location = new System.Drawing.Point(20, 33);
            this.lblConvectionHeatTransferCoefficient.Name = "lblConvectionHeatTransferCoefficient";
            this.lblConvectionHeatTransferCoefficient.Size = new System.Drawing.Size(338, 20);
            this.lblConvectionHeatTransferCoefficient.TabIndex = 0;
            this.lblConvectionHeatTransferCoefficient.Text = "Convection Heat Transfer Coefficient [W/m2-K]";
            // 
            // txtlblConvectionHeatTransferCoefficient
            // 
            this.txtlblConvectionHeatTransferCoefficient.Location = new System.Drawing.Point(402, 32);
            this.txtlblConvectionHeatTransferCoefficient.Name = "txtlblConvectionHeatTransferCoefficient";
            this.txtlblConvectionHeatTransferCoefficient.Size = new System.Drawing.Size(100, 26);
            this.txtlblConvectionHeatTransferCoefficient.TabIndex = 1;
            this.txtlblConvectionHeatTransferCoefficient.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAmbientTemperature
            // 
            this.lblAmbientTemperature.AutoSize = true;
            this.lblAmbientTemperature.Location = new System.Drawing.Point(20, 71);
            this.lblAmbientTemperature.Name = "lblAmbientTemperature";
            this.lblAmbientTemperature.Size = new System.Drawing.Size(186, 20);
            this.lblAmbientTemperature.TabIndex = 2;
            this.lblAmbientTemperature.Text = "Ambient Temperature [C]";
            // 
            // txtlblAmbientTemperature
            // 
            this.txtlblAmbientTemperature.Location = new System.Drawing.Point(402, 68);
            this.txtlblAmbientTemperature.Name = "txtlblAmbientTemperature";
            this.txtlblAmbientTemperature.Size = new System.Drawing.Size(100, 26);
            this.txtlblAmbientTemperature.TabIndex = 1;
            this.txtlblAmbientTemperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CalculationSettingUsrCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CalculationSettingUsrCtrl";
            this.Size = new System.Drawing.Size(1650, 980);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblAmbientTemperature;
        private System.Windows.Forms.TextBox txtlblAmbientTemperature;
        private System.Windows.Forms.TextBox txtlblConvectionHeatTransferCoefficient;
        private System.Windows.Forms.Label lblConvectionHeatTransferCoefficient;
    }
}
