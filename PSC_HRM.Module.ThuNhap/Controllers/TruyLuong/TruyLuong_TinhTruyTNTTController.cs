using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.ThuNhap.TruyLuong;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class TruyLuong_TinhTruyTNTTController: ViewController
    {
        public TruyLuong_TinhTruyTNTTController()
        {
            InitializeComponent();
            RegisterActions(components);
        } 

        private void TruyLuong_TinhTruyTNTTController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangTruyLuong>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Lưu dữ liệu lại
            View.ObjectSpace.CommitChanges();
            //
            BangTruyLuong bangTruyLuong = View.CurrentObject as BangTruyLuong;
            if (bangTruyLuong != null)
            {
                using (DialogUtil.AutoWait())
                {
                    if (bangTruyLuong.KyTinhLuong.KhoaSo)
                        DialogUtil.ShowWarning(String.Format("Kỳ tính lương '{0}' đã khóa sổ.", bangTruyLuong.KyTinhLuong.TenKy));
                    else if (bangTruyLuong.ChungTu != null)
                        DialogUtil.ShowWarning("Bảng truy lĩnh đã được lập chứng từ chi tiền.");
                    else if (bangTruyLuong.NgayLap < bangTruyLuong.KyTinhLuong.TuNgay || bangTruyLuong.NgayLap > bangTruyLuong.KyTinhLuong.DenNgay)
                    {
                        DialogUtil.ShowWarning("Ngày lập phải nằm trong kỳ tính lương.");
                    }
                    else if (bangTruyLuong.DenNgay > bangTruyLuong.KyTinhLuong.DenNgay && TruongConfig.MaTruong == "LUH")
                    {
                        DialogUtil.ShowWarning("Đến ngày phải nằm trong kỳ tính lương.");
                    }
                    else
                    {
                        //Tính dữ liệu mới
                        SystemContainer.Resolver<ITaiChinh>("TinhTruyTNTT").XuLy(View.ObjectSpace, bangTruyLuong, null);

                        View.ObjectSpace.ReloadObject(bangTruyLuong);
                        (View as DetailView).Refresh();  
                    }
                }

                //
                DialogUtil.ShowInfo("Truy lĩnh TNTT thành công.");
            }
        }
    }
}
