using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GT
{
    public partial class UserControl1 : UserControl
    {
        //Image img = Image.FromFile(@"..\\..\\Resources\\ColorWheel.png");
        Bitmap b;
        Graphics g;
        Rectangle r;
        public UserControl1()
        {
            InitializeComponent();
            selected = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void UserControl1_Click(object sender, EventArgs e)
        {
            if (this.Tag != null)
            {
                selected = !selected;
                r = new Rectangle(0, 0, this.Width, this.Height);
                b = new Bitmap(this.Width, this.Height);
                g = Graphics.FromImage(b);
                if (selected)
                {
                    ControlPaint.DrawBorder(g, r, Color.FromArgb(0, 255, 255), ButtonBorderStyle.Solid);
                }
                else
                {
                    g.Clear(Color.White);
                }
                this.BackgroundImage = b;
            }
        }
    }
}
