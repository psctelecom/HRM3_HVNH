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

    [ModelDefault("Caption", "Nhóm vai trò")]
    [DefaultProperty("TenNhom")]
    public class NhomVaiTro_KhaoThi : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNhom;

        [ModelDefault("Caption", "Mã quản lý")]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên Nhóm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNhom
        {
            get { return _TenNhom; }
            set { SetPropertyValue("TenNhom", ref _TenNhom, value); }
        }
        public NhomVaiTro_KhaoThi(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
