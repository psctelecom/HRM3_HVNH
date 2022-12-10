using System;

namespace PSC_HRM.Module.MailMerge.HopDong
{
    public class Non_HopDongKhoan : Non_HopDongCaNhan
    {
        [System.ComponentModel.DisplayName("Tiền lương")]
        public string TienLuong { get; set; }
        [System.ComponentModel.DisplayName("Tiền ăn")]
        public string TienAn { get; set; }
        [System.ComponentModel.DisplayName("Tiền xăng")]
        public string TienXang { get; set; }
        [System.ComponentModel.DisplayName("Các khoản khác")]
        public string CacKhoanKhac { get; set; }
        [System.ComponentModel.DisplayName("Tổng tiền lương")]
        public string TongTienLuong { get; set; }
        [System.ComponentModel.DisplayName("Tiền lương bằng chữ")]
        public string TienLuongBangChu { get; set; }
        [System.ComponentModel.DisplayName("Hình thức thanh toán")]
        public string HinhThucThanhToan { get; set; }
        [System.ComponentModel.DisplayName("Tham gia bảo hiểm xã hội")]
        public string ThamGiaBHXH { get; set; }
        
        //DLU
        [System.ComponentModel.DisplayName("Hệ số khu vực")]
        public string HeSoKhuVuc { get; set; }
        [System.ComponentModel.DisplayName("Tiền trang phục BHLĐ")]
        public string TienTrangPhucBHLD { get; set; }
    }
}
