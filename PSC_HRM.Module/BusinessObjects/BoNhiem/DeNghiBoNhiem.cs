using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BoNhiem
{
    [ImageName("BO_QuanLyDaoTao")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Đề nghị bổ nhiệm")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyBoNhiem;ThongTinNhanVien;ChucVu")]
    [Appearance("QuyetDinh.Khac", TargetItems = "TaiBoPhan", Visibility = ViewItemVisibility.Hide, Criteria = "!KiemNhiem")]
    public class DeNghiBoNhiem : BaseObject, IBoPhan
    {
        private string _GhiChu;
        private BoPhan _TaiBoPhan;
        private bool _KiemNhiem;
        private bool _BoNhiemMoi;
        private ChucVu _ChucVu;
        private QuanLyBoNhiem _QuanLyBoNhiem;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Quản lý bổ nhiệm")]
        [Association("QuanLyBoNhiem-ListDeNghiBoNhiem")]
        public QuanLyBoNhiem QuanLyBoNhiem
        {
            get
            {
                return _QuanLyBoNhiem;
            }
            set
            {
                SetPropertyValue("QuanLyBoNhiem", ref _QuanLyBoNhiem, value);
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
                }
            }
        }

        [ImmediatePostData]
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Bổ nhiệm mới")]
        public bool BoNhiemMoi
        {
            get
            {
                return _BoNhiemMoi;
            }
            set
            {
                SetPropertyValue("BoNhiemMoi", ref _BoNhiemMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Kiêm nhiệm")]
        public bool KiemNhiem
        {
            get
            {
                return _KiemNhiem;
            }
            set
            {
                SetPropertyValue("KiemNhiem", ref _KiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
            }
        }

        [ModelDefault("Caption", "Tại đơn vị")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "KiemNhiem")]
        public BoPhan TaiBoPhan
        {
            get
            {
                return _TaiBoPhan;
            }
            set
            {
                SetPropertyValue("TaiBoPhan", ref _TaiBoPhan, value);
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

        public DeNghiBoNhiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            UpdateNVList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

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
    }

}
