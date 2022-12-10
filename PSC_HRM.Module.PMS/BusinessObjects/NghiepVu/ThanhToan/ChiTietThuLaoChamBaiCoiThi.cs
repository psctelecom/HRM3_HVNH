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

    [Appearance("", TargetItems = "BacDaoTao;CMND;TongTienThueTNCN;TongTien;KhoanChi;LopHocPhan;SoBaiQuaTrinh;SoBaiGiuaKy;SoBaiCuoiKy;DonGiaQuaTrinh;DonGiaGiuaKy;DonGiaCuoiKy", Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyHoatDongKhac.ThongTinTruong.TenVietTat = 'HUFLIT'")]
    [ModelDefault("Caption", "Chi tiết thù lao chấm bài, coi thi")]
    [DefaultProperty("Caption")]
    public class ChiTietThuLaoChamBaiCoiThi : BaseObject
    {
        #region Khai báo
        private string _KhoanChi;
        private string _TenMonHoc;
        private string _LopHocPhan;
        //
        private int _SoBaiQuaTrinh;
        private int _SoBaiGiuaKy;
        private int _SoBaiCuoiKy;
        //
        private decimal _DonGiaQuaTrinh;
        private decimal _DonGiaGiuaKy;
        private decimal _DonGiaCuoiKy;
        //
        private decimal _TongTien;
        private decimal _TongTienThueTNCN;
        private QuanLyHoatDongKhac _QuanLyHoatDongKhac;
        private BacDaoTao _BacDaoTao;
        #endregion

        #region Khai báo nhân viên
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        #endregion
        #region Thêm sau
        private decimal _TongGio;
        private string _CMND;
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
        #endregion

        #region Sử dụng

        [ModelDefault("Caption", "Khoản chi")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Size(-1)]
        public string KhoanChi
        {
            get { return _KhoanChi; }
            set { SetPropertyValue("KhoanChi", ref _KhoanChi, value); }
        }

        [ModelDefault("Caption", "Môn học")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Size(-1)]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }

        [ModelDefault("Caption", "Lớp học phần")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Size(-1)]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }

        [ModelDefault("Caption", "Số bài quá trình")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public int SoBaiQuaTrinh
        {
            get { return _SoBaiQuaTrinh; }
            set { SetPropertyValue("SoBaiQuaTrinh", ref _SoBaiQuaTrinh, value); }
        }

        [ModelDefault("Caption", "Số bài giữa kỳ")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public int SoBaiGiuaKy
        {
            get { return _SoBaiGiuaKy; }
            set { SetPropertyValue("SoBaiGiuaKy", ref _SoBaiGiuaKy, value); }
        }

        [ModelDefault("Caption", "Số bài cuối kỳ")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public int SoBaiCuoiKy
        {
            get { return _SoBaiCuoiKy; }
            set { SetPropertyValue("SoBaiCuoiKy", ref _SoBaiCuoiKy, value); }
        }

        [ModelDefault("Caption", "Đơn giá quá trình")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public decimal DonGiaQuaTrinh
        {
            get { return _DonGiaQuaTrinh; }
            set { SetPropertyValue("DonGiaQuaTrinh", ref _DonGiaQuaTrinh, value); }
        }

        [ModelDefault("Caption", "Số bài giữa kỳ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public decimal DonGiaGiuaKy
        {
            get { return _DonGiaGiuaKy; }
            set { SetPropertyValue("DonGiaGiuaKy", ref _DonGiaGiuaKy, value); }
        }

        [ModelDefault("Caption", "Số bài cuối kỳ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public decimal DonGiaCuoiKy
        {
            get { return _DonGiaCuoiKy; }
            set { SetPropertyValue("DonGiaCuoiKy", ref _DonGiaCuoiKy, value); }
        }

        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public decimal TongTien
        {
            get { return _TongTien; }
            set { SetPropertyValue("TongTien", ref _TongTien, value); }
        }
        [ModelDefault("Caption", "Thành tiền (Thuế TNCN)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public decimal TongTienThueTNCN
        {
            get { return _TongTienThueTNCN; }
            set { SetPropertyValue("TongTienThueTNCN", ref _TongTienThueTNCN, value); }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Key")]
        [Association("QuanLyHoatDongKhac-ListChamBaiCoiThi")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuanLyHoatDongKhac QuanLyHoatDongKhac
        {
            get { return _QuanLyHoatDongKhac; }
            set { SetPropertyValue("QuanLyHoatDongKhac", ref _QuanLyHoatDongKhac, value); }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }


        #endregion

        #region Thêm sau

        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }


        [ModelDefault("Caption", "CMND")]
        public string CMND
        {
            get { return _CMND; }
            set { SetPropertyValue("CMND", ref _CMND, value); }
        }
        #endregion

        public ChiTietThuLaoChamBaiCoiThi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }
    }
}
