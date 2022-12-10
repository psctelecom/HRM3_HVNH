using System;

namespace PSC_HRM.Module
{
    public enum LoaiThongKe2Enum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Theo tháng")]
        TheoThang = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Theo quý")]
        TheoQuy = 1
    }
}
