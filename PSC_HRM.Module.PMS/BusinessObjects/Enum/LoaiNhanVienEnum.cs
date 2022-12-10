using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module.PMS.Enum
{
    public enum LoaiNhanVienEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("TatCa")]
        TatCa=0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảng viên")]
        GiangVien = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chuyên viên")]
        ChuyenViem = 2
    }
}
