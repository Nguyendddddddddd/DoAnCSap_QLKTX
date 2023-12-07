using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_KTX
{
    internal class ExceptionTrungDuLieuDuyNhat : Exception
    {
        public Guna2TextBox txt { get; set; }
        public string ThongBaoLoi { get; set; }
        public ExceptionTrungDuLieuDuyNhat(Guna2TextBox txt,string ThongBaoLoi) {
            this.txt = txt;
            this.ThongBaoLoi = ThongBaoLoi;
        }
    }
}
