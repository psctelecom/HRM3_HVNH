using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Quản lý cố vấn học tập")]
    [DefaultProperty("Caption")]
    public class QuanLyCoVanHocTap : BaseObject
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        [ModelDefault("Caption", "Trường")]
        [VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        [VisibleInListView(false)]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
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
        [Browsable(false)]
        [ModelDefault("Caption", "Hoc kỳ List")]
        public XPCollection<HocKy> listHocKy
        {
            get;
            set;
        }
        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format("{0} - Năm học  {1} {2}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.TenHocKy : "");      
            }
        }

        //

        [Aggregated]
        [Association("QuanLyCVHT-ListCoVanHocTap")]
        [ModelDefault("Caption", "Chi tiết cố vấn học tập")]
        public XPCollection<CoVanHocTap> ListCoVanHocTap
        {
            get
            {
                return GetCollection<CoVanHocTap>("ListCoVanHocTap");
            }
        }

        public QuanLyCoVanHocTap(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
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
    }
}
