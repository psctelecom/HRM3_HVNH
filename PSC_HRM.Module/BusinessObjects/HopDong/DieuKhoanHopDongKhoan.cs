using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.HopDong
{
    [ImageName("BO_HopDong")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Điều khoản hợp đồng khoán")]
    public class DieuKhoanHopDongKhoan : BaseObject
    {
        private decimal _LuongKhoan;

        [ModelDefault("Caption", "Lương khoán")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal LuongKhoan
        {
            get
            {
                return _LuongKhoan;
            }
            set
            {
                SetPropertyValue("LuongKhoan", ref _LuongKhoan, value);
            }
        }

        [NonPersistent]
        [Browsable(false)]
        public string Caption
        {
            get
            {
                return ObjectFormatter.Format("Lương khoán: {LuongKhoan:N0}", this, EmptyEntriesMode.RemoveDelimeterWhenEntryIsEmpty);
            }
        }

        public DieuKhoanHopDongKhoan(Session session) : base(session) { }
    }

}
