using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum Trong_NgoaiNuocEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Trong nước")]
        TrongNuoc = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ngoài nước")]
        NgoaiNuoc = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Khác")]
        Khac = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Cả hai")]
        CaHai = 3
    };
}
