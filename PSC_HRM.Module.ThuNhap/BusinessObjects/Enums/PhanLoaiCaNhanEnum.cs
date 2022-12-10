using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.ThuNhap
{
    public enum PhanLoaiCaNhanEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Cá nhân cư trú có hợp đồng lao động")]
        CoHopDongLaoDong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Cá nhân cư trú không có hợp đồng lao động")]
        KhongCoHopDongLaoDong = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Cá nhân không cư trú")]
        KhongCuTru = 2
    }
}
