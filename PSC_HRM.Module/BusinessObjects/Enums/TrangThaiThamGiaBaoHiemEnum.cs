using System;

namespace PSC_HRM.Module
{
    public enum TrangThaiThamGiaBaoHiemEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Đang tham gia")]
        DangThamGia = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảm tạm thời")]
        GiamTamThoi = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảm hẳn")]
        GiamHan = 2
    }
}
