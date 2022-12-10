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
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Controllers
{
    public partial class KhenThuong_ImportQuyetDinhKhenThuongTuExcelController : ViewController
    {
        private IObjectSpace obs;
        private KhenThuong_ImportQuyetDinhKhenThuong import;
        public KhenThuong_ImportQuyetDinhKhenThuongTuExcelController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("KhenThuong_ImportQuyetDinhKhenThuongTuExcelController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();

            import = obs.CreateObject<KhenThuong_ImportQuyetDinhKhenThuong>();
            e.View = Application.CreateDetailView(obs, import);

        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            e.PopupWindow.View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();
            import.XuLy_BUH(obs);
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();

        }
    }
}
