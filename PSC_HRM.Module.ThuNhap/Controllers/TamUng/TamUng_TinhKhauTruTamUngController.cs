using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using PSC_HRM.Module.ThuNhap.TamUng;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class TamUng_TinhKhauTruTamUngController : ViewController
    {
        public TamUng_TinhKhauTruTamUngController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TruyLuongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangKhauTruTamUng>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            BangKhauTruTamUng bangKhauTruTamUng = View.CurrentObject as BangKhauTruTamUng;
            if (bangKhauTruTamUng != null)
            {
                if (bangKhauTruTamUng.KyTinhLuong.KhoaSo)
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '{0}' đã khóa sổ.", bangKhauTruTamUng.KyTinhLuong.TenKy));
                else if (bangKhauTruTamUng.ChungTu != null)
                    DialogUtil.ShowWarning("Bảng khấu trừ tạm ứng đã được lập chứng từ chi tiền.");
                else if (bangKhauTruTamUng.NgayLap < bangKhauTruTamUng.KyTinhLuong.TuNgay || bangKhauTruTamUng.NgayLap > bangKhauTruTamUng.KyTinhLuong.DenNgay)
                {
                    DialogUtil.ShowWarning("Ngày lập phải nằm trong kỳ tính lương.");
                }
                else
                {
                    //tính dữ liệu mới
                    SystemContainer.Resolver<ITaiChinh>("TinhKhauTruTamUng").XuLy(View.ObjectSpace, bangKhauTruTamUng, null);

                    View.ObjectSpace.ReloadObject(bangKhauTruTamUng);
                    (View as DetailView).Refresh();
                    XtraMessageBox.Show("Tính khấu trừ tạm ứng thành công", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
        }
    }
}
