using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;



namespace QL_KTX
{
    enum DN
    {
        MaHoaDon,
        MaPhong,
        ChiSoDienCu,
        ChiSoDienMoi,
        ChiSoNuocCu,
        ChiSoNuocMoi,
        NgayLap,
        TongTien,
        ThanhTienDien,
        ThanhTienNuoc,
        DonGiaDien,
        DonGiaNuoc
    }
    public partial class FormPhong : Form
    {
        ConnectData connectData = null;
        DataSet dataSet = null;
        DataSet dataSetDienNuoc = null;
        SqlDataAdapter dataAdapter = null;
        private int indexDSPhong = -1;
        int indexDay = -1;
        int indexDienNuoc = -1;
        SqlDataAdapter dataAdapterDienNuoc = null;
        public FormPhong()
        {
            InitializeComponent();
        }
        private void loadPhong()
        {

            dtGWCapNhatPhong.DataSource = dataSet.Tables[0];
        }

        private void loadDay()
        {
            cboDay.DataSource = connectData.ReadData("select TenDay ,MaDay from DayKTX").Tables[0];
            cboDay.DisplayMember = "TenDay";
            cboDay.ValueMember = "MaDay";
        }

        private void loadLoaiPhong()
        {
            cboLoai.DataSource = connectData.ReadData("select TenLoai,MaLoai from LoaiPhong").Tables[0];
            cboLoai.DisplayMember = "TenLoai";
            cboLoai.ValueMember = "MaLoai";
        }

        private void FormPhong_Load(object sender, EventArgs e)
        {
            connectData = new ConnectData();
            connectData.openConnect();
            dataAdapter = new SqlDataAdapter("select * from Phong", connectData.Connection);
            dataAdapterDienNuoc = new SqlDataAdapter("select * from DienNuoc", connectData.Connection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(dataAdapter);
            SqlCommandBuilder sqlCommandBuilderDienNuoc = new SqlCommandBuilder(dataAdapterDienNuoc);
            dataSet = new DataSet();
            dataSetDienNuoc = new DataSet();
            dataAdapter.Fill(dataSet);
            dataAdapterDienNuoc.Fill(dataSetDienNuoc);
            dtGWCapNhatPhong.DataSource = dataSet.Tables[0];
            dtGWDienNuoc.DataSource = dataSetDienNuoc.Tables[0];
            connectData.closeConnect();

            LoadDataControl.Load_DataComBoBox(cboPhong, "select * from Phong", "MaPhong", "MaPhong");
            loadDay();
            loadLoaiPhong();
            LoadDanhSachDay();

        }

        private void LoadDanhSachDay()
        {
            DataRowCollection RowDay = connectData.ReadData("select * from DayKTX").Tables[0].Rows;



            for (int i = 0; i < RowDay.Count; i++)
            {
                flowLayoutDay.Controls.Add(new Guna2Panel()
                {
                    Size = new Size(200, 200),
                    BackColor = Color.Transparent,
                    FillColor = Color.FromArgb(109, 185, 239),
                    Margin = new Padding(10),
                    BorderRadius = 20,
                    BorderThickness = 1,
                    BorderColor = Color.FromArgb(109, 185, 239),
                });
            }
            int index = 0;
            foreach (var control in flowLayoutDay.Controls)
            {

                Guna2Panel panel = control as Guna2Panel;

                panel.Controls.Add(new Label()
                {
                    Text = RowDay[index]["TenDay"].ToString(),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    Size = new Size(200, 40),
                    TextAlign = ContentAlignment.TopCenter
                });
                DataRowCollection RowPhong = connectData.ReadData($"select count(MaPhong) as SLPhong from Phong as p where  p.MaDay = '{RowDay[index]["MaDay"].ToString()}'").Tables[0].Rows;
                panel.Controls.Add(new Label()
                {
                    Text = $"{RowPhong[0][0]} Phòng",
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    Size = new Size(200, 40),
                    TextAlign = ContentAlignment.TopCenter,
                    Dock = DockStyle.Bottom
                });

                panel.Controls.Add(new Guna2Panel()
                {
                    BackColor = Color.FromArgb(109, 185, 239),
                    Size = new Size(100, 100),
                    Location = new Point(panel.Width / 2 - 50, panel.Height / 2 - 50),
                    BackgroundImage = new Bitmap(global::QL_KTX.Properties.Resources.bed),
                    BackgroundImageLayout = ImageLayout.Stretch,
                    Tag = RowDay[index]["MaDay"].ToString()
                });

                panel.Controls.Add(new Panel()
                {
                    BackColor = Color.Transparent,
                    Size = new Size(200, 200),
                    Dock = DockStyle.Fill,
                    Tag = RowDay[index]["MaDay"].ToString()
                });
                index++;
            }

            foreach (var control in flowLayoutDay.Controls)
            {
                Panel panel1 = control as Panel;

                foreach (var panel in panel1.Controls)
                {
                    if (panel is Panel)
                    {
                        Panel panel2 = panel as Panel;
                        panel2.Click += loadDanhSachPhong;
                    }

                }
            }
        }



        public void loadDanhSachPhong(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            DanhSachPhongDay danhSachPhongDay = new DanhSachPhongDay(panel.Tag.ToString());
            danhSachPhongDay.ShowDialog();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaPhong.Text == "")
                {
                    MessageBox.Show("Bạn Chưa Nhập Mã Phòng", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    return;
                }
                if (txtTenPhong.Text == "")
                {
                    MessageBox.Show("Bạn Chưa Nhập Tên Phòng", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    return;
                }
                connectData.openConnect();
                DataRow insertRow = dataSet.Tables[0].Rows.Add();
                insertRow["MaPhong"] = txtMaPhong.Text;
                insertRow["MaDay"] = cboDay.SelectedValue.ToString();
                insertRow["MaLoai"] = cboLoai.SelectedValue.ToString();
                insertRow["TenPhong"] = txtTenPhong.Text;
                insertRow["SoNguoiHienTai"] = 0;
                int kq = dataAdapter.Update(dataSet);
                connectData.closeConnect();
                if (kq > 0)
                {
                    MessageBox.Show("Thêm phòng thành công");
                    dtGWCapNhatPhong.DataSource = connectData.ReadData("select * from Phong").Tables[0];
                }
                else
                {
                    MessageBox.Show("Thêm phòng không thành công");
                }
                foreach (var control in PanelTTPhong.Controls)
                {
                    if (control is Guna2TextBox)
                    {
                        Guna2TextBox textBox = control as Guna2TextBox;
                        textBox.Clear();

                    }

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Phòng Đã Tồn Tại Hãy Đổi Mã Phòng Khác");
                return;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (indexDSPhong < 0)
            {
                MessageBox.Show("Bạn chưa chọn phòng cần chỉnh sửa");
                return;
            }
            string maPhong = dtGWCapNhatPhong.SelectedRows[0].Cells["MaPhong"].Value.ToString().Trim();
            int SoNguoiTrongPhong = (int)connectData.ReadData($"select SoNguoiHienTai from Phong where MaPhong = '{maPhong}'").Tables[0].Rows[0]["SoNguoiHienTai"];
            if (SoNguoiTrongPhong > 0)
            {
                MessageBox.Show($"Phòng {maPhong.Trim()} hiện Đang có Người ở nếu muốn sửa phòng hãy xóa các sinh viên ra khỏi phòng rồi tiếp tục sửa phòng");
                return;
            }
            DialogResult result = MessageBox.Show("Bạn muốn sủa thông tin phòng", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
                return;
            try
            {
                if (txtMaPhong.Text == "")
                {
                    MessageBox.Show("Bạn Chưa Nhập Mã Phòng", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    return;
                }
                if (txtTenPhong.Text == "")
                {
                    MessageBox.Show("Bạn Chưa Nhập Tên Phòng", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    return;
                }
                if (maPhong != txtMaPhong.Text.Trim())
                    if (!kiemtraTrungDuLieu(txtMaPhong, "MaPhong"))
                        throw new ExceptionTrungDuLieuDuyNhat(txtMaPhong, "Mã Phòng");
                DataRow updateRow = dataSet.Tables[0].Rows[indexDSPhong];
                updateRow["MaPhong"] = txtMaPhong.Text;
                updateRow["MaDay"] = cboDay.SelectedValue.ToString();
                updateRow["MaLoai"] = cboLoai.SelectedValue.ToString();
                updateRow["TenPhong"] = txtTenPhong.Text;
                updateRow["SoNguoiHienTai"] = 0;
                MessageBox.Show("Sửa Phòng Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;
                foreach (var control in PanelTTPhong.Controls)
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
                MessageBox.Show("Phòng Đã Tồn Tại Hãy Đổi Mã Phòng Khác");
            }

        }
        private void dtGWCapNhatPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexDSPhong = e.RowIndex;
            if (e.RowIndex < 0)
                return;
            DataGridViewCellCollection CellSelected = dtGWCapNhatPhong.SelectedRows[0].Cells;
            cboDay.SelectedValue = CellSelected["MaDay"].Value.ToString();
            cboLoai.SelectedValue = CellSelected["MaLoai"].Value.ToString();
            txtMaPhong.Text = CellSelected["MaPhong"].Value.ToString();
            txtTenPhong.Text = CellSelected["TenPhong"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;


        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (indexDSPhong < 0)
            {
                MessageBox.Show("Bạn chưa chọn phòng cần chỉnh sửa", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                return;
            }
            string maPhong = dtGWCapNhatPhong.SelectedRows[0].Cells["MaPhong"].Value.ToString();

            int SoNguoiTrongPhong = (int)connectData.ReadData($"select SoNguoiHienTai from Phong where MaPhong = '{maPhong}'").Tables[0].Rows[0]["SoNguoiHienTai"];
            if (SoNguoiTrongPhong > 0)
            {
                MessageBox.Show($"Phòng {maPhong.Trim()} hiện Đang có Người ở nếu muốn xóa phòng hãy xóa các sinh viên ra khỏi phòng rồi tiếp tục xóa phòng");
                return;
            }
            DialogResult result = MessageBox.Show("Bạn muốn xóa phòng", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
                return;
            DataGridViewRow dr = dtGWCapNhatPhong.SelectedRows[0];
            dtGWCapNhatPhong.Rows.Remove(dr);
            MessageBox.Show("Xóa Phòng Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Bạn muốn lưu thay đổi không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    int kq = dataAdapter.Update(dataSet);
                    if (kq > 0)
                        MessageBox.Show("Lưu Thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    else
                        MessageBox.Show("Lưu Không Thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn hủy thay đổi không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                dataSet.Tables[0].Clear();
                dataAdapter.Fill(dataSet);
                MessageBox.Show("Hủy Thay Đổi Thành Công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            }
        }
        public bool kiemtraTrungDuLieu(Guna2TextBox txt, string strKT)
        {
            ConnectData connectData = new ConnectData();
            connectData.openConnect();
            DataSet dataSet = connectData.ReadData($"SELECT {strKT} FROM Phong");
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

        private void btnTinh_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn tính hóa đơn", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
                return;
            try
            {
                if (txtMaHD.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtMaHD, "Mã hóa đơn");
                float chiSoDienCu = float.Parse(txtChiSoDienCu.Text);
                float chiSoDienMoi = float.Parse(txtChiSoDienMoi.Text);
                float chiSoNuocCu = float.Parse(txtChiSoNuocCu.Text);
                float chiSoNuocMoi = float.Parse(txtChiSoNuocMoi.Text);
                float DonGiaDien = float.Parse(txtDonGiaDien.Text);
                float DonGiaNuoc = float.Parse(txtDonGiaNuoc.Text);


                float chiSoTieuThuDien = chiSoDienMoi - chiSoDienCu;
                float chiSoTieuThuNuoc = chiSoNuocMoi - chiSoNuocCu;
                float thanhTienDien = chiSoTieuThuDien * DonGiaDien;
                float thanhTienNuoc = chiSoTieuThuNuoc * DonGiaNuoc;
                if (chiSoDienMoi < chiSoDienCu)
                {
                    MessageBox.Show("Chỉ số điện mới phải lớn hơn chỉ số điện củ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (chiSoNuocMoi < chiSoNuocCu)
                {
                    MessageBox.Show("Chỉ số nước mới phải lớn hơn chỉ số nước củ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                txtThanhTienDien.Text = thanhTienDien.ToString();
                txtThanhTienNuoc.Text = thanhTienNuoc.ToString();
                txtTongTien.Text = (thanhTienDien + thanhTienNuoc).ToString();
                MessageBox.Show("Tính hóa đơn thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Mã hóa đơn đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (FormatException ex)
            {
                MessageBox.Show("Các chỉ số điện nước phải nhập bằng số hoặc bạn chưa nhập các chỉ số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ExceptionKhongCoDuLieu ex)
            {
                MessageBox.Show($"Bạn chưa nhập {ex.thongBaoLoi}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ex.txt.Focus();

            }
        }

        private void btnLap_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn lập hóa đơn", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
                return;
            try
            {
                if (txtMaHD.Text == "")
                    throw new ExceptionKhongCoDuLieu(txtMaHD, "Mã hóa đơn");
                float chiSoDienCu = float.Parse(txtChiSoDienCu.Text);
                float chiSoDienMoi = float.Parse(txtChiSoDienMoi.Text);
                float chiSoNuocCu = float.Parse(txtChiSoNuocCu.Text);
                float chiSoNuocMoi = float.Parse(txtChiSoNuocMoi.Text);
                float DonGiaDien = float.Parse(txtDonGiaDien.Text);
                float DonGiaNuoc = float.Parse(txtDonGiaNuoc.Text);
                float chiSoTieuThuDien = chiSoDienMoi - chiSoDienCu;
                float chiSoTieuThuNuoc = chiSoNuocMoi - chiSoNuocCu;
                float thanhTienDien = chiSoTieuThuDien * DonGiaDien;
                float thanhTienNuoc = chiSoTieuThuNuoc * DonGiaNuoc;
                DataRow rowDienNuoc = dataSetDienNuoc.Tables[0].Rows.Add();
                rowDienNuoc[((int)DN.MaHoaDon)] = txtMaHD.Text;
                rowDienNuoc[((int)DN.MaPhong)] = cboPhong.SelectedValue.ToString();
                rowDienNuoc[((int)DN.ChiSoDienCu)] = chiSoDienCu;
                rowDienNuoc[((int)DN.ChiSoDienMoi)] = chiSoDienMoi;
                rowDienNuoc[((int)DN.ChiSoNuocCu)] = chiSoNuocCu;
                rowDienNuoc[((int)DN.ChiSoNuocMoi)] = chiSoNuocMoi;
                rowDienNuoc[((int)DN.ThanhTienDien)] = thanhTienDien;
                rowDienNuoc[((int)DN.ThanhTienNuoc)] = thanhTienNuoc;
                rowDienNuoc[((int)DN.TongTien)] = thanhTienDien + thanhTienNuoc;
                rowDienNuoc[((int)DN.NgayLap)] = dtpNgayLap.Value.ToString();
                rowDienNuoc[((int)DN.DonGiaNuoc)] = DonGiaNuoc;
                rowDienNuoc[((int)DN.DonGiaDien)] = DonGiaDien;
                dataAdapterDienNuoc.Update(dataSetDienNuoc);
                MessageBox.Show("Lập hóa đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                foreach (var control in panelHHDienNuoc.Controls)
                {
                    if (control is Guna2TextBox)
                    {
                        Guna2TextBox textBox = control as Guna2TextBox;
                        textBox.Clear();

                    }

                }
            }
            catch (SqlException ex)
            {
                dataSetDienNuoc.Tables[0].Clear();
                dataAdapterDienNuoc.Fill(dataSetDienNuoc);
                MessageBox.Show("Mã hóa đơn đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (FormatException)
            {
                MessageBox.Show("Các chỉ số điện nước phải nhập bằng số hoặc bạn chưa nhập các chỉ số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (ExceptionKhongCoDuLieu ex)
            {
                MessageBox.Show($"Bạn chưa nhập {ex.thongBaoLoi}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ex.txt.Focus();

            }
        }

        private void dtGWDienNuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0)
                return;
            if (e.RowIndex >= dataSetDienNuoc.Tables[0].Rows.Count)
                return;
            indexDienNuoc = e.RowIndex;
            DataRow selectedRow = dataSetDienNuoc.Tables[0].Rows[e.RowIndex];

            txtChiSoDienCu.Text = selectedRow[((int)DN.ChiSoDienCu)].ToString();
            txtChiSoDienMoi.Text = selectedRow[((int)DN.ChiSoDienMoi)].ToString();
            txtChiSoNuocCu.Text = selectedRow[((int)DN.ChiSoNuocCu)].ToString();
            txtChiSoNuocMoi.Text = selectedRow[((int)DN.ChiSoNuocMoi)].ToString();
            txtDonGiaDien.Text = selectedRow[((int)DN.DonGiaDien)].ToString();
            txtDonGiaNuoc.Text = selectedRow[((int)DN.DonGiaNuoc)].ToString();
            txtMaHD.Text = selectedRow[((int)DN.MaHoaDon)].ToString();
            cboPhong.SelectedValue = selectedRow[((int)DN.MaPhong)].ToString();
            dtpNgayLap.Text = selectedRow[((int)DN.NgayLap)].ToString();
            txtTongTien.Text = selectedRow[((int)DN.TongTien)].ToString();
            txtThanhTienDien.Text = selectedRow[((int)DN.ThanhTienDien)].ToString();
            txtThanhTienNuoc.Text = selectedRow[((int)DN.ThanhTienNuoc)].ToString();
            btnSuaHoaDon.Enabled = true;
            btnXoaHoaDon.Enabled = true;
        }

        private void btnSuaHoaDon_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn sửa hóa đơn?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
                return;
            try
            {
                float chiSoDienCu = float.Parse(txtChiSoDienCu.Text);
                float chiSoDienMoi = float.Parse(txtChiSoDienMoi.Text);
                float chiSoNuocCu = float.Parse(txtChiSoNuocCu.Text);
                float chiSoNuocMoi = float.Parse(txtChiSoNuocMoi.Text);
                float DonGiaDien = float.Parse(txtDonGiaDien.Text);
                float DonGiaNuoc = float.Parse(txtDonGiaNuoc.Text);
                float chiSoTieuThuDien = chiSoDienMoi - chiSoDienCu;
                float chiSoTieuThuNuoc = chiSoNuocMoi - chiSoNuocCu;
                float thanhTienDien = chiSoTieuThuDien * DonGiaDien;
                float thanhTienNuoc = chiSoTieuThuNuoc * DonGiaNuoc;
                DataRow rowDienNuoc = dataSetDienNuoc.Tables[0].Rows[indexDienNuoc];
                rowDienNuoc[((int)DN.MaHoaDon)] = txtMaHD.Text;
                rowDienNuoc[((int)DN.MaPhong)] = cboPhong.SelectedValue.ToString();
                rowDienNuoc[((int)DN.ChiSoDienCu)] = chiSoDienCu;
                rowDienNuoc[((int)DN.ChiSoDienMoi)] = chiSoDienMoi;
                rowDienNuoc[((int)DN.ChiSoNuocCu)] = chiSoNuocCu;
                rowDienNuoc[((int)DN.ChiSoNuocMoi)] = chiSoNuocMoi;
                rowDienNuoc[((int)DN.ThanhTienDien)] = thanhTienDien;
                rowDienNuoc[((int)DN.ThanhTienNuoc)] = thanhTienNuoc;
                rowDienNuoc[((int)DN.TongTien)] = thanhTienDien + thanhTienNuoc;
                rowDienNuoc[((int)DN.NgayLap)] = dtpNgayLap.Value.ToString();
                rowDienNuoc[((int)DN.DonGiaNuoc)] = DonGiaNuoc;
                rowDienNuoc[((int)DN.DonGiaDien)] = DonGiaDien;

                MessageBox.Show("Sửa hóa đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataAdapterDienNuoc.Update(dataSetDienNuoc);
                foreach (var control in panelHHDienNuoc.Controls)
                {
                    if (control is Guna2TextBox)
                    {
                        Guna2TextBox textBox = control as Guna2TextBox;
                        textBox.Clear();

                    }

                }
            }
            catch (SqlException ex)
            {
                dataSetDienNuoc.Tables[0].Clear();
                dataAdapterDienNuoc.Fill(dataSetDienNuoc);
                MessageBox.Show("Mã hóa đơn đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (FormatException)
            {
                MessageBox.Show("Các chỉ số điện nước phải nhập bằng số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnXoaHoaDon_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn xóa hóa đơn?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
                return;
            DataGridViewRow dr = dtGWDienNuoc.SelectedRows[0];
            dtGWDienNuoc.Rows.Remove(dr);
            dataAdapterDienNuoc.Update(dataSetDienNuoc);
            MessageBox.Show("Xóa hóa đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn in hóa đơn? ", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
                return;
            HamChucNang.Export(dtGWDienNuoc, "D:\\ExportedData.xlsx");
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            if (HamChucNang.TiemKiem(dtGWCapNhatPhong, txtTimKiem.Text, "Phong", "MaPhong"))
                return;
            if (HamChucNang.TiemKiem(dtGWCapNhatPhong, txtTimKiem.Text, "Phong", "TenPhong"))
                return;
            MessageBox.Show($"Không Tìm Thấy {txtTimKiem.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text.Length == 0)
                dtGWCapNhatPhong.DataSource = dataSet.Tables[0];

        }

        private void btnTiemKiemHD_Click(object sender, EventArgs e)
        {
            if (HamChucNang.TiemKiem(dtGWDienNuoc, txtTimKiemHD.Text, "DienNuoc", "MaHoaDon"))
                return;
            if (HamChucNang.TiemKiem(dtGWDienNuoc, txtTimKiemHD.Text, "DienNuoc", "MaPhong"))
                return;
            MessageBox.Show($"Không Tìm Thấy {txtTimKiemHD.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtTimKiemHD_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiemHD.Text.Length == 0)
                dtGWDienNuoc.DataSource = dataSetDienNuoc.Tables[0];
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
/*            MessageBox.Show(tabControl1.SelectedIndex + "");
            if (tabControl1.SelectedIndex == 0)
            {
                dataSet.Tables[0].Clear();
                dataAdapter.Fill(dataSet);
                dtGWCapNhatPhong.DataSource = dataSet.Tables[0];
            }*/
        }

        private void FormPhong_Paint(object sender, PaintEventArgs e)
        {
           
            if (tabControl1.SelectedIndex == 0)
            {
                dataSet.Tables[0].Clear();
                dataAdapter.Fill(dataSet);
                dtGWCapNhatPhong.DataSource = dataSet.Tables[0];
            }
        }
    }
}