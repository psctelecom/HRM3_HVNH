using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhChiTienThuongTienSiDetail : Non_MergeItem
    {
        [DisplayName("Stt")]
        public string STT { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Trình độ")]
        public string TrinhDo { get; set; }
        [DisplayName("Nơi đào tạo")]
        public string NoiDaoTao { get; set; }
        [DisplayName("Chuyên ngành đào tạo")]
        public string ChuyenNganhDaoTao { get; set; }
        [DisplayName("Ngày cấp bằng")]
        public string NgayCapBang { get; set; }
        [DisplayName("Ngày trở lại công tác")]
        public string NgayTroLaiCongTac { get; set; }
        [DisplayName("Số tiền")]
        public string SoTien { get; set; }
        [DisplayName("Số tiền bằng chữ")]
        public string SoTienBangChu { get; set; }
        [DisplayName("Ghi chú")]
        public string GhiChu { get; set; }
    }
}
