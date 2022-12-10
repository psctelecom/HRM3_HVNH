using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhHuongPhuCapTrachNhiem : Non_QuyetDinh
    {
        public Non_QuyetDinhHuongPhuCapTrachNhiem()
        {
            Master = new List<Non_ChiTietQuyetDinhHuongPhuCapTrachNhiemMaster>(); 
            Detail = new List<Non_ChiTietQuyetDinhHuongPhuCapTrachNhiemDetail>();
        }
    }
}
