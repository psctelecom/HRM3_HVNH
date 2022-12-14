using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.GiayTo;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết công nhận học hàm")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinNhanVien;QuyetDinhCongNhanHocHam")]
    //DLU
    public class ChiTietCongNhanHocHam : TruongBaseObject
    {
        //Cơ bản
        private QuyetDinhCongNhanHocHam _QuyetDinhCongNhanHocHam;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private GiayToHoSo _GiayToHoSo;
                
        //Học hàm cũ
        private HocHam _HocHamCu;      
        private DateTime _NgayCongNhanCu;

        //Học vị mới
        private HocHam _HocHamMoi;      
        private DateTime _NgayCongNhanMoi;
       
        private string _GhiChu;

        #region Cơ bản
        [Browsable(false)]
        [Association("QuyetDinhCongNhanHocHam-ListChiTietCongNhanHocHam")]
        public QuyetDinhCongNhanHocHam QuyetDinhCongNhanHocHam
        {
            get
            {
                return _QuyetDinhCongNhanHocHam;
            }
            set
            {
                SetPropertyValue("QuyetDinhCongNhanHocHam", ref _QuyetDinhCongNhanHocHam, value);
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

                    //Học hàm cũ                   
                    HocHamCu = value.NhanVienTrinhDo.HocHam;
                    NgayCongNhanCu = value.NhanVienTrinhDo.NgayCongNhanHocHam;
                }
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

        #region Học hàm cũ
        [ImmediatePostData]
        [ModelDefault("Caption", "Học hàm cũ")]
        public HocHam HocHamCu
        {
            get
            {
                return _HocHamCu;
            }
            set
            {
                SetPropertyValue("HocHamCu", ref _HocHamCu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Ngày công nhận cũ")]
        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayCongNhanCu
        {
            get
            {
                return _NgayCongNhanCu;
            }
            set
            {
                SetPropertyValue("NgayCongNhanCu", ref _NgayCongNhanCu, value);
            }
        }
        #endregion

        #region Học hàm mới
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Học hàm mới")]
        public HocHam HocHamMoi
        {
            get
            {
                return _HocHamMoi;
            }
            set
            {
                SetPropertyValue("HocHamMoi", ref _HocHamMoi, value);
            }
        }       

        [ModelDefault("Caption", "Ngày công nhận mới")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayCongNhanMoi
        {
            get
            {
                return _NgayCongNhanMoi;
            }
            set
            {
                SetPropertyValue("NgayCongNhanMoi", ref _NgayCongNhanMoi, value);
            }
        }
        #endregion       

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

        public ChiTietCongNhanHocHam(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định công nhận học hàm (GS, PGS)"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            NgayCongNhanMoi = HamDungChung.GetServerTime();
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
                if (NgayCongNhanMoi <= HamDungChung.GetServerTime() && QuyetDinhCongNhanHocHam.QuyetDinhMoi)
                {
                    ThongTinNhanVien.NhanVienTrinhDo.HocHam = HocHamMoi;
                    ThongTinNhanVien.NhanVienTrinhDo.NgayCongNhanHocHam = NgayCongNhanMoi;
                    ThongTinNhanVien.NhanVienTrinhDo.NamCongNhanHocHam = NgayCongNhanMoi.Year;
                    ThongTinNhanVien.NhanVienTrinhDo.NgayCongTac = NgayCongNhanMoi;                   
                    ThongTinNhanVien.NhanVienTrinhDo.NgayHuongCheDo = NgayCongNhanMoi;
                    ThongTinNhanVien.NhanVienTrinhDo.NgayPhongDanhHieu = NgayCongNhanMoi;                                       
                }                
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                if (NgayCongNhanMoi <= HamDungChung.GetServerTime() && QuyetDinhCongNhanHocHam.QuyetDinhMoi)
                {
                    //1. tình trạng
                    ThongTinNhanVien.NhanVienTrinhDo.HocHam = HocHamCu;
                    ThongTinNhanVien.NhanVienTrinhDo.NgayCongNhanHocHam = NgayCongNhanCu;
                    ThongTinNhanVien.NhanVienTrinhDo.NamCongNhanHocHam = NgayCongNhanCu != DateTime.MinValue ? NgayCongNhanCu.Year : 0;
                    ThongTinNhanVien.NhanVienTrinhDo.NgayCongTac = NgayCongNhanCu;                    
                    ThongTinNhanVien.NhanVienTrinhDo.NgayHuongCheDo = NgayCongNhanCu;
                    ThongTinNhanVien.NhanVienTrinhDo.NgayPhongDanhHieu = NgayCongNhanCu;
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
