using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi;
using PSC_HRM.Module.Win.Controllers.Import.ImportClass.NEU;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.Controllers.Import.NEU
{
    public partial class PMS_Import_QuanLyCongTacPhi_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session _Session;
        CollectionSource collectionSource;
        public PMS_Import_QuanLyCongTacPhi_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            if(TruongConfig.MaTruong == "NEU")
            {
                TargetViewId = "QuanLyCongTacPhi_DetailView";
            }
            else
            {
                TargetViewId = "NULL";
            }
        }
        

        

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            QuanLyCongTacPhi OidQuanLy = View.CurrentObject as QuanLyCongTacPhi;
            if (OidQuanLy != null)
            {
                _obs = View.ObjectSpace;
                int KQ =Imp_QuanLyCongTacPhi_NEU.XuLy(_obs, OidQuanLy);

                if(KQ == 400)
                {
                    XtraMessageBox.Show("Không thành công . Import không thành công vào cơ sỡ dữ liệu không thành công", "Lỗi");
                    return;
                }
                View.Refresh();
                View.ObjectSpace.Refresh();
            }

        }
    }
}
