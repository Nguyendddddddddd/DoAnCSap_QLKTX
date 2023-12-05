using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX
{
    internal class LoadDataControl
    {
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
    }
}
