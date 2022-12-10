using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum DieuKhoanHopDongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng làm việc")]
        HopDongLamViec = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng khoán")]
        HopDongKhoan = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng lao động")]
        HopDongLaoDong = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng thỉnh giảng (quy chế chi tiêu nội bộ)")]
        HopDongThinhGiang1 = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng thỉnh giảng (số tiền đề nghị)")]
        HopDongThinhGiang2 = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng liên kết")]
        HopDongThinhGiang3 = 5,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng chủ nhiệm")]
        HopDongThinhGiang4 = 6,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng cố vấn học tập")]
        HopDongThinhGiang5 = 7
    }
}
