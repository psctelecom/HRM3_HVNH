using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenXepLoaiTotNghiep")]
    [ModelDefault("Caption", "Xếp loại tốt nghiệp")]
    [RuleCombinationOfPropertiesIsUnique("XepLoaiTotNghiep", DefaultContexts.Save, "MaQuanLy;TenXepLoaiTotNghiep")]
    public class XepLoaiTotNghiep : BaseObject
    {
        private string _MaQuanLy;
        private string _TenXepLoaiTotNghiep;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên xếp loại tốt nghiệp")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenXepLoaiTotNghiep
        {
            get
            {
                return _TenXepLoaiTotNghiep;
            }
            set
            {
                SetPropertyValue("TenXepLoaiTotNghiep", ref _TenXepLoaiTotNghiep, value);
            }
        }

        public XepLoaiTotNghiep(Session session) : base(session) { }
    }

}
