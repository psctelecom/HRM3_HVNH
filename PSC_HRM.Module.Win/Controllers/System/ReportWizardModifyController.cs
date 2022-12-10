using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Reports.Win;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.Win.Common;
using DevExpress.ExpressApp.Reports;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class ReportWizardModifyController : WindowController
    {
        private WinReportServiceController reportServiceController;

        protected override void OnActivated()
        {
            base.OnActivated();

            reportServiceController = Frame.GetController<WinReportServiceController>();
            reportServiceController.NewXafReportWizardShowing += reportServiceController_NewXafReportWizardShowing;
        }

        protected override void OnDeactivated()
        {
            reportServiceController.NewXafReportWizardShowing -= reportServiceController_NewXafReportWizardShowing;
            reportServiceController = null;

            base.OnDeactivated();
        }

        void reportServiceController_NewXafReportWizardShowing(object sender, NewXafReportWizardShowingEventArgs e)
        {
            if (!e.ReportDataType.Equals(typeof(HRMReport)))
                return;
            CustomReportWizardParameters newReportParamsObject = new CustomReportWizardParameters(e.WizardParameters.Report, e.WizardParameters.ReportData);
            if (!String.IsNullOrEmpty(Window.View.Id))
            {
                int start = Window.View.Id.IndexOf("_") + 1;
                int end = Window.View.Id.LastIndexOf("_");
                string id = Window.View.Id.Substring(start, end - start);
                Guid oid;
                if (Guid.TryParse(id, out oid))
                    newReportParamsObject.Group = Window.View.ObjectSpace.GetObjectByKey<GroupReport>(oid);
            }
            e.WizardParameters = newReportParamsObject;           
        }
    }
}
