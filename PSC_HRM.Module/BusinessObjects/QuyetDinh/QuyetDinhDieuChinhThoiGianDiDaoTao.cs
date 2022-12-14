using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.DiNuocNgoai;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định điều chỉnh thời gian đi đào tạo")]
    public class QuyetDinhDieuChinhThoiGianDiDaoTao : QuyetDinh
    {
        //private bool _DiNuocNgoaiTren30Ngay;
        private QuyetDinhDaoTao _QuyetDinhDaoTao;
        private NguonKinhPhi _NguonKinhPhi;
        private string _TruongHoTro;
        private QuocGia _QuocGia;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private DateTime _TuNgayDC;
        private DateTime _DenNgayDC;
        private string _LyDo;
        //private string _LuuTru;
        private string _SoCongVan;
        private string _DiaDiem;
        private string _DonViToChuc;
        private bool _QuyetDinhMoi;
        private string _ChiTietKinhPhi;
        private string _TongKinhPhi;
        private DateTime _NgayXinDi;
        private string _GhiChu;
        private string _GhiChuTG;

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
                    QuocGia = value.QuocGia;
                    NguonKinhPhi = value.NguonKinhPhi;
                    TruongHoTro = value.TruongHoTro;
                    TuNgay = value.TuNgay;
                    DenNgay = value.DenNgay;



                    ChiTietQuyetDinhDieuChinhThoiGianDiDaoTao chiTiet;
                    foreach (var item in value.ListChiTietDaoTao)
                    {
                        if (!IsExists(item.ThongTinNhanVien))
                        {
                            chiTiet = new ChiTietQuyetDinhDieuChinhThoiGianDiDaoTao(Session);
                            chiTiet.QuyetDinhDieuChinhThoiGianDiDaoTao = this;
                            chiTiet.BoPhan = item.BoPhan;
                            //chiTiet.ViTriCongTac = item.ViTriCongTac;
                            chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                        }
                    }
                }
            }
        }

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
            }
        }

        [ModelDefault("Caption", "Nguồn kinh phí")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Chi tiết kinh phí")]
        public string ChiTietKinhPhi
        {
            get
            {
                return _ChiTietKinhPhi;
            }
            set
            {
                SetPropertyValue("ChiTietKinhPhi", ref _ChiTietKinhPhi, value);
            }
        }

        [ModelDefault("Caption", "Tổng kinh phí")]
        public string TongKinhPhi
        {
            get
            {
                return _TongKinhPhi;
            }
            set
            {
                SetPropertyValue("TongKinhPhi", ref _TongKinhPhi, value);
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
                //if (!IsLoading && TuNgay != DateTime.MinValue && DenNgay != DateTime.MaxValue)
                //    XuLyDiNuocNgoaiTren30Ngay();
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
                //if (!IsLoading && TuNgay != DateTime.MinValue && DenNgay != DateTime.MaxValue)
                //    XuLyDiNuocNgoaiTren30Ngay();
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày điều chỉnh")]
        public DateTime TuNgayDC
        {
            get
            {
                return _TuNgayDC;
            }
            set
            {
                SetPropertyValue("TuNgayDC", ref _TuNgayDC, value);
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày điều chỉnh")]
        public DateTime DenNgayDC
        {
            get
            {
                return _DenNgayDC;
            }
            set
            {
                SetPropertyValue("DenNgayDC", ref _DenNgayDC, value);

            }
        }
        [ImmediatePostData]
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
        //[ModelDefault("Caption", "Đi nước ngoài trên 30 ngày")]
        //public bool DiNuocNgoaiTren30Ngay
        //{
        //    get
        //    {
        //        return _DiNuocNgoaiTren30Ngay;
        //    }
        //    set
        //    {
        //        SetPropertyValue("DiNuocNgoaiTren30Ngay", ref _DiNuocNgoaiTren30Ngay, value);
        //    }
        //}

        [Size(250)]
        [ModelDefault("Caption", "Lý do")]
        // [RuleRequiredField(DefaultContexts.Save)]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        [Size(250)]
        [ModelDefault("Caption", "Ghi chú")]
        // [RuleRequiredField(DefaultContexts.Save)]
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
        [Size(250)]
        [ModelDefault("Caption", "Ghi chú TG")]
        // [RuleRequiredField(DefaultContexts.Save)]
        public string GhiChuTG
        {
            get
            {
                return _GhiChuTG;
            }
            set
            {
                SetPropertyValue("GhiChuTG", ref _GhiChuTG, value);
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

        [ModelDefault("Caption", "Đơn vị tổ chức")]
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

        [ModelDefault("Caption", "Địa điểm")]
        public string DiaDiem
        {
            get
            {
                return _DiaDiem;
            }
            set
            {
                SetPropertyValue("DiaDiem", ref _DiaDiem, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhDieuChinhThoiGianDiDaoTao-ListChiTietQuyetDinhDieuChinhThoiGianDiDaoTao")]
        public XPCollection<ChiTietQuyetDinhDieuChinhThoiGianDiDaoTao> ListChiTietQuyetDinhDieuChinhThoiGianDiDaoTao
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhDieuChinhThoiGianDiDaoTao>("ListChiTietQuyetDinhDieuChinhThoiGianDiDaoTao");
            }
        }

        public QuyetDinhDieuChinhThoiGianDiDaoTao(Session session) : base(session) { }

        [NonPersistent]
        [Browsable(false)]
        public string MaTruong { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //if (string.IsNullOrWhiteSpace(NoiDung))
            //    NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhDieuChinhThoiGianDiNuocNgoai;
            MaTruong = TruongConfig.MaTruong;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietQuyetDinhDieuChinhThoiGianDiDaoTao item in ListChiTietQuyetDinhDieuChinhThoiGianDiDaoTao)
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

        //private void XuLyDiNuocNgoaiTren30Ngay()
        //{
        //    int ngay = TuNgay.TinhSoNgay(DenNgay);
        //    DiNuocNgoaiTren30Ngay = ngay > 30;
        //}

        public bool IsExists(ThongTinNhanVien nhanVien)
        {
            foreach (var item in ListChiTietQuyetDinhDieuChinhThoiGianDiDaoTao)
            {
                if (item.ThongTinNhanVien.Oid == nhanVien.Oid)
                    return true;
            }
            return false;
        }
        //public void CreateListChiTietDaoTao(HoSo_NhanVienItem item)
        //{
        //    ChiTietDaoTao chiTiet = new ChiTietDaoTao(Session);
        //    chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
        //    chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
        //    this.ListChiTietDaoTao.Add(chiTiet);
        //}

    }
}
