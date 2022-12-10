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
using PSC_HRM.Module.BaoMat;


namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{

    [ModelDefault("Caption", "Hệ số TNTH")]
    [DefaultProperty("Caption")]
    public class HeSoTNTH : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoTNTH")]
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
        private decimal _HeSo_TNTH;
        private BoPhan _BoPhan;
        private bool _ApDungMonCNTT;

        [ModelDefault("Caption", "Ap dụng môn CNTT")]
        public bool ApDungMonCNTT
        {
            get { return _ApDungMonCNTT; }
            set { SetPropertyValue("ApDungMonCNTT", ref _ApDungMonCNTT, value); }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSo_TNTH", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_TNTH
        {
            get { return _HeSo_TNTH; }
            set { SetPropertyValue("HeSoTNTH", ref _HeSo_TNTH, value); }
        }

        [ModelDefault("Caption", "Khoa/Đơn vị")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        public HeSoTNTH(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
