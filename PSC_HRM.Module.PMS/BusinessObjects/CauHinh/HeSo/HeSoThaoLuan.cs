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


namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{
    [ModelDefault("Caption", "Hệ số thảo luận")]
    //[ModelDefault("Caption", "Hệ số tín chỉ")]//Hệ số thảo luận không phải hệ số tín chỉ
    [DefaultProperty("Caption")]
    public class HeSoThaoLuan : BaseObject
    {
        #region Key
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoThaoLuan")]
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
        #endregion

        private int _TuKhoan;
        private int _DenKhoan;
        private decimal _HeSo_ThaoLuan;

        [ModelDefault("Caption", "Từ khoản")]
        public int TuKhoan
        {
            get { return _TuKhoan; }
            set { SetPropertyValue("TuKhoan", ref _TuKhoan, value); }
        }

        [ModelDefault("Caption", "Đến khoản")]
        public int DenKhoan
        {
            get { return _DenKhoan; }
            set { SetPropertyValue("DenKhoan", ref _DenKhoan, value); }
        }
        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSo_ThaoLuans", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_ThaoLuan
        {
            get { return _HeSo_ThaoLuan; }
            set { SetPropertyValue("HeSo_ThaoLuan", ref _HeSo_ThaoLuan, value); }
        }
        public HeSoThaoLuan(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            HeSo_ThaoLuan = 1;
            TuKhoan = 0;
            DenKhoan = 0;
        }
    }
}