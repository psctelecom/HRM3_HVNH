using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum LoaiHoSoEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Nhân viên, giảng viên cơ hữu")]
        NhanVien = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảng viên thỉnh giảng")]
        GiangVien = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ứng viên")]
        UngVien = 2
    }
}
