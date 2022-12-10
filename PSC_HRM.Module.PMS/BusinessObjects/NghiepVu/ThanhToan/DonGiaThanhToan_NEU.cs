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

    [ModelDefault("Caption", "Đơn giá thanh toán mới")]
    //[DefaultProperty("Caption")]
    //[RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "ThongTinTruong;NamHoc;HocKy", "Đơn giá thanh toán vượt giờ đã tồn tại")]
    public class DonGiaThanhToan_NEU : BaseObject
    {
        private HeDaoTao _HeDaoTao;
        private BacDaoTao _BacDaoTao;
        private NgonNguEnum _NgonNguGiangDay;
        private decimal _DonGiaThanhToan;
        private decimal _DonGiaBaiKiemTra;

        [ModelDefault("Caption", "Hệ đào tạo")]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        [ModelDefault("Caption", "Ngôn ngữ giảng dạy")]
        public NgonNguEnum NgonNguGiangDay
        {
            get { return _NgonNguGiangDay; }
            set { SetPropertyValue("NgonNguGiangDay", ref _NgonNguGiangDay, value); }
        }
        [ModelDefault("Caption", "Đơn giá thanh toán")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DonGiaThanhToan
        {
            get { return _DonGiaThanhToan; }
            set { SetPropertyValue("DonGiaThanhToan", ref _DonGiaThanhToan, value); }
        }

        [ModelDefault("Caption", "Đơn giá bài kiểm tra")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DonGiaBaiKiemTra
        {
            get { return _DonGiaBaiKiemTra; }
            set { SetPropertyValue("DonGiaBaiKiemTra", ref _DonGiaBaiKiemTra, value); }
        }


        public DonGiaThanhToan_NEU(Session session) : base(session) { }
    }
}
