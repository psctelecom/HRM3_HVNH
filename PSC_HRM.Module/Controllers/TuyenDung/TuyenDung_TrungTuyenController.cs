using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.TuyenDung;
using DevExpress.Utils;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class TuyenDung_TrungTuyenController : ViewController
    {
        public TuyenDung_TrungTuyenController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TuyenDung_TrungTuyenController");
        }

        private void TuyenDung_TrungTuyenController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<TrungTuyen>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                QuanLyTuyenDung qlTuyenDung = View.CurrentObject as QuanLyTuyenDung;
                if (qlTuyenDung != null)
                {
                    qlTuyenDung = obs.GetObjectByKey<QuanLyTuyenDung>(qlTuyenDung.Oid);
                    TuyenDungHelper.TrungTuyen(obs, qlTuyenDung);
                    HamDungChung.ShowSuccessMessage("Xét ứng viên trúng tuyển thành công.");
                }
                View.ObjectSpace.Refresh();
                View.Refresh();
            }
        }
    }
}
