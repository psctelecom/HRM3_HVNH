using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhNghiKhongHuongLuong : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Ngày đề nghị xin nghỉ")]
        public string NgayDeNghi { get; set; }
        [System.ComponentModel.DisplayName("Số tháng nghỉ không hưởng lương")]
        public string SoThang { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
         [System.ComponentModel.DisplayName("Ngày Ký")]
        public string NgayKy { get; set; }
    }
}
