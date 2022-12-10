using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhBoNhiemNgachDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
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
        public string MocNangLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng lương")]
        public string NgayHuongLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngày bổ nhiệm ngạch")]
        public string NgayBoNhiemNgach { get; set; }
    }
}
