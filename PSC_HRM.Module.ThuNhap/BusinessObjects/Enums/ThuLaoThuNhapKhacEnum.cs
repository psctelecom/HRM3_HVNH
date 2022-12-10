using System;
using System.Collections.Generic;
using System.Text;

namespace PSC_HRM.Module.ThuNhap
{
    public enum ThuLaoThuNhapKhacEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Thù lao")]
        ThuLao = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thu nhập khác")]
        ThuNhapKhac = 1
    }
}
