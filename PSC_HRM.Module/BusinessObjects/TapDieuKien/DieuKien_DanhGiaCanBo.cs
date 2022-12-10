using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoHiem;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.TapDieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Đánh giá cán bộ")]
    public class DieuKien_DanhGiaCanBo : BaseObject
    {
        [ModelDefault("Caption", "Đánh giá")]
        public string Danhgia { get; set; }

        public DieuKien_DanhGiaCanBo(Session session) : base(session) { }
    }

}
