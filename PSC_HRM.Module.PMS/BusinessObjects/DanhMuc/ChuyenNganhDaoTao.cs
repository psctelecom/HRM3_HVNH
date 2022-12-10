using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Chuyên ngành đào tạo")]
    [DefaultProperty("TenChuyenNganh")]
    public class ChuyenNganhDaoTao : BaseObject
    {
        private string _MaQuanLy;
        private string _TenChuyenNganh;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Chuyên ngành đạo tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChuyenNganh
        {
            get { return _TenChuyenNganh; }
            set
            {
                SetPropertyValue("TenChuyenNganh", ref _TenChuyenNganh, value);
                if (!IsLoading)
                    if (MaQuanLy == "" && TenChuyenNganh != "")
                        MaQuanLy = HamDungChung.BoDauTiengViet(TenChuyenNganh);
            }
        }
        public ChuyenNganhDaoTao(Session session) : base(session) { }
       

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}