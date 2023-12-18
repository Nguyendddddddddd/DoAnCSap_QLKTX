using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_KTX
{
    public partial class FormDangNhap : Form
    {
        ConnectData conNet = null;
        int quyenTruyCap;
        Form1 form1;

        public FormDangNhap()
        {
            InitializeComponent();
            
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_DangNhap_Click(object sender, EventArgs e)
        {
            MD5 md5 = MD5.Create();
            string taiKhoan = txtDangNhap_TaiKhoan.Text;
            string matKhau = MaHoaPassWord.maHoaMd5Hash(md5, txtDangNhap_MatKhau.Text);
            string query_KTTaiKhoan = $"select * from taikhoan where username = '{taiKhoan}' and pass = '{matKhau}'";

            conNet = new ConnectData();
            DataSet dataset = conNet.ReadData(query_KTTaiKhoan);
            if (dataset.Tables[0].Rows.Count > 0)
            {
                try
                {
                    DataRow row = dataset.Tables[0].Rows[0];
                    quyenTruyCap = int.Parse(row["PhanQuyen"].ToString().Trim());
                    this.Hide();
                    form1 = new Form1();
                    form1.quyen = quyenTruyCap;
                    form1.tenTK = taiKhoan;
                    form1.ShowDialog();
                    form1 = null;
                    this.Show();
                }
                catch (Exception ex)
                {
                    Application.Exit();
                }


                txtDangNhap_TaiKhoan.Clear();
                txtDangNhap_MatKhau.Clear();
                txtDangNhap_TaiKhoan.Focus();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDangNhap_TaiKhoan.Focus();
            }
            conNet.closeConnect();
        }

        private void lblDangNhap_ThoatChuongTrinh_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dialog == DialogResult.OK)
            {
                Application.Exit();
            }
        }


        private void chkDangNhap_GhiNho_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDangNhap_GhiNho.Checked == true)
            {
                txtDangNhap_MatKhau.PasswordChar = '\0';
            }
            else
            {
                txtDangNhap_MatKhau.PasswordChar = '*';
            }
        }
    }
}
