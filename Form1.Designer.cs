namespace howto_trominoes
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtN = new System.Windows.Forms.TextBox();
            this.btnTile = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picBoard = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.auto = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.color = new System.Windows.Forms.CheckBox();
            this.zaman = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.justgenerate = new System.Windows.Forms.CheckBox();
            this.rndcolor = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(953, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "N:";
            // 
            // txtN
            // 
            this.txtN.Location = new System.Drawing.Point(977, 28);
            this.txtN.Name = "txtN";
            this.txtN.Size = new System.Drawing.Size(47, 20);
            this.txtN.TabIndex = 1;
            this.txtN.Text = "2";
            this.txtN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnTile
            // 
            this.btnTile.Location = new System.Drawing.Point(968, 54);
            this.btnTile.Name = "btnTile";
            this.btnTile.Size = new System.Drawing.Size(75, 23);
            this.btnTile.TabIndex = 2;
            this.btnTile.Text = "Tile";
            this.btnTile.UseVisualStyleBackColor = true;
            this.btnTile.Click += new System.EventHandler(this.btnTile_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.picBoard);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(906, 551);
            this.panel1.TabIndex = 5;
            // 
            // picBoard
            // 
            this.picBoard.BackColor = System.Drawing.Color.White;
            this.picBoard.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picBoard.Location = new System.Drawing.Point(29, 59);
            this.picBoard.Name = "picBoard";
            this.picBoard.Size = new System.Drawing.Size(863, 405);
            this.picBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBoard.TabIndex = 4;
            this.picBoard.TabStop = false;
            this.picBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.picBoard_Paint_1);
            this.picBoard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoard_MouseDown);
            // 
            // panel2
            // 
            this.panel2.AccessibleName = "paenl2";
            this.panel2.Controls.Add(this.auto);
            this.panel2.Controls.Add(this.checkBox3);
            this.panel2.Controls.Add(this.checkBox2);
            this.panel2.Controls.Add(this.checkBox4);
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(945, 129);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 5;
            // 
            // auto
            // 
            this.auto.AutoSize = true;
            this.auto.Location = new System.Drawing.Point(42, 3);
            this.auto.Name = "auto";
            this.auto.Size = new System.Drawing.Size(47, 17);
            this.auto.TabIndex = 10;
            this.auto.Text = "auto";
            this.auto.UseVisualStyleBackColor = true;
            this.auto.Click += new System.EventHandler(this.checkBox4_Click_1);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(40, 70);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(43, 17);
            this.checkBox3.TabIndex = 0;
            this.checkBox3.Text = "3px";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.Click += new System.EventHandler(this.checkBox3_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(40, 47);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(43, 17);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "6px";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Click += new System.EventHandler(this.checkBox2_Click);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(95, 24);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(49, 17);
            this.checkBox4.TabIndex = 0;
            this.checkBox4.Text = "20px";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.Click += new System.EventHandler(this.checkBox4_Click_2);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(40, 24);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(49, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "12px";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Location = new System.Drawing.Point(-933, -117);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(906, 551);
            this.panel3.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox1.Location = new System.Drawing.Point(29, 59);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(863, 405);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.picBoard_Paint_1);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoard_MouseDown);
            // 
            // color
            // 
            this.color.AutoSize = true;
            this.color.Checked = true;
            this.color.CheckState = System.Windows.Forms.CheckState.Checked;
            this.color.Location = new System.Drawing.Point(1007, 264);
            this.color.Name = "color";
            this.color.Size = new System.Drawing.Size(60, 17);
            this.color.TabIndex = 6;
            this.color.Text = "color it!";
            this.color.UseVisualStyleBackColor = true;
            this.color.Click += new System.EventHandler(this.checkBox4_Click);
            // 
            // zaman
            // 
            this.zaman.Enabled = false;
            this.zaman.Location = new System.Drawing.Point(1076, 28);
            this.zaman.Name = "zaman";
            this.zaman.Size = new System.Drawing.Size(100, 20);
            this.zaman.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1035, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "zaman:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1076, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "zakhire";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // justgenerate
            // 
            this.justgenerate.AutoSize = true;
            this.justgenerate.Location = new System.Drawing.Point(977, 298);
            this.justgenerate.Name = "justgenerate";
            this.justgenerate.Size = new System.Drawing.Size(87, 17);
            this.justgenerate.TabIndex = 9;
            this.justgenerate.Text = "just generate";
            this.justgenerate.UseVisualStyleBackColor = true;
            // 
            // rndcolor
            // 
            this.rndcolor.AutoSize = true;
            this.rndcolor.Checked = true;
            this.rndcolor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rndcolor.Location = new System.Drawing.Point(1018, 356);
            this.rndcolor.Name = "rndcolor";
            this.rndcolor.Size = new System.Drawing.Size(92, 17);
            this.rndcolor.TabIndex = 0;
            this.rndcolor.Text = "random colors";
            this.rndcolor.UseVisualStyleBackColor = true;
            this.rndcolor.Click += new System.EventHandler(this.rndcolor_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnTile;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1188, 575);
            this.Controls.Add(this.justgenerate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rndcolor);
            this.Controls.Add(this.zaman);
            this.Controls.Add(this.color);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnTile);
            this.Controls.Add(this.txtN);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "howto_trominoes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtN;
        private System.Windows.Forms.Button btnTile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picBoard;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox color;
        private System.Windows.Forms.TextBox zaman;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox justgenerate;
        private System.Windows.Forms.CheckBox auto;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox rndcolor;
    }
}

