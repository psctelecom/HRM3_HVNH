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
using System.Windows.Forms;
using PSC_HRM.Module.PMS.NghiepVu.NCKH;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.PMS.BaoCao;
using PSC_HRM.Module.PMS.BusinessObjects.BaoCao.UFM;
using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.NEU.DaoTaoTuXa;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Win.Controllers.PMS
{      
    public partial class BaoCao_TongHopDuLieuTuXa_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        QuanLyXemKeKhaiTuXa_Non non;
        public BaoCao_TongHopDuLieuTuXa_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyXemKeKhaiTuXa_Non_DetailView";
        }    

        private void btnSearch_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            non = View.CurrentObject as QuanLyXemKeKhaiTuXa_Non;
            DevExpress.ExpressApp.DC.ITypeInfo type;
            int OIDReport = 3581;

            type = ObjectSpace.TypesInfo.FindTypeInfo("PSC_HRM.Module.PMS.BusinessObjects.BaoCao.UFM.Report_PMS_HeTuXa_BangTongHopDuLieuKeKhai");
            if (type != null)
            {
                _obs = Application.CreateObjectSpace();
                session = ((XPObjectSpace)_obs).Session;
                StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type.Type);

                HRMReport report = _obs.FindObject<HRMReport>(CriteriaOperator.Parse("Oid =?", OIDReport));
                if (report != null)
                {
                    //Truyền parameter
                    ((Report_PMS_HeTuXa_BangTongHopDuLieuKeKhai)rpt).NamHoc = session.GetObjectByKey<NamHoc>(non.NamHoc.Oid);
                    ((Report_PMS_HeTuXa_BangTongHopDuLieuKeKhai)rpt).HocKy = session.GetObjectByKey<HocKy>(non.HocKy.Oid);
                    //
                    StoreProcedureReport.Param = rpt;
                    Frame.GetController<ReportServiceController>().ShowPreview(report);
                }
            }
        }
    }
}
