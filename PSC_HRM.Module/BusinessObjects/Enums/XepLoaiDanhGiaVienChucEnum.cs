using System;

namespace PSC_HRM.Module
{
    public enum XepLoaiDanhGiaVienChucEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Hoàn thành xuất sắc nhiệm vụ")]
        HoanThanhXuatSacNhiemVu = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hoàn thành tốt nhiệm vụ")]
        HoanThanhTotNhiemVu = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hoàn thành nhiệm vụ")]
        HoanThanhNhiemVu = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chưa hoàn thành nhiệm vụ")]
        ChuaHoanThanhNhiemVu = 4,
    }
}
