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
using PSC_HRM.Module.Report;
using PSC_HRM.Module.PMS.BaoCao;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class BaoCaoHopDong_ThanhLy_ThinhGiang_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        public BaoCaoHopDong_ThanhLy_ThinhGiang_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "HopDong_ThanhLy_ThinhGiang_ListView";
        }
        private void BaoCaoHopDong_ThanhLy_ThinhGiang_Controller_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong != "UEL")
                btTinhThuLao.Active["TruyCap"] = true;
            else
                btTinhThuLao.Active["TruyCap"] = false;
        }

        private void btTinhThuLao_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = View.ObjectSpace;
            HopDong_ThanhLy_ThinhGiang obj = View.CurrentObject as HopDong_ThanhLy_ThinhGiang;
            if (obj != null)
            {
                foreach (HopDong_ThanhLy_ThinhGiang item in View.SelectedObjects)
                {
                    DevExpress.ExpressApp.DC.ITypeInfo type;
                    //Bắt đầu xuất report
                    int OIDReport = 20;

                    type = ObjectSpace.TypesInfo.FindTypeInfo("PSC_HRM.Module.PMS.BaoCao.Report_PMS_HopDongGiangDay");
                    if (type != null)
                    {
                        _obs = Application.CreateObjectSpace();
                        StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type.Type);
                        if (obj != null)
                        {

                            HRMReport report = _obs.FindObject<HRMReport>(CriteriaOperator.Parse("Oid =?", OIDReport));
                            if (report != null)
                            {
                                //Truyền parameter
                                ((Report_PMS_HopDongGiangDay)rpt).QuanLy = _obs.GetObjectByKey<HopDong_ThanhLy_ThinhGiang>(item.Oid);
                                //
                                StoreProcedureReport.Param = rpt;
                                Frame.GetController<ReportServiceController>().ShowPreview(report);
                            }
                        }
                    }
                }
            }
        }
    }
}