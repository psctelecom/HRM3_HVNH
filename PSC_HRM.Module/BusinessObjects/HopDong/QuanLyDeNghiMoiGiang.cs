using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module;

namespace PSC_HRM.Module.HopDong
{
    [DefaultClassOptions]
    [ImageName("BO_Contract")]
    [DefaultProperty("NamHoc")]
    [ModelDefault("Caption", "Quản lý đề nghị mời giảng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;NamHoc")]
    public class QuanLyDeNghiMoiGiang : BaoMatBaseObject
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public HocKy HocKy
        {
            get
            {
                return _HocKy;
            }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách mời giảng")]
        [Association("QuanLyDeNghiMoiGiang-ListDeNghiMoiGiang")]
        public XPCollection<DeNghiMoiGiang> ListDeNghiMoiGiang
        {
            get
            {
                return GetCollection<DeNghiMoiGiang>("ListDeNghiMoiGiang");
            }
        }
        public QuanLyDeNghiMoiGiang(Session session) : base(session) { }

        protected override void OnLoaded()
        {
            base.OnLoaded();
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
    }

}
