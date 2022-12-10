using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using PSC_HRM.Module.PMS.NghiepVu;

namespace PSC_HRM.Module.PMS.ThoiKhoaBieu
{
    [ModelDefault("Caption", "Thời khóa biểu giảng dạy")]

    [DefaultProperty("Caption")]

    [Appearance("KhoiLuongGiangDay_Khoa", TargetItems = "*", Enabled = false, Criteria = "BangChotThuLao is not null")]
   
    [Appearance("Hide_NEU", TargetItems = "KyTinhPMS"
            , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.MaQuanLy = 'NEU'")]
     public class ThoiKhoaBieu_KhoiLuongGiangDay : ThongTinChungPMS
    {
        private bool _Khoa;
        private BangChotThuLao _BangChotThuLao;
        [ModelDefault("Caption", "Bảng chốt")]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public BangChotThuLao BangChotThuLao
        {
            get { return _BangChotThuLao; }
            set
            {
                SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value);
                if (!IsLoading)
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
        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format(" {0} {1} {2}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", HocKy != null ? HocKy.TenHocKy : "");
               }
        }

        [Aggregated]
        [Association("ThoiKhoaBieu_KhoiLuongGiangDay-ListChiTiet")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiTiet_ThoiKhoaBieu> ListChiTiet
        {
            get
            {
                return GetCollection<ChiTiet_ThoiKhoaBieu>("ListChiTiet");
            }
        }
        public ThoiKhoaBieu_KhoiLuongGiangDay(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}