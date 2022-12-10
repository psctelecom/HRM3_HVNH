using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.ThuNhap
{
    public enum MauThanhToanEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Giấy rút dự toán ngân sách")]
        GiayRutDuToanNganSach = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ủy nhiệm chi")]
        UyNhiemChi = 1
    }
}
