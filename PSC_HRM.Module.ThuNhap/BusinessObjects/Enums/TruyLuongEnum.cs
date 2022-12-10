using System;
using System.Collections.Generic;
using System.Text;

namespace PSC_HRM.Module.ThuNhap
{
    public enum TruyLuongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương")]
        Luong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Phụ cấp")]
        PhuCap = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thưởng")]
        Thuong = 2
    }
}
