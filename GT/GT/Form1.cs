﻿using Fcn;
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
        Function[] a = new Function[5];
        int max_x, max_y, x0, y0, k = 30;
        float mx, my;
        Graphics g;
        public Form1()
        {
            InitializeComponent();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            Create(null, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Create(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            frmMain_Resize(null, null);
            VeTruc();
            a[0] = new Bac_n(5);
            float[] x = new float[6];
            x[0] = 1; x[1] = 0; x[2] = 0; x[3] = 0; x[4] = 0; x[5] = 0;
            a[0].X = x;
            VeDoThi();
        }

        private void Create(object sender, EventArgs e)
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

            int i;
            f = new Font("Tahoma", 8);
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

        private void VeDoThi()
        {
            mx = Convert.ToSingle(max_x) / Convert.ToSingle(k);
            my = Convert.ToSingle(max_y) / Convert.ToSingle(k);

            for (int i = 0; a[i] != null; i++)
            {
                Point[] pGraph = new Point[100];
                for(int p = 0; p < 100; p++)
                {
                    float x, y;
                    x = p * (mx / 100) - mx / 2;
                    y = a[i].f(x);
                    int _x = Convert.ToInt32(x * k) + x0;
                    int _y = -Convert.ToInt32(y * k) + y0;
                    if (_x < 0) _x = -1; if (_x > max_x) _x = max_x + 1;
                    if (_y < 0) _y = -1; if (_y > max_y) _y = max_y + 1;
                    pGraph[p] = new Point(_x, _y);
                }
                Pen pen = new Pen(Color.Red, 2);
                g.DrawCurve(pen, pGraph);
            }
        }
    }
}
