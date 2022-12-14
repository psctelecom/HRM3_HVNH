using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DoanDang
{
    [DefaultClassOptions]
    [ImageName("BO_Doan")]
    [ModelDefault("Caption", "Đoàn viên")]
    [DefaultProperty("SoTheDoan")]
    public class DoanVien : BaseObject, ICategorizedItem, IBoPhan
    {
        private bool _TruongThanhDoan;
        private ToChucDoan _ToChucDoan;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _SoTheDoan;
        private DateTime _NgayCap;
        private DateTime _NgayKetNap;
        private TinhThanh _NoiKetNap;
        private ChucVuDoan _ChucVuDoan;
        private DateTime _NgayBoNhiem;
        private DateTime _NgayHetNhiemKy;

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

        [ModelDefault("Caption", "Số thẻ đoàn")]
        public string SoTheDoan
        {
            get
            {
                return _SoTheDoan;
            }
            set
            {
                SetPropertyValue("SoTheDoan", ref _SoTheDoan, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp")]
        public DateTime NgayCap
        {
            get
            {
                return _NgayCap;
            }
            set
            {
                SetPropertyValue("NgayCap", ref _NgayCap, value);
            }
        }

        [ModelDefault("Caption", "Ngày kết nạp")]
        public DateTime NgayKetNap
        {
            get
            {
                return _NgayKetNap;
            }
            set
            {
                SetPropertyValue("NgayKetNap", ref _NgayKetNap, value);
            }
        }

        [ModelDefault("Caption", "Nơi kết nạp")]
        public TinhThanh NoiKetNap
        {
            get
            {
                return _NoiKetNap;
            }
            set
            {
                SetPropertyValue("NoiKetNap", ref _NoiKetNap, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ Đoàn")]
        public ChucVuDoan ChucVuDoan
        {
            get
            {
                return _ChucVuDoan;
            }
            set
            {
                SetPropertyValue("ChucVuDoan", ref _ChucVuDoan, value);
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm")]
        public DateTime NgayBoNhiem
        {
            get
            {
                return _NgayBoNhiem;
            }
            set
            {
                SetPropertyValue("NgayBoNhiem", ref _NgayBoNhiem, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết nhiệm kỳ")]
        public DateTime NgayHetNhiemKy
        {
            get
            {
                return _NgayHetNhiemKy;
            }
            set
            {
                SetPropertyValue("NgayHetNhiemKy", ref _NgayHetNhiemKy, value);
            }
        }

        [ModelDefault("Caption", "Tổ chức Đoàn")]
        [Association("ToChucDoan-DoanVienList")]
        public ToChucDoan ToChucDoan
        {
            get
            {
                return _ToChucDoan;
            }
            set
            {
                SetPropertyValue("ToChucDoan", ref _ToChucDoan, value);
            }
        }

        [ModelDefault("Caption", "Trưởng thành Đoàn")]
        public bool TruongThanhDoan
        {
            get
            {
                return _TruongThanhDoan;
            }
            set
            {
                SetPropertyValue("TruongThanhDoan", ref _TruongThanhDoan, value);
            }
        }

        [Aggregated]
        [Association("DoanVien-ListChucVuDoanKiemNhiem")]
        [ModelDefault("Caption", "Chức vụ kiêm nhiệm")]
        public XPCollection<ChucVuDoanKiemNhiem> ListChucVuDoanKiemNhiem
        {
            get
            {
                return GetCollection<ChucVuDoanKiemNhiem>("ListChucVuDoanKiemNhiem");
            }
        }

        [NonPersistent]
        [Browsable(false)]
        public bool IsSave { get; set; }

        public DoanVien(Session session) : base(session) { }

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

        ITreeNode ICategorizedItem.Category
        {
            get
            {
                return ToChucDoan;
            }
            set
            {
                ToChucDoan = (ToChucDoan)value;
            }
        }


        [Browsable(false)]
        [NonPersistent]
        public ToChucDoan Category
        {
            get
            {
                return ToChucDoan;
            }
            set
            {
            	ToChucDoan = value;
            }
        }
    }

}
