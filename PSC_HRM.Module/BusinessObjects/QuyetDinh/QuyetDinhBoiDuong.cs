using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BoiDuong;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định bồi dưỡng")]
    [Appearance("QuyetDinhBoiDuong.Hide", TargetItems = "DuyetDangKyBoiDuong", Visibility = ViewItemVisibility.Hide, Criteria = "QuyetDinhBoiDuongThayThe is not null")]

    public class QuyetDinhBoiDuong : QuyetDinh
    {
        private string _SoCongVan;
        private DuyetDangKyBoiDuong _DuyetDangKyBoiDuong;
        private ChuongTrinhBoiDuong _ChuongTrinhBoiDuong;
        private QuocGia _QuocGia;
        private NguonKinhPhi _NguonKinhPhi;
        private string _TruongHoTro;
        private string _NoiDungBoiDuong;
        private string _NoiBoiDuong;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private LoaiChungChi _ChungChi;
        private LoaiHinhBoiDuong _LoaiHinhBoiDuong;
        private string _DonViToChuc;
        //private string _LuuTru;
        private string _ThoiGian;
        private bool _QuyetDinhMoi;
        private TrinhDoChuyenMon _ChuyenNganhDaoTao;
        private string _TruongDaoTao;

        private QuyetDinhBoiDuong _QuyetDinhBoiDuongThayThe;
       //NEU
        private decimal _SoTien;
        private string _SoTienBangChu;
        private DateTime _NgayKhaiGiang;

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

        [ImmediatePostData]
        [ModelDefault("Caption", "Điều chỉnh từ quyết định")]
        public QuyetDinhBoiDuong QuyetDinhBoiDuongThayThe
        {
            get
            {
                return _QuyetDinhBoiDuongThayThe;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoiDuongThayThe", ref _QuyetDinhBoiDuongThayThe, value);
                if (!IsLoading && value != null)
                {
                    QuocGia = value.QuocGia;
                    ChuongTrinhBoiDuong = value.ChuongTrinhBoiDuong;
                    NguonKinhPhi = value.NguonKinhPhi;
                    TuNgay = value.TuNgay;
                    DenNgay = value.DenNgay;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đăng ký bồi dưỡng")]
        public DuyetDangKyBoiDuong DuyetDangKyBoiDuong
        {
            get
            {
                return _DuyetDangKyBoiDuong;
            }
            set
            {
                SetPropertyValue("DuyetDangKyBoiDuong", ref _DuyetDangKyBoiDuong, value);
                if (!IsLoading && value != null)
                {
                    QuocGia = value.QuocGia;
                    ChuongTrinhBoiDuong = value.ChuongTrinhBoiDuong;
                    NguonKinhPhi = value.NguonKinhPhi;
                    TuNgay = value.TuNgay;
                    DenNgay = value.DenNgay;
                }
            }
        }

        [ModelDefault("Caption", "Quốc gia")]
        public QuocGia QuocGia
        {
            get
            {
                return _QuocGia;
            }
            set
            {
                SetPropertyValue("QuocGia", ref _QuocGia, value);
            }
        }


        [ImmediatePostData]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Chương trình bồi dưỡng")]
        public ChuongTrinhBoiDuong ChuongTrinhBoiDuong
        {
            get
            {
                return _ChuongTrinhBoiDuong;
            }
            set
            {
                SetPropertyValue("ChuongTrinhBoiDuong", ref _ChuongTrinhBoiDuong, value);
                if (!IsLoading && value != null)
                {
                    DonViToChuc = ChuongTrinhBoiDuong.DonViToChuc;
                    NoiBoiDuong = ChuongTrinhBoiDuong.DiaDiem;
                    NoiDungBoiDuong = ChuongTrinhBoiDuong.TenChuongTrinhBoiDuong;
                }
            }
        }

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

        [Size(-1)]
        [ModelDefault("Caption", "Trường/Phía mời hỗ trợ")]
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

        [Size(-1)]
        [ModelDefault("Caption", "Nơi bồi dưỡng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong = 'UTE'")]
        [Appearance("NoiBoiDuong_UTE", TargetItems = "NoiBoiDuong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'IUH'")]
        public string NoiBoiDuong
        {
            get
            {
                return _NoiBoiDuong;
            }
            set
            {
                SetPropertyValue("TruongBoiDuong", ref _NoiBoiDuong, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Nội dung bồi dưỡng")]
        [Appearance("NoiDungBoiDuong_UTE", TargetItems = "NoiDungBoiDuong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'IUH'")]
        public string NoiDungBoiDuong
        {
            get
            {
                return _NoiDungBoiDuong;
            }
            set
            {
                SetPropertyValue("NoiDungBoiDuong", ref _NoiDungBoiDuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                DenNgay = value;
                if (TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
                {
                    ThoiGian = "Từ " + TuNgay.ToString("d") + " - " + DenNgay.ToString("d");
                }
                else if (TuNgay == DateTime.MinValue || DenNgay == DateTime.MinValue)
                {
                    ThoiGian = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        //[RuleRequiredField(DefaultContexts.Save)]
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
                    ThoiGian = "Từ " + TuNgay.ToString("d") + " - " + DenNgay.ToString("d");
                }
                else if (TuNgay == DateTime.MinValue || DenNgay == DateTime.MinValue)
                {
                    ThoiGian = null;
                }
            }
        }

        [ModelDefault("Caption", "Loại chứng chỉ")]
        public LoaiChungChi ChungChi
        {
            get
            {
                return _ChungChi;
            }
            set
            {
                SetPropertyValue("ChungChi", ref _ChungChi, value);
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

        [ModelDefault("Caption", "Loại hình bồi dưỡng")]
        [Appearance("LoaiHinhBoiDuong_UTE", TargetItems = "LoaiHinhBoiDuong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'IUH'")]
        public LoaiHinhBoiDuong LoaiHinhBoiDuong
        {
            get
            {
                return _LoaiHinhBoiDuong;
            }
            set
            {
                SetPropertyValue("LoaiHinhBoiDuong", ref _LoaiHinhBoiDuong, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị tổ chức")]
        [Appearance("DonViToChuc_UTE", TargetItems = "DonViToChuc", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'IUH'")]
        public string DonViToChuc
        {
            get
            {
                return _DonViToChuc;
            }
            set
            {
                SetPropertyValue("DonViToChuc", ref _DonViToChuc, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhBoiDuong-ListChiTietBoiDuong")]
        public XPCollection<ChiTietBoiDuong> ListChiTietBoiDuong
        {
            get
            {
                return GetCollection<ChiTietBoiDuong>("ListChiTietBoiDuong");
            }
        }

        [ModelDefault("Caption", "Thời gian")]
        public string ThoiGian
        {
            get
            {
                return _ThoiGian;
            }
            set
            {
                SetPropertyValue("ThoiGian", ref _ThoiGian, value);
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

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        public TrinhDoChuyenMon ChuyenNganhDaoTao
        {
            get
            {
                return _ChuyenNganhDaoTao;
            }
            set
            {
                SetPropertyValue("ChuyenNganhDaoTao", ref _ChuyenNganhDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Trường đào tạo")]
        public string TruongDaoTao
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

        public QuyetDinhBoiDuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhBoiDuong;
            QuocGia = HamDungChung.GetCurrentQuocGia(Session);
            //
            QuyetDinhMoi = true;
        }
        public void CreateListChiTietBoiDuong(HoSo_NhanVienItem item)
        {
            ChiTietBoiDuong chiTiet = new ChiTietBoiDuong(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            this.ListChiTietBoiDuong.Add(chiTiet);
        }

        private void UpdateGiayToList()
        {
            if (ListChiTietBoiDuong.Count == 1)
                GiayToList = ListChiTietBoiDuong[0].ThongTinNhanVien.ListGiayToHoSo;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateGiayToList();
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietBoiDuong item in ListChiTietBoiDuong)
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
            }
        }
    }

}
