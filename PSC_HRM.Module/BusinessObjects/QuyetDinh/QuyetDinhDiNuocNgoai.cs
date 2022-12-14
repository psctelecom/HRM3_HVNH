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
    [ModelDefault("Caption", "Quyết định đi nước ngoài")]
    [Appearance("Hide_NEU", TargetItems = "ChiTietKinhPhi;TongKinhPhi", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'NEU'")]
    public class QuyetDinhDiNuocNgoai : QuyetDinh
    {
        private bool _DiNuocNgoaiTren30Ngay;
        private DangKyDiNuocNgoai _DangKyDiNuocNgoai;
        private NguonKinhPhi _NguonKinhPhi;
        private string _TruongHoTro;
        private QuocGia _QuocGia;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
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
        [ModelDefault("Caption", "Đăng ký đi nước ngoài")]
        public DangKyDiNuocNgoai DangKyDiNuocNgoai
        {
            get
            {
                return _DangKyDiNuocNgoai;
            }
            set
            {
                SetPropertyValue("DangKyDiNuocNgoai", ref _DangKyDiNuocNgoai, value);
                if (!IsLoading && value != null)
                {
                    QuocGia = value.QuocGia;
                    NguonKinhPhi = value.NguonKinhPhi;
                    TruongHoTro = value.TruongHoTro;
                    TuNgay = value.TuNgay;
                    DenNgay = value.DenNgay;
                    LyDo = value.LyDo;
                    

                    ChiTietQuyetDinhDiNuocNgoai chiTiet;
                    foreach (var item in value.ListChiTietDangKyDiNuocNgoai)
                    {
                    	if (!IsExists(item.ThongTinNhanVien))
                        {
                            chiTiet = new ChiTietQuyetDinhDiNuocNgoai(Session);
                            chiTiet.QuyetDinhDiNuocNgoai = this;
                            chiTiet.BoPhan = item.BoPhan;
                            chiTiet.ViTriCongTac = item.ViTriCongTac;
                            chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                        }
                    }
                }
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
                if (!IsLoading && MaTruong.Equals("NEU"))
                    DiaDiem = QuocGia.TenQuocGia;
            }
        }

        [ModelDefault("Caption", "Nguồn kinh phí")]
        //[RuleRequiredField(DefaultContexts.Save)]
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
                if (!IsLoading && TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
                {
                    XuLyDiNuocNgoaiTren30Ngay();
                    GhiChuTG = HamDungChung.GetThoiGianViecRieng(Session, TuNgay, DenNgay, null, 0);
                }
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
                if (!IsLoading && TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
                {
                    XuLyDiNuocNgoaiTren30Ngay();
                    GhiChuTG = HamDungChung.GetThoiGianViecRieng(Session, TuNgay, DenNgay, null, 0);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày xin đi/mời")]
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
        [ModelDefault("Caption", "Đi nước ngoài trên 3 tháng")]
        public bool DiNuocNgoaiTren30Ngay
        {
            get
            {
                return _DiNuocNgoaiTren30Ngay;
            }
            set
            {
                SetPropertyValue("DiNuocNgoaiTren30Ngay", ref _DiNuocNgoaiTren30Ngay, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Lý do")]
        //[RuleRequiredField(DefaultContexts.Save)]
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

        [Size(500)]
        [ModelDefault("Caption", "Ghi chú")]
        //[RuleRequiredField(DefaultContexts.Save)]
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

        [Size(500)]
        [ModelDefault("Caption", "Ghi chú TG")]
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

        [ModelDefault("Caption", "Số công văn/phiếu trình")]
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
        [Association("QuyetDinhDiNuocNgoai-ListChiTietQuyetDinhDiNuocNgoai")]
        public XPCollection<ChiTietQuyetDinhDiNuocNgoai> ListChiTietQuyetDinhDiNuocNgoai
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhDiNuocNgoai>("ListChiTietQuyetDinhDiNuocNgoai");
            }
        }

        public QuyetDinhDiNuocNgoai(Session session) : base(session) { }

        [NonPersistent]
        [Browsable(false)]
        public string MaTruong { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhDiNuocNgoai;
            MaTruong = TruongConfig.MaTruong;
            TuNgay = HamDungChung.GetServerTime();
            DenNgay = HamDungChung.GetServerTime();
        }

        private void UpdateGiayToList()
        {
            if (ListChiTietQuyetDinhDiNuocNgoai.Count == 1)
                GiayToList = ListChiTietQuyetDinhDiNuocNgoai[0].ThongTinNhanVien.ListGiayToHoSo;
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
                    foreach (ChiTietQuyetDinhDiNuocNgoai item in ListChiTietQuyetDinhDiNuocNgoai)
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
                if (ListChiTietQuyetDinhDiNuocNgoai.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhDiNuocNgoai[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhDiNuocNgoai[0].ThongTinNhanVien.HoTen;
                }
                else
                {
                    BoPhanText = string.Empty;
                    NhanVienText = string.Empty;
                }
            }
        }

        private void XuLyDiNuocNgoaiTren30Ngay()
        {
            int ngay = TuNgay.TinhSoNgay(DenNgay);
            DiNuocNgoaiTren30Ngay = ngay > 30;
        }   

        public bool IsExists(ThongTinNhanVien nhanVien)
        {
            foreach (var item in ListChiTietQuyetDinhDiNuocNgoai)
            {
                if (item.ThongTinNhanVien.Oid == nhanVien.Oid)
                    return true;
            }
            return false;
        }
        public void CreateListChiTietQuyetDinhDiNuocNgoai(HoSo_NhanVienItem item)
        {
            ChiTietQuyetDinhDiNuocNgoai chiTiet = new ChiTietQuyetDinhDiNuocNgoai(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            this.ListChiTietQuyetDinhDiNuocNgoai.Add(chiTiet);
        }
    }

}
