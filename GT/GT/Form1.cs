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
        Point u = new Point(0, 0);
        Point LastMouse = new Point(0, 0);
        Graphics g;
        bool S = false;
        int G = 10;
        const int E = 10000;
        public Form1()
        {
            InitializeComponent();

            this.pictureBox1.MouseMove += _MouseMove;

            this.pictureBox1.MouseWheel += (s, e) => {
                if (e.Delta > 0)
                    k = k + 15;
                else
                    k = (k - 15) < 30 ? 30 : k - 15;
                button1_Click(null, null);
            };

            this.pictureBox1.MouseDown += (s, e) =>
            {
                LastMouse = e.Location;
                pictureBox1.Cursor = Cursors.NoMove2D;
                S = true;
            };

            this.pictureBox1.MouseUp += (s, e) =>
            {
                pictureBox1.Cursor = Cursors.Default;
                S = false;
            };

        }

        void _MouseMove(object sender, MouseEventArgs e)
        {
            if (S)
            {
                u.X = e.X - LastMouse.X;
                u.Y = e.Y - LastMouse.Y;
                LastMouse = e.Location;
                button1_Click(null, null);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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
        void themLabo(int n,PictureBox pt)
        {
            Bac_n bac_N = new Bac_n(n);
            Label name = new Label();
            Button ve = new Button();
            if (n > 0) n++;
            else n = -n + 1;
            name.Text = "phương trình có dạng";
            name.Size = new Size(170, 30);
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
            ve.Text = "draw";
            ve.Size = new Size(60, 30);
            ve.Location = new Point(0, n * 30 + 30);
            formInput f = new formInput();
            f.Controls.Add(name);
            f.Controls.Add(pt);
            f.Controls.AddRange(lb);
            f.Controls.AddRange(txt);
            f.Controls.Add(ve);
            f.ShowDialog();
            float[] x = new float[n + 1];
            for (int i = 0; i < n; i++)
            {
                x[i] = Convert.ToSingle(txt[i].Text);
            }
            bac_N.X = x;
            a.Add(bac_N);
        }
        void themLaboDuongTron(int n,PictureBox pt)
        {
            Button ve = new Button();
            ve.Text = "draw";
            Label name = new Label();
            name.Text = "phương trình có dạng";
            name.Size = new Size(180, 30);
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
            ve.Size = new Size(60, 30);
            ve.Location = new Point(0, n * 30 + 30);
            f.Controls.Add(pt);
            f.Controls.Add(name);
            f.Controls.AddRange(lb);
            f.Controls.AddRange(txt);
            f.Controls.Add(ve);
            f.ShowDialog();
            Circle circle = new Circle();
            float[] x = new float[n];
            for(int i = 0; i < n; i++)
            {
                x[i] = Convert.ToSingle(txt[i].Text);   
            }
            circle.X = x;
            a.Add(circle);
        }
        
        private void button2_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem == null)
                return;

            string _input = comboBox1.SelectedItem.ToString();
            
            
            switch (_input)
            {
                case "phương trình đường tròn":
                    PictureBox pt = new PictureBox();
                    pt.Size = new Size(200,50 );
                    pt.Location = new Point(190, 0);
                    pt.BackColor = Color.White;
                    Image ig = Image.FromFile(@"..\\..\\Resources\\lt-b2-chuong-3-sgk-hh-10-0.jpg");
                    pt.SizeMode = PictureBoxSizeMode.AutoSize;
                    pt.Image = ig;
                    
                    int a1 = 3;
                    themLaboDuongTron(a1,pt);
                    break;
                case "phương trình bậc- 1":
                    PictureBox pt1 = new PictureBox();
                    pt1.Size = new Size(200, 50);
                    pt1.Location = new Point(190, 0);
                    pt1.BackColor = Color.White;
                    Image ig1 = Image.FromFile(@"..\\..\\Resources\\Screenshot (56).png");
                    pt1.SizeMode = PictureBoxSizeMode.AutoSize;
                    pt1.Image = ig1;
                    int a2 = -1;
                    themLabo(a2,pt1);
                    break;
                case "phương trình bậc 1":
                    PictureBox pt2 = new PictureBox();
                    pt2.Size = new Size(200, 50);
                    pt2.Location = new Point(190, 0);
                    pt2.BackColor = Color.White;
                    Image ig2 = Image.FromFile(@"..\\..\\Resources\\bac1.png");
                    pt2.SizeMode = PictureBoxSizeMode.AutoSize;
                    pt2.Image = ig2;
                    int a3 = 1;
                    themLabo(a3,pt2);
                    break;
                case "phương trình bậc 2":
                    PictureBox pt3 = new PictureBox();
                    pt3.Size = new Size(200, 50);
                    pt3.Location = new Point(190, 0);
                    pt3.BackColor = Color.White;
                    Image ig3 = Image.FromFile(@"..\\..\\Resources\\unnamed.jpg");
                    pt3.SizeMode = PictureBoxSizeMode.AutoSize;
                    pt3.Image = ig3;
                    int a4 = 2;
                    themLabo(a4,pt3);
                    break;
                case "phương trình bậc 3":
                    PictureBox pt4 = new PictureBox();
                    pt4.Size = new Size(250, 50);
                    pt4.Location = new Point(190, 0);
                    pt4.BackColor = Color.White;
                    Image ig4 = Image.FromFile(@"..\\..\\Resources\\Screenshot (74).png");
                    pt4.SizeMode = PictureBoxSizeMode.AutoSize;
                    pt4.Image = ig4;
                    int a5 = 3;
                    themLabo(a5,pt4);
                    break;
                case "phương trình bậc 4":
                    PictureBox pt5 = new PictureBox();
                    pt5.Size = new Size(200, 50);
                    pt5.Location = new Point(190, 0);
                    pt5.BackColor = Color.White;
                    Image ig5 = Image.FromFile(@"..\\..\\Resources\\Screenshot (47).png");
                    pt5.SizeMode = PictureBoxSizeMode.AutoSize;
                    pt5.Image = ig5;
                    int f = 4;
                    themLabo(f,pt5);
                    break;
                case "phương trình bậc 5":
                    PictureBox pt6 = new PictureBox();
                    pt6.Size = new Size(250, 50);
                    pt6.Location = new Point(190, 0);
                    pt6.BackColor = Color.White;
                    Image ig6 = Image.FromFile(@"..\\..\\Resources\\Screenshot (49).png");
                    pt6.SizeMode = PictureBoxSizeMode.AutoSize;
                    pt6.Image = ig6;
                    int a7 = 5;
                    themLabo(a7,pt6);
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

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Form1_Load(null, null);
            button1_Click(null, null);
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            G = this.pictureBox1.Width * 5;
            x0 = this.pictureBox1.Width / 2;
            y0 = this.pictureBox1.Height / 2;
            Create(null, null);
            button1_Click(null, null);
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            this.Create(null, null);
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

                    while (k < pGraph_1.Length)
                        if (pGraph_1[k].Y.ToString() == float.NaN.ToString()) k++;
                        else break;
                    if(k < pGraph.Length)
                        g.DrawCurve(pen, new PointF[2]{ pGraph[k], pGraph_1[k]});
                    
                    k = pGraph.Length - 1;

                    while (k > 0)
                        if (pGraph_1[k].Y.ToString() == float.NaN.ToString()) k--;
                        else break;
                    if(k > 0)
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
