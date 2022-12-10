using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhThoiChuc : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [System.ComponentModel.DisplayName("HSPC chức vụ")]
        public string HSPCChucVu { get; set; }
        [System.ComponentModel.DisplayName("Ngày thôi hưởng HSPC chức vụ")]
        public string NgayThoiHuongHSPCChucVu { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
    }
}
