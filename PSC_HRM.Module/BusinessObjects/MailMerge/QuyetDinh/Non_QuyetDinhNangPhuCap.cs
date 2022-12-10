using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhNangPhuCap : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Số tiền phụ cấp")]
        public string SoTienPhuCap { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng phụ cấp")]
        public string NgayHuongPhuCap { get; set; }
    }
}
