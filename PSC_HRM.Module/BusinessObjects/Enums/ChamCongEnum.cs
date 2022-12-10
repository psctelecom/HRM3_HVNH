using System;

namespace PSC_HRM.Module
{
    public enum ChamCongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Chấm bằng tay")]
        ChamBangTay = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chấm bằng máy")]
        ChamBangMay = 1
    }
}
