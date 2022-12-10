using System;

namespace PSC_HRM.Module
{
    public enum TenTaiKhoanEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Mã quản lý")]
        MaQuanLy = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Số hồ sơ")]
        SoHoSo,
        [DevExpress.ExpressApp.DC.XafDisplayName("Số hiệu công chức")]
        SoHieuCongChuc,
        [DevExpress.ExpressApp.DC.XafDisplayName("Viết tắt họ tên")]
        VietTatHoTen
    }
}
