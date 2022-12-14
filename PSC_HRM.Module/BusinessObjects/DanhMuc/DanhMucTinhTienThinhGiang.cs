using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_BenhVien")]
    [ModelDefault("Caption", "Danh mục tính tiền thỉnh giảng")]
    [DefaultProperty("Ten")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "HocHam,HocVi")]
    public class DanhMucTinhTienThinhGiang : BaseObject
    {
        private HocHam _HocHam;
        private HocVi _HocVi;
        private decimal _DonGia;
        private decimal _DonGiaCLC;

        [ModelDefault("Caption", "Học hàm")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public HocHam HocHam
        {
            get
            {
                return _HocHam;
            }
            set
            {
                SetPropertyValue("HocHam", ref _HocHam, value);
            }
        }
        [ModelDefault("Caption", "Học vị")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public HocVi HocVi
        {
            get
            {
                return _HocVi;
            }
            set
            {
                SetPropertyValue("HocVi", ref _HocVi, value);
            }
        }
        [ModelDefault("Caption", "Đơn giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal DonGia
        {
            get
            {
                return _DonGia;
            }
            set
            {
                SetPropertyValue("DonGia", ref _DonGia, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá CLC")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal DonGiaCLC
        {
            get
            {
                return _DonGiaCLC;
            }
            set
            {
                SetPropertyValue("DonGiaCLC", ref _DonGiaCLC, value);
            }
        }

        public DanhMucTinhTienThinhGiang(Session session) : base(session) { }
    }

}
