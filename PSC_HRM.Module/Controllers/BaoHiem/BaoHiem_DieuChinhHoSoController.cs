using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class BaoHiem_DieuChinhHoSoController : ViewController
    {
        private DieuChinhHoSo obj;

        public BaoHiem_DieuChinhHoSoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void BaoHiem_DieuChinhHoSoController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyDieuChinhHoSo>()
                && HamDungChung.IsWriteGranted<ThongTinDieuChinh>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            obj = View.CurrentObject as DieuChinhHoSo;
            if (obj != null)
            {
                obj.XuLy();
            }
        }
    }
}
