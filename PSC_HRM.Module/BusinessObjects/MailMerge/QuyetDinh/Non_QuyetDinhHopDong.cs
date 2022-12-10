using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhHopDong : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Mức lương")]
        public string MucLuong { get; set; }
        [System.ComponentModel.DisplayName("Thời gian thữ việc")]
        public string ThoiGianThuViec { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
    }
}
