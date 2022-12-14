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
using PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao;
using System.Windows.Forms;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class HoSo_LayDanhSachGVCH_TG_Controller : ViewController
    {
        IObjectSpace _obs = null;
        TrichDanhSachNhanVienCH_TG trichdanhsach;
        Session ses = null;
        public HoSo_LayDanhSachGVCH_TG_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "TrichDanhSachNhanVienCH_TG_DetailView";
        }

        private void btQuiDoi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            trichdanhsach = View.CurrentObject as TrichDanhSachNhanVienCH_TG;
            if (trichdanhsach != null)
            {
                trichdanhsach.LoadChiTiet(trichdanhsach.Session);
            }
        }
        
    }
}