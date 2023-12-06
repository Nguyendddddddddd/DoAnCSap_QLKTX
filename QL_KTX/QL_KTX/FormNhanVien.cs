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
        string strcon = "Data Source=HOAINAMPC\\SQLSERVER;Initial Catalog=QLKTX;Integrated Security=True";
        SqlConnection sqlCon = null;
        public FormNhanVien()
        {
            InitializeComponent();
            cboNVThem_GioiTinh.SelectedItem = "Nam";
            LoadDataControl.Load_DataComBoBox(cboNVThem_ChucVu," select cv.MaCV, cv.TenCV, cv.PhuCapChucVu from ChucVu as cv", "TenCV","MaCV");
        }

        private void txtNVThem_Them_Click(object sender, EventArgs e)
        {
            themNhanVien();
        }

        public void themNhanVien()
        {
            
                if(sqlCon == null)
                {
                  sqlCon = new SqlConnection(strcon);
                }
                if(sqlCon.State == ConnectionState.Closed)
                {
                  sqlCon.Open();
                }

                string hoten = txtNVThem_HoTenLot.Text;
                string ten = txtNVThem_Ten.Text;
                string gioiTinh = cboNVThem_GioiTinh.SelectedItem.ToString();
                string ngaySinh = dtpNVThem_NgaySinh.Value.ToShortDateString();
                string noiSinh = txtNVThem_NoiSinh.Text;
                int sdt = int.Parse(txtNVThem_SDT.Text);
                string email = txtNVThem_Email.Text;
                string cccd = txtNVThem_CCCD.Text;
                string ngayCap = txtNVThem_NgayCap.Text;
                string noiCap = txtNVThem_NoiCap.Text;
                string maNhanVien = txtNVThem_MaNhanVien.Text;
                int luongChinh = int.Parse(txtNVThem_LuongChinh.Text);
                DataRowView macv = (DataRowView) cboNVThem_ChucVu.SelectedItem;
                string chucvu = macv["MaCV"].ToString();

                string thanhpho = txtNVThem_ThanhPho.Text;
                string quan = txtNVThem_Huyen.Text;
                string xa = txtNVThem_Phuong.Text;
                string soNha = txtNVThem_HoKhau.Text;

            // string hoKhau = $"{thanhpho} /{quan} /{xa} /{soNha}";
            //string duLieu = $"'{maNhanVien}',N'{hoten}', N'{ten}', N'{gioiTinh}', {ngaySinh} ,{cccd} ,{ngayCap}, {noiCap} ,{noiSinh} ,{hoKhau}, {sdt}, {email} ,{luongChinh} ,{1} ,{chucVu}";


            //  sqlCMD.Connection = con.Connection;
            //int kq = sqlCMD.ExecuteNonQuery();
            //con.insertData($"insert into NhanVien values ({duLieu})");
            //con.closeConnect();

            SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = "insert into NhanVien values('"+maNhanVien+"', N'"+hoten+"', N'"+ten+"', N'"+gioiTinh+"', N'"+ngaySinh+"', '"+cccd+"', N'"+ngayCap+"', N'"+noiCap+"', N'"+noiSinh+"', N'"+thanhpho+"', "+sdt+", '"+email+"', "+luongChinh+", "+1+", '"+ chucvu + "')";
            sqlCMD.Connection = sqlCon;
            int kq = sqlCMD.ExecuteNonQuery();

            if (kq>0)
            {
                MessageBox.Show("Thêm thành công");

            }
            else
            {
                MessageBox.Show("Thêm không thành công");
            }




        }
    }
}
