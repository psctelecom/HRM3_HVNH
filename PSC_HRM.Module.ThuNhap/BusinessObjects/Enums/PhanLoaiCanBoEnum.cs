using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.ThuNhap
{
    public enum PhanLoaiCanBoEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Biên chế")]
        BienChe = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Cơ hữu")]
        CoHuu = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tất cả")]
        TatCa = 2
    }
}
