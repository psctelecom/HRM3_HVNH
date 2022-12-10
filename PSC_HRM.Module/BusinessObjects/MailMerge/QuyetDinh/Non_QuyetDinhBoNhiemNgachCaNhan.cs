using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhBoNhiemNgachCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Nhóm ngạch lương")]
        public string NhomNgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Mã ngạch lương")]
        public string MaNgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Bậc lương")]
        public string BacLuong { get; set; }
        [System.ComponentModel.DisplayName("Hệ số lương")]
        public string HeSoLuong { get; set; }
        [System.ComponentModel.DisplayName("Mốc nâng lương")]
        //---
        public string MocNangLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng lương")]
        public string NgayHuongLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngày bổ nhiệm ngạch")]
        public string NgayBoNhiemNgach { get; set; }
        //---
        [System.ComponentModel.DisplayName("Mốc nâng lương (date)")]
        public string MocNangLuongDate { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng lương (date)")]
        public string NgayHuongLuongDate { get; set; }
        [System.ComponentModel.DisplayName("Ngày bổ nhiệm ngạch (date)")]
        public string NgayBoNhiemNgachDate { get; set; }
        [System.ComponentModel.DisplayName("Ngày xác nhận")]
        public string NgayXacNhan { get; set; }
        [System.ComponentModel.DisplayName("Số quyết định tuyển dụng")]
        public string SoQDTD { get; set; }
        [System.ComponentModel.DisplayName("Số quyết định HDTS")]
        public string SoQDHDTS { get; set; }
        [System.ComponentModel.DisplayName("Ngày tuyển dụng")]
        public string NgayTungDung { get; set; }
        [System.ComponentModel.DisplayName("Ngày hướng dẫn tập sự")]
        public string NgayHuongDanTapSu { get; set; }
    }
}
