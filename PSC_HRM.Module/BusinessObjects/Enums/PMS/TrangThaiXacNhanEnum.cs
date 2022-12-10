using System;

namespace PSC_HRM.Module
{
    public enum TrangThaiXacNhanEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Chờ xét duyệt")]
        ChoXetDuyet = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Duyệt")]
        Duyet = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Không duyệt")]
        KhongDuyet = 2
    }
}
