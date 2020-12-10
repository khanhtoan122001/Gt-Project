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
    public partial class Forminput: Form
    {
        public Forminput()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "phương trình đường tròn")
            {
                
                this.Size = new Size(700, 250);
                button3.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                pictureBox1.Visible = true;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                textBox6.Visible = false;

                label1.Text = "Phương Trình Có Dạng";
                label2.Text = "Nhập a";
                label3.Text = "Nhập b";
                label4.Text = "Nhập R";
                Image ig = Image.FromFile(@"..\\..\\Resources\\lt-b2-chuong-3-sgk-hh-10-0.jpg");
                pictureBox1.Image = ig;
            }
            else if (comboBox1.SelectedItem.ToString() == "phương trình đặc biệt")
            {
                this.Size = new Size(600, 200);
                button3.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                //label4.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                //textBox3.Visible = true;
                pictureBox1.Visible = true;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                textBox6.Visible = false;
                label1.Text = "Phương Trình Có Dạng";
                label2.Text = "Nhập a";
                label3.Text = "Nhập b";
                //label4.Text = "nhập R";
                Image ig = Image.FromFile(@"..\\..\\Resources\\Screenshot (56).png");
                pictureBox1.Image = ig;
            }
            else if (comboBox1.SelectedItem.ToString() == "phương trình bậc 1")
            {
                this.Size = new Size(600, 200);
                button3.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                //label4.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                // textBox3.Visible = true;
                pictureBox1.Visible = true;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                textBox6.Visible = false;
                label1.Text = "Phương Trình Có Dạng";
                label2.Text = "Nhập a";
                label3.Text = "Nhập b";
                //label4.Text = "nhập c";
                Image ig = Image.FromFile(@"..\\..\\Resources\\bac1.png");
                pictureBox1.Image = ig;
            }
            else if (comboBox1.SelectedItem.ToString() == "phương trình bậc 2")
            {
                this.Size = new Size(700, 250);
                button3.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                pictureBox1.Visible = true;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                textBox6.Visible = false;
                label1.Text = "Phương Trình Có Dạng";
                label2.Text = "Nhập a";
                label3.Text = "Nhập b";
                label4.Text = "Nhập c";
                Image ig = Image.FromFile(@"..\\..\\Resources\\unnamed.jpg");
                pictureBox1.Image = ig;
            }
            else if (comboBox1.SelectedItem.ToString() == "phương trình bậc 3")
            {
                this.Size = new Size(700, 300);
                button3.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                pictureBox1.Visible = true;
                //label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                //textBox1.Visible = false;
                textBox5.Visible = false;
                textBox6.Visible = false;
                label1.Text = "Phương Trình Có Dạng";
                label2.Text = "Nhập a";
                label3.Text = "Nhập b";
                label4.Text = "Nhập c";
                label5.Text = "Nhập d";
                Image ig = Image.FromFile(@"..\\..\\Resources\\Screenshot (74).png");
                pictureBox1.Image = ig;
            }
            else if (comboBox1.SelectedItem.ToString() == "phương trình bậc 4")
            {
                this.Size = new Size(800, 350);
                button3.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                pictureBox1.Visible = true;
                //label5.Visible = false;
                //label6.Visible = false;
                label7.Visible = false;
                //textBox1.Visible = false;
                // textBox5.Visible = false;
                textBox6.Visible = false;
                label1.Text = "Phương Trình Có Dạng";
                label2.Text = "Nhập a";
                label3.Text = "Nhập b";
                label4.Text = "Nhập c";
                label5.Text = "Nhập d";
                label6.Text = "Nhập e";
                Image ig = Image.FromFile(@"..\\..\\Resources\\Screenshot (47).png");
                pictureBox1.Image = ig;
            }
            else if (comboBox1.SelectedItem.ToString() == "phương trình bậc 5")
            {
                this.Size = new Size(850, 380);
                button3.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                textBox6.Visible = true;
                pictureBox1.Visible = true;
                label1.Text = "Phương Trình Có Dạng";
                label2.Text = "Nhập a";
                label3.Text = "Nhập b";
                label4.Text = "Nhập c";
                label5.Text = "Nhập d";
                label6.Text = "Nhập e";
                label7.Text = "Nhập f";
                Image ig = Image.FromFile(@"..\\..\\Resources\\Screenshot (49).png");
                pictureBox1.Image = ig;
            }
        }

       
    }
}
