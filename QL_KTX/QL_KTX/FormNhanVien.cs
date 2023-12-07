using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_KTX
{
    public partial class FormNhanVien : Form
    {
        string strCon = @"Data Source=HOAINAMPC\SQLSERVER;Initial Catalog=QLKTX;Integrated Security=True";
        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet dataset = null;

        public FormNhanVien()
        {
            InitializeComponent();
            cboNVThem_GioiTinh.SelectedItem = "Nam";
            LoadDataControl.Load_DataComBoBox(cboNVThem_ChucVu, " select cv.MaCV, cv.TenCV, cv.PhuCapChucVu from ChucVu as cv", "TenCV", "MaCV");
            LoadDataControl.Load_DataComBoBox(cboNVSua_ChucVu, " select cv.MaCV, cv.TenCV, cv.PhuCapChucVu from ChucVu as cv", "TenCV", "MaCV");
            cboNVSua_GioiTinh.Items.Add("Nam");
            cboNVSua_GioiTinh.Items.Add("Nữ");
        }

        private void txtNVThem_Them_Click(object sender, EventArgs e)
        {
            themNhanVien();
        }

        public void themNhanVien()
        {
            ConnectData connectData = new ConnectData();
            connectData.openConnect();

            string hoten = txtNVThem_HoTenLot.Text;
            string ten = txtNVThem_Ten.Text;
            string gioiTinh = cboNVThem_GioiTinh.SelectedItem.ToString();
            string ngaySinh = dtpNVThem_NgaySinh.Value.ToShortDateString();
            string noiSinh = txtNVThem_NoiSinh.Text;
            int sdt = int.Parse(txtNVThem_SDT.Text);
            string email = txtNVThem_Email.Text;
            string cccd = txtNVThem_CCCD.Text;
            string ngayCap = dtpNVSua_NgayCap.Value.ToShortDateString();
            string noiCap = cboNVThem_NoiCap.SelectedItem.ToString();
            string maNhanVien = txtNVThem_MaNhanVien.Text;
            int luongChinh = int.Parse(txtNVThem_LuongChinh.Text);
            DataRowView macv = (DataRowView)cboNVThem_ChucVu.SelectedItem;
            string chucvu = macv["MaCV"].ToString();
            string thanhpho = txtNVThem_ThanhPho.Text;
            string huyen = txtNVThem_Huyen.Text;
            string xa = txtNVThem_Phuong.Text;
            string soNha = txtNVThem_HoKhau.Text;

            string HKTT = $"{soNha}, {xa}, {huyen}, {thanhpho}";


            string sql = "insert into NhanVien values('" + maNhanVien + "', N'" + hoten + "', N'" + ten + "', N'" + gioiTinh + "', N'" + ngaySinh + "', '" + cccd + "', N'" + ngayCap + "', N'" + noiCap + "', N'" + noiSinh + "', N'" + HKTT + "', " + sdt + ", '" + email + "', " + luongChinh + ", " + 1 + ", '" + chucvu + "')";
            connectData.insertData(sql);
            connectData.closeConnect();



        }

        private void FormNhanVien_Load(object sender, EventArgs e)
        {

            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(strCon);
            }

            string query = "select * from NhanVien";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            dataset = new DataSet();
            adapter.Fill(dataset, "tblNhanVien");
            dgvNVSua_DanhSach.DataSource = dataset.Tables["tblNhanVien"];
        }

        int viTri = -1;

        private void btnNVSua_Sua_Click(object sender, EventArgs e)
        {
            if (viTri == -1)
                MessageBox.Show("Bạn chưa chọn sinh viên cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DataRow Row = dataset.Tables["tblNhanVien"].Rows[viTri];

            Row.BeginEdit();
            Row["MaNV"] = txtNVSua_MaNV.Text;
            Row["HovaTenLot"] = txtNVSua_hoTenLot.Text;
            Row["Ten"] = txtNVSua_ten.Text;
            Row["GioiTinh"] = cboNVSua_GioiTinh.SelectedItem.ToString();

            // date chu y
            Row["NgaySinh"] = dtpNVSua_NgaySinh.Text;
            Row["CCCD"] = txtNVSua_CCCD.Text;

            // date chu y
            Row["NgayCap"] = dtpNVSua_NgayCap.Text;
            Row["NoiCap"] = cboNVSua_NoiCap.SelectedItem.ToString();
            Row["NoiSinh"] = txtNVSua_NoiSinh.Text;

            // hoKhauThuongTru
            string HOKHAU = $"{txtNVSua_HoKhau.Text},{txtNVSua_Phuong.Text},{txtNVSua_Quan.Text},{txtNVSua_ThanhPho.Text}";
            Row["HKTT"] = HOKHAU;

            Row["SDT"] = txtNVSua_SDT.Text;
            Row["EMAIL"] = txtNVSua_email.Text;
            Row["LuongChinh"] = txtNVSua_LuongChinh.Text;
            DataRowView macv = (DataRowView)cboNVSua_ChucVu.SelectedItem;
            Row["MaCV"] = macv["MaCV"].ToString();

            Row.EndEdit();

        }


        private void dgvNVSua_DanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // bat enable
            txtNVSua_hoTenLot.Enabled = true;
            txtNVSua_ten.Enabled = true;
            cboNVSua_GioiTinh.Enabled = true;
            dtpNVSua_NgaySinh.Enabled = true;
            txtNVSua_hoTenLot.Enabled = true;
            txtNVSua_NoiSinh.Enabled = true;
            txtNVSua_SDT.Enabled = true;
            txtNVSua_email.Enabled = true;
            txtNVSua_CCCD.Enabled = true;
            dtpNVSua_NgayCap.Enabled = true;
            cboNVSua_NoiCap.Enabled = true;
            txtNVSua_MaNV.Enabled = true;
            cboNVSua_ChucVu.Enabled = true;
            txtNVSua_LuongChinh.Enabled = true;
            txtNVSua_ThanhPho.Enabled = true;
            txtNVSua_Quan.Enabled = true;
            txtNVSua_Phuong.Enabled = true;
            txtNVSua_HoKhau.Enabled = true;


            viTri = e.RowIndex;
            if (viTri == -1) return;
            DataRow Row = dataset.Tables["tblNhanVien"].Rows[viTri];

            Row.BeginEdit();
            txtNVSua_MaNV.Text = Row["MaNV"].ToString();
            txtNVSua_hoTenLot.Text = Row["HovaTenLot"].ToString();
            txtNVSua_ten.Text = Row["Ten"].ToString();
            cboNVSua_GioiTinh.SelectedItem = Row["GioiTinh"].ToString();

            // date chu y
            dtpNVSua_NgaySinh.Text = Row["NgaySinh"].ToString();
            txtNVSua_CCCD.Text = Row["CCCD"].ToString();

            // date chu y
            dtpNVSua_NgayCap.Text = Row["NgayCap"].ToString();
            cboNVSua_NoiCap.SelectedItem = Row["NoiCap"].ToString();
            txtNVSua_NoiSinh.Text = Row["NoiSinh"].ToString();

            // hoKhauThuongTru
            string hktt = Row["HKTT"].ToString();
            string[] manghokhauthuongtru = hktt.Split(',');
            txtNVSua_ThanhPho.Text = manghokhauthuongtru[3];
            txtNVSua_Quan.Text = manghokhauthuongtru[2];
            txtNVSua_Phuong.Text = manghokhauthuongtru[1];
            txtNVSua_HoKhau.Text = manghokhauthuongtru[0];

            txtNVSua_SDT.Text = Row["SDT"].ToString();
            txtNVSua_email.Text = Row["EMAIL"].ToString();
            txtNVSua_LuongChinh.Text = Row["LuongChinh"].ToString();
            cboNVSua_ChucVu.SelectedItem = Row["MaCV"].ToString();

            
        }
        private void btnNVSua_Luu_Click(object sender, EventArgs e)
        {
            int kq = adapter.Update(dataset.Tables["tblNhanVien"]);
            if (kq > 0)
                MessageBox.Show("Lưu thành công", "Thông báo");
            else
                MessageBox.Show("Lưu thất bại", "Thông báo");
        }

        private void btnNVSua_Huy_Click(object sender, EventArgs e)
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(strCon);
            }

            string query = "select * from NhanVien";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            dataset = new DataSet();
            adapter.Fill(dataset, "tblNhanVien");
            dgvNVSua_DanhSach.DataSource = dataset.Tables["tblNhanVien"];
        }
    }
}
