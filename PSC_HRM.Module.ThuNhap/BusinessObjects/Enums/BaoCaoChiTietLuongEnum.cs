using System;
using System.Collections.Generic;
using System.Text;

namespace PSC_HRM.Module.ThuNhap
{
    public enum BaoCaoChiTietLuongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Cán bộ không có tài khoản ngân hàng")]
        KhongCoTaiKhoan = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tất cả cán bộ")]
        TatCa = 1
    }
}
