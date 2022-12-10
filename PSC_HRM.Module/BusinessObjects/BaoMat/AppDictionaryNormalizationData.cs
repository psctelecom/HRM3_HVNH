using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_NormalizationData;

namespace PSC_HRM.Module.BaoMat
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Chuẩn hóa danh mục")]
    [ImageName("BO_Position")]
    public class AppDictionaryNormalizationData : BaseObject
    {
        private string _Name;
        private string _TABLE_SCHEMA;
        private string _TABLE_NAME;
        private string _ColumnName;
        private string _ColumnCode;
        private string _Predicate;

        public string ColumnCode
        {
            get
            {
                return _ColumnCode;
            }
            set
            {
                SetPropertyValue("ColumnCode", ref _ColumnCode, value);
            }
        }
        public string ColumnName
        {
            get
            {
                return _ColumnName;
            }
            set
            {
                SetPropertyValue("ColumnName", ref _ColumnName, value);
            }
        }
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                SetPropertyValue("Name", ref _Name, value);
            }
        }
        public string Predicate
        {
            get
            {
                return _Predicate;
            }
            set
            {
                SetPropertyValue("Predicate", ref _Predicate, value);
            }
        }
        public string TABLE_NAME
        {
            get
            {
                return _TABLE_NAME;
            }
            set
            {
                SetPropertyValue("TABLE_NAME", ref _TABLE_NAME, value);
            }
        }
        public string TABLE_SCHEMA
        {
            get
            {
                return _TABLE_SCHEMA;
            }
            set
            {
                SetPropertyValue("TABLE_SCHEMA", ref _TABLE_SCHEMA, value);
            }
        }
        public AppDictionaryNormalizationData(Session session)
            : base(session)
        {
        }
    }

}
