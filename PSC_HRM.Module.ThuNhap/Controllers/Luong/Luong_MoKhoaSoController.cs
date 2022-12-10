using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class Luong_MoKhoaSoController : ViewController
    {
        public Luong_MoKhoaSoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void Luong_MoKhoaSoController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.CurrentUser().MoKhoaSoLuong;
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            KyTinhLuong kyTinhLuong = View.CurrentObject as KyTinhLuong;
            if (kyTinhLuong != null)
            {
                kyTinhLuong.KhoaSo = false;
                View.Refresh();
            }
        }
    }
}
