using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.ThuNhap
{
    public enum TrangThaiChiLuongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Tạm giữ lương")]
        TamGiuLuong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đã chi lại lương")]
        ChiLaiLuong = 1
    }
}
