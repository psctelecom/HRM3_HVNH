using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DoanDang
{
    [DefaultClassOptions]
    [ImageName("BO_DoanThe")]
    [ModelDefault("Caption", "Quản lý Đoàn thể")]
    [DefaultProperty("ThongTinNhanVien")]
    public class DoanThe : BaseObject, ICategorizedItem, IBoPhan
    {
        private DateTime _NgayVaoCongDoan;
        private string _SoQuyetDinh;
        private string _SoThe;
        private ToChucDoanThe _ToChucDoanThe;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private ChucVuDoanThe _ChucVuDoanThe;
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

        [ModelDefault("Caption", "Ngày vào Công đoàn")]
        public DateTime NgayVaoCongDoan
        {
            get
            {
                return _NgayVaoCongDoan;
            }
            set
            {
                SetPropertyValue("NgayVaoCongDoan", ref _NgayVaoCongDoan, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ Đoàn thể")]
        public ChucVuDoanThe ChucVuDoanThe
        {
            get
            {
                return _ChucVuDoanThe;
            }
            set
            {
                SetPropertyValue("ChucVuDoanThe", ref _ChucVuDoanThe, value);
                if (!IsLoading && ThongTinNhanVien != null)
                {
                    if (value != null && value.HSPCChucVuDoanThe > 0)
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuCongDoan = value.HSPCChucVuDoanThe;
                }
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

        [ModelDefault("Caption", "Tổ chức Đoàn thể")]
        [Association("ToChucDoanThe-DoanTheList")]
        public ToChucDoanThe ToChucDoanThe
        {
            get
            {
                return _ToChucDoanThe;
            }
            set
            {
                SetPropertyValue("ToChucDoanThe", ref _ToChucDoanThe, value);
            }
        }

        [ModelDefault("Caption", "Số thẻ")]
        public string SoThe
        {
            get
            {
                return _SoThe;
            }
            set
            {
                SetPropertyValue("SoThe", ref _SoThe, value);
            }
        }

        [ModelDefault("Caption", "Số quyết định")]
        public string SoQuyetDinh
        {
            get
            {
                return _SoQuyetDinh;
            }
            set
            {
                SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value);
            }
        }

        [Aggregated]
        [Association("DoanThe-ListChucVuDoanTheKiemNhiem")]
        [ModelDefault("Caption", "Chức vụ kiêm nhiệm")]
        public XPCollection<ChucVuDoanTheKiemNhiem> ListChucVuDoanTheKiemNhiem
        {
            get
            {
                return GetCollection<ChucVuDoanTheKiemNhiem>("ListChucVuDoanTheKiemNhiem");
            }
        }

        [NonPersistent]
        [Browsable(false)]
        public bool IsSave { get; set; }

        public DoanThe(Session session) : base(session) { }

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
                return ToChucDoanThe;
            }
            set
            {
                ToChucDoanThe = (ToChucDoanThe)value;
            }
        }


        [Browsable(false)]
        [NonPersistent]
        public ToChucDoanThe Category
        {
            get
            {
                return ToChucDoanThe;
            }
            set
            {
            	ToChucDoanThe = value;
            }
        }
    }

}
