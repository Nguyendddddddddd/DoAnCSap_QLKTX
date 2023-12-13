using Guna.UI2.WinForms;
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

namespace QL_KTX
{
    public partial class DanhSachPhongDay : Form
    {
        ConnectData connectData = null;
        DataSet dataSet = null;
        SqlDataAdapter dataAdapter = null;
        private int indexDSPhong = -1;
        int indexDay = -1;
        private string maDay;
        public DanhSachPhongDay()
        {
            InitializeComponent();
        }
        public DanhSachPhongDay(string maDay)
        {
            this.maDay = maDay;
            InitializeComponent();
        }

        private void DanhSachPhongDay_Load(object sender, EventArgs e)
        {
            connectData = new ConnectData();
            connectData.openConnect();
            dataAdapter = new SqlDataAdapter("select * from Phong", connectData.Connection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(dataAdapter);
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            connectData.openConnect();
            LoadDanhSachDay();
        }
        private void LoadDanhSachDay()
        {
            DataRowCollection RowPhong = connectData.ReadData($"select * from Phong where MaDay = '{maDay}'").Tables[0].Rows;


            for (int i = 0; i < RowPhong.Count; i++)
            {
                flowLayoutDay.Controls.Add(new Guna2Panel()
                {
                    Size = new Size(200, 200),
                    BackColor = Color.Transparent,
                    FillColor = Color.Aqua,
                    Margin = new Padding(10),
                    BorderRadius = 20,
                    BorderThickness = 1,
                    BorderColor = Color.Aqua,
                });
            }
            int index = 0;
            foreach (var control in flowLayoutDay.Controls)
            {

                Guna2Panel panel = control as Guna2Panel;

                panel.Controls.Add(new Label()
                {
                    Text = RowPhong[index]["TenPhong"].ToString(),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    Size = new Size(200, 40),
                    TextAlign = ContentAlignment.TopCenter
                });

                panel.Controls.Add(new Guna2Panel()
                {
                    BackColor = Color.Aqua,
                    Size = new Size(100, 100),
                    Location = new Point(panel.Width / 2 - 50, panel.Height / 2 - 50),
                    BackgroundImage = new Bitmap(global::QL_KTX.Properties.Resources.bed),
                    BackgroundImageLayout = ImageLayout.Stretch,
                    Tag = RowPhong[index]["MaPhong"].ToString()
                });

                panel.Controls.Add(new Panel()
                {
                    BackColor = Color.Transparent,
                    Size = new Size(200, 200),
                    Dock = DockStyle.Fill,
                    Tag = RowPhong[index]["MaPhong"].ToString()
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
                        panel2.Click += loadchiTietPhong;
                    }

                }
            }
        }


        private void loadchiTietPhong(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;

            FormChiTietPhong formChiTietPhong = new FormChiTietPhong(panel.Tag.ToString());
            formChiTietPhong.ShowDialog();
        }
    }
}
