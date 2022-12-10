using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhNangPhuCapThamNienNhaGiaoCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Mã ngạch lương")]
        public string MaNgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Thâm niên cũ")]
        public string ThamNienCu { get; set; }
        [System.ComponentModel.DisplayName("Thâm niên mới")]
        public string ThamNienMoi { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng thâm niên mới")]
        public string NgayHuongThamNienMoi { get; set; }
        [System.ComponentModel.DisplayName("Ngày Ký")]
        public string NgayKy { get; set; }
    }
}
