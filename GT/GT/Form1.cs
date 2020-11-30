﻿using Fcn;
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
using System.IO;
using System.Diagnostics;
namespace GT
{
    public partial class Form1 : Form
    {
        float[] C_dv = { 1f, 2f, 5f};
        Theme theme = new Theme();
        const int MinZoom = 120, MaxZoom = 180, Normal = 100; 
        List<Function> a = new List<Function>();
        List<UserControl1> ListFnc = new List<UserControl1>();
        int max_x, max_y, x0, y0, k = 120, idv = 0;
        double dv = 1;
        const float MaxDv = 500, MinDv = 0.001f;
        Point u = new Point(0, 0);
        Point LastMouse = new Point(0, 0);
        Graphics g;
        Bitmap bitmap;
        bool S = false, Dark = false, LuoiNho = true;
        int G = 10;
        const int E = 10000;
        const float Zoom = 1.1f;
        public Form1()
        {
            InitializeComponent();

            ListFnc.Add(create_UserControl1());

            flowLayoutPanel1.Controls.Add(ListFnc[ListFnc.Count-1]);

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
                    if ((k / Zoom) < Normal)
                    {
                        k = MaxZoom;
                        if(dv < MaxDv) SetDv(false);
                    }
                    else { k = (int)(k / Zoom); }
                    u.X = (int)(-((_x / dv) * k) + (e.Location.X - x0));
                    u.Y = (int)(-((_y / dv) * k) + (e.Location.Y - y0));
                }
                DrawGr();
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
                DrawGr();
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
            flowLayoutPanel1_SizeChanged(null, null);
            Create();
        }

        private void DrawGr()
        {
            pictureBox1.Refresh();

            frmMain_Resize(null, null);
            VeTruc();
            VeDoThi();
        }

        private void Create()
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
                addListFcn();
                f.Close();
            };
            f.ShowDialog();
            DrawGr();
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
                addListFcn();
                f.Close();
            };
            f.ShowDialog();
            DrawGr();
        }

        private void VeTruc()
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bitmap;
            g = Graphics.FromImage(bitmap);
            {
                g.Clear(theme.BackGroundPic);

                VeLuoi();

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Pen pen = new Pen(theme.TextColor, 2);
                g.DrawLine(pen, 0, y0, max_x, y0);
                g.DrawLine(pen, x0, 0, x0, max_y);
                Font f = new Font("Arial", 12);
                Brush br = new SolidBrush(theme.TextColor);
                g.DrawString("O", f, br, x0, y0);
                g.DrawString("x", f, br, max_x - 20, y0 - 20);
                g.DrawString("y", f, br, x0 - 20, 1);
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Form1_Load(null, null);
            DrawGr();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            G = this.pictureBox1.Width * 5;
            x0 = this.pictureBox1.Width / 2;
            y0 = this.pictureBox1.Height / 2;
            Create();
            DrawGr();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            a.Clear();
            ListFnc.Clear();
            Refresh_ListFcn();
            flowLayoutPanel1.Controls.Clear();
            ListFnc.Add(create_UserControl1());
            flowLayoutPanel1.Controls.Add(ListFnc[ListFnc.Count - 1]);
            flowLayoutPanel1_SizeChanged(null, null);
            this.Create();
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

        private void darkThemeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Dark = !Dark;
            theme.ChangeAll();
            foreach(Function i in a)
            {
                int r = i.color.R, g = i.color.G, b = i.color.B;
                i.color = Color.FromArgb(255 - r, 255 - g, 255 - b);
                SetColorItemList();
            }
            refresh_form();
            DrawGr();
        }
        private void refresh_form()
        {
            this.BackColor = theme.Bg1;
            this.flowLayoutPanel1.BackColor = theme.Bg2;
            this.toolStrip1.BackColor = theme.Bg3;
            this.panel2.BackColor = theme.Bg3;
        }
        private void frmMain_Resize(object sender, EventArgs e)
        {

            max_x = pictureBox1.Width;
            max_y = pictureBox1.Height;

            x0 += u.X;
            y0 += u.Y;

            u = new Point(0, 0);
        }

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            foreach(UserControl1 i in ListFnc)
            {
                i.Width = flowLayoutPanel1.Width - 2;
            }
        }

        private void splitContainer1_Panel1_SizeChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Width = splitContainer1.Panel1.Width - 7;
        }

        private void VeDoThi()
        {

            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].Enable)
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
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (a.Count == 0)
                return;
            
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter file = new StreamWriter(saveFileDialog1.FileName))
                {
                    file.WriteLine(a.Count);
                    foreach(Function i in a)
                    {
                        file.WriteLine("*=*=*");
                        file.Write(i.SaveString());
                    }
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < ListFnc.Count - 1; i++)
            {
                if (ListFnc[i].selected)
                {
                    flowLayoutPanel1.Controls.Remove(ListFnc[i]);
                    ListFnc.RemoveAt(i);
                    a.RemoveAt(i);
                    i--;
                    Refresh_ListFcn();
                    DrawGr();
                }
            }
            if (a.Count==0)
            {
                saveToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
                savetoolStripButton3.Enabled = false;
                saveastoolStripButton4.Enabled = false;
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "jpg files (*.jpg)|*.jpg|png files (*.png)|*.png";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
                saveFileDialog1.FileName = "";
            }
        }

        private void lướiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LuoiNho = !LuoiNho;
            DrawGr();
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
        }

        private void buttonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = !splitContainer2.Panel2Collapsed;
        }

        private void toolStrip1_BackColorChanged(object sender, EventArgs e)
        {
            if (Dark)
            {
                newtoolStripButton1.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-add-file-80-dark.png");
                Open.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-opened-folder-144-dark.png");
                savetoolStripButton3.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-save-100-dark.png");
                saveastoolStripButton4.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-save-as-100-dark.png");
                deletetoolStripButton2.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-delete-bin-96-dark.png");
                exittoolStripButton5.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-exit-52-dark.png");
            }
            else
            {
                newtoolStripButton1.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-add-file-80.png");
                Open.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-opened-folder-144.png");
                savetoolStripButton3.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-save-100.png");
                saveastoolStripButton4.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-save-as-100.png");
                deletetoolStripButton2.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-delete-bin-96.png");
                exittoolStripButton5.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-exit-52.png");
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
            Pen pen_x = new Pen(theme.Nest, 2);
            Pen pen_n = new Pen(theme.Tail, 1);
            int i, xd = 0, yd = 0;
            float n = ((float)k / 5);
            Brush br = new SolidBrush(theme.TextColor);
            Font f = new Font("Arial", 12);
            //for (int j = 1; j <= 5; j++)
            //{
            //    g.DrawLine(pen_n, x0 + j * n, 0, x0 + j * n, max_y);
            //    g.DrawLine(pen_n, x0 - j * n, 0, x0 - j * n, max_y);
            //    g.DrawLine(pen_n, 0, y0 - j * n, max_x, y0 - j * n);
            //    g.DrawLine(pen_n, 0, y0 + j * n, max_x, y0 + j * n);
            //}
            for (i = x0 + k; i < max_x; i += k)
            {
                yd = y0;
                if (y0 < 0) yd = 3;
                if (y0 > max_y) yd = max_y - 20;
                g.DrawLine(pen_x, i, 0, i, max_y);
                g.DrawString(((i - x0) / k * dv).ToString(), f, br, i, yd);
            }
            for (i = x0 - k; i > 0; i -= k)
            {
                yd = y0;
                if (y0 < 0) yd = 3;
                if (y0 > max_y) yd = max_y - 20;
                g.DrawLine(pen_x, i, 0, i, max_y);
                g.DrawString(((i - x0) / k * dv).ToString(), f, br, i, yd);
            }
            for (i = y0 + k; i < max_y; i += k)
            {
                xd = x0;
                if (x0 < 0) xd = 3;
                if (x0 > max_x) xd = max_x - 20;
                g.DrawLine(pen_x, 0, i, max_x, i);
                g.DrawString((-(i - y0) * dv / k).ToString(), f, br, xd, i);
            }
            for (i = y0 - k; i > 0; i -= k)
            {
                xd = x0;
                if (x0 < 0) xd = 3;
                if (x0 > max_x) xd = max_x - 20;
                g.DrawLine(pen_x, 0, i, max_x, i);
                g.DrawString((-(i - y0) * dv / k).ToString(), f, br, xd, i);
            }
            if(LuoiNho) VeLuoiNho();
        }
        private void VeLuoiNho()
        {
            Pen pen_x = new Pen(theme.Nest, 2);
            Pen pen_n = new Pen(theme.Tail, 1);
            int i;
            float n = ((float)k / 5);
            for (int j = 1; j <= 5; j++)
            {
                g.DrawLine(pen_n, x0 + j * n, 0, x0 + j * n, max_y);
                g.DrawLine(pen_n, x0 - j * n, 0, x0 - j * n, max_y);
                g.DrawLine(pen_n, 0, y0 - j * n, max_x, y0 - j * n);
                g.DrawLine(pen_n, 0, y0 + j * n, max_x, y0 + j * n);
            }
            for (i = x0 + k; i < max_x; i += k)
            {
                for (int j = 1; j <= 5; j++)
                    g.DrawLine(pen_n, i + j * n, 0, i + j * n, max_y);
            }
            for (i = x0 - k; i > 0; i -= k)
            {
                for (int j = 1; j <= 5; j++)
                    g.DrawLine(pen_n, i - j * n, 0, i - j * n, max_y);
            }
            for (i = y0 + k; i < max_y; i += k)
            {
                for (int j = 1; j <= 5; j++)
                    g.DrawLine(pen_n, 0, i + j * n, max_x, i + j * n);
            }
            for (i = y0 - k; i > 0; i -= k)
            {
                for (int j = 1; j <= 5; j++)
                    g.DrawLine(pen_n, 0, i - j * n, max_x, i - j * n);
            }
        }
        int Pow10(int i)
        {
            int r = 1;
            for(int n = 0; n < i; n++)
                r *= 10;
            return r;
        }

        UserControl1 create_UserControl1()
        {
            UserControl1 n = new UserControl1();
            //n.textBox1.Visible = false;
            n.checkBox1.Checked = true;
            n.checkBox1.CheckedChanged += (s, e) =>
            {
                if(n.Tag != null)
                    a[(int)n.Tag].Enable = !a[(int)n.Tag].Enable;
                DrawGr();
            };
            return n;
        }

        private void Refresh_ListFcn()
        {
            for(int i = 0; i < ListFnc.Count - 1; i++)
            {
                ListFnc[i].color = a[i].color;
                ListFnc[i].Change_Color();
                ListFnc[i].Tag = i;
            }
        }

        private void addListFcn()
        {
            ListFnc.Add(create_UserControl1());
            ListFnc[ListFnc.Count - 2].textBox1.Text = a[a.Count - 1].ToString();
            flowLayoutPanel1.Controls.Add(ListFnc[ListFnc.Count - 1]);
            Refresh_ListFcn();
            flowLayoutPanel1_SizeChanged(null, null);
            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            savetoolStripButton3.Enabled = true;
            saveastoolStripButton4.Enabled = true;
        }

        private void SetColorItemList()
        {
            foreach(UserControl1 i in ListFnc)
            {
                if(i.Tag != null)
                {
                    i.color = a[(int)i.Tag].color;
                    i.Change_Color();
                }
            }
        }
    }
    
}
