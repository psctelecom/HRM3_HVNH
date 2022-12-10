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
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.Win.Controllers.NhanSu
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ThamNien_CapNhatThamNienController : ViewController
    {
        TrinhDoChuyenMon_NangThoiHanApDung NangThamNien;
        public ThamNien_CapNhatThamNienController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetViewId = "TrinhDoChuyenMon_NangThoiHanApDung_DetailView";
        }

        void ThamNien_CapNhatThamNienController_ViewControlsCreated(object sender, System.EventArgs e)
        {
            NangThamNien = View.CurrentObject as TrinhDoChuyenMon_NangThoiHanApDung;
            DetailView view = View as DetailView;
            if (NangThamNien != null)
            {
                ControlDetailItem item = ((DetailView)View).FindItem("btKiemTraDuLieu") as ControlDetailItem;
                //
                if (item != null)
                {
                    SimpleButton btnSearch = item.Control as SimpleButton;
                    if (btnSearch != null)
                    {
                        btnSearch.Text = "Kiểm tra dữ liệu";
                        //btnSearch.Width = 80;
                        btnSearch.Click += (obj, ea) =>
                        {
                            NangThamNien.KiemTra();
                        };
                    }
                }
                ControlDetailItem itemCapNhat = ((DetailView)View).FindItem("btCapNhatThamNien") as ControlDetailItem;
                //
                if (itemCapNhat != null)
                {
                    SimpleButton btnCapNhat = itemCapNhat.Control as SimpleButton;
                    if (btnCapNhat != null)
                    {
                        btnCapNhat.Text = "Cập nhật";
                        //btnSearch.Width = 80;
                        btnCapNhat.Click += (obj, ea) =>
                        {
                            NangThamNien.CapNhat();
                        };
                    }
                }
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
