using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using PSC_HRM.Module.Report;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Reports;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Reports.Win;


namespace PSC_HRM.Module.Win.Controllers
{
    public partial class ShowReportOnToolbarController : ViewController
    {
        private XPCollection<HRMReport> _reportList;
        private HRMReport report;
        private IObjectSpace obs;

        public ShowReportOnToolbarController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ShowReportOnToolbarController_Activated(object sender, EventArgs e)
        {
            if (View.ObjectTypeInfo != null)
            {
                Type type = View.ObjectTypeInfo.Type;
                if (type != null && HamDungChung.GetPhanQuyenBaoCao() != null)
                {
                    //
                    SortProperty sort = new SortProperty("ReportName", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    //
                    if (!HamDungChung.CheckAdministrator())
                    {
                        List<Int32> phanQuyenBaoCaoList = new List<int>();
                        string[] quyenList = HamDungChung.GetPhanQuyenBaoCao().Quyen.Split(';');
                        foreach (var item in quyenList)
                        {
                            if (!string.IsNullOrEmpty(item))
                                phanQuyenBaoCaoList.Add(Convert.ToInt32(item));
                        }
                        //
                        GroupOperator go = new GroupOperator(GroupOperatorType.And);
                        CriteriaOperator filter = CriteriaOperator.Parse("TargetTypeName=? and MaTruong=?", type.FullName, TruongConfig.MaTruong);
                        go.Operands.Add(filter);
                        go.Operands.Add(new InOperator("Oid", phanQuyenBaoCaoList));            
                        //
                        _reportList = new XPCollection<HRMReport>(((XPObjectSpace)View.ObjectSpace).Session, go, sort);
                        //
                    }
                    else
                    {
                        CriteriaOperator filter = CriteriaOperator.Parse("TargetTypeName=? and MaTruong=?", type.FullName, TruongConfig.MaTruong);
                        _reportList = new XPCollection<HRMReport>(((XPObjectSpace)View.ObjectSpace).Session, filter,sort);
                    }

                    if (_reportList.Count > 0)
                        singleChoiceAction1.Active["ByMainForm"] = true;
                    else
                        singleChoiceAction1.Active["ByMainForm"] = false;
                }
                else
                {
                    singleChoiceAction1.Active["ByMainForm"] = false;
                }
            }
        }

        private void ShowReportOnToolbarController_ViewControlsCreated(object sender, EventArgs e)
        {
            if (_reportList != null)
            {
                singleChoiceAction1.Items.Clear();
                ChoiceActionItem subItem;
                foreach (HRMReport item in _reportList)
                {
                    subItem = new ChoiceActionItem();
                    subItem.Id = item.Oid.ToString();
                    subItem.Caption = item.ReportName;
                    subItem.ImageName = "Action_Report_Object_Inplace_Preview";
                    singleChoiceAction1.Items.Add(subItem);
                }
            }
        }

        private void singleChoiceAction1_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            foreach (HRMReport item in _reportList)
            {
                if (item.Oid.ToString() == e.SelectedChoiceActionItem.Id)
                {
                    //Xu ly execute report o day   
                    DevExpress.ExpressApp.DC.ITypeInfo type = ObjectSpace.TypesInfo.FindTypeInfo(item.DataTypeName);
                    if (type != null)
                    {
                        obs = Application.CreateObjectSpace();
                        StoreProcedureReport obj = (StoreProcedureReport)obs.CreateObject(type.Type);
                        if (obj != null)
                        {
                            report = item;
                            StoreProcedureReport.Param = obj;
                            e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, obj);
                            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                            var ctrl = new DevExpress.ExpressApp.SystemModule.DialogController();
                            e.ShowViewParameters.Controllers.Add(ctrl);
                            ctrl.AcceptAction.Execute += AcceptAction_Execute;
                            ctrl.AcceptAction.Caption = "Đồng ý";
                        }
                        break;
                    }
                }
            }
        }

        private void AcceptAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (report != null)
            {
                Frame.GetController<ReportServiceController>().ShowPreview(report);
            }
        }
    }
}
