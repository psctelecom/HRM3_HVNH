using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.MailMerge.HopDong
{
    public class Non_HopDongCaNhan : Non_HopDong
    {
        [System.ComponentModel.DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày date")]
        public string TuNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày date")]
        public string DenNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Hệ số lương")]
        public string HeSoLuong { get; set; }
        [System.ComponentModel.DisplayName("Mã ngạch")]
        public string MaNgach { get; set; }
        [System.ComponentModel.DisplayName("Vượt khung")]
        public string VuotKhung { get; set; }
        
        [System.ComponentModel.DisplayName("Bộ phận")]
        public string BoPhan { get; set; }
    }
}
