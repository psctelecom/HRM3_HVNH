using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhKhenThuongTapTheDetail : Non_MergeItem
    {
        [System.ComponentModel.DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị")]
        public string DonVi { get; set; }
    }
}
