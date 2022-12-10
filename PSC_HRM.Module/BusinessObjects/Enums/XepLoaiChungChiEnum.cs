using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum XepLoaiChungChiEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Xuất sắc")]
        XuatSac = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giỏi")]
        Gioi = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Khá")]
        Kha = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Trung bình khá")]
        TrungBinhKha = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Trung bình")]
        TrungBinh = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Không chọn")]
        KhongChon = 5
    }
}
