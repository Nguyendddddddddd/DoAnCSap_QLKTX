using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX
{
    public class ExceptionKhongCoDuLieu : Exception
    {
        public Guna2TextBox txt { get; set; }
        public string thongBaoLoi { get; set; }
        public ExceptionKhongCoDuLieu(Guna2TextBox txt, string thongBaoLoi)
        {
            this.txt = txt;
            this.thongBaoLoi = thongBaoLoi;
        }
    }
}
