using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fcn;
namespace GT
{
    
    public delegate void datasentHandler(float a);
    public delegate void datasentHandler1(bool a);
    public partial class Forminput: Form
    {
        public event datasentHandler datasent0;
        public event datasentHandler datasent1;
        public event datasentHandler datasent2;
        public event datasentHandler datasent3;
        public event datasentHandler datasent4;
        public event datasentHandler datasent5;
        public event datasentHandler datasent6;
        /***************************************************/
        public event datasentHandler1 datasentflat0;
        public event datasentHandler1 datasentflat1;
        public event datasentHandler1 datasentflat2;
        public event datasentHandler1 datasentflat3;
        public event datasentHandler1 datasentflat4;
        public event datasentHandler1 datasentflat5;
        public event datasentHandler1 datasentflat6;
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

        private void button3_Click(object sender, EventArgs e)
        {
           
            if(comboBox1.SelectedItem.ToString()=="phương trình đường tròn")
            {
                this.datasentflat0(true);
                this.datasentflat1(false);
                this.datasentflat2(false);
                this.datasentflat3(false);
                this.datasentflat4(false);
                this.datasentflat5(false);
                this.datasentflat6(false);
                /**************************************************************************/
                this.datasent0(float.Parse(textBox1.Text));
                this.datasent1(float.Parse(textBox2.Text));
                this.datasent2(float.Parse(textBox3.Text));
                this.datasent3(0);
                this.datasent4(0);
                this.datasent5(0);
                this.datasent6(1);
                this.Close();
            }
            else if (comboBox1.SelectedItem.ToString() == "phương trình đặc biệt")
            {
                this.datasentflat0(false);
                this.datasentflat1(true);
                this.datasentflat2(false);
                this.datasentflat3(false);
                this.datasentflat4(false);
                this.datasentflat5(false);
                this.datasentflat6(false);
                /****************************************************************************/
                this.datasent0(float.Parse(textBox1.Text));
                this.datasent1(float.Parse(textBox2.Text));
                this.datasent2(0);
                this.datasent3(0);
                this.datasent4(0);
                this.datasent5(0);
                this.datasent6(1);
                this.Close();
            }
            else if (comboBox1.SelectedItem.ToString() == "phương trình bậc 1")
            {
                this.datasentflat0(false);
                this.datasentflat1(false);
                this.datasentflat2(true);
                this.datasentflat3(false);
                this.datasentflat4(false);
                this.datasentflat5(false);
                this.datasentflat6(false);
                /****************************************************************************/
                this.datasent0(float.Parse(textBox1.Text));
                this.datasent1(float.Parse(textBox2.Text));
                this.datasent2(0);
                this.datasent3(0);
                this.datasent4(0);
                this.datasent5(0);
                this.datasent6(1);
                this.Close();
            }
            else if (comboBox1.SelectedItem.ToString() == "phương trình bậc 2")
            {
                this.datasentflat0(false);
                this.datasentflat1(false);
                this.datasentflat2(false);
                this.datasentflat3(true);
                this.datasentflat4(false);
                this.datasentflat5(false);
                this.datasentflat6(false);
                /****************************************************************************/
                this.datasent0(float.Parse(textBox1.Text));
                this.datasent1(float.Parse(textBox2.Text));
                this.datasent2(float.Parse(textBox3.Text));
                this.datasent3(0);
                this.datasent4(0);
                this.datasent5(0);
                this.datasent6(1);
                this.Close();
            }
            else if (comboBox1.SelectedItem.ToString() == "phương trình bậc 3")
            {
                this.datasentflat0(false);
                this.datasentflat1(false);
                this.datasentflat2(false);
                this.datasentflat3(false);
                this.datasentflat4(true);
                this.datasentflat5(false);
                this.datasentflat6(false);
                /****************************************************************************/
                this.datasent0(float.Parse(textBox1.Text));
                this.datasent1(float.Parse(textBox2.Text));
                this.datasent2(float.Parse(textBox3.Text));
                this.datasent3(float.Parse(textBox4.Text));
                this.datasent4(0);
                this.datasent5(0);
                this.datasent6(1);
                this.Close();
            }
            else if (comboBox1.SelectedItem.ToString() == "phương trình bậc 4")
            {
                this.datasentflat0(false);
                this.datasentflat1(false);
                this.datasentflat2(false);
                this.datasentflat3(false);
                this.datasentflat4(false);
                this.datasentflat5(true);
                this.datasentflat6(false);
                /****************************************************************************/
                this.datasent0(float.Parse(textBox1.Text));
                this.datasent1(float.Parse(textBox2.Text));
                this.datasent2(float.Parse(textBox3.Text));
                this.datasent3(float.Parse(textBox4.Text));
                this.datasent4(float.Parse(textBox5.Text));
                this.datasent5(0);
                this.datasent6(1);
                this.Close();
            }
            else if (comboBox1.SelectedItem.ToString() == "phương trình bậc 5")
            {
                this.datasentflat0(false);
                this.datasentflat1(false);
                this.datasentflat2(false);
                this.datasentflat3(false);
                this.datasentflat4(false);
                this.datasentflat5(false);
                this.datasentflat6(true);
                /****************************************************************************/
                this.datasent0(float.Parse(textBox1.Text));
                this.datasent1(float.Parse(textBox2.Text));
                this.datasent2(float.Parse(textBox3.Text));
                this.datasent3(float.Parse(textBox4.Text));
                this.datasent4(float.Parse(textBox5.Text));
                this.datasent5(float.Parse(textBox6.Text));
                this.datasent6(1);
                this.Close();
            }
        }
    }
}
