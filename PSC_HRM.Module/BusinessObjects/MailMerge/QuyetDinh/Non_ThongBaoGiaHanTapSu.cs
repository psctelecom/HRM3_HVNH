using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ThongBaoGiaHanTapSu : Non_QuyetDinh
    {
        public Non_ThongBaoGiaHanTapSu()
        {
            Master = new List<Non_ChiTietThongBaoGiaHanTapSuMaster>();
            Detail = new List<Non_ChiTietThongBaoGiaHanTapSuDetail>();
        }
    }
}
