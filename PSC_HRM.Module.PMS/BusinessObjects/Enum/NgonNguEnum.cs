using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module.PMS.Enum
{
    public enum NgonNguEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Bình thường")]
        BinhThuong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chuyên ngữ")]
        ChuyenNgu = 1
    }
}
