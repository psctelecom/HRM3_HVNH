using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{

    [ModelDefault("Caption", "Hệ số dạy online")]
    [DefaultProperty("TenCoSo")]
    public class HeSoDayOnline : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoDayOnline")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }
        private CoSoGiangDay _CoSoGiangDay;
        private string _TenCoSo;
        private decimal _HeSo_CoSo;      

        [ModelDefault("Caption", "Tên cơ sở")]
        [Browsable(false)]
        public string TenCoSo
        {
            get { return _TenCoSo; }
            set { SetPropertyValue("TenCoSo", ref _TenCoSo, value); }
        }
        [ModelDefault("Caption", "Tên cơ sở")]
        public CoSoGiangDay CoSoGiangDay
        {
            get { return _CoSoGiangDay; }
            set { SetPropertyValue("CoSoGiangDay", ref _CoSoGiangDay, value);
            if (!IsLoading)
                if (CoSoGiangDay != null)
                    TenCoSo = CoSoGiangDay.TenCoSo;
                else
                    TenCoSo = "";
            }
        }     
       
        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSoDayOnline", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_CoSo
        {
            get { return _HeSo_CoSo; }
            set { SetPropertyValue("HeSo_CoSo", ref _HeSo_CoSo, value); }
        }

        public HeSoDayOnline(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
