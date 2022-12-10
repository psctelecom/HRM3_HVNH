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
using PSC_HRM.Module.PMS.DanhMuc;


namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{

    [ModelDefault("Caption", "Hệ số học phần")]
    [DefaultProperty("Caption")]
    public class HeSoMonHocBoSung : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        private DanhMucTinhChatHocPhan _DanhMucTinhChatHocPhan;
        private decimal _HeSo_TinhChat;

        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoMonHocBoSung")]
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
        [ModelDefault("Caption", "Tính chất học phần")]
        public DanhMucTinhChatHocPhan DanhMucTinhChatHocPhan
        {
            get { return _DanhMucTinhChatHocPhan; }
            set { SetPropertyValue("DanhMucTinhChatHocPhan", ref _DanhMucTinhChatHocPhan, value); }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSo_HocPhan_1", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_TinhChat
        {
            get { return _HeSo_TinhChat; }
            set { SetPropertyValue("HeSo_TinhChat", ref _HeSo_TinhChat, value); }
        }

        public HeSoMonHocBoSung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
