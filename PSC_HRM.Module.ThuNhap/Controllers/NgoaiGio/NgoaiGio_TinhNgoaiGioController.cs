using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.NgoaiGio;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class NgoaiGio_TinhNgoaiGioController : ViewController
    {
        public NgoaiGio_TinhNgoaiGioController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void NgoaiGio_TinhNgoaiGioController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangLuongNgoaiGio>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            BangLuongNgoaiGio obj = View.CurrentObject as BangLuongNgoaiGio;
            if (obj != null)
            {
                if (obj.KyTinhLuong.KhoaSo)
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '{0}' đã khóa sổ.", obj.KyTinhLuong.TenKy));
                else if (obj.ChungTu != null)
                    DialogUtil.ShowWarning("Bảng lương ngoài giờ đã được lập chứng từ chi tiền.");
                else if (obj.NgayLap < obj.KyTinhLuong.TuNgay || obj.NgayLap > obj.KyTinhLuong.DenNgay)
                {
                    DialogUtil.ShowWarning("Ngày lập phải nằm trong kỳ tính lương.");
                }
                else
                {
                    //tính dữ liệu mới
                    SystemContainer.Resolver<ITaiChinh>("TinhTienNgoaiGio").XuLy(View.ObjectSpace, obj, null);
                    XtraMessageBox.Show("Tính tiền ngoài giờ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    View.ObjectSpace.Refresh();
                }
            }
        }
    }
}
