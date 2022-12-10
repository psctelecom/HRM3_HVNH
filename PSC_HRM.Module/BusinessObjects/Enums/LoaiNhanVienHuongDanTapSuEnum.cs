using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum LoaiNhanVienHuongDanTapSuEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Nhân viên")]
        NhanVien = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảng viên thỉnh giảng")]
        ThinhGiang = 2
    }
}
