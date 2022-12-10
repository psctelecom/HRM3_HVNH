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
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;


namespace PSC_HRM.Module.PMS.NghiepVu.NCKH
{
    [Appearance("Hide_VHU", TargetItems = "KyTinhPMS"
                                            , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'VHU'")]
    [Appearance("Hide_HUFLIT", TargetItems = "KyTinhPMS;HocKy"
                                            , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'HUFLIT'")]
    [Appearance("Hide_Khoa", TargetItems = "ThongTinTruong;NamHoc;HocKy;KyTinhPMS"
                                            , Enabled = false, Criteria = "Khoa")]
    [Appearance("AnDuLieu", TargetItems = "HocKy; NamHoc; KyTinhPMS", Enabled = false, Criteria = "AnDuLieu")]
    [ModelDefault("Caption", "Quản lý NCKH")]
    [DefaultProperty("ThongTin")]
    public class QuanLyNCKH : ThongTinChungPMS
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

        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public String ThongTin
        {
            get
            {
                return String.Format("{0} {1}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "");
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
        [Association("QuanLyNCKH-ListChiTietNCKH")]
        [ModelDefault("Caption", "Danh sách")]
        public XPCollection<ChiTietNCKH> ListChiTietNCKH
        {
            get
            {
                return GetCollection<ChiTietNCKH>("ListChiTietNCKH");
            }
        }
        public QuanLyNCKH(Session session) : base(session) { }

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
       }
    }

}
