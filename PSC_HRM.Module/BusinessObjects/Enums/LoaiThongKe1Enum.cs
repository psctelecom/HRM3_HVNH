using System;
using System.Collections.Generic;
using System.Text;

namespace PSC_HRM.Module
{
    public enum LoaiThongKe1Enum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Theo tháng")]
        Thang = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Theo quý")]
        Quy = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Theo năm")]
        Nam = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Theo khoảng thời gian")]
        KhoangThoiGian = 3
    }
}
