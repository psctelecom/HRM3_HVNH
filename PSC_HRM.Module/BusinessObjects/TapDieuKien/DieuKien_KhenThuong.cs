using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.KhenThuong;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.TapDieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Khen thưởng")]
    public class DieuKien_KhenThuong : BaseObject
    {
        [ModelDefault("Caption", "Được khen thưởng")]
        public bool DuocKhenThuong { get; set; }
        
        [ModelDefault("Caption", "Danh hiệu")]
        public DanhHieuKhenThuong DanhHieuKhenThuong { get; set; }

        public DieuKien_KhenThuong(Session session) : base(session) { }
    }

}
