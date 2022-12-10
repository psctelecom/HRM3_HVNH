using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.CauHinh
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình Website")]
    [Appearance("CauHinhWebsite", TargetItems = "TenTaiKhoan", Visibility = ViewItemVisibility.Hide, Criteria = "!TuDongTaoTaiKhoan")]
    public class CauHinhWebsite : BaseObject
    {
        // Fields...
        // Fields...
        private TenTaiKhoanEnum _TenTaiKhoan = TenTaiKhoanEnum.MaQuanLy;
        private bool _TuDongTaoTaiKhoan = false;

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo tài khoản")]
        public bool TuDongTaoTaiKhoan
        {
            get
            {
                return _TuDongTaoTaiKhoan;
            }
            set
            {
                SetPropertyValue("TuDongTaoTaiKhoan", ref _TuDongTaoTaiKhoan, value);
            }
        }

        [ModelDefault("Caption", "Tên tài khoản theo")]
        public TenTaiKhoanEnum TenTaiKhoan
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

        public CauHinhWebsite(Session session) : base(session) { }
    }

}
