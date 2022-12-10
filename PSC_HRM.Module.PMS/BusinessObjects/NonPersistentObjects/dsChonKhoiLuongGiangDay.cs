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
using PSC_HRM.Module.PMS.NghiepVu;

namespace PSC_HRM.Module.PMS.NonPersistent
{
    [NonPersistent]
    [ModelDefault("Caption", "DS chi tiết khối lượng")]
    [DefaultProperty("Caption")]
    public class dsChonKhoiLuongGiangDay : BaseObject
    {

        private bool _Chon;
        private ChiTietKhoiLuongGiangDay _ChiTietKhoiLuongGiangDay;

        [ModelDefault("Caption", "Chọn")]    
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }

        [ModelDefault("Caption", "Khối lượng giảng dạy")]
        public ChiTietKhoiLuongGiangDay ChiTietKhoiLuongGiangDay
        {
            get { return _ChiTietKhoiLuongGiangDay; }
            set { SetPropertyValue("ChiTietKhoiLuongGiangDay", ref _ChiTietKhoiLuongGiangDay, value); }
        }

        public dsChonKhoiLuongGiangDay(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
