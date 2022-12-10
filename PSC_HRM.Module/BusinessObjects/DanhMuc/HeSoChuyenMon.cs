using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [DefaultProperty("TrinhDoChuyenMon")]
    [ModelDefault("Caption", "Hệ số chuyên môn")]
    [RuleCombinationOfPropertiesIsUnique("HeSoChuyenMon.Unique", DefaultContexts.Save, "CoHocVi;CongViecHienNay;TrinhDoChuyenMon")]
    [Appearance("HeSoChuyenMon", TargetItems = "CongViecHienNay;TrinhDoChuyenMon", Enabled = false, Criteria = "!CoHocVi")]
    public class HeSoChuyenMon : BaseObject
    {
        // Fields...
        private CongViec _CongViecHienNay;
        private bool _CoHocVi;
        private decimal _HSPCChuyenMon;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;

        [ImmediatePostData]
        [ModelDefault("Caption", "Có học vị")]
        public bool CoHocVi
        {
            get
            {
                return _CoHocVi;
            }
            set
            {
                SetPropertyValue("CoHocVi", ref _CoHocVi, value);
            }
        }

        [ModelDefault("Caption", "Công việc hiện nay")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria="CoHocVi")]
        public CongViec CongViecHienNay
        {
            get
            {
                return _CongViecHienNay;
            }
            set
            {
                SetPropertyValue("CongViecHienNay", ref _CongViecHienNay, value);
            }
        }

        [ModelDefault("Caption", "Học vị")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria="CoHocVi")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "Hệ số chuyên môn")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal HSPCChuyenMon
        {
            get
            {
                return _HSPCChuyenMon;
            }
            set
            {
                SetPropertyValue("HSPCChuyenMon", ref _HSPCChuyenMon, value);
            }
        }

        public HeSoChuyenMon(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            CoHocVi = true;
        }
    }

}
