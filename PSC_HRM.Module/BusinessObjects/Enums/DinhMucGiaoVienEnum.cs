using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum DinhMucGiaoVienEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Theo lớp")]
        TheoLop = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Theo số học sinh")]
        TheoSoHocSinh = 1
    }
}
