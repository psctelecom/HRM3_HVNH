using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhThoiHuongPhuCapTrachNhiem : Non_QuyetDinh
    {
        public Non_QuyetDinhThoiHuongPhuCapTrachNhiem()
        {
            Master = new List<Non_ChiTietQuyetDinhThoiHuongPhuCapTrachNhiemMaster>();
            Detail = new List<Non_ChiTietQuyetDinhThoiHuongPhuCapTrachNhiemDetail>(); 
        }
    }
}
