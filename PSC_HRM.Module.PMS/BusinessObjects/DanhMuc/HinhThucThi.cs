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
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Hình thức thi")]
    [DefaultProperty("TenHinhThucThi")]
    [Appearance("HinhThucThi_Hile", TargetItems = "HeSo", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong <> 'HVNH'")]
    public class HinhThucThi : BaseObject
    {
        private string _MaQuanLy;
        private string _TenHinhThucThi;
        private decimal _HeSo;
        private string _MaTruong;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên hình thức thi")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHinhThucThi
        {
            get { return _TenHinhThucThi; }
            set { SetPropertyValue("TenHinhThucThi", ref _TenHinhThucThi, value); }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo
        {
            get { return _HeSo; }
            set { SetPropertyValue("HeSo", ref _HeSo, value); }
        }

        [ModelDefault("Caption", "MaTruong")]
        [NonPersistent]
        [Browsable(false)]
        public string MaTruong
        {
            get { return _MaTruong; }
            set { SetPropertyValue("MaTruong", ref _MaTruong, value); }
        }

        public HinhThucThi(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        protected override void OnLoaded()
        {
            MaTruong = TruongConfig.MaTruong;
        }
    }

}
