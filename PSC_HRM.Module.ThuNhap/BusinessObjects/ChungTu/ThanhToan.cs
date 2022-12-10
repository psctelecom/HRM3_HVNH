using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.ChungTu
{
    [DefaultClassOptions]
    [DefaultProperty("ThanhToanText")]
    [ImageName("BO_HoaDon")]
    [ModelDefault("Caption", "Thanh toán")]
    [Appearance("UyNhiemChi.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "ChungTu.KyTinhLuong is not null and ChungTu.KyTinhLuong.KhoaSo")]
    //[RuleCriteria(DefaultContexts.Save, "SoTien <= NganSachConLai")]
    [RuleCombinationOfPropertiesIsUnique("ThanhToan.Unique", DefaultContexts.Save, "ChungTu;LoaiThanhToan;MauThanhToan")]
    public class ThanhToan : BaseObject
    {
        private ChungTu _ChungTu;
        private MauThanhToanEnum _MauThanhToan;
        private string _So;
        private DateTime _NgayLap;
        private ThongTinTruong _DonViTraTien;
        private string _MaDVQHNS;
        private string _DiaChi;
        private string _TaiKhoan;
        private NganHang _KhoBac;
        private string _DonViNhanTien;
        private string _MaDVQHNS1;
        private string _DiaChi1;
        private string _TaiKhoan1;
        private NganHang _KhoBac1;
        private string _NoiDungThanhToan;
        private decimal _SoTien;
        private string _SoTienBangChu;
        private LoaiThanhToanEnum _LoaiThanhToan;

        [ModelDefault("Caption", "Số")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [RuleUniqueValue()]
        public string So
        {
            get
            {
                return _So;
            }
            set
            {
                SetPropertyValue("So", ref _So, value);
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Chứng từ")]
        public ChungTu ChungTu
        {
            get
            {
                return _ChungTu;
            }
            set
            {
                SetPropertyValue("ChungTu", ref _ChungTu, value);
                if (!IsLoading && value != null)
                {
                    TinhNganSachConLai();
                }
            }
        }

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Loại thanh toán")]
        public LoaiThanhToanEnum LoaiThanhToan
        {
            get
            {
                return _LoaiThanhToan;
            }
            set
            {
                SetPropertyValue("LoaiThanhToan", ref _LoaiThanhToan, value);
                if (!IsLoading)
                {
                    DonViNhanTien item = Session.FindObject<DonViNhanTien>(CriteriaOperator.Parse("LoaiThanhToan = ? and NgungSuDung = False", value));
                    if (item != null)
                        LoadDonViNhanTien(item);
                }
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Mẫu thanh toán")]
        public MauThanhToanEnum MauThanhToan
        {
            get
            {
                return _MauThanhToan;
            }
            set
            {
                SetPropertyValue("MauThanhToan", ref _MauThanhToan, value);
            }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Thanh toán")]
        public string ThanhToanText
        {
            get
            {
                return ObjectFormatter.Format("Số: {So} - Loại thanh toán: {LoaiThanhToan}", this);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị trả tiền")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinTruong DonViTraTien
        {
            get
            {
                return _DonViTraTien;
            }
            set
            {
                SetPropertyValue("DonViTraTien", ref _DonViTraTien, value);
                if (!IsLoading && value != null)
                {
                    DiaChi = value.DiaChi.FullDiaChi;
                    foreach (TaiKhoanNganHang item in value.ListTaiKhoanNganHang)
                    {
                        if (item.TaiKhoanChinh)
                        {
                            TaiKhoan = item.SoTaiKhoan;
                            KhoBac = item.NganHang;
                            break;
                        }
                    }
                }
            }
        }

        [ModelDefault("Caption", "Mã ĐVQHNS")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string MaDVQHNS
        {
            get
            {
                return _MaDVQHNS;
            }
            set
            {
                SetPropertyValue("MaDVQHNS", ref _MaDVQHNS, value);
            }
        }

        [ModelDefault("Caption", "Địa chỉ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string DiaChi
        {
            get
            {
                return _DiaChi;
            }
            set
            {
                SetPropertyValue("DiaChi", ref _DiaChi, value);
            }
        }

        [ModelDefault("Caption", "Tài khoản")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string TaiKhoan
        {
            get
            {
                return _TaiKhoan;
            }
            set
            {
                SetPropertyValue("TaiKhoan", ref _TaiKhoan, value);
            }
        }

        [ModelDefault("Caption", "Tại kho bạc Nhà nước (NH)")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NganHang KhoBac
        {
            get
            {
                return _KhoBac;
            }
            set
            {
                SetPropertyValue("KhoBac", ref _KhoBac, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị nhận tiền")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string DonViNhanTien
        {
            get
            {
                return _DonViNhanTien;
            }
            set
            {
                SetPropertyValue("DonViNhanTien", ref _DonViNhanTien, value);
            }
        }

        [ModelDefault("Caption", "Mã ĐVQHNS")]
        public string MaDVQHNS1
        {
            get
            {
                return _MaDVQHNS1;
            }
            set
            {
                SetPropertyValue("MaDVQHNS1", ref _MaDVQHNS1, value);
            }
        }

        [ModelDefault("Caption", "Địa chỉ")]
        public string DiaChi1
        {
            get
            {
                return _DiaChi1;
            }
            set
            {
                SetPropertyValue("DiaChi1", ref _DiaChi1, value);
            }
        }

        [ModelDefault("Caption", "Tài khoản")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string TaiKhoan1
        {
            get
            {
                return _TaiKhoan1;
            }
            set
            {
                SetPropertyValue("TaiKhoan1", ref _TaiKhoan1, value);
            }
        }

        [ModelDefault("Caption", "Tại Kho bạc Nhà nước (NH)")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NganHang KhoBac1
        {
            get
            {
                return _KhoBac1;
            }
            set
            {
                SetPropertyValue("KhoBac1", ref _KhoBac1, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Nội dung thanh toán")]
        public string NoiDungThanhToan
        {
            get
            {
                return _NoiDungThanhToan;
            }
            set
            {
                SetPropertyValue("NoiDungThanhToan", ref _NoiDungThanhToan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
                if (!IsLoading)
                    SoTienBangChu = HamDungChung.DocTien(value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Số tiền bằng chữ")]
        public string SoTienBangChu
        {
            get
            {
                return _SoTienBangChu;
            }
            set
            {
                SetPropertyValue("SoTienBangChu", ref _SoTienBangChu, value);
            }
        }

        [NonPersistent]
        [Browsable(false)]
        public decimal NganSachConLai { get; set; }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách thanh toán")]
        [Association("ThanhToan-ChiTietThanhToan")]
        public XPCollection<ChiTietThanhToan> ListChiTietThanhToan
        {
            get
            {
                return GetCollection<ChiTietThanhToan>("ListChiTietThanhToan");
            }
        }

        public ThanhToan(Session session) : base(session) { }

        public void TinhNganSachConLai()
        {
            XPCollection<ThanhToan> thanhToanList = new XPCollection<ThanhToan>(Session);
            thanhToanList.Criteria = CriteriaOperator.Parse("ChungTu.KyTinhLuong = ? and MauThanhToan = 0", this.ChungTu.KyTinhLuong.Oid);
            NganSachConLai = this.ChungTu.KyTinhLuong.ThongTinChung.NganSachNhaNuoc - thanhToanList.Sum(a => a.SoTien);
        }

        public void LoadDonViNhanTien(DonViNhanTien item)
        {
            DonViNhanTien = item.TenDonViNhanTien;
            MaDVQHNS1 = item.MaDVQHNS;
            DiaChi1 = item.DiaChi;
            TaiKhoan1 = item.SoTaiKhoan;
            KhoBac1 = item.NganHang;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DonViTraTien = HamDungChung.ThongTinTruong(Session);
            NgayLap = HamDungChung.GetServerTime();
            LoaiThanhToan = 0;
            DonViNhanTien item = Session.FindObject<DonViNhanTien>(CriteriaOperator.Parse("LoaiThanhToan = ? and NgungSuDung = False", LoaiThanhToan));
            if (item != null)
                LoadDonViNhanTien(item);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            NganSachConLai = this.ChungTu.KyTinhLuong.ThongTinChung.NganSachNhaNuoc;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            SoTienBangChu = HamDungChung.DocTien(SoTien);
            TinhNganSachConLai();
        }
    }

}
