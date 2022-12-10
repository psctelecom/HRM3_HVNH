using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhDieuChinhThoiGianDiDaoTaoDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [DisplayName("Danh xưng viết thường")]
        public string DanhXungVietThuong { get; set; }
        [DisplayName("Danh xưng viết hoa")]
        public string DanhXungVietHoa { get; set; }
        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [DisplayName("Vị trí công tác")]
        public string ViTriCongTac { get; set; }
    }
}
