using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhNangNgachCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Nhóm ngạch lương cũ")]
        public string NhomNgachLuongCu { get; set; }
        [System.ComponentModel.DisplayName("Mã ngạch lương cũ")]
        public string MaNgachLuongCu { get; set; }
        [System.ComponentModel.DisplayName("Ngạch lương cũ")]
        public string NgachLuongCu { get; set; }
        [System.ComponentModel.DisplayName("Bậc lương cũ")]
        public string BacLuongCu { get; set; }
        [System.ComponentModel.DisplayName("Hệ số lương cũ")]
        public string HeSoLuongCu { get; set; }
        [System.ComponentModel.DisplayName("Mốc nâng lương cũ")]
        public string MocNangLuongCu { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng lương cũ")]
        public string NgayHuongLuongCu { get; set; }
        [System.ComponentModel.DisplayName("Ngày bổ nhiệm ngạch lương cũ")]
        public string NgayBoNhiemNgachCu { get; set; }
        [System.ComponentModel.DisplayName("Nhóm ngạch lương mới")]
        public string NhomNgachLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Mã ngạch lương mới")]
        public string MaNgachLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Ngạch lương mới")]
        public string NgachLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Bậc lương mới")]
        public string BacLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Hệ số lương mới")]
        public string HeSoLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Mốc nâng lương mới")]
        public string MocNangLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng lương mới")]
        public string NgayHuongLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Ngày bổ nhiệm ngạch mới")]
        public string NgayBoNhiemNgachMoi { get; set; }
    }
}
