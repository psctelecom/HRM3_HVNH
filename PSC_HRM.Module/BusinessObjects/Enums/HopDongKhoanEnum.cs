using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum HopDongKhoanEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Thử việc")]
        ThuViec = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chính thức")]
        ChinhThuc = 1
    }
}
