namespace QL_KTX
{
    public partial class Form1 : Form
    {
        Form formhienTai;
        FormTrangChu formTrangChu;
        FormSinhVien formSinhVien;
        FormPhong formPhong;
        FormNhanVien formNhanVien;
        FormCapTaiKhoan formCapTaiKhoan;

        FormDangNhap dangNhap;
        public int quyen;
        public string tenTK;


        public Form1()
        {
            InitializeComponent();
            formTrangChu = new FormTrangChu();
            formSinhVien = new FormSinhVien();
            formPhong = new FormPhong();
            formNhanVien = new FormNhanVien();
            formCapTaiKhoan = new FormCapTaiKhoan();

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

            openChillForm(formTrangChu);

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

        private void btnCapTaiKhoan_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "CẤP TÀI KHOẢN";
            openChillForm(formCapTaiKhoan);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Bạn có muốn đăng xuất ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (d == DialogResult.OK)
            {
                this.Close();
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (quyen == 1)
            {
                btnNhanVien.Enabled = true;
                btnCapTaiKhoan.Enabled = true;
            }
            sttThoiGianDN.Text = "TG: " + DateTime.Now.ToString();
            lblUserName.Text = tenTK.ToUpper();

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dialog == DialogResult.OK)
            {
                Application.Exit();
            }
        }


    }
}