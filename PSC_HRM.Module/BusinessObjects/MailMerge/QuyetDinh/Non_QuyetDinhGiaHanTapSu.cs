using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhGiaHanTapSu : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Thời gian gia hạn")]
        public string ThoiGianGiaHan { get; set; }
        [System.ComponentModel.DisplayName("Tập sự đến ngày")]
        public string TapSuDenNgay { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
    }
}
