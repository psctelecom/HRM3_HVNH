using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum LoaiNhanVienEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Tất cả")]
        TatCa = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảng viên")]
        GiangVien = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chuyên viên")]
        ChuyenVien = 2
    }
}
