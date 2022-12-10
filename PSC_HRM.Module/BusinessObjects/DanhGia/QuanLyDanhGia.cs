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
    //xét thi đua
    [DefaultClassOptions]
    [DefaultProperty("NamHoc")]
    [ImageName("BO_DanhGiaCanBo")]
    [ModelDefault("Caption", "Quản lý đánh giá")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;NamHoc")]
    [Appearance("QuanLyDanhGia", TargetItems = "NamHoc", Enabled = false, Criteria = "NamHoc is not null")]
    public class QuanLyDanhGia : BaoMatBaseObject
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
        [ModelDefault("Caption", "Danh sách cá nhân")]
        [Association("QuanLyDanhGia-ListDanhGiaCaNhan")]
        public XPCollection<DanhGiaCaNhan> ListDanhGiaCaNhan
        {
            get
            {
                return GetCollection<DanhGiaCaNhan>("ListDanhGiaCaNhan");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách tập thể")]
        [Association("QuanLyDanhGia-ListDanhGiaTapThe")]
        public XPCollection<DanhGiaTapThe> ListDanhGiaTapThe
        {
            get
            {
                return GetCollection<DanhGiaTapThe>("ListDanhGiaTapThe");
            }
        }

        public QuanLyDanhGia(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
    }

}
