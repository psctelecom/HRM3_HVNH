using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum TuyenDungEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Chưa thi")]
        ChuaThi = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đạt")]
        Dat = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Không đạt")]
        KhongDat = 2
    }
}
