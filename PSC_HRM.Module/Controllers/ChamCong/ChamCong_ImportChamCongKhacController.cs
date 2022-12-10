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
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class ChamCong_ImportChamCongKhacController : ViewController
    {
        public ChamCong_ImportChamCongKhacController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ChamCong_ImportChamCongKhacController");
        }

        private void ChamCong_ImportChamCongKhacController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyChamCongKhac>() &&
                HamDungChung.IsWriteGranted<ChiTietChamCongKhac>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Lưu bảng quản lý chấm công
            View.ObjectSpace.CommitChanges();
            //
            QuanLyChamCongKhac quanLyChamCong = View.CurrentObject as QuanLyChamCongKhac;
            if (quanLyChamCong != null)
            {
                    bool oke = false;
                    using (DialogUtil.AutoWait())
                    {           
                        IObjectSpace obs = Application.CreateObjectSpace();
                        //
                        oke = ChamCong_ImportChamCongKhac.XuLy(obs, quanLyChamCong);
                        //Refesh lại dữ liệu
                        View.ObjectSpace.Refresh();
                    }
                    //Xuất thông báo cho người dùng
                    if (oke)
                    {
                        DialogUtil.ShowInfo("Import chấm công thành công.");
                    }
                    else
                    {
                        DialogUtil.ShowError("Import chấm công không thành công.");
                    }
            }

        }
    }
}
