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
    [DefaultProperty("TenPhanLoaiToChucDang")]
    [ModelDefault("Caption", "Phân loại tổ chức Đảng")]
    [RuleCombinationOfPropertiesIsUnique("PhanLoaiToChucDang.Unique", DefaultContexts.Save, "MaQuanLy;TenPhanLoaiToChucDang")]
    public class PhanLoaiToChucDang : BaseObject
    {
        public PhanLoaiToChucDang(Session session) : base(session) { }

        // Fields...
        private string _TenPhanLoaiToChucDang;
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

        [ModelDefault("Caption", "Tên phân loại tổ chức Đảng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenPhanLoaiToChucDang
        {
            get
            {
                return _TenPhanLoaiToChucDang;
            }
            set
            {
                SetPropertyValue("TenPhanLoaiToChucDang", ref _TenPhanLoaiToChucDang, value);
            }
        }
    }

}
