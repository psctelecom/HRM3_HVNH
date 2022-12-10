using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.Report;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Reports.Win;
using System.Configuration;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class CustomCreateReportController : ViewController
    {
        private CreateReport report;
        private IObjectSpace obs;
        private StoreProcedureReport obj;

        public CustomCreateReportController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            report = obs.CreateObject<CreateReport>();

            e.View = Application.CreateDetailView(obs, report);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            obj = (StoreProcedureReport)obs.CreateObject(report.DataType);
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, obj);
            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            DevExpress.ExpressApp.SystemModule.DialogController ct = new DevExpress.ExpressApp.SystemModule.DialogController();
            ct.AcceptAction.Caption = "OK";
            e.ShowViewParameters.Controllers.Add(ct);
            ct.AcceptAction.Execute += AcceptAction_Execute;        
        }

        private void AcceptAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                HRMReport rpt = new HRMReport(uow, report.DataType);
                rpt.ReportName = report.TenBaoCao;
                rpt.NhomBaoCao = uow.GetObjectByKey<GroupReport>(report.NhomBaoCao.Oid);

                CustomXafReport custom = new CustomXafReport();
                StoreProcedureReport.Param = obj;
                custom.ReportName = report.TenBaoCao;
                custom.DataType = report.DataType;
                custom.TargetType = report.TargetType;

                rpt.SaveReport(custom);
                uow.CommitChanges();

                Frame.GetController<WinReportServiceController>().ShowDesigner(rpt);

                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            }
        }

        private void CustomCreateReportController_Activated(object sender, EventArgs e)
        {
            string value = ConfigurationManager.AppSettings["EnableCreateReport"];
            popupWindowShowAction1.Active["TruyCap"] = value == "True";
        }
    }
}
