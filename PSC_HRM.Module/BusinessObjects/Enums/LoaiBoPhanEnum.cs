using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum LoaiBoPhanEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Trường; Đơn vị")]
        Truong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Phòng; Ban")]
        PhongBan = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Khoa; Trung tâm; Viện")]
        Khoa = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Bộ môn trực thuộc Trường")]
        BoMonTrucThuocTruong = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Bộ môn trực thuộc Khoa")]
        BoMonTrucThuocKhoa = 3
    }
}
