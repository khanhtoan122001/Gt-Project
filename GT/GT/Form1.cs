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
    }
}
