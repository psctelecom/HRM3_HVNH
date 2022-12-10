using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
//5689
//cuong123
//long
//long  
namespace PSC_HRM.Module
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Biểu mẫu")]
    [DefaultProperty("TenBieuMau")]
    [RuleCombinationOfPropertiesIsUnique("BieuMau.Unique", DefaultContexts.Save, "TenBieuMau;TargetTypeName")]
    public class BieuMau : BaseObject
    {
        // Fields...
        private FileData _File;
        private string _TenBieuMau;
        private string _TargetTypeName;

        [ModelDefault("Caption", "Tên biểu mẫu")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenBieuMau
        {
            get
            {
                return _TenBieuMau;
            }
            set
            {
                SetPropertyValue("TenBieuMau", ref _TenBieuMau, value);
            }
        }

        [Browsable(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TargetTypeName
        {
            get
            {
                return _TargetTypeName;
            }
            set
            {
                SetPropertyValue("TargetTypeName", ref _TargetTypeName, value);
            }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Kiểu dữ liệu đích")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.TemplateTypeEditor")]
        public Type TargetType
        {
            get
            {
                if (!String.IsNullOrEmpty(TargetTypeName))
                {
                    ITypeInfo info = XafTypesInfo.Instance.FindTypeInfo(TargetTypeName);
                    if (info != null)
                        return info.Type;
                    return null;
                }
                return null;
            }
            set
            {
                TargetTypeName = (value != null) ? value.FullName : string.Empty;
            }
        }

        [ModelDefault("Caption", "Lưu trữ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public FileData File
        {
            get
            {
                return _File;
            }
            set
            {
                SetPropertyValue("File", ref _File, value);
            }
        }

        public BieuMau(Session session) : base(session) { }
    }

}
