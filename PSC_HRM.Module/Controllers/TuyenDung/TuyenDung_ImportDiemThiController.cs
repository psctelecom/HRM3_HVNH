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
    public partial class TuyenDung_ImportDiemThiController : ViewController
    {

        private IObjectSpace obs;
        private DanhSachThi dsThi;
        private TuyenDung_ImportDiemThi import;
        public TuyenDung_ImportDiemThiController()
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

        private void TuyenDung_ImportDiemThiController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<TuyenDung.QuanLyTuyenDung>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            dsThi = View.CurrentObject as DanhSachThi;
            import = obs.CreateObject<TuyenDung_ImportDiemThi>();
           
                import.XuLy(View.ObjectSpace, dsThi);
                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            
        }
    }
}
