using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.CauHinh
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình thông tin lương")]

    public class CauHinhThongTinLuong : BaseObject
    {
        // Fields...
        private string _CongThucTinhHSPCVuotKhung;
        private string _CongThucTinhHSPCUuDai;
        private string _CongThucTinhHSPCThamNien;
        private string _CongThucTinhHSPCThamNienHanhChinh;
        private string _CongThucTinhHSPCKhoiHanhChinh;
        private string _CongThucTinhHSPCKhac;
        private string _CongThucTinhHSPCDocHai;
        private string _CongThucTinhHSPCTrachNhiem;

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính HSPC vượt khung")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaEditor")]
        public string CongThucTinhHSPCVuotKhung
        {
            get
            {
                return _CongThucTinhHSPCVuotKhung;
            }
            set
            {
                SetPropertyValue("CongThucTinhHSPCVuotKhung", ref _CongThucTinhHSPCVuotKhung, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính HSPC ưu đãi")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaEditor")]
        public string CongThucTinhHSPCUuDai
        {
            get
            {
                return _CongThucTinhHSPCUuDai;
            }
            set
            {
                SetPropertyValue("CongThucTinhHSPCUuDai", ref _CongThucTinhHSPCUuDai, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính HSPC khác")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaEditor")]
        public string CongThucTinhHSPCKhac
        {
            get
            {
                return _CongThucTinhHSPCKhac;
            }
            set
            {
                SetPropertyValue("CongThucTinhHSPCKhac", ref _CongThucTinhHSPCKhac, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính HSPC độc hại")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaEditor")]
        public string CongThucTinhHSPCDocHai
        {
            get
            {
                return _CongThucTinhHSPCDocHai;
            }
            set
            {
                SetPropertyValue("CongThucTinhHSPCDocHai", ref _CongThucTinhHSPCDocHai, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính HSPC thâm niên")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaEditor")]
        public string CongThucTinhHSPCThamNien
        {
            get
            {
                return _CongThucTinhHSPCThamNien;
            }
            set
            {
                SetPropertyValue("CongThucTinhHSPCThamNien", ref _CongThucTinhHSPCThamNien, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính HSPC thâm niên hành chính")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaEditor")]
        public string CongThucTinhHSPCThamNienHanhChinh
        {
            get
            {
                return _CongThucTinhHSPCThamNienHanhChinh;
            }
            set
            {
                SetPropertyValue("CongThucTinhHSPCThamNienHanhChinh", ref _CongThucTinhHSPCThamNienHanhChinh, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính HSPC khối hành chính")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaEditor")]
        public string CongThucTinhHSPCKhoiHanhChinh
        {
            get
            {
                return _CongThucTinhHSPCKhoiHanhChinh;
            }
            set
            {
                SetPropertyValue("CongThucTinhHSPCKhoiHanhChinh", ref _CongThucTinhHSPCKhoiHanhChinh, value);
            }
        }   

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính HSPC trách nhiệm")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaEditor")]
        public string CongThucTinhHSPCTrachNhiem
        {
            get
            {
                return _CongThucTinhHSPCTrachNhiem;
            }
            set
            {
                SetPropertyValue("CongThucTinhHSPCTrachNhiem", ref _CongThucTinhHSPCTrachNhiem, value);
            }
        }
        public CauHinhThongTinLuong(Session session) : base(session) { }
    }

}
