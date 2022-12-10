using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhDaoTaoDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [DisplayName("Tên viết tắt đơn vị")]
        public string TenVietTatDonVi { get; set; }
        [DisplayName("Mã đơn vị")]
        public string MaDonVi { get; set; }
        [DisplayName("Chuyên ngành đào tạo")]
        public string ChuyenNganhDaoTao { get; set; }
        [DisplayName("Nhiệm vụ")]
        public string NhiemVu { get; set; }
    }
}
