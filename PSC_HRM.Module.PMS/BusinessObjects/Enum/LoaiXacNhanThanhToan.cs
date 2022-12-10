using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.Enum
{
    public enum LoaiXacNhanThanhToan : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Tất cả")]
        TatCa=0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đã xác nhận")]
        DaXacNhan = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chưa xác nhận")]
        ChuaXacNhan = 2
    }
}
