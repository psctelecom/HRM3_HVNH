using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module.PMS.Enum
{
    public enum VaiTroNCKHEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Chính")]
        Chinh = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chủ trì đề tài, tác giả chính")]
        TacGia = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thành viên")]
        ThanhVien= 2
        
    }
}
