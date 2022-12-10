using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhThoiHuongPhuCapTrachNhiemDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [DisplayName("HSPC Trách nhiệm mới")]
        public string HSPCTrachNhiemMoi { get; set; }
        [DisplayName("Lý do")]
        public string LyDo { get; set; } 
    }
}
