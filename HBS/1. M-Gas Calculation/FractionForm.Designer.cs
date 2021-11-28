
namespace HBS
{
    partial class FractionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FractionForm));
            this.gbUserSelection = new System.Windows.Forms.GroupBox();
            this.rbMassFraction = new System.Windows.Forms.RadioButton();
            this.rbMoleFraction = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cbNormalize = new System.Windows.Forms.CheckBox();
            this.gbUserSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbUserSelection
            // 
            this.gbUserSelection.Controls.Add(this.rbMassFraction);
            this.gbUserSelection.Controls.Add(this.rbMoleFraction);
            this.gbUserSelection.Location = new System.Drawing.Point(0, 6);
            this.gbUserSelection.Name = "gbUserSelection";
            this.gbUserSelection.Size = new System.Drawing.Size(249, 49);
            this.gbUserSelection.TabIndex = 0;
            this.gbUserSelection.TabStop = false;
            this.gbUserSelection.Text = "User Selection";
            // 
            // rbMassFraction
            // 
            this.rbMassFraction.AutoSize = true;
            this.rbMassFraction.Location = new System.Drawing.Point(145, 19);
            this.rbMassFraction.Name = "rbMassFraction";
            this.rbMassFraction.Size = new System.Drawing.Size(91, 17);
            this.rbMassFraction.TabIndex = 1;
            this.rbMassFraction.Text = "Mass Fraction";
            this.rbMassFraction.UseVisualStyleBackColor = true;
            this.rbMassFraction.CheckedChanged += new System.EventHandler(this.rbMassFraction_CheckedChanged);
            // 
            // rbMoleFraction
            // 
            this.rbMoleFraction.AutoSize = true;
            this.rbMoleFraction.Checked = true;
            this.rbMoleFraction.Location = new System.Drawing.Point(12, 19);
            this.rbMoleFraction.Name = "rbMoleFraction";
            this.rbMoleFraction.Size = new System.Drawing.Size(89, 17);
            this.rbMoleFraction.TabIndex = 0;
            this.rbMoleFraction.TabStop = true;
            this.rbMoleFraction.Text = "Mole Fraction";
            this.rbMoleFraction.UseVisualStyleBackColor = true;
            this.rbMoleFraction.CheckedChanged += new System.EventHandler(this.rbMoleFraction_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(174, 88);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(93, 88);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // cbNormalize
            // 
            this.cbNormalize.AutoSize = true;
            this.cbNormalize.Checked = true;
            this.cbNormalize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNormalize.Location = new System.Drawing.Point(12, 65);
            this.cbNormalize.Name = "cbNormalize";
            this.cbNormalize.Size = new System.Drawing.Size(78, 17);
            this.cbNormalize.TabIndex = 3;
            this.cbNormalize.Text = "Normalize?";
            this.cbNormalize.UseVisualStyleBackColor = true;
            this.cbNormalize.CheckedChanged += new System.EventHandler(this.cbNormalize_CheckedChanged);
            // 
            // FractionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 123);
            this.Controls.Add(this.cbNormalize);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbUserSelection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FractionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mole/Mass Fraction?";
            this.Load += new System.EventHandler(this.FractionForm_Load);
            this.gbUserSelection.ResumeLayout(false);
            this.gbUserSelection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbUserSelection;
        private System.Windows.Forms.RadioButton rbMassFraction;
        private System.Windows.Forms.RadioButton rbMoleFraction;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox cbNormalize;
    }
}