using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GT
{
    public partial class Form1 : Form
    {
        int max_x, max_y, x0, y0;
        Graphics g;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            max_x = pictureBox1.Width;
            max_y = pictureBox1.Height;
            g = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Color.Red, 1);
            g.DrawLine(pen, 0, max_y / 2, max_x, max_y / 2);
            g.DrawLine(pen, max_x / 2, 0, max_x / 2, max_y);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "X: " + e.X.ToString() + "\nY: " + e.Y.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void PaintGraph()
        {
            pictureBox1.Refresh();
            g = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Color.Black, 2);
            g.DrawLine(pen, 0, y0, max_x, y0);
            g.DrawLine(pen, x0, 1, x0, max_y);
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            max_x = pictureBox1.Width;
            max_y = pictureBox1.Height;

            x0 = (int)(max_x / 2);
            y0 = (int)(max_y / 2);

        }
        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
