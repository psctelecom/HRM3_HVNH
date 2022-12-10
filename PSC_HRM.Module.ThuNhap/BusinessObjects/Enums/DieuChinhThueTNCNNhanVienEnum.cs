using System;
using System.Collections.Generic;
using System.Text;

namespace PSC_HRM.Module.ThuNhap
{
    public enum DieuChinhThueTNCNNhanVienEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("BHXH")]
        BHXH = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("BHYT")]
        BHYT = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("BHTN")]
        BHTN = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Số người phụ thuộc")]
        SoNguoiPhuThuoc = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thu nhập chịu thuế")]
        ThuNhapChiuThue = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("TNCT làm căn cứ giảm trừ")]
        TNCTLamCanCuGiamTru = 5
    }
}
