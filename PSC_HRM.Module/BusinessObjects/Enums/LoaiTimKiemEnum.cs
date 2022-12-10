using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum LoaiTimKiemEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Chỉ các cán bộ hiện tại")]
        ConLam = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lọc theo tình trạng")]
        Khac
    }
}
