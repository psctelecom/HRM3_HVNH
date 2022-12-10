using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.PMS.Enum;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.PMS.NghiepVu;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.PMS.NonPersistentObjects.TaiChinh
{
    [NonPersistent]
    [ModelDefault("Caption","Thanh toán đợt tổng kết")]
    public class dsThanhToanPhuLuc01 : BaseObject
    {

        private bool _Chon;
        private Guid _QuanLyKhaoThi;
        private string _DienGiai;


        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }

        [ModelDefault("Caption", "Quản lý khảo thí")]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        public Guid QuanLyKhaoThi
        {
            get { return _QuanLyKhaoThi; }
            set { SetPropertyValue("QuanLyKhaoThi", ref _QuanLyKhaoThi, value); }
        }

        [ModelDefault("Caption", "Diễn giải")]
        [ModelDefault("AllowEdit", "False")]
        public string DienGiai
        {
            get { return _DienGiai; }
            set { SetPropertyValue("DienGiai", ref _DienGiai, value); }
        }

        public dsThanhToanPhuLuc01(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }     
    }

}