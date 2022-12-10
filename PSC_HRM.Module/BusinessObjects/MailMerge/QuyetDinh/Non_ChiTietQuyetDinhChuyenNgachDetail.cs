using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhChuyenNgachDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
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
        [System.ComponentModel.DisplayName("Ngày hưỡng lương cũ")]
        public string NgayHuongLuongCu { get; set; }
        [System.ComponentModel.DisplayName("Ngày bổ nhiệm ngạch cũ")]
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
