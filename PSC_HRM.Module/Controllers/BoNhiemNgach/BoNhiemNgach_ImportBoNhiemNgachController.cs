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
    public partial class BoNhiemNgach_ImportBoNhiemNgachController : ViewController
    {       
        public BoNhiemNgach_ImportBoNhiemNgachController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BoNhiemNgach_ImportBoNhiemNgachController");
        }

        private void BoNhiemNgach_ImportBoNhiemNgachController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhBoNhiemNgach>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            QuyetDinhBoNhiemNgach quyetDinhBoNhiemNgach = View.CurrentObject as QuyetDinhBoNhiemNgach;
            //
            if (TruongConfig.MaTruong != "NEU" && quyetDinhBoNhiemNgach != null)
            {
                BoNhiemNgach_ImportBoNhiemNgach.XuLyCu(View.ObjectSpace, quyetDinhBoNhiemNgach);
            }
            else
            {
                BoNhiemNgach_ImportBoNhiemNgach.XuLy(View.ObjectSpace);
            }
            View.ObjectSpace.Refresh();
        }
    }
}
