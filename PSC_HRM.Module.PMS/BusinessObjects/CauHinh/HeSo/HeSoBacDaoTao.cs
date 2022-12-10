using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{
    [DefaultClassOptions]
    [DefaultProperty("BacDaoTao")]
    [ModelDefault("Caption", "Hệ số bậc đào tạo")]
    [Appearance("Hide_HVNH", TargetItems = "HeDaoTao", Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyHeSo.ThongTinTruong.TenVietTat != 'NHH'")]
    public class HeSoBacDaoTao : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;
        private decimal _HeSo_BacDaoTao;
        private bool _TinhVaoGioChuan;

        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoBacDaoTao")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BacDaoTao BacDaoTao
        {
            get
            {
                return _BacDaoTao;
            }
            set
            {
                SetPropertyValue("BacDaoTao", ref _BacDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        public HeDaoTao HeDaoTao
        {
            get
            {
                return _HeDaoTao;
            }
            set
            {
                SetPropertyValue("HeDaoTao", ref _HeDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Hệ số")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_BacDaoTao
        {
            get
            {
                return _HeSo_BacDaoTao;
            }
            set
            {
                SetPropertyValue("HeSo_BacDaoTao", ref _HeSo_BacDaoTao, value);
            }
        }

        [ModelDefault("Caption", "TinhVaoGioChuan")]
        [Browsable(false)]
        public bool TinhVaoGioChuan
        {
            get
            {
                return _TinhVaoGioChuan;
            }
            set
            {
                SetPropertyValue("TinhVaoGioChuan", ref _TinhVaoGioChuan, value);
            }
        }

        public HeSoBacDaoTao(Session session) : base(session) { }
    }

}
