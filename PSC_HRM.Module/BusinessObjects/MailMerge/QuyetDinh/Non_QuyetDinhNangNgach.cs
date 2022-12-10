using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhNangNgach : Non_QuyetDinh
    {
        public Non_QuyetDinhNangNgach()
        {
            Master = new List<Non_ChiTietQuyetDinhNangNgachMaster>();
            Detail = new List<Non_ChiTietQuyetDinhNangNgachDetail>();
        }
    }
}
