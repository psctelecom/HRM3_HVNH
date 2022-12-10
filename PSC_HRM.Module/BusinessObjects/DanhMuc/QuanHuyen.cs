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
    [ImageName("BO_List")]
    [DefaultProperty("TenQuanHuyen")]
    [ModelDefault("Caption", "Quận huyện")]
    public class QuanHuyen : BaseObject
    {
        public QuanHuyen(Session session) : base(session) { }

        private string _MaQuanLy;
        private string _TenQuanHuyen;
        private TinhThanh _TinhThanh;

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

        [ModelDefault("Caption", "Tên quận huyện")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenQuanHuyen
        {
            get
            {
                return _TenQuanHuyen;
            }
            set
            {
                SetPropertyValue("TenQuanHuyen", ref _TenQuanHuyen, value);
            }
        }

        [ModelDefault("Caption", "Tỉnh thành")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Association("TinhThanh-QuanHuyenList")]
        public TinhThanh TinhThanh
        {
            get
            {
                return _TinhThanh;
            }
            set
            {
                SetPropertyValue("TinhThanh", ref _TinhThanh, value);
            }
        }

        [ModelDefault("Caption", "Danh sách xã phường")]
        [Association("QuanHuyen-XaPhuongList")]
        public XPCollection<XaPhuong> XaPhuongList
        {
            get
            {
                return GetCollection<XaPhuong>("XaPhuongList");
            }
        }
    }

}
