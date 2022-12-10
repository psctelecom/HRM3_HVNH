using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhCongNhanHetHanTapSu : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Số người công nhận")]
        public string SoNguoiCongNhan { get; set; } 

        public Non_QuyetDinhCongNhanHetHanTapSu()
        {
            Master = new List<Non_ChiTietQuyetDinhCongNhanHetHanTapSuMaster>();
            Detail = new List<Non_ChiTietQuyetDinhCongNhanHetHanTapSuDetail>();
        }
    }
}
