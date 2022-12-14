using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DiNuocNgoai;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.DaoTao;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết công nhận học vị")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinNhanVien;QuyetDinhCongNhanHocVi")]
    //DLU
    public class ChiTietCongNhanHocVi : TruongBaseObject
    {
        //Cơ bản
        private QuyetDinhCongNhanHocVi _QuyetDinhCongNhanHocVi;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private TinhTrang _TinhTrangCu;
        private TinhTrang _TinhTrangMoi;
        private GiayToHoSo _GiayToHoSo;
                
        //Học vị cũ
        private TrinhDoChuyenMon _TrinhDoChuyenMonCu;
        private ChuyenMonDaoTao _ChuyenMonDaoTaoCu;
        private TruongDaoTao _TruongDaoTaoCu;
        private HinhThucDaoTao _HinhThucDaoTaoCu;
        private int _NamTotNghiepCu;
        private DateTime _NgayCapBangCu;

        //Học vị mới
        private TrinhDoChuyenMon _TrinhDoChuyenMonMoi;
        private ChuyenMonDaoTao _ChuyenMonDaoTaoMoi;
        private TruongDaoTao _TruongDaoTaoMoi;
        private HinhThucDaoTao _HinhThucDaoTaoMoi;
        private int _NamTotNghiepMoi;
        private DateTime _NgayCapBangMoi;

        //Từ ngày
        private DateTime _TuNgay;
        private string _GhiChu;

        #region Cơ bản
        [Browsable(false)]
        [Association("QuyetDinhCongNhanHocVi-ListChiTietCongNhanHocVi")]
        public QuyetDinhCongNhanHocVi QuyetDinhCongNhanHocVi
        {
            get
            {
                return _QuyetDinhCongNhanHocVi;
            }
            set
            {
                SetPropertyValue("QuyetDinhCongNhanHocVi", ref _QuyetDinhCongNhanHocVi, value);
                //if (!IsLoading && value != null)
                //{
                //    GiayToHoSo.SoGiayTo = value.SoQuyetDinh;
                //    GiayToHoSo.NgayBanHanh = value.NgayHieuLuc;
                //    GiayToHoSo.LuuTru = value.LuuTru;
                //    GiayToHoSo.TrichYeu = value.NoiDung;
                //}
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                    BoPhanText = value.TenBoPhan;
                }
            }
        }
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Bộ phận")]
        public string BoPhanText
        {
            get
            {
                return _BoPhanText;
            }
            set
            {
                SetPropertyValue("BoPhanText", ref _BoPhanText, value);
            }
        }
        [ImmediatePostData]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save)]
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    if (GiayToHoSo != null)
                        GiayToHoSo.HoSo = value;
                    TinhTrangCu = value.TinhTrang;

                    //Học vị cũ
                    TrinhDoChuyenMonCu = value.NhanVienTrinhDo.TrinhDoChuyenMon;
                    ChuyenMonDaoTaoCu = value.NhanVienTrinhDo.ChuyenMonDaoTao;
                    TruongDaoTaoCu = value.NhanVienTrinhDo.TruongDaoTao;
                    HinhThucDaoTaoCu = value.NhanVienTrinhDo.HinhThucDaoTao;
                    NamTotNghiepCu = value.NhanVienTrinhDo.NamTotNghiep;
                    NgayCapBangCu = value.NhanVienTrinhDo.NgayCapBang;
                }
            }
        }

        [Browsable(false)]
        public TinhTrang TinhTrangCu
        {
            get
            {
                return _TinhTrangCu;
            }
            set
            {
                SetPropertyValue("TinhTrangCu", ref _TinhTrangCu, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public TinhTrang TinhTrangMoi
        {
            get
            {
                return _TinhTrangMoi;
            }
            set
            {
                SetPropertyValue("TinhTrangMoi", ref _TinhTrangMoi, value);
            }
        }

        //[Aggregated]
        //[Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ")]
        //[ExpandObjectMembers(ExpandObjectMembers.Never)]
        //[ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        [DataSourceProperty("GiayToList", DataSourcePropertyIsNullMode.SelectAll)]
        public GiayToHoSo GiayToHoSo
        {
            get
            {
                return _GiayToHoSo;
            }
            set
            {
                SetPropertyValue("GiayToHoSo", ref _GiayToHoSo, value);
            }
        }
        #endregion

        #region Học vị cũ
        [ImmediatePostData]
        [ModelDefault("Caption", "Trình độ chuyên môn cũ")]
        public TrinhDoChuyenMon TrinhDoChuyenMonCu
        {
            get
            {
                return _TrinhDoChuyenMonCu;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMonCu", ref _TrinhDoChuyenMonCu, value);
            }
        }

        //toán, lý, hóa, sinh, cntt,...
        [Browsable(false)]
        [ModelDefault("Caption", "Chuyên ngành đào tạo cũ")]
        [ModelDefault("AllowEdit", "False")]
        public ChuyenMonDaoTao ChuyenMonDaoTaoCu
        {
            get
            {
                return _ChuyenMonDaoTaoCu;
            }
            set
            {
                SetPropertyValue("ChuyenMonDaoTaoCu", ref _ChuyenMonDaoTaoCu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Trường đào tạo cũ")]
        [ModelDefault("AllowEdit", "False")]
        public TruongDaoTao TruongDaoTaoCu
        {
            get
            {
                return _TruongDaoTaoCu;
            }
            set
            {
                SetPropertyValue("TruongDaoTaoCu", ref _TruongDaoTaoCu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Hình thức đào tạo cũ")]
        [ModelDefault("AllowEdit", "False")]
        public HinhThucDaoTao HinhThucDaoTaoCu
        {
            get
            {
                return _HinhThucDaoTaoCu;
            }
            set
            {
                SetPropertyValue("HinhThucDaoTaoCu", ref _HinhThucDaoTaoCu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Năm tốt nghiệp cũ")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamTotNghiepCu
        {
            get
            {
                return _NamTotNghiepCu;
            }
            set
            {
                SetPropertyValue("NamTotNghiepCu", ref _NamTotNghiepCu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Ngày cấp bằng cũ")]
        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayCapBangCu
        {
            get
            {
                return _NgayCapBangCu;
            }
            set
            {
                SetPropertyValue("NgayCapBangCu", ref _NgayCapBangCu, value);
            }
        }
        #endregion

        #region Học vị mới
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Trình độ chuyên môn mới")]
        public TrinhDoChuyenMon TrinhDoChuyenMonMoi
        {
            get
            {
                return _TrinhDoChuyenMonMoi;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMonMoi", ref _TrinhDoChuyenMonMoi, value);
            }
        }

        //toán, lý, hóa, sinh, cntt,...
        [ModelDefault("Caption", "Chuyên ngành đào tạo mới")]
        public ChuyenMonDaoTao ChuyenMonDaoTaoMoi
        {
            get
            {
                return _ChuyenMonDaoTaoMoi;
            }
            set
            {
                SetPropertyValue("ChuyenMonDaoTaoMoi", ref _ChuyenMonDaoTaoMoi, value);
            }
        }

        [ModelDefault("Caption", "Trường đào tạo mới")]
        public TruongDaoTao TruongDaoTaoMoi
        {
            get
            {
                return _TruongDaoTaoMoi;
            }
            set
            {
                SetPropertyValue("TruongDaoTaoMoi", ref _TruongDaoTaoMoi, value);
            }
        }

        [ModelDefault("Caption", "Hình thức đào tạo mới")]
        public HinhThucDaoTao HinhThucDaoTaoMoi
        {
            get
            {
                return _HinhThucDaoTaoMoi;
            }
            set
            {
                SetPropertyValue("HinhThucDaoTaoMoi", ref _HinhThucDaoTaoMoi, value);
            }
        }

        [ModelDefault("Caption", "Năm tốt nghiệp mới")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamTotNghiepMoi
        {
            get
            {
                return _NamTotNghiepMoi;
            }
            set
            {
                SetPropertyValue("NamTotNghiepMoi", ref _NamTotNghiepMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp bằng mới")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayCapBangMoi
        {
            get
            {
                return _NgayCapBangMoi;
            }
            set
            {
                SetPropertyValue("NgayCapBangMoi", ref _NgayCapBangMoi, value);
            }
        }
        #endregion

        [ModelDefault("Caption", "Công nhận từ ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [Size(-1)]
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

        public ChiTietCongNhanHocVi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định công nhận học vị và cấp bằng"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            TinhTrangMoi = HoSoHelper.DangLamViec(Session);
            TuNgay = HamDungChung.GetServerTime();
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
            UpdateGiayToList();
            if (BoPhan != null)
            {
                BoPhanText = BoPhan.TenBoPhan;
            } 
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        [Browsable(false)]
        public XPCollection<GiayToHoSo> GiayToList { get; set; }
        private void UpdateGiayToList()
        {
            //if (GiayToList == null)
            //    GiayToList = new XPCollection<GiayToHoSo>(Session);

            if (ThongTinNhanVien != null)
                GiayToList = ThongTinNhanVien.ListGiayToHoSo;
            //    GiayToList.Criteria = CriteriaOperator.Parse("HoSo=? and GiayTo.TenGiayTo like ?", ThongTinNhanVien.Oid, "%Quyết định%");
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            //Oid != Guid.Empty -> bị trường hợp đã save rồi sửa không dc
            //(Session is NestedUnitOfWork) -> bị trường hợp sửa form cha/ tạo tự động chi tiết từ quyết định
            if (!IsDeleted && (Oid == Guid.Empty || Session is NestedUnitOfWork))
            {
                //1. Tình trạng
                if (TuNgay <= HamDungChung.GetServerTime() && QuyetDinhCongNhanHocVi.QuyetDinhMoi)
                {
                    ThongTinNhanVien.TinhTrang = TinhTrangMoi != null ? TinhTrangMoi : ThongTinNhanVien.TinhTrang;
                    ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = TrinhDoChuyenMonMoi != null ? TrinhDoChuyenMonMoi : ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon;
                    ThongTinNhanVien.NhanVienTrinhDo.ChuyenMonDaoTao = ChuyenMonDaoTaoMoi != null ? ChuyenMonDaoTaoMoi : ThongTinNhanVien.NhanVienTrinhDo.ChuyenMonDaoTao;
                    ThongTinNhanVien.NhanVienTrinhDo.TruongDaoTao = TruongDaoTaoMoi != null ? TruongDaoTaoMoi : ThongTinNhanVien.NhanVienTrinhDo.TruongDaoTao;
                    ThongTinNhanVien.NhanVienTrinhDo.HinhThucDaoTao = HinhThucDaoTaoMoi != null ? HinhThucDaoTaoMoi : ThongTinNhanVien.NhanVienTrinhDo.HinhThucDaoTao;
                    ThongTinNhanVien.NhanVienTrinhDo.NamTotNghiep = NamTotNghiepMoi;
                    ThongTinNhanVien.NhanVienTrinhDo.NgayCapBang = NgayCapBangMoi;
                    // Xoa dang theo hoc
                    DaoTaoHelper.ResetDangTheoHoc(ThongTinNhanVien);
                    // Tao van bang
                    DaoTaoHelper.CreateVanBang(Session, ThongTinNhanVien, TrinhDoChuyenMonMoi, ChuyenMonDaoTaoMoi,
                        TruongDaoTaoMoi, HinhThucDaoTaoMoi, NamTotNghiepMoi, NgayCapBangMoi);
                }
                //Quá trình đào tạo
                if (QuyetDinhCongNhanHocVi.QuyetDinhDaoTao != null)
                {
                    QuaTrinhHelper.CreateQuaTrinhDaoTao(Session, QuyetDinhCongNhanHocVi.QuyetDinhDaoTao, ThongTinNhanVien);
                    //5. quá trình đi nước ngoài
                    if (QuyetDinhCongNhanHocVi.QuyetDinhDaoTao.QuocGia != null
                        && HamDungChung.CauHinhChung.QuocGia != null
                        && QuyetDinhCongNhanHocVi.QuyetDinhDaoTao.QuocGia.Oid != HamDungChung.CauHinhChung.QuocGia.Oid)
                        QuaTrinhHelper.UpdateQuaTrinhDiNuocNgoai(Session, QuyetDinhCongNhanHocVi.QuyetDinhDaoTao, ThongTinNhanVien, TuNgay);

                }
                else
                {
                    QuaTrinhHelper.CreateQuaTrinhDaoTao(Session, ThongTinNhanVien, TrinhDoChuyenMonMoi, ChuyenMonDaoTaoMoi,
                        TruongDaoTaoMoi, HinhThucDaoTaoMoi, NamTotNghiepMoi, NgayCapBangMoi);
                }
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                if (TuNgay <= HamDungChung.GetServerTime() && QuyetDinhCongNhanHocVi.QuyetDinhMoi)
                {
                    //1. tình trạng
                    ThongTinNhanVien.TinhTrang = TinhTrangCu != null ? TinhTrangCu : ThongTinNhanVien.TinhTrang;
                    ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = TrinhDoChuyenMonCu != null ? TrinhDoChuyenMonCu : null;
                    ThongTinNhanVien.NhanVienTrinhDo.ChuyenMonDaoTao = ChuyenMonDaoTaoCu != null ? ChuyenMonDaoTaoCu : null;
                    ThongTinNhanVien.NhanVienTrinhDo.TruongDaoTao = TruongDaoTaoCu != null ? TruongDaoTaoCu : null;
                    ThongTinNhanVien.NhanVienTrinhDo.HinhThucDaoTao = HinhThucDaoTaoCu != null ? HinhThucDaoTaoCu : null;
                    ThongTinNhanVien.NhanVienTrinhDo.NamTotNghiep = NamTotNghiepCu;
                    ThongTinNhanVien.NhanVienTrinhDo.NgayCapBang = NgayCapBangCu;
                    // dang theo hoc
                    DaoTaoHelper.CreateDangTheoHoc(Session, ThongTinNhanVien, QuyetDinhCongNhanHocVi.QuyetDinhDaoTao.TrinhDoChuyenMon, QuyetDinhCongNhanHocVi.QuyetDinhDaoTao.QuocGia);
                    // Tao van bang
                    DaoTaoHelper.DeleteVanBang(Session, ThongTinNhanVien, TrinhDoChuyenMonMoi, ChuyenMonDaoTaoMoi,
                        TruongDaoTaoMoi, HinhThucDaoTaoMoi, NamTotNghiepMoi, NgayCapBangMoi);
                }
                if (QuyetDinhCongNhanHocVi.QuyetDinhDaoTao != null)
                {
                    //Quá trình đào tạo
                    QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhDaoTao>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhCongNhanHocVi.QuyetDinhDaoTao, ThongTinNhanVien));
                    //5. quá trình đi nước ngoài
                    QuaTrinhHelper.ResetQuaTrinhDiNuocNgoai(Session, QuyetDinhCongNhanHocVi.QuyetDinhDaoTao, ThongTinNhanVien);
                }
                else
                {
                    //Quá trình đào tạo
                    QuaTrinhHelper.DeleteQuaTrinhDaoTao(Session, ThongTinNhanVien, TrinhDoChuyenMonMoi, ChuyenMonDaoTaoMoi,
                        TruongDaoTaoMoi, HinhThucDaoTaoMoi, NamTotNghiepMoi, NgayCapBangMoi);
                }
            }
            
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }

            base.OnDeleting();
        }
    }
}
