using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định đi công tác")]
    [Appearance("Hide_NEU", TargetItems = "ThayDoiNhanSu", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'NEU'")]
    [Appearance("Hide_KhacNEU", TargetItems = "TuNgayTT;DenNgayTT", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'NEU'")]

    public class QuyetDinhDiCongTac : QuyetDinh
    {
        private QuocGia _QuocGia;
        private string _SoCongVan;
        private string _DiaDiem;
        private string _DonViToChuc;
        private NguonKinhPhi _NguonKinhPhi;
        private string _TruongHoTro;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private DateTime _TuNgayTT;
        private DateTime _DenNgayTT;
        private string _LyDo;
        //private string _LuuTru;
        private DateTime _NgayXinDi;
        private bool _ThayDoiNhanSu;
        private string _GhiChuTG;

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

        [ModelDefault("Caption", "Số công văn/Phiếu trình")]
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
                if (!IsLoading)
                    TuNgayTT = TuNgay;
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
                if (!IsLoading)
                    DenNgayTT = DenNgay;
            }
        }

        [ModelDefault("Caption", "Từ ngày thực tế")]
        public DateTime TuNgayTT
        {
            get
            {
                return _TuNgayTT;
            }
            set
            {
                SetPropertyValue("TuNgayTT", ref _TuNgayTT, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày thực tế")]
        public DateTime DenNgayTT
        {
            get
            {
                return _DenNgayTT;
            }
            set
            {
                SetPropertyValue("DenNgayTT", ref _DenNgayTT, value);
            }
        }

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

        [ModelDefault("Caption", "Thay đổi nhân sự")]
        public bool ThayDoiNhanSu
        {
            get
            {
                return _ThayDoiNhanSu;
            }
            set
            {
                SetPropertyValue("ThayDoiNhanSu", ref _ThayDoiNhanSu, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Lý do")]
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhDiCongTac-ListChiTietQuyetDinhDiCongTac")]
        public XPCollection<ChiTietQuyetDinhDiCongTac> ListChiTietQuyetDinhDiCongTac
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhDiCongTac>("ListChiTietQuyetDinhDiCongTac");
            }
        }

        public QuyetDinhDiCongTac(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhDiCongTac;
        }

        private void UpdateGiayToList()
        {
            if (TruongConfig.MaTruong.Equals("NEU"))
            {
                if (ListChiTietQuyetDinhDiCongTac.Count == 1)
                    GiayToList = ListChiTietQuyetDinhDiCongTac[0].ThongTinNhanVien.ListGiayToHoSo;
            }
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
                    foreach (ChiTietQuyetDinhDiCongTac item in ListChiTietQuyetDinhDiCongTac)
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
                if (ListChiTietQuyetDinhDiCongTac.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhDiCongTac[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhDiCongTac[0].ThongTinNhanVien.HoTen;
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
