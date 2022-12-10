using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum HinhThucNghiEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ trừ lương")]
        NghiTruLuong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ chế độ thai sản")]
        NghiBHXH = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ phép")]
        NghiPhep = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ đi học có lương")]
        NghiDiHocCoLuong = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ đi học không lương")]
        NghiDiHocKhongLuong = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ chế độ ốm đau")]
        NghiOm = 5,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ có hưởng lương")]
        NghiCoHuongLuong = 6
    }
}
