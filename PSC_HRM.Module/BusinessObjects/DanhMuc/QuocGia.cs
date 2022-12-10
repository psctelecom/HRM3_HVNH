using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_Country_v92")]
    [DefaultProperty("TenQuocGia")]
    [ModelDefault("Caption", "Quốc gia")]
    public class QuocGia : BaseObject
    {
        private string _MaQuanLy;
        private string _TenQuocGia;

        public QuocGia(Session session) : base(session) { }

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField("",DefaultContexts.Save)]
        [RuleUniqueValue("",DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên quốc gia")]
        [RuleRequiredField("",DefaultContexts.Save)]
        public string TenQuocGia
        {
            get
            {
                return _TenQuocGia;
            }
            set
            {
                SetPropertyValue("TenQuocGia", ref _TenQuocGia, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách tỉnh thành")]
        [Association("QuocGia-TinhThanhList")]
        public XPCollection<TinhThanh> TinhThanhList
        {
            get
            {
                return GetCollection<TinhThanh>("TinhThanhList");
            }
        }
 
    }

}
