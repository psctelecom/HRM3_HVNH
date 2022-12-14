using System;
using System.Collections.Generic;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;


namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Export mẫu báo cáo")]
    public class ExportReport : BaseObject
    {
        [ModelDefault("AllowEdit", "True")]
        [ModelDefault("Caption", "Danh sách báo cáo")]
        public XPCollection<ChonBaoCao> BaoCaoList { get; set; }

        public ExportReport(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            BaoCaoList = new XPCollection<ChonBaoCao>(Session, false);

            using (XPCollection<HRMReport> reports = new XPCollection<HRMReport>(Session))
            {
                foreach (HRMReport item in reports)
                {
                    BaoCaoList.Add(new ChonBaoCao(Session) { BaoCao = item });
                }
            }
        }

    }
}
