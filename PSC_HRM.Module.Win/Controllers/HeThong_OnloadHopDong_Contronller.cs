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

namespace PSC_HRM.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class HeThong_OnloadHopDong_Contronller : ViewController
    {
        IObjectSpace _obs = null;
        Session _Session;
        public HeThong_OnloadHopDong_Contronller()
        {
            InitializeComponent();
            RegisterActions(components);

            TargetViewId = "HopDong_DetailView";
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
                if (_DetailView.ToString().Contains("HopDong_DetailView"))
                {
                    PSC_HRM.Module.HopDong.HopDong hopdong = View.CurrentObject as PSC_HRM.Module.HopDong.HopDong;
                    if (hopdong != null)
                    {
                        hopdong.UpdateNguoiKyList();
                    }
                }
            }
        }
    }
}
