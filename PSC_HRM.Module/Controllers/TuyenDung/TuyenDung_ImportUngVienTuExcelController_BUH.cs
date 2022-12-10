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

namespace PSC_HRM.Module.Controllers
{    
    public partial class TuyenDung_ImportUngVienTuExcelController_BUH : ViewController
    {

        private IObjectSpace obs;
        private QuanLyTuyenDung qlTuyenDung;
        private TuyenDung_ImportUngVienTuExcel_BUH import;
        public TuyenDung_ImportUngVienTuExcelController_BUH()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TuyenDung_ImportUngVienTuExcelController_BUH");
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

        private void TuyenDung_ImportUngVienTuExcelController_BUH_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong == "BUH" || TruongConfig.MaTruong == "LUH")
                simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<TuyenDung.QuanLyTuyenDung>();
            else
                simpleAction1.Active["TruyCap"] = false;
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            qlTuyenDung = View.CurrentObject as QuanLyTuyenDung;
            import = obs.CreateObject<TuyenDung_ImportUngVienTuExcel_BUH>();
            if (TruongConfig.MaTruong == "LUH")
            {
                import.XuLy_LUH(View.ObjectSpace, qlTuyenDung);       
            }
            else
            {
                import.XuLy(View.ObjectSpace, qlTuyenDung);                
            }
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }
    }
}
