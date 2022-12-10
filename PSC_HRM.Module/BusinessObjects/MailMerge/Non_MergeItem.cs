using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PSC_HRM.Module
{
    public class Non_MergeItem
    {
        [Browsable(false)]
        public string Oid { get; set; }
        [DisplayName("Danh xưng viết hoa")]
        public string DanhXungVietHoa { get; set; }
        [DisplayName("Danh xưng viết thường")]
        public string DanhXungVietThuong { get; set; }
    }
}
