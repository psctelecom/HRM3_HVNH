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

    [ModelDefault("Caption", "Vai trò khảo thí")]
    [DefaultProperty("TenVaiTro")]
    public class VaiTro_KhaoThi : BaseObject
    {
        private string _MaQuanLy;
        private string _TenVaiTro;
        private decimal _DonGia;
        private NhomVaiTro_KhaoThi _Nhom;

        [ModelDefault("Caption", "Mã quản lý")]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên vai trò")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenVaiTro
        {
            get { return _TenVaiTro; }
            set { SetPropertyValue("TenVaiTro", ref _TenVaiTro, value); }
        }

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set { SetPropertyValue("DonGia", ref _DonGia, value); }
        }

        [ModelDefault("Caption", "Nhóm vai trò")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NhomVaiTro_KhaoThi Nhom
        {
            get { return _Nhom; }
            set { SetPropertyValue("Nhom", ref _Nhom, value); }
        }
        public VaiTro_KhaoThi(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
