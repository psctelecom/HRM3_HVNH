using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang
{
    [ModelDefault("Caption", "Quản lý sau giảng dạy")]
    [DefaultProperty("ThongTin")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "ThongTinTruong;NamHoc;HocKy", "Bảng kê khai đã tồn tại")]
    [Appearance("Khoa", TargetItems = "*", Enabled = false, Criteria = "BangChotThuLao is not null")]
    [Appearance("Hide_HVNH", TargetItems = "KyTinhPMS"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'NHH'")]
    public class QuanLyKeKhaiSauGiang : ThongTinChungPMS
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
                if (BangChotThuLao != null)
                    Khoa = true;
                else
                    Khoa = false;
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
        [Association("QuanLyKeKhaiSauGiang-ListChiTietKeKhaiSauGiang")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiTietKeKhaiSauGiang> ListChiTietKeKhaiSauGiang
        {
            get
            {
                return GetCollection<ChiTietKeKhaiSauGiang>("ListChiTietKeKhaiSauGiang");
            }
        }
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        [VisibleInDetailView(false)]
        public String ThongTin
        {
            get
            {
                return String.Format("{0} {1} {2}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", HocKy != null ? " -  " + HocKy.TenHocKy : "");
            }
        }
        public QuanLyKeKhaiSauGiang(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
       
        public override void AfterConstruction()
        {
            base.AfterConstruction();     
        }
    }
}