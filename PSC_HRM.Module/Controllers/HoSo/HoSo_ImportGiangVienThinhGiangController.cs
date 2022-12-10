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

namespace PSC_HRM.Module.Controllers
{    
    public partial class HoSo_ImportGiangVienThinhGiangController : ViewController
    {
        public HoSo_ImportGiangVienThinhGiangController()
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
        
        private void HoSo_ImportGiangVienThinhGiangController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<GiangVienThinhGiang>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {                      
                        HoSo_ImportGiangVienThinhGiang.XuLy(View.ObjectSpace, dialog.FileName);
                        View.ObjectSpace.Refresh();
                    }
                }
            }
        }

       
    }
}
