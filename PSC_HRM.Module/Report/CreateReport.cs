using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;

namespace PSC_HRM.Module.Report
{

    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Tạo báo cáo")]
    public class CreateReport : BaseObject
    {
        // Fields...
        private GroupReport _NhomBaoCao;
        private string _TenBaoCao;
        private Type _DataType;
        private Type _TargetType;

        [ModelDefault("Caption", "Tên báo cáo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenBaoCao
        {
            get
            {
                return _TenBaoCao;
            }
            set
            {
                SetPropertyValue("TenBaoCao", ref _TenBaoCao, value);
            }
        }

        [ModelDefault("Caption", "Nhóm báo cáo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public GroupReport NhomBaoCao
        {
            get
            {
                return _NhomBaoCao;
            }
            set
            {
                SetPropertyValue("NhomBaoCao", ref _NhomBaoCao, value);
            }
        }

        [ModelDefault("Caption", "Kiểu dữ liệu")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.ReportTypeEditor")]
        public Type DataType
        {
            get
            {
                return _DataType;
            }
            set
            {
                SetPropertyValue("DataType", ref _DataType, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Hiển thị trên cửa sổ")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.PersistentTypeEditor")]
        public Type TargetType
        {
            get
            {
                return _TargetType;
            }
            set
            {
                SetPropertyValue("TargetType", ref _TargetType, value);
            }
        }

        public CreateReport(Session session) : base(session) { }
    }
}
