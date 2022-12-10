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

    [ModelDefault("Caption", "HD thanh toán tiền mặt")]
    [DefaultProperty("Caption")]
    public class HoatDongKhac_ThanhToanTienMat : BaseObject
    {
        #region key
        //private QuanLyHoatDongKhac _QuanLyHoatDongKhac;
        //[Association("QuanLyHoatDongKhac-ListHoatDongKhac_ThanhToanTienMat")]
        //[ModelDefault("Caption", "Khối lượng giảng dạy")]
        //[Browsable(false)]
        //public QuanLyHoatDongKhac QuanLyHoatDongKhac
        //{
        //    get
        //    {
        //        return _QuanLyHoatDongKhac;
        //    }
        //    set
        //    {
        //        SetPropertyValue("QuanLyHoatDongKhac", ref _QuanLyHoatDongKhac, value);
        //    }
        //}
        #endregion

        #region Khai báo
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private HoatDongKhac _HoatDong;
        private string _DienGiai;
        private BacDaoTao _BacDaoTao;
        private decimal _SoTienThanhToan;
        #endregion

        #region Giá trị
        [ModelDefault("Caption", "Bộ phận")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        [ModelDefault("Caption", "Loại hoạt động")]
        [RuleRequiredField(DefaultContexts.Save)]
        public HoatDongKhac HoatDong
        {
            get { return _HoatDong; }
            set { SetPropertyValue("HoatDong", ref _HoatDong, value); }
        }
        [ModelDefault("Caption", "Diễn giải")]
        [Size(-1)]
        public string DienGiai
        {
            get { return _DienGiai; }
            set { SetPropertyValue("DienGiai", ref _DienGiai, value); }
        }
        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
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
        #endregion

        public HoatDongKhac_ThanhToanTienMat(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}