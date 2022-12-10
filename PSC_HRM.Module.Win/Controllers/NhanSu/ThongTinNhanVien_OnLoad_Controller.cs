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
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.Win.Controllers.NhanSu
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ThongTinNhanVien_OnLoad_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session _Session;
        public ThongTinNhanVien_OnLoad_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ThongTinNhanVien_DetailView";
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            _obs = Application.CreateObjectSpace();
            _Session = ((XPObjectSpace)_obs).Session;
            DetailView _DetailView = View as DetailView;
            if (_DetailView != null)
            {
                if (_DetailView.ToString().Contains("ThongTinNhanVien_DetailView"))
                {
                    ThongTinNhanVien thongTinNhanVien = View.CurrentObject as ThongTinNhanVien;
                    if (thongTinNhanVien != null)
                    {
                        thongTinNhanVien.onloadTTNV();
                    }
                }
            }
        }
    }
}
