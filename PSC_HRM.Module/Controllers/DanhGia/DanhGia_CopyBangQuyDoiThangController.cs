using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.DanhGia;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class DanhGia_CopyBangQuyDoiThangController : ViewController
    {
        private IObjectSpace obs;
        private BangQuyDoiThang thang;

        public DanhGia_CopyBangQuyDoiThangController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DanhGia_CopyBangQuyDoiThangController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //lưu Quản lý hợp đồng
            View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();

            thang = View.CurrentObject as BangQuyDoiThang;
            BangQuyDoiThang copy = HamDungChung.Copy<BangQuyDoiThang>(((XPObjectSpace)obs).Session, thang);
            e.View = Application.CreateDetailView(obs, copy);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            obs.CommitChanges();
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void DanhGia_CopyBangQuyDoiThangController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangQuyDoiThang>();
        }
    }
}
