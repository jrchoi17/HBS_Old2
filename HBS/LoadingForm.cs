using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HBS
{
    public partial class LoadingForm : Form
    {
        private Bitmap bit;

        public LoadingForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            bit = new Bitmap(Application.StartupPath + "\\Resources\\AnimationStyle5.gif");

            ImageAnimator.Animate(bit, new EventHandler(this.OnFrameChanged));
            base.OnLoad(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ImageAnimator.UpdateFrames();

            Graphics g = pictureBox1.CreateGraphics();
            g.DrawImage(this.bit, new Point(0, 0));
            base.OnPaint(e);
        }

        private void OnFrameChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
