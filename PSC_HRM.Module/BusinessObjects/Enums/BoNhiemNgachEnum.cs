using System;

namespace PSC_HRM.Module
{
    public enum BoNhiemNgachEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Bổ nhiệm ngạch")]
        BoNhiemNgach = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nâng ngạch")]
        NangNgach = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chuyển ngạch")]
        ChuyenNgach = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Xếp lương")]
        XepLuong = 3
    }
}
