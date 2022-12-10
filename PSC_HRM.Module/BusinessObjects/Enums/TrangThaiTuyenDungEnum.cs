using System;

namespace PSC_HRM.Module
{
    public enum TrangThaiTuyenDungEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Đang lập kế hoạch")]
        DangLapKeHoach = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đang thực hiện")]
        DangThucHien,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đã hoàn thành")]
        DaHoanThanh,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tạm hoãn")]
        TamHoan,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hủy bỏ")]
        HuyBo
    }
}
