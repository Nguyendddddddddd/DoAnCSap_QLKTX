namespace QL_KTX
{
    partial class FormTrangChu
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
            label1 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(590, 418);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(65, 12);
            label1.TabIndex = 0;
            label1.Text = "Form Trang chủ";
            // 
            // FormTrangChu
            // 
            AutoScaleDimensions = new SizeF(5F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1244, 848);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Pixel);
            Margin = new Padding(2);
            Name = "FormTrangChu";
            Text = "FormTrangChu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
    }
}