using Fcn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GT
{
    public partial class Form1 : Form
    {
        List<Function> a = new List<Function>();
        int max_x, max_y, x0, y0, k = 60;
        float mx, my;
        Graphics g;
        const int G = 100000;
        const int E = 10000;
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            Create(null, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Create(null, null);
        }

        //private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    label1.Text = string.Format("X: {0}\nY: {1}", e.X, e.Y);
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            frmMain_Resize(null, null);
            VeTruc();
            a.Clear();
            a.Add(new Bac_n(-1));
            float[] x = new float[2];
            x[0] = 1; x[1] = 1;
            a[0].X = x;
            a.Add(new Circle());
            float[] y = new float[2];
            y[0] = 1; y[1] = 2;
            a[1].X = y;
            VeDoThi();
            //Pen pen = new Pen(Color.Red, 2);
            //g.DrawEllipse(pen, 200f, 200f, 100f, 100f);
            //a.Clear();
            //a.Add(new Circle());
            //a.Add(new Bac_n(2));
            //label2.Text = string.Format("{0}\n{1}", a[0].GetType(), a[1].GetType());
        }

        private void Create(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            frmMain_Resize(null, null);
            VeTruc();
        }

        Label[] lb;
        TextBox[] txt;
        Label labo()
        {
            Label n = new Label();

            n.Size = new Size(60, 30);

            return n;
        }
        TextBox txtBox()
        {
            TextBox t = new TextBox();
            t.Size = new Size(60, 30);
            t.BackColor = Color.White;
            return t;
        }
        void themLabo(int n)
        {
            Label name = new Label();
            name.Text = "phương trình có dạng";
            name.Size = new Size(200, 30);
            name.Location = new Point(0, 0);
            lb = new Label[n];
            txt = new TextBox[n];
            for (int i = 0; i < n; i++)
            {
                lb[i] = labo();
                lb[i].Text = "nhập " + Convert.ToChar('a' + i);
                lb[i].Location = new Point(0, i * 30+30);
                txt[i] = txtBox();
                txt[i].Location = new Point(100, i*30+30);
            }
            formInput f = new formInput();
            f.Controls.Add(name);
            f.Controls.AddRange(lb);
            f.Controls.AddRange(txt);
            f.ShowDialog();
        }
        void themLaboDuongTron(int n)
        {
            Label name = new Label();
            name.Text = "phương trình có dạng";
            name.Size = new Size(200, 30);
            name.Location = new Point(0,0);
            lb = new Label[n];
            txt = new TextBox[n];
            for (int i = 0; i < 3; i++)
            {
                lb[i] = labo();

                lb[i].Location = new Point(0, i * 30+30);
                txt[i] = txtBox();
                txt[i].Location = new Point(100,i*30+30);
            }
            lb[0].Text = "nhập a";
            lb[1].Text = "nhập b";
            lb[2].Text = "nhập R";
            formInput f = new formInput();
            f.Controls.Add(name);
            f.Controls.AddRange(lb);
            f.Controls.AddRange(txt);
            f.ShowDialog();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string _input = combo.SelectedItem.ToString();
            switch (_input)
            {
                case "phương trình đường tròn":
                    int a1 = 3;
                    themLaboDuongTron(a1);
                    break;
                case "phương trình bậc- 1":
                    int a2 = 2;
                    themLabo(a2);
                    break;
                case "phương trình bậc 1":
                    int a3 = 2;
                    themLabo(a3);
                    break;
                case "phương trình bậc 2":
                    int a4 = 3;
                    themLabo(a4);
                    break;
                case "phương trình bậc 3":
                    int a5 = 4;
                    themLabo(a5);
                    break;
                case "phương trình bậc 4":
                    int f = 5;
                    themLabo(f);
                    break;
                case "phương trình bậc 5":
                    int a7 = 6;
                    themLabo(a7);
                    break;

                default:

                    break;
            }
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

            for (int i = 0; i < a.Count; i++)
            {
                PointF[] pGraph = SetGraph(a[i], 1);
                PaintGraph(pGraph);
                if (a[i].GetType().ToString() == "Fcn.Circle")
                {
                    pGraph = SetGraph(a[i], -1);
                    PaintGraph(pGraph);
                }
            }
        }

        PointF[] SetGraph(Function a, int C)
        {
            int p = 0;
            PointF[] pGraph = new PointF[G];
            for (p = 0; p < G; p++)
            {
                float x, y;
                x = p * (mx / G) - mx / 2;
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
