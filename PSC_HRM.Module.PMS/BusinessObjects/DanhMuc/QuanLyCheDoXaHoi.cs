using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ModelDefault("Caption","Quản lý chế độ xã hội")]
    [DefaultProperty("Caption")]
    [Appearance("Hide_QuanLyCheDoXaHoi_DNU", TargetItems = "KyTinhPMS,HocKy"
                                                , Visibility = ViewItemVisibility.Hide, 
                                                Criteria = "ThongTinTruong.TenVietTat = 'DNU' OR ThongTinTruong.TenVietTat = 'VHU'")]

    [Appearance("HUFLIT_Hide", TargetItems = "KyTinhPMS",
                                    Visibility = ViewItemVisibility.Hide,
                                    Criteria = "ThongTinTruong.TenVietTat = 'HUFLIT'")]
    [Appearance("AnDuLieu", TargetItems = "HocKy; NamHoc; KyTinhPMS", Enabled = false, Criteria = "AnDuLieu")]
    public class QuanLyCheDoXaHoi : ThongTinChungPMS
    {

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
        [VisibleInDetailView(false)]
        public string Caption
        {
            get
            {
                return String.Format(" {0} {1} {2}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.TenHocKy : "");
            }
        }


        [Aggregated]
        [ModelDefault("Caption", "Danh sách chi tiết")]
        [Association("QuanLyCheDoXaHoi-ListChiTietCheDoXH")]
        public XPCollection<ChiTietCheDoXaHoi> ListChiTietCheDoXH
        {
            get
            {
                return GetCollection<ChiTietCheDoXaHoi>("ListChiTietCheDoXH");
            }
        }

        public QuanLyCheDoXaHoi(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }
        protected override void OnSaving()
        {
            if (TruongConfig.MaTruong == "VHU")
            {
                AnDuLieu = true;
            }
        }
    }
}
