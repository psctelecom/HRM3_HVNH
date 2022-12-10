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
using PSC_HRM.Module.BaoMat;


namespace PSC_HRM.Module.PMS.GioChuan
{

    [ModelDefault("Caption", "Định mức nghiên cứu khoa học")]
    [DefaultProperty("BoPhan")]
    public class DinhMuc_NghienCuuKhoaHoc : BaseObject
    {
        private BoPhan _BoPhan;
        private decimal _GioDinhMuc;
        private QuanLyGioChuan _QuanLyGioChuan;

        [ModelDefault("Caption", "Quản lý giờ chuẩn")]
        [Association("QuanLyGioChuan-ListDinhMuc_NghienCuuKhoaHoc")]
        [Browsable(false)]
        public QuanLyGioChuan QuanLyGioChuan
        {
            get
            {
                return _QuanLyGioChuan;
            }
            set
            {
                SetPropertyValue("QuanLyGioChuan", ref _QuanLyGioChuan, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption", "Giờ định mức")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal GioDinhMuc
        {
            get { return _GioDinhMuc; }
            set { SetPropertyValue("GioDinhMuc", ref _GioDinhMuc, value); }
        }
        public DinhMuc_NghienCuuKhoaHoc(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
