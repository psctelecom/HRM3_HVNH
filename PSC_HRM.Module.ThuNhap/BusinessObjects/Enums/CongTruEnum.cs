using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.ThuNhap
{
    public enum CongTruEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Cộng")]
        Cong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Trừ")]
        Tru = 1
    }
}
