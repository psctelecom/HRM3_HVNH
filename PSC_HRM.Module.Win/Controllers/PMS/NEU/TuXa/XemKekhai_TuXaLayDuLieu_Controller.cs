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
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.NEU.DaoTaoTuXa;

namespace PSC_HRM.Module.Win.Controllers.PMS.NEU
{
    public partial class XemKekhai_TuXaLayDuLieu_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session ses;
        QuanLyXemKeKhaiTuXa_Non _KeKhai;
        public XemKekhai_TuXaLayDuLieu_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyXemKeKhaiTuXa_Non_DetailView";
        }

        private void btQuyDoi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _KeKhai = View.CurrentObject as QuanLyXemKeKhaiTuXa_Non;
            if (_KeKhai != null)
            {
                _KeKhai.LoadData();
                View.Refresh();
            }

        }
    }
}