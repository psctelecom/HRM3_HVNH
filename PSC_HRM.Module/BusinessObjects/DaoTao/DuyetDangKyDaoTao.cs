using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.DaoTao
{
    [ImageName("BO_QuanLyDaoTao")]
    [DefaultProperty("DangKyDaoTao")]
    [ModelDefault("Caption", "Duyệt đăng ký đào tạo")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyDaoTao;DangKyDaoTao")]
    public class DuyetDangKyDaoTao : PSC_HRM.Module.BaoMat.TruongBaseObject
    {
        // Fields...
        private NganhDaoTao _NganhDaoTao;
        private HinhThucDaoTao _HinhThucDaoTao;
        private DangKyDaoTao _DangKyDaoTao;
        private KhoaDaoTao _KhoaDaoTao;
        private string _GhiChu;
        private NguonKinhPhi _NguonKinhPhi;
        private TruongDaoTao _TruongDaoTao;
        private ChuyenMonDaoTao _ChuyenMonDaoTao;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private QuocGia _QuocGia;
        private QuanLyDaoTao _QuanLyDaoTao;

        [Browsable(false)]
        [Association("QuanLyDaoTao-ListDuyetDangKyDaoTao")]
        public QuanLyDaoTao QuanLyDaoTao
        {
            get
            {
                return _QuanLyDaoTao;
            }
            set
            {
                SetPropertyValue("QuanLyDaoTao", ref _QuanLyDaoTao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đăng ký đào tạo")]
        public DangKyDaoTao DangKyDaoTao
        {
            get
            {
                return _DangKyDaoTao;
            }
            set
            {
                SetPropertyValue("DangKyDaoTao", ref _DangKyDaoTao, value);
                if (!IsLoading && value != null)
                {
                    TrinhDoChuyenMon = value.TrinhDoChuyenMon;
                    ChuyenMonDaoTao = value.ChuyenMonDaoTao;
                    QuocGia = value.QuocGia;
                    TruongDaoTao = value.TruongDaoTao;
                    KhoaDaoTao = value.KhoaDaoTao;
                    NguonKinhPhi = value.NguonKinhPhi;
                    HinhThucDaoTao = value.HinhThucDaoTao;
                }
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Hình thức đào tạo")]
        public HinhThucDaoTao HinhThucDaoTao
        {
            get
            {
                return _HinhThucDaoTao;
            }
            set
            {
                SetPropertyValue("HinhThucDaoTao", ref _HinhThucDaoTao, value);
            }
        }

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

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChuyenMonDaoTao ChuyenMonDaoTao
        {
            get
            {
                return _ChuyenMonDaoTao;
            }
            set
            {
                SetPropertyValue("ChuyenMonDaoTao", ref _ChuyenMonDaoTao, value);
            }
        }

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
                    TruongDaoTao = null;
                    UpdateTruongDaoTaoList();
                }
            }
        }

        [DataSourceProperty("TruongList")]
        [ModelDefault("Caption", "Trường đào tạo")]
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("DuyetDangKyDaoTao-ListChiTietDuyetDangKyDaoTao")]
        public XPCollection<ChiTietDuyetDangKyDaoTao> ListChiTietDuyetDangKyDaoTao
        {
            get
            {
                return GetCollection<ChiTietDuyetDangKyDaoTao>("ListChiTietDuyetDangKyDaoTao");
            }
        }

        public DuyetDangKyDaoTao(Session session) : base(session) { }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateTruongDaoTaoList();
            //
            MaTruong = TruongConfig.MaTruong;
        }

        [Browsable(false)]
        public XPCollection<TruongDaoTao> TruongList { get; set; }

        private void UpdateTruongDaoTaoList()
        {
            if (TruongList == null)
                TruongList = new XPCollection<TruongDaoTao>(Session);
            TruongList.Criteria = CriteriaOperator.Parse("QuocGia=?", QuocGia);
        }

        public bool IsExists(ThongTinNhanVien nhanVien)
        {
            foreach (ChiTietDuyetDangKyDaoTao item in ListChiTietDuyetDangKyDaoTao)
            {
                if (item.ThongTinNhanVien.Oid == nhanVien.Oid)
                    return true;
            }
            return false;
        }

        public void CreateListChiTietDuyetDangKyDaoTao(HoSo_NhanVienItem item)
        {
            ChiTietDuyetDangKyDaoTao chiTiet = new ChiTietDuyetDangKyDaoTao(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            this.ListChiTietDuyetDangKyDaoTao.Add(chiTiet);
            //this.ListChiTietDuyetDangKyDaoTao.Reload();
        }
    }

}
