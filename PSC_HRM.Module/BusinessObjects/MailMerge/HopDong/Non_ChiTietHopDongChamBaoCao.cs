using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.HopDong
{
    public class Non_ChiTietHopDongChamBaoCao : Non_MergeItem
    {
        [System.ComponentModel.DisplayName("Lớp")]
        public string Lop { get; set; }
        [System.ComponentModel.DisplayName("Sỉ số")]
        public string SiSo { get; set; }
        [System.ComponentModel.DisplayName("Số bài báo cáo")]
        public string SoBaiBaoCao { get; set; }
        [System.ComponentModel.DisplayName("Số tiền một bài báo cáo")]
        public string SoTien1BaiBaoCao { get; set; }
        [System.ComponentModel.DisplayName("Thành tiền")]
        public string ThanhTien { get; set; }
        [System.ComponentModel.DisplayName("Thuế thu nhập cá nhân")]
        public string ThueTNCN { get; set; }
        [System.ComponentModel.DisplayName("Số tiền còn lại")]
        public string SoTienConLai { get; set; }
    }
}
