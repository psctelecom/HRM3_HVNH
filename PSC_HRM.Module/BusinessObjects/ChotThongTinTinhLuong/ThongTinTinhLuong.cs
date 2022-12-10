using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module;
using System.Data.SqlClient;

namespace PSC_HRM.Module.ChotThongTinTinhLuong
{
    [ImageName("BO_TroCap")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Thông tin tính lương")]
    [Appearance("Hide_LuongNgachBac", TargetItems = "Huong85PhanTramLuong;NgachLuong;BacLuong;HeSoLuong", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoai=1 or PhanLoai=2")]
    [Appearance("Hide_LuongKhoan", TargetItems = "LuongKhoan", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoai=0 and MaTruong <> 'UEL'")]
    [Appearance("Hide_QNU", TargetItems = "PhanTramKhoiHC;BoPhanTinhLuong;HSPCKhoiHanhChinh;HSPCThamNienHC;PhanTramThamNienHC;KhongTinhTienAnTrua;KhongTinhSinhNhat;PhuCapTienXang;CongViecHienNay;KhongDongBHXH;KhongDongBHYT;KhongDongBHTN;PhanTramHuongLuong;KhongCuTru;HSChung;HSHoanThanhCV;HSPCGiangDay", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'QNU'")]
    [Appearance("Hide_NEU", TargetItems = "SoThangThanhToanTNQL;SoThangThanhToanDTCV;KhoanTienNgoaiGio;TienTroCap;KhoanPhuongTien;SoThangThanhToanPVDT;HeSoChucVuTangThem;HSLTangThem;PhuCapTienDocHai;HSPCTracNhiemTruong;HSPCPhucVuDaoTao;SoThangThanhToan;HSPCChucVuBaoLuu;HSPCKiemNhiem;HSPCKiemNhiemTrongTruong;HSChung;HSHoanThanhCV;PhuCapTienXang;HSPCChucVuBaoLuu", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'NEU'")]
    [Appearance("Hide_UEL", TargetItems = "PhanTramKhoiHC;BoPhanTinhLuong;KhoanTienNgoaiGio;TienTroCap;KhoanPhuongTien;HSPCKhoiHanhChinh;HSPCThamNienHC;PhanTramThamNienHC;SoThangThanhToanTNQL;SoThangThanhToanDTCV;SoThangThanhToanPVDT;HeSoChucVuTangThem;PhuCapTienDocHai;HSPCTracNhiemTruong;HSPCPhucVuDaoTao;SoThangThanhToan;HSChung;HSHoanThanhCV;HSPCGiangDay", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UEL'")]
    public class ThongTinTinhLuong : TruongBaseObject
    {
        //-------------------------Bảng chốt-------------------------
        private BangChotThongTinTinhLuong _BangChotThongTinTinhLuong;
        
        //----------------------ThongTinNhanVien---------------------
        private BoPhan _BoPhan;
        private BoPhan _BoMon;
        private BoPhan _BoPhanTinhLuong;
        private ThongTinNhanVien _ThongTinNhanVien;
        private TinhTrang _TinhTrang;
        private LoaiNhanSu _LoaiNhanSu;
        private LoaiNhanVien _LoaiNhanVien; //Loại hợp đồng - BUH
        private ChucVu _ChucVu;
        private ChucVu _ChucVuKiemNhiem;
        private ChucVuDang _ChucVuDang;
        private ChucVuDoan _ChucVuDoan;
        private ChucVuDoanThe _ChucVuDoanThe;
        private ChucDanh _ChucDanh;
        private bool _BangCapDaKiemDuyet;
        private CongViec _CongViecHienNay;
        private DateTime _NgayVaoCoQuan; //IUH - BUH
        private string _GhiChu;
        
        //---------------------TaiKhoanNganHang----------------------
        private NganHang _NganHang;
        private string _SoTaiKhoan;

        //----------------------NhanVienTrinhDo----------------------
        private TrinhDoChuyenMon _TrinhDoChuyenMon; //BUH
        private HocHam _HocHam; //BUH
        
        //------------------NhanVienThongTinLuong--------------------
        private TrangThaiThamGiaBaoHiemEnum _TrangThaiThamGiaBaoHiem;
        private bool _Huong85PhanTramLuong;
        private bool _TinhLuong;
        private bool _KhongDongBaoHiem;
        private bool _KhongThamGiaCongDoan;
        private bool _KhongTinhSinhNhat;
        private bool _KhongTinhTienAnTrua; //UEL
        
        private bool _KhongDongBHXH; //DLU
        private bool _KhongDongBHYT; //DLU
        private bool _KhongDongBHTN; //DLU
        private decimal _PhanTramHuongLuong; //GTVT
                
        //----------Tính thuế----------
        private int _SoNguoiPhuThuoc;
        private int _SoThangGiamTru;
        private bool _KhongCuTru;
        
        private ThongTinLuongEnum _PhanLoai;

        //-----------Lương khoán-----------
        private decimal _LuongKhoan;
        //HBU
        private int _PhuongThucTinhThue;
        private bool _TinhThueTNCNMacDinh;
        
        //-----------Lương ngạch bậc-----------
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuong;
        private decimal _HeSoLuong;
        //---Phụ cấp thâm niên vượt khung
        private int _VuotKhung;
        private decimal _HSPCVuotKhung;
        //---Phụ cấp chức vụ
        private decimal _HSPCChucVu;
        private decimal _HSPCChucVuDang; 
        private decimal _HSPCChucVuDoan; 
        private decimal _HSPCChucVuCongDoan; 
        //---Phụ cấp trách nhiệm - kiêm nhiệm
        private int _PhuCapKiemNhiem; //BUH
        private decimal _HSPCKiemNhiem; //IUH - BUH - DLU
        private decimal _HSPCTrachNhiem; //BUH
       
        //---Phụ cấp ưu đãi - độc hại
        private int _PhuCapUuDai;
        private decimal _HSPCUuDai;
        private decimal _HSPCDocHai;

        //---Phụ cấp khác
        private int _PhuCapKhac;
        private decimal _HSPCKhac;        

        private int _PhuCapThuHut;
        private decimal _PhuCapTienXang;
        private decimal _PhuCapDienThoai;
        private decimal _PhuCapTienAn;        
        private decimal _HSPCChuyenMon;
        private decimal _HSPCChucVuBaoLuu;
        private DateTime _NgayHetHanHuongHSPChucVuBaoLuu;

        //---Phụ cấp thâm niên nhà giáo
        private decimal _ThamNien;
        private decimal _HSPCThamNien;
        private DateTime _NgayHuongThamNien; //BUH
        private string _ThangNamTNCongTac; //Thâm niên công tác - IUH - BUH (Số tháng TNNG)
        private decimal _ThamNienCongTac; //Số năm công tác - LUH - UTE
        
        //-----------Thu nhập tăng thêm - Tăng cường độ lao động-----------
        private decimal _SoNamCongTac; //DLU //Số tháng TNCT - BUH
        private int _TiLeTangThem;
        private decimal _PhuCapTangThem;
        
        //----------Khác----------
        private int _STT; //UTE       
        //
        private DateTime _NgayHuongHSPCThamNien;
        private DateTime _NgayHuongHSPCChuyenMon;
        private DateTime _NgayHuongHSPCTracNhiem;
        //
        private bool _DuocHuongHSPCChuyenVien;
        //
        private decimal _HSPCKiemNhiemTrongTruong;
        private decimal _HSPCKhuVuc;
        private decimal _HSPCLuuDong;
        private decimal _HSPCDacThu;
        private decimal _PhanTramPhuCapDacBiet;
        private decimal _HSPCLanhDao;
        private decimal _KhoanNgay;
        private decimal _KhoanDem;
        private string _SoTheATM;
        private decimal _LuongGio;
        private string _SoSoBHXH;
        private decimal _HSPCThamNienTrongTruong;

        //QNU
        private decimal _HSLTangThem;
        private decimal _HeSoChucVuTangThem; 
        private decimal _PhuCapTienDocHai; 
        private decimal _HSPCPhucVuDaoTao;
        private decimal _HSPCTracNhiemTruong; 
        private decimal _SoThangThanhToan;
        private decimal _SoThangThanhToanTNQL;
        private decimal _SoThangThanhToanDTCV;
        private decimal _SoThangThanhToanPVDT;
        private decimal _KhoanTienNgoaiGio;
        private decimal _TienTroCap;
        private decimal _KhoanPhuongTien;

        //NEU
        private decimal _HSChung;
        private decimal _HSHoanThanhCV;
        private decimal _HSPCGiangDay;        
        private decimal _PhanTramThamNienHC;
        private decimal _HSPCThamNienHC;
        private decimal _PhanTramKhoiHC;
        private decimal _HSPCKhoiHanhChinh;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng chốt thông tin tính lương")]
        [Association("BangChotThongTinTinhLuong-ListThongTinTinhLuong")]
        public BangChotThongTinTinhLuong BangChotThongTinTinhLuong
        {
            get
            {
                return _BangChotThongTinTinhLuong;
            }
            set
            {
                SetPropertyValue("BangChotThongTinTinhLuong", ref _BangChotThongTinTinhLuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading && value != null)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ModelDefault("Caption", "Bộ môn")]
        public BoPhan BoMon
        {
            get
            {
                return _BoMon;
            }
            set
            {
                SetPropertyValue("BoMon", ref _BoMon, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị tính lương")]
        public BoPhan BoPhanTinhLuong
        {
            get
            {
                return _BoPhanTinhLuong;
            }
            set
            {
                SetPropertyValue("BoPhanTinhLuong", ref _BoPhanTinhLuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;

                    TinhTrang = value.TinhTrang;
                    if (TinhTrang.TenTinhTrang.ToLower().Contains("đang làm việc")
                        || TinhTrang.TenTinhTrang.ToLower().Contains("có hưởng lương"))
                        TrangThaiThamGiaBaoHiem = TrangThaiThamGiaBaoHiemEnum.DangThamGia;
                    else
                        TrangThaiThamGiaBaoHiem = TrangThaiThamGiaBaoHiemEnum.GiamTamThoi;
                    PhanLoai = value.NhanVienThongTinLuong.PhanLoai;
                    NgachLuong = value.NhanVienThongTinLuong.NgachLuong;
                    BacLuong = value.NhanVienThongTinLuong.BacLuong;
                    HeSoLuong = value.NhanVienThongTinLuong.HeSoLuong;
                    Huong85PhanTramLuong = value.NhanVienThongTinLuong.Huong85PhanTramLuong;
                    LuongKhoan = value.NhanVienThongTinLuong.LuongKhoan;
                    HSPCChucVu = value.NhanVienThongTinLuong.HSPCChucVu;
                    HSPCDocHai = value.NhanVienThongTinLuong.HSPCDocHai;
                    HSPCTrachNhiem = value.NhanVienThongTinLuong.HSPCTrachNhiem;
                    HSPCKiemNhiem = value.NhanVienThongTinLuong.HSPCKiemNhiem;
                    PhuCapKhac = value.NhanVienThongTinLuong.PhuCapKhac;
                    HSPCPhucVuDaoTao = value.NhanVienThongTinLuong.HSPCPhucVuDaoTao;
                    HSPCKhac = value.NhanVienThongTinLuong.HSPCKhac;
                    PhuCapUuDai = value.NhanVienThongTinLuong.PhuCapUuDai;
                    HSPCUuDai = value.NhanVienThongTinLuong.HSPCUuDai;
                    VuotKhung = value.NhanVienThongTinLuong.VuotKhung;
                    HSPCVuotKhung = value.NhanVienThongTinLuong.HSPCVuotKhung;
                    ThamNien = value.NhanVienThongTinLuong.ThamNien;
                    HSPCThamNien = value.NhanVienThongTinLuong.HSPCThamNien;
                    PhuCapThuHut = value.NhanVienThongTinLuong.PhuCapThuHut;
                    HSPCChucVuDang = value.NhanVienThongTinLuong.HSPCChucVuDang;
                    HSPCChucVuDoan = value.NhanVienThongTinLuong.HSPCChucVuDoan;
                    HSPCChucVuCongDoan = value.NhanVienThongTinLuong.HSPCChucVuCongDoan;
                    PhuCapDienThoai = value.NhanVienThongTinLuong.PhuCapDienThoai;
                    PhuCapTienXang = value.NhanVienThongTinLuong.PhuCapTienXang;
                    PhuCapTangThem = value.NhanVienThongTinLuong.PhuCapTangThem;                    
                    PhuCapTienDocHai = value.NhanVienThongTinLuong.PhuCapTienDocHai;
                    KhongCuTru = value.NhanVienThongTinLuong.KhongCuTru;
                    SoNguoiPhuThuoc = value.NhanVienThongTinLuong.SoNguoiPhuThuoc;
                    SoThangGiamTru = value.NhanVienThongTinLuong.SoThangGiamTru;
                    HSPCTracNhiemTruong = value.NhanVienThongTinLuong.HSPCTracNhiemTruong;
                    HSPCChucVuBaoLuu = value.NhanVienThongTinLuong.HSPCChucVuBaoLuu;
                
                    foreach (TaiKhoanNganHang item in value.ListTaiKhoanNganHang)
                    {
                        if (item.TaiKhoanChinh)
                        {
                            SoTaiKhoan = item.SoTaiKhoan;
                            NganHang = item.NganHang;
                            break;
                        }
                    }
                }
            }
        }

        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrang TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        [ModelDefault("Caption", "Bằng cấp đã kiểm duyệt")]
        public bool BangCapDaKiemDuyet
        {
            get
            {
                return _BangCapDaKiemDuyet;
            }
            set
            {
                SetPropertyValue("BangCapDaKiemDuyet", ref _BangCapDaKiemDuyet, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Trạng thái tham gia bảo hiểm")]
        public TrangThaiThamGiaBaoHiemEnum TrangThaiThamGiaBaoHiem
        {
            get
            {
                return _TrangThaiThamGiaBaoHiem;
            }
            set
            {
                SetPropertyValue("TrangThaiThamGiaBaoHiem", ref _TrangThaiThamGiaBaoHiem, value);
            }
        }

        [ModelDefault("Caption", "Không tham gia công đoàn")]
        public bool KhongThamGiaCongDoan
        {
            get
            {
                return _KhongThamGiaCongDoan;
            }
            set
            {
                SetPropertyValue("KhongThamGiaCongDoan", ref _KhongThamGiaCongDoan, value);
            }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        //[ModelDefault("AllowEdit", "False")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get
            {
                return _HocHam;
            }
            set
            {
                SetPropertyValue("HocHam", ref _HocHam, value);
            }
        }

        //--------------------------------------------------------------------------
        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại")]
        public ThongTinLuongEnum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
                if (!IsLoading)
                {
                    if (value == ThongTinLuongEnum.LuongHeSo)
                        LuongKhoan = 0;
                    else if (value == ThongTinLuongEnum.LuongKhoanCoBHXH
                        || value == ThongTinLuongEnum.LuongKhoanKhongBHXH)
                    {
                        NgachLuong = null;
                        BacLuong = null;
                        HeSoLuong = 0;
                    }
                }
            }
        }

        [ModelDefault("Caption", "Ngạch lương")]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
            }
        }

        [ModelDefault("Caption", "Bậc lương")]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
        public BacLuong BacLuong
        {
            get
            {
                return _BacLuong;
            }
            set
            {
                SetPropertyValue("BacLuong", ref _BacLuong, value);
                if (!IsLoading && value != null)
                {
                    HeSoLuong = value.HeSoLuong;
                }
            }
        }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
        public decimal HeSoLuong
        {
            get
            {
                return _HeSoLuong;
            }
            set
            {
                SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);
            }
        }

        [ModelDefault("Caption", "Hưởng 85%")]
        public bool Huong85PhanTramLuong
        {
            get
            {
                return _Huong85PhanTramLuong;
            }
            set
            {
                SetPropertyValue("Huong85PhanTramLuong", ref _Huong85PhanTramLuong, value);
            }
        }

        [ModelDefault("Caption", "Khoán tháng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongKhoan
        {
            get
            {
                return _LuongKhoan;
            }
            set
            {
                SetPropertyValue("LuongKhoan", ref _LuongKhoan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "HSPC chức vụ")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
        public decimal HSPCChucVu
        {
            get
            {
                return _HSPCChucVu;
            }
            set
            {
                SetPropertyValue("HSPCChucVu", ref _HSPCChucVu, value);
            }
        }

        [ModelDefault("Caption", "HSPC độc hại")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
        public decimal HSPCDocHai
        {
            get
            {
                return _HSPCDocHai;
            }
            set
            {
                SetPropertyValue("HSPCDocHai", ref _HSPCDocHai, value);
            }
        }

        [ModelDefault("Caption", "HSPC trách nhiệm")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
        public decimal HSPCTrachNhiem
        {
            get
            {
                return _HSPCTrachNhiem;
            }
            set
            {
                SetPropertyValue("HSPCTrachNhiem", ref _HSPCTrachNhiem, value);
            }
        }

        [ModelDefault("Caption", "HSPC kiêm nhiệm")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
        public decimal HSPCKiemNhiem
        {
            get
            {
                return _HSPCKiemNhiem;
            }
            set
            {
                SetPropertyValue("HSPCKiemNhiem", ref _HSPCKiemNhiem, value);
            }
        }
        
        [ModelDefault("Caption", "HSPC ưu đãi")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.SpinEditor")]
        public decimal HSPCUuDai
        {
            get
            {
                return _HSPCUuDai;
            }
            set
            {
                SetPropertyValue("HSPCUuDai", ref _HSPCUuDai, value);
            }
        }

        [ModelDefault("Caption", "HSPC khác")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
        public decimal HSPCKhac
        {
            get
            {
                return _HSPCKhac;
            }
            set
            {
                SetPropertyValue("HSPCKhac", ref _HSPCKhac, value);
            }
        }
        
        [ModelDefault("Caption", "HSPC phục vụ đào tạo")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSPCPhucVuDaoTao
        {
            get
            {
                return _HSPCPhucVuDaoTao;
            }
            set
            {
                SetPropertyValue("HSPCPhucVuDaoTao", ref _HSPCPhucVuDaoTao, value);
            }
        }

        [ModelDefault("Caption", "% PC ưu đãi")]
        public int PhuCapUuDai
        {
            get
            {
                return _PhuCapUuDai;
            }
            set
            {
                SetPropertyValue("PhuCapUuDai", ref _PhuCapUuDai, value);
            }
        }

        [ModelDefault("Caption", "HS Khác")]
        public int PhuCapKhac
        {
            get
            {
                return _PhuCapKhac;
            }
            set
            {
                SetPropertyValue("PhuCapKhac", ref _PhuCapKhac, value);
            }
        }

        [ModelDefault("Caption", "% vượt khung")]
        public int VuotKhung
        {
            get
            {
                return _VuotKhung;
            }
            set
            {
                SetPropertyValue("VuotKhung", ref _VuotKhung, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "HSPC vượt khung")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.SpinEditor")]
        public decimal HSPCVuotKhung
        {
            get
            {
                return _HSPCVuotKhung;
            }
            set
            {
                SetPropertyValue("HSPCVuotKhung", ref _HSPCVuotKhung, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "% thâm niên")]
        public decimal ThamNien
        {
            get
            {
                return _ThamNien;
            }
            set
            {
                SetPropertyValue("ThamNien", ref _ThamNien, value); 
            }
        }

        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.SpinEditor")]
        [ModelDefault("Caption", "HSPC Thâm niên")]
        public decimal HSPCThamNien
        {
            get
            {
                return _HSPCThamNien;
            }
            set
            {
                SetPropertyValue("HSPCThamNien", ref _HSPCThamNien, value);
            }
        }

        [ModelDefault("Caption", "Tính lương")]
        public bool TinhLuong
        {
            get
            {
                return _TinhLuong;
            }
            set
            {
                SetPropertyValue("TinhLuong", ref _TinhLuong, value);
                {

                }
            }
        }

        [ModelDefault("Caption", "Không đóng bảo hiểm")]
        public bool KhongDongBaoHiem
        {
            get
            {
                return _KhongDongBaoHiem;
            }
            set
            {
                SetPropertyValue("KhongDongBaoHiem", ref _KhongDongBaoHiem, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp thu hút")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int PhuCapThuHut
        {
            get
            {
                return _PhuCapThuHut;
            }
            set
            {
                SetPropertyValue("PhuCapThuHut", ref _PhuCapThuHut, value);
            }
        }

        [ModelDefault("Caption", "HSPC CV Đảng")]
        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        public decimal HSPCChucVuDang
        {
            get
            {
                return _HSPCChucVuDang;
            }
            set
            {
                SetPropertyValue("HSPCChucVuDang", ref _HSPCChucVuDang, value);
            }
        }

        [ModelDefault("Caption", "HSPC CV Đoàn")]
        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        public decimal HSPCChucVuDoan
        {
            get
            {
                return _HSPCChucVuDoan;
            }
            set
            {
                SetPropertyValue("HSPCChucVuDoan", ref _HSPCChucVuDoan, value);
            }
        }

        [ModelDefault("Caption", "HSPC CV Công đoàn")]
        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        public decimal HSPCChucVuCongDoan
        {
            get
            {
                return _HSPCChucVuCongDoan;
            }
            set
            {
                SetPropertyValue("HSPCChucVuCongDoan", ref _HSPCChucVuCongDoan, value);
            }
        }

        [ModelDefault("Caption", "PC điện thoại")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapDienThoai
        {
            get
            {
                return _PhuCapDienThoai;
            }
            set
            {
                SetPropertyValue("PhuCapDienThoai", ref _PhuCapDienThoai, value);
            }
        }

        [ModelDefault("Caption", "HSPC chuyên môn")]
        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        public decimal HSPCChuyenMon
        {
            get
            {
                return _HSPCChuyenMon;
            }
            set
            {
                SetPropertyValue("HSPCChuyenMon", ref _HSPCChuyenMon, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "HSPCCV bảo lưu")]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.SpinEditor")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
        public decimal HSPCChucVuBaoLuu
        {
            get
            {
                return _HSPCChucVuBaoLuu;
            }
            set
            {
                SetPropertyValue("HSPCChucVuBaoLuu", ref _HSPCChucVuBaoLuu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn hưởng HSPCCV bảo lưu")]
        public DateTime NgayHetHanHuongHSPChucVuBaoLuu
        {
            get
            {
                return _NgayHetHanHuongHSPChucVuBaoLuu;
            }
            set
            {
                SetPropertyValue("NgayHetHanHuongHSPChucVuBaoLuu", ref _NgayHetHanHuongHSPChucVuBaoLuu, value);
            }
        }

        [ModelDefault("Caption", "PC tiền ăn")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTienAn
        {
            get
            {
                return _PhuCapTienAn;
            }
            set
            {
                SetPropertyValue("PhuCapTienAn", ref _PhuCapTienAn, value);
            }
        }

        [ModelDefault("Caption", "Số lít xăng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTienXang
        {
            get
            {
                return _PhuCapTienXang;
            }
            set
            {
                SetPropertyValue("PhuCapTienXang", ref _PhuCapTienXang, value);
            }
        }

        [ModelDefault("Caption", "PC tiền độc hại")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTienDocHai
        {
            get
            {
                return _PhuCapTienDocHai;
            }
            set
            {
                SetPropertyValue("PhuCapTienDocHai", ref _PhuCapTienDocHai, value);
            }
        }

        [ModelDefault("Caption", "PC tăng thêm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTangThem
        {
            get
            {
                return _PhuCapTangThem;
            }
            set
            {
                SetPropertyValue("PhuCapTangThem", ref _PhuCapTangThem, value);
            }
        }

        [ModelDefault("Caption", "Tỉ lệ tăng thêm")]
        public int TiLeTangThem
        {
            get
            {
                return _TiLeTangThem;
            }
            set
            {
                SetPropertyValue("TiLeTangThem", ref _TiLeTangThem, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số năm thâm niên")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal ThamNienCongTac
        {
            get
            {
                return _ThamNienCongTac;
            }
            set
            {
                SetPropertyValue("ThamNienCongTac", ref _ThamNienCongTac, value);
            }
        }

        [ModelDefault("Caption", "HSPC trách nhiệm trường")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCTracNhiemTruong
        {
            get
            {
                return _HSPCTracNhiemTruong;
            }
            set
            {
                SetPropertyValue("HSPCTracNhiemTruong", ref _HSPCTracNhiemTruong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "% PC Kiêm Nhiệm")]
        public int PhuCapKiemNhiem
        {
            get
            {
                return _PhuCapKiemNhiem;
            }
            set
            {
                SetPropertyValue("PhuCapKiemNhiem", ref _PhuCapKiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "Loại hợp đồng")]
        public LoaiNhanVien LoaiNhanVien
        {
            get
            {
                return _LoaiNhanVien;
            }
            set
            {
                SetPropertyValue("LoaiNhanVien", ref _LoaiNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng thâm niên")]
        public DateTime NgayHuongThamNien
        {
            get
            {
                return _NgayHuongThamNien;
            }
            set
            {
                SetPropertyValue("NgayHuongThamNien", ref _NgayHuongThamNien, value);
            }
        }

        [ModelDefault("Caption", "Không cư trú")]
        public bool KhongCuTru
        {
            get
            {
                return _KhongCuTru;
            }
            set
            {
                SetPropertyValue("KhongCuTru", ref _KhongCuTru, value);
            }
        }

        [ModelDefault("Caption", "Số người phụ thuộc")]
        public int SoNguoiPhuThuoc
        {
            get
            {
                return _SoNguoiPhuThuoc;
            }
            set
            {
                SetPropertyValue("SoNguoiPhuThuoc", ref _SoNguoiPhuThuoc, value);
            }
        }

        [ModelDefault("Caption", "Số tháng giảm trừ")]
        public int SoThangGiamTru
        {
            get
            {
                return _SoThangGiamTru;
            }
            set
            {
                SetPropertyValue("SoThangGiamTru", ref _SoThangGiamTru, value);
            }
        }

        [ModelDefault("Caption", "Số tài khoản")]
        public string SoTaiKhoan
        {
            get
            {
                return _SoTaiKhoan;
            }
            set
            {
                SetPropertyValue("SoTaiKhoan", ref _SoTaiKhoan, value);
            }
        }

        [ModelDefault("Caption", "Ngân hàng")]
        public NganHang NganHang
        {
            get
            {
                return _NganHang;
            }
            set
            {
                SetPropertyValue("NganHang", ref _NganHang, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanh ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ kiêm nhiệm")]
        public ChucVu ChucVuKiemNhiem
        {
            get
            {
                return _ChucVuKiemNhiem;
            }
            set
            {
                SetPropertyValue("ChucVuKiemNhiem", ref _ChucVuKiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "Thâm niên công tác")]
        public string ThangNamTNCongTac
        {
            get
            {
                return _ThangNamTNCongTac;
            }
            set
            {
                SetPropertyValue("ThangNamTNCongTac", ref _ThangNamTNCongTac, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ Đảng")]
        public ChucVuDang ChucVuDang
        {
            get
            {
                return _ChucVuDang;
            }
            set
            {
                SetPropertyValue("ChucVuDang", ref _ChucVuDang, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ Đoàn")]
        public ChucVuDoan ChucVuDoan
        {
            get
            {
                return _ChucVuDoan;
            }
            set
            {
                SetPropertyValue("ChucVuDoan", ref _ChucVuDoan, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ Đoàn thể")]
        public ChucVuDoanThe ChucVuDoanThe
        {
            get
            {
                return _ChucVuDoanThe;
            }
            set
            {
                SetPropertyValue("ChucVuDoanThe", ref _ChucVuDoanThe, value);
            }
        }

        [ModelDefault("Caption", "Công việc hiện nay")]
        public CongViec CongViecHienNay
        {
            get
            {
                return _CongViecHienNay;
            }
            set
            {
                SetPropertyValue("CongViecHienNay", ref _CongViecHienNay, value);
            }
        }

        [ModelDefault("Caption", "Loại nhân sự")]
        public LoaiNhanSu LoaiNhanSu
        {
            get
            {
                return _LoaiNhanSu;
            }
            set
            {
                SetPropertyValue("LoaiNhanSu", ref _LoaiNhanSu, value);
            }
        }

        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        //Không dc browsble vi se khong sort theo stt duoc
        public int STT
        {
            get
            {
                return _STT;
            }
            set
            {
                SetPropertyValue("STT", ref _STT, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào cơ quan")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        //Không dc browsble vi se khong sort theo ngay vao co quan duoc
        public DateTime NgayVaoCoQuan
        {
            get
            {
                return _NgayVaoCoQuan;
            }
            set
            {
                SetPropertyValue("NgayVaoCoQuan", ref _NgayVaoCoQuan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tính thuế TNCN theo mặc định")]
        public bool TinhThueTNCNMacDinh
        {
            get
            {
                return _TinhThueTNCNMacDinh;
            }
            set
            {
                SetPropertyValue("TinhThueTNCNMacDinh", ref _TinhThueTNCNMacDinh, value);
            }
        }

        [ModelDefault("Caption", "Phương thức tính thuế")]
        public int PhuongThucTinhThue
        {
            get
            {
                return _PhuongThucTinhThue;
            }
            set
            {
                SetPropertyValue("PhuongThucTinhThue", ref _PhuongThucTinhThue, value);
            }
        }

        [ModelDefault("Caption", "Không đóng BHXH")]
        public bool KhongDongBHXH
        {
            get
            {
                return _KhongDongBHXH;
            }
            set
            {
                SetPropertyValue("KhongDongBHXH", ref _KhongDongBHXH, value);
            }
        }

        [ModelDefault("Caption", "Không đóng BHYT")]
        public bool KhongDongBHYT
        {
            get
            {
                return _KhongDongBHYT;
            }
            set
            {
                SetPropertyValue("KhongDongBHYT", ref _KhongDongBHYT, value);
            }
        }

        [ModelDefault("Caption", "Không đóng BHTN")]
        public bool KhongDongBHTN
        {
            get
            {
                return _KhongDongBHTN;
            }
            set
            {
                SetPropertyValue("KhongDongBHTN", ref _KhongDongBHTN, value);
            }
        }

        [ModelDefault("Caption", "Số năm công tác")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNamCongTac
        {
            get
            {
                return _SoNamCongTac;
            }
            set
            {
                SetPropertyValue("SoNamCongTac", ref _SoNamCongTac, value);
            }
        }

        [ModelDefault("Caption", "Không tính tiền ăn")]
        public bool KhongTinhTienAnTrua
        {
            get
            {
                return _KhongTinhTienAnTrua;
            }
            set
            {
                SetPropertyValue("KhongTinhTienAnTrua", ref _KhongTinhTienAnTrua, value);
            }
        }

        [ModelDefault("Caption", "Không tính sinh nhật")]
        public bool KhongTinhSinhNhat
        {
            get
            {
                return _KhongTinhSinhNhat;
            }
            set
            {
                SetPropertyValue("KhongTinhSinhNhat", ref _KhongTinhSinhNhat, value);
            }
        }

        [ModelDefault("Caption", "% hưởng lương")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhanTramHuongLuong
        {
            get
            {
                return _PhanTramHuongLuong;
            }
            set
            {
                SetPropertyValue("PhanTramHuongLuong", ref _PhanTramHuongLuong, value);
            }
        }


        [ModelDefault("Caption", "Ngày hưởng HSPC Thâm niên")]
        public DateTime NgayHuongHSPCThamNien
        {
            get
            {
                return _NgayHuongHSPCThamNien;
            }
            set
            {
                SetPropertyValue("NgayHuongHSPCThamNien", ref _NgayHuongHSPCThamNien, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng HSPC chuyên môn")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public DateTime NgayHuongHSPCChuyenMon
        {
            get
            {
                return _NgayHuongHSPCChuyenMon;
            }
            set
            {
                SetPropertyValue("NgayHuongHSPCChuyenMon", ref _NgayHuongHSPCChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng HSPC trách nhiệm")]       
        public DateTime NgayHuongHSPCTracNhiem
        {
            get
            {
                return _NgayHuongHSPCTracNhiem;
            }
            set
            {
                SetPropertyValue("NgayHuongHSPCTracNhiem", ref _NgayHuongHSPCTracNhiem, value);
            }
        }


        [ModelDefault("Caption", "Được hưởng HSPC chuyên viên")]     
        public bool DuocHuongHSPCChuyenVien
        {
            get
            {
                return _DuocHuongHSPCChuyenVien;
            }
            set
            {
                SetPropertyValue("DuocHuongHSPCChuyenVien", ref _DuocHuongHSPCChuyenVien, value);
            }
        }

        [ModelDefault("Caption", "HSPC kiêm nhiệm trong trường")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSPCKiemNhiemTrongTruong
        {
            get
            {
                return _HSPCKiemNhiemTrongTruong;
            }
            set
            {
                SetPropertyValue("HSPCKiemNhiemTrongTruong", ref _HSPCKiemNhiemTrongTruong, value);
            }
        }

        [ModelDefault("Caption", "HSPC khu vực")]
        public decimal HSPCKhuVuc
        {
            get
            {
                return _HSPCKhuVuc;
            }
            set
            {
                SetPropertyValue("HSPCKhuVuc", ref _HSPCKhuVuc, value);
            }
        }

        [ModelDefault("Caption", "HSPC lưu động")]
        public decimal HSPCLuuDong
        {
            get
            {
                return _HSPCLuuDong;
            }
            set
            {
                SetPropertyValue("HSPCLuuDong", ref _HSPCLuuDong, value);
            }
        }

        [ModelDefault("Caption", "HSPC đặc thù")]
        public decimal HSPCDacThu
        {
            get
            {
                return _HSPCDacThu;
            }
            set
            {
                SetPropertyValue("HSPCDacThu", ref _HSPCDacThu, value);
            }
        }

        [ModelDefault("Caption", "Phần trăm phụ cấp đặc biệt")]
        public decimal PhanTramPhuCapDacBiet
        {
            get
            {
                return _PhanTramPhuCapDacBiet;
            }
            set
            {
                SetPropertyValue("PhanTramPhuCapDacBiet", ref _PhanTramPhuCapDacBiet, value);
            }
        }

        [ModelDefault("Caption", "HSPC lãnh đạo")]
        public decimal HSPCLanhDao
        {
            get
            {
                return _HSPCLanhDao;
            }
            set
            {
                SetPropertyValue("HSPCLanhDao", ref _HSPCLanhDao, value);
            }
        }

        [ModelDefault("Caption", "Khoán ngày")]
        public decimal KhoanNgay
        {
            get
            {
                return _KhoanNgay;
            }
            set
            {
                SetPropertyValue("KhoanNgay", ref _KhoanNgay, value);
            }
        }

        [ModelDefault("Caption", "HS chung")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSChung
        {
            get
            {
                return _HSChung;
            }
            set
            {
                SetPropertyValue("HSChung", ref _HSChung, value);
            }
        }

        [ModelDefault("Caption", "HS hoàn thành CV")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSHoanThanhCV
        {
            get
            {
                return _HSHoanThanhCV;
            }
            set
            {
                SetPropertyValue("HSHoanThanhCV", ref _HSHoanThanhCV, value);
            }
        }

        [ModelDefault("Caption", "HSPC giảng dạy")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCGiangDay
        {
            get
            {
                return _HSPCGiangDay;
            }
            set
            {
                SetPropertyValue("HSPCGiangDay", ref _HSPCGiangDay, value);
            }
        }

        [ModelDefault("Caption", "Khoán đêm")]
        public decimal KhoanDem
        {
            get
            {
                return _KhoanDem;
            }
            set
            {
                SetPropertyValue("KhoanDem", ref _KhoanDem, value);
            }
        }

        [ModelDefault("Caption", "Lương giờ")]
        public decimal LuongGio
        {
            get
            {
                return _LuongGio;
            }
            set
            {
                SetPropertyValue("LuongGio", ref _LuongGio, value);
            }
        }
    
        [ModelDefault("Caption", "Số thẻ ATM")]
        public string SoTheATM
        {
            get
            {
                return _SoTheATM;
            }
            set
            {
                SetPropertyValue("SoTheATM", ref _SoTheATM, value);
            }
        }

        [ModelDefault("Caption", "Số sổ BHXH")]
        public string SoSoBHXH
        {
            get
            {
                return _SoSoBHXH;
            }
            set
            {
                SetPropertyValue("SoSoBHXH", ref _SoSoBHXH, value);
            }
        }

        [ModelDefault("Caption", "HSPC thâm niên trong trường")]
        public decimal HSPCThamNienTrongTruong
        {
            get
            {
                return _HSPCThamNienTrongTruong;
            }
            set
            {
                SetPropertyValue("HSPCThamNienTrongTruong", ref _HSPCThamNienTrongTruong, value);
            }
        }

        [ModelDefault("Caption", "HSL tăng thêm")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.SpinEditor")]
        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        public decimal HSLTangThem
        {
            get
            {
                return _HSLTangThem;
            }
            set
            {
                SetPropertyValue("HSLTangThem", ref _HSLTangThem, value);
            }
        }

        [ModelDefault("Caption", "HSCV tăng thêm")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.SpinEditor")]
        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        public decimal HeSoChucVuTangThem
        {
            get
            {
                return _HeSoChucVuTangThem;
            }
            set
            {
                SetPropertyValue("HeSoChucVuTangThem", ref _HeSoChucVuTangThem, value);
            }
        }

        [ModelDefault("Caption", "Số tháng thanh toán TNTT")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal SoThangThanhToan
        {
            get
            {
                return _SoThangThanhToan;
            }
            set
            {
                SetPropertyValue("SoThangThanhToan", ref _SoThangThanhToan, value);
            }
        }

        [ModelDefault("Caption", "Số tháng thanh toán PVDT")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal SoThangThanhToanPVDT
        {
            get
            {
                return _SoThangThanhToanPVDT;
            }
            set
            {
                SetPropertyValue("SoThangThanhToanPVDT", ref _SoThangThanhToanPVDT, value);
            }
        }

        [ModelDefault("Caption", "Số tháng thanh toán TNQL")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal SoThangThanhToanTNQL
        {
            get
            {
                return _SoThangThanhToanTNQL;
            }
            set
            {
                SetPropertyValue("SoThangThanhToanTNQL", ref _SoThangThanhToanTNQL, value);
            }
        }

        [ModelDefault("Caption", "Số tháng thanh toán DTCV")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal SoThangThanhToanDTCV
        {
            get
            {
                return _SoThangThanhToanDTCV;
            }
            set
            {
                SetPropertyValue("SoThangThanhToanDTCV", ref _SoThangThanhToanDTCV, value);
            }
        }      

        [ModelDefault("Caption", "% Thâm niên HC")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhanTramThamNienHC
        {
            get
            {
                return _PhanTramThamNienHC;
            }
            set
            {
                SetPropertyValue("PhanTramThamNienHC", ref _PhanTramThamNienHC, value);
            }
        }

        [ModelDefault("Caption", "HSPC Thâm niên HC")]
        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        public decimal HSPCThamNienHC
        {
            get
            {
                return _HSPCThamNienHC;
            }
            set
            {
                SetPropertyValue("HSPCThamNienHC", ref _HSPCThamNienHC, value);
            }
        }

        [ModelDefault("Caption", "% Khối HC")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhanTramKhoiHC
        {
            get
            {
                return _PhanTramKhoiHC;
            }
            set
            {
                SetPropertyValue("PhanTramKhoiHC", ref _PhanTramKhoiHC, value);
            }
        }

        [ModelDefault("Caption", "HSPC khối hành chính")]
        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        public decimal HSPCKhoiHanhChinh
        {
            get
            {
                return _HSPCKhoiHanhChinh;
            }
            set
            {
                SetPropertyValue("HSPCKhoiHanhChinh", ref _HSPCKhoiHanhChinh, value);
            }
        }

        [ModelDefault("Caption", "Tiền khoán ngoài giờ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal KhoanTienNgoaiGio
        {
            get
            {
                return _KhoanTienNgoaiGio;
            }
            set
            {
                SetPropertyValue("KhoanTienNgoaiGio", ref _KhoanTienNgoaiGio, value);
            }
        }

        [ModelDefault("Caption", "Tiền trợ cấp khác")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienTroCap
        {
            get
            {
                return _TienTroCap;
            }
            set
            {
                SetPropertyValue("TienTroCap", ref _TienTroCap, value);
            }
        }

        [ModelDefault("Caption", "Khoán phương tiện")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal KhoanPhuongTien
        {
            get
            {
                return _KhoanPhuongTien;
            }
            set
            {
                SetPropertyValue("KhoanPhuongTien", ref _KhoanPhuongTien, value);
            }
        }

        [NonPersistent]
        [Browsable(false)]
        private string MaTruong { get; set; }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //            
            MaTruong = TruongConfig.MaTruong; 
            //
            UpdateNhanVienList();
        }
        public ThongTinTinhLuong(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);

            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
        }
       
    }

}
