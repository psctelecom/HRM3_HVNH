using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PSC_HRM.Module.ThuNhap.Import
{
    public class ExportItem
    {
        [DisplayName("Chọn")]
        public bool Chon { get; set; }
        [DisplayName("Mã quản lý")]
        public string SoHieuCongChuc { get; set; }
        [DisplayName("Họ")]
        public string Ho { get; set; }
        [DisplayName("Tên")]
        public string Ten { get; set; }
        [DisplayName("Đơn vị")]
        public string BoPhan { get; set; }
    }
}
