using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum HopDongLaoDongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng lao động thử việc")]
        TapSuThuViec = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng lao động có thời hạn")]
        CoThoiHan = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng lao động không thời hạn")]
        KhongThoiHan = 2
    }
}
