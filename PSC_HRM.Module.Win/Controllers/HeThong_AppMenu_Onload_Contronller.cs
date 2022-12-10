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
using PSC_HRM.Module.PMS.GioChuan;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.CauHinh;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class HeThong_AppMenu_Onload_Contronller : ViewController
    {
        IObjectSpace _obs = null;
        Session _Session;
        public HeThong_AppMenu_Onload_Contronller()
        {
            InitializeComponent();
            RegisterActions(components);

            TargetViewId = "AppMenu_DetailView";
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            _obs = Application.CreateObjectSpace();
            _Session = ((XPObjectSpace)_obs).Session;
            DetailView _DetailView = View as DetailView;
            if (_DetailView != null)
            {
                if (_DetailView.ToString().Contains("AppMenu_DetailView"))
                {
                    AppMenu app = View.CurrentObject as AppMenu;
                    if (app != null)
                    {
                        app.UpdateAppMenuParentList();
                    }
                }
            }
        }
    }
}
