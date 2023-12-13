using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Guna.UI2.WinForms;
using System.Collections;
using System.Net.Mail;

namespace QL_KTX
{
    public enum SV
    {
        MSSV,
        HoVaTenLot,
        Ten,
        GioiTinh,
        NgaySinh,
        CCCD,
        NoiSinh,
        HKTT,
        NgayCapCCCD,
        NoiCapCCCD,
        MaLop,
        SDT,
        Email,
        MaPhong
    }
    public struct SinhVien
    {
        public string MSSV;
        public string HoVaTenLot;
        public string Ten;
        public string GioiTinh;
        public string NgaySinh;
        public string CCCD;
        public string NoiSinh;
        public string HKTT;
        public string NgayCapCCCD;
        public string NoiCapCCCD;
        public string MaLop;
        public string SDT;
        public string Email;
        public string MaPhong;

    }
    public partial class FormSinhVien : Form
    {

        private SqlDataAdapter DataAdapterSV = null;
        private DataSet DataSetSV = null;
        private ConnectData ConnectData = null;
        private int indexDtgwXoaSV = -1;
        private int indexDtgwSuaSV = -1;
        private SinhVien SinhVien;
        public FormSinhVien()
        {
            InitializeComponent();
            LoadDataControl.Load_DataComBoBox(cboSVThem_Khoa, "SELECT TenKhoa,MaKhoa From Khoa", "TenKhoa", "MaKhoa");
            LoadDataControl.Load_DataComBoBox(cboThemSV_Day, "SELECT TenDay,MaDay From DayKTX", "TenDay", "MaDay");
            LoadDataControl.Load_DataComBoBox(cboSVThem_LoaiPhong, "SELECT TenLoai,MaLoai From LoaiPhong", "TenLoai", "MaLoai");
            LoadDataControl.Load_DataComBoBox(cboSVThem_Phong, "SELECT TenPhong,MaPhong From Phong ", "TenPhong", "MaPhong");
            LoadDataControl.Load_DataComBoBox(cboSVSua_LoaiPhong, "SELECT TenLoai,MaLoai From LoaiPhong", "TenLoai", "MaLoai");
            LoadDataControl.Load_DataComBoBox(cboSVSua_Day, "SELECT TenDay,MaDay From DayKTX", "TenDay", "MaDay");
            LoadDataControl.Load_DataComBoBox(cboSVSua_Khoa, "SELECT TenKhoa,MaKhoa From Khoa", "TenKhoa", "MaKhoa");

            cboSVThem_GioiTinh.SelectedItem = cboSVThem_GioiTinh.Items[0];

        }
        private void FormSinhVien_Load(object sender, EventArgs e)
        {
            ConnectData = new ConnectData();
            ConnectData.openConnect();
            DataAdapterSV = new SqlDataAdapter("select * from SinhVien", ConnectData.Connection);
            DataSetSV = new DataSet();
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(DataAdapterSV);
            DataAdapterSV.Fill(DataSetSV);
            ConnectData.closeConnect();
            dtGWXoaSV.DataSource = DataSetSV.Tables[0];
            dtGWSuaSinhVien.DataSource = DataSetSV.Tables[0];
        }
        private void cboSVSua_Day_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView Day = (DataRowView)cboSVSua_Day.SelectedItem;
            LoadDataControl.Load_DataComBoBox(cboSVSua_Phong, $"SELECT MaPhong From Phong WHERE MaDay= '{Day["MaDay"].ToString()}'", "MaPhong", "MaPhong");
        }
        private void cboSVThem_Nganh_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView MaNganh = (DataRowView)cboSVThem_Nganh.SelectedItem;
            LoadDataControl.Load_DataComBoBox(cboSVThem_Lop, $"SELECT MaLop From Lop WHERE MaNganh = '{MaNganh["MaNganh"].ToString()}'", "MaLop", "MaLop");
        }

        private void cboSVSua_Khoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView MaKhoa = (DataRowView)cboSVSua_Khoa.SelectedItem;
            LoadDataControl.Load_DataComBoBox(cboSVSua_Nganh, $"SELECT TenNganh,MaNganh From Nganh WHERE MaKhoa = '{MaKhoa["MaKhoa"].ToString()}'", "TenNganh", "MaNganh");
        }
        private void cboSVSua_Nganh_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView MaNganh = (DataRowView)cboSVSua_Nganh.SelectedItem;
            LoadDataControl.Load_DataComBoBox(cboSVSua_Lop, $"SELECT MaLop From Lop WHERE MaNganh = '{MaNganh["MaNganh"].ToString()}'", "MaLop", "MaLop");
        }
        private void cboSVThem_Khoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView MaKhoa = (DataRowView)cboSVThem_Khoa.SelectedItem;
            LoadDataControl.Load_DataComBoBox(cboSVThem_Nganh, $"SELECT TenNganh,MaNganh From Nganh WHERE MaKhoa = '{MaKhoa["MaKhoa"].ToString()}'", "TenNganh", "MaNganh");
        }

        private void btnSinhVien_Them_Click(object sender, EventArgs e)
        {
            ThemSinhVien();
        }

        private void ThemSinhVien()
        {

            try
            {

                //Tao doi tuong De ket noi den csdl
                ConnectData connectData = new ConnectData();
                // Mo Ket Noi

                //Cac thuoc tinh cua sinh vien

                string MSSV = txtSVThem_MSSV.Text;

                string HovaTenLot = txtSVThem_HoTenLot.Text;

                string Ten = txtSVThem_Ten.Text;

                string GioiTinh = cboSVThem_GioiTinh.SelectedItem.ToString();

                string NgaySinh = dtpSVThem_NgaySinh.Value.ToString();
                string CCCD = txtSinhVien_CCCD.Text;

                string NoiSinh = txtSVThem_NoiSinh.Text;

                string HoKhau = txtSVThem_HoKhau.Text;
                string Phuong = txtSVThem_Phuong.Text;
                string Huyen = txtSVThem_Huyen.Text;
                string Tinh = txtSVThem_Tinh.Text;

                string HKTT = $"{HoKhau}, {Phuong}, {Huyen}, {Tinh}";
                string NgayCap = dtpSVThem_NgayCap.Value.ToString();
                string NoiCap = txtSVThem_NoiCap.Text;

                DataRowView Lop = (DataRowView)cboSVThem_Lop.SelectedItem;
                string MaLop = Lop["MaLop"].ToString();
                string SDT = txtSVThem_SDT.Text;

                string Email = txtSVThem_Email.Text;

                DataRowView Phong = (DataRowView)cboSVThem_Phong.SelectedItem;
                string MaPhong = Phong["MaPhong"].ToString().Trim();

                if (HovaTenLot == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_HoTenLot, "Họ Và Tên Lót");
                if (Ten == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_Ten, "Tên");
                if (NoiSinh == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_NoiSinh, "Nơi Sinh");
                if (SDT == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_SDT, "Số Điện Thoại");
                if (Email == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_Email, "Email");
                if (CCCD == "")
                    throw new ExceptionKhongCoDuLieu(txtSinhVien_CCCD, "CCCD");
                if (NoiCap == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_NoiCap, "Nơi Cấp");
                if (MSSV == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_MSSV, "MSSV");
                if (Tinh == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_Tinh, "Tỉnh");
                if (Huyen == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_Huyen, "Huyện");
                if (Phuong == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_Phuong, "Phường");
                if (HoKhau == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_HoKhau, "Hộ Khẩu");


                if (!kiemtraTrungDuLieu(txtSVThem_MSSV, "MSSV"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtSVThem_MSSV, "Mã số sinh viên đã tồn tại vui lòng nhập mã số sinh viên khác");
                if (!kiemtraTrungDuLieu(txtSVThem_SDT, "STD"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtSVThem_SDT, "Số điện thoại đã tồn tại vui lòng nhập số điện thoại khác");
                if (!kiemtraTrungDuLieu(txtSVThem_Email, "Email"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtSVThem_Email, "Email đã tồn tại vui lòng nhập email khác");
                if (!kiemtraTrungDuLieu(txtSinhVien_CCCD, "CCCD"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtSinhVien_CCCD, "CCCD đã tồn tại vui lòng nhập CCCD khác");

                if (!IsEmail(Email))
                {
                    MessageBox.Show("Email Không Đúng định dạng vui lòng nhập lại");
                    return;
                }
                // Cau lenh sql insert sinh vien
                string TruongDuLieu = "(MSSV,HovaTenLot,Ten,GioiTinh,NgaySinh,CCCD,NoiSinh,HKTT,ngayCapCCCD,NoiCapCCCD,MaLop,STD,Email,MaPhong)";
                string cacBien = $"('{MSSV}','{HovaTenLot}','{Ten}','{GioiTinh}','{NgaySinh}','{CCCD}','{NoiSinh}','{HKTT}','{NgayCap}','{NoiCap}','{MaLop}','{SDT}','{Email}','{MaPhong}')";
                string sql = $"INSERT INTO SinhVien values {cacBien} ";
                connectData.openConnect();
                // Cau lenh sql insert sinh vien
                int kq = connectData.insertData(sql);
                //Dong ket noi
                connectData.closeConnect();
                if (kq > 0)
                {
                    MessageBox.Show("Thêm sinh viên thành công");
                }
                else
                {
                    MessageBox.Show("Thêm sinh viên không thành công", "Thông báo");
                }

                ConnectData.openConnect();
                string updatePhong = @$"UPDATE Phong
                                       SET SoNguoiHienTai = SoNguoiHienTai +1
                                       WHERE MaPhong = '{MaPhong}'";
                connectData.insertData(updatePhong);
                
                ConnectData.closeConnect();


            }
            catch (ExceptionTrungDuLieuDuyNhat ex)
            {
                MessageBox.Show(ex.ThongBaoLoi);
            }
            catch (ExceptionKhongCoDuLieu ex)
            {
                MessageBox.Show($"Bạn chưa nhập dư {ex.thongBaoLoi} vui lòng nhập để tiếp tục", "Thông báo");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối đến cơ sở dư liệu", "Thông báo");
            }
        }


        public bool kiemtraTrungDuLieu(Guna2TextBox txt, string strKT)
        {
            ConnectData connectData = new ConnectData();
            connectData.openConnect();
            DataSet dataSet = connectData.ReadData($"SELECT {strKT} FROM SinhVien");
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
        private void btnSVXoa_Xoa_Click(object sender, EventArgs e)
        {
            if (indexDtgwXoaSV == -1)
            {
                MessageBox.Show("Bạn chưa chọn sinh viên cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ConnectData.openConnect();
            ConnectData.DeleteRow(DataSetSV, indexDtgwXoaSV);
            dtGWXoaSV.DataSource = DataSetSV.Tables[0];
            ConnectData.closeConnect();
        }

        private void dtGWXoaSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexDtgwXoaSV = e.RowIndex;
        }

        private void btnSVXoa_Luu_Click(object sender, EventArgs e)
        {
            ConnectData.openConnect();
            DataAdapterSV.Update(DataSetSV.Tables[0]);
            ConnectData.closeConnect();
        }

        private void btnSVXoa_Huy_Click(object sender, EventArgs e)
        {
            ConnectData.openConnect();
            DataTable table = ConnectData.ReadData("select * from SinhVien").Tables[0];
            dtGWXoaSV.DataSource = table;
            DataSetSV.Tables.Clear();
            DataAdapterSV.Fill(DataSetSV);
            ConnectData.closeConnect();
        }
        private void btnSVSua_Sua_Click(object sender, EventArgs e)
        {
            if (indexDtgwSuaSV == -1)
            {
                MessageBox.Show("Bạn Chưa Chọn Sinh Viên");
                return;
            }
            DataRow RowUpdate = DataSetSV.Tables[0].Rows[indexDtgwSuaSV];
            RowUpdate[((int)SV.MSSV)] = txtSVSua_MSSV.Text;
            RowUpdate[((int)SV.HoVaTenLot)] = txtSVSua_HoTenLot.Text;
            RowUpdate[((int)SV.Ten)] = txtSVSua_Ten.Text;
            RowUpdate[((int)SV.NgaySinh)] = dtpSVSua_NgaySinh.Text;
            RowUpdate[((int)SV.GioiTinh)] = cboSVSua_GioiTinh.SelectedItem.ToString();
            RowUpdate[((int)SV.Email)] = txtSVSua_Email.Text;
            RowUpdate[((int)SV.SDT)] = txtSVSua_SDT.Text;
            RowUpdate[((int)SV.CCCD)] = txtSVSua_CCCD.Text;
            RowUpdate[((int)SV.NoiSinh)] = txtSVSua_NoiSinh.Text;
            RowUpdate[((int)SV.NgayCapCCCD)] = dtpSVSua_NgayCap.Value.ToString();
            RowUpdate[((int)SV.NoiCapCCCD)] = txtSVSua_NoiCap.Text;
            RowUpdate[((int)SV.MaLop)] = cboSVSua_Lop.SelectedValue.ToString();
            RowUpdate[((int)SV.MaPhong)] = cboSVSua_Phong.SelectedValue.ToString();
            RowUpdate[((int)SV.HKTT)] = $"{txtSVSua_HoKhau.Text}, {txtSVSua_Phuong.Text}, {txtSVSua_Huyen.Text}, {txtSVSua_Tinh.Text}";
            dtGWSuaSinhVien.DataSource = DataSetSV.Tables[0];
        }

        private void dtGWSuaSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexDtgwSuaSV = e.RowIndex;
            if (e.RowIndex < 0)
                return;
            string hokhau = "";
            DataTable dataTable = (DataTable)dtGWSuaSinhVien.DataSource;
            DataRow rowSeleted = dataTable.Rows[e.RowIndex];

            ConnectData.openConnect();
            hokhau = rowSeleted[((int)SV.HKTT)].ToString();
            string[] hoKhaus = hokhau.Split(',');

            string sqlTenDay = $"select TenDay from DayKTX as d,Phong as p where  d.MaDay = p.MaDay and p.MaPhong like '{rowSeleted[((int)SV.MaPhong)].ToString()}'";
            string sqlLoaiPhong = $@"select TenLoai from LoaiPhong as lp,Phong as p where  lp.MaLoai= p.MaLoai and p.MaPhong like '{rowSeleted[((int)SV.MaPhong)].ToString()}'";
            string sqlNganh = $"select TenNganh, n.MaNganh  From Nganh as n,Lop as l where n.MaNganh = l.MaNganh and l.MaLop like  '{rowSeleted[((int)SV.MaLop)]}'";
            DataRow rowDay = ConnectData.ReadData(sqlTenDay).Tables[0].Rows[0];
            DataRow rowLoaiPhong = ConnectData.ReadData(sqlLoaiPhong).Tables[0].Rows[0];
            DataRow rowNganh = ConnectData.ReadData(sqlNganh).Tables[0].Rows[0];
            string sqlKhoa = $"select TenKhoa From Nganh as n,Khoa as k where n.MaKhoa = k.MaKhoa and n.MaNganh like  '{rowNganh[1]}'";
            DataRow rowKhoa = ConnectData.ReadData(sqlKhoa).Tables[0].Rows[0];
            ConnectData.closeConnect();
            txtSVSua_HoTenLot.Text = rowSeleted[(int)SV.HoVaTenLot].ToString();
            txtSVSua_Ten.Text = rowSeleted[(int)SV.Ten].ToString();
            cboSVSua_GioiTinh.SelectedIndex = cboSVSua_GioiTinh.FindStringExact(rowSeleted[(int)SV.GioiTinh].ToString().Trim());
            txtSVSua_NoiSinh.Text = rowSeleted[(int)SV.NoiSinh].ToString();
            dtpSVSua_NgaySinh.Text = rowSeleted[(int)SV.NgaySinh].ToString();
            dtpSVSua_NgayCap.Text = rowSeleted[(int)SV.NgayCapCCCD].ToString();
            txtSVSua_SDT.Text = rowSeleted[(int)SV.SDT].ToString();
            txtSVSua_Email.Text = rowSeleted[(int)SV.Email].ToString();
            txtSVSua_CCCD.Text = rowSeleted[(int)SV.CCCD].ToString();
            txtSVSua_NoiCap.Text = rowSeleted[(int)SV.NoiCapCCCD].ToString();
            txtSVSua_MSSV.Text = rowSeleted[(int)SV.MSSV].ToString();
            cboSVSua_Day.SelectedIndex = cboSVSua_Day.FindStringExact(rowDay[0].ToString());
            cboSVSua_LoaiPhong.SelectedIndex = cboSVSua_LoaiPhong.FindStringExact(rowLoaiPhong[0].ToString());
            cboSVSua_Khoa.SelectedIndex = cboSVSua_Khoa.FindStringExact(rowKhoa[0].ToString());
            cboSVSua_Lop.SelectedIndex = cboSVSua_Lop.FindStringExact(rowSeleted[((int)SV.MaLop)].ToString());
            cboSVSua_Nganh.SelectedIndex = cboSVSua_Nganh.FindStringExact(rowNganh[0].ToString());

            txtSVSua_Tinh.Text = hoKhaus[3];
            txtSVSua_Huyen.Text = hoKhaus[2];
            txtSVSua_Phuong.Text = hoKhaus[1];
            txtSVSua_HoKhau.Text = hoKhaus[0];
        }

        private void btnSVSua_Luu_Click(object sender, EventArgs e)
        {
            ConnectData.openConnect();
            int kq = DataAdapterSV.Update(DataSetSV);
            ConnectData.closeConnect();
            dtGWSuaSinhVien.DataSource = DataSetSV.Tables[0];
            if (kq > 0)
                MessageBox.Show("Lưu Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Lưu Không Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void tabControlChucNangSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tabindex = tabControlChucNangSinhVien.SelectedIndex;
            switch (tabindex)
            {
                case 1:
                    dtGWSuaSinhVien.DataSource = DataSetSV.Tables[0];
                    break;
                case 2:
                    dtGWXoaSV.DataSource = DataSetSV.Tables[0];
                    break;
                case 3:
                    dtgSV_XemDanhSach.DataSource = DataSetSV.Tables[0];
                    break;
            }
        }

        private void btnSVSua_Huy_Click(object sender, EventArgs e)
        {
            ConnectData.openConnect();
            DataTable table = ConnectData.ReadData("select * from SinhVien").Tables[0];
            dtGWSuaSinhVien.DataSource = table;
            DataSetSV.Tables.Clear();
            DataAdapterSV.Fill(DataSetSV);
            ConnectData.closeConnect();
        }

        private void btnSVXemChiTiet_Click(object sender, EventArgs e)
        {
            FormChiTietSinhVien formChiTietSinhVien = new FormChiTietSinhVien(SinhVien);
            formChiTietSinhVien.ShowDialog();
        }

        private void dtgSV_XemDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            DataRow selectedRow = DataSetSV.Tables[0].Rows[e.RowIndex];
            SinhVien.MSSV = selectedRow[((int)SV.MSSV)].ToString();
            SinhVien.HoVaTenLot = selectedRow[((int)SV.HoVaTenLot)].ToString();
            SinhVien.Ten = selectedRow[((int)SV.Ten)].ToString();
            SinhVien.GioiTinh = selectedRow[((int)SV.GioiTinh)].ToString();
            SinhVien.NgaySinh = selectedRow[((int)SV.NgaySinh)].ToString();
            SinhVien.NgayCapCCCD = selectedRow[((int)SV.NgayCapCCCD)].ToString();
            SinhVien.NoiCapCCCD = selectedRow[((int)SV.NoiCapCCCD)].ToString();
            SinhVien.SDT = selectedRow[((int)SV.SDT)].ToString();
            SinhVien.Email = selectedRow[((int)SV.Email)].ToString();
            SinhVien.HKTT = selectedRow[((int)SV.HKTT)].ToString();
            SinhVien.MaLop = selectedRow[((int)SV.MaLop)].ToString();
            SinhVien.MaPhong = selectedRow[((int)SV.MaPhong)].ToString();
            SinhVien.CCCD = selectedRow[((int)SV.CCCD)].ToString();
            SinhVien.NoiSinh = selectedRow[((int)SV.NoiSinh)].ToString();
        }

        private void cboThemSV_Day_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}