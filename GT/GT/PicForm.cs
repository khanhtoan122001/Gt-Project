using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Fcn;

namespace GT
{
    public struct _dth
    {
        public bool a1;
        public PointG a;
    }
    public partial class PicForm : Form
    {
        float[] C_dv = { 1f, 2f, 5f };
        Theme theme = new Theme();
        public string pathFile = string.Empty;
        const int MaxZoom = 180, Normal = 100;
        List<Function> ListFcn = new List<Function>();
        List<UserControl1> ListFcnControls = new List<UserControl1>();
        int max_x, max_y, x0, y0, k = 120, idv = 0, countName = -1, pointSelect = -1;
        double dv = 1;
        const float MaxDv = 500, MinDv = 0.001f;
        Point u = new Point(0, 0);
        Point LastMouse = new Point(0, 0), p_Export;
        List<PointG> GD = new List<PointG>();
        PointG p1_Dr, p2_Dr;
        char nameP = 'Z', nameF = 'e';
        Graphics g;
        Bitmap MainBitmap;
        bool isMouseDown = false, isSave = false, Dark = false, LuoiNho = true, Luoi = true, SwExport = false, isMove = false;
        int G = 10;
        string str_nameF = "f";
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
        public static bool[] flat = new bool[] { false, false, false, false, false, false, false };
        public PicForm()
        {
            InitializeComponent();



            ListFcnControls.Add(create_UserControl1());

            flowLayoutPanel1.Controls.Add(ListFcnControls[ListFcnControls.Count - 1]);

            this.pictureBox1.MouseMove += _MouseMove;

            this.pictureBox1.MouseWheel += (s, e) =>
            {
                float _x = (float)dv * (e.Location.X - x0) / (float)k;
                float _y = (float)dv * (e.Location.Y - y0) / (float)k;
                if (e.Delta > 0)
                {
                    if ((k * Zoom) > MaxZoom)
                    {
                        k = Normal;
                        if (dv > MinDv) SetDv(true);
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
                        if (dv < MaxDv) SetDv(false);
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
                pointSelect = PointSelect((PointF)e.Location);
                switch (c_mouse)
                {
                    case mouse.none:
                        if (pointSelect == -1)
                        {
                            pictureBox1.Cursor = Cursors.SizeAll;
                            isMove = true;
                        }
                        else
                        {
                            pictureBox1.Cursor = Cursors.Hand;
                            isMove = false;
                        }
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
                        pictureBox1.Cursor = Cursors.Hand;
                        DrawGr();
                        g.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(new Point(e.X - 5, e.Y - 5), new Size(10, 10)));
                        break;
                }
            };

            this.pictureBox1.MouseUp += (s, e) =>
            {
                pictureBox1.Cursor = Cursors.Default;
                isMouseDown = false;
                isMove = false;
                CheckDr();
                if (c_mouse == mouse.s_point)
                {
                    ListFcn.Add(new PointG(PointName(), e.Location, new Point(x0, y0), k, (float)dv));
                    if (Dark)
                        ListFcn[ListFcn.Count - 1].color = Color.White;
                    else
                        ListFcn[ListFcn.Count - 1].color = Color.Black;
                    addListFcn();
                    DrawGr();
                }
                if (c_mouse == mouse.dr_Circle)
                {
                    if (p1_Dr == null)
                    {
                        p1_Dr = new PointG("", e.Location, new Point(x0, y0), k, (float)dv);
                    }
                    else if (p2_Dr == null) 
                    {
                        p2_Dr = new PointG("", e.Location, new Point(x0, y0), k, (float)dv);
                        ListFcn.Add(new Circle(p1_Dr, p2_Dr));
                        addListFcn();
                        CheckDr();
                        DrawGr();
                    }
                }
                if (c_mouse == mouse.dr_Line)
                {
                    if (p1_Dr == null)
                    {
                        p1_Dr = new PointG("", e.Location, new Point(x0, y0), k, (float)dv);
                    }
                    else if (p2_Dr == null)
                    {
                        p2_Dr = new PointG("", e.Location, new Point(x0, y0), k, (float)dv);
                        Function f = new Function();
                        f.Parse(Ex_function(p1_Dr, p2_Dr));
                        ListFcn.Add(f);
                        addListFcn();
                        CheckDr();
                        DrawGr();
                    }
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
                        if (isMove)
                        {
                            u.X = e.X - LastMouse.X;
                            u.Y = e.Y - LastMouse.Y;
                            LastMouse = e.Location;
                            DrawGr();
                        }
                        else
                        {
                            u.X = e.X - LastMouse.X;
                            u.Y = e.Y - LastMouse.Y;
                            if (e.Location != LastMouse)
                            {
                                PointG pG = (PointG)ListFcn[pointSelect];
                                pG.I = new PointF(pG.I.X + (float)(u.X) / k * (float)dv, pG.I.Y + -(float)(u.Y) / k * (float)dv);
                                LastMouse = e.Location;
                                u = new Point(0, 0);
                                Refresh_ListFcn();
                            }
                            DrawGr();
                        }
                    }
                    else
                    {
                        if (PointSelect(e.Location) != -1)
                            pictureBox1.Cursor = Cursors.Hand;
                        else
                            pictureBox1.Cursor = Cursors.Default;
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
                            if (e.X - p_Export.X > 10 && e.Y - p_Export.Y > 10)
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
                    if (isMouseDown)
                    {
                        DrawGr();
                        g.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(new Point(e.X - 5, e.Y - 5), new Size(10, 10)));
                    }
                    break;
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            LoadToolstrip1();
            foreach (ToolStripItem i in toolStrip2.Items)
                i.BackgroundImage = Image.FromFile(string.Format(@"..\\..\\Resources\\{0}", i.Text));
            x0 = this.pictureBox1.Width / 2;
            y0 = this.pictureBox1.Height / 2;
            flowLayoutPanel1_SizeChanged(null, null);
            //Create();
        }
        /**********************************************************************************************************/
        public void DrawGr()
        {
            pictureBox1.Refresh();
            CheckDr();
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
        /***************************************************************************************************************************/
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
                int i, yd, xd;
                Pen pen_x = new Pen(theme.TextColor);
                for (i = x0 + k; i < max_x; i += k)
                {
                    yd = y0;
                    if (y0 < 0) yd = 3;
                    if (y0 > max_y) yd = max_y - 20;
                    g.DrawLine(pen_x, i, yd - 2, i, yd + 2);
                    g.DrawString(((i - x0) / k * dv).ToString(), f, br, i, yd);
                }
                for (i = x0 - k; i > 0; i -= k)
                {
                    yd = y0;
                    if (y0 < 0) yd = 3;
                    if (y0 > max_y) yd = max_y - 20;
                    g.DrawLine(pen_x, i, yd - 2, i, yd + 2);
                    g.DrawString(((i - x0) / k * dv).ToString(), f, br, i, yd);
                }
                for (i = y0 + k; i < max_y; i += k)
                {
                    xd = x0;
                    if (x0 < 0) xd = 3;
                    if (x0 > max_x) xd = max_x - 20;
                    g.DrawLine(pen_x, x0 - 2, i, x0 + 2, i);
                    g.DrawString((-(i - y0) * dv / k).ToString(), f, br, xd, i);
                }
                for (i = y0 - k; i > 0; i -= k)
                {
                    xd = x0;
                    if (x0 < 0) xd = 3;
                    if (x0 > max_x) xd = max_x - 20;
                    g.DrawLine(pen_x, x0 - 2, i, x0 + 2, i);
                    g.DrawString((-(i - y0) * dv / k).ToString(), f, br, xd, i);
                }

                //VeLuoi();
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
            if (pathFile != "" || ListFcn.Count != 0)
            {
                switch (MessageBox.Show("Bạn có muốn lưu thay đổi của bạn?", "Save", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        this.saveToolStripMenuItem_Click(null, null);
                        break;
                    case DialogResult.No:
                    case DialogResult.Cancel:
                        return;
                    default:
                        break;
                }
            }
            pictureBox1.Refresh();
            ListFcn.Clear();
            ListFcnControls.Clear();
            Refresh_ListFcn();
            flowLayoutPanel1.Controls.Clear();
            ListFcnControls.Add(create_UserControl1());
            flowLayoutPanel1.Controls.Add(ListFcnControls[ListFcnControls.Count - 1]);
            flowLayoutPanel1_SizeChanged(null, null);
            isSave = false;
            pathFile = "";
            this.Text = "New work Table";
            this.Create();
        }
        public void darkThemeToolStripMenuItem_CheckedChanged()
        {
            Dark = !Dark;
            theme.ChangeAll();
            foreach (Function i in ListFcn)
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
            //this.toolStrip2.BackColor = theme.Bg3;
        }
        private void frmMain_Resize(object sender, EventArgs e)
        {

            max_x = pictureBox1.Width;
            max_y = pictureBox1.Height;
            if (c_mouse == mouse.none)
            {
                x0 += u.X;
                y0 += u.Y;
                u = new Point(0, 0);
            }

        }
        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            Refresh_ListFcn();
            int io = 0;
            if ((ListFcnControls.Count * new UserControl1().Height - flowLayoutPanel1.Height) > 0)
                io = 13;
            foreach (UserControl1 i in ListFcnControls)
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
                            g.FillEllipse(new SolidBrush(pG.color), lo.X, lo.Y, 10, 10);
                            g.DrawString(pG.name, new Font("Arial", 10), new SolidBrush(theme.TextColor), lo.X + 8, lo.Y + 8);
                            break;
                        default:
                            pGraph = SetGraph(ListFcn[i]);
                            PaintGraph(pGraph, i);
                            break;
                    }
                }
            }
            if(c_mouse == mouse.dr_Circle)
            {
                PointG pG;
                PointF lo;
                if(p1_Dr != null)
                {
                    pG = p1_Dr;
                    lo = new PointF(pG.I.X / (float)dv * k + x0 - 5, -pG.I.Y / (float)dv * k + y0 - 5);
                    g.FillEllipse(new SolidBrush(Color.Blue), lo.X, lo.Y, 10, 10);
                }
                if(p2_Dr != null)
                {
                    pG = p2_Dr;
                    lo = new PointF(pG.I.X / (float)dv * k + x0 - 5, -pG.I.Y / (float)dv * k + y0 - 5);
                    g.FillEllipse(new SolidBrush(Color.Blue), lo.X, lo.Y, 10, 10);
                }
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ListFcn.Count == 0)
                return;
            if (!isSave)
            {
                saveFileDialog1.Filter = "GT files (*.gt)|*.gt";
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
        public void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.FileName = "";

            openFileDialog1.Filter = "GT files (*.gt)|*.gt";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
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
            for (int i = 0; i < ListFcnControls.Count - 1; i++)
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
            if (ListFcn.Count == 0)
            {
                //saveToolStripMenuItem.Enabled = false;
                //saveAsToolStripMenuItem.Enabled = false;
                savetoolStripButton.Enabled = false;
                saveastoolStripButton.Enabled = false;
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
        public void draw_grap(Form form, Button a1, int n, TextBox[] t)
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

            Button dr = new Button() { Text = "Draw", Size = new Size(70, 60), Location = new Point(180, 100), Visible = false, BackColor = Color.White };

            PictureBox p = new PictureBox()
            {
                Size = new Size(800, 500),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(180, 0),
                Visible = false
            };

            Label[] label = new Label[] { l1, l2, l3, l4, l5, l6 };

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
                    for (int i = 0; i < 3; i++)
                    {
                        label[i].Visible = true;
                        texbox[i].Visible = true;
                    }
                    label[2].Text = "Nhập R";
                    for (int i = 3; i < 6; i++)
                    {
                        label[i].Visible = false;
                        texbox[i].Visible = false;
                    }

                    p.Visible = true;
                    dr.Visible = true;
                    Image ig = Image.FromFile(@"..\\..\\Resources\\lt-b2-chuong-3-sgk-hh-10-0.jpg");
                    p.Image = ig;
                    draw_grap(f, dr, 3, texbox);

                    DrawGr();
                }

            };
        }
        private void toolStripLabel1_Click_1(object sender, EventArgs e) { c_mouse = mouse.none; DrawGr(); }
        private void toolStripLabel2_Click(object sender, EventArgs e) { c_mouse = mouse.export; DrawGr(); }
        private void toolStripLabel3_Click(object sender, EventArgs e) { c_mouse = mouse.s_point; DrawGr(); }
        /*****************************************************************************************************************************/

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            DrawGr();
            Function f = new Function();
            PointG a, b;
            a = b = null;

            for (int i = 0; i < ListFcn.Count; i++)
            {
                if (ListFcnControls[i].selected && ListFcn[i].GetType().ToString() == "Fcn.PointG")
                {
                    if (a == null)
                    {
                        a = (PointG)ListFcn[i];
                    }
                    else
                    {
                        if (b == null)
                            b = (PointG)ListFcn[i];
                    }
                }

            }
            if (a == null || b == null)
            {
                c_mouse = mouse.dr_Line;
                return;
            }
            f.Parse(Ex_function(a,b));
            f.name = FunctionName();
            // f.Parse("2x+2");
            ListFcn.Add(f);
            addListFcn();
            DrawGr();
        }

        string Ex_function(PointG a, PointG b)
        {
            string function = string.Empty;
            float _x = (a.I.Y - b.I.Y) / (a.I.X - b.I.X);
            float _x1 = (a.I.X * b.I.Y - b.I.X * a.I.Y) / (a.I.X - b.I.X);


            function = string.Format("{0}x{1}", _x, _x1 < 0 ? _x1.ToString() : "+" + _x1.ToString());
            return function;
        }
        private void toolStripLabel6_Click(object sender, EventArgs e)
        {
            p1_Dr = p2_Dr = null;
            c_mouse = mouse.dr_Circle;
        }
        private void toolStripLabel7_Click(object sender, EventArgs e)
        {
            Function f = new Function();
            PointG a, b;
            a = b = null;

            for (int i = 0; i < ListFcn.Count; i++)
            {
                if (ListFcnControls[i].selected && ListFcn[i].GetType().ToString() == "Fcn.PointG")
                {
                    if (a == null)
                    {
                        a = (PointG)ListFcn[i];
                    }
                    else
                    {
                        if (b == null)
                            b = (PointG)ListFcn[i];
                        else
                        {
                            return;
                        }
                    }
                    //else if (a != null && b != null)
                    //{
                    //    MessageBox.Show("phải chọn 2 điểm");
                    //    return;
                    //}
                }

            }
            if (a == null || b == null)
            {
                //MessageBox.Show("phải chọn 2 điểm", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            float _b = EX1_funtion(a, b);
            MessageBox.Show(string.Format("{0}{1} = {2}",a.name,b.name,_b.ToString()));
        }
        float EX1_funtion(PointG a, PointG b)
        {
            return (float)Math.Sqrt((a.I.X - b.I.X) * (a.I.X - b.I.X) + (a.I.Y - b.I.Y) * (a.I.Y - b.I.Y));
        }
        /*****************************************************************************************************************************/
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ListFcn.Count == 0)
                return;
            saveFileDialog1.Filter = "GT files (*.gt)|*.gt";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                WriteToSave(saveFileDialog1.FileName);
                pathFile = saveFileDialog1.FileName;
                saveFileDialog1.FileName = " ";
            }
        }

        private void PicForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(pathFile != "" || ListFcn.Count != 0)
            {
                switch(MessageBox.Show("Bạn có muốn lưu thay đổi của bạn?", "Save", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        this.saveToolStripMenuItem_Click(null, null);
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            darkThemeToolStripMenuItem_CheckedChanged();
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            toolStrip1.Refresh();
            foreach(ToolStripItem i in toolStrip2.Items)
            {
                Image img = Image.FromFile(string.Format(@"..\\..\\Resources\\{0}", i.Text));
                Bitmap bitmap = new Bitmap(50, 50);
                Graphics g = Graphics.FromImage(bitmap);
                g.DrawImage(img,
                new Rectangle(new Point(0,0), bitmap.Size),
                new Rectangle(0, 0, img.Width, img.Height),
                GraphicsUnit.Pixel);
                i.BackgroundImage = bitmap;
                if (i == e.ClickedItem)
                    ControlPaint.DrawBorder(g, new Rectangle(0, 0, i.Width, i.Height), Color.FromArgb(255, 0, 0), ButtonBorderStyle.Solid);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            {
                checkBox3.Checked = true;
                Luoi = true;
                DrawGr();
            }
            else
            {
                checkBox3.Checked = false;
                Luoi = false;
                DrawGr();
            }
        }

        private void LoadToolstrip1()
        {
                newtoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-add-file-80.png");
                opentoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-opened-folder-144.png");
                savetoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-save-100.png");
                saveastoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-save-as-100.png");
                deletetoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-delete-bin-96.png");
                exittoolStripButton.BackgroundImage = Image.FromFile(@"..\\..\\Resources\\icons8-exit-52.png");
        }
        void SetDv(bool c_i)
        {
            if (c_i) idv--;
            else idv++;
            if (idv >= 0)
            {
                dv = C_dv[idv % 3] * Pow10(idv / 3);
            }
            else
            {
                int i = 2 - (-idv + 2) % 3;
                dv = C_dv[i] / Pow10(((-idv + 2) / 3));
            }
            if (idv == -5)
            {
                c_i = true;
            }
        }
        void VeLuoi()
        {
            if (Luoi)
            {
                Pen pen_x = new Pen(theme.Nest, 2);
                int i;
                for (i = x0 + k; i < max_x; i += k)
                    g.DrawLine(pen_x, i, 0, i, max_y);
                for (i = x0 - k; i > 0; i -= k)
                    g.DrawLine(pen_x, i, 0, i, max_y);
                for (i = y0 + k; i < max_y; i += k)
                    g.DrawLine(pen_x, 0, i, max_x, i);
                for (i = y0 - k; i > 0; i -= k)
                    g.DrawLine(pen_x, 0, i, max_x, i);
                if (LuoiNho) VeLuoiNho();
            }
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
            for (int n = 0; n < i; n++)
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
                if (n.Tag != null)
                    ListFcn[(int)n.Tag].Enable = !ListFcn[(int)n.Tag].Enable;
                DrawGr();
            };

            n.pictureBox1.Click += (s, e) =>
            {
                
                if (n.Tag != null)
                {
                    ColorDialog dlg = new ColorDialog();
                    int r, g, b;
                    Color a = new Color();
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        r = dlg.Color.R;
                        g = dlg.Color.G;
                        b = dlg.Color.B;
                        a = Color.FromArgb(r, g, b);
                        n.color = a;
                        n.Set_Color();
                        ListFcn[Convert.ToInt32(n.Tag)].color = a;
                        DrawGr();
                    }
                }              
            };
            n.textBox1.KeyDown += (s, e) =>
            {
                //Refresh_ListFcn();
                if (e.KeyValue == 13)
                {
                    Function fn = new Function();
                    Circle cir = new Circle();
                    if (n.Tag == null)
                    {
                        if (cir.parse(n.textBox1.Text.ToLower()))
                        {
                            ListFcn.Add(cir);
                            addListFcn();
                            DrawGr();
                            n.UserControl1_DoubleClick(null, null);
                            return;
                        }
                        fn.Parse(n.textBox1.Text.ToLower());
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
                        n.UserControl1_DoubleClick(null, null);
                        fn.name = FunctionName();
                        ListFcn.Add(fn);
                        addListFcn();
                    }
                    else
                    {
                        string text = n.textBox1.Text;
                        text = text.Substring(text.IndexOf('=') + 1);
                        ListFcn[(int)n.Tag].Parse(text.ToLower());
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
                        n.UserControl1_DoubleClick(null, null);
                    }
                    DrawGr();
                }
            };
            return n;
        }
        private void Refresh_ListFcn()
        {
            for (int i = 0; i < ListFcnControls.Count - 1; i++)
            {
                ListFcnControls[i].textBox1.Text = ListFcn[i].ToString();
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
            //saveToolStripMenuItem.Enabled = true;
            //saveAsToolStripMenuItem.Enabled = true;
            savetoolStripButton.Enabled = true;
            saveastoolStripButton.Enabled = true;
        }
        private void SetColorItemList()
        {
            foreach (UserControl1 i in ListFcnControls)
            {
                if (i.Tag != null)
                {
                    i.color = ListFcn[(int)i.Tag].color;
                    i.Set_Color();
                }
            }
        }
        string PointName()
        {
            if (nameP == 'Z')
            {
                nameP = 'A';
                countName++;
            }
            else nameP++;
            return string.Format("{0}{1}", nameP, countName == 0 ? "" : countName.ToString());
        }
        
        string FunctionName()
        {
            if (nameF == 'z')
            {
                nameF = 'f';
                str_nameF += nameF;
            }
            else {
                nameF++;
                str_nameF = str_nameF.Substring(0,str_nameF.Length - 1) + nameF;
            }
            return str_nameF;
        }

        void WriteToSave(string fileName)
        {
            StreamWriter file = new StreamWriter(fileName);
            file.WriteLine(Dark);
            file.WriteLine(x0);
            file.WriteLine(y0);
            file.WriteLine(k);
            file.WriteLine(dv);
            file.WriteLine(nameP);
            file.WriteLine(countName);
            foreach (Function i in ListFcn)
            {
                file.WriteLine("*=*=*");
                file.Write(i.SaveString());
            }
            file.Close();
            file.Dispose();
        }
        /**************************************************************************************************************/

        /****************************************************************************************************************/
        void LoadFile(string path)
        {
            pathFile = path;
            isSave = true;
            StreamReader stream = new StreamReader(path);
            List<string> listF = new List<string>();
            FileInfo info = new FileInfo(path);
            while (!stream.EndOfStream)
            {
                listF.Add(stream.ReadLine());
            }
            if (listF.Count < 7)
            {
                listF.Clear();
                stream.Close();
                stream.Dispose();
                return;
            }
            Dark = Convert.ToBoolean(listF[0]);
            if (Dark)
                this.darkThemeToolStripMenuItem_CheckedChanged();
            x0 = Convert.ToInt32(listF[1]);
            y0 = Convert.ToInt32(listF[2]);
            k = Convert.ToInt32(listF[3]);
            dv = Convert.ToDouble(listF[4]);
            nameP = Convert.ToChar(listF[5]);
            countName = Convert.ToInt32(listF[6]);
            int i = 0;
            ListFcn.Clear();
            ListFcnControls.Clear();
            flowLayoutPanel1.Controls.Clear();
            //flowLayoutPanel1.Refresh();
            ListFcnControls.Add(create_UserControl1());
            while (i < listF.Count)
            {
                while (listF[i] != "*=*=*")
                {
                    DrawGr();
                    if (i == listF.Count - 1)
                    {
                        listF.Clear();
                        stream.Close();
                        stream.Dispose();
                        this.Text = info.Name;
                        return;
                    }
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
                        data.color = Color.FromArgb(Convert.ToInt32(listF[j + 2]));
                        data.Parse(listF[j + 1]);
                        ListFcn.Add(data);
                        break;
                    case "Fcn.PointG":
                        ListFcn.Add(new PointG(listF[j + 1], listF[j + 2]));
                        break;
                    default:
                        break;
                }
                addListFcn();
                i++;
            }
            return;
        }
        int PointSelect(PointF p)
        {
            for(int i = 0; i < ListFcn.Count; i++)
            {
                if(ListFcn[i].GetType().ToString() == "Fcn.PointG")
                {
                    PointG pG = (PointG)ListFcn[i];
                    PointF lo = new PointF(pG.I.X / (float)dv * k + x0 - 5, -pG.I.Y / (float)dv * k + y0 - 5);
                    RectangleF rect = new RectangleF(lo, new SizeF(10, 10));
                    if (rect.Contains(p))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        void CheckDr()
        {
            if (c_mouse != mouse.dr_Circle && c_mouse != mouse.dr_Line)
            {
                if (p1_Dr != null) ListFcn.Remove(p1_Dr);
                if (p2_Dr != null) ListFcn.Remove(p2_Dr);
                p1_Dr = p2_Dr = null;
            }
        }
    }
    
}
