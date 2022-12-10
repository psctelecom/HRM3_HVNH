using System;

namespace PSC_HRM.Module
{
    public enum HinhThucThiTuyenEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Thi viết")]
        ThiViet = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thi thực hành")]
        ThiThucHanh = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thi nghiệp vụ chuyên môn")]
        ThiNghiepVuChuyenMon = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảng thử")]
        GiangThu = 3
    }
}
