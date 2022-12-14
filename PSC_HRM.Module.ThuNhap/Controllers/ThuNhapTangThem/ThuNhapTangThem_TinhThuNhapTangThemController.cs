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
    public partial class ThuNhapTangThem_TinhThuNhapTangThemController : ViewController
    {
        public ThuNhapTangThem_TinhThuNhapTangThemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ThuNhapTangThem_TinhThuNhapTangThemController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangThuNhapTangThem>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            BangThuNhapTangThem bangThuNhapTangThem = View.CurrentObject as BangThuNhapTangThem;
            if (bangThuNhapTangThem != null)
            {
                if (bangThuNhapTangThem.KyTinhLuong.KhoaSo)
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '{0}' đã khóa sổ.", bangThuNhapTangThem.KyTinhLuong.TenKy));
                else if (bangThuNhapTangThem.ChungTu != null)
                    DialogUtil.ShowWarning("Bảng thu nhập tăng thêm đã được lập chứng từ chi tiền.");
                else if (bangThuNhapTangThem.NgayLap < bangThuNhapTangThem.KyTinhLuong.TuNgay || bangThuNhapTangThem.NgayLap > bangThuNhapTangThem.KyTinhLuong.DenNgay)
                {
                    DialogUtil.ShowWarning("Ngày lập phải nằm trong kỳ tính lương.");
                }
                else
                {
                    try
                    {
                        using (DialogUtil.AutoWait())
                        {
                            SystemContainer.Resolver<ITaiChinh>("TinhThuNhapTangThem").XuLy(View.ObjectSpace, bangThuNhapTangThem, null);

                            View.ObjectSpace.ReloadObject(bangThuNhapTangThem);
                            (View as DetailView).Refresh();
                        }

                        //
                        DialogUtil.ShowInfo("Tính thu nhập tăng thêm thành công.");
                    }
                    catch (Exception ex)
                    {
                        //
                        DialogUtil.ShowInfo("Tính thu nhập tăng thêm không thành công." + ex.Message);
                    }
                }
            }
        }
    }
}
