using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.NangThamNienTangThem
{
    [DefaultProperty("ThangText")]
    [ImageName("BO_NangThamNien")]
    [ModelDefault("Caption", "Đề nghị nâng thâm niên tăng thêm")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyNangThamNienTangThem;Thang")]
    public class DeNghiNangThamNienTangThem : BaseObject
    {
        private DateTime _Thang;
        private QuanLyNangThamNienTangThem _QuanLyNangThamNienTangThem;

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Quản lý nâng thâm niên tăng thêm")]
        [Association("QuanLyNangThamNienTangThem-ListDeNghiNangThamNienTangThem")]
        public QuanLyNangThamNienTangThem QuanLyNangThamNienTangThem
        {
            get
            {
                return _QuanLyNangThamNienTangThem;
            }
            set
            {
                SetPropertyValue("QuanLyNangThamNienTangThem", ref _QuanLyNangThamNienTangThem, value);
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
        [Association("DeNghiNangThamNienTangThem-ListChiTietDeNghiNangThamNienTangThem")]
        public XPCollection<ChiTietDeNghiNangThamNienTangThem> ListChiTietDeNghiNangThamNienTangThem
        {
            get
            {
                return GetCollection<ChiTietDeNghiNangThamNienTangThem>("ListChiTietDeNghiNangThamNienTangThem");
            }
        }

        public DeNghiNangThamNienTangThem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Thang = HamDungChung.GetServerTime();
        }
    }

}
