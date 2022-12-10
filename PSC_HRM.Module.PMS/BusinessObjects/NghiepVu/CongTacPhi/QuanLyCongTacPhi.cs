using System;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi
{

    [ModelDefault("Caption", "Quản lý công tác phí")]
    [DefaultProperty("Caption")]
    [Appearance("QuanLyCongTacPhi_Khoa", TargetItems = "*", Enabled = false, Criteria = "Khoa = 1")]
    [Appearance("Hide_HVNH", TargetItems = "KyTinhPMS;BacDaoTao"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'NHH'")]
    [Appearance("Hide_NEU", TargetItems = "KyTinhPMS;ListChiTietCongTacPhi"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.MaQuanLy = 'NEU'")]
    public class QuanLyCongTacPhi : ThongTinChungPMS
    {
        private bool _Khoa;

        [ModelDefault("Caption", "Khóa")]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }
        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                if (TruongConfig.MaTruong == "HVNH")
                    return String.Format(" {0} {1} {2}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.TenHocKy : "");
                else
                    return String.Format(" {0} {1} {2} {3}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.TenHocKy : "", KyTinhPMS != null ? " - Đợt " + KyTinhPMS.Dot.ToString() : "");
            }
        }

        [Aggregated]
        [Association("QuanLyCongTacPhi-ListChiTietCongTacPhi")]
        [ModelDefault("Caption", "Chi tiết công tác phí")]
        public XPCollection<ChiTietCongTacPhi> ListChiTietCongTacPhi
        {
            get
            {
                return GetCollection<ChiTietCongTacPhi>("ListChiTietCongTacPhi");
            }
        }

        [Aggregated]
        [Association("QuanLyCongTacPhi-ListChiTietCongTacPhi_NEU")]
        [ModelDefault("Caption", "Chi tiết công tác phí")]
        public XPCollection<ChiTietCongTacPhi_NEU> ListChiTietCongTacPhi_NEU
        {
            get
            {
                return GetCollection<ChiTietCongTacPhi_NEU>("ListChiTietCongTacPhi_NEU");
            }
        }
        public QuanLyCongTacPhi(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}