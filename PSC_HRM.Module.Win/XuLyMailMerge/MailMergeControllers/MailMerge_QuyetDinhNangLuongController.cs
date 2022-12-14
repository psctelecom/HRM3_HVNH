using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Reports;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhNangLuongController : ViewController
    {
        public MailMerge_QuyetDinhNangLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhNangLuong>();
            QuyetDinhNangLuong qd;
            ITypeInfo type;
            IObjectSpace obs = Application.CreateObjectSpace();

            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhNangLuong)item;
                if (qd != null)
                    list.Add(qd);

                if (TruongConfig.MaTruong == "NEU" && qd.ListChiTietQuyetDinhNangLuong.Count > 1)
                {
                    //In report danh sách kèm theo
                    type = obs.TypesInfo.FindTypeInfo("PSC_HRM.Module.Report.Report_NangLuong_DanhSachNangLuong");
                    if (type != null)
                    {
                        StoreProcedureReport rpt = (StoreProcedureReport)obs.CreateObject(type.Type);
                        if (rpt != null)
                        {
                            HRMReport report = obs.FindObject<HRMReport>(CriteriaOperator.Parse("Oid =?", "1"));
                            if (report != null)
                            {
                                //Truyền parameter
                                ((Report_NangLuong_DanhSachNangLuong)rpt).QuyetDinhNangLuong = obs.GetObjectByKey<QuyetDinhNangLuong>(qd.Oid);
                                //     
                                StoreProcedureReport.Param = rpt;
                                Frame.GetController<ReportServiceController>().ShowPreview(report);
                            }
                        }
                    }
                }
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhNangLuong>>>().Merge(obs, list);
        }
    }
}
