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

    [ModelDefault("Caption", "Quản lý đào tạo (Chính quy)")]
    [DefaultProperty("Caption")]
    [Appearance("AnDuLieu", TargetItems = "HocKy; NamHoc; KyTinhPMS", Enabled = false, Criteria = "AnDuLieu")]
    [Appearance("KhoiLuongGiangDay_Khoa", TargetItems = "*", Enabled = false, Criteria = "BangChotThuLao is not null")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "NamHoc;HocKy;KyTinhPMS", "Khối lượng giảng dạy đã tồn tại")]
    [Appearance("", TargetItems = "HocKy", Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'QNU'")]

    [Appearance("Hide_ListKhoaLuanTotNghiep", TargetItems = "ListKhoaLuanTotNghiep", Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'NHH' OR ThongTinTruong.TenVietTat = 'DNU'")]
   
    [Appearance("Khoa", TargetItems = "*", Enabled = false, Criteria = "BangChotThuLao is not null")]
    [Appearance("Hide_HVNH", TargetItems = "KyTinhPMS;BacDaoTao"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'NHH'")]
    [Appearance("Hide_DNU", TargetItems = "KyTinhPMS;HocKy"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'DNU'")]

    [Appearance("Hide_ListChiTietKhoiLuongGiangDay_Moi", TargetItems = "ListChiTietKhoiLuongGiangDay_Moi"
                                         , Visibility = ViewItemVisibility.Hide, Criteria = "SuDungListMoi = 0")]

    [Appearance("Hide_ListChiTietKhoiLuongGiangDay", TargetItems = "ListChiTietKhoiLuongGiangDay"
                                         , Visibility = ViewItemVisibility.Hide, Criteria = "SuDungListMoi = 1")]
    [Appearance("Hide_UFM", TargetItems = "KyTinhPMS;HocKy"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'UFM'")]
    [Appearance("Hide_HUFLIT", TargetItems = "KyTinhPMS;ListChiTietKhoiLuongGiangDay;ListKhoaLuanTotNghiep"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'HUFLIT'")]
    public class KhoiLuongGiangDay : BaseObject
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private KyTinhPMS _KyTinhPMS;
        private bool _Khoa;
        private BangChotThuLao _BangChotThuLao;
        private BacDaoTao _BacDaoTao;
        private bool _SuDungListMoi;
        private bool _AnDuLieu;


        [ModelDefault("Caption", "ẩn dữ liệu")]
        [Browsable(false)]
        [ImmediatePostData]
        public bool AnDuLieu
        {
            get { return _AnDuLieu; }
            set { SetPropertyValue("AnDuLieu", ref _AnDuLieu, value); }
        }

        [ModelDefault("Caption", "Bảng chốt")]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public BangChotThuLao BangChotThuLao
        {
            get { return _BangChotThuLao; }
            set
            {
                SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value);
                if (BangChotThuLao != null)
                    Khoa = true;
                else
                    Khoa = false;
            }
        }


        [ModelDefault("Caption", "Trường")]
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
                    updateKyPMS();
                    HocKy = null;
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
        [ModelDefault("Caption", "Kỳ tính PMS")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("KyTinhPMSList")]
        [VisibleInListView(false)]
        public KyTinhPMS KyTinhPMS
        {
            get { return _KyTinhPMS; }
            set { SetPropertyValue("KyTinhPMS", ref _KyTinhPMS, value); }
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
            KyTinhPMSList = new XPCollection<KyTinhPMS>(Session, CriteriaOperator.Parse("NamHoc =?", NamHoc.Oid));
            OnChanged("KyTinhPMSList");
        }
        [ModelDefault("Caption", "Bậc đào tạo")]
        [VisibleInListView(false)]
        [DataSourceProperty("listBacDaoTao")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        //[NonPersistent]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }

        [ModelDefault("Caption", "Hide")]
        [Browsable(false)]
        public bool SuDungListMoi
        {
            get { return _SuDungListMoi; }
            set { SetPropertyValue("SuDungListMoi", ref _SuDungListMoi, value); }
        }
        [Aggregated]
        [Association("KhoiLuongGiangDay-ListChiTietKhoiLuongGiangDay")]
        [ModelDefault("Caption", "Đào tạo ĐH-CĐ")]
        public XPCollection<ChiTietKhoiLuongGiangDay> ListChiTietKhoiLuongGiangDay
        {
            get
            {
                return GetCollection<ChiTietKhoiLuongGiangDay>("ListChiTietKhoiLuongGiangDay");
            }
        }
        [Aggregated]
        [Association("KhoiLuongGiangDay-ListChiTietKhoiLuongGiangDay_Moi")]
        [ModelDefault("Caption", "Đào tạo ĐH-CĐ (Mới)")]
        public XPCollection<ChiTietKhoiLuongGiangDay_Moi> ListChiTietKhoiLuongGiangDay_Moi
        {
            get
            {
                return GetCollection<ChiTietKhoiLuongGiangDay_Moi>("ListChiTietKhoiLuongGiangDay_Moi");
            }
        }
        [Aggregated]
        [Association("KhoiLuongGiangDay-ListKhoaLuanTotNghiep")]
        [ModelDefault("Caption", "Khóa luận tốt nghiệp")]
        public XPCollection<KhoaLuanTotNghiep> ListKhoaLuanTotNghiep
        {
            get
            {
                return GetCollection<KhoaLuanTotNghiep>("ListKhoaLuanTotNghiep");
            }
        }

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                if (TruongConfig.MaTruong == "HVNH" || TruongConfig.MaTruong == "HUFLIT")
                    return String.Format(" {0}{1}{2}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.TenHocKy : "");
                else
                    return String.Format(" {0} {1} {2} {3}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.TenHocKy : "", KyTinhPMS != null ? " - Đợt " + KyTinhPMS.Dot.ToString() : "");
            }
        }
        public KhoiLuongGiangDay(Session session) : base(session) { }
      
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            if (TruongConfig.MaTruong == "VHU" || TruongConfig.MaTruong == "UFM" || TruongConfig.MaTruong == "HUFLIT")
            {
                SuDungListMoi = true;
            }
        }

        protected override void OnSaving()
        {
            if (TruongConfig.MaTruong == "VHU")
            {
                AnDuLieu = true;
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Hoc kỳ List")]
        public XPCollection<HocKy> listHocKy
        {
            get;
            set;
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

        [Browsable(false)]
        [ModelDefault("Caption", " Danh sách Bậc đào tạo")]
        public XPCollection<BacDaoTao> listBacDaoTao
        {
            get;
            set;
        }
        public void LoadBacDaoTao()
        {

            listBacDaoTao = new XPCollection<DanhMuc.BacDaoTao>(Session, false);
            if (HamDungChung.CheckAdministrator())
            {
                XPCollection<BacDaoTao> listBDT = new XPCollection<BacDaoTao>(Session);
                if(listBDT!=null)
                {
                    foreach (BacDaoTao item in listBDT)
                    {
                        listBacDaoTao.Add(item);
                    }
                }
            }
            else
            {
                CriteriaOperator filter = CriteriaOperator.Parse("NguoiSuDung = ?", HamDungChung.CurrentUser().Oid);
                XPCollection<NguoiSuDung_TheoBacDaoTao> dsBacDaoTao = new XPCollection<NguoiSuDung_TheoBacDaoTao>(Session, filter);
                if (dsBacDaoTao != null)
                {
                    BacDaoTao bdt = null;
                    foreach (var item in dsBacDaoTao)
                    {
                        bdt = Session.FindObject<BacDaoTao>(CriteriaOperator.Parse("Oid =?", item.BacDaoTao.Oid));
                        if (bdt != null)
                            listBacDaoTao.Add(bdt);
                    }
                }
            }
            OnChanged("listBacDaoTao");
        }
    }
}
