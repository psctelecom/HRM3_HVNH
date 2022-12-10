using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.ThuNhap.Thue;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ThueTNCN_QuyetToanThueTNCNController : ViewController
    {
        public ThueTNCN_QuyetToanThueTNCNController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void QuyetToanThueTNCNController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<ToKhaiQuyetToanThueTNCN>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            ToKhaiQuyetToanThueTNCN obj = View.CurrentObject as ToKhaiQuyetToanThueTNCN;
            if (obj != null)
            {
                using (WaitDialogForm wait = new WaitDialogForm("Hệ thống đang xử lý", "Vui lòng chờ..."))
                {
                    SystemContainer.Resolver<ITaiChinh>("TinhQuyetToanThueTNCN").XuLy(View.ObjectSpace, obj, null);

                    View.ObjectSpace.ReloadObject(obj);
                    (View as DetailView).Refresh();
                    XtraMessageBox.Show("Lập tờ khai quyết toán thuế TNCN thành công", "Thông báo");
                }
            }
        }
    }
}
