using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.NangThamNien;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định nâng thâm niên tăng thêm")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhNangThamNienTangThem;ThongTinNhanVien")]
    public class ChiTietQuyetDinhNangThamNienTangThem : BaseObject
    {
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private QuyetDinhNangThamNienTangThem _QuyetDinhNangThamNienTangThem;
        private GiayToHoSo _GiayToHoSo;
        private HSLTangThemTheoThamNien _HSLTangThemTheoThamNienCu;
        private HSLTangThemTheoThamNien _HSLTangThemTheoThamNienMoi;
        private DateTime _MocHuongThamNienTangThemCu;
        private DateTime _MocHuongThamNienTangThemMoi;
        private NgachLuong _NgachLuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định nâng thâm niên tăng thêm")]
        [Association("QuyetDinhNangThamNienTangThem-ListChiTietQuyetDinhNangThamNienTangThem")]
        public QuyetDinhNangThamNienTangThem QuyetDinhNangThamNienTangThem
        {
            get
            {
                return _QuyetDinhNangThamNienTangThem;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangThamNienTangThem", ref _QuyetDinhNangThamNienTangThem, value);               
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
                    NgachLuong = ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong;
                }
            }
        }

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
            }
        }

        [ModelDefault("Caption", "Mốc hưởng TNTT cũ")]
        public DateTime MocHuongThamNienTangThemCu
        {
            get
            {
                return _MocHuongThamNienTangThemCu;
            }
            set
            {
                SetPropertyValue("MocHuongThamNienTangThemCu", ref _MocHuongThamNienTangThemCu, value);
                if (!IsLoading && value != DateTime.MinValue && HSLTangThemTheoThamNienCu != null)
                {
                    MocHuongThamNienTangThemMoi = value.AddYears(HSLTangThemTheoThamNienCu.ThoiGianNangBac);
                }
            }
        }

        [ModelDefault("Caption", "Hệ số TNTT cũ")]
        public HSLTangThemTheoThamNien HSLTangThemTheoThamNienCu
        {
            get
            {
                return _HSLTangThemTheoThamNienCu;
            }
            set
            {
                SetPropertyValue("HSLTangThemTheoThamNienCu", ref _HSLTangThemTheoThamNienCu, value);
                if (!IsLoading && value != null)
                {
                    HSLTangThemTheoThamNien hslTangThemTheoThamNien = Session.FindObject<HSLTangThemTheoThamNien>(CriteriaOperator.Parse("Bac=? ", value.Bac + 1));
                    if (hslTangThemTheoThamNien != null)
                    { HSLTangThemTheoThamNienMoi = hslTangThemTheoThamNien; }
                }
            }
        }

        [ModelDefault("Caption", "Mốc tính TNTT mới")]
        public DateTime MocHuongThamNienTangThemMoi
        {
            get
            {
                return _MocHuongThamNienTangThemMoi;
            }
            set
            {
                SetPropertyValue("MocHuongThamNienTangThemMoi", ref _MocHuongThamNienTangThemMoi, value);
            }
        }

        [ModelDefault("Caption", "Hệ số TNTT mới")]
        public HSLTangThemTheoThamNien HSLTangThemTheoThamNienMoi
        {
            get
            {
                return _HSLTangThemTheoThamNienMoi;
            }
            set
            {
                SetPropertyValue("HSLTangThemTheoThamNienMoi", ref _HSLTangThemTheoThamNienMoi, value);
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

        public ChiTietQuyetDinhNangThamNienTangThem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định nâng thâm niên tăng thêm"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (QuyetDinhNangThamNienTangThem != null
                && !IsLoading
                && !QuyetDinhNangThamNienTangThem.IsDirty)
                QuyetDinhNangThamNienTangThem.IsDirty = true;
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

            //if (!IsDeleted && Oid != Guid.Empty)
            //{
            //    //CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
            //}
        }

        protected override void OnDeleting()
        {
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
