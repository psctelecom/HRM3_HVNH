using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;

namespace PSC_HRM.Module.PMS.NghiepVu.GDTC_QP
{
    [ModelDefault("Caption", "Chi tiết thống kê GDTC-QP")]
    public class ChiTietThongKeGDQP_TC : BaseObject
    {

        #region key
        private QuanLyGDTC_QP _QuanLyGDTC_QP;
        [Association("QuanLyGDTC_QP-ListChiTietThongKeGDQP_TC")]
        [ModelDefault("Caption", "Quản lý")]
        [Browsable(false)]
        public QuanLyGDTC_QP QuanLyGDTC_QP
        {
            get
            {
                return _QuanLyGDTC_QP;
            }
            set
            {
                SetPropertyValue("QuanLyGDTC_QP", ref _QuanLyGDTC_QP, value);
            }
        }
        #endregion

        #region ThongTin
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private string _SoCMND;
        private string _Email;
        private string _LopHP;
        private string _TenHP;
        private decimal _SoTC;
        private decimal _SoTietLT;
        private decimal _SoTiet_TNTH;
        private decimal _ThaoLuan;
        private int _SiSo;
        private string _HK;
        private KyTinhPMS _KyTinhPMS;
        #endregion

        #region ThongTin
        [ModelDefault("Caption", "Bộ phận")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Số CMND")]
        public string SoCMND
        {
            get { return _SoCMND; }
            set { SetPropertyValue("SoCMND", ref _SoCMND, value); }
        }

        [ModelDefault("Caption", "Email")]
        public string Email
        {
            get { return _Email; }
            set { SetPropertyValue("Email", ref _Email, value); }
        }

        [ModelDefault("Caption", "Lớp học phần")]
        public string LopHP
        {
            get { return _LopHP; }
            set { SetPropertyValue("LopHP", ref _LopHP, value); }
        }

        [ModelDefault("Caption", "Tên học phần")]
        public string TenHP
        {
            get { return _TenHP; }
            set { SetPropertyValue("TenHP", ref _TenHP, value); }
        }

        [ModelDefault("Caption", "Số tín chỉ")]
        public decimal SoTC
        {
            get { return _SoTC; }
            set { SetPropertyValue("SoTC", ref _SoTC, value); }
        }

        [ModelDefault("Caption", "Số tiết LT")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietLT
        {
            get { return _SoTietLT; }
            set { SetPropertyValue("SoTietLT", ref _SoTietLT, value); }
        }

        [ModelDefault("Caption", "Số tiết TNTH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTiet_TNTH
        {
            get { return _SoTiet_TNTH; }
            set { SetPropertyValue("SoTiet_TNTH", ref _SoTiet_TNTH, value); }
        }
        [ModelDefault("Caption", "Thảo Luận")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ThaoLuan
        {
            get { return _ThaoLuan; }
            set { SetPropertyValue("ThaoLuan", ref _ThaoLuan, value); }
        }

        [ModelDefault("Caption", "Sĩ số")]
        public int SiSo
        {
            get { return _SiSo; }
            set { SetPropertyValue("SiSo", ref _SiSo, value); }
        }

        [ModelDefault("Caption", "Học kỳ")]
        public string HK
        {
            get { return _HK; }
            set { SetPropertyValue("HK", ref _HK, value); }
        }

        [ModelDefault("Caption", "Kỳ PMS")]
        public KyTinhPMS KyTinhPMS
        {
            get { return _KyTinhPMS; }
            set { SetPropertyValue("KyTinhPMS", ref _KyTinhPMS, value); }
        }
        #endregion
        public ChiTietThongKeGDQP_TC(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}