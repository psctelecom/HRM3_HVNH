using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.TuyenDung
{
    [DefaultProperty("Caption")]
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Đăng ký tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique("DangKyTuyenDung", DefaultContexts.Save, "ViTriTuyenDung;BoPhan;QuanLyTuyenDung")]
    [Appearance("Hide_BUH", TargetItems = "BoMon", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'BUH'")]
    public class DangKyTuyenDung : TruongBaseObject, IBoPhan
    {
        // Fields...
        private BoPhan _BoMon;
        private ViTriTuyenDung _ViTriTuyenDung;
        private int _SoLuongTuyen;
        private BoPhan _BoPhan;
        private QuanLyTuyenDung _QuanLyTuyenDung;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        [Association("QuanLyTuyenDung-ListDangKyTuyenDung")]
        public QuanLyTuyenDung QuanLyTuyenDung
        {
            get
            {
                return _QuanLyTuyenDung;
            }
            set
            {
                SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "Vị trí tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QuanLyTuyenDung.ListViTriTuyenDung")]
        public ViTriTuyenDung ViTriTuyenDung
        {
            get
            {
                return _ViTriTuyenDung;
            }
            set
            {
                SetPropertyValue("ViTriTuyenDung", ref _ViTriTuyenDung, value);
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
                    BoMon = null;
                    UpdateBoMonList();
                }
            }
        }

        [ModelDefault("Caption", "Bộ môn")]
        [DataSourceProperty("BoMonList")]
        public BoPhan BoMon
        {
            get
            {
                return _BoMon;
            }
            set
            {
                SetPropertyValue("BoMon", ref _BoMon, value);
            }
        }

        [ModelDefault("Caption", "Số lượng tuyển")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoLuongTuyen
        {
            get
            {
                return _SoLuongTuyen;
            }
            set
            {
                SetPropertyValue("SoLuongTuyen", ref _SoLuongTuyen, value);
            }
        }

        [Browsable(false)]
        public string Caption
        {
            get
            {
                return ObjectFormatter.Format("{ViTriTuyenDung.TenViTriTuyenDung} {BoMon.TenBoPhan} {BoPhan.TenBoPhan}", this);
            }
        }

        public DangKyTuyenDung(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<BoPhan> BoMonList { get; set; }

        private void UpdateBoMonList()
        {
            if (BoMonList == null)
                BoMonList = new XPCollection<BoPhan>(Session);

            BoMonList.Criteria = CriteriaOperator.Parse("BoPhanCha=? and LoaiBoPhan=3",
                BoPhan);
        }
    }

}
