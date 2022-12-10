using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum LoaiBaoCaoEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("6 tháng")]
        SauThang = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("12 tháng")]
        MuoiHaiThang = 1,
    }
}
