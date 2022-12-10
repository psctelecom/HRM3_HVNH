using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("Nam")]
    [ModelDefault("Caption", "Ngày nghỉ trong năm")]
    public class QuanLyNgayNghiTrongNam : BaseObject
    {
        // Fields...
        private int _Nam;

        [ModelDefault("Caption", "Năm")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Ngày nghỉ trong năm")]
        [Association("QuanLyNgayNghiTrongNam-ListNgayNghiTrongNam")]
        public XPCollection<NgayNghiTrongNam> ListNgayNghiTrongNam
        {
            get
            {
                return GetCollection<NgayNghiTrongNam>("ListNgayNghiTrongNam");
            }
        }

        public QuanLyNgayNghiTrongNam(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DateTime current = HamDungChung.GetServerTime();
            Nam = current.Year;
        }
    }

}
