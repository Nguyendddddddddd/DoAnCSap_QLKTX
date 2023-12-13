using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_KTX
{
    public partial class FormChiTietPhong : Form
    {
        private string MaPhong;
        public FormChiTietPhong()
        {
            InitializeComponent();
        }
        public FormChiTietPhong(string MaPhong)
        {
            InitializeComponent();
            this.MaPhong = MaPhong;
        }
        private void LoadChiTietPhong()
        {
            ConnectData connectData = new ConnectData();
            DataRow RowPhong = connectData.ReadData($"select * from Phong where MaPhong ='{MaPhong}'").Tables[0].Rows[0];
            DataRow RowDay = connectData.ReadData($"select * from DayKTX where MaDay ='{RowPhong["MaDay"].ToString()}'").Tables[0].Rows[0];
            DataRow RowLoaiPhong = connectData.ReadData($"select * from LoaiPhong where MaLoai = '{RowPhong["MaLoai"].ToString()}'").Tables[0].Rows[0];
            txtTenPhong.Text = RowPhong["TenPhong"].ToString();
            txtMaPhong.Text = RowPhong["MaPhong"].ToString();
            txtTrangThai.Text = $"{RowPhong["SoNguoiHienTai"]}/{RowLoaiPhong["SoNguoiToiDa"]}";
            txtDay.Text = RowDay["TenDay"].ToString();
            txtLoaiPhong.Text = RowLoaiPhong["TenLoai"].ToString();
            dtGWPhong.DataSource = connectData.ReadData($"select * from SinhVien where MaPhong like '{MaPhong}'").Tables[0];
        }

        private void FormChiTietPhong_Load(object sender, EventArgs e)
        {
            LoadChiTietPhong();
        }
    }
}
