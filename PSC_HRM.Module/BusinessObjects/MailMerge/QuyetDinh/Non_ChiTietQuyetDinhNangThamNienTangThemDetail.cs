using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhNangThamNienTangThemDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; }
        [DisplayName("Ngày sinh")]
        public string NgaySinh { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [System.ComponentModel.DisplayName("Hệ số TNTT cũ")]
        public string HSLTangThemTheoThamNienCu { get; set; }
        [System.ComponentModel.DisplayName("Mốc hưởng TNTT cũ")]
        public string MocHuongThamNienTangThemCu { get; set; }
        [System.ComponentModel.DisplayName("Hệ số TNTT mới")]
        public string HSLTangThemTheoThamNienMoi { get; set; }
        [System.ComponentModel.DisplayName("Mốc hưởng TNTT mới")]
        public string MocHuongThamNienTangThemMoi { get; set; }
        [DisplayName("Mã ngạch lương")]
        public string MaNgachLuong { get; set; }
        [DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }

        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
    }
}
