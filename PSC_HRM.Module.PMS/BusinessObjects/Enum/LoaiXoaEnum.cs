using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.Enum
{
    public enum LoaiXoaEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Tất cả")]
        TatCa=0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Dữ liệu Import")]
        DuLieuImport = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Dữ liệu Đồng bộ")]
        DuLieuDongBo = 2
    }
}
