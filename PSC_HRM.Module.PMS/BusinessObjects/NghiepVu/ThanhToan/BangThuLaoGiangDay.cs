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
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.NonPersistent;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoMat;


namespace PSC_HRM.Module.PMS.NghiepVu.ThanhToan
{

    [ModelDefault("Caption", "Bảng thù lao giảng dạy")]
    [DefaultProperty("Caption")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "ThongTinTruong;NamHoc;HocKy", "Khối lượng giảng dạy đã tồn tại")]
    public class BangThuLaoGiangDay : BaseObject
    {
        #region Khai báo
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        #endregion

        #region Sử dụng

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading && value != null)
                {
                    UpdateHocKy();
                }
            }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("listHocKy")]
        [VisibleInListView(false)]
        [ImmediatePostData]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }

        [Aggregated]
        [Association("BangThuLaoGiangDay-listChiTiet")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiTietThuLaoGiangDay> listChiTiet
        {
            get
            {
                return GetCollection<ChiTietThuLaoGiangDay>("listChiTiet");
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Hoc kỳ List")]
        public XPCollection<HocKy> listHocKy
        {
            get;
            set;
        }

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Caption")]
        public string Caption
        {
            get
            {
                return String.Format("Năm học {0} ", NamHoc != null ? NamHoc.TenNamHoc : "", HocKy != null ? "- Học kỳ " + HocKy.TenHocKy : "");
            }
        }
       

        public void UpdateHocKy()
        {
            CriteriaOperator filter = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
            XPCollection<HocKy> DS_HocKy = new XPCollection<HocKy>(Session, filter);
            if (listHocKy != null)
            {
                listHocKy.Reload();
            }
            else
            {
                listHocKy = new XPCollection<HocKy>(Session, false);
            }
            foreach (HocKy item in DS_HocKy)
            {
                listHocKy.Add(item);
            }
            OnChanged("listHocKy");
        }
        #endregion
        public BangThuLaoGiangDay(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
       
    }
}
