using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn cán bộ")]
    public class HoSo_ChonNhanVien : BaseObject
    {
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<HoSo_NhanVienItem> ListNhanVien { get; set; }

        public HoSo_ChonNhanVien(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ListNhanVien = new XPCollection<HoSo_NhanVienItem>(Session, false);
        }

    }

}
