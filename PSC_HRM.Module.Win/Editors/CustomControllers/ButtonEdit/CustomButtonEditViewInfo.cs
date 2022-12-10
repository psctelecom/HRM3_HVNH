using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System.Drawing;
using DevExpress.Utils.Drawing;

namespace PSC_HRM.Module.Win.CustomControllers.ButtonEdit
{
    public class CustomButtonEditViewInfo : ButtonEditViewInfo
    {
        public CustomButtonEditViewInfo(RepositoryItem item) 
            : base(item) 
        { }

        protected override void CalcContentRect(Rectangle bounds)
        {
            base.CalcContentRect(bounds);
            fMaskBoxRect = ContentRect;
            if (!(BorderPainter is EmptyBorderPainter))
                fMaskBoxRect.Inflate(-1, -1);
        }

    }
}
