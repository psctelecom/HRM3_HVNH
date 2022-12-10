using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhChuyenNgach : Non_QuyetDinhNhanVien
    {
        public Non_QuyetDinhChuyenNgach()
        {
            Master = new List<Non_ChiTietQuyetDinhChuyenNgachMaster>();
            Detail = new List<Non_ChiTietQuyetDinhChuyenNgachDetail>();
        }
    }
}
