using System;

namespace PSC_HRM.Module
{
    public enum DonViTrucThuocEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Trụ sở chính")]
        TruSoChinh = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Trụ sở trưc thuộc")]
        TruSoTrucThuoc = 1
    }
}
