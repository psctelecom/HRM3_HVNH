using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module.ThuNhap
{
    public enum KyQuyetToanThueEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Quý")]
        Quy = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Năm")]
        Nam = 1
    }
}
