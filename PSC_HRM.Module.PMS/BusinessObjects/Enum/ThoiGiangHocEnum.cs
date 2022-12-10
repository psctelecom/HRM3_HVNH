using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module.PMS.Enum
{
    public enum ThoiGiangHocEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Buổi học bình thường")]
        BuoiHocBinhThuong=0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Buổi học ngoài giờ")]
        BuoiHocNgoaiGio = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tất cả")]
        TatCa = 2
    }
}
