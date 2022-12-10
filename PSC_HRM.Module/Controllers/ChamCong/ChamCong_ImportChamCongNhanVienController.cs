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
    public partial class ChamCong_ImportChamCongNhanVienController : ViewController
    {
        public ChamCong_ImportChamCongNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ChamCong_ImportChamCongNhanVienController");
        }

        private void ChamCong_ImportChamCongNhanVienController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyChamCongNhanVien>() &&
                HamDungChung.IsWriteGranted<ChiTietChamCongNhanVien>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Lưu bảng quản lý chấm công
            View.ObjectSpace.CommitChanges();
            //
            QuanLyChamCongNhanVien quanLyChamCong = View.CurrentObject as QuanLyChamCongNhanVien;
            if (quanLyChamCong != null)
            {
                    bool oke = false;
                    using (DialogUtil.AutoWait())
                    {           
                        IObjectSpace obs = Application.CreateObjectSpace();
                        if (TruongConfig.MaTruong.Equals("NEU"))
                        {
                            oke = ChamCong_ImportChamCongNEU.XuLy(obs, quanLyChamCong);
                        }
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
