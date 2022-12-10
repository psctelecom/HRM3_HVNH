using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.NghiepVu.PhiGiaoVu
{
    [ModelDefault("Caption", "Chi tiết giáo vụ phí")]
    [Appearance("Hide_HocKy", TargetItems = "HeSo_HocKyHe;GiaoVuPhi_HocKyHe", Visibility = ViewItemVisibility.Hide, Criteria = "!QuanLyPhiGiaoVu.HocKy.HocKyHe")]
    public class ChiTietPhiGiaoVu : BaseObject
    {
        private string _MaHocPhan;
        private string _TenHocPhan;
        private string _MaLopHocPhan;
        private int _SiSo;
        private decimal _HeSo_GiaoVu;
        private decimal _HeSo_HocKyHe;
        private decimal _GiaoVuPhi_HocKyHe;
        private string _GhiChu;
        private BoPhan _BoPhan;
        private QuanLyPhiGiaoVu _QuanLyPhiGiaoVu;

        [ModelDefault("Caption", "Quản lý bộ phận")]
        [Association("QuanLyPhiGiaoVu-ListChiTietPhiGiaoVu")]
        [Browsable(false)]

        public QuanLyPhiGiaoVu QuanLyPhiGiaoVu
        {
            get { return _QuanLyPhiGiaoVu; }
            set { SetPropertyValue("QuanLyPhiGiaoVu", ref _QuanLyPhiGiaoVu, value); }
        }

        [ModelDefault("Caption", "Mã học phần")]
        public string MaHocPhan
        {
            get { return _MaHocPhan; }
            set { SetPropertyValue("MaHocPhan", ref _MaHocPhan, value); }
        }

        [ModelDefault("Caption", "Tên học phần")]
        public string TenHocPhan
        {
            get { return _TenHocPhan; }
            set { SetPropertyValue("TenHocPhan", ref _TenHocPhan, value); }
        }

        [ModelDefault("Caption", "Mã lớp học phần")]
        public string MaLopHocPhan
        {
            get { return _MaLopHocPhan; }
            set { SetPropertyValue("MaLopHocPhan", ref _MaLopHocPhan, value); }
        }

        [ModelDefault("Caption", "Sĩ số")]
        public int SiSo
        {
            get { return _SiSo; }
            set { SetPropertyValue("SiSo", ref _SiSo, value); }
        }

        [ModelDefault("Caption", "Hệ số giáo vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_GiaoVu
        {
            get { return _HeSo_GiaoVu; }
            set { SetPropertyValue("HeSo_GiaoVu", ref _HeSo_GiaoVu, value); }
        }

        [ModelDefault("Caption", "Hệ số học kỳ hè")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_HocKyHe
        {
            get { return _HeSo_HocKyHe; }
            set { SetPropertyValue("HeSo_HocKyHe", ref _HeSo_HocKyHe, value); }
        }

        [ModelDefault("Caption", "Giáo vụ phí hè")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GiaoVuPhi_HocKyHe
        {
            get { return _GiaoVuPhi_HocKyHe; }
            set { SetPropertyValue("GiaoVuPhi_HocKyHe", ref _GiaoVuPhi_HocKyHe, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Bộ phận")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }

        public ChiTietPhiGiaoVu(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.          
        }
    }
}