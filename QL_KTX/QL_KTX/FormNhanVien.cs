using Guna.UI2.WinForms;
using QL_KTX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_KTX
{
    public partial class FormNhanVien : Form
    {
        string strCon = @"Data Source=LAPTOP-OUMK55PL\SQLEXPRESS;Initial Catalog=QLKTX;Integrated Security=True";
        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        SqlDataAdapter adapter_NVLamViec = null;
        SqlDataAdapter adapter_NVNghiViec = null;
        SqlDataAdapter adapter_TimKiem = null;
        SqlDataAdapter adapter_InBangLuong = null;
        DataSet dataset = null;
        DataSet dataset_InBangLuong = null;

        public FormNhanVien()
        {
            InitializeComponent();
            cboNVThem_GioiTinh.SelectedItem = "Nam";
            LoadDataControl.Load_DataComBoBox(cboNVThem_ChucVu, " select cv.MaCV, cv.TenCV, cv.PhuCapChucVu from ChucVu as cv", "TenCV", "MaCV");
            LoadDataControl.Load_DataComBoBox(cboNVSua_ChucVu, " select cv.MaCV, cv.TenCV, cv.PhuCapChucVu from ChucVu as cv", "TenCV", "MaCV");
            LoadDataControl.Load_DataComBoBox(cboInBangLuong_ChucVu, " select cv.MaCV, cv.TenCV, cv.PhuCapChucVu from ChucVu as cv", "TenCV", "MaCV");
            LoadDataControl.Load_DataComBoBox(cboInBangLuong_MaNV, " select nv.MaNV,nv.MaCV from NhanVien as nv", "MaNV", "MaNV");

            cboNVSua_GioiTinh.Items.Add("Nam");
            cboNVSua_GioiTinh.Items.Add("Nữ");



        }
        public bool kiemtraTrungDuLieu(Guna2TextBox txt, string strKT, string table)
        {
            ConnectData connectData = new ConnectData();
            connectData.openConnect();
            DataSet dataSet = connectData.ReadData($"SELECT {strKT} FROM {table}");
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
        public bool IsEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
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
            dgvNVXoa_DanhSach.DataSource = dataset.Tables["tblNhanVien"];
            dgvDSNVLamViec_DanhSach.DataSource = dataset.Tables["tblNhanVien"];

            // truy van nhan vien lam viec
            string query_NVLamViec = "select * from nhanvien where TrangThaiLamViec = 1";
            adapter_NVLamViec = new SqlDataAdapter(query_NVLamViec, sqlCon);
            adapter_NVLamViec.Fill(dataset, "tblNhanVienLamViec");
            dgvDSNVLamViec_DanhSach.DataSource = dataset.Tables["tblNhanVienLamViec"];
            dgvTinhLuong_DanhSach.DataSource = dataset.Tables["tblNhanVienLamViec"];

            // truy van nhan vien nghi viec
            string query_NVNghiViec = "select * from nhanvien where TrangThaiLamViec = 0";
            adapter_NVNghiViec = new SqlDataAdapter(query_NVNghiViec, sqlCon);
            adapter_NVNghiViec.Fill(dataset, "tblNhanVienNghiViec");
            dgvNVNghiViec_DanhSach.DataSource = dataset.Tables["tblNhanVienNghiViec"];

            // In Bang Luong
            string query_LuongNhanVien = "  select * from luongnhanvien";
            adapter_InBangLuong = new SqlDataAdapter(query_LuongNhanVien, sqlCon);
            SqlCommandBuilder sqlCommandBuilder_InBangLuong = new SqlCommandBuilder(adapter_InBangLuong);
            dataset_InBangLuong = new DataSet();
            adapter_InBangLuong.Fill(dataset_InBangLuong, "tblInBangLuong_Them");
            dgvTinhLuong_DanhSach.DataSource = dataset_InBangLuong.Tables["tblInBangLuong_Them"];
        }

        private void txtNVThem_Them_Click(object sender, EventArgs e)
        {
            themNhanVien();
        }

        public void themNhanVien()
        {
            try
            {
                DataRow row = dataset.Tables["tblNhanVien"].NewRow();
                if (txtNVThem_MaNhanVien.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtNVThem_MaNhanVien, "Mã nhân viên");
                if (txtNVThem_HoTenLot.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtNVThem_HoTenLot, "Họ và Tên lót");
                if (txtNVThem_Ten.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtNVThem_Ten, "Ten");
                if (txtNVThem_CCCD.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtNVThem_CCCD, "CCCD");
                if (txtNVThem_NoiSinh.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtNVThem_NoiSinh, "Nơi Sinh");
                if (txtNVThem_ThanhPho.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtNVThem_ThanhPho, "Tỉnh");
                if (txtNVThem_Huyen.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtNVThem_Huyen, "Huyện");
                if (txtNVThem_Phuong.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtNVThem_Phuong, "Xã");
                if (txtNVThem_HoKhau.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtNVThem_HoKhau, "Hộ Khẩu");
                if (txtNVThem_SDT.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtNVThem_SDT, "Số Điện Thoại");
                if (txtNVThem_Email.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtNVThem_Email, "Email");
                if (txtNVThem_LuongChinh.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtNVThem_LuongChinh, "Lương chính");

                if (!kiemtraTrungDuLieu(txtNVThem_MaNhanVien, "MaNV", "NhanVien"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtNVThem_MaNhanVien, "Mã số nhân viên đã tồn tại vui lòng nhập mã số nhân viên khác");
                if (!kiemtraTrungDuLieu(txtNVThem_SDT, "SDT", "NhanVien"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtNVThem_SDT, "Số điện thoại đã tồn tại vui lòng nhập số điện thoại khác");
                if (!kiemtraTrungDuLieu(txtNVThem_Email, "Email", "NhanVien"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtNVThem_Email, "Email đã tồn tại vui lòng nhập email khác");
                if (!kiemtraTrungDuLieu(txtNVThem_CCCD, "CCCD", "NhanVien"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtNVThem_CCCD, "CCCD đã tồn tại vui lòng nhập CCCD khác");

                if (!IsEmail(txtNVThem_Email.Text))
                {
                    MessageBox.Show("Email Không Đúng định dạng vui lòng nhập lại");
                    return;
                }
                row["MaNV"] = txtNVThem_MaNhanVien.Text;
                row["HovaTenLot"] = txtNVThem_HoTenLot.Text.Trim();
                row["Ten"] = txtNVThem_Ten.Text.Trim();
                row["GioiTinh"] = cboNVThem_GioiTinh.SelectedItem.ToString();
                row["NgaySinh"] = dtpNVThem_NgaySinh.Value.ToShortDateString();
                row["CCCD"] = txtNVThem_CCCD.Text.Trim();
                row["NgayCap"] = dtpNVThem_NgayCap.Value.ToShortDateString();
                row["NoiCap"] = cboNVThem_NoiCap.SelectedItem.ToString();
                row["NoiSinh"] = txtNVThem_NoiSinh.Text.Trim();
                string thanhpho = txtNVThem_ThanhPho.Text;
                string huyen = txtNVThem_Huyen.Text;
                string xa = txtNVThem_Phuong.Text;
                string soNha = txtNVThem_HoKhau.Text;
                string HKTT = $"{soNha}, {xa}, {huyen}, {thanhpho}";
                row["HKTT"] = HKTT.Trim();
                row["SDT"] = txtNVThem_SDT.Text.Trim();
                row["Email"] = txtNVThem_Email.Text.Trim();
                row["LuongChinh"] = int.Parse(txtNVThem_LuongChinh.Text);
                row["TrangThaiLamViec"] = 1;
                DataRowView macv = (DataRowView)cboNVThem_ChucVu.SelectedItem;
                row["MaCV"] = macv["MaCV"].ToString();

                // string sql = "insert into NhanVien values('" + maNhanVien + "', N'" + hoten + "', N'" + ten + "', N'" + gioiTinh + "', N'" + ngaySinh + "', " + cccd + ", N'" + ngayCap + "', N'" + noiCap + "', N'" + noiSinh + "', N'" + HKTT + "', " + sdt + ", '" + email + "', " + luongChinh + ", " + 1 + ", '" + chucvu + "')";
                dataset.Tables["tblNhanVien"].Rows.Add(row);
                int kq = adapter.Update(dataset.Tables["tblNhanVien"]);
                if (kq > 0)
                    MessageBox.Show("Thêm nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Thêm nhân viên thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // them xong xoa du lieu o cac o textbox
                txtNVThem_HoTenLot.Clear();
                txtNVThem_Ten.Clear();
                cboNVThem_GioiTinh.SelectedIndex = 0;
                dtpNVThem_NgaySinh.Value = DateTime.Now;
                txtNVThem_NoiSinh.Clear();
                txtNVThem_SDT.Clear();
                txtNVThem_Email.Clear();
                txtNVThem_CCCD.Clear();
                dtpNVThem_NgayCap.Value = DateTime.Now;
                cboNVThem_NoiCap.SelectedIndex = 0;
                txtNVThem_MaNhanVien.Clear();
                cboNVThem_ChucVu.SelectedIndex = 0;
                txtNVThem_LuongChinh.Clear();
                txtNVThem_ThanhPho.Clear();
                txtNVThem_Huyen.Clear();
                txtNVThem_Phuong.Clear();
                txtNVThem_HoKhau.Clear();
                txtNVThem_HoTenLot.Focus();
            }
            catch (ExceptionKhongCoDuLieu ex)
            {
                MessageBox.Show($"Bạn chưa nhập ${ex.thongBaoLoi}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ex.txt.Focus();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Lương chính phải là số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ExceptionTrungDuLieuDuyNhat ex)
            {
                MessageBox.Show(ex.ThongBaoLoi);
            }


        }



        int viTri = -1;

        private void dgvNVSua_DanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataset.Tables["tblNhanVien"].RejectChanges();
            btnNVSua_Sua.Enabled = true;
            btnNVSua_Luu.Enabled = false;
            btnNVSua_Huy.Enabled = false;
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
            rdbNVSua_DangLam.Enabled = true;
            rdbNVSua_NghiLam.Enabled = true;


            viTri = e.RowIndex;
            if (viTri == -1) return;
            if (viTri >= dataset.Tables["tblNhanVien"].Rows.Count) return;

            DataRow Row = dataset.Tables["tblNhanVien"].Rows[viTri];


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

            int trangThai = int.Parse(Row["TrangThaiLamViec"].ToString());
            if (trangThai == 1)
                rdbNVSua_DangLam.Checked = true;
            else
                rdbNVSua_NghiLam.Checked = true;

            txtNVSua_SDT.Text = Row["SDT"].ToString();
            txtNVSua_email.Text = Row["EMAIL"].ToString();
            txtNVSua_LuongChinh.Text = Row["LuongChinh"].ToString();
            cboNVSua_ChucVu.SelectedValue = Row["MaCV"];   // CON LOI CHUA FIX DUOC, CHO HIEN THI TEN CHUC VU LEN COBOBOX, DA TEXT THU NHUNG CHI HIEN THI BAO VE LEN THOI


        }

        private void btnNVSua_Sua_Click(object sender, EventArgs e)
        {
            // enable nut sua, luu, huy
            btnNVSua_Luu.Enabled = true;
            btnNVSua_Huy.Enabled = true;


            if (viTri == -1)
            {
                MessageBox.Show("Bạn chưa chọn nhân viên cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (viTri >= dataset.Tables["tblNhanVien"].Rows.Count)
            {
                MessageBox.Show("Vị trí chọn không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtNVSua_MaNV.Text == "")
                throw new ExceptionKhongCoDuLieu(txtNVSua_MaNV, "Mã nhân viên");
            if (txtNVSua_hoTenLot.Text == "")
                throw new ExceptionKhongCoDuLieu(txtNVSua_hoTenLot, "Họ và Tên lót");
            if (txtNVSua_ten.Text == "")
                throw new ExceptionKhongCoDuLieu(txtNVSua_ten, "Ten");
            if (txtNVSua_CCCD.Text == "")
                throw new ExceptionKhongCoDuLieu(txtNVSua_CCCD, "CCCD");
            if (txtNVSua_NoiSinh.Text == "")
                throw new ExceptionKhongCoDuLieu(txtNVSua_NoiSinh, "Nơi Sinh");
            if (txtNVSua_ThanhPho.Text == "")
                throw new ExceptionKhongCoDuLieu(txtNVSua_ThanhPho, "Tỉnh");
            if (txtNVSua_Quan.Text == "")
                throw new ExceptionKhongCoDuLieu(txtNVSua_Quan, "Huyện");
            if (txtNVSua_Phuong.Text == "")
                throw new ExceptionKhongCoDuLieu(txtNVSua_Phuong, "Xã");
            if (txtNVSua_HoKhau.Text == "")
                throw new ExceptionKhongCoDuLieu(txtNVSua_HoKhau, "Hộ Khẩu");
            if (txtNVSua_SDT.Text == "")
                throw new ExceptionKhongCoDuLieu(txtNVSua_SDT, "Số Điện Thoại");
            if (txtNVSua_email.Text == "")
                throw new ExceptionKhongCoDuLieu(txtNVSua_email, "Email");
            if (txtNVSua_LuongChinh.Text == "")
                throw new ExceptionKhongCoDuLieu(txtNVSua_LuongChinh, "Lương chính");
            if (!IsEmail(txtNVSua_email.Text))
            {
                MessageBox.Show("Email Không Đúng định dạng vui lòng nhập lại");
                return;
            }
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
            Row["LuongChinh"] = int.Parse(txtNVSua_LuongChinh.Text);

            if (rdbNVSua_DangLam.Checked)
            {
                Row["TrangThaiLamViec"] = 1;
            }
            else
            {
                Row["TrangThaiLamViec"] = 0;
            }

            DataRowView macv = (DataRowView)cboNVSua_ChucVu.SelectedItem;
            Row["MaCV"] = macv["MaCV"].ToString();
            Row.EndEdit();

            // xoa du lieu tren cac o texbox
            txtNVSua_hoTenLot.Clear();
            txtNVSua_ten.Clear();
            cboNVSua_GioiTinh.SelectedIndex = 0;
            dtpNVSua_NgaySinh.Value = DateTime.Now;
            txtNVSua_NoiSinh.Clear();
            txtNVSua_SDT.Clear();
            txtNVSua_email.Clear();
            txtNVSua_CCCD.Clear();
            dtpNVSua_NgayCap.Value = DateTime.Now;
            cboNVSua_NoiCap.SelectedIndex = 0;
            txtNVSua_MaNV.Clear();
            cboNVSua_ChucVu.SelectedIndex = 0;
            txtNVSua_LuongChinh.Clear();
            txtNVSua_ThanhPho.Clear();
            txtNVSua_Quan.Clear();
            txtNVSua_Phuong.Clear();
            txtNVSua_HoKhau.Clear();
            rdbNVSua_DangLam.Checked = false;
            rdbNVSua_NghiLam.Checked = false;


        }



        private void btnNVSua_Luu_Click(object sender, EventArgs e)
        {
            btnNVSua_Huy.Enabled = false;
            int kq = adapter.Update(dataset.Tables["tblNhanVien"]);
            if (kq > 0)
                MessageBox.Show("Lưu thông tin nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Lưu thông tin nhân viên thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnNVSua_Huy_Click(object sender, EventArgs e)
        {
            btnNVSua_Sua.Enabled = false;
            btnNVSua_Luu.Enabled = false;

            string query = "select * from NhanVien";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            dataset.Tables["tblNhanVien"].Clear();
            adapter.Fill(dataset, "tblNhanVien");
            dgvNVSua_DanhSach.DataSource = dataset.Tables["tblNhanVien"];
            dgvNVXoa_DanhSach.DataSource = dataset.Tables["tblNhanVien"];
            dgvDSNVLamViec_DanhSach.DataSource = dataset.Tables["tblNhanVien"];

            MessageBox.Show("Bạn đã hủy sửa đổi thông tin nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void dgvNVXoa_DanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnNVXoa_Xoa.Enabled = true;
            btnNVXoa_huy.Enabled = false;

            viTri = e.RowIndex;
            if (viTri == -1) return;

        }

        private void btnNVXoa_Xoa_Click(object sender, EventArgs e)
        {
            if (viTri == -1)
            {
                MessageBox.Show("Bạn chưa chọn nhân viên cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (viTri >= dataset.Tables["tblNhanVien"].Rows.Count)
            {
                MessageBox.Show("Vị trí xóa không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (viTri < 0)
            {
                MessageBox.Show("Vị trí xóa không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có muốn xóa nhân viên ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {

                DataRow row = dataset.Tables["tblNhanVien"].Rows[viTri];
                row.Delete();

                btnNVXoa_Luu.Enabled = true;
                btnNVXoa_huy.Enabled = true;
            }


        }

        private void btnNVXoa_Luu_Click(object sender, EventArgs e)
        {

            ConnectData condata = new ConnectData();
            try
            {
                int kq = adapter.Update(dataset.Tables["tblNhanVien"]);
                if (kq > 0)
                    MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Lưu không thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnNVXoa_huy.Enabled = false;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Bản lương nhân viên còn tồn tại, hãy xóa bản lương của nhân viên trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                dataset.Tables["tblNhanVien"].Clear();
                adapter.Fill(dataset.Tables["tblNhanVien"]);
                dgvNVXoa_DanhSach.DataSource = dataset.Tables["tblNhanVien"];
            }

        }

        private void btnNVXoa_huy_Click(object sender, EventArgs e)
        {
            btnNVXoa_Luu.Enabled = false;
            btnNVXoa_Xoa.Enabled = false;

            string query = "select * from NhanVien";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            dataset.Tables["tblNhanVien"].Clear();
            adapter.Fill(dataset, "tblNhanVien");
            dgvNVXoa_DanhSach.DataSource = dataset.Tables["tblNhanVien"];
            dgvNVSua_DanhSach.DataSource = dataset.Tables["tblNhanVien"];
            dgvDSNVLamViec_DanhSach.DataSource = dataset.Tables["tblNhanVien"];
            MessageBox.Show("Bạn đã hủy xóa nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tabControl_NhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tabindex = tabControl_NhanVien.SelectedIndex;
            switch (tabindex)
            {
                case 1:
                    txtNVSua_TimKiem.Focus();
                    break;
                case 2:
                    txtNVXoa_timKiem.Focus();
                    break;
                case 3:

                    dataset.Tables["tblNhanVienLamViec"].Clear();
                    adapter_NVLamViec.Fill(dataset, "tblNhanVienLamViec");
                    dgvDSNVLamViec_DanhSach.DataSource = dataset.Tables["tblNhanVienLamViec"];

                    break;
                case 4:

                    dataset.Tables["tblNhanVienNghiViec"].Clear();
                    adapter_NVNghiViec.Fill(dataset, "tblNhanVienNghiViec");
                    dgvNVNghiViec_DanhSach.DataSource = dataset.Tables["tblNhanVienNghiViec"];
                    break;
                case 5:
                    LoadDataControl.Load_DataComBoBox(cboInBangLuong_MaNV, " select nv.MaNV,nv.MaCV from NhanVien as nv", "MaNV", "MaNV");
                    dgvTinhLuong_DanhSach.DataSource = dataset_InBangLuong.Tables["tblInBangLuong_Them"];
                    break;
            }

        }

        private void btnNVSua_TimKiem_Click(object sender, EventArgs e)
        {

            if (HamChucNang.TiemKiem(dgvNVSua_DanhSach, txtNVSua_TimKiem.Text, "NhanVien", "MaNV"))
                return;
            MessageBox.Show($"Không tìm thấy {txtNVSua_TimKiem.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnNVXoa_TimKiem_Click(object sender, EventArgs e)
        {
            if (HamChucNang.TiemKiem(dgvNVXoa_DanhSach, txtNVXoa_timKiem.Text, "NhanVien", "MaNV"))
                return;
            MessageBox.Show($"Không tìm thấy {txtNVXoa_timKiem.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cboInBangLuong_MaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maNV = cboInBangLuong_MaNV.SelectedValue.ToString().Trim();
            LoadDataControl.Load_DataComBoBox(cboInBangLuong_ChucVu, "select c.TenCV from NhanVien as n,ChucVu as c where n.MaCV =c.MaCV and MaNV = '" + maNV + "'", "TenCV", "TenCV");
            string query_InBangLuong = $"select * from NhanVien as n, ChucVu as c where n.MaCV = c.MaCV and n.MaNV LIKE '{maNV}'";
            ConnectData connectData = new ConnectData();
            connectData.openConnect();
            DataRowCollection rowLuong = connectData.ReadData(query_InBangLuong).Tables[0].Rows;
            if (rowLuong.Count <= 0)
            {
                return;
            }
            txtInBanLuong_LuongChinh.Text = rowLuong[0]["LuongChinh"].ToString();
            txtInBanLuong_PhuCap.Text = rowLuong[0]["PhuCapChucVu"].ToString();
            connectData.closeConnect();

        }

        private void btnInBanLuong_TinhLuong_Click(object sender, EventArgs e)
        {
            int phuCapKhac;
            int thuong;
            int luongChinh = int.Parse(txtInBanLuong_LuongChinh.Text);
            int phuCap = int.Parse(txtInBanLuong_PhuCap.Text);
            int soNgayCong = int.Parse(txtInBanLuong_NgayCong.Text);
            int soNgayNghi = int.Parse(txtInBanLuong_NgayNghi.Text);

            if (txtInBanLuong_PhuCapKhac.Text == "" || txtInBanLuong_Thuong.Text == "")
            {
                phuCapKhac = 0;
                thuong = 0;
            }
            else
            {
                phuCapKhac = int.Parse(txtInBanLuong_PhuCapKhac.Text);
                thuong = int.Parse(txtInBanLuong_Thuong.Text);
            }

            if (26 - soNgayCong != soNgayNghi)
            {
                MessageBox.Show("Số ngày nghĩ không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                double luongCoBan = luongChinh + phuCap + phuCapKhac;
                double tongLuong = ((luongCoBan / 26) * soNgayCong) + thuong;
                txtInBanLuong_TongLuong.Text = tongLuong.ToString();
            }

        }

        private void btnInBanLuong_Them_Click(object sender, EventArgs e)
        {
            if (txtInBanLuong_PhuCapKhac.Text == "" || txtInBanLuong_Thuong.Text == "")
            {
                txtInBanLuong_PhuCapKhac.Text = 0 + "";
                txtInBanLuong_Thuong.Text = 0 + "";
            }

            DataRow row = dataset_InBangLuong.Tables["tblInBangLuong_Them"].NewRow();
            row["MaNV"] = cboInBangLuong_MaNV.SelectedValue.ToString().Trim();
            row["PhuCapKhac"] = int.Parse(txtInBanLuong_PhuCapKhac.Text.Trim());
            row["Thuong"] = int.Parse(txtInBanLuong_Thuong.Text.Trim());
            row["Thang"] = int.Parse(txtInBanLuong_Thang.Text.Trim());
            row["Nam"] = int.Parse(txtInBanLuong_Nam.Text.Trim());
            row["SoNgayCong"] = int.Parse(txtInBanLuong_NgayCong.Text.Trim());
            row["SoNgayNghi"] = int.Parse(txtInBanLuong_NgayNghi.Text.Trim());
            row["Luong"] = float.Parse(txtInBanLuong_TongLuong.Text.Trim());
            row["GhiChu"] = txtInBanLuong_GhiChu.Text;
            dataset_InBangLuong.Tables["tblInBangLuong_Them"].Rows.Add(row);
            adapter_InBangLuong.Update(dataset_InBangLuong.Tables["tblInBangLuong_Them"]);

            // xoa du lieu khi them xong
            txtInBanLuong_LuongChinh.Clear();
            txtInBanLuong_Thang.Clear();
            txtInBanLuong_Nam.Clear();
            txtInBanLuong_NgayNghi.Clear();
            txtInBanLuong_PhuCap.Clear();
            txtInBanLuong_PhuCapKhac.Clear();
            txtInBanLuong_NgayCong.Clear();
            txtInBanLuong_TongLuong.Clear();
            txtInBanLuong_GhiChu.Clear();
            txtInBanLuong_Thuong.Clear();
        }

        int viTri_InBangLuong = -1;
        private void dgvTinhLuong_DanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            viTri_InBangLuong = e.RowIndex;
            if (viTri_InBangLuong == -1)
                return;
            if (viTri_InBangLuong >= dataset_InBangLuong.Tables["tblInBangLuong_Them"].Rows.Count)
                return;

            DataRow row = dataset_InBangLuong.Tables["tblInBangLuong_Them"].Rows[viTri_InBangLuong];
            cboInBangLuong_MaNV.SelectedValue = row["MaNV"];
            txtInBanLuong_PhuCapKhac.Text = row["PhuCapKhac"].ToString();
            txtInBanLuong_Thuong.Text = row["Thuong"].ToString();
            txtInBanLuong_Thang.Text = row["Thang"].ToString();
            txtInBanLuong_Nam.Text = row["Nam"].ToString();
            txtInBanLuong_NgayCong.Text = row["SoNgayCong"].ToString();
            txtInBanLuong_NgayNghi.Text = row["SoNgayNghi"].ToString();
            txtInBanLuong_TongLuong.Text = row["Luong"].ToString();
            txtInBanLuong_GhiChu.Text = row["GhiChu"].ToString();

        }

        private void btnInBanLuong_Xoa_Click(object sender, EventArgs e)
        {
            if (viTri_InBangLuong == -1)
            {
                MessageBox.Show("Chưa chọn bảng lương cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (viTri_InBangLuong >= dataset_InBangLuong.Tables["tblInBangLuong_Them"].Rows.Count)
            {
                MessageBox.Show("Vị trí xóa không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (viTri_InBangLuong < 0)
            {
                MessageBox.Show("Vị trí xóa không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow row = dataset_InBangLuong.Tables["tblInBangLuong_Them"].Rows[viTri_InBangLuong];
            DialogResult result = MessageBox.Show("Bạn có muốn xóa bảng lương không ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                row.Delete();
                int kq = adapter_InBangLuong.Update(dataset_InBangLuong.Tables["tblInBangLuong_Them"]);
                if (kq > 0)
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnInBanLuong_Sua_Click(object sender, EventArgs e)
        {
            if (viTri_InBangLuong == -1)
            {
                MessageBox.Show("Chưa chọn bảng lương cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (viTri_InBangLuong >= dataset_InBangLuong.Tables["tblInBangLuong_Them"].Rows.Count)
            {
                MessageBox.Show("Vị trí sửa không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (viTri_InBangLuong < 0)
            {
                MessageBox.Show("Vị trí sửa không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtInBanLuong_PhuCapKhac.Text == "" || txtInBanLuong_Thuong.Text == "")
            {
                txtInBanLuong_PhuCapKhac.Text = 0 + "";
                txtInBanLuong_Thuong.Text = 0 + "";
            }
            DataRow row = dataset_InBangLuong.Tables["tblInBangLuong_Them"].Rows[viTri_InBangLuong];
            row.BeginEdit();
            row["MaNV"] = cboInBangLuong_MaNV.SelectedValue.ToString().Trim();
            row["PhuCapKhac"] = int.Parse(txtInBanLuong_PhuCapKhac.Text.Trim());
            row["Thuong"] = int.Parse(txtInBanLuong_Thuong.Text.Trim());
            row["Thang"] = int.Parse(txtInBanLuong_Thang.Text.Trim());
            row["Nam"] = int.Parse(txtInBanLuong_Nam.Text.Trim());
            row["SoNgayCong"] = int.Parse(txtInBanLuong_NgayCong.Text.Trim());
            row["SoNgayNghi"] = int.Parse(txtInBanLuong_NgayNghi.Text.Trim());
            row["Luong"] = float.Parse(txtInBanLuong_TongLuong.Text.Trim());
            row["GhiChu"] = txtInBanLuong_GhiChu.Text;
            row.EndEdit();


            int kq = adapter_InBangLuong.Update(dataset_InBangLuong.Tables["tblInBangLuong_Them"]);
            if (kq > 0)
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Sửa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnInBanLuong_In_Click(object sender, EventArgs e)
        {
            HamChucNang.Export(dgvTinhLuong_DanhSach, "F:\\Test_IN\\test_inHoaDon.xlsx");
        }

        private void txtNVSua_TimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtNVSua_TimKiem.Text.Length == 0)
                dgvNVSua_DanhSach.DataSource = dataset.Tables["tblNhanVien"];

        }

        private void txtNVXoa_timKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtNVXoa_timKiem.Text.Length == 0)
                dgvNVXoa_DanhSach.DataSource = dataset.Tables["tblNhanVien"];
        }

        private void txtDSNV_Tim_TextChanged(object sender, EventArgs e)
        {
            if (txtDSNV_Tim.Text.Length == 0)
                dgvDSNVLamViec_DanhSach.DataSource = dataset.Tables["tblNhanVienLamViec"];
        }

        private void btnDSNV_Tim_Click(object sender, EventArgs e)
        {
            if (HamChucNang.TiemKiem(dgvDSNVLamViec_DanhSach, txtDSNV_Tim.Text, "NhanVien", "MaNV", "TrangThaiLamViec", "1")) 
            return;
            MessageBox.Show($"Không tìm thấy {txtNVXoa_timKiem.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnNVThoiViec_timKiem_Click(object sender, EventArgs e)
        {
            if (HamChucNang.TiemKiem(dgvNVNghiViec_DanhSach, txtNVThoiViec_timKiem.Text, "NhanVien", "MaNV", "TrangThaiLamViec", "0")) 
            return;
            MessageBox.Show($"Không tìm thấy {txtNVXoa_timKiem.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtNVThoiViec_timKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtNVThoiViec_timKiem.Text.Length == 0)
                dgvNVNghiViec_DanhSach.DataSource = dataset.Tables["tblNhanVienNghiViec"];
        }

        private void btnInBanLuong_Tim_Click(object sender, EventArgs e)
        {
            if (HamChucNang.TiemKiem(dgvTinhLuong_DanhSach, txtTimKiemLuong.Text, "LuongNhanVien", "MaNV")) 
            return;
            MessageBox.Show($"Không tìm thấy {txtNVXoa_timKiem.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtTimKiemLuong_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiemLuong.Text.Length == 0)
                dgvTinhLuong_DanhSach.DataSource = dataset_InBangLuong.Tables[0];
        }
    }
}


