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
        public Form1()
        {
           
            InitializeComponent();
            this.newToolStripMenuItem1_Click(null, null);
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PicForm picForm = new PicForm();
            picForm.MdiParent = this;
            picForm.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicForm picForm = new PicForm();
            picForm.MdiParent = this;
            picForm.openToolStripMenuItem_Click(null, null);
            picForm.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private double khoangCach(Point a,Point b)
        {
            return (Math.Sqrt((a.X-b.X)*(a.X-b.X)+(a.Y-b.X)*(a.Y-a.Y)));
        }

        private List<double> duongThang(Point a,Point b)
        {
            
            double _x = (a.Y - b.Y) / (a.X - b.X);
            double _x1 = (a.X * b.Y - b.X * a.Y) / (a.X - b.X);
            List<double> dth = new List<double>() { _x, _x1 };
            return dth;
        }
        private List<double> duongTron(Point a,Point b)
        {
            double r = Math.Sqrt((a.X-b.X)*(a.X-b.X)+(a.Y-b.Y)*(a.Y-b.Y));
            List<double> dtr = new List<double>() { a.X, a.Y, r };
            return dtr;
        }
    }
}
