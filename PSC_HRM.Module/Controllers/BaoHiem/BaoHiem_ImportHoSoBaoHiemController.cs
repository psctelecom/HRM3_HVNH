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
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoHiem;

namespace PSC_HRM.Module.Controllers
{
    public partial class BaoHiem_ImportHoSoBaoHiemController : ViewController
    {
        public BaoHiem_ImportHoSoBaoHiemController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BaoHiem_ImportHoSoBaoHiemController");
        }

        private void BaoHiem_ImportHoSoBaoHiemController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<HoSoBaoHiem>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            bool oke = false;
            //
            using (DialogUtil.AutoWait())
            {
                oke = BaoHiem_ImportHoSoBaoHiem.XuLy(View.ObjectSpace);
            }
            //Xuất thông báo cho người dùng
            if (oke)
            {
                DialogUtil.ShowInfo("Import cán bộ thành công.");
            }
            else
            {
                DialogUtil.ShowError("Import cán bộ không thành công.");
            }
            //
            View.ObjectSpace.Refresh();
        }
    }
}
