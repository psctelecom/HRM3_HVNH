using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module.PMS.Enum
{
    public enum NguonDuLieuEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Khối lượng giảng dạy")]
        KhoiLuongGiangDay = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghiên cứu khoa học")]
        NCKH = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thanh toán tiền mặt")]
        ThanhToanTienMat = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hoạt động khác")]
        HoatDongKhac = 3
    }
}
