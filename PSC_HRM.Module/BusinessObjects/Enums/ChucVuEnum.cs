using System;

namespace PSC_HRM.Module
{
    public enum ChucVuEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Chức vụ trong trường")]
        TrongTruong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chức vụ ngoài trường")]
        NgoaiTruong = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Cả hai")]
        CaHai = 2
    }
}
