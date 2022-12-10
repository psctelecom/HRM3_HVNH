using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using PSC_HRM.Module.PMS.NonPersistent;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.ThuNhap.Controllers;
using PSC_HRM.Module.PMS;
using PSC_HRM.Module.ThuNhap.ThuLao;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class BangChotThuLao_XoaBangChot_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        public BangChotThuLao_XoaBangChot_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "BangChotThuLao_ListView";
        }

        private void btXoaDuLieu_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

        }

    }
}