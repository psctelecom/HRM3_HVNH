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
using PSC_HRM.Module.Win.Controllers.Import.ImportClass;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.Win.Controllers.Import.ImportControl
{
    public partial class PMS_ImportThanhToanPhuTroi_Controller : ViewController
    {
        IObjectSpace _obs = null;
        public PMS_ImportThanhToanPhuTroi_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanlyThanhToan_DetailView";
        }
        //Đăng ký sự kiện Click 
        private void btnImportThanhToanPhuTroi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                QuanlyThanhToan OidQuanLy = View.CurrentObject as QuanlyThanhToan;
                if (OidQuanLy != null)
                {
                    _obs = View.ObjectSpace;
                    View.ObjectSpace.CommitChanges();
                    //Import
                    Import_ThanhToanPhuTroi.XuLy(_obs, OidQuanLy);
                    _obs.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    
    }
}
