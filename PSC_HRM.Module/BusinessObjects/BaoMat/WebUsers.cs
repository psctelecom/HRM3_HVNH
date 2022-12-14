using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.Persistent.BaseImpl;

namespace PSC_HRM.Module.BaoMat
{
    [DefaultClassOptions]
    [ImageName("BO_User")]
    [DefaultProperty("UserName")]
    [ModelDefault("Caption", "Web user")]
    public class WebUsers : BaseObject
    {
        // Fields...
        private bool _UserChamCong;
        private bool _HoatDong;
        private Guid _WebGroupID;
        private string _UserName;
        private string _Password;
        private string _AdminEmail;
        private ThongTinNhanVien _ThongTinNhanVien;

        private GiangVienThinhGiang _GiangVienThinhGiang;
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
        public GiangVienThinhGiang GiangVienThinhGiang
        {
            get
            {
                return _GiangVienThinhGiang;
            }
            set
            {
                SetPropertyValue("GiangVienThinhGiang", ref _GiangVienThinhGiang, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                SetPropertyValue("UserName", ref _UserName, value);
            }
        }
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                SetPropertyValue("PassWord", ref _Password, value);
            }
        }

        public bool HoatDong
        {
            get
            {
                return _HoatDong;
            }
            set
            {
                SetPropertyValue("HoatDong", ref _HoatDong, value);
            }
        }
       
        public bool UserChamCong
        {
            get
            {
                return _UserChamCong;
            }
            set
            {
                SetPropertyValue("UserChamCong", ref _UserChamCong, value);
            }
        }

        public Guid WebGroupID
        {
            get
            {
                return _WebGroupID;
            }
            set
            {
                SetPropertyValue("WebGroupID", ref _WebGroupID, value);

            }
        }

        public string AdminEmail
        {
            get
            {
                return _AdminEmail;
            }
            set
            {
                SetPropertyValue("AdminEmail", ref _AdminEmail, value);
            }
        }

        public WebUsers(Session session) : base(session) { }

      
    }

}
