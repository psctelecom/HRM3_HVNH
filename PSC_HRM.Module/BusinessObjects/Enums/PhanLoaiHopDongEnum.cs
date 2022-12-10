using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum PhanLoaiHopDongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng lập trong trường")]
        HopDongTrongTruong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng lập ngoài trường")]
        HopDongNgoaiTruong = 1
    }
}
