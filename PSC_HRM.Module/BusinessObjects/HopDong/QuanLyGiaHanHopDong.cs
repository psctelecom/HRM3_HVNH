using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.HopDong
{
    [DefaultClassOptions]
    [ImageName("BO_HopDong")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Quản lý gia hạn hợp đồng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;NamHoc")]
    //[Appearance("QuanLyGiaHanHopDong", TargetItems = "NamHoc", Enabled = false, Criteria = "NamHoc is not null")]
    public class QuanLyGiaHanHopDong : BaoMatBaseObject
    {
        private NamHoc _NamHoc;

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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuanLyGiaHanHopDong-ListChiTietGiaHanHopDong")]
        public XPCollection<ChiTietGiaHanHopDong> ListChiTietGiaHanHopDong
        {
            get
            {
                return GetCollection<ChiTietGiaHanHopDong>("ListChiTietGiaHanHopDong");
            }
        }

        public QuanLyGiaHanHopDong(Session session) : base(session) { }
    }

}
