using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhThoiHuongPhuCapTrachNhiemCaNhan : Non_QuyetDinhNhanVien
    {
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("HSPC Trách nhiệm cũ")]
        public string HSPCTrachNhiemCu { get; set; }
        [DisplayName("Ngày thôi hưởng HSPC Trách nhiệm cũ")]
        public string NgayThoiHuongHSPCTrachNhiemCu { get; set; }
        [DisplayName("Lý do")]
        public string LyDo { get; set; } 
        
    }
}
