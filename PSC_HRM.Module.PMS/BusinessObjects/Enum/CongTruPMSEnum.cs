using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module.PMS.Enum
{
    public enum CongTruPMSEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Cộng")]
        Cong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Trừ")]
        Tru = 1
    }
}
