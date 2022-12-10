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
using PSC_NormalizationData;
using System.Windows.Forms;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Controllers
{
    public partial class BaoMat_ChuanHoaDanhMucController : ViewController
    {
        public BaoMat_ChuanHoaDanhMucController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var form = new frmNormalizationData((((XPObjectSpace)View.ObjectSpace).Session).Connection as SqlConnection);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void BaoMat_ChuanHoaDanhMucController_Activated(object sender, EventArgs e)
        {
            this.simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<AppDictionaryNormalizationData>();
        }
    }
}
