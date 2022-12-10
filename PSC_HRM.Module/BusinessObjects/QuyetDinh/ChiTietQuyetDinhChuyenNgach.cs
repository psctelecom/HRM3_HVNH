using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.ChuyenNgach;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định chuyển ngạch")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhChuyenNgach;ThongTinNhanVien")]
    public class ChiTietQuyetDinhChuyenNgach : BaseObject
    {
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private ThongTinNhanVien _ThongTinNhanVien;
        private QuyetDinhChuyenNgach _QuyetDinhChuyenNgach;
        private GiayToHoSo _GiayToHoSo;

        private DateTime _NgayBoNhiemNgachCu;
        private int _VuotKhungCu;
        
        private NgachLuong _NgachLuongCu;
        private BacLuong _BacLuongCu;
        private decimal _HeSoLuongCu;
        private DateTime _NgayHuongLuongCu;
        private DateTime _MocNangLuongCu;
        private ChucDanh _ChucDanhCu;//Thảo thêm
        
        private NgachLuong _NgachLuongMoi;
        private BacLuong _BacLuongMoi;
        private decimal _HeSoLuongMoi;
        private DateTime _NgayHuongLuongMoi;
        private DateTime _MocNangLuongMoi;
        private ChucDanh _ChucDanhMoi;//Thảo thêm
        
        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định chuyển ngạch")]
        [Association("QuyetDinhChuyenNgach-ListChiTietQuyetDinhChuyenNgach")]
        public QuyetDinhChuyenNgach QuyetDinhChuyenNgach
        {
            get
            {
                return _QuyetDinhChuyenNgach;
            }
            set
            {
                SetPropertyValue("QuyetDinhChuyenNgach", ref _QuyetDinhChuyenNgach, value);
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
                    if (GiayToHoSo != null)
                        GiayToHoSo.HoSo = value;
                    AfterNhanVienChanged();
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                }                
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương cũ")]
        public NgachLuong NgachLuongCu
        {
            get
            {
                return _NgachLuongCu;
            }
            set
            {
                SetPropertyValue("NgachLuongCu", ref _NgachLuongCu, value);
                if (!IsLoading)
                {
                    BacLuongCu = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương cũ")]
        [DataSourceProperty("NgachLuongCu.ListBacLuong")]
        public BacLuong BacLuongCu
        {
            get
            {
                return _BacLuongCu;
            }
            set
            {
                SetPropertyValue("BacLuongCu", ref _BacLuongCu, value);
                if (!IsLoading && value != null)
                {
                    HeSoLuongCu = value.HeSoLuong;
                }
            }
        }

        [ModelDefault("Caption", "Hệ số lương cũ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLuongCu
        {
            get
            {
                return _HeSoLuongCu;
            }
            set
            {
                SetPropertyValue("HeSoLuongCu", ref _HeSoLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương cũ")]
        public DateTime NgayHuongLuongCu
        {
            get
            {
                return _NgayHuongLuongCu;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongCu", ref _NgayHuongLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương cũ")]
        public DateTime MocNangLuongCu
        {
            get
            {
                return _MocNangLuongCu;
            }
            set
            {
                SetPropertyValue("MocNangLuongCu", ref _MocNangLuongCu, value);
            }
        }
        [ModelDefault("Caption", "Chức danh cũ")]
        public ChucDanh ChucDanhCu
        {
            get
            {
                return _ChucDanhCu;
            }
            set
            {
                SetPropertyValue("ChucDanhCu", ref _ChucDanhCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NgachLuong NgachLuongMoi
        {
            get
            {
                return _NgachLuongMoi;
            }
            set
            {
                SetPropertyValue("NgachLuongMoi", ref _NgachLuongMoi, value);
                if (!IsLoading)
                {
                    BacLuongMoi = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương mới")]
        [DataSourceProperty("NgachLuongMoi.ListBacLuong")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BacLuong BacLuongMoi
        {
            get
            {
                return _BacLuongMoi;
            }
            set
            {
                SetPropertyValue("BacLuongMoi", ref _BacLuongMoi, value);
                if (!IsLoading && value != null)
                {
                    HeSoLuongMoi = value.HeSoLuong;
                }
            }
        }

        [ModelDefault("Caption", "Hệ số lương mới")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLuongMoi
        {
            get
            {
                return _HeSoLuongMoi;
            }
            set
            {
                SetPropertyValue("HeSoLuongMoi", ref _HeSoLuongMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongLuongMoi
        {
            get
            {
                return _NgayHuongLuongMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongMoi", ref _NgayHuongLuongMoi, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime MocNangLuongMoi
        {
            get
            {
                return _MocNangLuongMoi;
            }
            set
            {
                SetPropertyValue("MocNangLuongMoi", ref _MocNangLuongMoi, value);
            }
        }
        [ModelDefault("Caption", "Chức danh mới")]
        public ChucDanh ChucDanhMoi
        {
            get
            {
                return _ChucDanhMoi;
            }
            set
            {
                SetPropertyValue("ChucDanhMoi", ref _ChucDanhMoi, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Ngày bổ nhiệm cũ")]
        public DateTime NgayBoNhiemNgachCu
        {
            get
            {
                return _NgayBoNhiemNgachCu;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemNgachCu", ref _NgayBoNhiemNgachCu, value);
            }
        }

        [ModelDefault("Caption", "% vuợt khung cũ")]
        public int VuotKhungCu
        {
            get
            {
                return _VuotKhungCu;
            }
            set
            {
                SetPropertyValue("VuotKhungCu", ref _VuotKhungCu, value);
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

        public ChiTietQuyetDinhChuyenNgach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định chuyển ngạch"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            NgayHuongLuongMoi = QuyetDinhChuyenNgach != null ? QuyetDinhChuyenNgach.NgayHieuLuc : DateTime.MinValue;
            UpdateNhanVienList();
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

        private void AfterNhanVienChanged()
        {
            NgachLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong;
            BacLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.BacLuong;
            HeSoLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
            MocNangLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong;
            NgayHuongLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
            NgayBoNhiemNgachCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach;
            ChucDanhCu = ThongTinNhanVien.ChucDanh;
            if (NgachLuongCu != null)
                MocNangLuongMoi = MocNangLuongCu.AddMonths(NgachLuongCu.ThoiGianNangBac);
            else
                MocNangLuongMoi = MocNangLuongCu;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted
                && (Oid == Guid.Empty || Session is NestedUnitOfWork))
            {
                //1. cập nhật hồ sơ
                if (QuyetDinhChuyenNgach.QuyetDinhMoi)
                {
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = NgayHuongLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong = HeSoLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong = MocNangLuongMoi;
                    ThongTinNhanVien.ChucDanh = ChucDanhMoi;
                }

                //2. update dien bien luong
                QuaTrinhHelper.UpdateDienBienLuong(Session, QuyetDinhChuyenNgach, ThongTinNhanVien, NgayHuongLuongMoi);

                //3. tạo diễn biến lương
                QuaTrinhHelper.CreateDienBienLuong(Session, QuyetDinhChuyenNgach, ThongTinNhanVien, NgayHuongLuongMoi,this);
            }
        }

        protected override void OnDeleting()
        {
            //1. reset ho so
            if (QuyetDinhChuyenNgach.QuyetDinhMoi
                && NgayHuongLuongMoi != DateTime.MinValue)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay>?",
                    ThongTinNhanVien, NgayHuongLuongMoi);
                SortProperty sort = new SortProperty("TuNgay", SortingDirection.Descending);
                using (XPCollection<DienBienLuong> dblList = new XPCollection<DienBienLuong>(Session, filter, sort))
                {
                    dblList.TopReturnedObjects = 1;
                    //Quyết định còn hiệu lực nhất
                    if (dblList.Count == 0 ||
                        (dblList.Count == 1 && dblList[0].QuyetDinh == QuyetDinhChuyenNgach))
                    {
                        ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuongCu;
                        ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongCu;
                        ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong = HeSoLuongCu;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongCu;
                        ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong = MocNangLuongCu;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = NgayBoNhiemNgachCu;
                        ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung = VuotKhungCu;
                        ThongTinNhanVien.ChucDanh = ChucDanhCu;
                    }
                }
            }

            //3. xóa diễn biến lương
            QuaTrinhHelper.DeleteQuaTrinh<DienBienLuong>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhChuyenNgach, ThongTinNhanVien));

            //2. reset dien bien luong
            QuaTrinhHelper.ResetDienBienLuong(Session, ThongTinNhanVien);

            //xóa giấy tờ hồ sơ
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }

            base.OnDeleting();
        }
    }

}
