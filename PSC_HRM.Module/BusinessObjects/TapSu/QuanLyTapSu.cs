using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module;

namespace PSC_HRM.Module.TapSu
{
    [DefaultClassOptions]
    [ImageName("BO_NghiHuu")]
    [DefaultProperty("NamHoc")]
    [ModelDefault("Caption", "Quản lý tập sự")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;NamHoc")]
    public class QuanLyTapSu : BaoMatBaseObject
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
        [ModelDefault("Caption", "Danh sách đề nghị bổ nhiệm ngạch")]
        [Association("QuanLyTapSu-ListDeNghiBoNhiemNgach")]
        public XPCollection<DeNghiBoNhiemNgach> ListDeNghiBoNhiemNgach
        {
            get
            {
                return GetCollection<DeNghiBoNhiemNgach>("ListDeNghiBoNhiemNgach");
            }
        }

        public QuanLyTapSu(Session session)
            : base(session) 
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
    }

}
