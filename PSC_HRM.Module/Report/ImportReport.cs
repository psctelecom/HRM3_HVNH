using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Import báo cáo")]
    public class ImportReport : BaseObject
    {
        // Fields...
        private bool _GhiDe;
        private GroupReport _NhomBaoCao;

        [ModelDefault("Caption", "Nhóm báo cáo")]
        public GroupReport NhomBaoCao
        {
            get
            {
                return _NhomBaoCao;
            }
            set
            {
                SetPropertyValue("NhomBaoCao", ref _NhomBaoCao, value);
            }
        }

        [ModelDefault("Caption", "Ghi đè")]
        public bool GhiDe
        {
            get
            {
                return _GhiDe;
            }
            set
            {
                SetPropertyValue("GhiDe", ref _GhiDe, value);
            }
        }

        public ImportReport(Session session) : base(session) { }
    }
}
