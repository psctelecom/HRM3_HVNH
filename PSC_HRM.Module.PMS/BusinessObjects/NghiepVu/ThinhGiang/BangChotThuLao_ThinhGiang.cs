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
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.PMS.NghiepVu
{

    [ModelDefault("Caption", "Bảng chốt thù lao(thỉnh giảng)")]
    [DefaultProperty("Caption")]
    [Appearance("Khoa", TargetItems = "*", Enabled = false, Criteria = "Khoa = 1")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "ThongTinTruong;NamHoc;HocKy;DotTinh", "Bảng chốt thông tin giảng dạy đã tồn tại")]
    public class BangChotThuLao_ThinhGiang : BaseObject
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private ThongTinTruong _ThongTinTruong;
        private bool _Khoa;
        private bool _DaTinhThuLao;
        private DateTime _NgayChot;
        private DotTinhPMS _DotTinh;


        [ModelDefault("Caption", "Trường")]
        [VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit","False")]
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
                    HocKy = null;
                    DotTinh = null;
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
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
                if (value != null)
                {
                    UpdateDotTinh();
                    DotTinh = null;
                }
            }
        }

        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }
        [ModelDefault("Caption", "Đã tính thù lao")]
        public bool DaTinhThuLao
        {
            get { return _DaTinhThuLao; }
            set { SetPropertyValue("DaTinhThuLao", ref _DaTinhThuLao, value); }
        }

        [ModelDefault("Caption", "Đợt tính PMS")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("listDotTinhPMS")]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public DotTinhPMS DotTinh
        {
            get { return _DotTinh; }
            set { SetPropertyValue("DotTinh", ref _DotTinh, value); }
        }

        [ModelDefault("Caption", "Ngày chốt")]
        [ModelDefault("AllowEdit", "False")]
        public DateTime NgayChot
        {
            get { return _NgayChot; }
            set { SetPropertyValue("NgayChot", ref _NgayChot, value); }
        }

        [Aggregated]
        [Association("BangChotThuLao_ThinhGiang-ListThongTinBangChotThuLao")]
        [ModelDefault("Caption", "Chi tiết thù lao")]
        public XPCollection<ThongTinBangChotThuLao> ListThongTinBangChotThuLao
        {
            get
            {
                return GetCollection<ThongTinBangChotThuLao>("ListThongTinBangChotThuLao");
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Hoc kỳ List")]
        public XPCollection<HocKy> listHocKy
        {
            get;
            set;
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Đợt tính List")]
        public XPCollection<DotTinhPMS> listDotTinhPMS
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
                return String.Format("{0} - Năm học  {1} {2} {3}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.TenHocKy : "", DotTinh != null ? " - Đợt " + DotTinh.Dot.ToString() : "");      
            }
        }


        public BangChotThuLao_ThinhGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            NgayChot = DateTime.Now;
            listDotTinhPMS = new XPCollection<DotTinhPMS>(Session, false);
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

        public void UpdateDotTinh()
        {
            if (HocKy != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("HocKy=?", HocKy.Oid);
                if (listDotTinhPMS != null)
                {
                    listDotTinhPMS.Reload();

                    XPCollection<DotTinhPMS> DS_List = new XPCollection<DotTinhPMS>(Session, filter);
                    foreach (DotTinhPMS item in DS_List)
                    {
                        listDotTinhPMS.Add(item);
                    }
                }
            }
        }
        //protected override void OnDeleting()
        //{
        //    base.OnDeleting();           
        //}
        //protected override void OnDeleted()
        //{
        //    base.OnDeleted();
        //}
    }
}
