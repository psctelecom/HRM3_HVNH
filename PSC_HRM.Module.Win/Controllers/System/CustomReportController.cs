using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;

using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class CustomReportController : DevExpress.ExpressApp.Reports.Win.ReportsController
    {
        private SimpleActionExecuteEventArgs action;
        public CustomReportController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void ShowReportPreview(SimpleActionExecuteEventArgs args)
        {
            string s = (args.CurrentObject as HRMReport).DataTypeName;
            Type t = DevExpress.Persistent.Base.ReflectionHelper.GetType(s);
            if (t != null && t.BaseType == typeof(StoreProcedureReport))
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                object source = obs.CreateObject(t);

                args.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, source);
                args.ShowViewParameters.Context = TemplateContext.PopupWindow;
                args.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                var ctrl = new DevExpress.ExpressApp.SystemModule.DialogController();
                args.ShowViewParameters.Controllers.Add(ctrl);
                ctrl.AcceptAction.Execute += Preview_Execute;
                ctrl.CanCloseWindow = true;
                action = args;
            }
            else
                base.ShowReportPreview(args);
        }

        protected override void ShowReportDesigner(SimpleActionExecuteEventArgs args)
        {
            string s = (args.CurrentObject as HRMReport).DataTypeName;
                                                    Type t = DevExpress.Persistent.Base.ReflectionHelper.GetType(s);

            if (t != null && t.BaseType == typeof(StoreProcedureReport))
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                object source = obs.CreateObject(t);

                args.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, source);
                args.ShowViewParameters.Context = TemplateContext.PopupWindow;
                args.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                var ctrl = new DevExpress.ExpressApp.SystemModule.DialogController();
                args.ShowViewParameters.Controllers.Add(ctrl);
                ctrl.AcceptAction.Execute += Design_Execute;
                ctrl.CanCloseWindow = true;
                action = args;
            }
            else
                base.ShowReportDesigner(args);
        }    

        void Design_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            e.Action.Controller.Frame.View.ObjectSpace.CommitChanges();
            StoreProcedureReport.Param = e.CurrentObject as StoreProcedureReport;
            base.ShowReportDesigner(action);
        }

        void Preview_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            e.Action.Controller.Frame.View.ObjectSpace.CommitChanges();
            StoreProcedureReport.Param = HamDungChung.Copy<StoreProcedureReport>(((XPObjectSpace)ObjectSpace).Session, e.CurrentObject as StoreProcedureReport);

            base.ShowReportPreview(action);
        }
    }
}
