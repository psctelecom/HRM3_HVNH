using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum LoaiBienDongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Bổ sung")]
        BoSung = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thoái trả")]
        ThoaiTra
    }
}
