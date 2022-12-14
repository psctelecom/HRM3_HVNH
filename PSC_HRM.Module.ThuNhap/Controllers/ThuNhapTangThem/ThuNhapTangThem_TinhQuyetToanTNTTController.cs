using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using PSC_HRM.Module.ThuNhap.ThuNhapTangThem;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ThuNhapTangThem_TinhQuyetToanTNTTController : ViewController
    {
        public ThuNhapTangThem_TinhQuyetToanTNTTController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ThuNhapTangThem_TinhQuyetToanTNTTController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangThuNhapTangThem>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            BangQuyetToanThuNhapTangThem bangQuyetToan = View.CurrentObject as BangQuyetToanThuNhapTangThem;
            if (bangQuyetToan != null)
            {
                if (bangQuyetToan.KyTinhLuong.KhoaSo)
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '{0}' đã khóa sổ.", bangQuyetToan.KyTinhLuong.TenKy));
                else if (bangQuyetToan.ChungTu != null)
                    DialogUtil.ShowWarning("Không thể tính quyết toán thu nhập tăng thêm. Bảng quyết toán thu nhập tăng thêm đã được lập chứng từ chi tiền.");
                else
                {
                    try
                    {
                        using (DialogUtil.AutoWait())
                        {
                            SystemContainer.Resolver<ITaiChinh>("TinhQuyetToanThuNhapTangThem").XuLy(View.ObjectSpace, bangQuyetToan, null);

                            View.ObjectSpace.ReloadObject(bangQuyetToan);
                            (View as DetailView).Refresh();
                        }
                        
                        //
                        DialogUtil.ShowInfo("Quyết toán thu nhập tăng thêm thành công.");
                    }
                    catch (Exception ex)
                    {
                        //
                        DialogUtil.ShowInfo("Quyết toán thu nhập tăng thêm không thành công." + ex.Message);
                    }
                }
            }
        }
    }
}
