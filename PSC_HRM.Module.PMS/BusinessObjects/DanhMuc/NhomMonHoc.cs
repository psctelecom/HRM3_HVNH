using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ImageName("BO_ChuyenNgach")]
    [DefaultProperty("TenNhomMonHoc")]
    [ModelDefault("Caption", "Nhóm môn học")]
    public class NhomMonHoc : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNhomMonHoc;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Nhóm môn học")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNhomMonHoc
        {
            get { return _TenNhomMonHoc; }
            set { SetPropertyValue("TenNhomMonHoc", ref _TenNhomMonHoc, value); }
        }
        public NhomMonHoc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}