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
    [DefaultProperty("TenNamHoc")]
    [ModelDefault("Caption", "Năm học")]
    //[RuleCombinationOfPropertiesIsUnique("",DefaultContexts.Save,"NgayBatDau;NgayKetThuc")]
    public class NamHoc : BaseObject
    {
        private DateTime _NgayBatDau;
        private DateTime _NgayKetThuc;
        private bool _KeKhai;


        public NamHoc(Session session) : base(session) { }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày bắt đầu")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayBatDau
        {
            get
            {
                return _NgayBatDau;
            }
            set
            {
                SetPropertyValue("NgayBatDau", ref _NgayBatDau, value);
                if (!IsLoading && value != null && value != DateTime.MinValue)
                {
                    NgayKetThuc = NgayBatDau.Date.AddYears(1).AddDays(-1).AddHours(23).AddSeconds(59);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày kết thúc")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayKetThuc
        {
            get
            {
                return _NgayKetThuc;
            }
            set
            {
                SetPropertyValue("NgayKetThuc", ref _NgayKetThuc, value);
            }
        }

        [Persistent]
        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[RuleUniqueValue("", DefaultContexts.Save)]
        public string TenNamHoc
        {
            get
            {
                return String.Format("{0}{1}", NgayBatDau != DateTime.MinValue ? NgayBatDau.Year.ToString():"", NgayKetThuc != DateTime.MinValue ?"-" + NgayKetThuc.Year.ToString() : "");
               // return ObjectFormatter.Format("{NgayBatDau:yyyy} - {NgayKetThuc:yyyy}", this);
            }
        }


        [ModelDefault("Caption", "Loại phần mềm")]
        public bool KeKhai
        {
            get
            {
                return _KeKhai;
            }
            set
            {
                SetPropertyValue("KeKhai", ref _KeKhai, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách học kỳ")]
        [Association("NamHoc-ListHocKy")]
        public XPCollection<HocKy> ListHocKy
        {
            get
            {
                return GetCollection<HocKy>("ListHocKy");
            }
        }
    }

}
