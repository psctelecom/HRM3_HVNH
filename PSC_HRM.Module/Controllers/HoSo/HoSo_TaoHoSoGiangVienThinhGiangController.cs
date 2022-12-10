using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class HoSo_TaoHoSoGiangVienThinhGiangController : ViewController
    {
        public HoSo_TaoHoSoGiangVienThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HoSo_TaoHoSoGiangVienThinhGiangController");
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinNhanVien nhanVien = View.CurrentObject as ThongTinNhanVien;
            //
            if (nhanVien != null)
            {
                if (DialogUtil.ShowYesNo(String.Format("Bạn thật sự muốn tạo hồ sơ thỉnh giảng cho cán bộ {0} - {1}", nhanVien.MaQuanLy,nhanVien.HoTen)) == System.Windows.Forms.DialogResult.Yes)
                {
                    IObjectSpace obs = Application.CreateObjectSpace();
                    HoSo_TaoHoSoThingGiang.XuLy(obs, nhanVien);
                }
            }
        }

        private void HoSo_TaoHoSoGiangVienThinhGiangController_Activated(object sender, EventArgs e)
        {
            //
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<GiangVienThinhGiang>();
        }
    }
}
