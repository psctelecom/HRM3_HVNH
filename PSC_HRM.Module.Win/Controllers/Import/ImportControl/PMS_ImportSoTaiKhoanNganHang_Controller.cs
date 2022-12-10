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
    public partial class PMS_ImportSoTaiKhoanNganHang_Controller : ViewController
    {
        IObjectSpace _obs = null;
        public PMS_ImportSoTaiKhoanNganHang_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ThongTinNhanVien_RutGon_ListView";
        }
        //Đăng ký sự kiện Click 

        void PMS_ImportSoTaiKhoanNganHang_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong != "HUFLIT")
                PMS_ImportSoTaiKhoanNganHang.Active["TruyCap"] = false;
        }
        private void PMS_ImportSoTaiKhoanNganHang_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                _obs = View.ObjectSpace;
                Imp_BoSungTaiKhoanNganHang.XuLy(_obs);
                _obs.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
