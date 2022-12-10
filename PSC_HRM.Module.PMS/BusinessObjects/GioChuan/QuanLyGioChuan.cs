using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.PMS.GioChuan
{
    //[DefaultProperty("Caption")]

    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Quản lý giờ chuẩn")]
    [Appearance("UFM_Hide", TargetItems = "ListDinhMuc_NghienCuuKhoaHoc",
                                    Visibility = ViewItemVisibility.Hide,
                                    Criteria = "ThongTinTruong.TenVietTat != 'UFM'")]
    [Appearance("VHU_Hide", TargetItems = "ListDinhMucGiamTruNhanVien",
                                    Visibility = ViewItemVisibility.Hide,
                                    Criteria = "ThongTinTruong.TenVietTat != 'VHU'")]

    [Appearance("HUFLIT_Hide", TargetItems = "ListDinhMucChucVu",
                                    Visibility = ViewItemVisibility.Hide,
                                    Criteria = "ThongTinTruong.TenVietTat = 'HUFLIT'")]

    [Appearance("QNU_Hide", TargetItems = "HocKy",
                                    Visibility = ViewItemVisibility.Hide,
                                    Criteria = "ThongTinTruong.TenVietTat = 'QNU'")]
    [Appearance("UEL_Hide", TargetItems = "ListDinhMucChucVu",
                                    Visibility = ViewItemVisibility.Hide,
                                    Criteria = "ThongTinTruong.TenVietTat = 'UEL'")]
    [Appearance("HVNH_Hide", TargetItems = "HocKy",
                                    Visibility = ViewItemVisibility.Hide,
                                    Criteria = "ThongTinTruong.TenVietTat = 'NHH'")]
    [Appearance("!HVNH_Hide", TargetItems = "ListDinhMuc_GioTruKhac",
                                    Visibility = ViewItemVisibility.Hide,
                                    Criteria = "ThongTinTruong.TenVietTat != 'NHH'")]
    public class QuanLyGioChuan : BaseObject
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy", DataSourcePropertyIsNullMode.SelectAll)]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }
      

        [Aggregated]
        [Association("QuanLyGioChuan-ListDinhMucChucVu")]
        [ModelDefault("Caption", "Định mức chức vụ")]
        public XPCollection<DinhMucChucVu> ListDinhMucChucVu
        {
            get
            {
                return GetCollection<DinhMucChucVu>("ListDinhMucChucVu");
            }
        }//dinhmucgiochuan
        [Aggregated]
        [Association("QuanLyGioChuan-ListDinhMuc_NghienCuuKhoaHoc")]
        [ModelDefault("Caption", "Định mức NCKH")]
        public XPCollection<DinhMuc_NghienCuuKhoaHoc> ListDinhMuc_NghienCuuKhoaHoc
        {
            get
            {
                return GetCollection<DinhMuc_NghienCuuKhoaHoc>("ListDinhMuc_NghienCuuKhoaHoc");
            }
        }//dinhmucgiochuan

        [Aggregated]
        [Association("QuanLyGioChuan-ListDinhMucChucVuNhanVien")]
        [ModelDefault("Caption", "Định mức chức vụ nhân viên")]
        public XPCollection<DinhMucChucVu_NhanVien> ListDinhMucChucVuNhanVien
        {
            get
            {
                return GetCollection<DinhMucChucVu_NhanVien>("ListDinhMucChucVuNhanVien");
            }
        }//dinhmucgiochuan

        [Aggregated]
        [Association("QuanLyGioChuan-ListDinhMucGiamTruNhanVien")]
        [ModelDefault("Caption", "Định mức giảm trừ nhân viên")]
        public XPCollection<DinhMucGiamTru_NhanVien> ListDinhMucGiamTruNhanVien
        {
            get
            {
                return GetCollection<DinhMucGiamTru_NhanVien>("ListDinhMucGiamTruNhanVien");
            }
        }

        [Aggregated]
        [Association("QuanLyGioChuan-ListDinhMuc_GioTruKhac")]
        [ModelDefault("Caption", "Định mức giờ trừ khác")]
        public XPCollection<DinhMuc_GioTruKhac> ListDinhMuc_GioTruKhac
        {
            get
            {
                return GetCollection<DinhMuc_GioTruKhac>("ListDinhMuc_GioTruKhac");
            }
        }
        public QuanLyGioChuan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction(); 
            // Place here your initialization code.
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
        }
      
    }
}