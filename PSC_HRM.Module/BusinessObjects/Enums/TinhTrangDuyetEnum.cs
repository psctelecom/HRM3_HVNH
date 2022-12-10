using System;

namespace PSC_HRM.Module
{
    public enum TinhTrangDuyetEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Chưa duyệt")]
        ChuaDuyet = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đã duyệt")]
        DaDuyet = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đã chốt")]
        DaChot = 2
        
    }
}
