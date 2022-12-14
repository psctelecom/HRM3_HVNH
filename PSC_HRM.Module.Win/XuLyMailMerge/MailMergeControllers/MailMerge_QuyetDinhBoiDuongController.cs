using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.DC;
using PSC_HRM.Module.Report;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Reports;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhBoiDuongController : ViewController
    {
        public MailMerge_QuyetDinhBoiDuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ITypeInfo type;
            IObjectSpace obs = Application.CreateObjectSpace();

            var list = new List<QuyetDinhBoiDuong>();
            QuyetDinhBoiDuong quyetDinh;
            foreach (object item in View.SelectedObjects)
            {
                quyetDinh = (QuyetDinhBoiDuong)item;
                if (quyetDinh != null)
                    list.Add(quyetDinh);

                //if (TruongConfig.MaTruong == "NEU" && quyetDinh.ListChiTietBoiDuong.Count > 1)
                //{
                //    //In report danh sách kèm theo
                //    type = obs.TypesInfo.FindTypeInfo("PSC_HRM.Module.Report.Report_BoiDuong_DanhSachBoiDuongTheoQuyetDinh");
                //    if (type != null)
                //    {
                //        StoreProcedureReport rpt = (StoreProcedureReport)obs.CreateObject(type.Type);
                //        if (rpt != null)
                //        {
                //            HRMReport report = obs.FindObject<HRMReport>(CriteriaOperator.Parse("Oid =?", "2514"));
                //            if (report != null)
                //            {
                //                //Truyền parameter
                //                ((Report_BoiDuong_DanhSachBoiDuongTheoQuyetDinh)rpt).QuyetDinhBoiDuong = obs.GetObjectByKey<QuyetDinhBoiDuong>(quyetDinh.Oid);
                //                //     
                //                StoreProcedureReport.Param = rpt;
                //                Frame.GetController<ReportServiceController>().ShowPreview(report);
                //            }
                //        }
                //    }
                //}
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhBoiDuong>>>().Merge(obs, list);
        }
    }
}
