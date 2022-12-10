using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum PhanLoaiBienCheEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Tăng")]
        TangLaoDong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảm")]
        GiamLaoDong = 1,
     
      
    }
}
