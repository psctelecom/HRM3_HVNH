using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.Reports;
using DevExpress.XtraReports;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;

namespace PSC_HRM.Module.Report
{
    [RootClass]
    public class CustomXafReport : XafReport
    {
        public GroupReport GroupReport { get; set; }
        public Type TargetType { get; set; }

        public CustomXafReport()
        {
            PaperKind = System.Drawing.Printing.PaperKind.A4;
        }

        //protected override void InitializeDataSource()
        //{
        //    if (DataType != null && DataType.BaseType == typeof(StoreProcedureReport))
        //        DataSource = GetDataSource();
        //    else
        //        base.InitializeDataSource();
        //}

        protected override void RefreshDataSourceForPrint()
        {
            if (DataType != null && DataType.BaseType == typeof(StoreProcedureReport))
                return;
            base.RefreshDataSourceForPrint();
        }

        //private List<StoreProcedureReport> GetDataSource()
        //{
        //    List<StoreProcedureReport> dataSource = new List<StoreProcedureReport>();
        //    StoreProcedureReport currentDataSource = StoreProcedureReport.Param;
        //    currentDataSource.FillDataSource();
        //    dataSource.Add(currentDataSource);
        //    return dataSource;
        //}
    }
}
