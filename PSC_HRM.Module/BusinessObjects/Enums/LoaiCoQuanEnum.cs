using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum LoaiCoQuanEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Trường")]
        Truong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Trường cũ")]
        TruongCu = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Cơ quan khác")]
        CoQuanKhac = 2
    }
}
