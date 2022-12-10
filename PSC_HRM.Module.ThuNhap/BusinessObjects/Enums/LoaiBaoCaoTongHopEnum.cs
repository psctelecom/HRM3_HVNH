using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.ThuNhap
{
    public enum LoaiBaoCaoTongHopEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Đang làm việc")]
        DangLamViec = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tạm giữ lương và nghỉ không lương")]
        TamGiuLuongVaKhongLuong = 1
    }
}
