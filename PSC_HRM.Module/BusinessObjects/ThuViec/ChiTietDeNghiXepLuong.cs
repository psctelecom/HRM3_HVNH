using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuViec
{
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết đề nghị xếp lương")]
    public class ChiTietDeNghiXepLuong : BaseObject
    {
        private QuanLyThuViec _QuanLyThuViec;
        private ThongTinNhanVien _ThongTinNhanVien;
        private DateTime _NgayDeNghiHopDongChinhThuc;
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuong;
        private BoPhan _BoPhan;
        private decimal _HeSoLuong;
        private DateTime _NgayHuongLuong;
        private string _GhiChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý thử việc")]
        [Association("QuanLyThuViec-ListChiTietDeNghiXepLuong")]
        public QuanLyThuViec QuanLyThuViec
        {
            get
            {
                return _QuanLyThuViec;
            }
            set
            {
                SetPropertyValue("QuanLyThuViec", ref _QuanLyThuViec, value);
            }
        }

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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Ngày kí hợp đồng chính thức")]
        public DateTime NgayDeNghiHopDongChinhThuc
        {
            get
            {
                return _NgayDeNghiHopDongChinhThuc;
            }
            set
            {
                SetPropertyValue("NgayDeNghiHopDongChinhThuc", ref _NgayDeNghiHopDongChinhThuc, value);
            }
        }

        [ImmediatePostData]
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
                if (!IsLoading)
                {
                    BacLuong = null;
                }

            }
        }

        [ModelDefault("Caption", "Bậc lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
        public BacLuong BacLuong
        {
            get
            {
                return _BacLuong;
            }
            set
            {
                SetPropertyValue("BacLuong", ref _BacLuong, value);
            }
        }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal HeSoLuong
        {
            get
            {
                return _HeSoLuong;
            }
            set
            {
                SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongLuong
        {
            get
            {
                return _NgayHuongLuong;
            }
            set
            {
                SetPropertyValue("NgayHuongLuong", ref _NgayHuongLuong, value);
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

        public ChiTietDeNghiXepLuong(Session session) : base(session) { }

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
