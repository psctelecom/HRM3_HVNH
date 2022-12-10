using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhTamDungGiaiQuyetCheDo : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Ngày họp")]
        public string NgayHop { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
         [System.ComponentModel.DisplayName("Ngày Ký")]
        public string NgayKy { get; set; }
    }
}
