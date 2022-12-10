using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.DanhGia;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class DanhGia_DanhSachGiangVienViPhamController : ViewController
    {
        public DanhGia_DanhSachGiangVienViPhamController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DanhGia_DanhSachGiangVienViPhamController");
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //lưu bảng theo dõi vi phạm
            View.ObjectSpace.CommitChanges();

            BangTheoDoiViPham bang = View.CurrentObject as BangTheoDoiViPham;
            if (bang != null)
                DanhGiaHelper.ImportViPhamPMS(Application, bang);
        }

        private void DanhGia_DanhSachGiangVienViPhamController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangTheoDoiViPham>() &&
                HamDungChung.IsWriteGranted<ChiTietViPham>();
        }

    }
}
