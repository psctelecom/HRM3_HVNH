using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.BaoHiem
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "TKD02TK1_TNN")]
    public class TKD02TK1_TNN : BaoMatBaseObject
    {
        private DateTime _Thang;
        private int _Nam;
        private DateTime _NgayLap;

        [ModelDefault("Caption", "Tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                SetPropertyValue("Thang", ref _Thang, value);
            }
        }

        [ModelDefault("Caption", "Năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int Nam
        {
            get
            {
                return Thang.Year;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }


        [ModelDefault("Caption", "Ngày lập")]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách")]
        [Association("TKD02TK1_TNN-ListChiTietTKD02TK1_TNN")]
        public XPCollection<ChiTietTKD02TK1_TNN> ListChiTietTKD02TK1_TNN
        {
            get
            {
                return GetCollection<ChiTietTKD02TK1_TNN>("ListChiTietTKD02TK1_TNN");
            }
        }


        public TKD02TK1_TNN(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Thang = HamDungChung.GetServerTime();
            NgayLap = HamDungChung.GetServerTime();

        }

    }

}
