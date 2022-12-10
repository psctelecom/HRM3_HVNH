using System;

namespace PSC_HRM.Module
{
    public enum XepLoaiDanhGiaEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("A")]
        LoaiA = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("B")]
        LoaiB = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("C")]
        LoaiC = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("D")]
        LoaiD = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Không")]
        Khong = 4
    }
}
