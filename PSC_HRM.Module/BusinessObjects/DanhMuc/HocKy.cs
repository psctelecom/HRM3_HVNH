using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenHocKy")]
    [ModelDefault("Caption", "Học kỳ")]
    [RuleCombinationOfPropertiesIsUnique("HocKy.Unique", DefaultContexts.Save, "NamHoc;MaQuanLy")]
    public class HocKy : BaseObject
    {
        private NamHoc _NamHoc;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private string _MaQuanLy;
        private string _TenHocKy;
        private bool _HocKyHe;

        private DateTime _TuNgay_PMS;
        private DateTime _DenNgay_PMS;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
        [Association("NamHoc-ListHocKy")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading && value != null && string.IsNullOrEmpty(MaQuanLy))
                {
                    int count = value.ListHocKy.Count + 1;
                    MaQuanLy = String.Format("HK0{0}", count);
                    TenHocKy = String.Format("Học kỳ {0}", count);
                }
            }
        }

        [ModelDefault("AllowEdit", "False")]
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

        [ModelDefault("Caption", "Tên học kỳ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHocKy
        {
            get
            {
                return _TenHocKy;
            }
            set
            {
                SetPropertyValue("TenHocKy", ref _TenHocKy, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Học kỳ hè")]
        public bool HocKyHe
        {
            get
            {
                return _HocKyHe;
            }
            set
            {
                SetPropertyValue("HocKyHe", ref _HocKyHe, value);
            }
        }
        [ModelDefault("Caption", "Chỉnh sửa thù lao từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay_PMS
        {
            get
            {
                return _TuNgay_PMS;
            }
            set
            {
                SetPropertyValue("TuNgay_PMS", ref _TuNgay_PMS, value);
            }
        }

        [ModelDefault("Caption", "Chỉnh sửa thù lao từ ngày đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DenNgay_PMS
        {
            get
            {
                return _DenNgay_PMS;
            }
            set
            {
                SetPropertyValue("DenNgay_PMS", ref _DenNgay_PMS, value);
            }
        }
        public HocKy(Session session) : base(session) { }
    }

}
