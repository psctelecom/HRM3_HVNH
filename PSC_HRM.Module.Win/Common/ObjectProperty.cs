using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Win.Common
{
    public class ObjectProperty
    {
        [DisplayName("Chọn")]
        public bool Chon { get; set; }
        [Browsable(false)]
        public int Index { get; set; }
        [Browsable(false)]
        public string PropertyName { get; set; }
        [DisplayName("Thuộc tính")]
        public string DisplayName { get; set; }
    }

    public static class ObjectPropertyHelper
    {
        public static List<ObjectProperty> GetDataSource(Type type)
        {
            List<ObjectProperty> dataSource = new List<ObjectProperty>();
            PropertyInfo[] pis = type.GetProperties();
            int index = 0;
            foreach (PropertyInfo item in pis)
            {
                object[] attrs = item.GetCustomAttributes(typeof(ModelDefaultAttribute), false);
                string attr = string.Empty;
                ModelDefaultAttribute mda;
                foreach (object obj in attrs)
                {
                    mda = obj as ModelDefaultAttribute;
                    if (mda != null && mda.PropertyName == "Caption")
                    {
                        attr = mda.PropertyValue;
                    }
                }


                if (!string.IsNullOrWhiteSpace(attr))
                    dataSource.Add(new ObjectProperty { Index = index++, PropertyName = item.Name, DisplayName = attr });
                else
                    dataSource.Add(new ObjectProperty { Index = index++, PropertyName = item.Name, DisplayName = item.Name });
            }

            return dataSource;
        }
    }
}
