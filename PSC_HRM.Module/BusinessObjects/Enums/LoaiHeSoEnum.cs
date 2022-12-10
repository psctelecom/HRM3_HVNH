using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum LoaiHeSoEnumUTE : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Phụ cấp ưu  đãi")]
        PhuCapUuDai = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("HSL tăng thêm")]
        HSLTangThem = 2,
    }
    public enum LoaiHeSoEnumIUH : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Phụ cấp ưu  đãi")]
        PhuCapUuDai = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Phần trăm tiết chuẩn")]
        PhanTramTietChuan = 2,
    }

    public enum LoaiHeSoEnumDLU : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Hệ số PC độc hại")]
        HSPCDocHai = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hệ số PC trách nhiệm")]
        HSPCTrachNhiem = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hệ số PC khu vực")]
        HSPCKhuVuc = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tỉ lệ tăng thêm")]
        TiLeTangThem = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Phụ cấp ưu đãi")]
        PhuCapUuDai = 5,
    }
}
