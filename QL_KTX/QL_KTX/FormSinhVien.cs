using Guna.UI2.WinForms;
using System.Data;
using System.Data.SqlClient;
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
        private SqlDataAdapter DataAdapterHDTT = null;
        private SqlDataAdapter DataAdapterPhong = null;
        private DataSet dataSet = null;
        private ConnectData connectData = null;
        private int indexDtgwXoaSV = -1;
        private int indexDtgwSuaSV = -1;
        private int indexHD = -1;
        private int indexDSSV = -1;
        private SinhVien SinhVien;
        public FormSinhVien()
        {
            InitializeComponent();
            LoadDataControl.Load_DataComBoBox(cboSVThem_Khoa, "SELECT TenKhoa,MaKhoa From Khoa", "TenKhoa", "MaKhoa");
            LoadDataControl.Load_DataComBoBox(cboThemSV_Day, "SELECT TenDay,MaDay From DayKTX", "TenDay", "MaDay");
            LoadDataControl.Load_DataComBoBox(cboSVThem_LoaiPhong, "SELECT TenLoai,MaLoai From LoaiPhong", "TenLoai", "MaLoai");
            LoadDataControl.Load_DataComBoBox(cboSVSua_LoaiPhong, "SELECT TenLoai,MaLoai From LoaiPhong", "TenLoai", "MaLoai");
            LoadDataControl.Load_DataComBoBox(cboSVSua_Day, "SELECT TenDay,MaDay From DayKTX", "TenDay", "MaDay");
            LoadDataControl.Load_DataComBoBox(cboSVSua_Khoa, "SELECT TenKhoa,MaKhoa From Khoa", "TenKhoa", "MaKhoa");
            LoadDataControl.Load_DataComBoBox(cboHD_MaSV, "SELECT MSSV FROM SinhVien", "MSSV", "MSSV");

            cboSVThem_GioiTinh.SelectedItem = cboSVThem_GioiTinh.Items[0];

        }
        private void FormSinhVien_Load(object sender, EventArgs e)
        {
            connectData = new ConnectData();
            connectData.openConnect();
            DataAdapterSV = new SqlDataAdapter("select * from SinhVien", connectData.Connection);
            DataAdapterHDTT = new SqlDataAdapter("select * from HoaDonTienTro", connectData.Connection);
            DataAdapterPhong = new SqlDataAdapter("select * from Phong", connectData.Connection);
            dataSet = new DataSet();
            SqlCommandBuilder sqlCommandBuilderSV = new SqlCommandBuilder(DataAdapterSV);
            SqlCommandBuilder sqlCommandBuilderDHTT = new SqlCommandBuilder(DataAdapterHDTT);
            SqlCommandBuilder sqlCommandBuilderPhong = new SqlCommandBuilder(DataAdapterPhong);


            DataAdapterSV.Fill(dataSet, "SV");
            DataAdapterHDTT.Fill(dataSet, "HDTT");
            DataAdapterPhong.Fill(dataSet, "Phong");
            connectData.closeConnect();
            dtGWXoaSV.DataSource = dataSet.Tables[0];
            dtGWSuaSinhVien.DataSource = dataSet.Tables[0];
            dtGWHoaDonThuePhong.DataSource = dataSet.Tables[1];
        }
        private void cboSVSua_Day_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (connectData == null)
                return;
            cboSVSua_LoaiPhong.Enabled = true;
            cboSVSua_Phong.Enabled = false;
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
                if (txtSVThem_HoTenLot.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_HoTenLot, "Họ Và Tên Lót");
                if (txtSVThem_Ten.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_Ten, "Tên");
                if (txtSVThem_NoiSinh.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_NoiSinh, "Nơi Sinh");
                if (txtSVThem_SDT.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_SDT, "Số Điện Thoại");
                if (txtSVThem_Email.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_Email, "Email");
                if (txtSinhVien_CCCD.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSinhVien_CCCD, "CCCD");
                if (txtSVThem_NoiCap.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_NoiCap, "Nơi Cấp");
                if (txtSVThem_MSSV.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_MSSV, "MSSV");
                if (txtSVThem_Tinh.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_Tinh, "Tỉnh");
                if (txtSVThem_Huyen.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_Huyen, "Huyện");
                if (txtSVThem_Phuong.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_Phuong, "Phường");
                if (txtSVThem_HoKhau.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVThem_HoKhau, "Hộ Khẩu");


                if (!kiemtraTrungDuLieu(txtSVThem_MSSV, "MSSV", "SinhVien"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtSVThem_MSSV, "Mã số sinh viên đã tồn tại vui lòng nhập mã số sinh viên khác");
                if (!kiemtraTrungDuLieu(txtSVThem_SDT, "STD", "SinhVien"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtSVThem_SDT, "Số điện thoại đã tồn tại vui lòng nhập số điện thoại khác");
                if (!kiemtraTrungDuLieu(txtSVThem_Email, "Email", "SinhVien"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtSVThem_Email, "Email đã tồn tại vui lòng nhập email khác");
                if (!kiemtraTrungDuLieu(txtSinhVien_CCCD, "CCCD", "SinhVien"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtSinhVien_CCCD, "CCCD đã tồn tại vui lòng nhập CCCD khác");

                if (!IsEmail(txtSVThem_Email.Text))
                {
                    MessageBox.Show("Email Không Đúng định dạng vui lòng nhập lại");
                    return;
                }
                //Tao doi tuong De ket noi den csdl
                DataRow row = dataSet.Tables[0].Rows.Add();
                // Mo Ket Noi

                //Cac thuoc tinh cua sinh vien

                row[((int)SV.MSSV)] = txtSVThem_MSSV.Text;

                row[((int)SV.HoVaTenLot)] = txtSVThem_HoTenLot.Text;

                row[((int)SV.Ten)] = txtSVThem_Ten.Text;

                row[((int)SV.GioiTinh)] = cboSVThem_GioiTinh.SelectedItem.ToString();

                row[((int)SV.NgaySinh)] = dtpSVThem_NgaySinh.Value.ToString();
                row[((int)SV.CCCD)] = txtSinhVien_CCCD.Text;

                row[((int)SV.NoiSinh)] = txtSVThem_NoiSinh.Text;
                string HoKhau = txtSVThem_HoKhau.Text;
                string Phuong = txtSVThem_Phuong.Text;
                string Huyen = txtSVThem_Huyen.Text;
                string Tinh = txtSVThem_Tinh.Text;
                row[((int)SV.HKTT)] = $"{HoKhau}, {Phuong}, {Huyen}, {Tinh}";
                row[((int)SV.NgayCapCCCD)] = dtpSVThem_NgayCap.Value.ToString();
                row[((int)SV.NoiCapCCCD)] = txtSVThem_NoiCap.Text;
                row[((int)SV.MaLop)] = cboSVThem_Lop.SelectedValue.ToString();
                row[((int)SV.SDT)] = txtSVThem_SDT.Text;
                row[((int)SV.Email)] = txtSVThem_Email.Text;
                row[((int)SV.MaPhong)] = cboSVThem_Phong.SelectedValue.ToString();
                int kq = DataAdapterSV.Update(dataSet.Tables[0]);

                connectData.closeConnect();
                if (kq > 0)
                {
                    MessageBox.Show("Thêm sinh viên thành công");
                }
                else
                {
                    MessageBox.Show("Thêm sinh viên không thành công", "Thông báo");
                }
                DataTable table = dataSet.Tables["Phong"];
                DataRow Phong = (from DataRow r in table.Rows
                                 where r["MaPhong"].ToString() == cboSVThem_Phong.SelectedValue.ToString()
                                 select r).FirstOrDefault();
                Phong["SoNguoiHienTai"] = Convert.ToInt32(Phong["SoNguoiHienTai"]) + 1;
                DataAdapterPhong.Update(dataSet, "Phong");

                cboSVThem_LoaiPhong.Enabled = false;
                cboSVThem_Phong.Enabled = false;


                foreach (var control in panelThongTinSV.Controls)
                {
                    if (control is Guna2TextBox)
                    {
                        Guna2TextBox textBox = control as Guna2TextBox;
                        textBox.Clear();

                    }

                }
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
        private void btnSVXoa_Xoa_Click(object sender, EventArgs e)
        {
            if (indexDtgwXoaSV == -1)
            {
                MessageBox.Show("Bạn chưa chọn sinh viên cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable table = dataSet.Tables["Phong"];
            DataRow Phong = (from DataRow r in table.Rows
                             where r["MaPhong"].ToString() == dtGWXoaSV.SelectedRows[0].Cells["MaPhong"].Value.ToString()
                             select r).FirstOrDefault();
            Phong["SoNguoiHienTai"] = Convert.ToInt32(Phong["SoNguoiHienTai"]) - 1;
            DataGridViewRow dr = dtGWXoaSV.SelectedRows[0];
            dtGWXoaSV.Rows.Remove(dr);

        }

        private void dtGWXoaSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexDtgwXoaSV = e.RowIndex;
        }

        private void btnSVXoa_Luu_Click(object sender, EventArgs e)
        {
            try
            {
                int kq = DataAdapterSV.Update(dataSet.Tables[0]);
                int kq1 = DataAdapterPhong.Update(dataSet, "Phong");

                if (kq > 0)
                    MessageBox.Show("Lưu Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Lưu Không Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lưu Không Thành Công Vì Sinh Viên Tồn tại hóa đơn thuê phòng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnSVXoa_Huy_Click(object sender, EventArgs e)
        {
            if (indexDtgwXoaSV < 0)
            {
                MessageBox.Show("Bạn chưa chọn sinh viên để xóa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dataSet.Tables[0].Clear();
            DataAdapterSV.Fill(dataSet.Tables[0]);
            MessageBox.Show("Hủy thay đổi thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void btnSVSua_Sua_Click(object sender, EventArgs e)
        {
            try
            {
                if (indexDtgwSuaSV == -1)
                {
                    MessageBox.Show("Bạn chưa chọn sinh viên để sữa", "Thông báo");
                    return;
                }
                DataTable table = dataSet.Tables["Phong"];
                DataRow PhongX = (from DataRow r in table.Rows
                                  where r["MaPhong"].ToString().Trim() == dtGWSuaSinhVien.SelectedRows[0].Cells["MaPhong"].Value.ToString().Trim()
                                  select r).FirstOrDefault();
                if (txtSVSua_HoTenLot.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVSua_HoTenLot, "Họ Và Tên Lót");
                if (txtSVSua_Ten.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVSua_Ten, "Tên");
                if (txtSVSua_NoiSinh.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVSua_NoiSinh, "Nơi Sinh");
                if (txtSVSua_SDT.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVSua_SDT, "Số Điện Thoại");
                if (txtSVSua_Email.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVSua_Email, "Email");
                if (txtSVSua_CCCD.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVSua_CCCD, "CCCD");
                if (txtSVSua_NoiCap.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVSua_NoiCap, "Nơi Cấp");
                if (txtSVSua_MSSV.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVSua_MSSV, "MSSV");
                if (txtSVSua_Tinh.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVSua_Tinh, "Tỉnh");
                if (txtSVSua_Huyen.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVSua_Huyen, "Huyện");
                if (txtSVSua_Phuong.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVSua_Phuong, "Phường");
                if (txtSVSua_HoKhau.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtSVSua_HoKhau, "Hộ Khẩu");
                if (!IsEmail(txtSVSua_Email.Text))
                {
                    MessageBox.Show("Email Không Đúng định dạng vui lòng nhập lại");
                    return;
                }
                DataRow RowUpdate = dataSet.Tables[0].Rows[indexDtgwSuaSV];
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
                MessageBox.Show("Sửa sinh viên thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


                DataRow PhongT = (from DataRow r in table.Rows
                                  where r["MaPhong"].ToString().Trim() == RowUpdate[((int)SV.MaPhong)].ToString().Trim()
                                  select r).FirstOrDefault();



                PhongX["SoNguoiHienTai"] = Convert.ToInt32(PhongX["SoNguoiHienTai"]) - 1;
                PhongT["SoNguoiHienTai"] = Convert.ToInt32(PhongT["SoNguoiHienTai"]) + 1;

                cboSVSua_LoaiPhong.Enabled = false;
                cboSVSua_Phong.Enabled = false;
                foreach (var control in PanelSuaSV_TTSV.Controls)
                {
                    if (control is Guna2TextBox)
                    {
                        Guna2TextBox textBox = control as Guna2TextBox;
                        textBox.Clear();

                    }

                }
                foreach (var control in PanelSuaSV_DCHKTT.Controls)
                {
                    if (control is Guna2TextBox)
                    {
                        Guna2TextBox textBox = control as Guna2TextBox;
                        textBox.Clear();

                    }

                }
            }
            catch (ExceptionTrungDuLieuDuyNhat ex)
            {
                MessageBox.Show(ex.ThongBaoLoi);
            }
            catch (ExceptionKhongCoDuLieu ex)
            {
                MessageBox.Show($"Bạn chưa nhập dư {ex.thongBaoLoi} vui lòng nhập để tiếp tục", "Thông báo");

            }
        }

        private void dtGWSuaSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexDtgwSuaSV = e.RowIndex;
            if (e.RowIndex < 0)
                return;
            string hokhau = "";
            DataTable dataTable = dataSet.Tables[0];
            DataRow rowSeleted = dataTable.Rows[e.RowIndex];

            connectData.openConnect();
            hokhau = rowSeleted[((int)SV.HKTT)].ToString();
            string[] hoKhaus = hokhau.Split(',');

            string sqlTenDay = $"select TenDay from DayKTX as d,Phong as p where  d.MaDay = p.MaDay and p.MaPhong like '{rowSeleted[((int)SV.MaPhong)].ToString()}'";
            string sqlLoaiPhong = $@"select TenLoai from LoaiPhong as lp,Phong as p where  lp.MaLoai= p.MaLoai and p.MaPhong like '{rowSeleted[((int)SV.MaPhong)].ToString()}'";
            string sqlNganh = $"select TenNganh, n.MaNganh  From Nganh as n,Lop as l where n.MaNganh = l.MaNganh and l.MaLop like  '{rowSeleted[((int)SV.MaLop)]}'";
            DataRow rowDay = connectData.ReadData(sqlTenDay).Tables[0].Rows[0];
            DataRow rowLoaiPhong = connectData.ReadData(sqlLoaiPhong).Tables[0].Rows[0];
            DataRow rowNganh = connectData.ReadData(sqlNganh).Tables[0].Rows[0];
            string sqlKhoa = $"select TenKhoa From Nganh as n,Khoa as k where n.MaKhoa = k.MaKhoa and n.MaNganh like  '{rowNganh[1]}'";
            DataRow rowKhoa = connectData.ReadData(sqlKhoa).Tables[0].Rows[0];
            connectData.closeConnect();
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
            cboSVSua_Phong.SelectedValue = rowSeleted[((int)SV.MaPhong)].ToString();
            txtSVSua_Tinh.Text = hoKhaus[3];
            txtSVSua_Huyen.Text = hoKhaus[2];
            txtSVSua_Phuong.Text = hoKhaus[1];
            txtSVSua_HoKhau.Text = hoKhaus[0];
        }

        private void btnSVSua_Luu_Click(object sender, EventArgs e)
        {
            try
            {
                int kq = DataAdapterSV.Update(dataSet.Tables[0]);
                int kq2 = DataAdapterPhong.Update(dataSet, "Phong");

                if (kq > 0)
                    MessageBox.Show("Lưu Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Lưu Không Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không thể lưu vì trùng Mã Sinh Viên");
            }
        }

        private void tabControlChucNangSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tabindex = tabControlChucNangSinhVien.SelectedIndex;
            switch (tabindex)
            {
                case 1:
                    dtGWSuaSinhVien.DataSource = dataSet.Tables[0];
                    break;
                case 2:
                    dtGWXoaSV.DataSource = dataSet.Tables[0];
                    break;
                case 3:
                    dtgSV_XemDanhSach.DataSource = dataSet.Tables[0];
                    break;
            }
        }

        private void btnSVSua_Huy_Click(object sender, EventArgs e)
        {
            dataSet.Tables[0].Clear();
            DataAdapterSV.Fill(dataSet.Tables[0]);
            MessageBox.Show("Hủy thay đổi thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSVXemChiTiet_Click(object sender, EventArgs e)
        {
            if (indexDSSV < 0)
            {
                MessageBox.Show("Bạn chưa chọn sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FormChiTietSinhVien formChiTietSinhVien = new FormChiTietSinhVien(SinhVien);
            formChiTietSinhVien.ShowDialog();
        }
        private void cboThemSV_Day_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (connectData == null)
                return;
            cboSVThem_LoaiPhong.Enabled = true;
            cboSVThem_Phong.Enabled = false;

        }
        private void dtgSV_XemDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexDSSV = e.RowIndex;
            if (e.RowIndex < 0)
                return;
            DataRow selectedRow = dataSet.Tables[0].Rows[e.RowIndex];
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

        private void btnLapHD_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaHD.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtMaHD, "Mã hóa đơn");
                if (txt_SoThangToan.Text == "")
                    throw new ExceptionKhongCoDuLieu(txt_SoThangToan, "Số thanh toán");
                if (!kiemtraTrungDuLieu(txtMaHD, "MaHD", "HoaDonTienTro"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtMaHD, "Mã hóa đơn đã tồn tại");
                DataRow row = dataSet.Tables[1].Rows.Add();
                row["MaHD"] = txtMaHD.Text;
                row["NgayLap"] = dtpNgayLapHD.Value.ToString();
                row["MSSV"] = cboHD_MaSV.SelectedValue.ToString();
                row["ThanhTien"] = Convert.ToInt32(txtGiaPhong_HD.Text) * Convert.ToInt32(txt_SoThangToan.Text);
                row["SoThangThanhToan"] = Convert.ToInt32(txt_SoThangToan.Text);
                row["MaPhong"] = txtPhong_HD.Text;

                int kq = DataAdapterHDTT.Update(dataSet.Tables[1]);
                if (kq > 0)
                {
                    MessageBox.Show("Thêm hóa đơn thành công");
                }
                else
                {
                    MessageBox.Show("Thêm hóa đơn không thành công", "Thông báo");
                }
                foreach (var control in PanelHDTP.Controls)
                {
                    if (control is Guna2TextBox)
                    {
                        Guna2TextBox textBox = control as Guna2TextBox;
                        textBox.Clear();

                    }

                }
            }
            catch (ExceptionKhongCoDuLieu ex)
            {
                MessageBox.Show($"Bạn chưa nhập dư {ex.thongBaoLoi} vui lòng nhập để tiếp tục", "Thông báo");
                ex.txt.Focus();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Số tháng thanh toán phải là số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ExceptionTrungDuLieuDuyNhat ex)
            {
                MessageBox.Show(ex.ThongBaoLoi, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi", "Thông báo");
            }

        }

        private void cboHD_MaSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (connectData == null)
                return;
            DataTable table = connectData.ReadData($"select * from SinhVien as sv, Phong as p, LoaiPhong as L where MSSV = '{cboHD_MaSV.SelectedValue.ToString()}' and p.MaPhong = sv.MaPhong and p.MaLoai = L.MaLoai").Tables[0];
            if (table.Rows.Count <= 0)
                return;
            DataRow r = table.Rows[0];
            txtTenSV_HD.Text = r[((int)SV.HoVaTenLot)].ToString() + " " + r[((int)SV.Ten)].ToString();
            txtLop_HD.Text = r[((int)SV.MaLop)].ToString();
            txtPhong_HD.Text = r[((int)SV.MaPhong)].ToString();
            txtGiaPhong_HD.Text = r["GiaPhong"].ToString();
        }

        private void dtGWHoaDonThuePhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            indexHD = e.RowIndex;
            DataRow row = dataSet.Tables[1].Rows[indexHD];
            DataRow rowSelect = dataSet.Tables[1].Rows[indexHD];
            cboHD_MaSV.SelectedValue = rowSelect["MSSV"].ToString();
            txtMaHD.Text = rowSelect["MaHD"].ToString();
            txt_SoThangToan.Text = rowSelect["SoThangThanhToan"].ToString();
        }

        private void btn_SuaHD_Click(object sender, EventArgs e)
        {
            if (indexHD == -1)
            {
                MessageBox.Show("Bạn chưa chọn Hóa đơn cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataRow row = dataSet.Tables[1].Rows[indexHD];
            row["MaHD"] = txtMaHD.Text;
            row["NgayLap"] = dtpNgayLapHD.Value.ToString();
            row["MSSV"] = cboHD_MaSV.SelectedValue.ToString();
            row["ThanhTien"] = Convert.ToInt32(txtGiaPhong_HD.Text) * Convert.ToInt32(txt_SoThangToan.Text);
            row["SoThangThanhToan"] = Convert.ToInt32(txt_SoThangToan.Text);
            row["MaPhong"] = txtPhong_HD.Text;
            foreach (var control in PanelHDTP.Controls)
            {
                if (control is Guna2TextBox)
                {
                    Guna2TextBox textBox = control as Guna2TextBox;
                    textBox.Clear();

                }

            }
        }

        private void btnXoa_HD_Click(object sender, EventArgs e)
        {
            if (indexHD == -1)
            {
                MessageBox.Show("Bạn chưa chọn Hóa đơn cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow dr = dtGWHoaDonThuePhong.SelectedRows[0];
            dtGWHoaDonThuePhong.Rows.Remove(dr);
            MessageBox.Show("Xóa Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }



        private void btnHuyHD_Click(object sender, EventArgs e)
        {
            dataSet.Tables[1].Clear();

            DataAdapterHDTT.Fill(dataSet.Tables[1]);
            MessageBox.Show("Hủy Thay Đổi Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            int kq = DataAdapterHDTT.Update(dataSet.Tables[1]);
            if (kq > 0)
            {
                MessageBox.Show("Lưu Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lưu Không Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void cboSVThem_LoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (connectData == null)
                return;
            connectData.openConnect();
            cboSVThem_Phong.DataSource = connectData.ReadData($@"select * 
                                                                from Phong as p ,LoaiPhong as lp 
                                                                where p.SoNguoiHienTai < lp.SoNguoiToiDa
                                                                and p.MaLoai = lp.MaLoai
                                                                and p.MaDay = '{cboThemSV_Day.SelectedValue.ToString()}'
                                                                and lp.MaLoai = '{cboSVThem_LoaiPhong.SelectedValue.ToString()}'").Tables[0];
            cboSVThem_Phong.DisplayMember = "MaPhong";
            cboSVThem_Phong.ValueMember = "MaPhong";
            cboSVThem_Phong.Enabled = true;
        }
        private void cboSVSua_LoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (connectData == null)
                return;
            connectData.openConnect();
            cboSVSua_Phong.DataSource = connectData.ReadData($@"select p.MaPhong
                                                                from Phong as p,LoaiPhong as lp
                                                                where p.SoNguoiHienTai < lp.SoNguoiToiDa
                                                                and p.MaLoai = lp.MaLoai
                                                                and p.MaDay = '{cboSVSua_Day.SelectedValue.ToString()}'
                                                                and lp.MaLoai = '{cboSVSua_LoaiPhong.SelectedValue.ToString()}'").Tables[0];
            cboSVSua_Phong.DisplayMember = "MaPhong";
            cboSVSua_Phong.ValueMember = "MaPhong";
            cboSVSua_Phong.Enabled = true;
        }

        private void btnSVSua_TimKiem_Click(object sender, EventArgs e)
        {


            if (HamChucNang.TiemKiem(dtGWSuaSinhVien, txtSVSua_TimKiem.Text, "SinhVien", "MSSV"))
                return;
            MessageBox.Show($"Không Tìm Thấy {txtSVSua_TimKiem.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void txtSVSua_TimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtSVSua_TimKiem.Text.Length == 0)
                dtGWSuaSinhVien.DataSource = dataSet.Tables[0];
        }

        private void btnSVXoa_TimKiem_Click(object sender, EventArgs e)
        {

            if (HamChucNang.TiemKiem(dtGWXoaSV, txtSVXoa_timKiem.Text, "SinhVien", "MSSV"))
                return;
            MessageBox.Show($"Không Tìm Thấy {txtSVXoa_timKiem.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtSVXoa_timKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtSVXoa_timKiem.Text.Length == 0)
                dtGWSuaSinhVien.DataSource = dataSet.Tables[0];
        }

        private void btnSVDS_TimKiem_Click(object sender, EventArgs e)
        {
            if (HamChucNang.TiemKiem(dtgSV_XemDanhSach, txtSVDS_TimKiem.Text, "SinhVien", "MSSV"))
                return;
            MessageBox.Show($"Không Tìm Thấy {txtSVDS_TimKiem.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtSVDS_TimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtSVDS_TimKiem.Text.Length == 0)
                dtgSV_XemDanhSach.DataSource = dataSet.Tables[0];
        }

        private void btnTimKiem_HD_Click(object sender, EventArgs e)
        {
            if (HamChucNang.TiemKiem(dtGWHoaDonThuePhong, txtTimKiem_HD.Text, "HoaDonTienTro", "MaHD"))
                return;
            if (HamChucNang.TiemKiem(dtGWHoaDonThuePhong, txtTimKiem_HD.Text, "HoaDonTienTro", "MSSV"))
                return;
            MessageBox.Show($"Không Tìm Thấy {txtTimKiem_HD.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtTimKiem_HD_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem_HD.Text.Length == 0)
                dtGWHoaDonThuePhong.DataSource = dataSet.Tables[1];
        }
        private void btnInHD_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                HamChucNang.Export(dtGWHoaDonThuePhong, saveFileDialog1.FileName);
            }
        }
    }
}