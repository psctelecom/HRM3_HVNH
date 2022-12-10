using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.ThuNhap
{
    public enum ChungTuEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương")]
        Luong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ngoài giờ")]
        NgoaiGio = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thù lao")]
        ThuLao = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thưởng")]
        Thuong = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thu nhập khác")]
        ThuNhapKhac = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Truy lương")]
        TruyLuong = 5
    }
}
