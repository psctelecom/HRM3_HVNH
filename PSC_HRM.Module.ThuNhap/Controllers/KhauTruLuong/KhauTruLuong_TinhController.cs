using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.ThuNhap.KhauTru;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class KhauTruLuong_TinhController : ViewController
    {
        public KhauTruLuong_TinhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TinhLuongController_Activated(object sender, EventArgs e)
        {
            //05/11/2016 - Không tính khấu trừ lương từ công thức nữa (chỉ dùng import)
            //simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangKhauTruLuong>();
            simpleAction1.Active["TruyCap"] = false;
        }
        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            BangKhauTruLuong bangKhauTru = View.CurrentObject as BangKhauTruLuong;
            if (bangKhauTru != null)
            {
                if (bangKhauTru.KyTinhLuong.KhoaSo)
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '{0}' đã khóa sổ.", bangKhauTru.KyTinhLuong.TenKy));
                else if (bangKhauTru.ChungTu != null)
                    DialogUtil.ShowWarning("Không thể tính khấu trừ lương. Bảng khấu trừ lương đã được lập chứng từ chi tiền.");
                else if (bangKhauTru.NgayLap < bangKhauTru.KyTinhLuong.TuNgay || bangKhauTru.NgayLap > bangKhauTru.KyTinhLuong.DenNgay)
                {
                    DialogUtil.ShowWarning("Ngày lập phải nằm trong kỳ tính lương.");
                }
                else
                {
                    //tính dữ liệu mới
                    SystemContainer.Resolver<ITaiChinh>("TinhKhauTruLuong").XuLy(View.ObjectSpace, bangKhauTru, null);
                    View.ObjectSpace.ReloadObject(bangKhauTru);
                    (View as DetailView).Refresh();
                    XtraMessageBox.Show("Đã tính khấu trừ lương thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
