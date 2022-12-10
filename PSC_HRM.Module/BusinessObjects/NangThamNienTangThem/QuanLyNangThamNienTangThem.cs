using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.NangThamNienTangThem
{
    [DefaultClassOptions]
    [DefaultProperty("NamHienThi")]
    [ImageName("BO_NangThamNien")]
    [ModelDefault("Caption", "Quản lý nâng thâm niên tăng thêm")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "Nam;ThongTinTruong")]
    public class QuanLyNangThamNienTangThem : BaoMatBaseObject
    {
        // Fields...
        private int _Nam;

        [ModelDefault("Caption", "Năm")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
                if (IsLoading)
                {
                    NamHienThi = Convert.ToString(_Nam);
                }
            }
        }

        [NonPersistent]
        [Browsable(false)]
        [ModelDefault("Caption", "Năm")]
        public string NamHienThi { get; set; }
       
        [Aggregated]
        [ModelDefault("Caption", "Danh sách đề nghị nâng thâm niên tăng thêm")]
        [Association("QuanLyNangThamNienTangThem-ListDeNghiNangThamNienTangThem")]
        public XPCollection<DeNghiNangThamNienTangThem> ListDeNghiNangThamNienTangThem
        {
            get
            {
                return GetCollection<DeNghiNangThamNienTangThem>("ListDeNghiNangThamNienTangThem");
            }
        }

        public QuanLyNangThamNienTangThem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Nam = HamDungChung.GetServerTime().Year;
        }
    }

}
