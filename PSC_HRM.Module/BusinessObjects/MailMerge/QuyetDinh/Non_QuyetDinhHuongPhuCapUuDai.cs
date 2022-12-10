using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhHuongPhuCapUuDai : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Năm")]
        public string Nam { get; set; }
        [System.ComponentModel.DisplayName("Mức 25")]
        public string Muc25 { get; set; }
        [System.ComponentModel.DisplayName("Mức 40")]
        public string Muc40 { get; set; }
        [System.ComponentModel.DisplayName("Mức 45")]
        public string Muc45 { get; set; }

        public Non_QuyetDinhHuongPhuCapUuDai()
        {
            Master = new List<Non_ChiTietHuongPhuCapUuDaiMaster>();
            Detail = new List<Non_ChiTietHuongPhuCapUuDaiDetail>();
        }
    }
}
