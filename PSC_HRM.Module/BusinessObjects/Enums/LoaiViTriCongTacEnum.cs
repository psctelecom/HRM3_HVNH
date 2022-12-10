using System;

namespace PSC_HRM.Module
{
    public enum LoaiViTriCongTacEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Trưởng đoàn")]
        TruongDoan = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Phó đoàn")]
        PhoDoan = 2
    }
}
