using System;

namespace PSC_HRM.Module
{
    public enum LoaiCaChamCongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Ca gãy nghỉ giữa ca")]
        CaNguyen = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ca nguyên không nghĩ giữa ca")]
        CaGay = 1
    }
}
