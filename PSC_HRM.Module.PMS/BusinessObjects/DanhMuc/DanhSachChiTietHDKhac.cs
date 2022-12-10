using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.Enum;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ImageName("BO_ChuyenNgach")]
    [DefaultProperty("TenHoatDong")]
    [ModelDefault("Caption", "Danh sách hoạt động khác")]
    public class DanhSachChiTietHDKhac : BaseObject
    {     

        private string _MaQuanLy;
        private string _TenHoatDong;
        private NhomHoatDong _NhomHoatDong;
        private decimal _SoTiet;
        private string _GhiChu;
        private BoPhan _DonVi;
        private LoaiHoatDongEnum? _LoaiHoatDong;

        [ModelDefault("Caption", "Nhóm hoạt động")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [Association("NhomHoatDong-ListDanhSachChiTietHDKhac")]
        [Browsable(false)]
        public NhomHoatDong NhomHoatDong
        {
            get
            {
                return _NhomHoatDong;
            }
            set
            {
                SetPropertyValue("NhomHoatDong", ref _NhomHoatDong, value);
            }
        }

        [ModelDefault("Caption", "Mã quản lý")]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên hoạt động chi tiết")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(-1)]
        public string TenHoatDong
        {
            get { return _TenHoatDong; }
            set { SetPropertyValue("TenHoatDong", ref _TenHoatDong, value); }
        }

        [ModelDefault("Caption", "Số tiết")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTiet
        {
            get { return _SoTiet; }
            set { SetPropertyValue("SoTiet", ref _SoTiet, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan DonVi
        {
            get
            {
                return _DonVi;
            }
            set
            {
                SetPropertyValue("DonVi", ref _DonVi, value);
            }
        }

        [ModelDefault("Caption", "Loại hoạt động")]
        public LoaiHoatDongEnum? LoaiHoatDong
        {
            get
            {
                return _LoaiHoatDong;
            }
            set
            {
                SetPropertyValue("LoaiHoatDong", ref _LoaiHoatDong, value);
            }
        }
        public DanhSachChiTietHDKhac(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}