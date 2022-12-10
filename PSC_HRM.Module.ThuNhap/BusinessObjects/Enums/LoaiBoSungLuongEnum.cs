using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.ThuNhap
{
    public enum LoaiBoSungLuongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương kỳ 1")]
        LuongKy1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Phụ cấp ưu đãi")]
        PhuCapUuDai,
        [DevExpress.ExpressApp.DC.XafDisplayName("Phụ cấp trách nhiệm")]
        PhuCapTrachNhiem,
        [DevExpress.ExpressApp.DC.XafDisplayName("Phụ cấp thâm niên")]
        PhuCapThamNien,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương kỳ 2")]
        LuongKy2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Phụ cấp tiến sĩ")]
        PhuCapTienSi,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nâng lương kỳ 1")]
        NangLuongKy1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nâng lương kỳ 2")]
        NangLuongKy2,
    }
}
