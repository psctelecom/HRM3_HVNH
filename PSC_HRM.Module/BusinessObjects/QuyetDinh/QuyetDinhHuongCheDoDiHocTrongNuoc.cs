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
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định hưởng chế độ đi học trong nước")]
    [Appearance("Hide_QuyetDinhDaoTao", TargetItems = "QuyetDinhDaoTao;", Enabled = false, Criteria = "QuyetDinhDaoTao is not null")]
    
    public class QuyetDinhHuongCheDoDiHocTrongNuoc : QuyetDinh
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        //private DuyetDangKyDaoTao _DuyetDangKyDaoTao;
        //private bool _DaoTaoTapTrung;
        //private NguonKinhPhi _NguonKinhPhi;
        //private string _TruongHoTro;
        private QuocGia _QuocGia;
        private HinhThucDaoTao _LoaiHinhDaoTao;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private TruongDaoTao _TruongDaoTao;
        //private string _LuuTru;
        private string _ThoiGianDaoTao;
        private bool _QuyetDinhMoi;
        private QuyetDinhDaoTao _QuyetDinhDaoTao;
        
        //[ImmediatePostData]
        //[ModelDefault("Caption", "Đăng ký đào tạo")]
        //public DuyetDangKyDaoTao DuyetDangKyDaoTao
        //{
        //    get
        //    {
        //        return _DuyetDangKyDaoTao;
        //    }
        //    set
        //    {
        //        SetPropertyValue("DuyetDangKyDaoTao", ref _DuyetDangKyDaoTao, value);
        //        if (!IsLoading && value != null)
        //        {
        //            HinhThucDaoTao = value.HinhThucDaoTao;
        //            TrinhDoChuyenMon = value.TrinhDoChuyenMon;
        //            TruongDaoTao = value.TruongDaoTao;
        //            QuocGia = value.QuocGia;
        //            //NguonKinhPhi = value.NguonKinhPhi;
        //        }
        //    }
        //}

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định đào tạo")]
        public QuyetDinhDaoTao QuyetDinhDaoTao
        {
            get
            {
                return _QuyetDinhDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhDaoTao", ref _QuyetDinhDaoTao, value);
                if (!IsLoading && value != null)
                {
                    HinhThucDaoTao = value.HinhThucDaoTao;
                    TrinhDoChuyenMon = value.TrinhDoChuyenMon;
                    QuocGia = value.QuocGia;
                    TruongDaoTao = value.TruongDaoTao;
                    TuNgay = value.TuNgay;
                    DenNgay = value.DenNgay;
                    ThoiGianDaoTao = value.ThoiGianDaoTao;
                    //NguonKinhPhi = value.NguonKinhPhi;

                    ChiTietQuyetDinhHuongCheDoDiHocTrongNuoc chiTiet;
                    foreach (ChiTietDaoTao item in value.ListChiTietDaoTao)
                    {
                        chiTiet = Session.FindObject<ChiTietQuyetDinhHuongCheDoDiHocTrongNuoc>(CriteriaOperator.Parse("QuyetDinhHuongCheDoDiHocTrongNuoc=? and ThongTinNhanVien=?", Oid, item.ThongTinNhanVien));
                        if (chiTiet == null)
                        {
                            chiTiet = new ChiTietQuyetDinhHuongCheDoDiHocTrongNuoc(Session);
                            chiTiet.QuyetDinhHuongCheDoDiHocTrongNuoc = this;
                            chiTiet.BoPhan = item.BoPhan;
                            chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                            chiTiet.ChuyenMonDaoTao = item.ChuyenMonDaoTao;

                            ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc.Add(chiTiet);
                        }
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Hình thức đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        //[ImmediatePostData]
        //[ModelDefault("Caption", "Đào tạo tập trung")]
        //public bool DaoTaoTapTrung
        //{
        //    get
        //    {
        //        return _DaoTaoTapTrung;
        //    }
        //    set
        //    {
        //        SetPropertyValue("DaoTaoTapTrung", ref _DaoTaoTapTrung, value);
        //    }
        //}

        //[ImmediatePostData]
        //[ModelDefault("Caption", "Nguồn kinh phí")]
        //public NguonKinhPhi NguonKinhPhi
        //{
        //    get
        //    {
        //        return _NguonKinhPhi;
        //    }
        //    set
        //    {
        //        SetPropertyValue("NguonKinhPhi", ref _NguonKinhPhi, value);
        //    }
        //}

        //[ImmediatePostData]
        //[ModelDefault("Caption", "Trường hỗ trợ")]
        //public string TruongHoTro
        //{
        //    get
        //    {
        //        return _TruongHoTro;
        //    }
        //    set
        //    {
        //        SetPropertyValue("TruongHoTro", ref _TruongHoTro, value);
        //    }
        //}

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
        [RuleRequiredField(DefaultContexts.Save)]
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
        [RuleRequiredField(DefaultContexts.Save)]
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

        [Aggregated]
        [ImmediatePostData]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhHuongCheDoDiHocTrongNuoc-ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc")]
        public XPCollection<ChiTietQuyetDinhHuongCheDoDiHocTrongNuoc> ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhHuongCheDoDiHocTrongNuoc>("ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc");
            }
        }

        public QuyetDinhHuongCheDoDiHocTrongNuoc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            QuocGia = HamDungChung.GetCurrentQuocGia(Session);
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = "hưởng chế độ đi học";
                //NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhDaoTao;
            //
            QuyetDinhMoi = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateTruongList();

            //Lấy mã trường hiện tại dùng để phân quyền
            MaTruong = TruongConfig.MaTruong;
        }

        [Browsable(false)]
        public XPCollection<TruongDaoTao> TruongList { get; set; }

        private void UpdateTruongList()
        {
            if (TruongList == null)
                TruongList = new XPCollection<TruongDaoTao>(Session);
            TruongList.Criteria = CriteriaOperator.Parse("QuocGia=?", QuocGia);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietQuyetDinhHuongCheDoDiHocTrongNuoc item in ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc)
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

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc[0].ThongTinNhanVien.HoTen;
                }
                else
                {
                    BoPhanText = string.Empty;
                    NhanVienText = string.Empty;
                }
            }
        }
    }
}
