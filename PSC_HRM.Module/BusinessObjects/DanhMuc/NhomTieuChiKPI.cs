using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenTieuChiDanhGia")]
    [ModelDefault("Caption", "Nhóm tiêu chí KPI")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "BoPhan;CongViec;NgayApDung")]
    public class NhomTieuChiKPI : BaseObject
    {
        private BoPhan _BoPhan;
        private CongViec _CongViec;
        private DateTime _NgayApDung;
        private decimal _TongTrongSo;
        
        [ModelDefault("Caption", "Bộ phận")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Công việc")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CongViec CongViec
        {
            get
            {
                return _CongViec;
            }
            set
            {
                SetPropertyValue("CongViec", ref _CongViec, value);
            }
        }

        [ModelDefault("Caption", "Ngày áp dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayApDung
        {
            get
            {
                return _NgayApDung;
            }
            set
            {
                SetPropertyValue("NgayApDung", ref _NgayApDung, value);
            }
        }

        [ModelDefault("Caption", "Tổng trọng số")]
        [RuleRange(100, 100)]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal TongTrongSo
        {
            get
            {
                return _TongTrongSo;
            }
            set
            {
                SetPropertyValue("TongTrongSo", ref _TongTrongSo, value);
                if (!IsLoading)
                {
                }
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Nhóm tiêu chí KPI")]
        [Association("NhomTieuChiKPI-ListChiTietNhomTieuChiKPI")]
        public XPCollection<ChiTietNhomTieuChiKPI> ListChiTietNhomTieuChiKPI
        {
            get
            {
                return GetCollection<ChiTietNhomTieuChiKPI>("ListChiTietNhomTieuChiKPI");
            }
        }

        public NhomTieuChiKPI(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NgayApDung = HamDungChung.GetServerTime();
        }

        protected override void OnSaving()
        {
            base.OnSaving();
        }
    }

}
