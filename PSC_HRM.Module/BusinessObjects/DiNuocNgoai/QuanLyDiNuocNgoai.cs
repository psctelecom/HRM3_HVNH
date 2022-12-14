using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DiNuocNgoai
{
    [DefaultClassOptions]
    [DefaultProperty("NamHoc")]
    [ImageName("BO_QuanLyDiNuocNgoai")]
    [ModelDefault("Caption", "Quản lý đi nước ngoài")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;NamHoc")]
    //[Appearance("QuanLyDiNuocNgoai", TargetItems = "NamHoc", Enabled = false, Criteria = "NamHoc is not null")]
    public class QuanLyDiNuocNgoai : BaoMatBaseObject
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
        [ModelDefault("Caption", "Đăng ký đi nước ngoài")]
        [Association("QuanLyDiNuocNgoai-ListDangKyDiNuocNgoai")]
        public XPCollection<DangKyDiNuocNgoai> ListDangKyDiNuocNgoai
        {
            get
            {
                return GetCollection<DangKyDiNuocNgoai>("ListDangKyDiNuocNgoai");
            }
        }

        public QuanLyDiNuocNgoai(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
    }

}
