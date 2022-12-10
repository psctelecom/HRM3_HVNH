using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.PMS.NghiepVu.KhaoThi;

namespace PSC_HRM.Module.PMS.NghiepVu.KhaoThi
{
    [ModelDefault("Caption", "Quản lý khảo thí")]

    [DefaultProperty("ThongTin")]
    [Appearance("Khoa", TargetItems = "*", Enabled = false, Criteria = "BangChotThuLao is not null")]
    [Appearance("Hide_HVNH", TargetItems = "BacDaoTao;KyTinhPMS;ListChiTietCoiThi_ChamThi;ListChiTietChamBaiCoiThi;ListDanhSachSVThamGiaThi"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'NHH'")]

    [Appearance("Hide_DNU", TargetItems = "ListChiTietThongTinChamBai;BacDaoTao;KyTinhPMS;ListChiTietChamBaiCoiThi;ListDanhSachSVThamGiaThi;ListChiTietCoiThi_ChamThi"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'DNU'")]
    [Appearance("Hide_!DNU", TargetItems = "ListCoiThi;ListRaDe;ListChamThi;KhoaDaoTao;DonGiaRaDe;DonGiaDuyetDe;DonGiaChamThi;DonGiaCoiThi;LopDaoTao"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat != 'DNU'")]
    [Appearance("Hide_HUFLIT", TargetItems = "ListChiTietThongTinChamBai;BacDaoTao;KyTinhPMS;ListChiTietChamBaiCoiThi;ListCoiThi;ListRaDe;ListChamThi;KhoaDaoTao;DonGiaRaDe;DonGiaDuyetDe;DonGiaChamThi;DonGiaCoiThi;LopDaoTao"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'HUFLIT'")]

    public class QuanLyKhaoThi : ThongTinChungPMS
    {
        private BacDaoTao _BacDaoTao;
        private bool _Khoa; 
        private BangChotThuLao _BangChotThuLao;

        #region DNU
        private string _KhoaDaoTao;
        private string _LopDaoTao;
        private decimal _DonGiaRaDe;
        private decimal _DonGiaDuyetDe;
        private decimal _DonGiaChamThi;
        private decimal _DonGiaCoiThi;
        #endregion

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
        [ModelDefault("Caption", "Bậc đào tạo")]
        [ImmediatePostData]
        [DataSourceProperty("listBacDaoTao")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
     
        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }
        [Aggregated]
        [Association("QuanLyKhaoThi-ListChiTietChamBaiCoiThi")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiTietChamBaiCoiThi> ListChiTietChamBaiCoiThi
        {
            get
            {
                return GetCollection<ChiTietChamBaiCoiThi>("ListChiTietChamBaiCoiThi");
            }
        }


        #region DNU
        [ModelDefault("Caption", "Khóa đào tạo")]
        public string KhoaDaoTao
        {
            get { return _KhoaDaoTao; }
            set { SetPropertyValue("KhoaDaoTao", ref _KhoaDaoTao, value); }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Diễn giải")]
        public string LopDaoTao
        {
            get { return _LopDaoTao; }
            set { SetPropertyValue("LopDaoTao", ref _LopDaoTao, value); }
        }
        [ModelDefault("Caption", "Đơn giá (Ra đề)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        public decimal DonGiaRaDe
        {
            get { return _DonGiaRaDe; }
            set { SetPropertyValue("DonGiaRaDe", ref _DonGiaRaDe, value); }
        }
        [ModelDefault("Caption", "Đơn giá (Duyệt đề)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        public decimal DonGiaDuyetDe
        {
            get { return _DonGiaDuyetDe; }
            set { SetPropertyValue("DonGiaDuyetDe", ref _DonGiaDuyetDe, value); }
        }
        [ModelDefault("Caption", "Đơn giá (Chấm thi)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        public decimal DonGiaChamThi
        {
            get { return _DonGiaChamThi; }
            set { SetPropertyValue("DonGiaChamThi", ref _DonGiaChamThi, value); }
        }
        [ModelDefault("Caption", "Đơn giá (Coi thi)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DonGiaCoiThi
        {
            get { return _DonGiaCoiThi; }
            set { SetPropertyValue("DonGiaCoiThi", ref _DonGiaCoiThi, value); }
        }
        #endregion
        [Aggregated]
        [Association("QuanLyKhaoThi-ListChamThi")]
        [ModelDefault("Caption", "Chi tiết chấm thi")]
        public XPCollection<ChiTietChamThi> ListChamThi
        {
            get
            {
                return GetCollection<ChiTietChamThi>("ListChamThi");
            }
        }
        [Aggregated]
        [Association("QuanLyKhaoThi-ListRaDe")]
        [ModelDefault("Caption", "Chi tiết ra đề")]
        public XPCollection<ChiTietRaDe> ListRaDe
        {
            get
            {
                return GetCollection<ChiTietRaDe>("ListRaDe");
            }
        }
        [Aggregated]
        [Association("QuanLyKhaoThi-ListCoiThi")]
        [ModelDefault("Caption", "Chi tiết Coi thi")]
        public XPCollection<ChiTietCoiThi> ListCoiThi
        {
            get
            {
                return GetCollection<ChiTietCoiThi>("ListCoiThi");
            }
        }
        [Aggregated]
        [Association("QuanLyKhaoThi-ListChiTietCoiThi_ChamThi")]
        [ModelDefault("Caption", "Chi tiết Coi thi/Chấm thi")]
        public XPCollection<ChiTietCoiThi_ChamThi> ListChiTietCoiThi_ChamThi
        {
            get
            {
                return GetCollection<ChiTietCoiThi_ChamThi>("ListChiTietCoiThi_ChamThi");
            }
        }
        [Aggregated]
        [Association("QuanLyKhaoThi-ListThongTinChamBai")]
        [ModelDefault("Caption", "Thông tin khảo thí")]
        public XPCollection<ThongTinChamBai> ListChiTietThongTinChamBai
        {
            get
            {
                return GetCollection<ThongTinChamBai>("ListChiTietThongTinChamBai");
            }
        }

        [Aggregated]
        [Association("QuanLyKhaoThi-ListDanhSachSVThamGiaThi")]
        [ModelDefault("Caption", "Số lượt sv tham gia thi")]
        public XPCollection<DanhSachSVThamGiaThi> ListDanhSachSVThamGiaThi
        {
            get
            {
                return GetCollection<DanhSachSVThamGiaThi>("ListDanhSachSVThamGiaThi");
            }
        }
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public String ThongTin
        {
            get
            {
                return String.Format("{0} {1} {2} {3} {4}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", BacDaoTao != null ? " Bậc đào tạo " + BacDaoTao.TenBacDaoTao : "", KyTinhPMS != null ? " - Đợt " + KyTinhPMS.Dot.ToString() : "", HocKy != null ? " - " + HocKy.TenHocKy.ToString() : "");
            }
        }
        public QuanLyKhaoThi(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
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
                if (listBDT != null)
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
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            if (TruongConfig.MaTruong != "DNU")
                LoadBacDaoTao();
        }

    }

}