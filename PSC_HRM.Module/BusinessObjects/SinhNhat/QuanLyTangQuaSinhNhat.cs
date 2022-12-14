using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.SinhNhat
{
    [DefaultClassOptions]
    [ImageName("BO_NghiHuu")]
    [ModelDefault("Caption", "Quản lý tặng quà sinh nhật")]
    public class QuanLyTangQuaSinhNhat : BaoMatBaseObject
    {
        // Fields...
        private DateTime _Thang;

        [ModelDefault("Caption", "Tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.SetTime(SetTimeEnum.StartMonth);
                SetPropertyValue("Thang", ref _Thang, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuanLyTangQuaSinhNhat-ListChiTietTangQuaSinhNhat")]
        public XPCollection<ChiTietTangQuaSinhNhat> ListChiTietTangQuaSinhNhat
        {
            get
            {
                return GetCollection<ChiTietTangQuaSinhNhat>("ListChiTietTangQuaSinhNhat");
            }
        }

        public QuanLyTangQuaSinhNhat(Session session)
            : base(session) 
        { }
    }

}
