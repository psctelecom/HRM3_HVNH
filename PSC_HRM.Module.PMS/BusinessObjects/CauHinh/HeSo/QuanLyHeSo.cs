using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.NghiepVu.NCKH;

namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Quản lý hệ số")]

    [Appearance("Hide_HeSo_QNU", TargetItems = "ListHeSoLuong;ListHeSoHocPhan"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'QNU'")]//Mới sửa không đóng ListHeSoThaoLuan
    [Appearance("Hide_HeSo_DNU", TargetItems = "ListHeSoTNTH;ListHeSoHocPhan"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'DNU'")]//Mới sửa không đóng ListHeSoThaoLuan
    [Appearance("Hide_HeSo_HVNH", TargetItems = "ListHeSoThucHanh;ListHeSoTNTH;ListHeSoThaoLuan;ListHeSoCoSo;ListHeSoHocPhan"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'NHH'")]

    [Appearance("Hide_HeSo_VHU", TargetItems = "ListHeSoLuong;ListHeSoCoSo;ListHeSoTinChi;ListHeSoTNTH;ListHeSoHocPhan;ListHeSoTNTH;ListHeSoChucDanhMonHoc;ListHeSoChucDanhNhieuMonHoc;ListHeSoThamNien;ListHeSoThucHanh;ListHeSoThaoLuan"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'VHU'")]
    [Appearance("Hide_HeSo_NoVHU", TargetItems = "ListHeSoDayOnline"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat != 'VHU'")]
    [Appearance("Hide_HeSo_UFM", TargetItems = "ListHeSoLuong;ListHeSoTinChi;ListHeSoCoSo;ListHeSoTNTH;"
                                                +"ListHeSoBacDaoTao;ListHeSoThaoLuan;ListHeSoTNTH;ListHeSoNgonNgu;ListHeSoHocPhan"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'UFM'")]

    [Appearance("Hide_HeSo_NEU", TargetItems = "ListHeSoLuong;ListHeSoTNTH;ListHeSoThaoLuan;ListHeSoThucHanh;ListHeSoHocPhan;HocKy"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.MaQuanLy = 'NEU'")]

    [Appearance("Hide_HeSo_HufLit", TargetItems = "ListHeSoBacDaoTao;ListHeSoLuong;ListHeSoThaoLuan;ListHeSoTNTH;ListHeSoThucHanh;ListHeSoNgonNgu;ListHeSoHocPhan;ListHeSoThamNien;ListHeSoChucDanhMonHoc"
                                               , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.MaQuanLy = 'HUFLIT'")]
    [Appearance("Hide_HeSo_NoHufLit", TargetItems = "ListHeSoChucDanhNhieuMonHoc"
                                               , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.MaQuanLy != 'HUFLIT'")]
    [Appearance("AnDuLieu", TargetItems = "HocKy; NamHoc; KyTinhPMS", Enabled = false, Criteria = "AnDuLieu")]

    [Appearance("Hide_HeSo_UEL", TargetItems = "ListHeSoKhac;ListHeSoDiaDiemNgoaiGio", Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.MaQuanLy != 'UEL'")]
    [Appearance("Hide_HeSo_No_UEL", TargetItems = "ListHeSoGiangDayNgoaiGio;ListHeSoBacDaoTao;ListHeSoTinChi;ListHeSoLuong;ListHeSoCoSo;ListHeSoThaoLuan;ListHeSoTNTH;ListHeSoThucHanh;ListHeSoNgonNgu;ListHeSoHocPhan;ListHeSoThamNien;ListHeSoChucDanhMonHoc;ListHeSoChucDanhNhieuMonHoc", Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.MaQuanLy = 'UEL'")]
    public class QuanLyHeSo : BaseObject
    {
        #region ThongTin
        private ThongTinTruong _ThongTinTruong;

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "false")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }
        private NamHoc _NamHoc;
        private HocKy _HocKy;

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading)
                    if (NamHoc != null)
                    {
                        updateHocKyList();
                    }
            }
        }
        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("HocKyList", DataSourcePropertyIsNullMode.SelectAll)]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }
        [Browsable(false)]
        public XPCollection<HocKy> HocKyList { get; set; }
        public void updateHocKyList()
        {
            HocKyList = new XPCollection<HocKy>(Session);
            HocKyList.Criteria = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
            SortingCollection sortHK = new SortingCollection();
            sortHK.Add(new SortProperty("TuNgay", DevExpress.Xpo.DB.SortingDirection.Ascending));
            HocKyList.Sorting = sortHK;
            OnChanged("HocKyList");
        }
        #endregion

        private bool _AnDuLieu;

        [ModelDefault("Caption", "ẩn dữ liệu")]
        [Browsable(false)]
        [ImmediatePostData]
        public bool AnDuLieu
        {
            get { return _AnDuLieu; }
            set { SetPropertyValue("AnDuLieu", ref _AnDuLieu, value); }
        }

        [Aggregated]
        [ModelDefault("Caption", "Hệ số bậc đào tạo")]
        [Association("QuanLyHeSo-ListHeSoBacDaoTao")]
        public XPCollection<HeSoBacDaoTao> ListHeSoBacDaoTao
        {
            get
            {
                return GetCollection<HeSoBacDaoTao>("ListHeSoBacDaoTao");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Hệ số giảng dạy ngoài giờ")]
        [Association("QuanLyHeSo-ListHeSoGiangDayNgoaiGio")]
        public XPCollection<HeSoGiangDay_NgoaiGio> ListHeSoGiangDayNgoaiGio
        {
            get
            {
                return GetCollection<HeSoGiangDay_NgoaiGio>("ListHeSoGiangDayNgoaiGio");
            }
        }

        [ImmediatePostData]
        [Aggregated]
        [ModelDefault("Caption", "Hệ số lớp đông")]
        [Association("QuanLyHeSo-ListHeSoLopDong")]
        public XPCollection<HeSoLopDong> ListHeSoLopDong
        {
            get
            {
                return GetCollection<HeSoLopDong>("ListHeSoLopDong");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Hệ số khác")]
        [Association("QuanLyHeSo-ListHeSoKhac")]
        public XPCollection<HeSoKhac_UEL> ListHeSoKhac
        {
            get
            {
                return GetCollection<HeSoKhac_UEL>("ListHeSoKhac");
            }
        }



        [Aggregated]
        [ModelDefault("Caption", "Hệ số giảng dạy địa điểm ngoài giờ (UEL)")]
        [Association("QuanLyHeSo-ListHeSoDiaDiemNgoaiGio")]
        public XPCollection<HeSoDiaDiemNgoaiGio> ListHeSoDiaDiemNgoaiGio
        {
            get
            {
                return GetCollection<HeSoDiaDiemNgoaiGio>("ListHeSoDiaDiemNgoaiGio");
            }
        }



        [Aggregated]
        [ModelDefault("Caption", "Hệ số tín chỉ")]
        [Association("QuanLyHeSo-ListHeSoTinChi")]
        public XPCollection<HeSoTinChi> ListHeSoTinChi
        {
            get
            {
                return GetCollection<HeSoTinChi>("ListHeSoTinChi");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Hệ số lương")]
        [Association("QuanLyHeSo-ListHeSoLuong")]
        public XPCollection<HeSoLuong> ListHeSoLuong
        {
            get
            {
                return GetCollection<HeSoLuong>("ListHeSoLuong");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Hệ số cơ sở")]
        [Association("QuanLyHeSo-ListHeSoCoSo")]
        public XPCollection<HeSoCoSo> ListHeSoCoSo
        {
            get
            {
                return GetCollection<HeSoCoSo>("ListHeSoCoSo");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Hệ số dạy online")]
        [Association("QuanLyHeSo-ListHeSoDayOnline")]
        public XPCollection<HeSoDayOnline> ListHeSoDayOnline
        {
            get
            {
                return GetCollection<HeSoDayOnline>("ListHeSoDayOnline");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Hệ số chức danh")]
        [Association("QuanLyHeSo-ListHeSoChucDanh")]
        public XPCollection<HeSoChucDanh> ListHeSoChucDanh
        {
            get
            {
                return GetCollection<HeSoChucDanh>("ListHeSoChucDanh");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Hệ số chức danh (Nhân viên)")]
        [Association("QuanLyHeSo-ListHeSo_ChucDanhNhanVien")]
        public XPCollection<HeSo_ChucDanhNhanVien> ListHeSo_ChucDanhNhanVien
        {
            get
            {
                return GetCollection<HeSo_ChucDanhNhanVien>("ListHeSo_ChucDanhNhanVien");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Hệ số thảo luận")]
        [Association("QuanLyHeSo-ListHeSoThaoLuan")]
        public XPCollection<HeSoThaoLuan> ListHeSoThaoLuan
        {
            get
            {
                return GetCollection<HeSoThaoLuan>("ListHeSoThaoLuan");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Hệ số TNTH")]
        [Association("QuanLyHeSo-ListHeSoTNTH")]
        public XPCollection<HeSoTNTH> ListHeSoTNTH
        {
            get
            {
                return GetCollection<HeSoTNTH>("ListHeSoTNTH");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Hệ số Thực Hành")]
        [Association("QuanLyHeSo-ListHeSoThucHanh")]
        public XPCollection<HeSoThucHanh> ListHeSoThucHanh
        {
            get
            {
                return GetCollection<HeSoThucHanh>("ListHeSoThucHanh");
            }
        }
       
        [Aggregated]
        [ModelDefault("Caption", "Hệ số ngôn ngữ")]
        [Association("QuanLyHeSo-ListHeSoNgonNgu")]
        public XPCollection<HeSoNgonNgu> ListHeSoNgonNgu
        {
            get
            {
                return GetCollection<HeSoNgonNgu>("ListHeSoNgonNgu");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Hệ số học phần")]
        [Association("QuanLyHeSo-ListHeSoHocPhan")]
        public XPCollection<HeSoHocPhan> ListHeSoHocPhan
        {
            get
            {
                return GetCollection<HeSoHocPhan>("ListHeSoHocPhan");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Hệ số thâm niên")]
        [Association("QuanLyHeSo-ListHeSoThamNien")]
        public XPCollection<HeSoThamNien> ListHeSoThamNien
        {
            get
            {
                return GetCollection<HeSoThamNien>("ListHeSoThamNien");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Hệ số chức danh (Đặc biệt)")]
        [Association("QuanLyHeSo-ListHeSo_ChucDanhMonHoc")]
        public XPCollection<HeSo_ChucDanhMonHoc> ListHeSoChucDanhMonHoc
        {
            get
            {
                return GetCollection<HeSo_ChucDanhMonHoc>("ListHeSoChucDanhMonHoc");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Hệ số chức danh môn học")]
        [Association("QuanLyHeSo-ListHeSoChucDanhNhieuMonHoc")]
        public XPCollection<HeSoChucDanhNhieuMonHoc> ListHeSoChucDanhNhieuMonHoc
        {
            get
            {
                return GetCollection<HeSoChucDanhNhieuMonHoc>("ListHeSoChucDanhNhieuMonHoc");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Hệ số tính chất môn học")]
        [Association("QuanLyHeSo-ListHeSoMonHocBoSung")]
        public XPCollection<HeSoMonHocBoSung> ListHeSoMonHocBoSung
        {
            get
            {
                return GetCollection<HeSoMonHocBoSung>("ListHeSoMonHocBoSung");
            }
        }

        public QuanLyHeSo(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
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
