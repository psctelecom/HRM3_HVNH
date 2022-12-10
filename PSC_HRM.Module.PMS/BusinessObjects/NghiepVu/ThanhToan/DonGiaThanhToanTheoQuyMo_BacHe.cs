using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.DanhMuc;

namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.ThanhToan
{
    [ModelDefault("Caption", "Đơn giá quy mô(bậc hệ)")]
    [Appearance("Hide_NEU", TargetItems = "KyTinhPMS;HocKy"
            , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.MaQuanLy = 'NEU'")]
    public class DonGiaThanhToanTheoQuyMo_BacHe : ThongTinChungPMS
    {
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;
        private int _TuKhoan;
        private int _DenKhoan;
        private decimal _DonGia;

        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }

        [ModelDefault("Caption", "Từ khoản")]
        public int TuKhoan
        {
            get { return _TuKhoan; }
            set { SetPropertyValue("TuKhoan", ref _TuKhoan, value); }
        }

        [ModelDefault("Caption", "Đến khoản")]
        public int DenKhoan
        {
            get { return _DenKhoan; }
            set { SetPropertyValue("DenKhoan", ref _DenKhoan, value); }
        }

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("DonGiaTienGiang", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set { SetPropertyValue("DonGia", ref _DonGia, value); }
        }
        public DonGiaThanhToanTheoQuyMo_BacHe(Session session) : base(session) { }
       
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}