using System;
using System.Collections.Generic;

namespace PSC_HRM.Module
{
    public enum LoaiLuongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương kỳ 1")]
        LuongKy1 = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương kỳ 2")]
        LuongKy2 = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương tiến sĩ")]
        LuongTienSi = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương thử việc")]
        LuongThuViec = 3
    }
}
