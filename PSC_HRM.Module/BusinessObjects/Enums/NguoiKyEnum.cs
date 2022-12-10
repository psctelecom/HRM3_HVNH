using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum NguoiKyEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Đang tại chức")]
        DangTaiChuc = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đang không tại chức")]
        DangKhongTaiChuc = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ngoài trường")]
        NgoaiTruong = 2
    }
}
