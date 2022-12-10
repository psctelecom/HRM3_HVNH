using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum CaNhan_TruongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Cá nhân tự túc")]
        CaNhan = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Trường cử đi đào tạo")]
        Truong = 0
    }
}
