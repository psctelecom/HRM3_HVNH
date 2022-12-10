using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.NangThamNien
{
    [DefaultProperty("ThangText")]
    [ImageName("BO_NangThamNien")]
    [ModelDefault("Caption", "Đề nghị nâng phụ cấp thâm niên")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyNangPhuCapThamNien;Thang")]
    public class DeNghiNangPhuCapThamNien : BaseObject
    {
        private DateTime _Thang;
        private QuanLyNangPhuCapThamNien _QuanLyNangPhuCapThamNien;

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Quản lý nâng phụ cấp thâm niên")]
        [Association("QuanLyNangPhuCapThamNien-ListDeNghiNangPhuCapThamNien")]
        public QuanLyNangPhuCapThamNien QuanLyNangPhuCapThamNien
        {
            get
            {
                return _QuanLyNangPhuCapThamNien;
            }
            set
            {
                SetPropertyValue("QuanLyNangPhuCapThamNien", ref _QuanLyNangPhuCapThamNien, value);
            }
        }
        
        [ModelDefault("Caption", "Tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        [Association("DeNghiNangPhuCapThamNien-ListChiTietDeNghiNangPhuCapThamNien")]
        public XPCollection<ChiTietDeNghiNangPhuCapThamNien> ListChiTietDeNghiNangPhuCapThamNien
        {
            get
            {
                return GetCollection<ChiTietDeNghiNangPhuCapThamNien>("ListChiTietDeNghiNangPhuCapThamNien");
            }
        }

        public DeNghiNangPhuCapThamNien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Thang = HamDungChung.GetServerTime();
        }
    }

}
