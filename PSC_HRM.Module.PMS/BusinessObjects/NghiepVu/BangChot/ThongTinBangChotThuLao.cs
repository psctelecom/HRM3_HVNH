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

    [Appearance("ToMauTongGio_SoTien", TargetItems = "TongGio;TongGio_TinhThuLao;SoTienThanhToanHDKhac;SoTienThanhToanVuotGio", FontColor = "Red")]
    [Appearance("ThongTinBangChot_Khoa", TargetItems = "*", Enabled = false, Criteria = "Khoa = 1")]
    [Appearance("ToMauTongTien", TargetItems = "TongTienThanhToan", BackColor = "Aquamarine", FontColor = "Red")]
    [Appearance("ToMauKhoa", TargetItems = "Khoa;NhanVien", BackColor = "yellow", FontColor = "Red",Criteria="Khoa")]

    [Appearance("HUFLIT_Hide", TargetItems = "HeSo_ChucDanh;CamKetThuNhap",
                                        Visibility = ViewItemVisibility.Hide,
                                        Criteria = "BangChotThuLao.ThongTinTruong.MaQuanLy = 'HUFLIT'")]

    [Appearance("NEU_Hide", TargetItems = "KhongTinhTrongDot",
                                        Visibility = ViewItemVisibility.Hide,
                                        Criteria = "BangChotThuLao.ThongTinTruong.MaQuanLy = 'NEU'")]
    public class ThongTinBangChotThuLao : BaseObject,IBoPhan
    {
        #region key
        private BangChotThuLao _BangChotThuLao;
        [Association("BangChotThuLao-ListThongTinBangChotThuLao")]
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

        private BangChotThuLao_ThinhGiang _BangChotThuLao_ThinhGiang;
        [Association("BangChotThuLao_ThinhGiang-ListThongTinBangChotThuLao")]
        [ModelDefault("Caption", "Bảng chốt thông tin giảng dạy(thỉnh giảng)")]
        [Browsable(false)]
        public BangChotThuLao_ThinhGiang BangChotThuLao_ThinhGiang
        {
            get
            {
                return _BangChotThuLao_ThinhGiang;
            }
            set
            {
                SetPropertyValue("BangChotThuLao_ThinhGiang", ref _BangChotThuLao_ThinhGiang, value);
            }
        }

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format("{0} - {1} - {2}", NhanVien != null ? NhanVien.MaQuanLy : "",NhanVien != null ? NhanVien.HoTen : "", BangChotThuLao != null ? BangChotThuLao.Caption : "");
            }
        }
        #endregion

        #region Khai báo

        #region KB Thông tin nhân viên
        private bool _Khoa;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        
        #endregion

        #region Kết quả

        private decimal _TongGio;
        private decimal _GioNghiaVu;
        private decimal _HeSo_ChucDanh;
        private decimal _TongTienThanhToan;
        private decimal _TongTienThanhToanThueTNCN;
        /// <summary>
        /// 
        /// </summary>
        private bool _KhongTinhTrongDot;
        private bool _CamKetThuNhap;
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
        #endregion

        #region Kết quả
        
        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }
        
        [ModelDefault("Caption", "Định mức GD")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioNghiaVu
        {
            get { return _GioNghiaVu; }
            set { SetPropertyValue("GioNghiaVu", ref _GioNghiaVu, value); }
        }
        [ModelDefault("Caption", "Hệ số chức danh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal HeSo_ChucDanh
        {
            get { return _HeSo_ChucDanh; }
            set { SetPropertyValue("HeSo_ChucDanh", ref _HeSo_ChucDanh, value); }
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

        [ModelDefault("Caption", "Không tính(bảo lưu)")]
        [ModelDefault("AllowEdit", "false")]
        public bool KhongTinhTrongDot
        {
            get { return _KhongTinhTrongDot; }
            set { SetPropertyValue("KhongTinhTrongDot", ref _KhongTinhTrongDot, value); }
        }

        [ModelDefault("Caption", "Cam kết thu nhập")]
        [ModelDefault("AllowEdit", "false")]
        public bool CamKetThuNhap
        {
            get { return _CamKetThuNhap; }
            set { SetPropertyValue("CamKetThuNhap", ref _CamKetThuNhap, value); }
        }

        [Action(Caption = "Stick Không tính(bảo lưu)-cam kết thu nhập", ImageName = "Action_Import", SelectionDependencyType = MethodActionSelectionDependencyType.RequireMultipleObjects)]
        public void KhongTinhTrongDotNay()
        {
            KhongTinhTrongDot = !KhongTinhTrongDot;
            CamKetThuNhap = !CamKetThuNhap;
        }
        #endregion

        [Aggregated]
        [Association("ThongTinBangChotThuLao-ListChiTietBangChot")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiTietChotThuLao> ListChiTietBangChot
        {
            get
            {
                return GetCollection<ChiTietChotThuLao>("ListChiTietBangChot");
            }
        }
        public ThongTinBangChotThuLao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}