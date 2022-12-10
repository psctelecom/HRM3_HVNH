using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.DaoTao
{
    [ImageName("BO_QuanLyDaoTao")]
    [ModelDefault("Caption", "Đăng ký đào tạo")]
    [DefaultProperty("Caption")]
    [RuleCombinationOfPropertiesIsUnique("DangKyDaoTao", DefaultContexts.Save, "QuanLyDaoTao;TrinhDoChuyenMon;ChuyenMonDaoTao;TruongDaoTao;KhoaDaoTao")]
    public class DangKyDaoTao : TruongBaseObject
    {
        // Fields...
        private HinhThucDaoTao _HinhThucDaoTao;
        private KhoaDaoTao _KhoaDaoTao;
        private string _GhiChu;
        private NguonKinhPhi _NguonKinhPhi;
        private TruongDaoTao _TruongDaoTao;
        private ChuyenMonDaoTao _ChuyenMonDaoTao;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private QuocGia _QuocGia;
        private QuanLyDaoTao _QuanLyDaoTao;
        private ThongTinNhanVien _NhanVien; 

        [Browsable(false)]
        [Association("QuanLyDaoTao-ListDangKyDaoTao")]
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

        [ModelDefault("Caption", "Cán bộ")]
        public ThongTinNhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                
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

        [Browsable(false)]
        public string Caption
        {
            get
            {
                string trinhDo = TrinhDoChuyenMon != null ? TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                string chuyenMon = ChuyenMonDaoTao != null ? ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                string truong = TruongDaoTao != null ? TruongDaoTao.TenTruongDaoTao : "";
                return string.Format("{0} {1} - {2}", trinhDo, chuyenMon, truong);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("DangKyDaoTao-ListChiTietDangKyDaoTao")]
        public XPCollection<ChiTietDangKyDaoTao> ListChiTietDangKyDaoTao
        {
            get
            {
                return GetCollection<ChiTietDangKyDaoTao>("ListChiTietDangKyDaoTao");
            }
        }

        public DangKyDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            QuocGia = HamDungChung.GetCurrentQuocGia(Session);
        }

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
    }

}
