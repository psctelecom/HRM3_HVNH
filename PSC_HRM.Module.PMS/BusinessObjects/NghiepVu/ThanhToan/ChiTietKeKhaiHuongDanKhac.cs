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


namespace PSC_HRM.Module.PMS.NghiepVu.ThanhToan
{

    [ModelDefault("Caption", "Chi tiết hướng dẫn khác(NEU)")]
    [DefaultProperty("Caption")]
    public class ChiTietKeKhaiHuongDanKhac : BaseObject
    {
        #region key
        private QuanLyHoatDongKhac _QuanLyHoatDongKhac;
        [Association("QuanLyHoatDongKhac-ListChiTietKeKhaiHuongDanKhac")]
        [ModelDefault("Caption", "key")]
        [Browsable(false)]
        public QuanLyHoatDongKhac QuanLyHoatDongKhac
        {
            get
            {
                return _QuanLyHoatDongKhac;
            }
            set
            {
                SetPropertyValue("QuanLyHoatDongKhac", ref _QuanLyHoatDongKhac, value);
            }
        }

        #endregion

       #region Khai báo nhân viên
       private BoPhan _BoPhan;
       private NhanVien _NhanVien;
        #endregion

        #region Khai báo
        private LoaiHoatDong _LoaiHoatDong;
        private decimal _SoLuong;
        private decimal _SoGioQuyDoi;
        private string _GhiChu;
        private HeDaoTao _HeDaoTao;
        private ChamHuongDanEnum? _Cham;
        private bool _XacNhan;
        #endregion

        #region Giá trị nhân viên
       [ModelDefault("Caption", "Đơn vị")]
       [ModelDefault("AllowEdit", "False")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption", "Nhân viên")]
       [RuleRequiredField(DefaultContexts.Save)]
       [ModelDefault("AllowEdit", "False")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        #endregion
        #region Giá trị

        [ModelDefault("Caption", "Hoạt động")]
        [ModelDefault("AllowEdit", "False")]
        public LoaiHoatDong LoaiHoatDong
        {
            get { return _LoaiHoatDong; }
            set { SetPropertyValue("LoaiHoatDong", ref _LoaiHoatDong, value); }
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
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        //[ModelDefault("AllowEdit", "False")]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }
        [ModelDefault("Caption", "Trạng thái")]
        public ChamHuongDanEnum? Cham
        {
            get { return _Cham; }
            set { SetPropertyValue("Cham", ref _Cham, value); }
        }

        [ModelDefault("Caption", "Xác nhận")]
        public bool XacNhan
        {
            get { return _XacNhan; }
            set { SetPropertyValue("XacNhan", ref _XacNhan, value); }
        }
        #endregion

        public ChiTietKeKhaiHuongDanKhac(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}