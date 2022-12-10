using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.ThuNhap.Thue;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ThueTNCN_KhauTruThueTNCNController : ViewController
    {
        public ThueTNCN_KhauTruThueTNCNController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void KhauTruThueTNCNController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<ToKhaiKhauTruThueTNCN>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            ToKhaiKhauTruThueTNCN obj = View.CurrentObject as ToKhaiKhauTruThueTNCN;
            if (obj != null)
            {
                using (WaitDialogForm wait = new WaitDialogForm("Hệ thống đang xử lý", "Vui lòng chờ..."))
                {
                    SystemContainer.Resolver<ITaiChinh>("TinhKhauTruThueTNCN").XuLy(View.ObjectSpace, obj, null);

                    View.ObjectSpace.ReloadObject(obj);
                    (View as DetailView).Refresh();
                    XtraMessageBox.Show("Lập tờ khai khấu trừ thuế TNCN thành công", "Thông báo");
                }
            }
        }
    }
}
