using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module.ThuNhap
{
    public enum BangTongHopLuongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Hưởng 100% lương")]
        Huong100 = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hưởng 85% lương")]
        Huong85 = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tất cả")]
        TatCa = 2
    }
}
