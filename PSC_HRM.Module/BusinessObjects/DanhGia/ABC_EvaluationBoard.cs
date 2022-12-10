using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DanhGia
{
    [ModelDefault("Caption", "ABC_EvaluationBoard")]
    [DefaultProperty("Name")]
    public class ABC_EvaluationBoard : TruongBaseObject
    {
        // Fields...
        private int _Month;
        private int _Year;
        private string _Name;
        private int _EvaluationBoardType;

        [ModelDefault("Caption", "Tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int Month
        {
            get
            {
                return _Month;
            }
            set
            {
                SetPropertyValue("Month", ref _Month, value);
            }
        }
        
        [ModelDefault("Caption", "Năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int Year
        {
            get
            {
                return _Year;
            }
            set
            {
                SetPropertyValue("Year", ref _Year, value);
            }
        }

        [ModelDefault("Caption", "Tên bảng đánh giá")]
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

        [ModelDefault("Caption", "Loại bảng")]
        public int EvaluationBoardType
        {
            get
            {
                return _EvaluationBoardType;
            }
            set
            {
                SetPropertyValue("EvaluationBoardType", ref _EvaluationBoardType, value);
            }
        }

        public ABC_EvaluationBoard(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }
    }

}
