using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhDoiTenDonVi : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Đơn vị cũ")]
        public string DonViCu { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị mới")]
        public string DonViMoi { get; set; }
    }
}
