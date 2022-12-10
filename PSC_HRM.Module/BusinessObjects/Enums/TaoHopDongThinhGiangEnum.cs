using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum TaoHopDongThinhGiangEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng thỉnh giảng")]
        HopDongThinhGiang = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng thỉnh giảng chất lượng cao")]
        HopDongThinhGiangChatLuongCao = 1
    }
}
