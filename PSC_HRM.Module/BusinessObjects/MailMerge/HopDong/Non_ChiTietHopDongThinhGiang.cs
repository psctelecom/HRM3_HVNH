using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.HopDong
{
    public class Non_ChiTietHopDongThinhGiang : Non_MergeItem
    {
        [System.ComponentModel.DisplayName("Lớp")]
        public string Lop { get; set; }
        [System.ComponentModel.DisplayName("Số tiết")]
        public string SoTiet { get; set; }
        [System.ComponentModel.DisplayName("Số tiền một tiết")]
        public string SoTien1Tiet { get; set; }
        [System.ComponentModel.DisplayName("Thành tiền")]
        public string ThanhTien { get; set; }
        [System.ComponentModel.DisplayName("Thuế thu nhập cá nhân")]
        public string ThueTNCN { get; set; }
        [System.ComponentModel.DisplayName("Số tiền còn lại")]
        public string SoTienConLai { get; set; }
        [System.ComponentModel.DisplayName("Bộ môn")]
        public string BoMon { get; set; }
        [System.ComponentModel.DisplayName("Tại khoa")]
        public string TaiKhoa { get; set; }
        [System.ComponentModel.DisplayName("Môn học UTE")]
        public string MonHocUTE { get; set; }
        [System.ComponentModel.DisplayName("Vào lúc")]
        public string BanDemBanNgay { get; set; }
    }
}
