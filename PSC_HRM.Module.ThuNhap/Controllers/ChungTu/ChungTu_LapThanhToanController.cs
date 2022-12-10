using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.ThuNhap.ChungTu;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ChungTu_LapThanhToanController : ViewController
    {
        public ChungTu_LapThanhToanController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ChungTu_LapThanhToanController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<ThanhToan>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Tiến hành lưu bảng thanh toán
            View.ObjectSpace.CommitChanges();
            //
            ThanhToan thanhToan = View.CurrentObject as ThanhToan;
            if (thanhToan != null)
            {
                if (thanhToan.ChungTu.KyTinhLuong.KhoaSo)
                {
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '{0}' đã khóa sổ.", thanhToan.ChungTu.KyTinhLuong.TenKy));
                }
                //else if (thanhToan.NgayLap < thanhToan.KyTinhLuong.TuNgay || thanhToan.NgayLap > thanhToan.KyTinhLuong.DenNgay.AddHours(1))
                //{
                //    DialogUtil.ShowWarning("Ngày lập phải nằm trong kỳ tính lương.");
                //}
                else
                {
                    using (DialogUtil.AutoWait())
                    {
                        SystemContainer.Resolver<ITaiChinh>("LapThanhToan").XuLy(View.ObjectSpace, thanhToan, null);
                        //
                        View.ObjectSpace.ReloadObject(thanhToan);
                        thanhToan.SoTienBangChu = HamDungChung.DocTien(Math.Round(thanhToan.SoTien, 0));
                        View.ObjectSpace.CommitChanges();
                        (View as DetailView).Refresh();
                    }
                    DialogUtil.ShowInfo("Thanh toán chứng từ thành công!");
                }
            }
        }  
    }
}
