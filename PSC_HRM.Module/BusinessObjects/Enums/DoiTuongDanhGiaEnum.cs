using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module
{
    public enum DoiTuongDanhGiaEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảng viên")]
        GiangVien = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chuyên viên, Nhân viên")]
        NhanVien = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Trưởng đơn vị")]
        TruongDonVi = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ban giám hiệu")]
        BanGiamHieu = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hiệu trưởng")]
        HieuTruong = 5,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hội đồng thi đua, khen thưởng")]
        HoiDongKhenThuong = 6
    }
}
