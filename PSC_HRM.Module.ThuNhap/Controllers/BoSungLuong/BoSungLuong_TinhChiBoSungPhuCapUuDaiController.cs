using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Security;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;
using PSC_HRM.Module.ThuNhap.BoSungLuong;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class BoSungLuong_TinhChiBoSungPhuCapUuDaiController : ViewController
    {
        public BoSungLuong_TinhChiBoSungPhuCapUuDaiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void BoSungLuong_TinhChiBoSungPhuCapUuDaiController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BoSungLuongNhanVien>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            BoSungLuongNhanVien boSungLuong = View.CurrentObject as BoSungLuongNhanVien;
            if (boSungLuong != null)
            {
                if (boSungLuong.KyTinhLuong.KhoaSo)
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '{0}' đã khóa sổ.", boSungLuong.KyTinhLuong.TenKy));
                else if (boSungLuong.ChungTuLuongKy1 != null)
                    DialogUtil.ShowWarning("Bảng lương đã được lập chứng từ chi tiền.");
                else if (boSungLuong.NgayLap < boSungLuong.KyTinhLuong.TuNgay || boSungLuong.NgayLap > boSungLuong.KyTinhLuong.DenNgay)
                {
                    DialogUtil.ShowWarning("Ngày lập phải nằm trong kỳ tính lương.");
                }
                else
                {
                    try
                    {
                        using (DialogUtil.AutoWait())
                        {
                            //Tính chi bổ sung lương phụ cấp ưu đãi
                            TinhChiBoSungLuong.LuongPhuCapUuDai(boSungLuong, ((XPObjectSpace)View.ObjectSpace).Session);
                            
                            //Refesh lại dữ liệu
                            View.ObjectSpace.ReloadObject(boSungLuong);
                            (View as DetailView).Refresh();
                        }
                        //Thông báo kết quả
                        DialogUtil.ShowInfo("Tính bổ sung lương phụ cấp ưu đãi thành công.");
                    }
                    catch (Exception ex)
                    {
                        //Thông báo lỗi
                        DialogUtil.ShowError("Tính lương bổ sung lương phụ cấp ưu đãi không thành công." + ex.Message);
                    }
                }
            }
        }
    }
}
