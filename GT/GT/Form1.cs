using Fcn;
using System;
using System.Collections;
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
    enum mouse {none, export, s_point};
    public partial class Form1 : Form
    {
        float[] C_dv = { 1f, 2f, 5f};
        Theme theme = new Theme();
        string pathFile = string.Empty;
        const int MaxZoom = 180, Normal = 100; 
        List<Function> ListFcn = new List<Function>();
        List<UserControl1> ListFcnControls = new List<UserControl1>();
        int max_x, max_y, x0, y0, k = 120, idv = 0, countName = -1;
        double dv = 1;
        const float MaxDv = 500, MinDv = 0.001f;
        Point u = new Point(0, 0);
        Point LastMouse = new Point(0, 0), p_Export;
        char name = 'Z';
        Graphics g;
        Bitmap MainBitmap;
        bool isMouseDown = false, isSave = false, Dark = false, LuoiNho = true, SwExport = false;
        int G = 10;
        const int E = 10000;
        const float Zoom = 1.1f;
        mouse c_mouse = mouse.none;
        
        /* mảng lưu giá trị a,b,r cho đường tròn */
        public static float[] arr = new float[3];
        /*mảng lưu các giá trị cho các đường khác*/
        public static float[] arr1 = new float[2]; // đặc biệt
        public static float[] arr2 = new float[2];//bậc 1
        public static float[] arr3 = new float[3];//bậc 2
        public static float[] arr4 = new float[4];//bậc 3
        public static float[] arr5 = new float[5];//bậc 4
        public static float[] arr6 = new float[6];//bậc 5
        public static bool[] flat = new bool[] {false,false,false,false,false,false,false};


        public Form1()
        {
            InitializeComponent();

            ListFcnControls.Add(create_UserControl1());

            flowLayoutPanel1.Controls.Add(ListFcnControls[ListFcnControls.Count-1]);

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
                if (!isMouseDown)
                {
                    SwExport = false;
                    p_Export = e.Location;
                }
                LastMouse = e.Location;
                isMouseDown = true;
                switch (c_mouse)
                {
                    case mouse.none:
                        pictureBox1.Cursor = Cursors.SizeAll;
                        break;
                    case mouse.export:
                        pictureBox1.Cursor = Cursors.Cross;
                        break;
                    case mouse.s_point:
                        pictureBox1.Cursor = Cursors.Hand;
                        DrawGr();
                        g.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(new Point(e.X - 5, e.Y - 5), new Size(10, 10)));
                        break;
                    default:
                        break;
                }
            };

            this.pictureBox1.MouseUp += (s, e) =>
            {
                pictureBox1.Cursor = Cursors.Default;
                isMouseDown = false;
                if(c_mouse == mouse.s_point)
                {
                    ListFcn.Add(new PointG(PointName(), e.Location, new Point(x0, y0), k, (float)dv));
                    if (Dark)
                        ListFcn[ListFcn.Count - 1].color = Color.White;
                    else
                        ListFcn[ListFcn.Count - 1].color = Color.Black;
                    addListFcn();
                }
                SwExport = true;
            };
            
            this.DoubleBuffered = true;

        }

        void _MouseMove(object sender, MouseEventArgs e)
        {
            switch (c_mouse)
            {
                case mouse.none:
                    if (isMouseDown)
                    {
                        u.X = e.X - LastMouse.X;
                        u.Y = e.Y - LastMouse.Y;
                        LastMouse = e.Location;
                        DrawGr();
                    }
                    break;
                case mouse.export:
                    if (isMouseDown)
                    {
                        DrawGr();
                        g.DrawRectangle(new Pen(theme.Bg1), new Rectangle(p_Export, new Size(e.X - p_Export.X, e.Y - p_Export.Y)));
                    }
                    else
                    {
                        if (SwExport)
                        {
                            if(e.X - p_Export.X > 10 && e.Y - p_Export.Y > 10)
                            {
                                Rectangle rectangle = new Rectangle(p_Export, new Size(e.X - p_Export.X, e.Y - p_Export.Y));
                                exportPic(rectangle);
                            }
                            SwExport = !SwExport;
                        }
                        DrawGr();
                    }
                    break;
                case mouse.s_point:
                    if (isMouseDown)
                    {
                        DrawGr();
                        g.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(new Point(e.X - 5, e.Y - 5), new Size(10, 10)));
                    }
                    break;
                default:
                    break;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            x0 = this.pictureBox1.Width / 2;
            y0 = this.pictureBox1.Height / 2;
            flowLayoutPanel1_SizeChanged(null, null);
            //Create();
        }
        /**********************************************************************************************************/
        public void DrawGr()
        {
            pictureBox1.Refresh();

            frmMain_Resize(null, null);
            VeTruc();
            VeDoThi();
        }
        /************************************************************/
        private void Create()
        {
            pictureBox1.Refresh();
            G = this.pictureBox1.Width * 5;
            x0 = this.pictureBox1.Width / 2;
            y0 = this.pictureBox1.Height / 2;
            //frmMain_Resize(null, null);
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
                
                ListFcn.Add(circle);
                addListFcn();
                f.Close();
            };
            f.ShowDialog();
            DrawGr();
        }
            /***************************************************************************************************************************/
            /*****************************************************************************************************************************/
        private void VeTruc()
        {
            MainBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = MainBitmap;
            g = Graphics.FromImage(MainBitmap);
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
            //Create();
            DrawGr();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            ListFcn.Clear();
            ListFcnControls.Clear();
            Refresh_ListFcn();
            flowLayoutPanel1.Controls.Clear();
            ListFcnControls.Add(create_UserControl1());
            flowLayoutPanel1.Controls.Add(ListFcnControls[ListFcnControls.Count - 1]);
            flowLayoutPanel1_SizeChanged(null, null);
            this.Create();
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

        private void darkThemeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Dark = !Dark;
            theme.ChangeAll();
            foreach(Function i in ListFcn)
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
            //this.toolStrip2.BackColor = theme.Bg3;
        }
        private void frmMain_Resize(object sender, EventArgs e)
        {

            max_x = pictureBox1.Width;
            max_y = pictureBox1.Height;
            if(c_mouse == mouse.none)
            {
                x0 += u.X;
                y0 += u.Y;
                u = new Point(0, 0);
            }
            
        }

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            int io = 0;
            if ((ListFcnControls.Count * new UserControl1().Height - flowLayoutPanel1.Height) > 0)
                io = 13;
            foreach(UserControl1 i in ListFcnControls)
            {
                i.Width = flowLayoutPanel1.Width - 7 - io;
            }
        }

        private void VeDoThi()
        {

            for (int i = 0; i < ListFcn.Count; i++)
            {
                if (ListFcn[i].Enable)
                {
                    PointF[] pGraph;
                    switch (ListFcn[i].GetType().ToString())
                    {
                        case "Fcn.Circle":
                            Circle p = (Circle)ListFcn[i];
                            g.FillEllipse(new SolidBrush(p.color), p.I.X / (float)dv * k + x0 - 5, -p.I.Y / (float)dv * k + y0 - 5, 10, 10);
                            g.DrawEllipse(new Pen(p.color, 2), ((p.A - p.R) / (float)dv * k + x0), ((-p.B - p.R) / (float)dv * k + y0), ((p.R * 2) * k) / (float)dv, ((p.R * 2) * k) / (float)dv);
                            break;
                        case "Fcn.PointG":
                            PointG pG = (PointG)ListFcn[i];
                            PointF lo = new PointF(pG.I.X / (float)dv * k + x0 - 5, -pG.I.Y / (float)dv * k + y0 - 5);
                            g.FillEllipse(new SolidBrush(theme.TextColor), lo.X, lo.Y, 10, 10);
                            g.DrawString(pG.name, new Font("Arial", 10), new SolidBrush(theme.TextColor), lo.X + 8, lo.Y + 8);
                            break;
                        default:
                            pGraph = SetGraph(ListFcn[i]);
                            PaintGraph(pGraph, i);
                            break;
                    }
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ListFcn.Count == 0)
                return;
            if (!isSave)
            {
                saveFileDialog1.Filter = "GT files (*.GT)|*.GT";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    WriteToSave(saveFileDialog1.FileName);
                    pathFile = saveFileDialog1.FileName;
                    saveFileDialog1.FileName = "";
                    isSave = !isSave;
                }
            }
            else
            {
                WriteToSave(pathFile);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";

            openFileDialog1.Filter = "GT files (*.GT)|*.GT";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadFile(openFileDialog1.FileName);
                openFileDialog1.FileName = "";
            }
        }
        // ================================== Export ==========================================
        void exportPic(Rectangle r)
        {
            Bitmap b_export = new Bitmap(r.Width, r.Height);
            for (int i = 0; i < r.Height; i++)
                for (int j = 0; j < r.Width; j++)
                    b_export.SetPixel(j, i, MainBitmap.GetPixel(j + r.Location.X, i + r.Location.Y));


            saveFileDialog1.Filter = "jpg files (*.jpg)|*.jpg|png files (*.png)|*.png";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                b_export.Save(saveFileDialog1.FileName);
                saveFileDialog1.FileName = "";
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
                y = (float)a.f((double)x);
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
                    g.DrawCurve(new Pen(ListFcn[i].color, 2), d);
                }
                p++;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < ListFcnControls.Count - 1; i++)
            {
                if (ListFcnControls[i].selected)
                {
                    flowLayoutPanel1.Controls.Remove(ListFcnControls[i]);
                    ListFcnControls.RemoveAt(i);
                    ListFcn.RemoveAt(i);
                    i--;
                    Refresh_ListFcn();
                    DrawGr();
                }
            }
            if (ListFcn.Count==0)
            {
                saveToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
                savetoolStripButton.Enabled = false;
                saveastoolStripButton.Enabled = false;
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
        /**********************************************************************************************/
        public void draw_grap(Form form,Button a1,int n,TextBox[] t)
        {
            float[] f = new float[n];
            a1.Click += (s4, e4) =>
            {
                Circle circe = new Circle();
                for (int i = 0; i < n; i++)
                {
                    float o;
                    if (t[i].Text == string.Empty)
                    {
                        MessageBox.Show("Nhập Đầy Đủ Giá Trị", "lỗi");
                        return;
                    }
                    if (!float.TryParse(t[i].Text, out o))
                    {
                        MessageBox.Show("Giá Trị Phải Là Số", "Lỗi");
                        return;
                    }
                    if (i == 2 && float.Parse(t[i].Text) < 0)
                    {
                        MessageBox.Show("R phải lớn hơn 0");
                        return;
                    }
                    f[i] = float.Parse(t[i].Text);
                }

                circe.X = f;
                ListFcn.Add(circe);
                addListFcn();
                form.Close();
            };
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            f.Font = new Font("Arial", 12, FontStyle.Regular);

            SplitContainer split = new SplitContainer();
            Color color = new Color();
            color = Color.FromArgb(255, 224, 192);
            split.Panel2.BackColor = color;
            split.Size = new Size(800, 1000);
            ComboBox combobox = new ComboBox();
            combobox.Dock = DockStyle.Fill;
            combobox.DropDownHeight = 100;
            //combobox.DropDownWidth = 400;
            combobox.Items.Add("Phương Trình Đường Tròn");
            combobox.Items.Add("Phương Trình Đặc Biệt");
            for (int i = 1; i <= 5; i++)
            {
                string bac = "Phương Trình Bậc " + i.ToString();
                combobox.Items.Add(bac);
            }
            split.Panel1.Controls.Add(combobox);

            Label l = new Label();
            l.Text = "Phương Trình Có Dạng";
            l.Size = new Size(180, 30);
            l.Visible = false;
            split.Panel2.Controls.Add(l);
            
            Label l1 = new Label() { Text = "Nhập a", Size = new Size(60, 30), Location = new Point(0, 60), Visible = false };
            Label l2 = new Label() { Text = "Nhập b", Size = new Size(60, 30), Location = new Point(0, 100), Visible = false };
            Label l3 = new Label() { Text = "Nhập c", Size = new Size(60, 30), Location = new Point(0, 140), Visible = false };
            Label l4 = new Label() { Text = "Nhập d", Size = new Size(60, 30), Location = new Point(0, 180), Visible = false };
            Label l5 = new Label() { Text = "Nhập e", Size = new Size(60, 30), Location = new Point(0, 220), Visible = false };
            Label l6 = new Label() { Text = "Nhập f", Size = new Size(60, 30), Location = new Point(0, 260), Visible = false };
        
            TextBox t1 = new TextBox() { Size = new Size(60, 30), Location = new Point(70, 60), Visible = false };
            TextBox t2 = new TextBox() { Size = new Size(60, 30), Location = new Point(70, 100), Visible = false };
            TextBox t3 = new TextBox() { Size = new Size(60, 30), Location = new Point(70, 140), Visible = false };
            TextBox t4 = new TextBox() { Size = new Size(60, 30), Location = new Point(70, 180), Visible = false };
            TextBox t5 = new TextBox() { Size = new Size(60, 30), Location = new Point(70, 220), Visible = false };
            TextBox t6 = new TextBox() { Size = new Size(60, 30), Location = new Point(70, 260), Visible = false };

            Button dr = new Button() { Text = "Draw", Size = new Size(70, 60), Location = new Point(180, 100), Visible = false,BackColor=Color.White };

            PictureBox p = new PictureBox()
            {
                Size = new Size(800, 500),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(180, 0),
                Visible = false
            };

            Label[] label = new Label[] {l1,l2,l3,l4,l5,l6 };

            TextBox[] texbox = new TextBox[] { t1, t2, t3, t4, t5, t6 };
            split.Panel2.Controls.AddRange(label);
            split.Panel2.Controls.AddRange(texbox);
            split.Panel2.Controls.Add(p);
            split.Panel2.Controls.Add(dr);
            f.Controls.Add(split);
            f.Size = new Size(600, 400);
            f.Show();
            
            combobox.SelectedIndexChanged += (s, e1) =>
            {
                if (combobox.SelectedItem.ToString() == "Phương Trình Đường Tròn")
                {
                    f.Size = new Size(720, 250);
                    l.Visible = true;
                    for(int i = 0; i < 3; i++)
                    {
                        label[i].Visible = true;
                        texbox[i].Visible = true;
                    }
                    label[2].Text = "Nhập R";
                    for(int i = 3; i < 6; i++)
                    {
                        label[i].Visible = false;
                        texbox[i].Visible = false;
                    }
                                      
                    p.Visible = true;
                    dr.Visible = true;
                    Image ig = Image.FromFile(@"..\\..\\Resources\\lt-b2-chuong-3-sgk-hh-10-0.jpg");
                    p.Image = ig;
                    draw_grap(f,dr, 3, texbox);
                   
                    DrawGr();
                }
                 
            };
        }

        private void toolStripLabel1_Click_1(object sender, EventArgs e) => c_mouse = mouse.none;
        private void toolStripLabel2_Click(object sender, EventArgs e) => c_mouse = mouse.export;
        private void toolStripLabel3_Click(object sender, EventArgs e) => c_mouse = mouse.s_point;

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ListFcn.Count == 0)
                return;
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                WriteToSave(saveFileDialog1.FileName);
                pathFile = saveFileDialog1.FileName;
                saveFileDialog1.FileName = " ";
            }
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void toolStrip2_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void toolStrip1_BackColorChanged(object sender, EventArgs e)
        {
            if (Dark)
            {
                newtoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-add-file-80-dark.png");
                opentoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-opened-folder-144-dark.png");
                savetoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-save-100-dark.png");
                saveastoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-save-as-100-dark.png");
                deletetoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-delete-bin-96-dark.png");
                exittoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-exit-52-dark.png");
            }
            else
            {
                newtoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-add-file-80.png");
                opentoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-opened-folder-144.png");
                savetoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-save-100.png");
                saveastoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-save-as-100.png");
                deletetoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-delete-bin-96.png");
                exittoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-exit-52.png");
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
                    ListFcn[(int)n.Tag].Enable = !ListFcn[(int)n.Tag].Enable;
                DrawGr();
            };
            n.textBox1.KeyDown += (s, e) =>
            {
                Refresh_ListFcn();
                if (e.KeyValue == 13)
                {
                    Function fn = new Function();
                    if (n.Tag == null)
                    {
                        fn.Parse(n.textBox1.Text.ToLower());
                        fn.Infix2Postfix();
                        fn.arr = fn.Variables;
                        if (fn.arr.Count != 1)
                        {
                            return;
                        }
                        else
                        {
                            if (fn.arr[0].ToString() != "x")
                            {
                                MessageBox.Show("Biểu thức không hợp lệ. Vui lòng nhập lại !\n\nVí dụ: (sin(x)+3)/(x+4)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        n.UserControl1_DoubleClick(null,null);
                        ListFcn.Add(fn);
                        addListFcn();
                    }
                    else
                    {
                        ListFcn[(int)n.Tag].Parse(n.textBox1.Text.ToLower());
                        ListFcn[(int)n.Tag].Infix2Postfix();
                        ListFcn[(int)n.Tag].arr = ListFcn[(int)n.Tag].Variables;
                        if (ListFcn[(int)n.Tag].arr.Count != 1)
                        {
                            return;
                        }
                        else
                        {
                            if (ListFcn[(int)n.Tag].arr[0].ToString() != "x")
                            {
                                MessageBox.Show("Biểu thức không hợp lệ. Vui lòng nhập lại !\n\nVí dụ: (sin(x)+3)/(x+4)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    DrawGr();
                }
            };
            return n;
        }

        private void Refresh_ListFcn()
        {
            for(int i = 0; i < ListFcnControls.Count - 1; i++)
            {
                ListFcnControls[i].color = ListFcn[i].color;
                ListFcnControls[i].Set_Color();
                ListFcnControls[i].Tag = i;
            }
        }

        private void addListFcn()
        {
            ListFcnControls.Add(create_UserControl1());
            ListFcnControls[ListFcnControls.Count - 2].textBox1.Text = ListFcn[ListFcn.Count - 1].ToString();
            flowLayoutPanel1.Controls.Add(ListFcnControls[ListFcnControls.Count - 1]);
            Refresh_ListFcn();
            flowLayoutPanel1_SizeChanged(null, null);
            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            savetoolStripButton.Enabled = true;
            saveastoolStripButton.Enabled = true;
        }

        private void SetColorItemList()
        {
            foreach(UserControl1 i in ListFcnControls)
            {
                if(i.Tag != null)
                {
                    i.color = ListFcn[(int)i.Tag].color;
                    i.Set_Color();
                }
            }
        }
        string PointName()
        {
            if (name == 'Z')
            {
                name = 'A';
                countName++;
            }
            else name++;
            return string.Format("{0}{1}", name, countName == 0? "": countName.ToString());
        }

        void WriteToSave(string fileName)
        {
            using (StreamWriter file = new StreamWriter(fileName))
            {
                file.WriteLine(Dark);
                file.WriteLine(x0);
                file.WriteLine(y0);
                file.WriteLine(k);
                file.WriteLine(dv);
                file.WriteLine(name);
                file.WriteLine(countName);
                foreach (Function i in ListFcn)
                {
                    file.WriteLine("*=*=*");
                    file.Write(i.SaveString());
                }
            }
        }

        void LoadFile(string path)
        {
            pathFile = path;
            isSave = true;
            StreamReader stream = new StreamReader(path);
            List<string> listF = new List<string>();
            while (!stream.EndOfStream)
            {
                listF.Add(stream.ReadLine());
            }
            Dark = Convert.ToBoolean(listF[0]);
            if (Dark)
                this.darkThemeToolStripMenuItem_CheckedChanged(null,null);
            x0 = Convert.ToInt32(listF[1]);
            y0 = Convert.ToInt32(listF[2]);
            k = Convert.ToInt32(listF[3]);
            dv = Convert.ToDouble(listF[4]);
            name = Convert.ToChar(listF[5]);
            countName = Convert.ToInt32(listF[6]);
            int i = 0;
            ListFcn.Clear();
            while(i < listF.Count)
            {
                while (listF[i] != "*=*=*")
                {
                    DrawGr();
                    if (i == listF.Count - 1) return;
                    i++;
                }
                int j = i + 1;
                switch (listF[j])
                {
                    case "Fcn.Circle":
                        ListFcn.Add(new Circle(listF[j + 1], Convert.ToInt32(listF[j + 2])));
                        break;
                    case "Fcn.Function":
                        Function data = new Function();
                        data.arr = data.Variables;
                        data.EvaluatePostfix();
                        data.Parse(listF[j + 1]);
                        ListFcn.Add(data);
                        break;
                    case "Fcn.PointG":
                        ListFcn.Add(new PointG(listF[j + 1]));
                        ListFcn[ListFcn.Count - 1].color = theme.TextColor;
                        break;
                    default:
                        break;
                }
                addListFcn();
                i++;
            }
        }
    }
    
}
