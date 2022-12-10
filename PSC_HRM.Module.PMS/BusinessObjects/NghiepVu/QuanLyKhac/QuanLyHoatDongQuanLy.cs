using System;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi
{
    [Appearance("Hide_Khoa", TargetItems = "ThongTinTruong;NamHoc;HocKy;KyTinhPMS"
                                            , Enabled = false, Criteria = "Khoa")]
    [ModelDefault("Caption", "Giảng viên tham gia hoạt động quản lý")]
    [Appearance("AnDuLieu", TargetItems = "HocKy; NamHoc; KyTinhPMS", Enabled = false, Criteria = "AnDuLieu")]
    [DefaultProperty("Caption")]
    public class QuanLyHoatDongQuanLy : ThongTinChungPMS
    {
        private bool _Khoa;
        private bool _AnDuLieu;


        [ModelDefault("Caption", "ẩn dữ liệu")]
        [Browsable(false)]
        [ImmediatePostData]
        public bool AnDuLieu
        {
            get { return _AnDuLieu; }
            set { SetPropertyValue("AnDuLieu", ref _AnDuLieu, value); }
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


        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }

        [Aggregated]
        [Association("QuanLyHoatDongQuanLy-ListChiTietQuanLyHoatDongQuanLy")]
        [ModelDefault("Caption", "Chi tiết hoạt động quản lý")]
        public XPCollection<ChiTietQuanLyHoatDongQuanLy> ListChiTietQuanLyHoatDongQuanLy
        {
            get
            {
                return GetCollection<ChiTietQuanLyHoatDongQuanLy>("ListChiTietQuanLyHoatDongQuanLy");
            }
        }
        public QuanLyHoatDongQuanLy(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        protected override void OnSaving()
        {
            if (TruongConfig.MaTruong == "VHU")
            {
                AnDuLieu = true;
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}