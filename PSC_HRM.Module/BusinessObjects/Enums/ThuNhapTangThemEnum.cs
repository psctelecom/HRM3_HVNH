using System;
using System.Collections.Generic;

namespace PSC_HRM.Module
{
    public enum ThuNhapTangThemEnum : byte
    {
        [DevExpress.Xpo.DisplayName("Tháng trước")]
        ThangTruoc = 0,
        [DevExpress.Xpo.DisplayName("Tháng này")]
        ThangNay = 1,
        [DevExpress.Xpo.DisplayName("Tháng sau")]
        ThangSau = 2
    }
}
