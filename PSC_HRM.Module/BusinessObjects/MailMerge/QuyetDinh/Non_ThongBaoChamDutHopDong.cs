using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ThongBaoChamDutHopDong : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Số hợp đồng")]
        public string SoHopDong { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }
        public string DanhXungVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng viết hoa")]
        public string DanhXungVietHoa { get; set; }
    }
}
