using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.BusinessObjects.NonPersistentObjects.TaiKhoan
{
    [NonPersistent]
    [ModelDefault("Caption", "Thông tin tài khoản")]
    public class ThongTinTaiKhoan : BaseObject
    {
       public void load()
        {
            NguoiSuDung user = HamDungChung.CurrentUser();
            if (user != null)
            {
                Username = user.UserName;
                DoiMatKhau = user.ChangePasswordOnFirstLogon;
                PhanLoai = user.PhanLoai;
                PhanQuyenBC = user.PhanQuyenBaoCao != null ? user.PhanQuyenBaoCao.Ten : "";
                PhanQuyenBP = user.PhanQuyenBoPhan != null ? user.PhanQuyenBoPhan.Ten : "";
                ThongTinTruong = user.ThongTinTruong.TenBoPhan;
                ThongTinNV = user.ThongTinNhanVien != null ? user.ThongTinNhanVien.HoTen : "";
            }
        }
        private string _Username;
        private bool _DoiMatKhau;
        private AccountTypeEnum _PhanLoai;
        private string _ThongTinTruong;
        private string _ThongTinNV;
        private string _PhanQuyenBC;
        private string _PhanQuyenBP;

        [ModelDefault("Caption", "Tài khoản")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                SetPropertyValue("Username", ref _Username, value);
            }
        }

        [ModelDefault("Caption", "Đổi mật khẩu khi đăng nhập")]
        public bool DoiMatKhau
        {
            get
            {
                return _DoiMatKhau;
            }
            set
            {
                SetPropertyValue("DoiMatKhau", ref _DoiMatKhau, value);
            }
        }
        [ModelDefault("Caption", "Phân quyền đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string PhanQuyenBP
        {
            get
            {
                return _PhanQuyenBP;
            }
            set
            {
                SetPropertyValue("PhanQuyenBP", ref _PhanQuyenBP, value);
            }
        }

        [ModelDefault("Caption", "Phân quyền báo cáo")]
        public string PhanQuyenBC
        {
            get
            {
                return _PhanQuyenBC;
            }
            set
            {
                SetPropertyValue("PhanQuyenBC", ref _PhanQuyenBC, value);
            }
        }

        [ModelDefault("Caption", "Phân loại")]
        public AccountTypeEnum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
            }
        }



        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        public string ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        public string ThongTinNV
        {
            get
            {
                return _ThongTinNV;
            }
            set
            {
                SetPropertyValue("ThongTinNV", ref _ThongTinNV, value);
            }
        }

        public ThongTinTaiKhoan(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            load();
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            load();
        }
    }
}