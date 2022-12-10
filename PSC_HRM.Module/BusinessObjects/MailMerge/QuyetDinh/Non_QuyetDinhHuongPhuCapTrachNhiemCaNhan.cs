using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhHuongPhuCapTrachNhiemCaNhan : Non_QuyetDinhNhanVien
    {
        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [DisplayName("HSPC Trách nhiệm mới")]
        public string HSPCTrachNhiemMoi { get; set; }
        [DisplayName("Ngày hưởng HSPC Trách nhiệm mới")]
        public string NgayHuongHSPCTrachNhiemMoi { get; set; }
        [DisplayName("Lý do")] 
        public string LyDo { get; set; }
        
    }
}
