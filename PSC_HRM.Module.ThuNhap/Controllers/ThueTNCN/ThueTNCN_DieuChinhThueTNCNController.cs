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
    public partial class ThueTNCN_DieuChinhThueTNCNController : ViewController
    {
        public ThueTNCN_DieuChinhThueTNCNController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ThueTNCN_DieuChinhThueTNCNController_Activated(object sender, EventArgs e)
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
                    SystemContainer.Resolver<ITaiChinh>("TinhDieuChinhThueTNCN").XuLy(View.ObjectSpace, obj,null);

                    View.ObjectSpace.ReloadObject(obj);
                    (View as DetailView).Refresh();
                    XtraMessageBox.Show("Điều chỉnh thuế TNCN thành công", "Thông báo");
                }
            }
        }
    }
}
