using Guna.UI2.WinForms;
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
    public partial class FormCapTaiKhoan : Form
    {
        string strCon = "Data Source=LAPTOP-OUMK55PL\\SQLEXPRESS;Initial Catalog=QLKTX;Integrated Security=True";
        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet dataset = null;

        public FormCapTaiKhoan()
        {
            InitializeComponent();
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(strCon);
            }

            string query_CapTK = "select * from taikhoan";
            adapter = new SqlDataAdapter(query_CapTK,sqlCon);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);
            dataset = new DataSet();
            adapter.Fill(dataset,"tblTaiKhoan");
        }

        private void btnCapTaiKhoan_DangKi_Click(object sender, EventArgs e)
        {
            if (txtCapTaiKhoan_MK.Text != txtCapTaiKhoan_NhapLaiMK.Text)
            {
                MessageBox.Show("Mật khẩu không khớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCapTaiKhoan_MK.Focus();
                return;
            }
            try
            {

                if (!kiemtraTrungDuLieu(txtCapTaiKhoan_TenTK, "UserName"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtCapTaiKhoan_TenTK, "Tên tài khoản đã tồn tại, vui lòng chọn tên tài khoản khác");
                MD5 md5 = MD5.Create();
                string matKhauMaHoa = MaHoaPassWord.maHoaMd5Hash(md5,txtCapTaiKhoan_MK.Text);
                DataRow row = dataset.Tables["tblTaiKhoan"].NewRow();
                row["UserName"] = txtCapTaiKhoan_TenTK.Text.Trim();
                row["Pass"] = matKhauMaHoa.Trim();
                string quyen = cboCapTaiKhoan_LoaiTK.SelectedItem.ToString().Trim();
                if (quyen == "Quản trị")
                {
                    row["PhanQuyen"] = 1;
                }
                else
                {
                    row["PhanQuyen"] = 2;
                }
                dataset.Tables["tblTaiKhoan"].Rows.Add(row);

                int kq = adapter.Update(dataset.Tables["tblTaiKhoan"]);
                if (kq > 0)
                {
                    MessageBox.Show("Cấp tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtCapTaiKhoan_TenTK.Clear();
                    txtCapTaiKhoan_MK.Clear();
                    txtCapTaiKhoan_NhapLaiMK.Clear();
                    cboCapTaiKhoan_LoaiTK.SelectedItem = null;
                    txtCapTaiKhoan_TenTK.Focus();
                }
                else
                {
                    MessageBox.Show("Cấp tài khoản thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ExceptionTrungDuLieuDuyNhat ex)
            {
                MessageBox.Show(ex.ThongBaoLoi,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        public bool kiemtraTrungDuLieu(Guna2TextBox txt, string strKT)
        {
            ConnectData connectData = new ConnectData();
            connectData.openConnect();
            DataSet dataSet = connectData.ReadData($"SELECT {strKT} FROM taikhoan");
            DataTable dataTable = dataSet.Tables[0];
            connectData.closeConnect();
            foreach (DataRow r in dataTable.Rows)
            {
                if (r[strKT].ToString().Trim() == txt.Text.Trim())
                {
                    return false;
                }
            }
            dataSet.Dispose();
            return true;
        }

    }
              
                
}
           
     
