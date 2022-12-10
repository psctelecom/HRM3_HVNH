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
using System.Windows.Forms;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.Controllers;
using PSC_HRM.Module;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.ChuyenNgach;
using PSC_HRM.Module.NangNgach;

namespace PSC_HRM.Module.Controllers
{
    public partial class NangNgach_ImportNangNgachController : ViewController
    {
        public NangNgach_ImportNangNgachController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NangNgach_ImportNangNgachController");
        }

        private void NangNgach_ImportNangNgachController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<DeNghiChuyenNgach>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            DeNghiNangNgach deNghiNangNgach = View.CurrentObject as DeNghiNangNgach;

            if (deNghiNangNgach != null)
            {
                bool oke = false;
                using (DialogUtil.AutoWait())
                {
                    oke = NangNgach_ImportNangNgach.XuLy(View.ObjectSpace, deNghiNangNgach);
                }

                //Xuất thông báo cho người dùng
                if (oke)
                {
                    //
                    DialogUtil.ShowInfo("Import nâng ngạch thành công.");
                }
                else
                {
                    DialogUtil.ShowError("Import nâng ngạch không thành công.");
                }
            }
        }
    }
}
