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
    [DefaultProperty("TenTrinhDoSuPham")]
    [ModelDefault("Caption", "Trình độ sư phạm")]
    public class TrinhDoSuPham : TruongBaseObject
    {
        private string _MaQuanLy;
        private string _TenTrinhDoSuPham;

        public TrinhDoSuPham(Session session) : base(session) { }

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
        [ModelDefault("Caption", "Tên trình độ sư phạm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTrinhDoSuPham
        {
            get
            {
                return _TenTrinhDoSuPham;
            }
            set
            {
                SetPropertyValue("TenTrinhDoSuPham", ref _TenTrinhDoSuPham, value);
            }
        }
      
    }

}
