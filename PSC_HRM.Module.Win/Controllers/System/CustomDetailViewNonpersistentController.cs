using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using PSC_HRM.Module.TapSu;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.NangLuong;
using DevExpress.ExpressApp.SystemModule;
using PSC_HRM.Module.Controllers;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using System.Diagnostics;
using PSC_HRM.Module.ThuNhap;
using DevExpress.Xpo;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class CustomDetailViewNonpersistentController : ViewController
    {
        private ListViewProcessCurrentObjectController processCurrentObjectController;
        public CustomDetailViewNonpersistentController()
        {
            TargetObjectType = typeof(ISupportController);
        }

        private void processCurrentObjectController_CustomProcessSelectedItem(object sender, CustomizeShowViewParametersEventArgs e)
        {
            DetailView view = Application.CreateDetailView(ObjectSpace, View.CurrentObject, false);
            e.ShowViewParameters.CreatedView = view;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            processCurrentObjectController =
                Frame.GetController<ListViewProcessCurrentObjectController>();
            if (processCurrentObjectController != null)
            {
                processCurrentObjectController.CustomizeShowViewParameters += processCurrentObjectController_CustomProcessSelectedItem;
            }
        }

        protected override void OnDeactivated()
        {
            if (processCurrentObjectController != null)
            {
                processCurrentObjectController.CustomizeShowViewParameters -= processCurrentObjectController_CustomProcessSelectedItem;
                TargetObjectType = null;
            }
            base.OnDeactivated();
        }
    }
}
