using Fcn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GT
{
    public partial class Form1 : Form
    {
        float[] C_dv = { 1f, 2f, 5f};
        const int MinZoom = 100, MaxZoom = 360, Normal = 180; 
        List<Function> a = new List<Function>();
        int max_x, max_y, x0, y0, k = 120, idv = 0;
        double dv = 1;
        const float MaxDv = 500, MinDv = 0.001f;
        Point u = new Point(0, 0);
        Point LastMouse = new Point(0, 0);
        Graphics g;
        Bitmap bitmap;
        bool S = false;
        int G = 10;
        const int E = 10000;
        const float Zoom = 1.1f;
        public Form1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.pictureBox1.MouseMove += _MouseMove;

            this.pictureBox1.MouseWheel += (s, e) => {
                float _x = (float)dv * (e.Location.X - x0) / (float)k;
                float _y = (float)dv * (e.Location.Y - y0) / (float)k;
                if (e.Delta > 0)
                {
                    if ((k * Zoom) > MaxZoom)
                    {
                        k = Normal;
                        if(dv > MinDv) SetDv(true);
                    }
                    else { k = (int)(k * Zoom); }
                    u.X = -(int)(((_x / dv) * k) - (e.Location.X - x0));
                    u.Y = -(int)(((_y / dv) * k) - (e.Location.Y - y0));
                }
                else
                {
                    if ((k / Zoom) < MinZoom)
                    {
                        k = Normal;
                        if(dv < MaxDv) SetDv(false);
                    }
                    else { k = (int)(k / Zoom); }
                    u.X = (int)(-((_x / dv) * k) + (e.Location.X - x0));
                    u.Y = (int)(-((_y / dv) * k) + (e.Location.Y - y0));
                }
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
            this.DoubleBuffered = true;

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
            t.KeyDown += (s, e) =>
            {
                if (e.KeyValue == 13)
                {
                    SendKeys.Send("{TAB}");
                }
            };
            return t;
        }
        void themLabo(int n, PictureBox pt)
        {
            Bac_n bac_N = new Bac_n(n);
            Label name = new Label();
            Button ve = new Button();
            if (n > 0) n++;
            else n = -n + 1;
            name.Text = "Phương trình có dạng";
            name.Size = new Size(170, 30);
            name.Location = new Point(0, 0);
            lb = new Label[n];
            txt = new TextBox[n];
            for (int i = 0; i < n; i++)
            {
                lb[i] = labo();
                lb[i].Text = string.Format("Nhập {0}", (char)('a' + i));//"Nhập " + Convert.ToChar('a' + i);
                lb[i].Location = new Point(0, i * 30 + 30);
                txt[i] = txtBox();
                txt[i].Location = new Point(100, i * 30 + 30);
            }
            ve.Text = "OK";
            ve.Size = new Size(60, 30);
            ve.Location = new Point(0, n * 30 + 30);
            Form f = createFormInput();
            f.Controls.Add(name);
            f.Controls.Add(pt);
            f.Controls.AddRange(lb);
            f.Controls.AddRange(txt);
            f.Controls.Add(ve);
            ve.Click += (s, e) =>
            {
                float[] x = new float[n + 1];
                for (int i = 0; i < n; i++)
                {
                    float o;
                    if (!float.TryParse(txt[i].Text, out o))
                    {
                        MessageBox.Show("Nhập số", "Lỗi");
                        return;
                    }
                    if (txt[i].Text == string.Empty)
                    {
                        MessageBox.Show("Nhập đầy đủ giá trị", "Lỗi");
                        return;
                    }
                    x[i] = Convert.ToSingle(txt[i].Text);
                }
                bac_N.X = x;
                a.Add(bac_N);
                f.Close();
            };
            f.ShowDialog();
            button1_Click(null, null);
        }
        void themLaboDuongTron(int n, PictureBox pt)
        {
            Button ve = new Button();
            ve.Text = "OK";

            Label name = new Label();
            name.Text = "Phương trình có dạng";
            name.Size = new Size(180, 30);
            name.Location = new Point(0, 0);
            lb = new Label[n];
            txt = new TextBox[n];
            for (int i = 0; i < 3; i++)
            {
                lb[i] = labo();

                lb[i].Location = new Point(0, i * 30 + 30);
                txt[i] = txtBox();
                txt[i].Location = new Point(100, i * 30 + 30);
            }
            lb[0].Text = "Nhập a";
            lb[1].Text = "Nhập b";
            lb[2].Text = "Nhập R";
            Form f = createFormInput();
            ve.Size = new Size(60, 30);
            ve.Location = new Point(0, n * 30 + 30);
            f.Controls.Add(pt);
            f.Controls.Add(name);
            f.Controls.AddRange(lb);
            f.Controls.AddRange(txt);
            f.Controls.Add(ve);
            ve.Click += (s, e) =>
            {
                Circle circle = new Circle();
                float[] x = new float[n];
                for (int i = 0; i < n; i++)
                {
                    if (txt[i].Text == string.Empty)
                    {
                        MessageBox.Show("Nhập đầy đủ giá trị", "Lỗi");
                        return;
                    }
                    x[i] = Convert.ToSingle(txt[i].Text);
                    if (i == 2) if (x[i] < 0)
                        {
                            MessageBox.Show("R phải lớn hơn 0");
                            return;
                        }
                }
                circle.X = x;
                a.Add(circle);
                f.Close();
            };
            f.ShowDialog();
            button1_Click(null, null);
        }



        private void VeTruc()
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bitmap;
            g = Graphics.FromImage(bitmap);
            {
                VeLuoi();

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Pen pen = new Pen(Color.FromArgb(0, 0, 0), 2);
                g.DrawLine(pen, 0, y0, max_x, y0);
                g.DrawLine(pen, x0, 0, x0, max_y);
                Font f = new Font("Arial", 12);

                g.DrawString("O", f, Brushes.Black, x0, y0);
                g.DrawString("x", f, Brushes.Black, max_x - 20, y0 - 20);
                g.DrawString("y", f, Brushes.Black, x0 - 20, 1);

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
            a.Clear();
            this.Create(null, null);
        }

        private void phươngTrìnhBậc1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pt2 = new PictureBox();
            pt2.Size = new Size(200, 50);
            pt2.Location = new Point(190, 0);
            pt2.BackColor = Color.White;
            Image ig2 = Image.FromFile(@"..\\..\\Resources\\bac1.png");
            pt2.SizeMode = PictureBoxSizeMode.AutoSize;
            pt2.Image = ig2;
            int a3 = 1;
            themLabo(a3, pt2);
        }

        private void phươngTrìnhBậc2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pt3 = new PictureBox();
            pt3.Size = new Size(200, 50);
            pt3.Location = new Point(190, 0);
            pt3.BackColor = Color.White;
            Image ig3 = Image.FromFile(@"..\\..\\Resources\\unnamed.jpg");
            pt3.SizeMode = PictureBoxSizeMode.AutoSize;
            pt3.Image = ig3;
            int a4 = 2;
            themLabo(a4, pt3);
        }

        private void phươngTrìnhBậc3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pt4 = new PictureBox();
            pt4.Size = new Size(250, 50);
            pt4.Location = new Point(190, 0);
            pt4.BackColor = Color.White;
            Image ig4 = Image.FromFile(@"..\\..\\Resources\\Screenshot (74).png");
            pt4.SizeMode = PictureBoxSizeMode.AutoSize;
            pt4.Image = ig4;
            int a5 = 3;
            themLabo(a5, pt4);
        }

        private void phươngTrìnhBậc4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pt5 = new PictureBox();
            pt5.Size = new Size(200, 50);
            pt5.Location = new Point(190, 0);
            pt5.BackColor = Color.White;
            Image ig5 = Image.FromFile(@"..\\..\\Resources\\Screenshot (47).png");
            pt5.SizeMode = PictureBoxSizeMode.AutoSize;
            pt5.Image = ig5;
            int f = 4;
            themLabo(f, pt5);
        }

        private void phươngTrìnhBậc5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pt6 = new PictureBox();
            pt6.Size = new Size(250, 50);
            pt6.Location = new Point(190, 0);
            pt6.BackColor = Color.White;
            Image ig6 = Image.FromFile(@"..\\..\\Resources\\Screenshot (49).png");
            pt6.SizeMode = PictureBoxSizeMode.AutoSize;
            pt6.Image = ig6;
            int a7 = 5;
            themLabo(a7, pt6);
        }

        private void phươngTrìnhĐườngTrònToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pt = new PictureBox();
            pt.Size = new Size(200, 50);
            pt.Location = new Point(190, 0);
            pt.BackColor = Color.White;
            Image ig = Image.FromFile(@"..\\..\\Resources\\lt-b2-chuong-3-sgk-hh-10-0.jpg");
            pt.SizeMode = PictureBoxSizeMode.AutoSize;
            pt.Image = ig;
            int a1 = 3;
            themLaboDuongTron(a1, pt);
        }

        private void phươngTrìnhĐặcBiệtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pt1 = new PictureBox();
            pt1.Size = new Size(200, 50);
            pt1.Location = new Point(190, 0);
            pt1.BackColor = Color.White;
            Image ig1 = Image.FromFile(@"..\\..\\Resources\\Screenshot (56).png");
            pt1.SizeMode = PictureBoxSizeMode.AutoSize;
            pt1.Image = ig1;
            int a2 = -1;
            themLabo(a2, pt1);
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

            for (int i = 0; i < a.Count; i++)
            {
                PointF[] pGraph;
                if (a[i].GetType().ToString() != "Fcn.Circle")
                {
                    pGraph = SetGraph(a[i]);
                    PaintGraph(pGraph, i);
                }
                else
                {
                    Circle p = (Circle)a[i];
                    g.FillEllipse(new SolidBrush(p.color), p.I.X / (float)dv * k + x0 - 5, -p.I.Y / (float)dv * k + y0 - 5, 10, 10);
                    g.DrawEllipse(new Pen(p.color, 2), ((p.A - p.R) / (float)dv * k + x0), ((-p.B - p.R) / (float)dv * k + y0), ((p.R * 2) * k) / (float)dv, ((p.R * 2) * k) / (float)dv);
                }
            }
        }

        PointF[] SetGraph(Function a)
        {
            float mx = Convert.ToSingle(max_x);
            int p = 0;
            PointF[] pGraph = new PointF[G];
            for (p = 0; p < G; p++)
            {
                float x, y;
                x = ((mx / Convert.ToSingle(G)) * Convert.ToSingle(p) - x0) / Convert.ToSingle(k) * (float)dv;
                y = a.f(x);
                float _x = (x / (float)dv * k) + x0;
                float _y = -(y / (float)dv * k) + y0;
                pGraph[p] = new PointF(_x, _y);
            }
            return pGraph;
        }

        void PaintGraph(PointF[] pGraph, int i)
        {
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
                    g.DrawCurve(new Pen(a[i].color, 2), d);
                }
                p++;
            }
        }

        Form createFormInput()
        {
            Form f = new Form();
            f.AutoSize = true;
            f.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            f.Font = new Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            f.Margin = new System.Windows.Forms.Padding(4);
            f.Name = "formInput";
            f.Text = "formInput";
            f.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            f.ResumeLayout(false);
            return f;
        }

        void SetDv(bool c_i)
        {
            if (c_i) idv--;
            else idv++;
            if(idv >= 0)
            {
                dv = C_dv[idv % 3] * Pow10(idv / 3);
            }
            else
            {
                int i = 2 - (-idv + 2) % 3;
                dv = C_dv[i] / Pow10(((-idv + 2) / 3));
            }
            if(idv == -5)
            {
                c_i = true;
            }
        }

        void VeLuoi()
        {
            Pen pen_x = new Pen(Color.FromArgb(150, 150, 150), 2);
            Pen pen_n = new Pen(Color.FromArgb(200, 200, 200), 1);
            int i, xd = 0, yd = 0;
            float n = ((float)k / 5);
            Font f = new Font("Arial", 12);
            for (int j = 1; j <= 5; j++)
            {
                g.DrawLine(pen_n, x0 + j * n, 0, x0 + j * n, max_y);
                g.DrawLine(pen_n, x0 - j * n, 0, x0 - j * n, max_y);
                g.DrawLine(pen_n, 0, y0 - j * n, max_x, y0 - j * n);
                g.DrawLine(pen_n, 0, y0 + j * n, max_x, y0 + j * n);
            }
            for (i = x0 + k; i < max_x; i += k)
            {
                yd = y0;
                if (y0 < 0) yd = 3;
                if (y0 > max_y) yd = max_y - 20;
                g.DrawLine(pen_x, i, 0, i, max_y);
                for (int j = 1; j <= 5; j++)
                    g.DrawLine(pen_n, i + j * n, 0, i + j * n, max_y);
                g.DrawString(((i - x0) / k * dv).ToString(), f, Brushes.Black, i, yd);
            }
            for (i = x0 - k; i > 0; i -= k)
            {
                yd = y0;
                if (y0 < 0) yd = 3;
                if (y0 > max_y) yd = max_y - 20;
                g.DrawLine(pen_x, i, 0, i, max_y);
                for (int j = 1; j <= 5; j++)
                    g.DrawLine(pen_n, i - j * n, 0, i - j * n, max_y);
                g.DrawString(((i - x0) / k * dv).ToString(), f, Brushes.Black, i, yd);
            }
            for (i = y0 + k; i < max_y; i += k)
            {
                xd = x0;
                if (x0 < 0) xd = 3;
                if (x0 > max_x) xd = max_x - 20;
                g.DrawLine(pen_x, 0, i, max_x, i);
                for (int j = 1; j <= 5; j++)
                    g.DrawLine(pen_n, 0, i + j * n, max_x, i + j * n);
                g.DrawString((-(i - y0) * dv / k).ToString(), f, Brushes.Black, xd, i);
            }
            for (i = y0 - k; i > 0; i -= k)
            {
                xd = x0;
                if (x0 < 0) xd = 3;
                if (x0 > max_x) xd = max_x - 20;
                g.DrawLine(pen_x, 0, i, max_x, i);
                for (int j = 1; j <= 5; j++)
                    g.DrawLine(pen_n, 0, i - j * n, max_x, i - j * n);
                g.DrawString((-(i - y0) * dv / k).ToString(), f, Brushes.Black, xd, i);
            }
        }
        int Pow10(int i)
        {
            int r = 1;
            for(int n = 0; n < i; n++)
                r *= 10;
            return r;
        }
    }

}
