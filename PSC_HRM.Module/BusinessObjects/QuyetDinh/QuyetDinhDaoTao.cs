using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DaoTao;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.QuyetDinh
{
    //Lưu ý: Khi tạo quyết định đào tạo thì không tạo quá trình đào tạo do:
    // chưa nhận được bằng. chỉ tạo quá trình đào tạo trong quyết định công
    // nhận đào tạo. Khi xóa quyết định đào tạo thì kiểm tra xem có quyết
    // định công nhận đào tạo và quá trình đào tạo không, nếu có thì xóa.
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định đào tạo")]
    [Appearance("Hide.KhacNEU", TargetItems = "NguoiKyHD;SoTien;SoTienBangChu;NgayKhaiGiang", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong!='NEU'")]
    [Appearance("QuyetDinhDaoTao.HideNguoiKyHD", TargetItems = "NguoiKyHD", Visibility = ViewItemVisibility.Hide, Criteria = "QuocGia.TenQuocGia like 'Việt Nam'")]
    public class QuyetDinhDaoTao : QuyetDinh
    {
        private string _SoCongVan;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private DuyetDangKyDaoTao _DuyetDangKyDaoTao;
        private KhoaDaoTao _KhoaDaoTao;
        private bool _DaoTaoTapTrung;
        private DateTime _NgayPhatSinhBienDong;
        private NganhDaoTao _NganhDaoTao;
        private NguonKinhPhi _NguonKinhPhi;
        private string _TruongHoTro;
        private QuocGia _QuocGia;
        private HinhThucDaoTao _LoaiHinhDaoTao;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private TruongDaoTao _TruongDaoTao;
        //private string _LuuTru;
        private string _ThoiGianDaoTao;
        private bool _QuyetDinhMoi;
        private DateTime _NgayXinDi;
        private ThongTinNhanVien _NguoiKyHD;
        private string _GhiChu;
        //NEU
        private decimal _SoTien;
        private string _SoTienBangChu;
        private DateTime _NgayKhaiGiang;

        [ModelDefault("Caption", "Số công văn")]
        public string SoCongVan
        {
            get
            {
                return _SoCongVan;
            }
            set
            {
                SetPropertyValue("SoCongVan", ref _SoCongVan, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Ngày phát sinh biến động")]
        public DateTime NgayPhatSinhBienDong
        {
            get
            {
                return _NgayPhatSinhBienDong;
            }
            set
            {
                SetPropertyValue("NgayPhatSinhBienDong", ref _NgayPhatSinhBienDong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đăng ký đào tạo")]
        public DuyetDangKyDaoTao DuyetDangKyDaoTao
        {
            get
            {
                return _DuyetDangKyDaoTao;
            }
            set
            {
                SetPropertyValue("DuyetDangKyDaoTao", ref _DuyetDangKyDaoTao, value);
                if (!IsLoading && value != null)
                {
                    HinhThucDaoTao = value.HinhThucDaoTao;
                    TrinhDoChuyenMon = value.TrinhDoChuyenMon;
                    NganhDaoTao = value.NganhDaoTao;
                    TruongDaoTao = value.TruongDaoTao;
                    QuocGia = value.QuocGia;
                    NguonKinhPhi = value.NguonKinhPhi;
                    KhoaDaoTao = value.KhoaDaoTao;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Hình thức đào tạo")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public HinhThucDaoTao HinhThucDaoTao
        {
            get
            {
                return _LoaiHinhDaoTao;
            }
            set
            {
                SetPropertyValue("LoaiHinhDaoTao", ref _LoaiHinhDaoTao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đào tạo tập trung")]
        public bool DaoTaoTapTrung
        {
            get
            {
                return _DaoTaoTapTrung;
            }
            set
            {
                SetPropertyValue("DaoTaoTapTrung", ref _DaoTaoTapTrung, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nguồn kinh phí")]
        public NguonKinhPhi NguonKinhPhi
        {
            get
            {
                return _NguonKinhPhi;
            }
            set
            {
                SetPropertyValue("NguonKinhPhi", ref _NguonKinhPhi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường hỗ trợ")]
        public string TruongHoTro
        {
            get
            {
                return _TruongHoTro;
            }
            set
            {
                SetPropertyValue("TruongHoTro", ref _TruongHoTro, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Quốc gia")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuocGia QuocGia
        {
            get
            {
                return _QuocGia;
            }
            set
            {
                SetPropertyValue("QuocGia", ref _QuocGia, value);
                if (!IsLoading)
                {
                    UpdateTruongList();
                    TruongDaoTao = null;
                }
               
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường đào tạo")]
        [DataSourceProperty("TruongList")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public TruongDaoTao TruongDaoTao
        {
            get
            {
                return _TruongDaoTao;
            }
            set
            {
                SetPropertyValue("TruongDaoTao", ref _TruongDaoTao, value);
            }
        }

       

        [ImmediatePostData]
        [ModelDefault("Caption", "Trình độ đào tạo")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngành đào tạo")]
        public NganhDaoTao NganhDaoTao
        {
            get
            {
                return _NganhDaoTao;
            }
            set
            {
                SetPropertyValue("NganhDaoTao", ref _NganhDaoTao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Khóa đào tạo")]
        public KhoaDaoTao KhoaDaoTao
        {
            get
            {
                return _KhoaDaoTao;
            }
            set
            {
                SetPropertyValue("KhoaDaoTao", ref _KhoaDaoTao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    NgayPhatSinhBienDong = value;
                    DenNgay = value;
                    if (TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
                    {
                        ThoiGianDaoTao = "Từ " + TuNgay.ToString("d") + " - " + DenNgay.ToString("d");
                    }
                    else if (TuNgay == DateTime.MinValue || DenNgay == DateTime.MinValue)
                    {
                        ThoiGianDaoTao = null;
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
                {
                    ThoiGianDaoTao = "Từ " + TuNgay.ToString("d") + " - " + DenNgay.ToString("d");
                }
                else if (TuNgay == DateTime.MinValue || DenNgay == DateTime.MinValue)
                {
                    ThoiGianDaoTao = null;
                }
            }
        }

        //[ModelDefault("Caption", "Lưu trữ")]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
        //public string LuuTru
        //{
        //    get
        //    {
        //        return _LuuTru;
        //    }
        //    set
        //    {
        //        SetPropertyValue("LuuTru", ref _LuuTru, value);
        //    }
        //}

        [ImmediatePostData]
        [ModelDefault("Caption", "Người kí Hợp Đồng")]
        public ThongTinNhanVien NguoiKyHD
        {
            get
            {
                return _NguoiKyHD;
            }
            set
            {
                SetPropertyValue("NguoiKyHD", ref _NguoiKyHD, value);
               
            }
        }
 
        [ImmediatePostData]
        [ModelDefault("Caption", "Thời gian đào tạo")]
        public string ThoiGianDaoTao
        {
            get
            {
                return _ThoiGianDaoTao;
            }
            set
            {
                SetPropertyValue("ThoiGianDaoTao", ref _ThoiGianDaoTao, value);
            }
        }
        
        [ModelDefault("Caption", "Ngày xin đi")]
        public DateTime NgayXinDi
        {
            get
            {
                return _NgayXinDi;
            }
            set
            {
                SetPropertyValue("NgayXinDi", ref _NgayXinDi, value);
            }
        }

        [Size(250)]
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

        [ModelDefault("Caption", "Quyết định mới")]
        public bool QuyetDinhMoi
        {
            get
            {
                return _QuyetDinhMoi;
            }
            set
            {
                SetPropertyValue("QuyetDinhMoi", ref _QuyetDinhMoi, value);
            }
        }

        [ImmediatePostData]
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

        //[Size(300)]
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

        
        [ModelDefault("Caption", "Ngày khai giảng")]
        public DateTime NgayKhaiGiang
        {
            get
            {
                return _NgayKhaiGiang;
            }
            set
            {
                SetPropertyValue("NgayKhaiGiang", ref _NgayKhaiGiang, value);
            }
        }

        [Aggregated]
        [ImmediatePostData]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhDaoTao-ListChiTietDaoTao")]
        public XPCollection<ChiTietDaoTao> ListChiTietDaoTao
        {
            get
            {
                return GetCollection<ChiTietDaoTao>("ListChiTietDaoTao");
            }
        }

        public QuyetDinhDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNguoiKyHDList();
            QuocGia = HamDungChung.GetCurrentQuocGia(Session);
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhDaoTao;
            
            QuyetDinhMoi = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateTruongList();
            UpdateGiayToList();
            //Lấy mã trường hiện tại dùng để phân quyền
            MaTruong = TruongConfig.MaTruong;

        }

        [Browsable(false)]
        public XPCollection<TruongDaoTao> TruongList { get; set; }

        private void UpdateGiayToList()
        {
            if (ListChiTietDaoTao.Count == 1)
                GiayToList = ListChiTietDaoTao[0].ThongTinNhanVien.ListGiayToHoSo;
        }
               
        private void UpdateTruongList()
        {
            if (TruongList == null)
                TruongList = new XPCollection<TruongDaoTao>(Session);
            TruongList.Criteria = CriteriaOperator.Parse("QuocGia=?", QuocGia);
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NguoiKyHDList { get; set; }

        private void UpdateNguoiKyHDList()
        {
            NguoiKyHDList = new XPCollection<ThongTinNhanVien>(Session);
            NguoiKyHDList.Criteria = CriteriaOperator.Parse("ChucVu.TenChucVu like 'Trưởng phòng' and BoPhan.TenBoPhan like 'Phòng Tổ chức cán bộ' and TinhTrang.KhongConCongTacTaiTruong=0");
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietDaoTao item in ListChiTietDaoTao)
                    {
                        if (item.GiayToHoSo != null)
                        {
                            item.GiayToHoSo.QuyetDinh = this;
                            item.GiayToHoSo.SoGiayTo = SoQuyetDinh;
                            item.GiayToHoSo.TrichYeu = NoiDung;
                            item.GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                            item.GiayToHoSo.NgayLap = NgayQuyetDinh;
                            item.GiayToHoSo.LuuTru = GiayToHoSo.LuuTru;
                            item.GiayToHoSo.DuongDanFile = GiayToHoSo.DuongDanFile;
                        }
                    }
                }

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietDaoTao.Count == 1)
                {
                    BoPhanText = ListChiTietDaoTao[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietDaoTao[0].ThongTinNhanVien.HoTen;
                }
                else
                {
                    BoPhanText = string.Empty;
                    NhanVienText = string.Empty;
                }
            }
        }
        public void CreateListChiTietDaoTao(HoSo_NhanVienItem item)
        {
            ChiTietDaoTao chiTiet = new ChiTietDaoTao(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            this.ListChiTietDaoTao.Add(chiTiet);
        }

        public bool ExistsNhanVien(ThongTinNhanVien nhanVien)
        {
            foreach (ChiTietDaoTao item in ListChiTietDaoTao)
            {
                if (item.ThongTinNhanVien.Oid == nhanVien.Oid)
                    return true;
            }
            return false;
        }
    }
}
