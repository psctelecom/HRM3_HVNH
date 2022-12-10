using System;

namespace PSC_HRM.Module
{
    public enum LoaiViewEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("ListView")]
        ListView = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("DetailView")]
        DetailView = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("DashboardView")]
        DashboardView = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("CustomCategorizedListEditor")]
        CustomCategorizedListEditor = 3
    }
}
