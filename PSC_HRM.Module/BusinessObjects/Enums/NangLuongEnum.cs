using System;

namespace PSC_HRM.Module
{
    public enum NangLuongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Nâng lương thường xuyên")]
        ThuongXuyen = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nâng lương do có thành tích xuất sắc")]
        CoThanhTichXuatSac = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nâng lương trước khi nghỉ hưu")]
        TruocKhiNghiHuu = 2
    }
}
