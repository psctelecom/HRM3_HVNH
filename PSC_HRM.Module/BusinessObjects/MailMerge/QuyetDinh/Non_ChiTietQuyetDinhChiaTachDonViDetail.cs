using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhChiaTachDonViDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Học vị")]
        public string HocVi { get; set; }
        [DisplayName("Ghi chú")]
        public string GhiChu { get; set; }
    }
}
