using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BaoHiem
{
    [DefaultClassOptions]
    [ImageName("BO_TroCap")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Quản lý thanh toán bảo hiểm")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;ThoiGian")]
    public class QuanLyTroCap : BaoMatBaseObject
    {
        // Fields...
        private DateTime _ThoiGian;

        [ModelDefault("Caption", "Thời gian")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime ThoiGian
        {
            get
            {
                return _ThoiGian;
            }
            set
            {
                SetPropertyValue("ThoiGian", ref _ThoiGian, value);
            }
        }

        [Browsable(false)]
        public string Caption
        {
            get
            {
                if (ThoiGian != DateTime.MinValue)
                    return ObjectFormatter.Format("Tháng {ThoiGian:MM/yyyy}", this);
                return "";
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách trợ cấp")]
        [Association("QuanLyTroCap-ListTroCap")]
        public XPCollection<TroCap> ListTroCap
        {
            get
            {
                return GetCollection<TroCap>("ListTroCap");
            }
        }

        public QuanLyTroCap(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (ThoiGian == DateTime.MinValue)
                ThoiGian = HamDungChung.GetServerTime();
        }
    }

}
