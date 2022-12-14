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

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định đào tạo")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinNhanVien;QuyetDinhDaoTao")]
    public class ChiTietDaoTao : BaseObject
    {
        private QuyetDinhDaoTao _QuyetDinhDaoTao;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private ThongTinNhanVien _ThongTinNhanVien;
        private ChuyenMonDaoTao _ChuyenMonDaoTao;
        private TinhTrang _TinhTrang;
        private TinhTrang _TinhTrangMoi;
        private bool _DuocHuongLuongKhiDiHoc;
        private GiayToHoSo _GiayToHoSo; 
        
        [Browsable(false)]
        [Association("QuyetDinhDaoTao-ListChiTietDaoTao")]
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
                    //GiayToHoSo.SoGiayTo = value.SoQuyetDinh;
                    //GiayToHoSo.NgayBanHanh = value.NgayHieuLuc;
                    //GiayToHoSo.LuuTru = value.LuuTru;
                    //GiayToHoSo.TrichYeu = value.NoiDung;
                    //
                    UpdateTinhTrangList();
                }
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
                    TinhTrang = value.TinhTrang;

                    if (value.LoaiNhanVien != null
                        && value.LoaiNhanVien.TenLoaiNhanVien.ToLower().Contains("tập sự"))
                        DuocHuongLuongKhiDiHoc = false;
                }                
            }
        }

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        [DataSourceProperty("QuyetDinhDaoTao.NganhDaoTao.ListChuyenNganhDaoTao", DataSourcePropertyIsNullMode.SelectAll)]
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

        [ModelDefault("Caption", "Được hưởng lương khi đi học")]
        [Browsable(false)]
        public bool DuocHuongLuongKhiDiHoc
        {
            get
            {
                return _DuocHuongLuongKhiDiHoc;
            }
            set
            {
                SetPropertyValue("DuocHuongLuongKhiDiHoc", ref _DuocHuongLuongKhiDiHoc, value);
            }
        }

        [ImmediatePostData]
        [DataSourceProperty("TinhTrangList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Tình trạng hưởng lương")] 
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

        [Browsable(false)]
        public TinhTrang TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        [NonPersistent]
        private string MaTruong { get; set; }

        public ChiTietDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DuocHuongLuongKhiDiHoc = true;
            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định đào tạo"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            MaTruong = TruongConfig.MaTruong;
            UpdateNhanVienList();          
        }

        //protected override void OnChanged(string propertyName, object oldValue, object newValue)
        //{
        //    base.OnChanged(propertyName, oldValue, newValue);

        //    if (QuyetDinhDaoTao != null
        //        && !IsLoading
        //        && !QuyetDinhDaoTao.IsDirty)
        //        QuyetDinhDaoTao.IsDirty = true;
        //}

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
            UpdateTinhTrangList();
            MaTruong = TruongConfig.MaTruong;
            UpdateGiayToList();
            //if (BoPhan != null)
            //{
            //    BoPhanText = BoPhan.TenBoPhan;
            //} 
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        [Browsable(false)]
        public XPCollection<TinhTrang> TinhTrangList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        private void UpdateTinhTrangList()
        {
            if (TinhTrangList == null)
                TinhTrangList = new XPCollection<TinhTrang>(Session);
            if (QuyetDinhDaoTao.QuocGia != null)
            {
                if (QuyetDinhDaoTao.QuocGia.TenQuocGia.Contains("Việt Nam"))
                    TinhTrangList.Criteria = CriteriaOperator.Parse("LoaiTinhTrang = ? OR LoaiTinhTrang = ?", Trong_NgoaiNuocEnum.TrongNuoc, Trong_NgoaiNuocEnum.CaHai);
                else
                    TinhTrangList.Criteria = CriteriaOperator.Parse("LoaiTinhTrang = ? OR LoaiTinhTrang = ?", Trong_NgoaiNuocEnum.NgoaiNuoc, Trong_NgoaiNuocEnum.CaHai);
            }
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

            if (!IsDeleted &&
                Oid != Guid.Empty)
            {
                if (QuyetDinhDaoTao.TuNgay <= HamDungChung.GetServerTime() && QuyetDinhDaoTao.QuyetDinhMoi)
                {
                    if (TinhTrangMoi != null)
                    {
                        //2. Tình trạng
                        ThongTinNhanVien.TinhTrang = TinhTrangMoi;

                        //1. đang theo học
                        DaoTaoHelper.CreateDangTheoHoc(Session, ThongTinNhanVien, QuyetDinhDaoTao.TrinhDoChuyenMon, QuyetDinhDaoTao.QuocGia);
                    }
                }
                //3. quản lý biến động
                //lao động giảm: đi học không lương khi người đi học đang tập sự hoặc đi học tự túc
                if (QuyetDinhDaoTao.NgayPhatSinhBienDong != DateTime.MinValue &&
                   !DuocHuongLuongKhiDiHoc && BienDongHelper.IsExistsHoSoBaoHiem(Session, ThongTinNhanVien))
                {
                    BienDongHelper.CreateBienDongGiamLaoDong(Session, QuyetDinhDaoTao, BoPhan, ThongTinNhanVien, QuyetDinhDaoTao.NgayPhatSinhBienDong, LyDoNghiEnum.NghiKhongLuong);
                }

                //4. qua trinh di nuoc ngoai
                if (DiNuocNgoaiHelper.IsNgoaiNuoc(QuyetDinhDaoTao.QuocGia))
                    QuaTrinhHelper.CreateQuaTrinhDiNuocNgoai(Session, QuyetDinhDaoTao, ThongTinNhanVien, QuyetDinhDaoTao.QuocGia, QuyetDinhDaoTao.NgayHieuLuc, DateTime.MinValue);
            }
        }

        protected override void OnDeleting()
        {
            //1. đang theo học
            DaoTaoHelper.ResetDangTheoHoc(ThongTinNhanVien);

            //2. tình trạng
            ThongTinNhanVien.TinhTrang = TinhTrang;

            //3. delete biến động
            if (QuyetDinhDaoTao.NgayPhatSinhBienDong != DateTime.MinValue)
                BienDongHelper.DeleteBienDong<BienDong_GiamLaoDong>(Session, ThongTinNhanVien, QuyetDinhDaoTao.NgayPhatSinhBienDong);
            
            //4. delete quá trình đi nước ngoài
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinh=?",
                    ThongTinNhanVien, Oid);
            QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhDiNuocNgoai>(Session, filter);

            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }

            base.OnDeleting();
        }
    }
}
