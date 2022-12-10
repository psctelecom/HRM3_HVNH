using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Reports.Win;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class DisableEditReportController : WindowController
    {
        private EditReportController report;

        public DisableEditReportController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();

            report = Frame.GetController<EditReportController>();
            if (report != null)
            {
                report.Active.Clear();
                report.Active["TruyCap"] = false;
            }
        }
    }
}
