using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhTamHoanTapSu : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Quyết định hướng dẫn tập sự")]
        public string QuyetDinhHuongDanTapSu { get; set; }
        [System.ComponentModel.DisplayName("Mã ngạch")]
        public string MaNgach { get; set; }
        [System.ComponentModel.DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Hoản từ ngày")]
        public string HoanTuNgay { get; set; }
        [System.ComponentModel.DisplayName("Hoản đến ngày")]
        public string HoanDenNgay { get; set; }
        [System.ComponentModel.DisplayName("Tập sự từ ngày")]
        public string TapSuTuNgay { get; set; }
        [System.ComponentModel.DisplayName("Tập sự đến ngày")]
        public string TapSuDenNgay { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
    }
}
