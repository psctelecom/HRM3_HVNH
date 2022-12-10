using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum BienDongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Tăng lao động")]
        TangLaoDong = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảm lao động")]
        GiamLaoDong = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Điều chỉnh mức đóng")]
        ThayDoiMucLuong = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Điều chỉnh chức danh")]
        ThayDoiChucDanh = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Điều chỉnh BHYT")]
        BHYT = 5,
        [DevExpress.ExpressApp.DC.XafDisplayName("Điều chỉnh BHTN")]
        BHTN = 6
    }
}
