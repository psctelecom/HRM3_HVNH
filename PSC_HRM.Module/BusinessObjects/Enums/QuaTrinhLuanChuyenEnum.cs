using System;

namespace PSC_HRM.Module
{
    public enum QuaTrinhLuanChuyenEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Thuyên chuyển nội bộ")]
        ThuyenChuyenNoiBo = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tiếp nhận")]
        LuanChuyen = 1
    }
}
