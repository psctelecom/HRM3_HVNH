using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum CoQuanRaQuyetDinhEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Trường ra quyết định")]
        TruongRaQuyetDinh = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Cơ quan khác ra quyết định")]
        CoQuanKhacRaQuyetDinh = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Trường cũ ra quyết định")]
        TruongCuRaQuyetDinh = 2
    }
}
