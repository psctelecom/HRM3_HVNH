using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.GiayTo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.QuyetDinhService;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định hướng dẫn tập sự")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhHuongDanTapSu,ThongTinNhanVien")]
    public class ChiTietQuyetDinhHuongDanTapSu : TruongBaseObject
    {
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private QuyetDinhHuongDanTapSu _QuyetDinhHuongDanTapSu;
        private GiayToHoSo _GiayToHoSo;
        private BoPhan _BoPhanCanBoHuongDan;
        private ThongTinNhanVien _CanBoHuongDan;
        private DateTime _DenNgay;
        private DateTime _TuNgay;    

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
                    if (value != null)
                        BoPhanCanBoHuongDan = value;
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
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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
        [ModelDefault("Caption", "Đơn vị cán bộ hướng dẫn")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhanCanBoHuongDan
        {
            get
            {
                return _BoPhanCanBoHuongDan;
            }
            set
            {
                SetPropertyValue("BoPhanCanBoHuongDan", ref _BoPhanCanBoHuongDan, value);
                if (!IsLoading)
                {
                    CanBoHuongDan = null;
                    UpdateNVList1();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ hướng dẫn tập sự")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NVList1")]       
        public ThongTinNhanVien CanBoHuongDan
        {
            get
            {
                return _CanBoHuongDan;
            }
            set
            {
                SetPropertyValue("CanBoHuongDan", ref _CanBoHuongDan, value); 
                if (!IsLoading && value != null)
                {
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
       // [RuleUniqueValue("", DefaultContexts.Save, TargetCriteria = "MaTruong != 'BUH'")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading && value != DateTime.MinValue)
                    DenNgay = TuNgay.AddMonths(12).AddDays(-1);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        //[RuleUniqueValue("", DefaultContexts.Save, TargetCriteria = "MaTruong != 'BUH'")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
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
        [Association("QuyetDinhHuongDanTapSu-ListChiTietQuyetDinhHuongDanTapSu")]
        public QuyetDinhHuongDanTapSu QuyetDinhHuongDanTapSu
        {
            get
            {
                return _QuyetDinhHuongDanTapSu;
            }
            set
            {
                SetPropertyValue("QuyetDinhHuongDanTapSu", ref _QuyetDinhHuongDanTapSu, value);
                //if (!IsLoading && value != null)
                //{
                //    GiayToHoSo.SoGiayTo = value.SoQuyetDinh;
                //    GiayToHoSo.NgayBanHanh = value.NgayHieuLuc;
                //    GiayToHoSo.LuuTru = value.LuuTru;
                //    GiayToHoSo.TrichYeu = value.NoiDung;
                //}
            }
        }

        public ChiTietQuyetDinhHuongDanTapSu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TuNgay = HamDungChung.GetServerTime();
            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định hướng dẫn tập sự"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNhanVienList();
            UpdateNVList1();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (QuyetDinhHuongDanTapSu != null
                && !IsLoading
                && !QuyetDinhHuongDanTapSu.IsDirty)
                QuyetDinhHuongDanTapSu.IsDirty = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (BoPhan != null)
            {
                BoPhanText = BoPhan.TenBoPhan;
            } 

            UpdateNhanVienList();
            UpdateNVList1();
            UpdateGiayToList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList1 { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        private void UpdateNVList1()
        {
            if (NVList1 == null)
                NVList1 = new XPCollection<ThongTinNhanVien>(Session);
            NVList1.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhanCanBoHuongDan);
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

            SystemContainer.Resolver<IQuyetDinhHuongDanTapSuService>("QuyetDinhHuongDanTapSu" + TruongConfig.MaTruong).Save(Session, this);
        }

        protected override void OnDeleting()
        {
            SystemContainer.Resolver<IQuyetDinhHuongDanTapSuService>("QuyetDinhHuongDanTapSu" + TruongConfig.MaTruong).Delete(Session, this);

            base.OnDeleting();
        }
    }

}
