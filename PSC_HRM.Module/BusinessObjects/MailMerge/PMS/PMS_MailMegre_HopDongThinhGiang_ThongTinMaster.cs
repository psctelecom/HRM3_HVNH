using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class PMS_MailMegre_HopDongThinhGiang_ThongTinMaster : Non_MergeItem
    {
        [DisplayName("Mã nhân viên")]
        public string MaNhanVien { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
    }
}
