using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using System.Windows.Forms;


namespace PSC_HRM.Module.Controllers
{
    public partial class HoSo_LuuTruGiayToHoSoController : ViewController
    {
        private IObjectSpace obs;
        public HoSo_LuuTruGiayToHoSoController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HoSo_LuuTruGiayToHoSoController");
        }
        protected override void OnActivated()
        {
            base.OnActivated();          
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();          
        }
        protected override void OnDeactivated()
        {          
            base.OnDeactivated();
        }

        private void HoSo_ImportQuanHeGiaDinhController_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong == "NEU")
                simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<NhanVien>();
            else
                simpleAction1.Active["TruyCap"] = false;
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            //
            if (View.Id.Contains("ThongTinNhanVien"))
            {
                ThongTinNhanVien _thongTinNhanVien = View.CurrentObject as ThongTinNhanVien;
                HoSo_LuuTruGiayToHoSo.XuLy(obs, _thongTinNhanVien);
            }
            if (View.Id.Contains("GiangVienThinhGiang"))
            {
                GiangVienThinhGiang _thongTinNhanVien = View.CurrentObject as GiangVienThinhGiang;
                HoSo_LuuTruGiayToHoSo.XuLyThinhGiang(obs, _thongTinNhanVien);
            }

            //
            View.ObjectSpace.Refresh();
        }
    }
}
