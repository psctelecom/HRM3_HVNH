using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum NguoiKyHopDongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Đang giữ chức")]
        DangLamViec = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Không còn giữ chức")]
        KhongConLamViec = 1
    }
}
