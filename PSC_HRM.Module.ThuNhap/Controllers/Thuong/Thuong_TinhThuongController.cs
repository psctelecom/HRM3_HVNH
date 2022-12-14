using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using PSC_HRM.Module.ThuNhap.ThuNhapTangThem;
using PSC_HRM.Module.ThuNhap.Thuong;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class Thuong_TinhThuongController : ViewController
    {
        public Thuong_TinhThuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TinhLuongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangThuNhapTangThem>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            BangThuongNhanVien obj = View.CurrentObject as BangThuongNhanVien;
            if (obj != null)
            {
                if (obj.KyTinhLuong.KhoaSo)
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '{0}' đã khóa sổ.", obj.KyTinhLuong.TenKy));
                else if (obj.ChungTu != null)
                    DialogUtil.ShowWarning("Bảng khen thưởng - phúc lợi đã được lập chứng từ chi tiền.");
                else if (obj.NgayLap < obj.KyTinhLuong.TuNgay || obj.NgayLap > obj.KyTinhLuong.DenNgay)
                {
                    DialogUtil.ShowWarning("Ngày lập phải nằm trong kỳ tính lương.");
                }
                else
                {
                    SystemContainer.Resolver<ITaiChinh>("TinhKhenThuongPhucLoi").XuLy(View.ObjectSpace, obj, null);

                    View.ObjectSpace.ReloadObject(obj);
                    (View as DetailView).Refresh();
                    XtraMessageBox.Show("Tính khen thưởng - phúc lợi thành công", "Thông báo");
                }
            }
        }
    }
}
