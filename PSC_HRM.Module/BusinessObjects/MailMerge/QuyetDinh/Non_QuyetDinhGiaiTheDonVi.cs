using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhGiaiTheDonVi : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [System.ComponentModel.DisplayName("Quyết định thành lập đơn vị")]
        public string QuyetDinhThanhLapDonVi { get; set; }
        [System.ComponentModel.DisplayName("Thời hạn bàn giao")]
        public string ThoiHanBanGiao { get; set; }
    }
}
