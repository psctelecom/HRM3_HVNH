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
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.DanhGiaKPI
{
    //xét thi đua
    [DefaultClassOptions]
    [DefaultProperty("NamHoc")]
    [ImageName("BO_DanhGiaCanBo")]
    [ModelDefault("Caption", "Chi tiết đánh giá KPI")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "TieuChiDanhGia;DanhGiaKPI")]
    [Appearance("ChiTietDanhGiaKPI.ChotDanhGia", TargetItems = "*", Enabled = false, Criteria = "DanhGiaKPI.TinhTrangDuyet=2")]
    public class ChiTietDanhGiaKPI : BaseObject
    {
        // Fields...
        private TieuChiDanhGia _TieuChiDanhGia;
        private decimal _TrongSo;
        private decimal _KetQua;
        private int _CapDo;
        private decimal _PhanTramHoanThanh;
        private DanhGiaKPI _DanhGiaKPI;
        private decimal _KetQuaCu = 0;
        private decimal _KetQuaMoi = 0;
        private string _DonViTinh;

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
            }
        }
        
        [ImmediatePostData]
        [ModelDefault("Caption", "Kết quả")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal KetQua
        {
            get
            {
                return _KetQua;
            }
            set
            {
                SetPropertyValue("KetQua", ref _KetQua, value);
                //tính ra kết quả đánh giá
                if (!IsLoading)
                { 
                }
            }
        }

        [ModelDefault("Caption", "Cấp độ")]
        public int CapDo
        {
            get
            {
                return _CapDo;
            }
            set
            {
                SetPropertyValue("CapDo", ref _CapDo, value);
            }
        }

        [ModelDefault("Caption", "% hoàn thành")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal PhanTramHoanThanh
        {
            get
            {
                return _PhanTramHoanThanh;
            }
            set
            {
                SetPropertyValue("PhanTramHoanThanh", ref _PhanTramHoanThanh, value);
            }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Đơn vị tính")]
        public string DonViTinh
        {
            get
            {
                return _DonViTinh;
            }
            set
            {
                SetPropertyValue("DonViTinh", ref _DonViTinh, value);
            }
        }

        [Browsable(false)]
        [Association("DanhGiaKPI-ListChiTietDanhGiaKPI")]
        public DanhGiaKPI DanhGiaKPI
        {
            get
            {
                return _DanhGiaKPI;
            }
            set
            {
                SetPropertyValue("DanhGiaKPI", ref _DanhGiaKPI, value);
            }
        }

        public ChiTietDanhGiaKPI(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            DonViTinh = TieuChiDanhGia.TyLeDanhGia.DonViTinh.TenDonViTinh;
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (propertyName == "PhanTramHoanThanh")
            {
                _KetQuaCu = Convert.ToDecimal(oldValue);
                _KetQuaMoi = Convert.ToDecimal(newValue);
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            ChiTietTyLeDanhGia ctTyLe;
            if (KetQua > 0)
                ctTyLe = Session.FindObject<ChiTietTyLeDanhGia>(CriteriaOperator.Parse("TyLeDanhGia = ? and TuGiaTri <= ? and DenGiaTri >= ?", TieuChiDanhGia.TyLeDanhGia.Oid, KetQua, KetQua));
            else
                ctTyLe = Session.FindObject<ChiTietTyLeDanhGia>(CriteriaOperator.Parse("TyLeDanhGia = ? and TuGiaTri = 0 and DenGiaTri = 0", TieuChiDanhGia.TyLeDanhGia.Oid));
            if (ctTyLe == null)
                ctTyLe = Session.FindObject<ChiTietTyLeDanhGia>(CriteriaOperator.Parse("TyLeDanhGia = ? and TuGiaTri <= ? and TuGiaTri > 0 and DenGiaTri = 0", TieuChiDanhGia.TyLeDanhGia.Oid, KetQua, KetQua));
            if (ctTyLe != null)
            {
                CapDo = ctTyLe.CapDo;
                PhanTramHoanThanh = TrongSo / (TieuChiDanhGia.TyLeDanhGia.CapDoCaoNhat - 1) * (CapDo - 1);
            }

            if (DanhGiaKPI != null)
            {
                if (DanhGiaKPI.TongPhanTramHoanThanh > 0)
                    DanhGiaKPI.TongPhanTramHoanThanh -= _KetQuaCu;
                DanhGiaKPI.TongPhanTramHoanThanh += _KetQuaMoi;
                BangQuyDoiPhanTramKPI bangQuyDoi = Session.FindObject<BangQuyDoiPhanTramKPI>(CriteriaOperator.Parse("TuGiaTri <= ? and DenGiaTri >= ?", Convert.ToInt32(DanhGiaKPI.TongPhanTramHoanThanh), Convert.ToInt32(DanhGiaKPI.TongPhanTramHoanThanh)));
                if (bangQuyDoi != null)
                    DanhGiaKPI.XepLoaiCanBo = bangQuyDoi.XepLoaiCanBo;
            }
        }
    }

}
