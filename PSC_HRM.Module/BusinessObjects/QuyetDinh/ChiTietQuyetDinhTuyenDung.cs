using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.QuyetDinhService;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhTuyenDung;ThongTinNhanVien")]
    public class ChiTietQuyetDinhTuyenDung : TruongBaseObject
    {
        // Fields...
        private DateTime _NgayBoNhiemNgach;
        private int _ThoiGianTapSu = 12;
        private DateTime _NgayHuongLuong;
        private bool _Huong85PhanTramLuong = true;
        private decimal _HeSoLuong;
        private BacLuong _BacLuong;
        private NgachLuong _NgachLuong;
        private QuyetDinhTuyenDung _QuyetDinhTuyenDung;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private GiayToHoSo _GiayToHoSo;
        private string _GhiChu;

        [Browsable(false)]
        [Association("QuyetDinhTuyenDung-ListChiTietQuyetDinhTuyenDung")]
        public QuyetDinhTuyenDung QuyetDinhTuyenDung
        {
            get
            {
                return _QuyetDinhTuyenDung;
            }
            set
            {
                SetPropertyValue("QuyetDinhTuyenDung", ref _QuyetDinhTuyenDung, value);
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
                    NgachLuong = value.NhanVienThongTinLuong.NgachLuong;
                    BacLuong = value.NhanVienThongTinLuong.BacLuong;
                    HeSoLuong = value.NhanVienThongTinLuong.HeSoLuong;
                    Huong85PhanTramLuong = value.NhanVienThongTinLuong.Huong85PhanTramLuong;
                    NgayHuongLuong = value.NhanVienThongTinLuong.NgayHuongLuong;
                    NgayBoNhiemNgach = value.NhanVienThongTinLuong.NgayBoNhiemNgach;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
                if (!IsLoading)
                    BacLuong = null;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương")]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
        public BacLuong BacLuong
        {
            get
            {
                return _BacLuong;
            }
            set
            {
                SetPropertyValue("BacLuong", ref _BacLuong, value);
                if (!IsLoading && value != null)
                    HeSoLuong = value.HeSoLuong;
            }
        }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        //[Appearance("HeSoLuong_LUH", TargetItems = "HeSoLuong", Enabled = false, Criteria = "MaTruong !='LUH'")]
        //[ModelDefault("AllowEdit", "False")]
        public decimal HeSoLuong
        {
            get
            {
                return _HeSoLuong;
            }
            set
            {
                SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);
            }
        }

        [ModelDefault("Caption", "Hưởng 85% lương")]
        public bool Huong85PhanTramLuong
        {
            get
            {
                return _Huong85PhanTramLuong;
            }
            set
            {
                SetPropertyValue("Huong85PhanTramLuong", ref _Huong85PhanTramLuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày hưởng lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongLuong
        {
            get
            {
                return _NgayHuongLuong;
            }
            set
            {
                SetPropertyValue("NgayHuongLuong", ref _NgayHuongLuong, value);
                if (!IsLoading && NgayHuongLuong != DateTime.MinValue && ThoiGianTapSu > 0)
                {
                    NgayBoNhiemNgach = NgayHuongLuong.AddMonths(ThoiGianTapSu).AddDays(-1);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thời gian tập sự (tháng)")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int ThoiGianTapSu
        {
            get
            {
                return _ThoiGianTapSu;
            }
            set
            {
                SetPropertyValue("ThoiGianTapSu", ref _ThoiGianTapSu, value);
                if (!IsLoading && NgayHuongLuong != DateTime.MinValue && ThoiGianTapSu > 0)
                {
                    NgayBoNhiemNgach = NgayHuongLuong.AddMonths(ThoiGianTapSu).AddDays(-1);
                }
            }
        }

        [ModelDefault("Caption", "Ngày kết thúc tập sự")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayBoNhiemNgach
        {
            get
            {
                return _NgayBoNhiemNgach;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemNgach", ref _NgayBoNhiemNgach, value);
                if(!IsLoading && value!=DateTime.MinValue)
                    if (ThongTinNhanVien.BienChe)
                        ThongTinNhanVien.NgayVaoBienChe = QuyetDinhTuyenDung.NgayHieuLuc == DateTime.MinValue ? NgayBoNhiemNgach : QuyetDinhTuyenDung.NgayHieuLuc;
            }
        }

        [Aggregated]
        [Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
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

        public ChiTietQuyetDinhTuyenDung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định tuyển dụng"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (QuyetDinhTuyenDung != null
                && !IsLoading
                && !QuyetDinhTuyenDung.IsDirty)
                QuyetDinhTuyenDung.IsDirty = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
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

        protected override void OnSaving()
        {
            base.OnSaving();

            SystemContainer.Resolver<IQuyetDinhTuyenDungService>("QDTuyenDung" + TruongConfig.MaTruong).Save(Session, this);
        }

        protected override void OnDeleting()
        {
            SystemContainer.Resolver<IQuyetDinhTuyenDungService>("QDTuyenDung" + TruongConfig.MaTruong).Delete(Session, this);

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
