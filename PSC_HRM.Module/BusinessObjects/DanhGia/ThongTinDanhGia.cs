using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DanhGia
{
    [ModelDefault("Caption", "Thông tin đánh giá")]
    [DefaultProperty("ThongTinNhanVien")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "BangChotThongTinDanhGia;ThongTinNhanVien;")]
    public class ThongTinDanhGia : BaseObject, IBoPhan
    {
        // Fields...
        //-------------------------Bảng chốt-------------------------
        private BangChotThongTinDanhGia _BangChotThongTinDanhGia;
        
        //----------------------ThongTinNhanVien---------------------
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;

        private LoaiNhanSu _LoaiNhanSu;
        private LoaiNhanVien _LoaiNhanVien;
        private ChucVu _ChucVu;
        private ChucDanh _ChucDanh;
        private TinhTrang _TinhTrang;
        private bool _DanhGia;
        private DoiTuongDanhGia _DoiTuongDanhGia;
        private string _MaDoiTuongDanhGia;

        private string _GhiChu;


        [Browsable(false)]
        [ModelDefault("Caption", "Bảng chốt thông tin đánh giá")]
        [Association("BangChotThongTinDanhGia-ListThongTinDanhGia")]
        public BangChotThongTinDanhGia BangChotThongTinDanhGia
        {
            get
            {
                return _BangChotThongTinDanhGia;
            }
            set
            {
                SetPropertyValue("BangChotThongTinDanhGia", ref _BangChotThongTinDanhGia, value);
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

        [ModelDefault("Caption", "Loại nhân sự")]
        public LoaiNhanSu LoaiNhanSu
        {
            get
            {
                return _LoaiNhanSu;
            }
            set
            {
                SetPropertyValue("LoaiNhanSu", ref _LoaiNhanSu, value);
            }
        }

        [ModelDefault("Caption", "Loại hợp đồng")]
        public LoaiNhanVien LoaiNhanVien
        {
            get
            {
                return _LoaiNhanVien;
            }
            set
            {
                SetPropertyValue("LoaiNhanVien", ref _LoaiNhanVien, value);
            }
        }
        
        [ModelDefault("Caption", "Chức vụ")]
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

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanh ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrang TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        [ModelDefault("Caption", "Đánh giá")]
        public bool DanhGia
        {
            get
            {
                return _DanhGia;
            }
            set
            {
                SetPropertyValue("DanhGia", ref _DanhGia, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đối tượng đánh giá")]
        public DoiTuongDanhGia DoiTuongDanhGia
        {
            get
            {
                return _DoiTuongDanhGia;
            }
            set
            {
                SetPropertyValue("DoiTuongDanhGia", ref _DoiTuongDanhGia, value);
                if (!IsLoading && value != null)
                {
                    MaDoiTuongDanhGia = value.MaQuanLy;
                }
            }
        }

        [ModelDefault("Caption", "Mã đối tượng đánh giá")]
        public string MaDoiTuongDanhGia
        {
            get
            {
                return _MaDoiTuongDanhGia;
            }
            set
            {
                SetPropertyValue("MaDoiTuongDanhGia", ref _MaDoiTuongDanhGia, value);
            }
        }

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

        public ThongTinDanhGia(Session session) : base(session) { }

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
