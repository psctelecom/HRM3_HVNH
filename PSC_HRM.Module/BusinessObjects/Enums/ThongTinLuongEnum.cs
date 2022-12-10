using System;

namespace PSC_HRM.Module
{
    public enum ThongTinLuongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương ngạch bậc")]
        LuongHeSo = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương khoán (có BHXH) theo số tiền")]
        LuongKhoanCoBHXH = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương khoán (không BHXH)")]
        LuongKhoanKhongBHXH = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương khoán (có BHXH) theo hệ số")]
        LuongKhoanBaoHiemLuongHeSo = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương khoán nhân viên công nhật")]
        LuongKhoanNhanVienCongNhat = 4
    }
}
