using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum TroCapEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Đề nghị hưởng chế độ ốm đau")]
        OmDau = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đề nghị hưởng chế độ thai sản")]
        ThaiSan,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đề nghị hưởng trợ cấp nghỉ DS-PHSK sau ốm đau")]
        PHSKSauOmDau,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đề nghị hưởng trợ cấp nghỉ DS-PHSK sau thai sản")]
        PHSKSauThaiSan,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đề nghị hưởng trợ cấp nghỉ DS-PHSK sau TNLĐ")]
        PHSKSauTNLD
    }
}
