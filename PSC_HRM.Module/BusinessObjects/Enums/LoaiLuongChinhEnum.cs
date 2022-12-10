using System;

namespace PSC_HRM.Module
{
    public enum LoaiLuongChinhEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương chính biên chế")]
        LuongChinhBienChe = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương chính hợp đồng trong chỉ tiêu biên chế")]
        LuongChinhHopDongTrongChiTieuBienChe = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương chính hợp đồng")]
        LuongChinhHopDong = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương chính hợp đồng khoán lương")]
        LuongChinhHopDongKhoanLuong = 3
    }
}
