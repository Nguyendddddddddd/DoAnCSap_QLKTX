using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore.Storage;
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
using Color = System.Drawing.Color;

namespace QL_KTX
{
    public partial class FormTrangChu : Form
    {
        string strcon = @"Data Source=Admin-PC\SQLEXPRESS;Initial Catalog=QLKTX;Integrated Security=True";
        SqlConnection sqlcon = null;

        public FormTrangChu()
        {
            InitializeComponent();
            timer1.Start();
        }



        private void FormTrangChu_Load_1(object sender, EventArgs e)
        {


            if (sqlcon == null)
            {
                sqlcon = new SqlConnection(strcon);
            }

            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }

            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.Connection = sqlcon;
            sqlcmd.CommandText = "SELECT count(MaPhong) FROM Phong";

            int slp = (int)sqlcmd.ExecuteScalar();
            label1.Text = slp + " Phòng";

            sqlcmd.CommandText = "Select count(MSSV) from SinhVien";
            int slsv = (int)sqlcmd.ExecuteScalar();
            label2.Text = slsv + " Sinh Viên";

            sqlcmd.CommandText = "Select count(MaNV) from NhanVien";
            int slnv = (int)sqlcmd.ExecuteScalar();
            label3.Text = slnv + " Nhân Viên";

            // guna2CustomGradientPanel1.BackColor = Color.FromArgb(100, 0, 0, 0);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToLongDateString();
            label5.Text = DateTime.Now.ToLongTimeString();


        }
    }
}