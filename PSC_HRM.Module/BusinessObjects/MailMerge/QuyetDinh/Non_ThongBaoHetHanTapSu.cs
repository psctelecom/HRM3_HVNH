using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ThongBaoHetHanTapSu : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Tìm kiếm từ ngày")]
        public string TimKiemTuNgay { get; set; }
        [System.ComponentModel.DisplayName("Tìm kiếm đến ngày")]
        public string TimKiemDenNgay { get; set; }

        public Non_ThongBaoHetHanTapSu()
        {
            Master = new List<Non_ChiTietThongBaoHetHanTapSuMaster>();
            Detail = new List<Non_ChiTietThongBaoHetHanTapSuDetail>();
       
        }
    }
}
