using System;

namespace PSC_HRM.Module
{
    public enum QuyEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Quý I")]
        QuyI = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quý II")]
        QuyII = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quý III")]
        QuyIII = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quý IV")]
        QuyIV = 3
    }
}
