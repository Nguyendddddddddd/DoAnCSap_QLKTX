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

namespace QL_KTX
{
    public partial class FormSinhVien : Form
    {
        public FormSinhVien()
        {
            InitializeComponent();
            Load_DataComBoBox(cboSVThem_Khoa, "SELECT TenKhoa,MaKhoa From Khoa", "TenKhoa", "MaKhoa");
            Load_DataComBoBox(cboSVThem_Lop, "SELECT MaLop From Lop", "MaLop", "MaLop");

        }
        private void FormSinhVien_Load(object sender, EventArgs e)
        {


        }

        public void Load_DataComBoBox(ComboBox cbo, string SQL, string DisplayMember, string ValueMember)
        {
            ConnectData connectData = new ConnectData();
            connectData.openConnect();
            DataSet dataSet = connectData.ReadData(SQL);
            cbo.DataSource = dataSet.Tables[0];
            cbo.DisplayMember = DisplayMember;
            cbo.ValueMember = ValueMember;
            connectData.closeConnect();
        }

        private void cboSVThem_Khoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView MaKhoa = (DataRowView)cboSVThem_Khoa.SelectedItem;
            Load_DataComBoBox(cboSVThem_Nganh, $"SELECT TenNganh,MaNganh From Nganh WHERE MaKhoa = '{MaKhoa["MaKhoa"].ToString()}'", "TenNganh", "MaNganh");
        }

        private void btnSinhVien_Them_Click(object sender, EventArgs e)
        {
           
        }

        private void ThemSinhVien()
        {
            ConnectData connectData = new ConnectData();
            connectData.openConnect();
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.InsertCommand = new SqlCommand("",connectData.Connection);



            connectData.closeConnect();
        }
//        insert into SinhVien(MSSV, HovaTenLot, Ten, GioiTinh, CCCD, NoiSinh, HKTT, NoiCapCCCD, STD, Email) values('DTH216054'
//,'Pham Duc','Nguyen','Nam',1234566,'Tan Chau','An Giang','An Giang',03214564,'123344')
    }
}
