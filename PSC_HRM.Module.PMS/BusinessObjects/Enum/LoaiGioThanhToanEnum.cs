using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module.PMS.Enum
{
    public enum LoaiGioThanhToanEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Giờ A1")]
        GioA1 = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giờ A2")]
        GioA2 = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tất cả")]
        TatCa= 2
        
    }
}
