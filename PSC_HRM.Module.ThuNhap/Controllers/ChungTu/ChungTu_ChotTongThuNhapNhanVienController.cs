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
    public partial class ChungTu_ChotTongThuNhapNhanVienController : ViewController
    {
        public ChungTu_ChotTongThuNhapNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ChungTu_ChotTongThuNhapNhanVienController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangChotTongThuNhapNhanVien>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Tiến hành lưu bảng chốt tổng thu nhập
            View.ObjectSpace.CommitChanges();
            //
            BangChotTongThuNhapNhanVien bangChotTongThuNhapNhanVien = View.CurrentObject as BangChotTongThuNhapNhanVien;
            if (bangChotTongThuNhapNhanVien != null)
            {
                if (bangChotTongThuNhapNhanVien.KyTinhLuong.KhoaSo)
                {
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '{0}' đã khóa sổ.", bangChotTongThuNhapNhanVien.KyTinhLuong.TenKy));
                }
                else if (bangChotTongThuNhapNhanVien.NgayChot < bangChotTongThuNhapNhanVien.KyTinhLuong.TuNgay || bangChotTongThuNhapNhanVien.NgayChot > bangChotTongThuNhapNhanVien.KyTinhLuong.DenNgay.AddHours(1))
                {
                    DialogUtil.ShowWarning("Ngày lập phải nằm trong kỳ tính lương.");
                }
                else
                {
                    using (DialogUtil.AutoWait())
                    {
                        SystemContainer.Resolver<ITaiChinh>("ChotTongThuNhapNhanVien").XuLy(View.ObjectSpace, bangChotTongThuNhapNhanVien, null);
                        //
                        View.ObjectSpace.ReloadObject(bangChotTongThuNhapNhanVien);
                       (View as DetailView).Refresh();
                    }
                    DialogUtil.ShowInfo("Chốt tổng thu nhập nhân viên thành công!");
                }
            }
        }  
    }
}
