
namespace HBS
{
    partial class UnitBrickForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitBrickForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblChracteristicLengthOfUnitBrick = new System.Windows.Forms.Label();
            this.lblDiameterOfUnitHole = new System.Windows.Forms.Label();
            this.txtChracteristicLengthOfUnitBrick = new System.Windows.Forms.TextBox();
            this.lblTheNumberOfUnitBricks = new System.Windows.Forms.Label();
            this.txtDiameterOfUnitHole = new System.Windows.Forms.TextBox();
            this.txtTheNumberOfUnitBricks = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(282, 187);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(201, 187);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::HBS.Properties.Resources.Unit_brick;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(363, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 200);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblChracteristicLengthOfUnitBrick
            // 
            this.lblChracteristicLengthOfUnitBrick.AutoSize = true;
            this.lblChracteristicLengthOfUnitBrick.Location = new System.Drawing.Point(7, 21);
            this.lblChracteristicLengthOfUnitBrick.Name = "lblChracteristicLengthOfUnitBrick";
            this.lblChracteristicLengthOfUnitBrick.Size = new System.Drawing.Size(205, 13);
            this.lblChracteristicLengthOfUnitBrick.TabIndex = 0;
            this.lblChracteristicLengthOfUnitBrick.Text = "Characteristic Length of Unit Brick, L [mm]";
            // 
            // lblDiameterOfUnitHole
            // 
            this.lblDiameterOfUnitHole.AutoSize = true;
            this.lblDiameterOfUnitHole.Location = new System.Drawing.Point(7, 48);
            this.lblDiameterOfUnitHole.Name = "lblDiameterOfUnitHole";
            this.lblDiameterOfUnitHole.Size = new System.Drawing.Size(147, 13);
            this.lblDiameterOfUnitHole.TabIndex = 2;
            this.lblDiameterOfUnitHole.Text = "Diameter of Unit Hole, D [mm]";
            // 
            // txtChracteristicLengthOfUnitBrick
            // 
            this.txtChracteristicLengthOfUnitBrick.Location = new System.Drawing.Point(239, 17);
            this.txtChracteristicLengthOfUnitBrick.Name = "txtChracteristicLengthOfUnitBrick";
            this.txtChracteristicLengthOfUnitBrick.Size = new System.Drawing.Size(100, 20);
            this.txtChracteristicLengthOfUnitBrick.TabIndex = 1;
            this.txtChracteristicLengthOfUnitBrick.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTheNumberOfUnitBricks
            // 
            this.lblTheNumberOfUnitBricks.AutoSize = true;
            this.lblTheNumberOfUnitBricks.Location = new System.Drawing.Point(7, 75);
            this.lblTheNumberOfUnitBricks.Name = "lblTheNumberOfUnitBricks";
            this.lblTheNumberOfUnitBricks.Size = new System.Drawing.Size(146, 13);
            this.lblTheNumberOfUnitBricks.TabIndex = 4;
            this.lblTheNumberOfUnitBricks.Text = "The Number of Unit Bricks, N";
            // 
            // txtDiameterOfUnitHole
            // 
            this.txtDiameterOfUnitHole.Location = new System.Drawing.Point(239, 44);
            this.txtDiameterOfUnitHole.Name = "txtDiameterOfUnitHole";
            this.txtDiameterOfUnitHole.Size = new System.Drawing.Size(100, 20);
            this.txtDiameterOfUnitHole.TabIndex = 3;
            this.txtDiameterOfUnitHole.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTheNumberOfUnitBricks
            // 
            this.txtTheNumberOfUnitBricks.Location = new System.Drawing.Point(239, 71);
            this.txtTheNumberOfUnitBricks.Name = "txtTheNumberOfUnitBricks";
            this.txtTheNumberOfUnitBricks.Size = new System.Drawing.Size(100, 20);
            this.txtTheNumberOfUnitBricks.TabIndex = 5;
            this.txtTheNumberOfUnitBricks.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTheNumberOfUnitBricks);
            this.groupBox1.Controls.Add(this.txtDiameterOfUnitHole);
            this.groupBox1.Controls.Add(this.lblTheNumberOfUnitBricks);
            this.groupBox1.Controls.Add(this.txtChracteristicLengthOfUnitBrick);
            this.groupBox1.Controls.Add(this.lblDiameterOfUnitHole);
            this.groupBox1.Controls.Add(this.lblChracteristicLengthOfUnitBrick);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 169);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Geometric Parameters";
            // 
            // UnitBrickForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(576, 222);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UnitBrickForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Unit Brick";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblChracteristicLengthOfUnitBrick;
        private System.Windows.Forms.Label lblDiameterOfUnitHole;
        private System.Windows.Forms.TextBox txtChracteristicLengthOfUnitBrick;
        private System.Windows.Forms.Label lblTheNumberOfUnitBricks;
        private System.Windows.Forms.TextBox txtDiameterOfUnitHole;
        private System.Windows.Forms.TextBox txtTheNumberOfUnitBricks;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}