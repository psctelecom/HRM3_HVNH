using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.NonPersistent;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.NghiepVu.NCKH
{

    [ModelDefault("Caption", "Chi tiết hoạt động khác khác(VHU)_Non")]
    [NonPersistent]
    [DefaultProperty("Caption")]
    public class ChiTietKeKhaiHDK_VHU_Non : BaseObject
    {
        private bool _Chon;
        private Guid _OidKey;
        #region Khai báo nhân viên
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        #endregion

        #region Khai báo
        private DanhMucHoatDongKhac _DanhMucHoatDongKhac;
        private HocKy _HocKy;
        private decimal _SoLuong;
        private decimal _SoGioQuyDoi;
        private bool _ThanhToanTienMat = false;
        private decimal _SoTienThanhToan;
        private string _GhiChu;
        private bool _XacNhan;
        #endregion

        [ModelDefault("Caption", "Chọn")]
        [ImmediatePostData]
        public bool Chon
        {
            get { return _Chon; }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "OidKey")]
        //[ModelDefault("AllowEdit", "False")]
        public Guid OidKey
        {
            get { return _OidKey; }
            set { SetPropertyValue("OidKey", ref _OidKey, value); }
        }
        #region Giá trị nhân viên
        [ModelDefault("Caption", "Đơn vị")]
       //[ModelDefault("AllowEdit", "False")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption", "Nhân viên")]
       [RuleRequiredField(DefaultContexts.Save)]
       //[ModelDefault("AllowEdit", "False")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        #endregion
        #region Giá trị

        [ModelDefault("Caption", "Loại HĐK")]
        //[ModelDefault("AllowEdit", "False")]
        public DanhMucHoatDongKhac DanhMucHoatDongKhac
        {
            get { return _DanhMucHoatDongKhac; }
            set { SetPropertyValue("DanhMucHoatDongKhac", ref _DanhMucHoatDongKhac, value); }
        }
        [ModelDefault("Caption", "Học kỳ")]
        //[ModelDefault("AllowEdit", "False")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }
        [ModelDefault("Caption", "Số lượng")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        //[ModelDefault("AllowEdit", "False")]
        public decimal SoLuong
        {
            get { return _SoLuong; }
            set { SetPropertyValue("SoLuong", ref _SoLuong, value); }
        }
        [ModelDefault("Caption", "Số giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[ModelDefault("AllowEdit", "False")]
        public decimal SoGioQuyDoi
        {
            get { return _SoGioQuyDoi; }
            set { SetPropertyValue("SoGioQuyDoi", ref _SoGioQuyDoi, value); }
        }

        [ModelDefault("Caption", "Thanh toán bằng tiền")]
        [ImmediatePostData]
        public bool ThanhToanTienMat
        {
            get { return _ThanhToanTienMat; }
            set { SetPropertyValue("ThanhToanTienMat", ref _ThanhToanTienMat, value); }
        }

        [ModelDefault("Caption", "Số tiền thanh toán")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        //[ModelDefault("AllowEdit", "False")]
        public decimal SoTienThanhToan
        {
            get { return _SoTienThanhToan; }
            set { SetPropertyValue("SoTienThanhToan", ref _SoTienThanhToan, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Xác nhận")]
        public bool XacNhan
        {
            get { return _XacNhan; }
            set { SetPropertyValue("XacNhan", ref _XacNhan, value); }
        }
        #endregion

        public ChiTietKeKhaiHDK_VHU_Non(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}