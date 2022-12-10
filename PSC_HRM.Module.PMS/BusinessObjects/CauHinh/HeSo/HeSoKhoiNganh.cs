using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.PMS.CauHinh.HeSo;

namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{
    [DefaultClassOptions]
    [DefaultProperty("TenKhoiNganh")]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Hệ số khối ngành")]
    public class HeSoKhoiNganh : BaseObject
    {
        //private QuanLyHeSo _QuanLyHeSo;
        private string _TenKhoiNganh;
        private decimal _HeSo;
        private string _BoPhan;
        private string _GhiChu;

        //[ModelDefault("Caption", "Quản lý hệ số")]
        //[Browsable(false)]
        //[RuleRequiredField("", DefaultContexts.Save)]
        //[Association("QuanLyHeSo-ListHeSoKhoiNganh")]
        //public QuanLyHeSo QuanLyHeSo
        //{
        //    get
        //    {
        //        return _QuanLyHeSo;
        //    }
        //    set
        //    {
        //        SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
        //    }
        //}

        [ModelDefault("Caption", "Tên khối ngành")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string TenKhoiNganh
        {
            get
            {
                return _TenKhoiNganh;
            }
            set
            {
                SetPropertyValue("TenKhoiNganh", ref _TenKhoiNganh, value);
            }
        }

        [ModelDefault("Caption", "Hệ số")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal HeSo
        {
            get
            {
                return _HeSo;
            }
            set
            {
                SetPropertyValue("HeSo", ref _HeSo, value);
            }
        }

        [ModelDefault("Caption", "Bộ phận")]
        [Size(-1)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public HeSoKhoiNganh(Session session) : base(session) { }
    }

}
