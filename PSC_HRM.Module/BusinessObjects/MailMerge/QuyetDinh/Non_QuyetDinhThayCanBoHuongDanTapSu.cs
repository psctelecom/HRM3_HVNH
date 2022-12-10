using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhThayCanBoHuongDanTapSu : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Năm học")]
        public string ChucDanhCanBoHuongDanCu { get; set; }
        [System.ComponentModel.DisplayName("Cán bộ hướng dẫn cũ")]
        public string CanBoHuongDanCu { get; set; }
        [System.ComponentModel.DisplayName("Chức danh cán bộ hướng dẫn mới")]
        public string ChucDanhCanBoHuongDanMoi { get; set; }
        [System.ComponentModel.DisplayName("Cán bộ hướng dẫn mới")]
        public string CanBoHuongDanMoi { get; set; }
        [System.ComponentModel.DisplayName("HSPC trách nhiệm")]
        public string HSPCTrachNhiem { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
    }
}
