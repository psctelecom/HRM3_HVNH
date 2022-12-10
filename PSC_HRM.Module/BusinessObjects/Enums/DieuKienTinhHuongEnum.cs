using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum DieuKienTinhHuongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Bản thân ốm ngắn ngày")]
        BanThanOmNganNgay = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Bản thân ốm dài ngày")]
        BanThanOmDaiNgay = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Con ốm")]
        ConOm = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Khám thai")]
        KhamThai = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Sẩy thai, nạo hút thai, thai chết lưu")]
        SayThai = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Sinh con, nuôi con nuôi")]
        SinhCon = 5,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thực hiện các biện pháp tránh thai")]
        TranhThai = 6,
        [DevExpress.ExpressApp.DC.XafDisplayName("DS-PHSK sau thai sản")]
        DSPHSKSauThaiSan = 7,
        [DevExpress.ExpressApp.DC.XafDisplayName("DS-PHSK sau ốm đau")]
        DSPHSKSauOmDau = 8
    }
}
