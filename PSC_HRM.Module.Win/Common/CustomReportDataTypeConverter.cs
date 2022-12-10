using System;
using System.ComponentModel;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp;

namespace PSC_HRM.Module.Win.Common
{
    public class CustomReportDataTypeConverter : LocalizedClassInfoTypeConverter
    {
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            IModelBOModel boModelNode = CaptionHelper.ApplicationModel.BOModel;
            List<Type> result = new List<Type>();
            
            foreach (IModelClass item in boModelNode.GetNodes<IModelClass>())
            {
                ITypeInfo type = XafTypesInfo.Instance.FindTypeInfo(item.Name);
                if (type.Base.Type == typeof(StoreProcedureReport))
                    result.Add(type.Type);
            }

            return new StandardValuesCollection(result);
        }
    }
}
