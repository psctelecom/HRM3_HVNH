using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum HinhThucNghiUTEEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ trừ lương")]
        NghiTruLuong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ BHXH")]
        NghiBHXH = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ hưởng lương")]
        NghiHuongLuong = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ đi học có lương")]
        NghiDiHocCoLuong = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ đi học không lương")]
        NghiDiHocKhongLuong = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ ốm hoặc con ốm")]
        NghiOm = 5
    }
}
