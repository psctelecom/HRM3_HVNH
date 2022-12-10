using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.HopDong
{
    [ImageName("BO_HopDong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết gia hạn hợp đồng")]
    public class ChiTietGiaHanHopDong : BaseObject
    {
        private BoPhan _BoPhan;
        private QuanLyGiaHanHopDong _QuanLyGiaHanHopDong;
        private ThongTinNhanVien _ThongTinNhanVien;
        private HopDong_NhanVien _HopDongNhanVien;
        private int _SoThang;
        private DateTime _NgayLap;

        [ModelDefault("Caption", "Quản lý gia hạn hợp đồng")]
        [Association("QuanLyGiaHanHopDong-ListChiTietGiaHanHopDong")]
        public QuanLyGiaHanHopDong QuanLyGiaHanHopDong
        {
            get
            {
                return _QuanLyGiaHanHopDong;
            }
            set
            {
                SetPropertyValue("QuanLyGiaHanHopDong", ref _QuanLyGiaHanHopDong, value);
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
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
                }
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Hợp đồng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("ThongTinNhanVien.ListHopDong")]
        public HopDong_NhanVien HopDongNhanVien
        {
            get
            {
                return _HopDongNhanVien;
            }
            set
            {
                SetPropertyValue("HopDongNhanVien", ref _HopDongNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Số tháng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoThang
        {
            get
            {
                return _SoThang;
            }
            set
            {
                SetPropertyValue("SoThang", ref _SoThang, value);
            }
        }

        public ChiTietGiaHanHopDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayLap = HamDungChung.GetServerTime();
            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<HoSo.ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                HopDongNhanVien.DenNgay = HopDongNhanVien.DenNgay.AddMonths(SoThang);
            }
        }

        protected override void OnDeleting()
        {
            HopDongNhanVien.DenNgay = HopDongNhanVien.DenNgay.AddMonths(-SoThang);
            base.OnDeleting();
        }
    }
}
