using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.TuyenDung
{
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [DefaultProperty("ViTriTuyenDung")]
    [ModelDefault("Caption", "Chi tiết tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyTuyenDung;ViTriTuyenDung")]
    public class ChiTietTuyenDung : BaseObject
    {
        // Fields...
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private ViTriTuyenDung _ViTriTuyenDung;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        [Association("QuanLyTuyenDung-ListChiTietTuyenDung")]
        public QuanLyTuyenDung QuanLyTuyenDung
        {
            get
            {
                return _QuanLyTuyenDung;
            }
            set
            {
                SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "Vị trí tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QuanLyTuyenDung.ListViTriTuyenDung", DataSourcePropertyIsNullMode.SelectNothing)]
        public ViTriTuyenDung ViTriTuyenDung
        {
            get
            {
                return _ViTriTuyenDung;
            }
            set
            {
                SetPropertyValue("ViTriTuyenDung", ref _ViTriTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "Vòng tuyển dụng")]
        [Association("ChiTietTuyenDung-ListBuocTuyenDung")]
        public XPCollection<BuocTuyenDung> ListBuocTuyenDung
        {
            get
            {
                return GetCollection<BuocTuyenDung>("ListBuocTuyenDung");
            }
        }

        [ModelDefault("Caption", "Chi tiết vòng tuyển dụng")]
        [Association("ChiTietTuyenDung-ListVongTuyenDung")]
        public XPCollection<VongTuyenDung> ListVongTuyenDung
        {
            get
            {
                return GetCollection<VongTuyenDung>("ListVongTuyenDung");
            }
        }

        [ModelDefault("Caption", "Danh sách thi")]
        [Association("ChiTietTuyenDung-ListDanhSachThi")]
        public XPCollection<DanhSachThi> ListDanhSachThi
        {
            get
            {
                return GetCollection<DanhSachThi>("ListDanhSachThi");
            }
        }

        public ChiTietTuyenDung(Session session) : base(session) { }

        protected override void OnDeleting()
        {
            Session.Delete(ListBuocTuyenDung);
            Session.Save(ListBuocTuyenDung);
            Session.Delete(ListVongTuyenDung);
            Session.Save(ListVongTuyenDung);
            Session.Delete(ListDanhSachThi);
            Session.Save(ListDanhSachThi);

            base.OnDeleting();
        }
    }

}
