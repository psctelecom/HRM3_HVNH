using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module.ThuNhap
{
    public enum KhauTruThueEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Theo Tháng")]
        TheoThang = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Theo Quý")]
        TheoQuy = 1
    }
}
