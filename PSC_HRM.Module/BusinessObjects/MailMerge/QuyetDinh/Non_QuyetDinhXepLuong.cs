using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhXepLuong : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Nhóm ngạch lương")]
        public string NhomNgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Bậc lương")]
        public string BacLuong { get; set; }
        [System.ComponentModel.DisplayName("Hệ số lương")]
        public string HeSoLuong { get; set; }
        [System.ComponentModel.DisplayName("Vượt khung")]
        public string VuotKhung { get; set; }
        [System.ComponentModel.DisplayName("Mốc nâng lương")]
        public string MocNangLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng lương")]
        public string NgayHuongLuong { get; set; }
    }
}
