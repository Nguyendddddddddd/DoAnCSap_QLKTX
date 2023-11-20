namespace QLKTX_AGU
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            panel3 = new Panel();
            label2 = new Label();
            label1 = new Label();
            panel2 = new Panel();
            label3 = new Label();
            panel4 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.ForeColor = Color.White;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(240, 1049);
            panel1.TabIndex = 0;
            // 
            // button5
            // 
            button5.ForeColor = Color.Black;
            button5.Location = new Point(12, 915);
            button5.Name = "button5";
            button5.Size = new Size(214, 44);
            button5.TabIndex = 26;
            button5.Text = "Thoát";
            button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.ForeColor = Color.Black;
            button4.Location = new Point(12, 445);
            button4.Name = "button4";
            button4.Size = new Size(214, 44);
            button4.TabIndex = 25;
            button4.Text = "Đăng Xuất";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.ForeColor = Color.Black;
            button3.Location = new Point(12, 375);
            button3.Name = "button3";
            button3.Size = new Size(214, 44);
            button3.TabIndex = 24;
            button3.Text = "Nhân Viên";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.ForeColor = Color.Black;
            button2.Location = new Point(12, 306);
            button2.Name = "button2";
            button2.Size = new Size(214, 44);
            button2.TabIndex = 23;
            button2.Text = "Phòng";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = Color.FromArgb(15, 3, 77);
            button1.Location = new Point(12, 237);
            button1.Name = "button1";
            button1.Size = new Size(214, 44);
            button1.TabIndex = 0;
            button1.Text = "Sinh viên";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Black;
            panel3.ForeColor = Color.Black;
            panel3.Location = new Point(82, 83);
            panel3.Name = "panel3";
            panel3.Size = new Size(60, 60);
            panel3.TabIndex = 22;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(6, 0, 70);
            label2.Location = new Point(12, 161);
            label2.Name = "label2";
            label2.Size = new Size(206, 38);
            label2.TabIndex = 21;
            label2.Text = "Người quản lý";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Tai Le", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(6, 0, 70);
            label1.Location = new Point(12, 25);
            label1.Name = "label1";
            label1.Size = new Size(196, 31);
            label1.TabIndex = 20;
            label1.Text = "KÝ TÚC XÁ AGU";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(label3);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(238, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1276, 986);
            panel2.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Tai Le", 14F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(20, 9);
            label3.Name = "label3";
            label3.Size = new Size(0, 35);
            label3.TabIndex = 3;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(255, 242, 236);
            panel4.Location = new Point(238, 98);
            panel4.Name = "panel4";
            panel4.Size = new Size(1273, 937);
            panel4.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1514, 986);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private Label label2;
        private Panel panel3;
        private Button button1;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button5;
        private Label label3;
        private Panel panel4;
    }
}