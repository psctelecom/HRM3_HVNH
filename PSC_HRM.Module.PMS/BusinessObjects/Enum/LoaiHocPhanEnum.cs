using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module.PMS.Enum
{
    public enum LoaiHocPhanEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Lý thuyết")]
        LyThuyet=0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thực hành")]
        ThucHanh = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thực tập")]
        ThucTap = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thảo luận")]
        ThaoLuan = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đồ án")]
        DoAn = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Luận án")]
        LuanAn = 5,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lý thuyết và thực hành")]
        LyThuyetThucHanh = 6,
        [DevExpress.ExpressApp.DC.XafDisplayName("LKL")]
        LKL = 7
    }
}
