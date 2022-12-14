using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThoiViec
{
    [DefaultClassOptions]
    [ImageName("BO_NghiHuu")]
    [ModelDefault("Caption", "Quản lý thôi việc")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;NamHoc")]
    //[Appearance("QuanLyThoiViec", TargetItems = "NamHoc", Enabled = false, Criteria = "NamHoc is not null")]
    public class QuanLyThoiViec : BaoMatBaseObject
    {
        // Fields...
        private NamHoc _NamHoc;

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Năm học")]
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
        [Association("QuanLyThoiViec-ListChiTietThoiViec")]
        [ModelDefault("Caption", "Danh sách cán bộ đã nghỉ việc")]
        public XPCollection<ChiTietThoiViec> ListChiTietThoiViec
        {
            get
            {
                return GetCollection<ChiTietThoiViec>("ListChiTietThoiViec");
            }
        }

        public QuanLyThoiViec(Session session)
            : base(session) 
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
    }

}
