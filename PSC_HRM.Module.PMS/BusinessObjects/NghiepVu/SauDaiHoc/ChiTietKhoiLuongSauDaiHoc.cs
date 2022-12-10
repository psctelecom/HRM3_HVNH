using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.PMS.DanhMuc;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.PMS.NghiepVu.SauDaiHoc
{
    [ModelDefault("Caption", "Chi tiết sau đại học")]
    public class ChiTietKhoiLuongSauDaiHoc : BaseObject
    {
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private HocPhan_SauDaiHoc _TenMonHoc;
        private SiSoChuyenNganh _ChuyenNganh1;
        private SiSoChuyenNganh _ChuyenNganh2;
        private SiSoChuyenNganh _ChuyenNganh3;
        private SiSoChuyenNganh _ChuyenNganh4;
        private SiSoChuyenNganh _ChuyenNganh5;
        private SiSoChuyenNganh _ChuyenNganh6;
        private SiSoChuyenNganh _ChuyenNganh7;
        private SiSoChuyenNganh _ChuyenNganh8;
        private SiSoChuyenNganh _ChuyenNganh9;
        private SiSoChuyenNganh _ChuyenNganh10;

        private int _TongSoHocVien;
        private decimal _SoTinChi;
        private decimal _SoTietLyThuyet;
        private decimal _HeSo_GiangDay;
        private decimal _HeSo_LopDong;

        private HinhThucThi _HinhThucThi;
        private decimal _TongGioGiangDay;

        private decimal _GioQuyDoi_RaDe;
        private decimal _GioQuyDoi_ChamBaiGiuaKy;
        private int _SoHocVien_DuThi;

        private decimal _GioQuyDoi_ChamTuLuan;
        private decimal _GioQuyDoi_ChamTieuLuan;
        private decimal _GioQuyDoi_VanDap;

        private int _SoCaCoiThi;
        private decimal _GioQuyDoi_CoiThi;

        private decimal _SoLuanVanHuongDan;
        private decimal _SoGioQuyDoiHuongDanLuanVan;
        private decimal _TongGio;
        private string _GhiChu;




        #region BP_NV
        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption", "Giảng viên")]
        [ImmediatePostData]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading)
                    if (NhanVien != null)
                        BoPhan = NhanVien.BoPhan;
                    else
                        BoPhan = null;
            }
        }
        #endregion

        #region Môn học
        [ModelDefault("Caption", "Học phần")]
        [ImmediatePostData]
        [DataSourceProperty("ListMonHoc", DataSourcePropertyIsNullMode.SelectAll)]
        public HocPhan_SauDaiHoc TenMonHoc
        {
            get { return _TenMonHoc; }
            set
            {
                SetPropertyValue("TenMonHoc", ref _TenMonHoc, value);
                if (!IsLoading)
                    if (TenMonHoc != null)
                        SoTinChi = TenMonHoc.SoTinChi;
                    else
                        SoTinChi = 0;
            }
        }
        [Browsable(false)]
        public XPCollection<HocPhan_SauDaiHoc> ListMonHoc { get; set; }
        public void UpdateListMonHoc()
        {
            ListMonHoc = new XPCollection<HocPhan_SauDaiHoc>(Session);
            ListMonHoc.Criteria = CriteriaOperator.Parse("NamHoc = ?", QuanLySauDaiHoc.NamHoc.Oid);
            SortingCollection sortList = new SortingCollection();
            sortList.Add(new SortProperty("TenHocPhan", DevExpress.Xpo.DB.SortingDirection.Ascending));
            ListMonHoc.Sorting = sortList;

            OnChanged("ListMonHoc");
        }
        #endregion

        #region Chuyên ngành
        [ModelDefault("Caption", "Chuyên ngành 1")]
        public SiSoChuyenNganh ChuyenNganh1
        {
            get { return _ChuyenNganh1; }
            set { SetPropertyValue("ChuyenNganh1", ref _ChuyenNganh1, value); }
        }
        [ModelDefault("Caption", "Chuyên ngành 2")]
        public SiSoChuyenNganh ChuyenNganh2
        {
            get { return _ChuyenNganh2; }
            set { SetPropertyValue("ChuyenNganh2", ref _ChuyenNganh2, value); }
        }

        [ModelDefault("Caption", "Chuyên ngành 3")]
        public SiSoChuyenNganh ChuyenNganh3
        {
            get { return _ChuyenNganh3; }
            set { SetPropertyValue("ChuyenNganh3", ref _ChuyenNganh3, value); }
        }

        [ModelDefault("Caption", "Chuyên ngành 4")]
        public SiSoChuyenNganh ChuyenNganh4
        {
            get { return _ChuyenNganh4; }
            set { SetPropertyValue("ChuyenNganh4", ref _ChuyenNganh4, value); }
        }

        [ModelDefault("Caption", "Chuyên ngành 5")]
        public SiSoChuyenNganh ChuyenNganh5
        {
            get { return _ChuyenNganh5; }
            set { SetPropertyValue("ChuyenNganh5", ref _ChuyenNganh5, value); }
        }

        [ModelDefault("Caption", "Chuyên ngành 6")]
        public SiSoChuyenNganh ChuyenNganh6
        {
            get { return _ChuyenNganh6; }
            set { SetPropertyValue("ChuyenNganh6", ref _ChuyenNganh6, value); }
        }

        [ModelDefault("Caption", "Chuyên ngành 7")]
        public SiSoChuyenNganh ChuyenNganh7
        {
            get { return _ChuyenNganh7; }
            set { SetPropertyValue("ChuyenNganh7", ref _ChuyenNganh7, value); }
        }

        [ModelDefault("Caption", "Chuyên ngành 8")]
        public SiSoChuyenNganh ChuyenNganh8
        {
            get { return _ChuyenNganh8; }
            set { SetPropertyValue("ChuyenNganh8", ref _ChuyenNganh8, value); }
        }

        [ModelDefault("Caption", "Chuyên ngành 9")]
        public SiSoChuyenNganh ChuyenNganh9
        {
            get { return _ChuyenNganh9; }
            set { SetPropertyValue("ChuyenNganh9", ref _ChuyenNganh9, value); }
        }
        [ModelDefault("Caption", "Chuyên ngành 10")]
        public SiSoChuyenNganh ChuyenNganh10
        {
            get { return _ChuyenNganh10; }
            set { SetPropertyValue("ChuyenNganh10", ref _ChuyenNganh10, value); }
        }
        #endregion

        #region Chi tiết
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("Caption", "Tổng số học viên")]
        [ModelDefault("AllowEdit", "false")]
        [VisibleInDetailView(false)]
        public int TongSoHocVien
        {
            get { return _TongSoHocVien; }
            set { SetPropertyValue("TongSoHocVien", ref _TongSoHocVien, value); }
        }

        [ModelDefault("Caption", "Số TC")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [VisibleInDetailView(false)]
        public decimal SoTinChi
        {
            get { return _SoTinChi; }
            set { SetPropertyValue("SoTinChi", ref _SoTinChi, value); }
        }

        [ModelDefault("Caption", "Số tiết LT")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [VisibleInDetailView(false)]
        public decimal SoTietLyThuyet
        {
            get { return _SoTietLyThuyet; }
            set { SetPropertyValue("SoTietLyThuyet", ref _SoTietLyThuyet, value); }
        }

        [ModelDefault("Caption", "Hệ số giảng dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [VisibleInDetailView(false)]
        public decimal HeSo_GiangDay
        {
            get { return _HeSo_GiangDay; }
            set { SetPropertyValue("HeSo_GiangDay", ref _HeSo_GiangDay, value); }
        }

        [ModelDefault("Caption", "Hệ số lớp đông")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [VisibleInDetailView(false)]
        public decimal HeSo_LopDong
        {
            get { return _HeSo_LopDong; }
            set { SetPropertyValue("HeSo_LopDong", ref _HeSo_LopDong, value); }
        }


        [ModelDefault("Caption", "Hình thức thi")]
        public HinhThucThi HinhThucThi
        {
            get { return _HinhThucThi; }
            set { SetPropertyValue("HinhThucThi", ref _HinhThucThi, value); }
        }
        [ModelDefault("Caption", "Tổng giờ giảng dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [VisibleInDetailView(false)]
        public decimal TongGioGiangDay
        {
            get { return _TongGioGiangDay; }
            set { SetPropertyValue("TongGioGiangDay", ref _TongGioGiangDay, value); }
        }



        [ModelDefault("Caption", "Giờ quy đổi - ra đề")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoi_RaDe
        {
            get { return _GioQuyDoi_RaDe; }
            set { SetPropertyValue("GioQuyDoi_RaDe", ref _GioQuyDoi_RaDe, value); }
        }
        [ModelDefault("Caption", "Giờ quy đổi - Chấm GK")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoi_ChamBaiGiuaKy
        {
            get { return _GioQuyDoi_ChamBaiGiuaKy; }
            set { SetPropertyValue("GioQuyDoi_ChamBaiGiuaKy", ref _GioQuyDoi_ChamBaiGiuaKy, value); }
        }

        [ModelDefault("Caption", "Số học viên dự thi")]
        public int SoHocVien_DuThi
        {
            get { return _SoHocVien_DuThi; }
            set { SetPropertyValue("SoHocVien_DuThi", ref _SoHocVien_DuThi, value); }
        }



        [ModelDefault("Caption", "Giờ quy đổi - Chấm Tự luận")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoi_ChamTuLuan
        {
            get { return _GioQuyDoi_ChamTuLuan; }
            set { SetPropertyValue("GioQuyDoi_ChamTuLuan", ref _GioQuyDoi_ChamTuLuan, value); }
        }

        [ModelDefault("Caption", "Giờ quy đổi - Chấm tiểu luận")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoi_ChamTieuLuan
        {
            get { return _GioQuyDoi_ChamTieuLuan; }
            set { SetPropertyValue("GioQuyDoi_ChamTieuLuan", ref _GioQuyDoi_ChamTieuLuan, value); }
        }

        [ModelDefault("Caption", "Giờ quy đổi - Vấn đáp")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoi_VanDap
        {
            get { return _GioQuyDoi_VanDap; }
            set { SetPropertyValue("GioQuyDoi_VanDap", ref _GioQuyDoi_VanDap, value); }
        }
        [ModelDefault("Caption", "Số ca coi thi")]
        public int SoCaCoiThi
        {
            get { return _SoCaCoiThi; }
            set { SetPropertyValue("SoCaCoiThi", ref _SoCaCoiThi, value); }
        }

        [ModelDefault("Caption", "Giờ quy đổi - Coi thi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoi_CoiThi
        {
            get { return _GioQuyDoi_CoiThi; }
            set { SetPropertyValue("GioQuyDoi_CoiThi", ref _GioQuyDoi_CoiThi, value); }
        }

        [ModelDefault("Caption", "Số luận văn HD")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoLuanVanHuongDan
        {
            get { return _SoLuanVanHuongDan; }
            set { SetPropertyValue("SoLuanVanHuongDan", ref _SoLuanVanHuongDan, value); }
        }

        [ModelDefault("Caption", "Số giờ quy đổi - HDLV")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [VisibleInDetailView(false)]
        public decimal SoGioQuyDoiHuongDanLuanVan
        {
            get { return _SoGioQuyDoiHuongDanLuanVan; }
            set { SetPropertyValue("SoGioQuyDoiHuongDanLuanVan", ref _SoGioQuyDoiHuongDanLuanVan, value); }
        }


        [ModelDefault("Caption", "Tổng giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [VisibleInDetailView(false)]
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }

        #endregion

        #region key
        private QuanLySauDaiHoc _QuanLySauDaiHoc;
        [Association("QuanLySauDaiHoc-ListChiTietKhoiLuongSauDaiHoc")]
        [ModelDefault("Caption", "Quản lý")]
        [Browsable(false)]
        public QuanLySauDaiHoc QuanLySauDaiHoc
        {
            get
            {
                return _QuanLySauDaiHoc;
            }
            set
            {
                SetPropertyValue("QuanLySauDaiHoc", ref _QuanLySauDaiHoc, value);
                if (!IsLoading)
                    if (QuanLySauDaiHoc != null)
                    {
                        UpdateListMonHoc();
                        Load_HeSoChung();
                    }
                    else
                        ListMonHoc = new XPCollection<HocPhan_SauDaiHoc>(Session, false);

            }
        }
        #endregion

        [ModelDefault("Caption", "Ghi chú")]
        [ModelDefault("AllowEdit", "false")]
        [VisibleInDetailView(false)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }
        public ChiTietKhoiLuongSauDaiHoc(Session session) : base(session) { }
        void Load_HeSoChung()
        {
            CauHinhQuyDoiPMS CauHinh = Session.FindObject<CauHinhQuyDoiPMS>(CriteriaOperator.Parse("NamHoc =? and BacDaoTao =?", QuanLySauDaiHoc.NamHoc.Oid,QuanLySauDaiHoc.BacDaoTao.Oid));
            if (CauHinh != null)
            {
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}