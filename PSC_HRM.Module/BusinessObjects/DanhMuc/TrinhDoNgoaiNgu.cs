using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenTrinhDoNgoaiNgu")]
    [ModelDefault("Caption", "Trình độ ngoại ngữ")]
    public class TrinhDoNgoaiNgu : TruongBaseObject
    {
        private string _MaQuanLy;
        private string _TenTrinhDoNgoaiNgu;

        public TrinhDoNgoaiNgu(Session session) : base(session) { }

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }
        [ModelDefault("Caption", "Tên trình độ ngoại ngữ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTrinhDoNgoaiNgu
        {
            get
            {
                return _TenTrinhDoNgoaiNgu;
            }
            set
            {
                SetPropertyValue("TenTrinhDoNgoaiNgu", ref _TenTrinhDoNgoaiNgu, value);
            }
        }

        private decimal _CapDo;
        [ModelDefault("Caption", "Cấp độ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal CapDo
        {
            get
            {
                return _CapDo;
            }
            set
            {
                SetPropertyValue("CapDo", ref _CapDo, value);
            }
        }
    }

}
