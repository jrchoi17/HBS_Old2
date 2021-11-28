
namespace HBS
{
    partial class AmbientConditionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AmbientConditionForm));
            this.gbParameters = new System.Windows.Forms.GroupBox();
            this.txtTemperature = new System.Windows.Forms.TextBox();
            this.txtConvectionHeatTransferCoefficient = new System.Windows.Forms.TextBox();
            this.lblConvectionHeatTransferCoefficient = new System.Windows.Forms.Label();
            this.lblTemperature = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gbParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbParameters
            // 
            this.gbParameters.Controls.Add(this.txtTemperature);
            this.gbParameters.Controls.Add(this.txtConvectionHeatTransferCoefficient);
            this.gbParameters.Controls.Add(this.lblConvectionHeatTransferCoefficient);
            this.gbParameters.Controls.Add(this.lblTemperature);
            this.gbParameters.Location = new System.Drawing.Point(12, 12);
            this.gbParameters.Name = "gbParameters";
            this.gbParameters.Size = new System.Drawing.Size(370, 81);
            this.gbParameters.TabIndex = 0;
            this.gbParameters.TabStop = false;
            this.gbParameters.Text = "Parameters";
            // 
            // txtTemperature
            // 
            this.txtTemperature.Location = new System.Drawing.Point(264, 45);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(100, 20);
            this.txtTemperature.TabIndex = 3;
            this.txtTemperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtConvectionHeatTransferCoefficient
            // 
            this.txtConvectionHeatTransferCoefficient.Location = new System.Drawing.Point(264, 19);
            this.txtConvectionHeatTransferCoefficient.Name = "txtConvectionHeatTransferCoefficient";
            this.txtConvectionHeatTransferCoefficient.Size = new System.Drawing.Size(100, 20);
            this.txtConvectionHeatTransferCoefficient.TabIndex = 1;
            this.txtConvectionHeatTransferCoefficient.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblConvectionHeatTransferCoefficient
            // 
            this.lblConvectionHeatTransferCoefficient.AutoSize = true;
            this.lblConvectionHeatTransferCoefficient.Location = new System.Drawing.Point(6, 23);
            this.lblConvectionHeatTransferCoefficient.Name = "lblConvectionHeatTransferCoefficient";
            this.lblConvectionHeatTransferCoefficient.Size = new System.Drawing.Size(226, 13);
            this.lblConvectionHeatTransferCoefficient.TabIndex = 0;
            this.lblConvectionHeatTransferCoefficient.Text = "Convection Heat Transfer Coefficient [W/㎡K]";
            // 
            // lblTemperature
            // 
            this.lblTemperature.AutoSize = true;
            this.lblTemperature.Location = new System.Drawing.Point(6, 49);
            this.lblTemperature.Name = "lblTemperature";
            this.lblTemperature.Size = new System.Drawing.Size(87, 13);
            this.lblTemperature.TabIndex = 2;
            this.lblTemperature.Text = "Temperature [℃]";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(307, 99);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(226, 99);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // AmbientConditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 132);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbParameters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AmbientConditionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ambient Conditions";
            this.gbParameters.ResumeLayout(false);
            this.gbParameters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbParameters;
        private System.Windows.Forms.TextBox txtConvectionHeatTransferCoefficient;
        private System.Windows.Forms.Label lblTemperature;
        private System.Windows.Forms.TextBox txtTemperature;
        private System.Windows.Forms.Label lblConvectionHeatTransferCoefficient;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
    }
}