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


namespace PSC_HRM.Module.PMS.NghiepVu
{

    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Danh sách nhân viên")]
    //[Appearance("Hide_HeSo", TargetItems = "HeSoCoSo;HeSoLuong;HeSoMonMoi;HeSoGiangDayNgoaiGio", Visibility = ViewItemVisibility.Hide, Criteria = "KhoiLuongGiangDay.ThongTinTruong.TenVietTat <> 'QNU'")]

    [Appearance("ToMauTongGio_SoTien", TargetItems = "TongGio;SoTienThanhToanHDKhac;SoTienThanhToanVuotGio", FontColor = "Red")]
    [Appearance("ThongTinBangChot_Khoa", TargetItems = "*", Enabled = false, Criteria = "Khoa = 1")]
    [Appearance("ToMauTongTien", TargetItems = "TongTienThanhToan", BackColor = "Aquamarine", FontColor = "Red")]
    [Appearance("ToMauKhoa", TargetItems = "Khoa;NhanVien", BackColor = "yellow", FontColor = "Red",Criteria="Khoa")]

    [Appearance("Hide_HVNH", TargetItems = "TongGioA1;TongGioA2;SoTienThanhToanHDKhac;SoTienThanhToanVuotGio;SoTienThanhToanHDKhac;SoTienThanhToanVuotGio;TongTienThanhToan;TongTietKiemTra;TongTienThanhToanThueTNCN"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "BangChotThuLao.ThongTinTruong.TenVietTat = 'NHH'")]

    [Appearance("Hide_DNU", TargetItems = "SoTienThanhToanHDKhac;SoTienThanhToanVuotGio;SoTienThanhToanHDKhac;SoTienThanhToanVuotGio;TongTienThanhToan;TongTienThanhToanThueTNCN;TongTietKiemTra"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "BangChotThuLao.ThongTinTruong.TenVietTat = 'DNU'")]
    [Appearance("Hide_QNU", TargetItems = "TongTietKiemTra"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "BangChotThuLao.ThongTinTruong.TenVietTat = 'QNU'")]
    public class ThongTinBangChot : BaseObject,IBoPhan
    {
        #region key
        private BangChotThuLao _BangChotThuLao;
        [Association("BangChotThuLao-ListThongTinBangChot")]
        [ModelDefault("Caption", "Bảng chốt thông tin giảng dạy")]
        [Browsable(false)]
        public BangChotThuLao BangChotThuLao
        {
            get
            {
                return _BangChotThuLao;
            }
            set
            {
                SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value);
            }
        }

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format("{0} - {1}", NhanVien != null ? NhanVien.HoTen : "", BangChotThuLao != null ? BangChotThuLao.Caption : "");
            }
        }
        #endregion

        #region Khai báo

        #region KB Thông tin nhân viên
        private bool _Khoa;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private string _MaGiangVien;
        private HocHam _HocHam;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;//Học vị
        private ChucVu _ChucVu;
        private bool _KiemNhiem;
        private decimal _HeSo_ChucDanh;

        #endregion


        #region Kết quả
        private decimal _TongGioA1;
        private decimal _TongGioA2;
        private decimal _TongGio_ChamBai;

        private decimal _TongGio;
        private decimal _GioNghiaVu;

        private decimal _SoTienThanhToanHDKhac;
        private decimal _SoTienThanhToanVuotGio;

        private decimal _TongTienThanhToan;
        private decimal _TongTienThanhToanThueTNCN;

        //DNU
        private decimal _SoTietThamQuan;
        private decimal _SoTietDiHoc;
        private decimal _SoTietKiemNhiem;
        private decimal _ConNho_PhuTaTn_vv;
        private decimal _NghienCuuKhoaHoc;
        private decimal _SoTietKhac;
        private decimal _DinhMucGD;
        private decimal _SoTietDinhMuc;
        private decimal _SoTietHopDong;
        private decimal _SoTietPhuTroi;
        private decimal _TongTietThucHien;
        private decimal _TongTietKiemTra;
        private bool _KeKhai;
        #endregion
        #endregion

        #region NhanVien
        [ModelDefault("Caption", "Khóa")]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }
        [ModelDefault("Caption", "Bộ phận")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption", "Nhân viên")]
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
            }
        }
        [ModelDefault("Caption", "Mã giảng viên")]        
        public string MaGiangVien
        {
            get { return _MaGiangVien; }
            set { SetPropertyValue("MaGiangVien", ref _MaGiangVien, value); }
        }

        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get { return _HocHam; }
            set { SetPropertyValue("HocHam", ref _HocHam, value); }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]//Học vị
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get { return _TrinhDoChuyenMon; }
            set { SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value); }
        }
        [ModelDefault("Caption", "Chức vụ")]//Học vị
        public ChucVu ChucVu
        {
            get { return _ChucVu; }
            set { SetPropertyValue("ChucVu", ref _ChucVu, value); }
        }
        [ModelDefault("Caption", "Kiêm nhiệm")]
        [Browsable(false)]
        public bool KiemNhiem
        {
            get { return _KiemNhiem; }
            set { SetPropertyValue("KiemNhiem", ref _KiemNhiem, value); }
        }
        #endregion

        #region Kết quả
        [ModelDefault("Caption", "Hệ số chức danh ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public decimal HeSo_ChucDanh
        {
            get { return _HeSo_ChucDanh; }
            set { SetPropertyValue("HeSo_ChucDanh", ref _HeSo_ChucDanh, value); }
        }
        [ModelDefault("Caption", "Tổng giờ A1 (HĐ khác)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioA1
        {
            get { return _TongGioA1; }
            set { SetPropertyValue("TongGioA1", ref _TongGioA1, value); }
        }
        [ModelDefault("Caption", "Tổng giờ A2 (Giảng dạy)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioA2
        {
            get { return _TongGioA2; }
            set { SetPropertyValue("TongGioA2", ref _TongGioA2, value); }
        }
        [ModelDefault("Caption", "Tổng giờ chấm bài...")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGio_ChamBai
        {
            get { return _TongGio_ChamBai; }
            set { SetPropertyValue("TongGio_ChamBai", ref _TongGio_ChamBai, value); }
        }
        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }
        [ModelDefault("Caption", "Giờ nghĩa vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioNghiaVu
        {
            get { return _GioNghiaVu; }
            set { SetPropertyValue("GioNghiaVu", ref _GioNghiaVu, value); }
        }

        [ModelDefault("Caption", "Thanh toán (HD Khác)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal SoTienThanhToanHDKhac
        {
            get { return _SoTienThanhToanHDKhac; }
            set { SetPropertyValue("SoTienThanhToanHDKhac", ref _SoTienThanhToanHDKhac, value); }
        }

        [ModelDefault("Caption", "Thanh toán vượt giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal SoTienThanhToanVuotGio
        {
            get { return _SoTienThanhToanVuotGio; }
            set { SetPropertyValue("SoTienThanhToanVuotGio", ref _SoTienThanhToanVuotGio, value); }
        }

        [ModelDefault("Caption", "Tổng tiền thanh toán")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongTienThanhToan
        {
            get { return _TongTienThanhToan; }
            set { SetPropertyValue("TongTienThanhToan", ref _TongTienThanhToan, value); }
        }

        [ModelDefault("Caption", "Tổng tiền thanh toán (Thuế TNCN)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongTienThanhToanThueTNCN
        {
            get { return _TongTienThanhToanThueTNCN; }
            set { SetPropertyValue("TongTienThanhToanThueTNCN", ref _TongTienThanhToanThueTNCN, value); }
        }

        //DNU

        [ModelDefault("Caption", "Số tiết tham quan")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietThamQuan
        {
            get { return _SoTietThamQuan; }
            set { SetPropertyValue("SoTietThamQuan", ref _SoTietThamQuan, value); }
        }

        [ModelDefault("Caption", "Số tiết đi học")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietDiHoc
        {
            get { return _SoTietDiHoc; }
            set { SetPropertyValue("SoTietDiHoc", ref _SoTietDiHoc, value); }
        }

        [ModelDefault("Caption", "Số tiết kiêm nhiệm")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietKiemNhiem
        {
            get { return _SoTietKiemNhiem; }
            set { SetPropertyValue("SoTietKiemNhiem", ref _SoTietKiemNhiem, value); }
        }

        [ModelDefault("Caption", "Con nhỏ, phụ tá ...")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ConNho_PhuTaTn_vv
        {
            get { return _ConNho_PhuTaTn_vv; }
            set { SetPropertyValue("ConNho_PhuTaTn_vv", ref _ConNho_PhuTaTn_vv, value); }
        }

        [ModelDefault("Caption", "Số tiết khác")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietKhac
        {
            get { return _SoTietKhac; }
            set { SetPropertyValue("SoTietKhac", ref _SoTietKhac, value); }
        }

        [ModelDefault("Caption", "Số tiết kiểm tra")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongTietKiemTra
        {
            get { return _TongTietKiemTra; }
            set { SetPropertyValue("TongTietKiemTra", ref _TongTietKiemTra, value); }
        }

        [ModelDefault("Caption", "Định mức giờ giảng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DinhMucGD
        {
            get { return _DinhMucGD; }
            set { SetPropertyValue("DinhMucGD", ref _DinhMucGD, value); }
        }

        [ModelDefault("Caption", "Số tiết định mức")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietDinhMuc
        {
            get { return _SoTietDinhMuc; }
            set { SetPropertyValue("SoTietDinhMuc", ref _SoTietDinhMuc, value); }
        }

        [ModelDefault("Caption", "Nghiên cứu khoa học")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal NghienCuuKhoaHoc
        {
            get { return _NghienCuuKhoaHoc; }
            set { SetPropertyValue("NghienCuuKhoaHoc", ref _NghienCuuKhoaHoc, value); }
        }

        [ModelDefault("Caption", "Số tiết hợp đồng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietHopDong
        {
            get { return _SoTietHopDong; }
            set { SetPropertyValue("SoTietHopDong", ref _SoTietHopDong, value); }
        }

        [ModelDefault("Caption", "Số tiết phụ trội")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietPhuTroi
        {
            get { return _SoTietPhuTroi; }
            set { SetPropertyValue("SoTietPhuTroi", ref _SoTietPhuTroi, value); }
        }
        [ModelDefault("Caption", "Tổng tiết thực hiện")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongTietThucHien
        {
            get { return _TongTietThucHien; }
            set { SetPropertyValue("TongTietThucHien", ref _TongTietThucHien, value); }
        }

        [ModelDefault("Caption", "Kê khai")]
        [Browsable(false)]
        public bool KeKhai
        {
            get { return _KeKhai; }
            set { SetPropertyValue("KeKhai", ref _KeKhai, value); }
        }

        #endregion

        [Aggregated]
        [Association("ThongTinBangChot-ListChiTietBangChot")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiTietBangChotThuLaoGiangDay> ListChiTietBangChot
        {
            get
            {
                return GetCollection<ChiTietBangChotThuLaoGiangDay>("ListChiTietBangChot");
            }
        }
        public ThongTinBangChot(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}