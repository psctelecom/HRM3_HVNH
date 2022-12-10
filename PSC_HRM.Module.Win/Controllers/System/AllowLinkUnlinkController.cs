using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;

namespace PSC_HRM.Module.Win.Controllers
{
    public class AllowLinkUnlinkController : LinkUnlinkController
    {
        protected override void UpdateActionsState()
        {
            /* Update link/unlink actions */
            LinkAction.BeginUpdate();
            UnlinkAction.BeginUpdate();
            try
            {
                /* Inherited */
                base.UpdateActionsState();
                /* Exit when no view availlable */
                if (View == null)
                    return;

                DevExpress.ExpressApp.Model.Core.ModelNode node = (DevExpress.ExpressApp.Model.Core.ModelNode)View.Model;
                if (node != null && node is IAllowLinkUnlink)
                {
                    IAllowLinkUnlink allow = node as IAllowLinkUnlink;
                    if (allow != null)
                    {
                        LinkAction.Active["ViewAllowNew"] = allow.AllowLinkUnlink;
                        UnlinkAction.Active["ViewAllowDelete"] = allow.AllowLinkUnlink;
                    }
                }
            }
            finally
            {
                LinkAction.EndUpdate();
                UnlinkAction.EndUpdate();
            }
        }
    }

    //public class AllowCloneObject : 
}
