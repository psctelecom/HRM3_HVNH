using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;


namespace PSC_HRM.Module.Website
{
    [DefaultProperty("TenPhanQuyenDonVi")]
    [ModelDefault("Caption", "Phân quyền đơn vị")]
    public class TaiKhoan : BaseObject
    {
        // Fields...
        private bool _QuanTri;
        private bool _DangHoatDong;
        private PhanQuyenDonVi _PhanQuyenDonVi;
        private NhomQuyen _PhanQuyenChucNang;
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _TenTaiKhoan;
        private string _Password;

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Phân quyền chức năng")]
        public string TenTaiKhoan
        {
            get
            {
                return _TenTaiKhoan;
            }
            set
            {
                SetPropertyValue("TenTaiKhoan", ref _TenTaiKhoan, value);
            }
        }

        [Browsable(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Phân quyền chức năng")]
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                SetPropertyValue("Password", ref _Password, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Phân quyền chức năng")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Phân quyền chức năng")]
        public NhomQuyen PhanQuyenChucNang
        {
            get
            {
                return _PhanQuyenChucNang;
            }
            set
            {
                SetPropertyValue("PhanQuyenChucNang", ref _PhanQuyenChucNang, value);
            }
        }

        [ModelDefault("Caption", "Phân quyền đơn vị")]
        public PhanQuyenDonVi PhanQuyenDonVi
        {
            get
            {
                return _PhanQuyenDonVi;
            }
            set
            {
                SetPropertyValue("PhanQuyenDonVi", ref _PhanQuyenDonVi, value);
            }
        }

        [ModelDefault("Caption", "Đang hoạt động")]
        public bool DangHoatDong
        {
            get
            {
                return _DangHoatDong;
            }
            set
            {
                SetPropertyValue("DangHoatDong", ref _DangHoatDong, value);
            }
        }

        [ModelDefault("Caption", "Quản trị")]
        public bool QuanTri
        {
            get
            {
                return _QuanTri;
            }
            set
            {
                SetPropertyValue("QuanTri", ref _QuanTri, value);
            }
        }

        public TaiKhoan(Session session) : base(session) { }
    }
}
