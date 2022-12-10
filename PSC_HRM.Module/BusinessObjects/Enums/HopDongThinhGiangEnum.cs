using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum HopDongThinhGiangEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Theo quy chế chi tiêu nội bộ")]
        TheoQuyCheChiTieuNoiBo = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Số tiền đề nghị")]
        SoTienDeNghi = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng liên kết")]
        HopDongLienket = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng chủ nhiệm")]
        HopDongChuNhiem = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng cố vấn học tập")]
        HopDongCoVanHocTap = 4
    }
}
