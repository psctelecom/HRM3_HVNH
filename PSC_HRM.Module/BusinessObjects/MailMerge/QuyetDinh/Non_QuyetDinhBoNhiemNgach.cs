using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhBoNhiemNgach : Non_QuyetDinhNhanVien
    {
        public Non_QuyetDinhBoNhiemNgach()
        {
            Master = new List<Non_ChiTietQuyetDinhBoNhiemNgachMaster>();
            Detail = new List<Non_ChiTietQuyetDinhBoNhiemNgachDetail>();
        }
    }
}
