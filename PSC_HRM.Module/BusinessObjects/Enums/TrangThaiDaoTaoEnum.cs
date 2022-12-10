using System;

namespace PSC_HRM.Module
{
    public enum TrangThaiDaoTaoEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Đã hoàn thành")]
        DaHoanThanh = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chưa hoàn thành")]
        ChuaHoanThanh = 1
    }
}
