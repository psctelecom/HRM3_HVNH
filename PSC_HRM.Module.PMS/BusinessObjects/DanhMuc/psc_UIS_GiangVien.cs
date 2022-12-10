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

namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Kết nối nhân viên")]
    [DefaultProperty("HoTen")]   
    public class psc_UIS_GiangVien : BaseObject
    {
        private string _ProfessorID;
        private Guid _OidNhanVien;
        private string _LastName;
        private string _MiddleName;
        private string _FirstName;
        private string _BirthDay;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string ProfessorID
        {
            get { return _ProfessorID; }
            set { SetPropertyValue("ProfessorID", ref _ProfessorID, value); }
        }

        [ModelDefault("Caption", "Oid Nhân viên")]
        public Guid OidNhanVien
        {
            get { return _OidNhanVien; }
            set { SetPropertyValue("OidNhanVien", ref _OidNhanVien, value); }
        }

        [ModelDefault("Caption", "Họ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string LastName
        {
            get { return _LastName; }
            set { SetPropertyValue("LastName", ref _LastName, value); }
        }

        [ModelDefault("Caption", "Tên điệm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MiddleName
        {
            get { return _MiddleName; }
            set { SetPropertyValue("MiddleName", ref _MiddleName, value); }
        }

        [ModelDefault("Caption", "Tên")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string FirstName
        {
            get { return _FirstName; }
            set { SetPropertyValue("FirstName", ref _FirstName, value); }
        }


        [ModelDefault("Caption", "Họ tên")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        public string HoTen
        {
            get
            {
                if (MiddleName == null || MiddleName == string.Empty)
                {                                     
                    return LastName + " " + FirstName;
                }
                else
                {
                    return LastName + " " + MiddleName + " " + FirstName;
                }
            }
        }
        [ModelDefault("Caption", "Tên")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string BirthDay
        {
            get { return _BirthDay; }
            set { SetPropertyValue("BirthDay", ref _BirthDay, value); }
        }
        public psc_UIS_GiangVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
