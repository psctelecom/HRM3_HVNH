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

    [Appearance("UFM_Hide", TargetItems = "TongGioA1;TongGioA2;SoTienThanhToanHDKhac;SoTienThanhToanVuotGio;"
                                            + "GioBaoLuu;GioBaoLuuNCKH;GioBaoLuuHDQL;"
                                            + "TongGiamGD;TongGiamNCKH;TongGiamHDQL;"
                                            + "TongDinhMucGDCuoi;TongDinhMucNCKHCuoi;TongDinhMucHDQLCuoi;"
                                            + "GioGDVuotThieu;GioNVKHVuotThieu;GioHDQLVuotThieu;"
                                            + "TongGioThucHienHDQL;GioHDQL;"
                                            + "TongTienThanhToanThueTNCN;GioChuanThanhToan;HeSoChucDanh;TongGioThucHienNCKH_UngDung;GioNCKH_UngDung;TongDinhMucNCKH_UngDungCuoi;GioNCKH_UngDungVuotThieu;",
                                     Visibility = ViewItemVisibility.Hide,
                                     Criteria = "BangChotThuLao.ThongTinTruong.TenVietTat = 'UFM'")]
    [Appearance("VHU_Hide", TargetItems = "TongGioA1;TongGioA2;SoTienThanhToanHDKhac;DaChiTien;",
                                     Visibility = ViewItemVisibility.Hide,
                                     Criteria = "BangChotThuLao.ThongTinTruong.TenVietTat = 'VHU'")]
    public class ThongTinBangChot_Moi : BaseObject,IBoPhan
    {
        #region key
        private BangChotThuLao _BangChotThuLao;
        [Association("BangChotThuLao-ListThongTinBangChot_Moi")]
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

        private decimal _TongGioDaTinhTien;
        private decimal _TongGioA1;
        private decimal _TongGioA2;
        private decimal _TongGio;

        private decimal _TongGio_TinhThuLao;
        private decimal _TongGioThucHienHDQL;
        private decimal _TongGioThucHienNCKH;
        private decimal _TongGioThucHienNCKH_UngDung;
        //Dịnh mức giờ chuẩn
        private decimal _GioNghiaVu;
        private decimal _GioNCKH;
        private decimal _GioHDQL;
        private decimal _GioNCKH_UngDung;

        private decimal _SoTienThanhToanHDKhac;
        private decimal _SoTienThanhToanVuotGio;

        private decimal _TongTienThanhToan;
        private decimal _TongTienThanhToanThueTNCN;

        //VHU
        //Bảo lưu năm trước
        private decimal _GioBaoLuu;
        private decimal _GioBaoLuuNCKH;
        private decimal _GioBaoLuuHDQL;
        //Tổng giảm
        private decimal _TongGiamGD;
        private decimal _TongGiamNCKH;
        private decimal _TongGiamHDQL;
        //Tổng định mức
        private decimal _TongDinhMucGDCuoi;
        private decimal _TongDinhMucNCKHCuoi;
        private decimal _TongDinhMucHDQLCuoi;
        private decimal _TongDinhMucNCKH_UngDungCuoi;
        //Vượt thiếu
        private decimal _GioGDVuotThieu;
        private decimal _GioNVKHVuotThieu;
        private decimal _GioHDQLVuotThieu;
        private decimal _GioNCKH_UngDungVuotThieu;
        //Gio chuẩn tahnh toán 
        private decimal _GioChuanThanhToan;
        private decimal _HeSoChucDanh;
        private decimal _DonGia;
        //
        private bool _DaChiTien;
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
        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }
        [ModelDefault("Caption", "Tổng thực hiện giảng dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGio_TinhThuLao
        {
            get { return _TongGio_TinhThuLao; }
            set { SetPropertyValue("TongGio_TinhThuLao", ref _TongGio_TinhThuLao, value); }
        }

        [ModelDefault("Caption", "Tổng thực hiện NCKH(ứng dụng)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioThucHienNCKH_UngDung
        {
            get { return _TongGioThucHienNCKH_UngDung; }
            set { SetPropertyValue("TongGioThucHienNCKH_UngDung", ref _TongGioThucHienNCKH_UngDung, value); }
        }

        [ModelDefault("Caption", "Tổng thực hiện NCKH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioThucHienNCKH
        {
            get { return _TongGioThucHienNCKH; }
            set { SetPropertyValue("TongGioThucHienNCKH", ref _TongGioThucHienNCKH, value); }
        }

        [ModelDefault("Caption", "Tổng thực hiện HDQL")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioThucHienHDQL
        {
            get { return _TongGioThucHienHDQL; }
            set { SetPropertyValue("TongGioThucHienHDQL", ref _TongGioThucHienHDQL, value); }
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
        [ModelDefault("Caption", "Bảo lưu giảng dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioBaoLuu
        {
            get { return _GioBaoLuu; }
            set { SetPropertyValue("GioBaoLuu", ref _GioBaoLuu, value); }
        }

        [ModelDefault("Caption", "Định mức NCKH(ứng dụng)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioNCKH_UngDung
        {
            get { return _GioNCKH_UngDung; }
            set { SetPropertyValue("GioNCKH_UngDung", ref _GioNCKH_UngDung, value); }
        }

        [ModelDefault("Caption", "Định mức NCKH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioNCKH
        {
            get { return _GioNCKH; }
            set { SetPropertyValue("GioNCKH", ref _GioNCKH, value); }
        }
        [ModelDefault("Caption", "Định mức HDQL")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioHDQL
        {
            get { return _GioHDQL; }
            set { SetPropertyValue("GioHDQL", ref _GioHDQL, value); }
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
        /// <summary>
        /// VHU
        /// </summary>
        [ModelDefault("Caption", "Bảo lưu NCKH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioBaoLuuNCKH
        {
            get { return _GioBaoLuuNCKH; }
            set { SetPropertyValue("GioBaoLuuNCKH", ref _GioBaoLuuNCKH, value); }
        }

        [ModelDefault("Caption", "Bảo lưu HDQL")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioBaoLuuHDQL
        {
            get { return _GioBaoLuuHDQL; }
            set { SetPropertyValue("GioBaoLuuHDQL", ref _GioBaoLuuHDQL, value); }
        }

        [ModelDefault("Caption", "Tổng giảm giảng dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGiamGD
        {
            get { return _TongGiamGD; }
            set { SetPropertyValue("TongGiamGD", ref _TongGiamGD, value); }
        }

        [ModelDefault("Caption", "Tổng giảm NCKH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGiamNCKH
        {
            get { return _TongGiamNCKH; }
            set { SetPropertyValue("TongGiamNCKH", ref _TongGiamNCKH, value); }
        }

        [ModelDefault("Caption", "Tổng giảm HDQL")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGiamHDQL
        {
            get { return _TongGiamHDQL; }
            set { SetPropertyValue("TongGiamHDQL", ref _TongGiamHDQL, value); }
        }

        [ModelDefault("Caption", "Tổng định mức GD")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongDinhMucGDCuoi
        {
            get { return _TongDinhMucGDCuoi; }
            set { SetPropertyValue("TongDinhMucGDCuoi", ref _TongDinhMucGDCuoi, value); }
        }

        [ModelDefault("Caption", "Tổng định mức NCKH(ứng dụng)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongDinhMucNCKH_UngDungCuoi
        {
            get { return _TongDinhMucNCKH_UngDungCuoi; }
            set { SetPropertyValue("TongDinhMucNCKH_UngDungCuoi", ref _TongDinhMucNCKH_UngDungCuoi, value); }
        }

        [ModelDefault("Caption", "Tổng định mức NCKH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongDinhMucNCKHCuoi
        {
            get { return _TongDinhMucNCKHCuoi; }
            set { SetPropertyValue("TongDinhMucNCKHCuoi", ref _TongDinhMucNCKHCuoi, value); }
        }

        [ModelDefault("Caption", "Tổng định mức HDQL")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongDinhMucHDQLCuoi
        {
            get { return _TongDinhMucHDQLCuoi; }
            set { SetPropertyValue("TongDinhMucHDQLCuoi", ref _TongDinhMucHDQLCuoi, value); }
        }

        [ModelDefault("Caption", "Giờ giảng dạy vượt thiếu")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioGDVuotThieu
        {
            get { return _GioGDVuotThieu; }
            set { SetPropertyValue("GioGDVuotThieu", ref _GioGDVuotThieu, value); }
        }

        [ModelDefault("Caption", "Giờ NCKH vượt thiếu(ứng dụng)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioNCKH_UngDungVuotThieu
        {
            get { return _GioNCKH_UngDungVuotThieu; }
            set { SetPropertyValue("GioNCKH_UngDungVuotThieu", ref _GioNCKH_UngDungVuotThieu, value); }
        }

        [ModelDefault("Caption", "Giờ NCKH vượt thiếu")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioNVKHVuotThieu
        {
            get { return _GioNVKHVuotThieu; }
            set { SetPropertyValue("GioNVKHVuotThieu", ref _GioNVKHVuotThieu, value); }
        }

        [ModelDefault("Caption", "Giờ HDQL vượt thiếu")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioHDQLVuotThieu
        {
            get { return _GioHDQLVuotThieu; }
            set { SetPropertyValue("GioHDQLVuotThieu", ref _GioHDQLVuotThieu, value); }
        }

        [ModelDefault("Caption", "Giờ chuẩn thanh toán")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioChuanThanhToan
        {
            get { return _GioChuanThanhToan; }
            set { SetPropertyValue("GioChuanThanhToan", ref _GioChuanThanhToan, value); }
        }

        [ModelDefault("Caption", "Hệ số chức danh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal HeSoChucDanh
        {
            get { return _HeSoChucDanh; }
            set { SetPropertyValue("HeSoChucDanh", ref _HeSoChucDanh, value); }
        }

        [ModelDefault("Caption", "Đơn Giá")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set { SetPropertyValue("DonGia", ref _DonGia, value); }
        }

        [ModelDefault("Caption", "Đã chi tiền")]
        [ModelDefault("AllowEdit", "false")]
        public bool DaChiTien
        {
            get { return _DaChiTien; }
            set { SetPropertyValue("DaChiTien", ref _DaChiTien, value); }
        }

        [ModelDefault("Caption", "Tổng giờ đã tính tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioDaTinhTien
        {
            get { return _TongGioDaTinhTien; }
            set { SetPropertyValue("TongGioDaTinhTien", ref _TongGioDaTinhTien, value); }
        }
        #endregion

        [Aggregated]
        [Association("ThongTinBangChot_Moi-ListChiTietBangChot")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiTietBangChotThuLaoGiangDay> ListChiTietBangChot
        {
            get
            {
                return GetCollection<ChiTietBangChotThuLaoGiangDay>("ListChiTietBangChot");
            }
        }
        public ThongTinBangChot_Moi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}