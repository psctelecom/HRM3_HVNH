using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module.PMS.Enum
{
    public enum GioGiangDayEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Giờ hành chính")]
        GioHanhChinh = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ngoài giờ")]
        NgoaiGio = 1
    }
}
