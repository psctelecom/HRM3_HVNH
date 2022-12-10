using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhBoiDuongDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [DisplayName("Ghi chú")]
        public string GhiChu { get; set; }
        [DisplayName("Chức vụ (Mã quản lý)")]
        public string MaChucVu { get; set; }
        [DisplayName("Nhiệm vụ")]
        public string NhiemVu { get; set; }
        [DisplayName("Chức vụ - Bộ phận")]
        public string ChucVu_BoPhan { get; set; }
        [DisplayName("Mã ngạch")]
        public string MaNgach { get; set; }
        [DisplayName("Ngày sinh")]
        public string NgaySinh { get; set; }
    }
}
