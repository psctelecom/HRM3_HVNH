using System;

namespace PSC_HRM.Module
{
    public enum GioiTinhBaoCaoEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Nam")]
        Nam = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nữ")]
        Nu = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tất cả")]
        TatCa = 2
    }
}
