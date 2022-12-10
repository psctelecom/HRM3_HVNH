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
using System.Data.SqlClient;
using DevExpress.XtraEditors;


namespace PSC_HRM.Module.PMS.NghiepVu.ThanhToan
{

    [ModelDefault("Caption", "Quản lý hoạt động khác")]
    [DefaultProperty("Caption")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "ThongTinTruong;NamHoc;HocKy", "Khối lượng giảng dạy đã tồn tại")]
    [Appearance("Khoa", TargetItems = "*", Enabled = false, Criteria = "BangChotThuLao is not null")]
    [Appearance("AnDuLieu", TargetItems = "HocKy; NamHoc; KyTinhPMS", Enabled = false, Criteria = "AnDuLieu")]

    [Appearance("", TargetItems = "HocKy;ListChiTietKeKhaiHDK_VHU", Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'QNU'")]
    [Appearance("", TargetItems = "HocKy;KyTinhPMS;listChiTiet;ListChiTietKeKhaiHuongDanKhac;ListChamBaiCoiThi;ListThanhToanKLGD;ListChiTietKeKhaiHoatDongKhac", Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'VHU'")]

    [Appearance("", TargetItems = "ListThanhToanKLGD;ListChiTietKeKhaiHDK_VHU;HocKy;KyTinhPMS", Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'HUFLIT'")]

    [Appearance("", TargetItems = "listChiTiet;ListChiTietKeKhaiHDK_VHU;HocKy", Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'UEL'")]

    [Appearance("", TargetItems = "KyTinhPMS;ListChiTietKeKhaiHDK_VHU;ListChamBaiCoiThi;listChiTiet;ListChiTietKeKhaiHoatDongKhac;ListChamBaiCoiThi", Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'UFM'")]


    [Appearance("", TargetItems = "ListChiTietKeKhaiHoatDongKhac;ListChiTietKeKhaiHuongDanKhac", Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.MaQuanLy <> 'NEU'")]

    [Appearance("", TargetItems = "KyTinhPMS;listChiTiet;ListChamBaiCoiThi;ListThanhToanKLGD;ChiTietKeKhaiHuongDanKhac;ListChiTietKeKhaiHDK_VHU", Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.MaQuanLy = 'NEU'")]
    public class QuanLyHoatDongKhac : BaseObject
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private KyTinhPMS _KyTinhPMS;

        private bool _Khoa;
        private BangChotThuLao _BangChotThuLao;
        private bool _AnDuLieu;
        
        [ModelDefault("Caption", "Năm học")]
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
                    updateKyPMS();
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
        [VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("KyTinhPMSList")]
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
            if (NamHoc != null)
            {
                KyTinhPMSList = new XPCollection<KyTinhPMS>(Session, CriteriaOperator.Parse("NamHoc =?", NamHoc.Oid));
            }
            else
                KyTinhPMSList = new XPCollection<DanhMuc.KyTinhPMS>(Session, false);
            OnChanged("KyTinhPMSList");
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
        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        //[NonPersistent]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }

        [ModelDefault("Caption", "ẩn dữ liệu")]
        [Browsable(false)]
        [ImmediatePostData]
        public bool AnDuLieu
        {
            get { return _AnDuLieu; }
            set { SetPropertyValue("AnDuLieu", ref _AnDuLieu, value); }
        }

        #region Danh sách
        [Aggregated]
        [Association("QuanLyHoatDongKhac-ListChiTietKeKhaiHoatDongKhac")]
        [ModelDefault("Caption", "Chi tiết kê khai HD khác")]
        public XPCollection<ChiTietKeKhaiHoatDongKhac> ListChiTietKeKhaiHoatDongKhac
        {
            get
            {
                return GetCollection<ChiTietKeKhaiHoatDongKhac>("ListChiTietKeKhaiHoatDongKhac");
            }
        }
        [Aggregated]
        [Association("QuanLyHoatDongKhac-ListChiTiet")]
        [ModelDefault("Caption", "Chi tiết hoạt động khác")]
        public XPCollection<ChiTietHoatDongKhac> listChiTiet
        {
            get
            {
                return GetCollection<ChiTietHoatDongKhac>("listChiTiet");
            }
        }

        [Aggregated]
        [Association("QuanLyHoatDongKhac-ListChamBaiCoiThi")]
        [ModelDefault("Caption", "Chi tiết chấm bài coi thi")]
        public XPCollection<ChiTietThuLaoChamBaiCoiThi> ListChamBaiCoiThi
        {
            get
            {
                return GetCollection<ChiTietThuLaoChamBaiCoiThi>("ListChamBaiCoiThi");
            }
        }

        [Aggregated]
        [Association("QuanLyHoatDongKhac-ListThanhToanKLGD")]
        [ModelDefault("Caption", "Chi tiết tính khối lượng giảng dạy")]
        public XPCollection<ChiTietThanhToanKhoiLuongGiangDay> ListThanhToanKLGD
        {
            get
            {
                return GetCollection<ChiTietThanhToanKhoiLuongGiangDay>("ListThanhToanKLGD");
            }
        }

        [Aggregated]
        [Association("QuanLyHoatDongKhac-ListChiTietKeKhaiHuongDanKhac")]
        [ModelDefault("Caption", "Chi tiết hướng dẫn khác")]
        public XPCollection<ChiTietKeKhaiHuongDanKhac> ListChiTietKeKhaiHuongDanKhac
        {
            get
            {
                return GetCollection<ChiTietKeKhaiHuongDanKhac>("ListChiTietKeKhaiHuongDanKhac");
            }
        }

        [Aggregated]
        [Association("QuanLyHoatDongKhac-ListChiTietKeKhaiHDK_VHU")]
        [ModelDefault("Caption", "Chi tiết hoạt động khác")]
        public XPCollection<ChiTietKeKhaiHDK_VHU> ListChiTietKeKhaiHDK_VHU
        {
            get
            {
                return GetCollection<ChiTietKeKhaiHDK_VHU>("ListChiTietKeKhaiHDK_VHU");
            }
        }

        #endregion
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
                return String.Format("{0} - Năm học {1} {2} {3} ",ThongTinTruong.TenBoPhan, NamHoc != null ? NamHoc.TenNamHoc : "", HocKy != null ? "- " + HocKy.TenHocKy : "", KyTinhPMS != null ? "- Đợt" + KyTinhPMS.Dot.ToString() : "");
            }
        }
       
     
        [ModelDefault("Caption", "Thông tin công ty")]
        [ModelDefault("AllowEdit","False")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }
        public QuanLyHoatDongKhac(Session session) : base(session) { }

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

        protected override void OnSaving()
        {
            if (TruongConfig.MaTruong == "VHU")
            {
                AnDuLieu = true;
            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            if (!IsDeleted)
            {
                if (TruongConfig.MaTruong == "UEL")
                {
                    if (Khoa)
                    {
                        SqlParameter[] param = new SqlParameter[1]; /*Số parameter trên Store Procedure*/
                        param[0] = new SqlParameter("@QuanLyHoatDongKhac", this.Oid);
                        DataProvider.ExecuteNonQuery("spd_PMS_UEL_TaoBangChotThuLao", System.Data.CommandType.StoredProcedure, param);
                    }
                }
            }
        }
        protected override void OnDeleting()
        {
            base.OnDeleting();
            if (BangChotThuLao != null)
            {
                XtraMessageBox.Show("Đã chốt thù lao - không thể xóa");
                return;
            }
        }
    }
}
