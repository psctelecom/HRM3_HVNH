using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum LyDoNghiEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Thôi việc, nghỉ hưu")]
        ThoiViec = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ thai sản")]
        ThaiSan,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ ốm")]
        OmDau,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ không hưởng lương")]
        NghiKhongLuong,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thuyên chuyển công tác")]
        ThuyenChuyen
    }
}
