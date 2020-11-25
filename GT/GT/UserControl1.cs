using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GT
{
    public partial class UserControl1 : UserControl
    {
        //Image img = Image.FromFile(@"..\\..\\Resources\\ColorWheel.png");
        Color C_panel = Color.FromArgb(255, 252, 48);
        public UserControl1()
        {
            InitializeComponent();
            _checked = false;

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            _checked = !_checked;
            if (_checked)
                this.BackColor = C_panel;
            else
                this.BackColor = Color.White;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
