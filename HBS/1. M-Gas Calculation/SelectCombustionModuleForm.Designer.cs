
namespace HBS
{
    partial class SelectCombustionModuleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectCombustionModuleForm));
            this.rb0DCantera = new System.Windows.Forms.RadioButton();
            this.rb1D = new System.Windows.Forms.RadioButton();
            this.gb1DCanteraParameter = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.cbSpecifiedValue = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCalculation = new System.Windows.Forms.Button();
            this.gb1DCanteraParameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // rb0DCantera
            // 
            this.rb0DCantera.AutoSize = true;
            this.rb0DCantera.Location = new System.Drawing.Point(12, 12);
            this.rb0DCantera.Name = "rb0DCantera";
            this.rb0DCantera.Size = new System.Drawing.Size(79, 17);
            this.rb0DCantera.TabIndex = 0;
            this.rb0DCantera.Text = "0D Cantera";
            this.rb0DCantera.UseVisualStyleBackColor = true;
            this.rb0DCantera.CheckedChanged += new System.EventHandler(this.rb0DCantera_CheckedChanged);
            // 
            // rb1D
            // 
            this.rb1D.AutoSize = true;
            this.rb1D.Location = new System.Drawing.Point(12, 35);
            this.rb1D.Name = "rb1D";
            this.rb1D.Size = new System.Drawing.Size(79, 17);
            this.rb1D.TabIndex = 1;
            this.rb1D.Text = "1D Cantera";
            this.rb1D.UseVisualStyleBackColor = true;
            this.rb1D.CheckedChanged += new System.EventHandler(this.rb1D_CheckedChanged);
            // 
            // gb1DCanteraParameter
            // 
            this.gb1DCanteraParameter.Controls.Add(this.label2);
            this.gb1DCanteraParameter.Controls.Add(this.txtWidth);
            this.gb1DCanteraParameter.Controls.Add(this.lblWidth);
            this.gb1DCanteraParameter.Controls.Add(this.cbSpecifiedValue);
            this.gb1DCanteraParameter.Location = new System.Drawing.Point(12, 58);
            this.gb1DCanteraParameter.Name = "gb1DCanteraParameter";
            this.gb1DCanteraParameter.Size = new System.Drawing.Size(218, 77);
            this.gb1DCanteraParameter.TabIndex = 2;
            this.gb1DCanteraParameter.TabStop = false;
            this.gb1DCanteraParameter.Text = "1D Cantera Parameter";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "m";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(91, 42);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(100, 20);
            this.txtWidth.TabIndex = 2;
            this.txtWidth.Text = "0.3";
            this.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWidth.Validating += new System.ComponentModel.CancelEventHandler(this.txtWidth_Validating);
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(6, 46);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(35, 13);
            this.lblWidth.TabIndex = 1;
            this.lblWidth.Text = "Width";
            // 
            // cbSpecifiedValue
            // 
            this.cbSpecifiedValue.AutoSize = true;
            this.cbSpecifiedValue.Checked = true;
            this.cbSpecifiedValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSpecifiedValue.Location = new System.Drawing.Point(6, 19);
            this.cbSpecifiedValue.Name = "cbSpecifiedValue";
            this.cbSpecifiedValue.Size = new System.Drawing.Size(100, 17);
            this.cbSpecifiedValue.TabIndex = 0;
            this.cbSpecifiedValue.Text = "Specified Value";
            this.cbSpecifiedValue.UseVisualStyleBackColor = true;
            this.cbSpecifiedValue.CheckedChanged += new System.EventHandler(this.cbSpecifiedValue_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(155, 141);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnCalculation
            // 
            this.btnCalculation.Location = new System.Drawing.Point(74, 141);
            this.btnCalculation.Name = "btnCalculation";
            this.btnCalculation.Size = new System.Drawing.Size(75, 23);
            this.btnCalculation.TabIndex = 3;
            this.btnCalculation.Text = "Calculation";
            this.btnCalculation.UseVisualStyleBackColor = true;
            this.btnCalculation.Click += new System.EventHandler(this.btnCalculation_Click);
            // 
            // SelectCombustionModuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 176);
            this.Controls.Add(this.btnCalculation);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gb1DCanteraParameter);
            this.Controls.Add(this.rb1D);
            this.Controls.Add(this.rb0DCantera);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectCombustionModuleForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Combustion Module";
            this.Load += new System.EventHandler(this.SelectCombustionModuleForm_Load);
            this.gb1DCanteraParameter.ResumeLayout(false);
            this.gb1DCanteraParameter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rb0DCantera;
        private System.Windows.Forms.RadioButton rb1D;
        private System.Windows.Forms.GroupBox gb1DCanteraParameter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.CheckBox cbSpecifiedValue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCalculation;
    }
}