using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Export thông tin bảo hiểm")]
    public class BaoHiem_ExportThongTin : BaseObject
    {
        // Fields...
        private DateTime _NgayVaoCoQuan;

        [ModelDefault("Caption", "Ngày vào cơ quan")]
        public DateTime NgayVaoCoQuan
        {
            get
            {
                return _NgayVaoCoQuan;
            }
            set
            {
                SetPropertyValue("NgayVaoCoQuan", ref _NgayVaoCoQuan, value);
            }
        }

        public BaoHiem_ExportThongTin(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NgayVaoCoQuan = HamDungChung.GetServerTime();
        }

     
    }

}
