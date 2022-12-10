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
    [DefaultProperty("TenPhanLoaiToChucDoan")]
    [ModelDefault("Caption", "Phân loại tổ chức Đoàn")]
    [RuleCombinationOfPropertiesIsUnique("PhanLoaiToChucDoan.Unique", DefaultContexts.Save, "MaQuanLy;TenPhanLoaiToChucDoan")]
    public class PhanLoaiToChucDoan : BaseObject
    {
        public PhanLoaiToChucDoan(Session session) : base(session) { }

        // Fields...
        private string _TenPhanLoaiToChucDoan;
        private string _MaQuanLy;

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

        [ModelDefault("Caption", "Tên phân loại tổ chức Đoàn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenPhanLoaiToChucDoan
        {
            get
            {
                return _TenPhanLoaiToChucDoan;
            }
            set
            {
                SetPropertyValue("TenPhanLoaiToChucDoan", ref _TenPhanLoaiToChucDoan, value);
            }
        }
    }

}
