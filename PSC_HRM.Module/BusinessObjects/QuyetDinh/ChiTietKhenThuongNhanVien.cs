using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.GiayTo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.KhenThuong;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Chi tiết khen thưởng nhân viên")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhKhenThuong;ThongTinNhanVien")]
    [Appearance("Hide_GTVT", TargetItems = "DanhHieuKhenThuong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong !='GTVT'")]
    public class ChiTietKhenThuongNhanVien : BaseObject, IBoPhan
    {
        private QuyetDinhKhenThuong _QuyetDinhKhenThuong;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private ThongTinNhanVien _ThongTinNhanVien;
        private GiayToHoSo _GiayToHoSo;
        private DanhHieuKhenThuong _DanhHieuKhenThuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định khen thưởng")]
        [Association("QuyetDinhKhenThuong-ListChiTietKhenThuongNhanVien")]
        public QuyetDinhKhenThuong QuyetDinhKhenThuong
        {
            get
            {
                return _QuyetDinhKhenThuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhKhenThuong", ref _QuyetDinhKhenThuong, value);
                //if (!IsLoading && value != null)
                //{
                //    GiayToHoSo.SoGiayTo = value.SoQuyetDinh;
                //    GiayToHoSo.NgayBanHanh = value.NgayHieuLuc;
                //    GiayToHoSo.LuuTru = value.LuuTru;
                //    GiayToHoSo.TrichYeu = value.NoiDung;                    
               // }
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
                    UpdateNVList();
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

        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Danh hiệu")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong ='GTVT'")]
        public DanhHieuKhenThuong DanhHieuKhenThuong
        {
            get
            {
                return _DanhHieuKhenThuong;
            }
            set
            {
                SetPropertyValue("DanhHieuKhenThuong", ref _DanhHieuKhenThuong, value);
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

        [NonPersistent]
        [Browsable(false)]
        private string MaTruong { get; set; }

        public ChiTietKhenThuongNhanVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (QuyetDinhKhenThuong != null)
            {
                DanhHieuKhenThuong = this.QuyetDinhKhenThuong.DanhHieuKhenThuong;
            }
            MaTruong = TruongConfig.MaTruong;
            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định khen thưởng"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNVList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (QuyetDinhKhenThuong != null
                && !IsLoading
                && !QuyetDinhKhenThuong.IsDirty)
                QuyetDinhKhenThuong.IsDirty = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (BoPhan != null)
            {
                BoPhanText = BoPhan.TenBoPhan;
            }              
            UpdateNVList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted &&
                Oid != Guid.Empty && Session is NestedUnitOfWork)
            {
                //2. Tạo quá trình khen thưởng
                QuaTrinhHelper.CreateQuaTrinhKhenThuong(Session, ThongTinNhanVien, QuyetDinhKhenThuong);
            }
        }

        protected override void OnDeleting()
        {
            //1. Xoa qua trinh khen thuong
            QuaTrinhHelper.DeleteQuaTrinhHoSo<QuaTrinhKhenThuong>(Session, ThongTinNhanVien, QuyetDinhKhenThuong);

            //xoa giay to ho so
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }

            base.OnDeleting();
        }
    }

}
