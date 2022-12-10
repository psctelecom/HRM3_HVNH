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
    [DefaultProperty("TenDonViTinh")]
    [ModelDefault("Caption", "Đơn vị tính")]
    public class DonViTinh : BaseObject
    {
        private string _MaQuanLy;
        private string _TenDonViTinh;
        private string _KyHieu;
        
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

        [ModelDefault("Caption", "Tên đơn vị tính")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenDonViTinh
        {
            get
            {
                return _TenDonViTinh;
            }
            set
            {
                SetPropertyValue("TenDonViTinh", ref _TenDonViTinh, value);
            }
        }

        [ModelDefault("Caption", "Ký hiệu")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string KyHieu
        {
            get
            {
                return _KyHieu;
            }
            set
            {
                SetPropertyValue("KyHieu", ref _KyHieu, value);
            }
        }

        public DonViTinh(Session session) : base(session) { }
    }

}
