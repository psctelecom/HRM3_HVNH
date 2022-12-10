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

namespace PSC_HRM.Module.DanhGia
{
    [DefaultClassOptions]
    [DefaultProperty("NamHoc")]
    [ImageName("BO_BangTheoDoiViPham")]
    [ModelDefault("Caption", "Quản lý vi phạm")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;NamHoc")]
    [Appearance("QuanLyViPham", TargetItems = "NamHoc", Enabled = false, Criteria = "NamHoc is not null")]
    public class QuanLyViPham : BaoMatBaseObject
    {
        // Fields...
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

        [Aggregated]
        [ModelDefault("Caption", "Bảng theo dõi vi phạm")]
        [Association("QuanLyViPham-ListBangTheoDoiViPham")]
        public XPCollection<BangTheoDoiViPham> ListBangTheoDoiViPham
        {
            get
            {
                return GetCollection<BangTheoDoiViPham>("ListBangTheoDoiViPham");
            }
        }

        public QuanLyViPham(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
    }

}
