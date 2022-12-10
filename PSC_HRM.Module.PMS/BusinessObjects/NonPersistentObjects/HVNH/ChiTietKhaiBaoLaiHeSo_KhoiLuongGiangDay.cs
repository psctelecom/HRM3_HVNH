using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    public class ChiTietKhaiBaoLaiHeSo_KhoiLuongGiangDay : BaseObject
    {
        private bool _Chon;
        private string _MaGV;
        private string _HoTen;
        private string _LopHocPhan;
        private string _LopSinhVien;
        private string _BacHeDaoTao;
        private string _MaHP;
        private string _TenHocPhan;
        private decimal _HeSoTinhChi;
        private decimal _HeSoLopDong;
        private decimal _HeSoNgoaiGio;
        private decimal _HeSoBacDaoTao;
        private decimal _HeSoNgonNgu;
        private Guid _OidChiTiet;

        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }

        [ModelDefault("Caption", "Mã GV")]
        public string MaGV
        {
            get
            {
                return _MaGV;
            }
            set
            {
                SetPropertyValue("MaGV", ref _MaGV, value);
            }
        }
        [ModelDefault("Caption", "Họ tên GV")]
        public string HoTen
        {
            get
            {
                return _HoTen;
            }
            set
            {
                SetPropertyValue("HoTen", ref _HoTen, value);
            }
        }

        [ModelDefault("Caption", "Bậc hệ đào tạo")]
        public string BacHeDaoTao
        {
            get
            {
                return _BacHeDaoTao;
            }
            set
            {
                SetPropertyValue("BacHeDaoTao", ref _BacHeDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Lớp học phần")]
        public string LopHocPhan
        {
            get
            {
                return _LopHocPhan;
            }
            set
            {
                SetPropertyValue("LopHocPhan", ref _LopHocPhan, value);
            }
        }
        [ModelDefault("Caption", "Lớp sinh viên")]
        public string LopSinhVien
        {
            get
            {
                return _LopSinhVien;
            }
            set
            {
                SetPropertyValue("LopSinhVien", ref _LopSinhVien, value);
            }
        }
        [ModelDefault("Caption", "Mã môn học")]
        public string MaHP
        {
            get
            {
                return _MaHP;
            }
            set
            {
                SetPropertyValue("MaHP", ref _MaHP, value);
            }
        }
        [ModelDefault("Caption", "Tên môn học")]
        public string TenHocPhan
        {
            get
            {
                return _TenHocPhan;
            }
            set
            {
                SetPropertyValue("TenHocPhan", ref _TenHocPhan, value);
            }
        }
        [Browsable(false)]
        public Guid OidChiTiet
        {
            get { return _OidChiTiet; }
            set { SetPropertyValue("OidChiTiet", ref _OidChiTiet, value); }
        }
        
        [ModelDefault("Caption", "Hệ số bậc đào tạo")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoBacDaoTao
        {
            get
            {
                return _HeSoBacDaoTao;
            }
            set
            {
                SetPropertyValue("HeSoBacDaoTao", ref _HeSoBacDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Hệ số lớp đông")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLopDong
        {
            get
            {
                return _HeSoLopDong;
            }
            set
            {
                SetPropertyValue("HeSoLopDong", ref _HeSoLopDong, value);
            }
        }

        [ModelDefault("Caption", "Hệ số ngoài giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoNgoaiGio
        {
            get
            {
                return _HeSoNgoaiGio;
            }
            set
            {
                SetPropertyValue("HeSoNgoaiGio", ref _HeSoNgoaiGio, value);
            }
        }

        [ModelDefault("Caption", "Hệ số ngôn ngữ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoNgonNgu
        {
            get
            {
                return _HeSoNgonNgu;
            }
            set
            {
                SetPropertyValue("HeSoNgonNgu", ref _HeSoNgonNgu, value);
            }
        }

        [ModelDefault("Caption", "Hệ số tín chỉ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoTinhChi
        {
            get
            {
                return _HeSoTinhChi;
            }
            set
            {
                SetPropertyValue("HeSoTinhChi", ref _HeSoTinhChi, value);
            }
        }
        public ChiTietKhaiBaoLaiHeSo_KhoiLuongGiangDay(Session session)
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