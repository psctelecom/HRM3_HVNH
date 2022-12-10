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
using PSC_HRM.Module.TapSu;

namespace PSC_HRM.Module.Controllers
{
    public partial class TapSu_ImportBoNhiemNgachController : ViewController
    {
        public TapSu_ImportBoNhiemNgachController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TapSu_ImportBoNhiemNgachController");
        }

        private void TapSu_ImportBoNhiemNgachController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<DeNghiBoNhiemNgach>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            DeNghiBoNhiemNgach deNghiBoNhiemNgach = View.CurrentObject as DeNghiBoNhiemNgach;

            if (deNghiBoNhiemNgach != null)
            {
                bool oke = false;
                using (DialogUtil.AutoWait())
                {
                    oke = TapSu_ImportBoNhiemNgach.XuLy(View.ObjectSpace, deNghiBoNhiemNgach);
                }
                //Xuất thông báo cho người dùng
                if (oke)
                {
                    //
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
