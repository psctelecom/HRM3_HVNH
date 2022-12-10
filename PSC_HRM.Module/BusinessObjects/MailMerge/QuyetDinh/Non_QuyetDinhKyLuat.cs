using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhKyLuat : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Hình thức kỷ luật")]
        public string HinhThucKyLuat { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày (Date)")]
        public string TuNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày (Date)")]
        public string DenNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Trừ lương tăng thêm")]
        public string TruLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngày Ký")]
        public string NgayKy { get; set; }
        
    }
}
