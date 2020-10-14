using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
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

        

        

        private void phươngTrìnhElipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formElip f = new formElip();
            f.ShowDialog();
        }

        private void phươngTrìnhĐườngTrònToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formTron f = new formTron();
            f.ShowDialog();
        }

        private void phươngTrìnhBậc3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formTru3 f = new formTru3();
            f.ShowDialog();        }

        private void phươngTrìnhBậc2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formTru2 f = new formTru2();
            f.ShowDialog();
        }

        private void phươngTrìnhBậcTrừ1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formTru1 f = new formTru1();
            f.ShowDialog();
        }

        private void phươngTrìnhBậc1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formBac1 f = new formBac1();
            f.ShowDialog();
        }

        private void phươngTrìnhBậc2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            formBac2 f = new formBac2();
            f.ShowDialog();
        }

        private void phươngTrìnhBậc3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            formBac3 f = new formBac3();
            f.ShowDialog();
        }

        private void phươngTrìnhBậc4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formBac4 f = new formBac4();
            f.ShowDialog();

        }

        private void phươngTrìnhBậc5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formBac5 f = new formBac5();
            f.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            frmMain_Resize(null, null);
            VeTruc();
        }

      

        private void VeTruc()
        {
            g = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Color.Black, 2);
            g.DrawLine(pen, 0, y0, max_x, y0);
            g.DrawLine(pen, x0, 0, x0, max_y);
            Font f = new Font("Tahoma", 10);
            Brush br = new SolidBrush(Color.Red);

            g.DrawString("O", f, br, x0 - 15, y0);
            g.DrawString("X", f, br, max_x - 20, y0 - 20);
            g.DrawString("Y", f, br, x0 - 20, 1);
            Pen pen_x = new Pen(Color.Gray, 1);

            int i, k = 30;
            f = new Font("Tahoma", 7);
            for (i = x0 + k; i < max_x; i += k)
            {
                g.DrawLine(pen_x, i, y0 - 3, i, y0 + 2);
                g.DrawString(((i - x0)/ k).ToString(), f, br, i - 7, y0 + 3);
            }
            for (i = x0 - k; i > 0; i -= k)
            {
                g.DrawLine(pen_x, i, y0 - 3, i, y0 + 2);
                g.DrawString(((i - x0) / k).ToString(), f, br, i - 7, y0 + 3);
            }

            for (i = y0 + k; i < max_y; i += k)
            {
                g.DrawLine(pen_x, x0 - 3, i, x0 + 2, i);
                g.DrawString((-(i - y0) / k).ToString(), f, br, x0 + 3, i - 7);
            }

            for (i = y0 - k; i > 0; i -= k)
            {
                g.DrawLine(pen_x, x0 - 3, i, x0 + 2, i);
                g.DrawString((-(i - y0) / k).ToString(), f, br, x0 + 3, i - 7);
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            max_x = pictureBox1.Width;
            max_y = pictureBox1.Height;

            x0 = (int)(max_x / 2);
            y0 = (int)(max_y / 2);

        }
    }
}
