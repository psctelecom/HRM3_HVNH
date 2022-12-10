using System;

namespace PSC_HRM.Module.MailMerge.HopDong
{
    public class Non_PhuLucHopDongKhoan : Non_HopDongCaNhan
    {
        [System.ComponentModel.DisplayName("Tiền ăn")]
        public string TienAn { get; set; }
        [System.ComponentModel.DisplayName("Tiền xăng")]
        public string TienXang { get; set; }
        [System.ComponentModel.DisplayName("Các khoản khác")]
        public string CacKhoanKhac { get; set; }
        [System.ComponentModel.DisplayName("Hợp đồng khoán")]
        public string HopDongKhoan { get; set; }
    }
}
