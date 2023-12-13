namespace QL_KTX
{
    partial class DanhSachPhongDay
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            flowLayoutDay = new FlowLayoutPanel();
            label10 = new Label();
            guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            guna2CustomGradientPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // guna2CustomGradientPanel1
            // 
            guna2CustomGradientPanel1.BorderColor = Color.DimGray;
            guna2CustomGradientPanel1.BorderRadius = 20;
            guna2CustomGradientPanel1.BorderThickness = 1;
            guna2CustomGradientPanel1.Controls.Add(flowLayoutDay);
            guna2CustomGradientPanel1.Controls.Add(label10);
            guna2CustomGradientPanel1.Controls.Add(guna2Separator1);
            guna2CustomGradientPanel1.CustomizableEdges = customizableEdges1;
            guna2CustomGradientPanel1.Location = new Point(13, 24);
            guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            guna2CustomGradientPanel1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2CustomGradientPanel1.Size = new Size(1219, 812);
            guna2CustomGradientPanel1.TabIndex = 0;
            // 
            // flowLayoutDay
            // 
            flowLayoutDay.BackColor = Color.White;
            flowLayoutDay.Location = new Point(59, 69);
            flowLayoutDay.Name = "flowLayoutDay";
            flowLayoutDay.Size = new Size(1100, 737);
            flowLayoutDay.TabIndex = 3;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.White;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label10.ForeColor = Color.FromArgb(15, 2, 73);
            label10.Location = new Point(27, 17);
            label10.Name = "label10";
            label10.Size = new Size(216, 28);
            label10.TabIndex = 2;
            label10.Text = "Danh sách phòng dãy";
            // 
            // guna2Separator1
            // 
            guna2Separator1.BackColor = Color.White;
            guna2Separator1.Location = new Point(3, 56);
            guna2Separator1.Name = "guna2Separator1";
            guna2Separator1.Size = new Size(1213, 15);
            guna2Separator1.TabIndex = 0;
            // 
            // DanhSachPhongDay
            // 
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1244, 848);
            Controls.Add(guna2CustomGradientPanel1);
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Pixel);
            Margin = new Padding(2, 1, 2, 1);
            Name = "DanhSachPhongDay";
            Text = "DanhSachPhongDay";
            Load += DanhSachPhongDay_Load;
            guna2CustomGradientPanel1.ResumeLayout(false);
            guna2CustomGradientPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private Label label10;
        private FlowLayoutPanel flowLayoutDay;
    }
}