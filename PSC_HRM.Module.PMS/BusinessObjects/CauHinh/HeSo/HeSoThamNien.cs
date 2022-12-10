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
using PSC_HRM.Module.BusinessObjects.HoSo;


namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{

    [ModelDefault("Caption", "Hệ số thâm niên")]
    [DefaultProperty("Caption")]
    public class HeSoThamNien : BaseObject
    {
        #region Key
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoThamNien")]
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
        #endregion

        private ThamNien _ThamNien;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private decimal _HeSo_ThamNien;


        [ModelDefault("Caption", "Thâm niên")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThamNien ThamNien
        {
            get { return _ThamNien; }
            set { SetPropertyValue("ThamNien", ref _ThamNien, value); }
        }
        [ModelDefault("Caption", "Trình độ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get { return _TrinhDoChuyenMon; }
            set { SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value); }
        }
        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSo_ThamNien", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_ThamNien
        {
            get { return _HeSo_ThamNien; }
            set { SetPropertyValue("HeSo_ThamNien", ref _HeSo_ThamNien, value); }
        }
        public HeSoThamNien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
