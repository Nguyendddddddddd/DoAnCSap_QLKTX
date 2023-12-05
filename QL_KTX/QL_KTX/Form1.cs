namespace QL_KTX
{
    public partial class Form1 : Form
    {
        Form formhienTai;
        FormTrangChu formTrangChu;
        FormSinhVien formSinhVien;
        FormPhong formPhong;
        FormNhanVien formNhanVien;


        public Form1()
        {
            InitializeComponent();
            formTrangChu = new FormTrangChu();
            formSinhVien = new FormSinhVien();
            formPhong = new FormPhong();
            formNhanVien = new FormNhanVien();

        }


        private void openChillForm(Form chillForm)
        {
            if (formhienTai != null)
            {
                formhienTai.Hide();
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
          
            openChillForm (formTrangChu);
            
        }

        private void btnSinhVien_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "SINH VIÊN";
           
            openChillForm(formSinhVien);

        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "PHÒNG";
            
            openChillForm(formPhong);

        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "NHÂN VIÊN";
            openChillForm(formNhanVien);

        }
    }
}