using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhKhac : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }
        //[System.ComponentModel.DisplayName("Đợt")]
        //public string Dot { get; set; }

        [System.ComponentModel.DisplayName("Chức danh hội đồng")]
        public string ChucDanhHoiDong { get; set; }

        public Non_QuyetDinhKhac()
        {
            Master = new List<Non_ChiTietQuyetDinhKhacMaster>();
            Detail = new List<Non_ChiTietQuyetDinhKhacDetail>();
        }
    }
}
