using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.NangNgach
{
    [DefaultProperty("ThangText")]
    [ImageName("BO_ChuyenNgach")]
    [ModelDefault("Caption", "Đề nghị nâng ngạch")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyNangNgach;Thang")]
    public class DeNghiNangNgach : BaseObject
    {
        // Fields...
        private DateTime _Thang;
        private QuanLyNangNgach _QuanLyNangNgach;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý nâng ngạch")]
        [Association("QuanLyNangNgach-ListDeNghiNangNgach")]
        public QuanLyNangNgach QuanLyNangNgach
        {
            get
            {
                return _QuanLyNangNgach;
            }
            set
            {
                SetPropertyValue("QuanLyNangNgach", ref _QuanLyNangNgach, value);
            }
        }

        [ModelDefault("Caption", "Tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        public DateTime Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.SetTime(SetTimeEnum.StartMonth);
                ThangText = value.ToString("MM/yyyy");
                SetPropertyValue("Thang", ref _Thang, value);
            }
        }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Tháng")]
        public string ThangText { get; set; }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("DeNghiNangNgach-ListChiTietDeNghiNangNgach")]
        public XPCollection<ChiTietDeNghiNangNgach> ListChiTietDeNghiNangNgach
        {
            get
            {
                return GetCollection<ChiTietDeNghiNangNgach>("ListChiTietDeNghiNangNgach");
            }
        }

        public DeNghiNangNgach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Thang = HamDungChung.GetServerTime();
        }
    }

}
