using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhChamDutHopDong : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Số hợp đồng")]
        public string SoHopDong { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }
        [System.ComponentModel.DisplayName("Ngày cấp CMND")]
        public string NgayCapCMND { get; set; }
        [System.ComponentModel.DisplayName("Nơi Cấp CMND")]
        public string NoiCapCMND { get; set; }
        [System.ComponentModel.DisplayName("Ngày Ký")]
        public string NgayKy { get; set; }
    }
}
