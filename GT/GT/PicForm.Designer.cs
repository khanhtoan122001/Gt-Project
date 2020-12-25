
namespace GT
{
    partial class PicForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PicForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newtoolStripButton = new System.Windows.Forms.ToolStripLabel();
            this.opentoolStripButton = new System.Windows.Forms.ToolStripLabel();
            this.savetoolStripButton = new System.Windows.Forms.ToolStripLabel();
            this.saveastoolStripButton = new System.Windows.Forms.ToolStripLabel();
            this.deletetoolStripButton = new System.Windows.Forms.ToolStripLabel();
            this.exittoolStripButton = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newtoolStripButton,
            this.opentoolStripButton,
            this.savetoolStripButton,
            this.saveastoolStripButton,
            this.deletetoolStripButton,
            this.exittoolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(9, 9);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(462, 71);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newtoolStripButton
            // 
            this.newtoolStripButton.AutoSize = false;
            this.newtoolStripButton.BackgroundImage = global::GT.Properties.Resources.icons8_add_file_801;
            this.newtoolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.newtoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newtoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newtoolStripButton.Name = "newtoolStripButton";
            this.newtoolStripButton.Size = new System.Drawing.Size(50, 50);
            this.newtoolStripButton.Text = "       ";
            this.newtoolStripButton.Click += new System.EventHandler(this.newToolStripMenuItem1_Click);
            // 
            // opentoolStripButton
            // 
            this.opentoolStripButton.AutoSize = false;
            this.opentoolStripButton.BackgroundImage = global::GT.Properties.Resources.icons8_opened_folder_144;
            this.opentoolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.opentoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.opentoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.opentoolStripButton.Name = "opentoolStripButton";
            this.opentoolStripButton.Size = new System.Drawing.Size(50, 50);
            this.opentoolStripButton.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // savetoolStripButton
            // 
            this.savetoolStripButton.AutoSize = false;
            this.savetoolStripButton.BackgroundImage = global::GT.Properties.Resources.icons8_save_100;
            this.savetoolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.savetoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.savetoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.savetoolStripButton.Name = "savetoolStripButton";
            this.savetoolStripButton.Size = new System.Drawing.Size(50, 50);
            this.savetoolStripButton.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveastoolStripButton
            // 
            this.saveastoolStripButton.AutoSize = false;
            this.saveastoolStripButton.BackgroundImage = global::GT.Properties.Resources.icons8_save_as_100;
            this.saveastoolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.saveastoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveastoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveastoolStripButton.Name = "saveastoolStripButton";
            this.saveastoolStripButton.Size = new System.Drawing.Size(50, 50);
            this.saveastoolStripButton.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // deletetoolStripButton
            // 
            this.deletetoolStripButton.AutoSize = false;
            this.deletetoolStripButton.BackgroundImage = global::GT.Properties.Resources.icons8_delete_bin_96;
            this.deletetoolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.deletetoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deletetoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deletetoolStripButton.Name = "deletetoolStripButton";
            this.deletetoolStripButton.Size = new System.Drawing.Size(50, 50);
            this.deletetoolStripButton.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // exittoolStripButton
            // 
            this.exittoolStripButton.AutoSize = false;
            this.exittoolStripButton.BackgroundImage = global::GT.Properties.Resources.icons8_exit_52;
            this.exittoolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.exittoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exittoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exittoolStripButton.Name = "exittoolStripButton";
            this.exittoolStripButton.Size = new System.Drawing.Size(51, 51);
            this.exittoolStripButton.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.toolStripLabel3});
            this.toolStrip2.Location = new System.Drawing.Point(471, 9);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(320, 71);
            this.toolStrip2.TabIndex = 6;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel1.Image")));
            this.toolStripLabel1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(49, 68);
            this.toolStripLabel1.Text = "None";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click_1);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel2.Image")));
            this.toolStripLabel2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(56, 68);
            this.toolStripLabel2.Text = "Export";
            this.toolStripLabel2.Click += new System.EventHandler(this.toolStripLabel2_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel3.Image")));
            this.toolStripLabel3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(46, 68);
            this.toolStripLabel3.Text = "Point";
            this.toolStripLabel3.Click += new System.EventHandler(this.toolStripLabel3_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Location = new System.Drawing.Point(12, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 355);
            this.panel1.TabIndex = 7;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(776, 355);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer2.Panel2Collapsed = true;
            this.splitContainer2.Size = new System.Drawing.Size(514, 355);
            this.splitContainer2.SplitterDistance = 171;
            this.splitContainer2.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(514, 355);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(258, 355);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.SizeChanged += new System.EventHandler(this.flowLayoutPanel1_SizeChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // PicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "PicForm";
            this.Text = "PicForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel newtoolStripButton;
        private System.Windows.Forms.ToolStripLabel opentoolStripButton;
        private System.Windows.Forms.ToolStripLabel savetoolStripButton;
        private System.Windows.Forms.ToolStripLabel saveastoolStripButton;
        private System.Windows.Forms.ToolStripLabel deletetoolStripButton;
        private System.Windows.Forms.ToolStripLabel exittoolStripButton;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolStripLabel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}