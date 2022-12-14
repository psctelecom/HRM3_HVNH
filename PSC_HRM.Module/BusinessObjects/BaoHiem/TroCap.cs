using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BaoHiem
{
    [ImageName("BO_TroCap")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Quản lý thanh toán BHXH")]
    [RuleCombinationOfPropertiesIsUnique("TroCap.Unique", DefaultContexts.Save, "ThongTinNhanVien;QuanLyTroCap")]
    public class TroCap : BaseObject, IBoPhan
    {
        private decimal _SoTienBHXHThanhToan;
        private string _LoaiTroCap;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private int _ThoiGianDongBaoHiem;
        private QuanLyTroCap _QuanLyTroCap;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private decimal _SoTien;
        private string _GhiChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Thời gian")]
        [Association("QuanLyTroCap-ListTroCap")]
        public QuanLyTroCap QuanLyTroCap
        {
            get
            {
                return _QuanLyTroCap;
            }
            set
            {
                SetPropertyValue("QuanLyTroCap", ref _QuanLyTroCap, value);
            }
        }

        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Loại trợ cấp")]
        public string LoaiTroCap
        {
            get
            {
                return _LoaiTroCap;
            }
            set
            {
                SetPropertyValue("LoaiTroCap", ref _LoaiTroCap, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
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
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    ThoiGianDongBaoHiem = TinhThoiGianDongBaoHiem();
                    TinhMucLuongHuongBHXH();
                    AfterThongTinNhanVienChanged();
                }
            }
        }

        [ModelDefault("Caption", "Thời gian đóng BHXH")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int ThoiGianDongBaoHiem
        {
            get
            {
                return _ThoiGianDongBaoHiem;
            }
            set
            {
                SetPropertyValue("ThoiGianDongBaoHiem", ref _ThoiGianDongBaoHiem, value);
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
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading && value != DateTime.MinValue && DenNgay != DateTime.MinValue)
                    TinhSoNgayNghi();
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
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (!IsLoading && value != DateTime.MinValue && TuNgay != DateTime.MinValue)
                    TinhSoNgayNghi();
            }
        }

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }

        [ModelDefault("Caption", "Số tiền BHXH thanh toán")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTienBHXHThanhToan
        {
            get
            {
                return _SoTienBHXHThanhToan;
            }
            set
            {
                SetPropertyValue("SoTienBHXHThanhToan", ref _SoTienBHXHThanhToan, value);
            }
        }

        [Size(300)]
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

        public TroCap(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<HoSo.ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        protected virtual void AfterThongTinNhanVienChanged()
        {

        }

        protected virtual void TinhSoNgayNghi()
        {
            
        }

        protected virtual void TinhSoTien()
        {
            
        }

        protected virtual void TinhMucLuongHuongBHXH()
        {
            
        }

        private int TinhThoiGianDongBaoHiem()
        {
            int thoiGian = 0;
            HoSoBaoHiem hoSo = Session.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien));
            if (hoSo != null)
            {
                DateTime currentTime = HamDungChung.GetServerTime();
                thoiGian = hoSo.NgayThamGiaBHXH.TinhSoThang(currentTime);
            }

            return thoiGian;
        }
    }
}