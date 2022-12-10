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
using DevExpress.Utils;
using PSC_HRM.Module.ChamCong;
using System.Windows.Forms;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.Controllers;
using PSC_HRM.Module;
using PSC_HRM.Module.NangThamNien;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThamNien_ImportNangPhuCapThamNienHCController : ViewController
    {
        public ThamNien_ImportNangPhuCapThamNienHCController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThamNien_ImportNangPhuCapThamNienHCController");
        }

        private void ThamNien_ImportNangPhuCapThamNienHCController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhNangPhuCapThamNienHanhChinh>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            //
            QuyetDinhNangPhuCapThamNienHanhChinh quyetDinh = View.CurrentObject as QuyetDinhNangPhuCapThamNienHanhChinh;
            //
            if (quyetDinh != null)
            {
                NangThamNien_ImportNangPhuCapThamNienHC.XuLy(View.ObjectSpace,quyetDinh);
                //
                View.ObjectSpace.Refresh();
            }
        }
    }
}
