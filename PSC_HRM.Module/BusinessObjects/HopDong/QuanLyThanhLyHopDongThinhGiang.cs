using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.HopDong
{
    [DefaultClassOptions]
    [ImageName("BO_HopDong")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Quản lý thanh lý hợp đồng thỉnh giảng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;NamHoc;HocKy")]
    //[Appearance("QuanLyThanhLyHopDongThinhGiang", TargetItems = "NamHoc", Enabled = false, Criteria = "NamHoc is not null")]
    public class QuanLyThanhLyHopDongThinhGiang : BaoMatBaseObject
    {
        // Fields...
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

        [NonPersistent]
        [Browsable(false)]
        public string Caption
        {
            get
            {
                if (NamHoc != null && HocKy != null)
                    return String.Format("{0} {1}", HocKy.TenHocKy, NamHoc.TenNamHoc);
                return "";
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Hợp đồng thỉnh giảng đã thanh lý")]
        [Association("QuanLyThanhLyHopDongThinhGiang-ListChiTietThanhLyHopDongThinhGiang")]
        public XPCollection<ChiTietThanhLyHopDongThinhGiang> ListChiTietThanhLyHopDongThinhGiang
        {
            get
            {
                return GetCollection<ChiTietThanhLyHopDongThinhGiang>("ListChiTietThanhLyHopDongThinhGiang");
            }
        }

        public QuanLyThanhLyHopDongThinhGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            HocKy = HamDungChung.GetCurrentHocKy(Session);
        }
    }

}
