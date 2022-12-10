using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenUuTienGiaDinh")]
    [ModelDefault("Caption", "Ưu tiên gia đình")]
    public class UuTienGiaDinh : BaseObject
    {
        private string _MaQuanLy;
        private string _TenUuTienGiaDinh;

        public UuTienGiaDinh(Session session) : base(session) { }

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên ưu tiên gia đình")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenUuTienGiaDinh
        {
            get
            {
                return _TenUuTienGiaDinh;
            }
            set
            {
                SetPropertyValue("TenUuTienGiaDinh", ref _TenUuTienGiaDinh, value);
            }
        }

    }

}
