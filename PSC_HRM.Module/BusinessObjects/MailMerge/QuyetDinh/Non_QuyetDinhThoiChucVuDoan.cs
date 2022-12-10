using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhThoiChucVuDoan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Chức vụ đoàn")]
        public string ChucVuDoan { get; set; }
        [System.ComponentModel.DisplayName("Ngày thôi hưởng HSPC chức vụ đoàn")]
        public string NgayThoiHuongHSPCChucVuDoan { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
    }
}
