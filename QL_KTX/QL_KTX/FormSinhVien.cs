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

namespace QL_KTX
{
    public partial class FormSinhVien : Form
    {
        public FormSinhVien()
        {
            InitializeComponent();
            LoadDataControl.Load_DataComBoBox(cboSVThem_Khoa, "SELECT TenKhoa,MaKhoa From Khoa", "TenKhoa", "MaKhoa");
            LoadDataControl.Load_DataComBoBox(cboThemSV_Day, "SELECT TenDay,MaDay From DayKTX", "TenDay", "MaDay");
            LoadDataControl.Load_DataComBoBox(cboSVThem_LoaiPhong, "SELECT TenLoai,MaLoai From LoaiPhong", "TenLoai", "MaLoai");
            LoadDataControl.Load_DataComBoBox(cboSVThem_Phong, "SELECT TenPhong,MaPhong From Phong ", "TenPhong", "MaPhong");
        }
        private void FormSinhVien_Load(object sender, EventArgs e)
        {
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
        private void cboSVThem_Nganh_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView MaNganh = (DataRowView)cboSVThem_Nganh.SelectedItem;
            LoadDataControl.Load_DataComBoBox(cboSVThem_Lop, $"SELECT MaLop From Lop WHERE MaNganh = '{MaNganh["MaNganh"].ToString()}'", "MaLop", "MaLop");
        }
        private void ThemSinhVien()
        {
            
            try
            {
                
                //Tao doi tuong De ket noi den csdl
                ConnectData connectData = new ConnectData();
                // Mo Ket Noi
                connectData.openConnect();
                //Cac thuoc tinh cua sinh vien

                string MSSV = txtSVThem_MSSV.Text;
                string HovaTenLot = txtSVThem_HoTenLot.Text;
                string Ten = txtSVThem_Ten.Text;
                string GioiTinh = cboSVThem_GioiTinh.SelectedItem.ToString();
                string NgaySinh = dtpSVThem_NgaySinh.Value.ToString();
                string CCCD = txtSinhVien_CCCD.Text;
                string NoiSinh = txtSVThem_NoiSinh.Text;
                string HKTT = txtSVThem_HoKhau.Text;
                string NgayCap = dtpSVThem_NgayCap.Value.ToString();
                string NoiCap = txtSVThem_NoiCap.Text;
                DataRowView Lop = (DataRowView)cboSVThem_Lop.SelectedItem;
                string MaLop = Lop["MaLop"].ToString();

                string SDT = txtSVThem_SDT.Text;
                string Email = txtSVThem_Email.Text;
                DataRowView Phong = (DataRowView)cboSVThem_Phong.SelectedItem;
                string MaPhong = Phong["MaPhong"].ToString();


                if (!kiemtraTrungDuLieu(txtSVThem_MSSV, "MSSV"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtSVThem_MSSV,"Mã số sinh viên đã tồn tại vui lòng nhập mã số sinh viên khác");
                if (!kiemtraTrungDuLieu(txtSVThem_SDT, "STD"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtSVThem_SDT,"Số điện thoại đã tồn tại vui lòng nhập số điện thoại khác");
                if (!kiemtraTrungDuLieu(txtSVThem_Email, "Email"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtSVThem_Email,"Email đã tồn tại vui lòng nhập email khác");
                if (!kiemtraTrungDuLieu(txtSinhVien_CCCD, "CCCD"))
                    throw new ExceptionTrungDuLieuDuyNhat(txtSinhVien_CCCD,"CCCD đã tồn tại vui lòng nhập CCCD khác");

                // Cau lenh sql insert sinh vien
                string TruongDuLieu = "(MSSV,HovaTenLot,Ten,GioiTinh,NgaySinh,CCCD,NoiSinh,HKTT,ngayCapCCCD,NoiCapCCCD,MaLop,STD,Email,MaPhong)";
                string cacBien = $"('{MSSV}','{HovaTenLot}','{Ten}','{GioiTinh}','{NgaySinh}','{CCCD}','{NoiSinh}','{HKTT}','{NgayCap}','{NoiCap}','{MaLop}','{SDT}','{Email}','{MaPhong}')";
                string sql = $"INSERT INTO SinhVien values {cacBien} ";

                // Cau lenh sql insert sinh vien
                int kq = connectData.insertData(sql);
                if(kq >0)
                {
                    MessageBox.Show("Thêm sinh viên thành công");
                }
                else
                {
                    MessageBox.Show("Thêm sinh viên không thành công","Thông báo");

                }

                //Dong chuoi ket noi
                connectData.closeConnect();
            }
            catch(ExceptionTrungDuLieuDuyNhat ex)
            {
                MessageBox.Show(ex.ThongBaoLoi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi");
            }
        }


        public bool kiemtraTrungDuLieu(Guna2TextBox txt,string strKT)
        {
            ConnectData connectData = new ConnectData();
            connectData.openConnect();
            DataSet dataSet = connectData.ReadData($"SELECT {strKT} FROM SinhVien");
            DataTable dataTable = dataSet.Tables[0];
            connectData.closeConnect();
            foreach (DataRow r in dataTable.Rows) {
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
