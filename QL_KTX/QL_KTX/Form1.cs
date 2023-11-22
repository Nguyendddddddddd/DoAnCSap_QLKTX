namespace QL_KTX
{
    public partial class Form1 : Form
    {
        Form formhienTai;
        public Form1()
        {
            InitializeComponent();
        }


        private void openChillForm(Form chillForm)
        {
            if (formhienTai != null)
            {
                formhienTai.Close();
            }
            formhienTai = chillForm;
            chillForm.TopLevel = false;
            chillForm.FormBorderStyle = FormBorderStyle.None;
            chillForm.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(chillForm);
            pnlMain.Tag = chillForm;
            pnlMain.BringToFront();
            chillForm.Show();
        }
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "TRANG CHỦ ";
            FormTrangChu formTrangChu = new FormTrangChu();
            openChillForm (formTrangChu);
            
        }

        private void btnSinhVien_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "SINH VIÊN";
            FormSinhVien formSinhVien = new FormSinhVien();
            openChillForm(formSinhVien);

        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "PHÒNG";
            FormPhong formPhong= new FormPhong();
            openChillForm(formPhong);

        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "NHÂN VIÊN";
            FormNhanVien formNhanVien= new FormNhanVien();
            openChillForm(formNhanVien);

        }
    }
}