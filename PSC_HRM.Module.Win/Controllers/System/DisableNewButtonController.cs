using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.ExpressApp.Security;

namespace PSC_HRM.Module.Win
{
    public partial class DisableNewButtonController : ViewController<ListView>
    {
        public DisableNewButtonController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            WinNewObjectViewController view = Frame.GetController<WinNewObjectViewController>();
            if (view != null)
            {
                if (View.ObjectTypeInfo.Type != null
                    && !SecuritySystem.IsGranted(new ObjectAccessPermission(View.ObjectTypeInfo.Type, ObjectAccess.Create)))
                {
                    view.NewObjectAction.Active[""] = false;
                }
                else
                    view.NewObjectAction.Active[""] = true;
            }
        }
    }
}
