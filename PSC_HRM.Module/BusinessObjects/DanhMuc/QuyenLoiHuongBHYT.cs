using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    //thương binh, gia đình có công với cách mạng,...
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenQuyenLoi")]
    [ModelDefault("Caption", "Quyền lợi hưởng BHYT")]
    [RuleCombinationOfPropertiesIsUnique("QuyenLoiBHYT.Unique", DefaultContexts.Save, "MaQuanLy;TenQuyenLoi")]
    public class QuyenLoiHuongBHYT : BaseObject
    {
        // Fields...
        private string _TenQuyenLoi;
        private string _MaQuanLy;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên quyền lợi")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenQuyenLoi
        {
            get
            {
                return _TenQuyenLoi;
            }
            set
            {
                SetPropertyValue("TenQuyenLoi", ref _TenQuyenLoi, value);
            }
        }

        public QuyenLoiHuongBHYT(Session session) : base(session) { }
    }

}
