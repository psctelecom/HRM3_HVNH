using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum HinhThucThanhToanEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Thanh toán qua thẻ")]
        ThanhToanQuaThe = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thanh toán bằng tiền mặt")]
        ThanhToanBangTienMat = 1
    }
}
