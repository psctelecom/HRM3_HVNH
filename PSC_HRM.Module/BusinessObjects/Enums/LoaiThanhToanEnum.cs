using System;
using System.Collections.Generic;

namespace PSC_HRM.Module
{
    public enum LoaiThanhToanEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Tiền lương ngân sách")]
        TienLuongNganSach = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Bảo hiểm xã hội NLĐ đóng")]
        BHXH_NLĐ = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Bảo hiểm xã hội NSDLĐ đóng")]
        BHXH_NSDLĐ = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Kinh phí công đoàn Trường")]
        KFCD_Truong = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Kinh phí công đoàn LĐLĐ")]
        KFCD_LDLD = 4,
    }
}
