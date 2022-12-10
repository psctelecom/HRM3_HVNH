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
using DevExpress.Data.Filtering;


namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Đợt tính PMS")]
    [DefaultProperty("TenKy")]
    public class DotTinhPMS : BaseObject
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private int _Dot;

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
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
            }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public HocKy HocKy
        {
            get
            {
                return _HocKy;
            }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
                if (!IsLoading && value != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("HocKy = ?", HocKy.Oid);
                    XPCollection<DotTinhPMS> listKyTinh = new XPCollection<DotTinhPMS>(Session, filter);
                    if (listKyTinh == null)
                        Dot = 1;
                    else
                        Dot = listKyTinh.Count + 1;
                }
            }
        }

        [ModelDefault("Caption", "Đợt")]
        [ModelDefault("AllowEdit", "False")]
        public int Dot
        {
            get { return _Dot; }
            set
            {
                SetPropertyValue("Dot", ref _Dot, value);
            }
        }

        [ModelDefault("Caption", "Tên đợt tính")]
        public string TenKy
        {
            get
            {
                return String.Format("Đợt {0} {1}", Dot.ToString(), HocKy != null ? HocKy.TenHocKy : "");
            }
        }
        public DotTinhPMS(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);            
        }
    }
}