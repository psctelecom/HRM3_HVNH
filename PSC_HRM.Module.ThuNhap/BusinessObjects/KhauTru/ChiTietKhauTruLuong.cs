using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.ChotThongTinTinhLuong;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.KhauTru
{
    [ImageName("BO_KhauTru")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết khấu trừ lương cán bộ")]
    [Appearance("ChiTietKhauTruLuong.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "BangKhauTruLuong is not null and BangKhauTruLuong.KyTinhLuong is not null and BangKhauTruLuong.KyTinhLuong.KhoaSo")]
    public class ChiTietKhauTruLuong : ThuNhapBaseObject, IBoPhan
    {
        private BangKhauTruLuong _BangKhauTruLuong;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private DateTime _NgayLap;
        private decimal _SoTien;
        private string _CongThucTinhSoTien;
        private string _GhiChu;
        //private ThongTinTinhLuong _ThongTinTinhLuong;
        private string _CongThucTinhBangChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng khấu trừ lương")]
        [Association("BangKhauTruLuong-ListChiTietKhauTruLuong")]
        public BangKhauTruLuong BangKhauTruLuong
        {
            get
            {
                return _BangKhauTruLuong;
            }
            set
            {
                SetPropertyValue("BangKhauTruLuong", ref _BangKhauTruLuong, value);
            }
        }
        
        //Chỉ dùng để lập công thức
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Kỳ tính lương")]
        public KyTinhLuong KyTinhLuong { get; set; }

        //Chỉ dùng để lập công thức
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Thông tin lương")]
        public NhanVienThongTinLuong ThongTinLuong { get; set; }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading && value != null)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
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
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    //05/11/2016 - Không tính khấu trừ lương từ công thức nữa (chỉ dùng import)
                    //ThongTinTinhLuong = Session.FindObject<ThongTinTinhLuong>(CriteriaOperator.Parse("BangChotThongTinTinhLuong=? and ThongTinNhanVien=?", BangKhauTruLuong.KyTinhLuong.BangChotThongTinTinhLuong, ThongTinNhanVien));
                }
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
            }
        }

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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

        [Size(-1)]
        [Browsable(false)]
        public string CongThucTinhSoTien
        {
            get
            {
                return _CongThucTinhSoTien;
            }
            set
            {
                SetPropertyValue("CongThucTinhSoTien", ref _CongThucTinhSoTien, value);
            }
        }

        [Size(-1)]
        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính bằng chữ")]
        public string CongThucTinhBangChu
        {
            get
            {
                return _CongThucTinhBangChu;
            }
            set
            {
                SetPropertyValue("CongThucTinhBangChu", ref _CongThucTinhBangChu, value);
            }
        }   

        [Size(500)]
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

        //[Browsable(false)]
        //public ThongTinTinhLuong ThongTinTinhLuong
        //{
        //    get
        //    {
        //        return _ThongTinTinhLuong;
        //    }
        //    set
        //    {
        //        SetPropertyValue("ThongTinTinhLuong", ref _ThongTinTinhLuong, value);
        //    }
        //}

        public ChiTietKhauTruLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayLap = HamDungChung.GetServerTime();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }

}
