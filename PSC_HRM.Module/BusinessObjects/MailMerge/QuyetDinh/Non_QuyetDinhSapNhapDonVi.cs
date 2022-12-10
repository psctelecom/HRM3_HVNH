using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhSapNhapDonVi : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Đơn vị cũ 1")]
        public string DonViCu1 { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị cũ 2")]
        public string DonViCu2 { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị Mới")]
        public string DonViMoi { get; set; }
    }
}
