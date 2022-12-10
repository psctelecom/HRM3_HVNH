using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.Enum
{
    public enum LoaiKhaoThi : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Coi Thi")]
        CoiThi = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chấm thi")]
        ChamThi = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hoạt động khác")]
        HoatDongKhac = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("NCKH")]
        NCKH = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ra đề")]
        RaDe = 4
    }
}
