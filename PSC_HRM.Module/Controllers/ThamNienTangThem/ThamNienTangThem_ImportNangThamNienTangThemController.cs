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
using PSC_HRM.Module.NangThamNienTangThem;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThamNienTangThem_ImportNangThamNienTangThemController : ViewController
    {
        public ThamNienTangThem_ImportNangThamNienTangThemController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThamNienTangThem_ImportNangThamNienTangThemController");
        }

        private void ThamNienTangThem_ImportNangThamNienTangThemController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyNangThamNienTangThem>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            DeNghiNangThamNienTangThem deNghiNangThamNienTangThem = View.CurrentObject as DeNghiNangThamNienTangThem;
            //
            if (deNghiNangThamNienTangThem != null)
            {
                bool oke = false;
                using (DialogUtil.AutoWait())
                {
                    //
                    oke = NangThamNienTangThem_ImportNangThamNienTangThem.XuLy(View.ObjectSpace, deNghiNangThamNienTangThem);
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
