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

    [ModelDefault("Caption", "Hệ số lương")]
    [DefaultProperty("Caption")]
    public class HeSoLuong : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoLuong")]
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
        //private NamHoc _NamHoc;
        private decimal _HeSo_Luong;
        private decimal _TuKhoan;
        private decimal _DenKhoan;

        //[ModelDefault("Caption", "Năm học")]
        //[DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        //public NamHoc NamHoc
        //{
        //    get { return _NamHoc; }
        //    set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        //}
        [ModelDefault("Caption", "Từ khoản")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TuKhoan
        {
            get { return _TuKhoan; }
            set { SetPropertyValue("TuKhoan", ref _TuKhoan, value); }
        }


        [ModelDefault("Caption", "Đến khoản")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DenKhoan
        {
            get { return _DenKhoan; }
            set { SetPropertyValue("DenKhoan", ref _DenKhoan, value); }
        }


        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSoLuong", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_Luong
        {
            get { return _HeSo_Luong; }
            set { SetPropertyValue("HeSo_Luong", ref _HeSo_Luong, value); }
        }


        public HeSoLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
