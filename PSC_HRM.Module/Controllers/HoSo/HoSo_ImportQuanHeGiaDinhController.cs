using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using PSC_HRM.Module.HoSo;
using System.Windows.Forms;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.TuyenDung;
using System.Data;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.DanhMuc;
using System.IO;

namespace PSC_HRM.Module.Controllers
{    
    public partial class HoSo_ImportQuanHeGiaDinhController : ViewController
    {
        private IObjectSpace obs;
        public HoSo_ImportQuanHeGiaDinhController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HoSo_ImportQuanHeGiaDinhController");
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
            if (TruongConfig.MaTruong == "BUH")
                simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<HoSo.ThongTinNhanVien>();
            else
                simpleAction1.Active["TruyCap"] = false;
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            //
            ThongTinNhanVien _thongTinNhanVien = View.CurrentObject as ThongTinNhanVien;
            if (TruongConfig.MaTruong == "BUH")
                HoSo_ImportQuanHeGiaDinh.XuLy_BUH(obs, _thongTinNhanVien);
            //
            View.ObjectSpace.Refresh();
        }
    }
}
