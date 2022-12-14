using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DanhGia
{
    [ImageName("BO_DanhGiaCanBo")]
    [ModelDefault("Caption", "Đánh giá cán bộ cuối năm")]
    [RuleCombinationOfPropertiesIsUnique("DanhGiaCanBoCuoiNam.Unique", DefaultContexts.Save, "Nam")]
    public class DanhGiaCanBoCuoiNam : BaseObject
    {
        // Fields...
        private int _Nam;
        private bool _ChotDanhGia;

        [ModelDefault("Caption", "Năm đánh giá")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết đánh giá cán bộ cuối năm lần 1")]
        [Association("DanhGiaCanBoCuoiNam-ListChiTietDanhGiaCanBoCuoiNamLan1")]
        public XPCollection<ChiTietDanhGiaCanBoCuoiNamLan1> ListChiTietDanhGiaCanBoCuoiNamLan1
        {
            get
            {
                return GetCollection<ChiTietDanhGiaCanBoCuoiNamLan1>("ListChiTietDanhGiaCanBoCuoiNamLan1");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết đánh giá cán bộ cuối năm lần 2")]
        [Association("DanhGiaCanBoCuoiNam-ListChiTietDanhGiaCanBoCuoiNamLan2")]
        public XPCollection<ChiTietDanhGiaCanBoCuoiNamLan2> ListChiTietDanhGiaCanBoCuoiNamLan2
        {
            get
            {
                return GetCollection<ChiTietDanhGiaCanBoCuoiNamLan2>("ListChiTietDanhGiaCanBoCuoiNamLan2");
            }
        }

        public DanhGiaCanBoCuoiNam(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Nam = HamDungChung.GetServerTime().Year;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
        }
    }

}
