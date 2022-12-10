using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PSC_HRM.Module.Win.QuyTrinh.Common
{
    public class NhanVienItem
    {
        [Browsable(false)]
        public Guid Oid { get; set; }
        [System.ComponentModel.DisplayName("Chọn")]
        public bool Chon { get; set; }
        [System.ComponentModel.DisplayName("Họ")]
        public string Ho { get; set; }
        [System.ComponentModel.DisplayName("Tên")]
        public string Ten { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị")]
        public string BoPhan { get; set; }
    }
}