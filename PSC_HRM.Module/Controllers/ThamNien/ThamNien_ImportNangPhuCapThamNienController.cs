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
    public partial class ThamNien_ImportNangPhuCapThamNienController : ViewController
    {
        public ThamNien_ImportNangPhuCapThamNienController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThamNien_ImportNangPhuCapThamNienController");
        }

        private void ThamNien_ImportNangPhuCapThamNienController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyNangPhuCapThamNien>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            DeNghiNangPhuCapThamNien deNghiNangPhuCapThamNien = View.CurrentObject as DeNghiNangPhuCapThamNien;
            //
            if (deNghiNangPhuCapThamNien != null)
            {
                bool oke = false;
                using (DialogUtil.AutoWait())
                {
                    //
                    oke = NangThamNien_ImportNangPhuCapThamNien.XuLy(View.ObjectSpace, deNghiNangPhuCapThamNien);
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
            }
        }
    }
}
