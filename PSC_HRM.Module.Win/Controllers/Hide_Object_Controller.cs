using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class Hide_Object_Controller : ViewController<ObjectView>
    {
        private AppearanceController appearanceController;
        public Hide_Object_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetViewId = "ThongTinBangChot_Moi_DetailView";
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            appearanceController = Frame.GetController<AppearanceController>();
            if (appearanceController != null)
            {
                appearanceController.CustomApplyAppearance += appearanceController_CustomApplyAppearance;
            }
        }

        void appearanceController_CustomApplyAppearance(object sender, ApplyAppearanceEventArgs e)
        {
            if (View is DetailView)
            {
                //if (e.ItemName == "BoPhan" && DevExpress.ExpressApp.ObjectView.ViewEditMode == ViewEditMode.Edit)
                //{
                //    foreach (IConditionalAppearanceItem item in e.AppearanceItems)
                //    {
                //        if (item is AppearanceItemVisibility)
                //        {
                //            ((AppearanceItemVisibility)item).Visibility = ViewItemVisibility.Show;
                //        }
                //    }
                //}  
                //if (e.Item is PropertyEditor)
                //{
                //    if (!DataManipulationRight.CanRead(View.ObjectTypeInfo.Type,
                //        ((PropertyEditor)e.Item).PropertyName,
                //        e.ContextObjects.Length > 0 ? e.ContextObjects[0] : null, null,
                //        View.ObjectSpace))
                //    {
                //        e.AppearanceObject.Visibility = ViewItemVisibility.Hide;
                //    }
                //}
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
