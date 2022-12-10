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

namespace PSC_HRM.Module.Controllers
{
    public partial class ChuyenNgach_ImportChuyenNgachController : ViewController
    {
        public ChuyenNgach_ImportChuyenNgachController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ChuyenNgach_ImportChuyenNgachController");
        }

        private void ChuyenNgach_ImportChuyenNgachController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<DeNghiChuyenNgach>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            DeNghiChuyenNgach deNghiChuyenNgach = View.CurrentObject as DeNghiChuyenNgach;

            if (deNghiChuyenNgach != null)
            {
                bool oke = false;
                using (DialogUtil.AutoWait())
                {
                    oke = ChuyenNgach_ImportChuyenNgach.XuLy(View.ObjectSpace, deNghiChuyenNgach);
                }

                //Xuất thông báo cho người dùng
                if (oke)
                {
                    DialogUtil.ShowInfo("Import chuyển ngạch thành công.");
                }
                else
                {
                    DialogUtil.ShowError("Import chuyển ngạch không thành công.");
                }
            }
        }
    }
}
