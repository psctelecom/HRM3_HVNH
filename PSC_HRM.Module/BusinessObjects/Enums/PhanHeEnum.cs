using System;

namespace PSC_HRM.Module
{
    public enum PhanHeEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Nhân sự")]
        NhanSu = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tài chính")]
        TaiChinh = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Danh mục")]
        DanhMuc = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hệ thống")]
        HeThong = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Bàn làm việc")]
        BanLamViec = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("PMS")]
        PMS = 5
    }
}
