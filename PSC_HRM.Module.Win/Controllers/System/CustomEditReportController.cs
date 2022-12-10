using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.Report;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class CustomEditReportController : ViewController
    {
        public CustomEditReportController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            HRMReport obj = View.CurrentObject as HRMReport;
            if (obj != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                obj = obs.GetObjectByKey<HRMReport>(obj.Oid);
                e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, obj);
                e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                //e.ShowViewParameters.CreateAllControllers = true;
            }
        }
    }
}
