using System;

namespace PSC_HRM.Module
{
    public enum AccountTypeEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Phòng Tổ chức cán bộ")]
        ToChucCanBo = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Phòng Kế hoạch - Tài chính")]
        KeHoachTaiChinh = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Phòng/Ban khác")]
        Khac = 2
    }
}
