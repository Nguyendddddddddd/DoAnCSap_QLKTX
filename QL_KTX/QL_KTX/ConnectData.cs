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

    internal class ConnectData
    {
        string StringConnection = "Data Source=LAPTOP-OUMK55PL\\SQLEXPRESS;Initial Catalog=QLKTX;Integrated Security=True";
        public SqlConnection Connection = null;

        
        public void openConnect()
        {
            Connection = new SqlConnection(StringConnection);
            if(Connection.State != ConnectionState.Open) {
                Connection.Open();
            }
        }
        public void closeConnect()
        {
            if(Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
                Connection.Dispose();
            }
        }
        
        public DataSet ReadData(string sql) {
            SqlDataAdapter adapter = new SqlDataAdapter(sql,Connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            adapter.Dispose();
            return dataSet;
        } 
    }
}
