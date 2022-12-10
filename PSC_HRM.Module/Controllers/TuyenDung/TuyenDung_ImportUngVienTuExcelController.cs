using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.TuyenDung;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.Utils;
using PSC_HRM.Module;
using DevExpress.Xpo;
using System.ComponentModel;

namespace PSC_HRM.Module.Controllers
{
    public partial class TuyenDung_ImportUngVienTuExcelController : ViewController
    {
        private IObjectSpace obs;
        private QuanLyTuyenDung qlTuyenDung;
        private TuyenDung_ImportUngVienTuExcel import;

        public TuyenDung_ImportUngVienTuExcelController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TuyenDung_ImportUngVienTuExcelController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            qlTuyenDung = View.CurrentObject as QuanLyTuyenDung;
            if (qlTuyenDung != null)
            {
                import = obs.CreateObject<TuyenDung_ImportUngVienTuExcel>();
                e.View = Application.CreateDetailView(obs, import);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            e.PopupWindow.View.ObjectSpace.CommitChanges();
            using (WaitDialogForm dialod = new WaitDialogForm("Đang import ứng viên từ file excel.", "Vui lòng chờ..."))
            {
                obs = Application.CreateObjectSpace();
                import.XuLy(obs);
                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            }
        }

        private void BienDongAction_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong == "BUH" || TruongConfig.MaTruong == "LUH")
            {
                popupWindowShowAction1.Active["TruyCap"] = false;
            }
            else
            popupWindowShowAction1.Active["TruyCap"] =
                HamDungChung.IsWriteGranted<QuanLyTuyenDung>() &&
                HamDungChung.IsWriteGranted<UngVien>();
        }
    }
}
