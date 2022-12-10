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
    [DefaultProperty("TenTrinhDoTinHoc")]
    [ModelDefault("Caption", "Trình độ tin học")]
    public class TrinhDoTinHoc : TruongBaseObject
    {
        public TrinhDoTinHoc(Session session) : base(session) { }
        private string _MaQuanLy;

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
        private string _TenTrinhDoTinHoc;
        [ModelDefault("Caption", "Tên trình độ tin học")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTrinhDoTinHoc
        {
            get
            {
                return _TenTrinhDoTinHoc;
            }
            set
            {
                SetPropertyValue("TenTrinhDoTinHoc", ref _TenTrinhDoTinHoc, value);
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
