using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSC_NormalizationData
{
    internal class DictionaryNormalizationData
    {
        public Object Id { get; set; }
        public string Name { get; set; }
        //public string Class { get; set; }
        public string TABLE_NAME { get; set; }
        public string TABLE_SCHEMA { get; set; }
        public string PrimaryKey { get; set; }
        //public string DisplayColumn { get; set; }
        public string ColumnName { get; set; }
        public string ColumnCode { get; set; }
        public string Predicate { get; set; }
        public List<DanhMuc> DanhMucList { get; set; }

        public DictionaryNormalizationData()
        {
            //DanhMucList = new List<DanhMuc>();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    internal class DanhMuc
    {
        public string PrimaryKey { get; set; }
        public Object Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        //public string Class { get; set; }
        public string TABLE_NAME { get; set; }
        public string TABLE_SCHEMA { get; set; }
        public List<Foreign> ForeignList { get; set; }
        public DanhMuc()
        {
            //ForeignList = new List<Foreign>();
        }
    }

    internal class Foreign
    {
        //public string IdColumn { get; set; }
        public string TABLE_NAME { get; set; }
        public string TABLE_SCHEMA { get; set; }
        public string ForeignColumn { get; set; }
        //public List<Data> DataList { get; set; }
        public Foreign()
        {
            //DataList = new List<Data>();
        }
    }
    //internal class Data
    //{
    //    public Object RowId { get; set; }
    //}
}
