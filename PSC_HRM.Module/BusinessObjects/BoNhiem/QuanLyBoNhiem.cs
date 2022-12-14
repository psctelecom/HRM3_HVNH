using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BoNhiem
{
    [DefaultClassOptions]
    [DefaultProperty("NamHoc")]
    [ImageName("BO_QuanLyKhenThuong")]
    [ModelDefault("Caption", "Quản lý bổ nhiệm")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;NamHoc;Dot")]
    //[Appearance("QuanLyBoNhiem", TargetItems = "NamHoc", Enabled = false, Criteria = "NamHoc is not null")]
    public class QuanLyBoNhiem : BaoMatBaseObject
    {
        private int _Dot = 1;
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
                SetPropertyValue("NamKhenThuong", ref _NamHoc, value);
            }
        }

        [ModelDefault("Caption", "Đợt")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Dot
        {
            get
            {
                return _Dot;
            }
            set
            {
                SetPropertyValue("Dot", ref _Dot, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách đề nghị bổ nhiệm")]
        [Association("QuanLyBoNhiem-ListDeNghiBoNhiem")]
        public XPCollection<DeNghiBoNhiem> ListDeNghiBoNhiem
        {
            get
            {
                return GetCollection<DeNghiBoNhiem>("ListDeNghiBoNhiem");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách bổ nhiệm")]
        [Association("QuanLyBoNhiem-ListChiTietBoNhiem")]
        public XPCollection<ChiTietBoNhiem> ListChiTietBoNhiem
        {
            get
            {
                return GetCollection<ChiTietBoNhiem>("ListChiTietBoNhiem");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách miễn nhiệm")]
        [Association("QuanLyBoNhiem-ListChiTietMienNhiem")]
        public XPCollection<ChiTietMienNhiem> ListChiTietMienNhiem
        {
            get
            {
                return GetCollection<ChiTietMienNhiem>("ListChiTietMienNhiem");
            }
        }

        public QuanLyBoNhiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
    }

}
