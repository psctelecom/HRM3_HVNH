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

    [ModelDefault("Caption", "Chi tiết HD khác(NEU)")]
    [DefaultProperty("Caption")]
    public class ChiTietKeKhaiHoatDongKhac : BaseObject
    {
        #region key
        private QuanLyHoatDongKhac _QuanLyHoatDongKhac;
        [Association("QuanLyHoatDongKhac-ListChiTietKeKhaiHoatDongKhac")]
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
       private BoPhan _BoMon;
       #endregion

        #region Khai báo
       private DanhSachChiTietHDKhac _HoatDong;
       private decimal _SoGioThucHien;
       private string _GhiChu;
       private TrangThaiXacNhanEnum _TrangThai;
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
        [ModelDefault("Caption", "Khoa/Bộ môn")]
        [ModelDefault("AllowEdit", "False")]
        public BoPhan BoMon
        {
            get { return _BoMon; }
            set { SetPropertyValue("BoMon", ref _BoMon, value); }
        }
        #endregion
        #region Giá trị

        [ModelDefault("Caption", "Hoạt động")]
        [ModelDefault("AllowEdit", "False")]
        public DanhSachChiTietHDKhac HoatDong
        {
            get { return _HoatDong; }
            set { SetPropertyValue("HoatDong", ref _HoatDong, value); }
        }
        [ModelDefault("Caption", "Số giờ thực hiện")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioThucHien
        {
            get { return _SoGioThucHien; }
            set { SetPropertyValue("SoGioThucHien", ref _SoGioThucHien, value); }
        }
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }
        [ModelDefault("Caption", "Trạng thái")]
        public TrangThaiXacNhanEnum TrangThai
        {
            get { return _TrangThai; }
            set { SetPropertyValue("TrangThai", ref _TrangThai, value); }
        }
        #endregion

        public ChiTietKeKhaiHoatDongKhac(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}