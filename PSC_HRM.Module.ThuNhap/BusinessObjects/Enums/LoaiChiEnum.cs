using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.ThuNhap
{
    public enum LoaiChiEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương kỳ 1")]
        LuongKy1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương kỳ 2")]
        LuongKy2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương tiến sĩ")]
        PhuCap,
        [DevExpress.ExpressApp.DC.XafDisplayName("Truy lĩnh")]
        TruyLinh,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương thử việc")]
        LuongThuViec,
        [DevExpress.ExpressApp.DC.XafDisplayName("Khen thưởng")]
        KhenThuong,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thu nhập tăng thêm")]
        ThuNhapTangThem,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ngoài giờ")]
        NgoaiGio,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thu nhập khác")]
        ThuNhapKhac,
        [DevExpress.ExpressApp.DC.XafDisplayName("Truy thu")]
        TruyThu,
        [DevExpress.ExpressApp.DC.XafDisplayName("Khấu trừ lương")]
        KhauTruLuong,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương và phụ cấp")]
        LuongVaPhuCap,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thù lao")]
        ThuLao,
        [DevExpress.ExpressApp.DC.XafDisplayName("Bổ sung lương kỳ 1")]
        BoSungLuongKy1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Bổ sung lương kỳ 2")]
        BoSungLuongKy2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Bổ sung phụ cấp thâm niên")]
        BoSungPhuCapThamNien,
        [DevExpress.ExpressApp.DC.XafDisplayName("Bổ sung phụ cấp trách nhiệm")]
        BoSungPhuCapTrachNhiem,
        [DevExpress.ExpressApp.DC.XafDisplayName("Bổ sung nâng lương kỳ 1")]
        BoSungNangLuongKy1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Bổ sung nâng lương kỳ 2")]
        BoSungNangLuongKy2,

        //QNU
        [DevExpress.ExpressApp.DC.XafDisplayName("Tiền phục vụ đào tạo")]
        PhucVuDaoTao,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tiền trách nhiệm quản lý")]
        TrachNhiemQuanLy,
        [DevExpress.ExpressApp.DC.XafDisplayName("Điện thoại công vụ")]
        DienThoai,
    }
}
