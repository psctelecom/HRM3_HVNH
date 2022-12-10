using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;

namespace PSC_HRM.Module.HopDong
{
    [DefaultClassOptions]
    [ImageName("BO_HopDong")]
    [DefaultProperty("NamHoc")]
    [ModelDefault("Caption", "Quản lý hợp đồng thỉnh giảng")]
    //[Appearance("QuanLyHopDongThinhGiang", TargetItems = "NamHoc", Enabled = false, Criteria = "NamHoc is not null")]
    [RuleCombinationOfPropertiesIsUnique("QuanLyHopDongThinhGiang", DefaultContexts.Save, "ThongTinTruong;NamHoc;HocKy")]
    public class QuanLyHopDongThinhGiang : BaoMatBaseObject
    {
        private HocKy _HocKy;
        private NamHoc _NamHoc;

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
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
                if (!IsLoading && value != null)
                {
                    HocKy = null;
                }
            }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NamHoc.ListHocKy")]
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
        [ModelDefault("Caption", "Danh sách hợp đồng")]
        [Association("QuanLyHopDongThinhGiang-ListHopDong")]
        public XPCollection<HopDong> ListHopDong
        {
            get
            {
                return GetCollection<HopDong>("ListHopDong");
            }
        }

        public QuanLyHopDongThinhGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
    }

}
