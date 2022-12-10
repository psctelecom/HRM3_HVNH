using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhNangThamNienTangThemCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Mã ngạch lương")]
        public string MaNgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Hệ số TNTT cũ")]
        public string HSLTangThemTheoThamNienCu { get; set; }
        [System.ComponentModel.DisplayName("Mốc hưởng TNTT cũ")]
        public string MocHuongThamNienTangThemCu { get; set; }
        [System.ComponentModel.DisplayName("Hệ số TNTT mới")]
        public string HSLTangThemTheoThamNienMoi { get; set; }
        [System.ComponentModel.DisplayName("Mốc hưởng TNTT mới")]
        public string MocHuongThamNienTangThemMoi { get; set; }
    }
}
