using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum Trong_NgoaiTruongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Trong trường đào tạo")]
        TrongTruong = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ngoài trường đào tạo")]
        NgoaiTruong = 0
    }
}
