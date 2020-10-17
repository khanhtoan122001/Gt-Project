using Fcn;
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
        List<Function> a = new List<Function>();
        int max_x, max_y, x0, y0, k = 60;
        Point u = new Point(0, 0);
        float mx;
        Graphics g;
        const int G = 100000;
        const int E = 10000;
        public Form1()
        {
            InitializeComponent();
            


            a.Clear();
            a.Add(new Bac_n(-1));
            float[] x = new float[2];
            x[0] = 1; x[1] = 1;
            a[0].X = x;
            a.Add(new Circle());
            float[] y = new float[2];
            y[0] = 1; y[1] = 2;
            a[1].X = y;



            this.pictureBox1.MouseWheel += (s, e) => {
                if (e.Delta > 0)
                    k = k + 15;
                else
                    k = (k - 15) < 15 ? 15 : k - 15;
                button1_Click(null, null);
            };

            this.pictureBox1.MouseClick += (s, e) =>
            {
                u.X = e.X - x0;
                u.Y = e.Y - y0;
                button1_Click(null, null);
            };

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
            x0 = this.pictureBox1.Width / 2;
            y0 = this.pictureBox1.Height / 2;
            Create(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            frmMain_Resize(null, null);
            VeTruc();
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
            Brush br = new SolidBrush(Color.Black);

            g.DrawString("O", f, br, x0 - 15, y0);
            g.DrawString("x", f, br, max_x - 20, y0 - 20);
            g.DrawString("y", f, br, x0 - 20, 1);
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

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Form1_Load(null, null);
            button1_Click(null, null);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Add(new Label() {Text = "hi", Size = new Size(100,100), Location = new Point(20, 20) });
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            x0 = this.pictureBox1.Width / 2;
            y0 = this.pictureBox1.Height / 2;
            Create(null, null);
            button1_Click(null, null);
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {

            max_x = pictureBox1.Width;
            max_y = pictureBox1.Height;

            x0 += u.X;
            y0 += u.Y;

            u = new Point(0, 0);
        }

        private void VeDoThi()
        {
            
            //mx = Convert.ToSingle(x0) / Convert.ToSingle(k);
            //my = Convert.ToSingle(max_y) / Convert.ToSingle(k);

            for (int i = 0; i < a.Count; i++)
            {
                PointF[] pGraph = SetGraph(a[i], 1);
                PaintGraph(pGraph);
                if (a[i].GetType().ToString() == "Fcn.Circle")
                {
                    PointF[] pGraph_1 = SetGraph(a[i], -1);
                    PaintGraph(pGraph_1);
                    
                    Pen pen = new Pen(Color.Red, 2);
                    int k = 0;

                    while (pGraph_1[k].Y.ToString() == float.NaN.ToString() && k < pGraph_1.Length) k++;
                    g.DrawCurve(pen, new PointF[2]{ pGraph[k], pGraph_1[k]});
                    
                    k = pGraph.Length - 1;
                    
                    while (pGraph_1[k].Y.ToString() == float.NaN.ToString() && k > 0) k--;
                    g.DrawCurve(pen, new PointF[2]{ pGraph[k], pGraph_1[k]});
                
                }
            }
        }

        PointF[] SetGraph(Function a, int C)
        {
            float mx = Convert.ToSingle(max_x);
            int p = 0;
            PointF[] pGraph = new PointF[G];
            for (p = 0; p < G; p++)
            {
                float x, y;
                x = ((mx / Convert.ToSingle(G)) * Convert.ToSingle(p) - x0) / Convert.ToSingle(k);
                y = a.f(x) * C;
                float _x = (x * k) + x0;
                float _y = -(y * k) + y0;
                pGraph[p] = new PointF(_x, _y);
            }
            return pGraph;
        }

        void PaintGraph(PointF[] pGraph)
        {
            Pen pen = new Pen(Color.Red, 2);
            int p = 0;
            while (p < G)
            {
                if (pGraph[p].X >= -E && pGraph[p].Y >= -E && pGraph[p].X <= max_x + E && pGraph[p].Y <= max_y + E)
                {
                    PointF[] d;
                    int f = p, l = 0;
                    while (p < G && pGraph[p].X >= -E && pGraph[p].Y >= -E && pGraph[p].X <= max_x + E && pGraph[p].Y <= max_y + E)
                    {
                        l++; p++;
                    }
                    d = new PointF[l];
                    for (int index = f; index - f < l; index++)
                    {
                        d[index - f] = pGraph[index];
                    }
                    g.DrawCurve(pen, d);
                }
                p++;
            }
        }
    }
}
