using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [ModelDefault("Caption", "Nhóm báo cáo")]
    [ImageName("BO_Category")]
    [DefaultProperty("TenNhom")]
    public class GroupReport : BaseObject
    {
        private int _STT;
        private string _TenNhom;

        [ModelDefault("Caption", "Số thứ tự")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int STT
        {
            get
            {
                return _STT;
            }
            set
            {
                SetPropertyValue("STT", ref _STT, value);
            }
        }

        [ModelDefault("Caption", "Tên nhóm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNhom
        {
            get
            {
                return _TenNhom;
            }
            set
            {
                SetPropertyValue("TenNhom", ref _TenNhom, value);
            }
        }

        public GroupReport(Session session) : base(session) { }
    }

}
