using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.KhenThuong
{
    [DefaultProperty("CanBo")]
    [ImageName("BO_QuanLyKhenThuong")]
    [ModelDefault("Caption", "Chi tiết cá nhân đề nghị khen thưởng")]
    public class ChiTietCaNhanDeNghiKhenThuong : BaseObject, IBoPhan
    {
        private BoPhan _BoPhan;
        private string _GhiChu;
        private ThongTinNhanVien _ThongTinNhanVien;
        private ChiTietDeNghiKhenThuong _ChiTietDeNghiKhenThuong;
        private string _SoPhieuBoMon;
        private string _SoPhieuDonVi;
        private string _SoPhieuHoiDong;
        private string _ThanhTichNCKH;
        private string _ThanhTichKhac;
        private HinhThucViPham _HinhThucViPham;

        [Browsable(false)]
        [ModelDefault("Caption", "Chi tiết đề nghị khen thưởng")]
        [Association("ChiTietDeNghiKhenThuong-ListChiTietCaNhanDeNghiKhenThuong")]
        public ChiTietDeNghiKhenThuong ChiTietDeNghiKhenThuong
        {
            get
            {
                return _ChiTietDeNghiKhenThuong;
            }
            set
            {
                SetPropertyValue("ChiTietDeNghiKhenThuong", ref _ChiTietDeNghiKhenThuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bộ phận")]
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

        [ModelDefault("Caption", "Số phiếu bộ môn")]
        public string SoPhieuBoMon
        {
            get
            {
                return _SoPhieuBoMon;
            }
            set
            {
                SetPropertyValue("SoPhieuBoMon", ref _SoPhieuBoMon, value);
            }
        }

        [ModelDefault("Caption", "Số phiếu đơn vị")]
        public string SoPhieuDonVi
        {
            get
            {
                return _SoPhieuDonVi;
            }
            set
            {
                SetPropertyValue("SoPhieuDonVi", ref _SoPhieuDonVi, value);
            }
        }

        [ModelDefault("Caption", "Số phiếu Hội đồng cơ sở")]
        public string SoPhieuHoiDong
        {
            get
            {
                return _SoPhieuHoiDong;
            }
            set
            {
                SetPropertyValue("SoPhieuHoiDong", ref _SoPhieuHoiDong, value);
            }
        }

        [ModelDefault("Caption", "Thành tích NCKH")]
        public string ThanhTichNCKH
        {
            get
            {
                return _ThanhTichNCKH;
            }
            set
            {
                SetPropertyValue("ThanhTichNCKH", ref _ThanhTichNCKH, value);
            }
        }

        [ModelDefault("Caption", "Thành tích khác")]
        public string ThanhTichKhac
        {
            get
            {
                return _ThanhTichKhac;
            }
            set
            {
                SetPropertyValue("ThanhTichKhac", ref _ThanhTichKhac, value);
            }
        }

        [ModelDefault("Caption", "Loại vi phạm")]
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

        public ChiTietCaNhanDeNghiKhenThuong(Session session) : base(session) { }

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
