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
    public partial class FormChiTietSinhVien : Form
    {
        private SinhVien SinhVien;
        public FormChiTietSinhVien()
        {
            InitializeComponent();
        }
        public FormChiTietSinhVien(SinhVien sv)
        {
            InitializeComponent();
            SinhVien = sv;
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormChiTietSinhVien_Load(object sender, EventArgs e)
        {
            string []arrayHoKhau = SinhVien.HKTT.Split(',');
            txtHoVaTen.Text = $"{SinhVien.HoVaTenLot} {SinhVien.Ten}";
            txtNgaySinh.Text = SinhVien.NgaySinh.Split(" ")[0];
            txtEmail.Text = SinhVien.Email;
            txtNoiSinh.Text = SinhVien.NoiSinh;
            txtCCCD.Text = SinhVien.CCCD;
            txtNgayCap.Text = SinhVien.NgayCapCCCD.Split(" ")[0];
            txtNoiCap.Text = SinhVien.NoiCapCCCD;
            txtLop.Text = SinhVien.MaLop;
            txtSDT.Text = SinhVien.SDT;
            txtTinh.Text = arrayHoKhau[3];
            txtHuyen.Text = arrayHoKhau[2];
            txtPhuong.Text = arrayHoKhau[1];
            txtHoKhau.Text = arrayHoKhau[0];
            txtPhong.Text = SinhVien.MaPhong;
            txtGioiTinh.Text = SinhVien.GioiTinh;
            txtMSSV.Text = SinhVien.MSSV;
        }
    }
}
