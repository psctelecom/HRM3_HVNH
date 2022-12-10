using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BaoHiem
{
    [DefaultClassOptions]
    [DefaultProperty("Caption")]
    [ImageName("BO_DeNghiCapSo")]    
    [ModelDefault("Caption", "Quản lý đề nghị cấp sổ")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;ThoiGian;Dot")]
    public class QuanLyDeNghiCapSo : BaoMatBaseObject
    {
        // Fields...
        private DateTime _ThoiGian;
        private int _Dot;

        [ModelDefault("Caption", "Thời gian")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
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

        [ModelDefault("Caption", "Đợt")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Dot
        {
            get
            {
                return _Dot;
            }
            set
            {
                SetPropertyValue("Dot", ref _Dot, value);
            }
        }

        [Browsable(false)]
        public string Caption 
        { 
            get
            {
                if (ThoiGian != DateTime.MinValue && Dot > 0)
                    return ObjectFormatter.Format("Tháng {ThoiGian:MM/yyyy} (Đợt {Dot})", this);
                return "";
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách đề nghị cấp sổ BHXH, BHYT")]
        [Association("QuanLyDeNghiCapSo-ListDeNghiCapSo")]
        public XPCollection<DeNghiCapSo> ListDeNghiCapSo
        {
            get
            {
                return GetCollection<DeNghiCapSo>("ListDeNghiCapSo");
            }
        }

        public QuanLyDeNghiCapSo(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThoiGian = HamDungChung.GetServerTime();
            Dot = 1;
        }
    }

}
