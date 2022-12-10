using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DanhGia
{
    [ImageName("BO_BangTheoDoiViPham")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết vi phạm")]
    public class ChiTietViPham : BaseObject
    {
        // Fields...
        private string _GhiChu;
        private HinhThucViPham _HinhThucViPham;
        private DateTime _Ngay;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private BangTheoDoiViPham _BangTheoDoiViPham;
        private string _Tiet;
        private string _Lop;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng theo dõi vi phạm")]
        [Association("BangTheoDoiViPham-ListChiTietViPham")]
        public BangTheoDoiViPham BangTheoDoiViPham
        {
            get
            {
                return _BangTheoDoiViPham;
            }
            set
            {
                SetPropertyValue("BangTheoDoiViPham", ref _BangTheoDoiViPham, value);
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

        [ModelDefault("Caption", "Ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime Ngay
        {
            get
            {
                return _Ngay;
            }
            set
            {
                SetPropertyValue("Ngay", ref _Ngay, value);
            }
        }

        [ModelDefault("Caption", "Hình thức vi phạm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public HinhThucViPham HinhThucViPham
        {
            get
            {
                return _HinhThucViPham;
            }
            set
            {
                SetPropertyValue("HinhThucViPham", ref _HinhThucViPham, value);
            }
        }

        [ModelDefault("Caption", "Tiết")]
        public string Tiet
        {
            get
            {
                return _Tiet;
            }
            set
            {
                SetPropertyValue("Tiet", ref _Tiet, value);
            }
        }

        [ModelDefault("Caption", "Lớp")]
        public string Lop
        {
            get
            {
                return _Lop;
            }
            set
            {
                SetPropertyValue("Lop", ref _Lop, value);
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

        public ChiTietViPham(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Ngay = HamDungChung.GetServerTime();
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
