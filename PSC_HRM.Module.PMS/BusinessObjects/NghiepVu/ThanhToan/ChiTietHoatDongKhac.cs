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


namespace PSC_HRM.Module.PMS.NghiepVu.ThanhToan
{

    [Appearance("", TargetItems = "VaiTro;BacDaoTao;SoThanhVien;SoTienThanhToan;SoTienThanhToanThueTNCN;LoaiHoatDong;DuKien", Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyHoatDongKhac.ThongTinTruong.TenVietTat = 'HUFLIT'")]
    [ModelDefault("Caption", "Chi tiết HD khác")]
    [DefaultProperty("Caption")]
    public class ChiTietHoatDongKhac : BaseObject
    {
        #region key
        private QuanLyHoatDongKhac _QuanLyHoatDongKhac;
        [Association("QuanLyHoatDongKhac-ListChiTiet")]
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
       private VaiTroNCKHEnum _VaiTro;
       #endregion

        #region Khai báo
        private BacDaoTao _BacDaoTao;
        private HoatDongKhac _HoatDongKhac;
        private string _DienGiai;
        private int _SoThanhVien;
        private bool _DuKien;
        private bool _XacNhan;
        private DateTime _NgayNhap;
        private decimal _SoTienThanhToan;
        private decimal _SoTienThanhToanThueTNCN;
        private decimal _SoGio;
        private LoaiHoatDongEnum _LoaiHoatDong;
        #endregion

        #region Giá trị nhân viên
        [ModelDefault("Caption", "Bộ phận")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption", "Nhân viên")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Vai trò")]
        public VaiTroNCKHEnum VaiTro
        {
            get { return _VaiTro; }
            set { SetPropertyValue("VaiTro", ref _VaiTro, value); }
        }
        #endregion
        #region Giá trị
        [ModelDefault("Caption", "Hoạt động")]
        public HoatDongKhac HoatDongKhac
        {
            get { return _HoatDongKhac; }
            set { SetPropertyValue("HoatDongKhac", ref _HoatDongKhac, value); }
        }
        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        [ModelDefault("Caption", "Diễn giải")]
        [Size(-1)]
        public string DienGiai
        {
            get { return _DienGiai; }
            set { SetPropertyValue("DienGiai", ref _DienGiai, value); }
        }

        [ModelDefault("Caption","Số thành viên")]
        public int SoThanhVien
        {
            get { return _SoThanhVien; }
            set { SetPropertyValue("SoThanhVien", ref _SoThanhVien, value); }
        }
        [ModelDefault("Caption", "Dự kiến")]
        public bool DuKien
        {
            get { return _DuKien; }
            set { SetPropertyValue("DuKien", ref _DuKien, value); }
        }
        [ModelDefault("Caption", "Xác nhận")]
        public bool XacNhan
        {
            get { return _XacNhan; }
            set { SetPropertyValue("XacNhan", ref _XacNhan, value); }
        }

        [ModelDefault("Caption","Ngày nhập")]
        public DateTime NgayNhap
        {
            get { return _NgayNhap; }
            set { SetPropertyValue("NgayNhap", ref _NgayNhap, value); }
        }
        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal SoTienThanhToan
        {
            get { return _SoTienThanhToan; }
            set { SetPropertyValue("SoTienThanhToan", ref _SoTienThanhToan, value); }
        }
        [ModelDefault("Caption", "Số tiền (Thuế TNCN)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal SoTienThanhToanThueTNCN
        {
            get { return _SoTienThanhToanThueTNCN; }
            set { SetPropertyValue("SoTienThanhToanThueTNCN", ref _SoTienThanhToanThueTNCN, value); }
        }
        [ModelDefault("Caption", "Số giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal SoGio
        {
            get { return _SoGio; }
            set { SetPropertyValue("SoGio", ref _SoGio, value); }
        }
        [ModelDefault("Caption", "Loại hoạt động")]
        public LoaiHoatDongEnum LoaiHoatDong
        {
            get { return _LoaiHoatDong; }
            set { SetPropertyValue("LoaiHoatDong", ref _LoaiHoatDong, value); }
        }
        #endregion

        public ChiTietHoatDongKhac(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}