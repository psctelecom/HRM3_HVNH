using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.ThuNhap.KhauTru;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.ThuNhap.Thue;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ThueTNCN_MienGiamThueTNCNController : ViewController
    {
        private ToKhaiQuyetToanThueTNCN toKhai;
        private MienGiamThueTNCN obj;
        private IObjectSpace obs;

        public ThueTNCN_MienGiamThueTNCNController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void KhauTruLuongCopyController_Activated(object sender, EventArgs e)
        {
            actCopy.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangKhauTruLuong>();
        }

        private void actCopy_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            toKhai = View.CurrentObject as ToKhaiQuyetToanThueTNCN;
            if (toKhai != null)
            {
                obs = Application.CreateObjectSpace();
                obj = obs.CreateObject<MienGiamThueTNCN>();
                e.View = Application.CreateDetailView(obs, obj);
                e.DialogController.AcceptAction.ConfirmationMessage = "Bạn có muốn tính miễn giảm thuế TNCN không?";
                e.DialogController.AcceptAction.Caption = "Xử lý";
            }
        }

        private void actCopy_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            using (WaitDialogForm wait = new WaitDialogForm("Hệ thống đang xử lý.", "Vui lòng chờ..."))
            {
                obs = Application.CreateObjectSpace();
                e.PopupWindow.View.ObjectSpace.CommitChanges();
                obj.XuLy(obs, toKhai);

                View.ObjectSpace.ReloadObject(toKhai);
                (View as DetailView).Refresh();
                XtraMessageBox.Show("Tính miễn giảm thuế TNCN thành công", "Thông báo");
            }
        }
    }
}
