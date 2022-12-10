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
using PSC_HRM.Module.PMS.CauHinh.HeSo;

namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Đơn vị đào tạo thường xuyên")]
    [DefaultProperty("TenDonVi")]
    public class DonViDaoTaoThuongXuyen : BaseObject
    {
        private string _MaQuanLy;
        private string _TenDonVi;
        private HeSoCoSo _HeSoCoSo;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên đơn vị đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenDonVi
        {
            get { return _TenDonVi; }
            set { SetPropertyValue("TenDonVi", ref _TenDonVi, value); }
        }

        [ModelDefault("Caption", "Hệ số cơ sở")]
        [RuleRequiredField(DefaultContexts.Save)]
        public HeSoCoSo HeSoCoSo
        {
            get { return _HeSoCoSo; }
            set { SetPropertyValue("HeSoCoSo", ref _HeSoCoSo, value); }
        }
        public DonViDaoTaoThuongXuyen(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
