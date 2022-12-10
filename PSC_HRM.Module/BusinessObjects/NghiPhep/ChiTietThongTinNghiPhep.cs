using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;
using PSC_HRM.Module.ChamCong;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.NghiPhep
{
    [ImageName("BO_NangThamNien")]
    [ModelDefault("Caption", "Chi tiết thông tin nghỉ phép")]
   // [Appearance("Hide_QNU", TargetItems = "CC_ChamCongNgayNghi", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong like 'QNU'")]
    public class ChiTietThongTinNghiPhep :  TruongBaseObject
    {
        private ThongTinNghiPhep _ThongTinNghiPhep;
        private CC_ChamCongNgayNghi _CC_ChamCongNgayNghi;

        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private decimal _SoNgay;
        //QNU
        private string _NoiNghiPhep;
        private string _LyDoNghi;
        private string _ThanhToan;
        private string _DaKiTraPhep;

        //private decimal _TruPhepNamTruoc;
        //private decimal _TruPhepNamNay;

        private bool _TruNgayDiDuong;

        private string _GhiChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Thông tin nghỉ phép")]
        [Association("ThongTinNghiPhep-ListChiTietThongTinNghiPhep")]
        public ThongTinNghiPhep ThongTinNghiPhep
        {
            get
            {
                return _ThongTinNghiPhep;
            }
            set
            {
                SetPropertyValue("ThongTinNghiPhep", ref _ThongTinNghiPhep, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chấm công ngày nghỉ")]
        //[DataSourceProperty("CC_ChamCongNgayNghiList", DataSourcePropertyIsNullMode.SelectAll)]
        public CC_ChamCongNgayNghi CC_ChamCongNgayNghi
        {
            get
            {
                return _CC_ChamCongNgayNghi;
            }
            set
            {
                SetPropertyValue("CC_ChamCongNgayNghi", ref _CC_ChamCongNgayNghi, value);
                if (!IsLoading && value != null)
                {
                    TuNgay = value.TuNgay;
                    DenNgay = value.DenNgay;
                    SoNgay = value.SoNgay;
                    ThongTinNghiPhep.SoNgayPhepDaNghi = value.SoNgay;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.SetTime(SetTimeEnum.StartDay);
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading && TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
                    SoNgay = TuNgay.TinhSoNgay(DenNgay, Session);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.SetTime(SetTimeEnum.EndDay);
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (!IsLoading && TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
                    SoNgay = TuNgay.TinhSoNgay(DenNgay, Session);
            }
        }

        [ImmediatePostData]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("Caption", "Số ngày")]
        public decimal SoNgay
        {
            get
            {
                return _SoNgay;
            }
            set
            {
                SetPropertyValue("SoNgay", ref _SoNgay, value);


            }
        }

        //[ModelDefault("DisplayFormat", "N1")]
        //[ModelDefault("EditMask", "N1")]
        //[ModelDefault("Caption", "Trừ phép năm trước")]
        //public decimal TruPhepNamTruoc
        //{
        //    get
        //    {
        //        return _TruPhepNamTruoc;
        //    }
        //    set
        //    {
        //        SetPropertyValue("TruPhepNamTruoc", ref _TruPhepNamTruoc, value);
        //    }
        //}

        //[ModelDefault("DisplayFormat", "N1")]
        //[ModelDefault("EditMask", "N1")]
        //[ModelDefault("Caption", "Trừ phép năm nay")]
        //public decimal TruPhepNamNay
        //{
        //    get
        //    {
        //        return _TruPhepNamNay;
        //    }
        //    set
        //    {
        //        SetPropertyValue("TruPhepNamNay", ref _TruPhepNamNay, value);
        //    }
        //}

        [ModelDefault("Caption", "Trừ ngày đi đường")]
        public bool TruNgayDiDuong
        {
            get
            {
                return _TruNgayDiDuong;
            }
            set
            {
                SetPropertyValue("TruNgayDiDuong", ref _TruNgayDiDuong, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }
        [ModelDefault("Caption", "Nơi nghỉ phép")]
        public string NoiNghiPhep
        {
            get
            {
                return _NoiNghiPhep;
            }
            set
            {
                SetPropertyValue("NoiNghiPhep", ref _NoiNghiPhep, value);
            }
        }
        [ModelDefault("Caption", "Lý do nghỉ")]
        public string LyDoNghi
        {
            get
            {
                return _LyDoNghi;
            }
            set
            {
                SetPropertyValue("LyDoNghi", ref _LyDoNghi, value);
            }
        }
        [ModelDefault("Caption", "Thanh toán")]
        public string ThanhToan
        {
            get
            {
                return _ThanhToan;
            }
            set
            {
                SetPropertyValue("ThanhToan", ref _ThanhToan, value);
            }
        }
        [ModelDefault("Caption", "Đã kí trả phép")]
        public string DaKiTraPhep
        {
            get
            {
                return _DaKiTraPhep;
            }
            set
            {
                SetPropertyValue("DaKiTraPhep", ref _DaKiTraPhep, value);
            }
        }

        
        private string _MaTruong;
        [NonPersistent]
        [Browsable(false)]
        public string MaTruong
        {
            get
            {
                return _MaTruong;
            }
            set
            {
                SetPropertyValue("MaTruong", ref _MaTruong, value);
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //            
            MaTruong = TruongConfig.MaTruong;
            //
            
        }
        public ChiTietThongTinNghiPhep(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            MaTruong = TruongConfig.MaTruong;
            TuNgay = HamDungChung.GetServerTime();
            DenNgay = HamDungChung.GetServerTime();
          
            //UpdateCC_ChamCongNgayNghiList();
        }

        //protected override void OnChanged(string propertyName, object oldValue, object newValue)
        //{
        //    base.OnChanged(propertyName, oldValue, newValue);
        //    if (!IsLoading && ThongTinNghiPhep != null)
        //        CapNhatSoNgayNghi();
        //}

        protected override void OnSaving()
        {
            base.OnSaving();
            if (!IsDeleted && ThongTinNghiPhep != null)
                CapNhatSoNgayNghi();
        }

        protected override void OnDeleting()
        {
            base.OnDeleting();
            if (!IsSaving && ThongTinNghiPhep != null)
                CapNhatSoNgayNghi1();
        }

        [Browsable(false)]
        public XPCollection<CC_ChamCongNgayNghi> CC_ChamCongNgayNghiList { get; set; }

        //private void UpdateCC_ChamCongNgayNghiList()
        //{
        //    if (CC_ChamCongNgayNghiList == null)
        //        CC_ChamCongNgayNghiList = new XPCollection<CC_ChamCongNgayNghi>(Session);
        //    if (ThongTinNghiPhep.ThongTinNhanVien != null)
        //        CC_ChamCongNgayNghiList.Criteria = CriteriaOperator.Parse("IDNhanVien = ?", ThongTinNghiPhep.ThongTinNhanVien);
        //}

        
        private void CapNhatSoNgayNghi()
        {
            ThongTinNghiPhep.SoNgayPhepDaNghi = 0;
            foreach (ChiTietThongTinNghiPhep item in ThongTinNghiPhep.ListChiTietThongTinNghiPhep)
            {
                ThongTinNghiPhep.SoNgayPhepDaNghi += item.SoNgay; 
            }
        }

        private void CapNhatSoNgayNghi1()
        {
            ThongTinNghiPhep.SoNgayPhepDaNghi -= SoNgay; 
        }
    }
}
