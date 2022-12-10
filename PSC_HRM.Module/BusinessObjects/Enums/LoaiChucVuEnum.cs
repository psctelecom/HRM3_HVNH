using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum LoaiChucVuEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Chức vụ chính quyền")]
        ChucVuChinhQuyen = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chức vụ đoàn thể")]
        ChucVuDoanThe = 1
    }
}
