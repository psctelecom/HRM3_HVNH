using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhChuyenCongTac : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Cơ quan mới")]
        public string CoQuanMoi { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Ngày Ký")]
        public string Ngayky { get; set; }
        [System.ComponentModel.DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }
    }
}
