using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.SystemModule;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class DisableNewButtonInDetailViewController : ViewController<DetailView>
    {
        public DisableNewButtonInDetailViewController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            WinNewObjectViewController view = Frame.GetController<WinNewObjectViewController>();
            if (view != null)
                view.NewObjectAction.Active[""] = false;
        }
    }
}
