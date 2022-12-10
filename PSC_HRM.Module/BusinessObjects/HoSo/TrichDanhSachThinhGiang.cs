using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.HoSo
{
    [NonPersistent]
    [ImageName("BO_Extract")]
    [ModelDefault("Caption", "Trích danh sách thỉnh giảng")]
    public class TrichDanhSachThinhGiang : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách giảng viên thỉnh giảng")]
        public XPCollection<ChiTietTrichDanhSachThinhGiang> ListChiTietTrichDanhSachThinhGiang { get; set; }

        public TrichDanhSachThinhGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ListChiTietTrichDanhSachThinhGiang = new XPCollection<ChiTietTrichDanhSachThinhGiang>(Session, false);
        }
    }

}
