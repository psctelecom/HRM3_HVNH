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
    [ModelDefault("Caption", "Chi tiết nhóm tiêu chí KPI")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "NhomTieuChiKPI;TieuChiDanhGia")]
    public class ChiTietNhomTieuChiKPI : BaseObject
    {
        private TieuChiDanhGia _TieuChiDanhGia;
        private decimal _TrongSo;
        private NhomTieuChiKPI _NhomTieuChiKPI;
        private decimal _TrongSoCu = 0;
        private decimal _TrongSoMoi = 0;
        
        [ModelDefault("Caption", "Tiêu chí đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public TieuChiDanhGia TieuChiDanhGia
        {
            get
            {
                return _TieuChiDanhGia;
            }
            set
            {
                SetPropertyValue("TieuChiDanhGia", ref _TieuChiDanhGia, value);
            }
        }

        [ModelDefault("Caption", "Trọng số")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleRange(1,100)]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal TrongSo
        {
            get
            {
                return _TrongSo;
            }
            set
            {
                SetPropertyValue("TrongSo", ref _TrongSo, value);
                if (!IsLoading)
                {
                }
            }
        }

        [Browsable(false)]
        [Association("NhomTieuChiKPI-ListChiTietNhomTieuChiKPI")]
        public NhomTieuChiKPI NhomTieuChiKPI
        {
            get
            {
                return _NhomTieuChiKPI;
            }
            set
            {
                SetPropertyValue("NhomTieuChiKPI", ref _NhomTieuChiKPI, value);
            }
        }

        public ChiTietNhomTieuChiKPI(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (propertyName == "TrongSo")
            {
                _TrongSoCu = Convert.ToDecimal(oldValue);
                _TrongSoMoi = Convert.ToDecimal(newValue);
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (NhomTieuChiKPI != null)
            {
                if (NhomTieuChiKPI.TongTrongSo > 0)
                    NhomTieuChiKPI.TongTrongSo -= _TrongSoCu;
                NhomTieuChiKPI.TongTrongSo += _TrongSoMoi;
            }
        }
    }

}
