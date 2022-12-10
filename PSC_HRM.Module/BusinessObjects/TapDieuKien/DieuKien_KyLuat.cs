using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.TapDieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Kỷ luật")]
    public class DieuKien_KyLuat : BaseObject
    {
        [ModelDefault("Caption", "Bị kỷ luật")]
        public bool BiKyLuat { get; set; }

        [ModelDefault("Caption", "Hình thức kỷ luật")]
        public HinhThucKyLuat HinhThucKyLuat { get; set; }

        public DieuKien_KyLuat(Session session) : base(session) { }
    }

}
