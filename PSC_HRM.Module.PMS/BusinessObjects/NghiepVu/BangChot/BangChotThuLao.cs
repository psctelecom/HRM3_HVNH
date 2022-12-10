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

    [ModelDefault("Caption", "Bảng chốt thù lao")]
    [DefaultProperty("Caption")]
    [Appearance("Khoa", TargetItems = "*", Enabled = false, Criteria = "Khoa = 1")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "ThongTinTruong;NamHoc;HocKy;KyTinhPMS", "Bảng chốt thông tin giảng dạy đã tồn tại")]

    [Appearance("QNU_Hide", TargetItems = "HocKy;ListThongTinBangChot_Moi;ListThongTinBangChotThuLao", 
                                        Visibility = ViewItemVisibility.Hide, 
                                        Criteria = "ThongTinTruong.TenVietTat = 'QNU'")]

    [Appearance("UEL_Hide", TargetItems = "HocKy;ListThongTinBangChot_Moi;ListThongTinBangChotThuLao", 
                                        Visibility = ViewItemVisibility.Hide, 
                                        Criteria = "ThongTinTruong.TenVietTat = 'UEL'")]

    [Appearance("NHH_Hide", TargetItems = "KyTinhPMS;ListThongTinBangChot_Moi;ListThongTinBangChotThuLao", 
                                        Visibility = ViewItemVisibility.Hide, 
                                        Criteria = "ThongTinTruong.TenVietTat = 'NHH'")]

    [Appearance("DNU_Hide", TargetItems = "KyTinhPMS;HocKy;ListThongTinBangChot_Moi;ListThongTinBangChotThuLao", 
                                        Visibility = ViewItemVisibility.Hide, 
                                        Criteria = "ThongTinTruong.TenVietTat = 'DNU'")]

    [Appearance("VHU_Hide", TargetItems = "ListThongTinBangChot;KyTinhPMS;ListThongTinBangChotThuLao", 
                                        Visibility = ViewItemVisibility.Hide, 
                                        Criteria = "ThongTinTruong.TenVietTat = 'VHU'")]

    [Appearance("UFM_Hide", TargetItems = "ListThongTinBangChot;KyTinhPMS;HocKy;ListThongTinBangChotThuLao", 
                                        Visibility = ViewItemVisibility.Hide, 
                                        Criteria = "ThongTinTruong.TenVietTat = 'UFM'")]


    [Appearance("NEU_Hide", TargetItems = "ListThongTinBangChot;ListThongTinBangChot_Moi;KyTinhPMS",
                                        Visibility = ViewItemVisibility.Hide,
                                        Criteria = "ThongTinTruong.MaQuanLy = 'NEU'")]

    [Appearance("HUFLIT_Hide", TargetItems = "ListThongTinBangChot;ListThongTinBangChot_Moi;KyTinhPMS",
                                        Visibility = ViewItemVisibility.Hide,
                                        Criteria = "ThongTinTruong.TenVietTat = 'HUFLIT'")]
    [Appearance("AnDuLieu", TargetItems = "HocKy; NamHoc; KyTinhPMS", Enabled = false, Criteria = "AnDuLieu")]
    public class BangChotThuLao : BaseObject
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private ThongTinTruong _ThongTinTruong;
        private bool _Khoa;
        private bool _DaTinhThuLao;
        private KyTinhPMS _KyTinhPMS;
        private DateTime _NgayChot;
        private bool _AnDuLieu;


        [ModelDefault("Caption", "ẩn dữ liệu")]
        [Browsable(false)]
        [ImmediatePostData]
        public bool AnDuLieu
        {
            get { return _AnDuLieu; }
            set { SetPropertyValue("AnDuLieu", ref _AnDuLieu, value); }
        }

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
                    updateKyPMS();
                    HocKy = null;
                }
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Kỳ PMS List")]
        public XPCollection<KyTinhPMS> KyTinhPMSList
        {
            get;
            set;
        }
        void updateKyPMS()
        {
            if (NamHoc != null)
            {
                KyTinhPMSList = new XPCollection<KyTinhPMS>(Session, CriteriaOperator.Parse("NamHoc =?", NamHoc.Oid));
            }
            else
                KyTinhPMSList = new XPCollection<DanhMuc.KyTinhPMS>(Session, false);
            OnChanged("KyTinhPMSList");
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

        [ModelDefault("Caption", "Kỳ tính PMS")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("KyTinhPMSList")]
        [VisibleInListView(false)]
        public KyTinhPMS KyTinhPMS
        {
            get { return _KyTinhPMS; }
            set { SetPropertyValue("KyTinhPMS", ref _KyTinhPMS, value); }
        }

        [ModelDefault("Caption", "Ngày chốt")]
        [ModelDefault("AllowEdit", "False")]
        public DateTime NgayChot
        {
            get { return _NgayChot; }
            set { SetPropertyValue("NgayChot", ref _NgayChot, value); }
        }

        [Aggregated]
        [Association("BangChotThuLao-ListThongTinBangChot")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ThongTinBangChot> ListThongTinBangChot
        {
            get
            {
                return GetCollection<ThongTinBangChot>("ListThongTinBangChot");
            }
        }
        [Aggregated]
        [Association("BangChotThuLao-ListThongTinBangChot_Moi")]
        [ModelDefault("Caption", "Chi tiết (Mới)")]
        public XPCollection<ThongTinBangChot_Moi> ListThongTinBangChot_Moi
        {
            get
            {
                return GetCollection<ThongTinBangChot_Moi>("ListThongTinBangChot_Moi");
            }
        }
        [Aggregated]
        [Association("BangChotThuLao-ListThongTinBangChotThuLao")]
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

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format("{0} - Năm học  {1} {2} {3}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.TenHocKy : "", KyTinhPMS != null ? " - Đợt " + KyTinhPMS.Dot.ToString() : "");      
            }
        }


        public BangChotThuLao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            NgayChot = DateTime.Now;
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

        protected override void OnSaving()
        {
            if (TruongConfig.MaTruong == "VHU")
            {
                AnDuLieu = true;
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
