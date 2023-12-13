using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX
{

    public class ConnectData
    {
        private string StringConnection = "Data Source=LAPTOP-OUMK55PL\\SQLEXPRESS;Initial Catalog=QLKTX;Integrated Security=True";
        public SqlConnection Connection = null;
       
        
        public void openConnect()
        {
            Connection = new SqlConnection();
            Connection.ConnectionString = StringConnection;
            if (Connection.State != ConnectionState.Open) {
                Connection.Open();
            }
        }
        public void closeConnect()
        {
            if(Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
        
        public DataSet ReadData(string sql) {
            SqlDataAdapter adapter = new SqlDataAdapter(sql,Connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            adapter.Dispose();
            return dataSet;
        } 
        public int insertData(string sql)
        {
            openConnect();
            SqlCommand sqlCommand = new SqlCommand(sql,Connection);
            int kq = sqlCommand.ExecuteNonQuery();
            closeConnect();
            sqlCommand.Dispose();
            return kq;
        }

        public void DeleteRow(DataSet dataSet,int index)
        {
            DataTable dataTable = dataSet.Tables[0];
            DataRow rowDelete = dataTable.Rows[index];
            rowDelete.Delete();
        }

    }
}
