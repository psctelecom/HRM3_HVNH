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


namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Học phần - Bồi dưỡng TX")]
    [DefaultProperty("TenHocPhan")]
    public class HocPhan_BoiDuongThuongXuyen : BaseObject
    {
        private string _MaQuanLy;
        private string _TenHocPhan;

        [ModelDefault("Caption", "Mã quản lý")]
        [Size(-1)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên học phần")]
        [Size(-1)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHocPhan
        {
            get { return _TenHocPhan; }
            set { SetPropertyValue("TenHocPhan", ref _TenHocPhan, value); }
        }
        public HocPhan_BoiDuongThuongXuyen(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
