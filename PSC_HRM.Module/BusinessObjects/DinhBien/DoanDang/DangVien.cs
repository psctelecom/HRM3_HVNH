using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.DoanDang
{
    [DefaultClassOptions]
    [ImageName("BO_Dang")]
    [ModelDefault("Caption", "Quản lý Đảng viên")]
    [ModelDefault("EditorTypeName", "PSC_HRM.Module.Win.Editors.CustomCategorizedToChucDangListEditor")]
    [DefaultProperty("HoTen")]

    public class DangVien : TruongBaseObject, ICategorizedItem, IBoPhan
    {
        #region HoSoDanhVien
        //---------------------------Thông tin cơ bản-----------------------
        private string _Ho;
        private string _Ten;
        private string _TenGoiKhac;
        private GioiTinhEnum _GioiTinh;
        private DateTime _NgaySinh;
        private DiaChi _NoiSinh;
        //---CMND
        private string _CMND;
        private DateTime _NgayCap;
        private TinhThanh _NoiCap;
        private DiaChi _QueQuan;
        private DiaChi _DiaChiThuongTru;
        private DiaChi _NoiOHienNay;
        private DanToc _DanToc;
        private TonGiao _TonGiao;
        private string _GhiChu;
        
        //--------------------------Thành phần xuất thân----------------------
        private string _NgheNghiepKhiVaoDang;
        private string _CongViecChinhDangLam;
        private DateTime _NgayTuyenDungCongChuc;
        private string _DonViTuyenDungCongChuc;
        //Thành phần gia đình
        private ThanhPhanXuatThan _ThanhPhanGiaDinh;
        //Gia đình liệt sỹ / Gia Đình có công với CM
        private UuTienGiaDinh _UuTienGiaDinh;
        //Thương binh loại
        private UuTienBanThan _UuTienBanThan;
        private SucKhoe _TinhTrangSucKhoe;
        
        //-------------------------Trình độ chuyên môn------------------------
        private TrinhDoVanHoa _HocVanPhoThong;
        private TrinhDoChuyenMon _HocVi;
        private HocHam _HocHam;
        private NganhDaoTao _ChuyenMonNghiepVu;
        private LyLuanChinhTri _LyLuanChinhTri;
        private NgoaiNgu _NgoaiNgu;
        private TrinhDoNgoaiNgu _TrinhDoNgoaiNgu;
        //---Danh hiệu được phong
        private DanhHieuDuocPhong _DanhHieuDuocPhong;
        
        #region Thông tin cơ bản

        [Delayed]
        [ModelDefault("Caption", "Hình ảnh")]
        [Size(SizeAttribute.Unlimited)]
        [ValueConverter(typeof(ImageValueConverter))]
        public System.Drawing.Image HinhAnh
        {
            get
            {
                return GetDelayedPropertyValue<System.Drawing.Image>("HinhAnh");
            }
            set
            {
                if (value != null)
                    SetDelayedPropertyValue<System.Drawing.Image>("HinhAnh", new System.Drawing.Bitmap(value, new System.Drawing.Size(100, 120)));
                else
                    SetDelayedPropertyValue<System.Drawing.Image>("HinhAnh", value);

            }
        }
        
        [ImmediatePostData]
        [ModelDefault("Caption", "Họ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Ho
        {
            get
            {
                return _Ho;
            }
            set
            {
                SetPropertyValue("Ho", ref _Ho, value);
                OnChanged("HoTen");
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tên")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Ten
        {
            get
            {
                return _Ten;
            }
            set
            {
                SetPropertyValue("Ten", ref _Ten, value);
                OnChanged("HoTen");
            }
        }

        [Persistent]
        [ModelDefault("Caption", "Họ và tên")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string HoTen
        {
            get
            {
                return String.Format("{0} {1}", Ho, Ten);
            }
        }

        [ModelDefault("Caption", "Tên gọi khác")]
        public string TenGoiKhac
        {
            get
            {
                return _TenGoiKhac;
            }
            set
            {
                SetPropertyValue("TenGoiKhac", ref _TenGoiKhac, value);
            }
        }

        [ModelDefault("Caption", "Ngày sinh")]
        public DateTime NgaySinh
        {
            get
            {
                return _NgaySinh;
            }
            set
            {
                SetPropertyValue("NgaySinh", ref _NgaySinh, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Nơi sinh")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi NoiSinh
        {
            get
            {
                return _NoiSinh;
            }
            set
            {
                SetPropertyValue("NoiSinh", ref _NoiSinh, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Giới tính")]
        public GioiTinhEnum GioiTinh
        {
            get
            {
                return _GioiTinh;
            }
            set
            {
                SetPropertyValue("GioiTinh", ref _GioiTinh, value);
                if (!IsLoading)
                    AfterGioiTinhChanged();
            }
        }

        [ModelDefault("Caption", "Số CMND")]
        public string CMND
        {
            get
            {
                return _CMND;
            }
            set
            {
                SetPropertyValue("CMND", ref _CMND, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp")]
        public DateTime NgayCap
        {
            get
            {
                return _NgayCap;
            }
            set
            {
                SetPropertyValue("NgayCap", ref _NgayCap, value);
            }
        }

        [ModelDefault("Caption", "Nơi cấp")]
        public TinhThanh NoiCap
        {
            get
            {
                return _NoiCap;
            }
            set
            {
                SetPropertyValue("NoiCap", ref _NoiCap, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Quê quán")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi QueQuan
        {
            get
            {
                return _QueQuan;
            }
            set
            {
                SetPropertyValue("QueQuan", ref _QueQuan, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Địa chỉ thường trú")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi DiaChiThuongTru
        {
            get
            {
                return _DiaChiThuongTru;
            }
            set
            {
                SetPropertyValue("DiaChiThuongTru", ref _DiaChiThuongTru, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Nơi ở hiện nay")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi NoiOHienNay
        {
            get
            {
                return _NoiOHienNay;
            }
            set
            {
                SetPropertyValue("NoiOHienNay", ref _NoiOHienNay, value);
            }
        }
        
        [ModelDefault("Caption", "Dân tộc")]
        public DanToc DanToc
        {
            get
            {
                return _DanToc;
            }
            set
            {
                SetPropertyValue("DanToc", ref _DanToc, value);
            }
        }

        [ModelDefault("Caption", "Tôn giáo")]
        public TonGiao TonGiao
        {
            get
            {
                return _TonGiao;
            }
            set
            {
                SetPropertyValue("TonGiao", ref _TonGiao, value);
            }
        }

        [Size(300)]
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
        #endregion

        #region Thành phần xuất thân

        [ModelDefault("Caption", "Nghề nghiệp khi vào Đảng")]
        public string NgheNghiepKhiVaoDang
        {
            get
            {
                return _NgheNghiepKhiVaoDang;
            }
            set
            {
                SetPropertyValue("NgheNghiepKhiVaoDang", ref _NgheNghiepKhiVaoDang, value);
            }
        }

        [ModelDefault("Caption", "Công việc chính đang làm")]
        public string CongViecChinhDangLam
        {
            get
            {
                return _CongViecChinhDangLam;
            }
            set
            {
                SetPropertyValue("CongViecChinhDangLam", ref _CongViecChinhDangLam, value);
            }
        }
        
        [ModelDefault("Caption", "Ngày tuyển dụng CBCC")]
        public DateTime NgayTuyenDungCongChuc
        {
            get
            {
                return _NgayTuyenDungCongChuc;
            }
            set
            {
                SetPropertyValue("NgayTuyenDungCongChuc", ref _NgayTuyenDungCongChuc, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị tuyển dụng CBCC")]
        public string DonViTuyenDungCongChuc
        {
            get
            {
                return _DonViTuyenDungCongChuc;
            }
            set
            {
                SetPropertyValue("DonViTuyenDungCongChuc", ref _DonViTuyenDungCongChuc, value);
            }
        }

        [ModelDefault("Caption", "Thành phần xuất thân")]
        public ThanhPhanXuatThan ThanhPhanXuatThan
        {
            get
            {
                return _ThanhPhanGiaDinh;
            }
            set
            {
                SetPropertyValue("ThanhPhanXuatThan", ref _ThanhPhanGiaDinh, value);
            }
        }

        [ModelDefault("Caption", "Ưu tiên gia đình")]
        public UuTienGiaDinh UuTienGiaDinh
        {
            get
            {
                return _UuTienGiaDinh;
            }
            set
            {
                SetPropertyValue("UuTienGiaDinh", ref _UuTienGiaDinh, value);
            }
        }

        [ModelDefault("Caption", "Ưu tiên bản thân")]
        public UuTienBanThan UuTienBanThan
        {
            get
            {
                return _UuTienBanThan;
            }
            set
            {
                SetPropertyValue("UuTienBanThan", ref _UuTienBanThan, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng sức khỏe")]
        public SucKhoe TinhTrangSucKhoe
        {
            get
            {
                return _TinhTrangSucKhoe;
            }
            set
            {
                SetPropertyValue("TinhTrangSucKhoe", ref _TinhTrangSucKhoe, value);
            }
        }
        #endregion

        #region Trình độ chuyên môn
        [ModelDefault("Caption", "Học vấn phổ thông")]
        public TrinhDoVanHoa HocVanPhoThong
        {
            get
            {
                return _HocVanPhoThong;
            }
            set
            {
                SetPropertyValue("HocVanPhoThong", ref _HocVanPhoThong, value);
            }
        }

        [ModelDefault("Caption", "Học vị")]
        public TrinhDoChuyenMon HocVi
        {
            get
            {
                return _HocVi;
            }
            set
            {
                SetPropertyValue("HocVi", ref _HocVi, value);
            }
        }

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

        [ModelDefault("Caption", "Chuyên môn nghiệp vụ")]
        public NganhDaoTao ChuyenMonNghiepVu
        {
            get
            {
                return _ChuyenMonNghiepVu;
            }
            set
            {
                SetPropertyValue("ChuyenMonNghiepVu", ref _ChuyenMonNghiepVu, value);
            }
        }

        

        [ModelDefault("Caption", "Ngoại ngữ chính")]
        public NgoaiNgu NgoaiNgu
        {
            get
            {
                return _NgoaiNgu;
            }
            set
            {
                SetPropertyValue("NgoaiNgu", ref _NgoaiNgu, value);
            }
        }

        [ModelDefault("Caption", "Trình độ ngoại ngữ chính")]
        public TrinhDoNgoaiNgu TrinhDoNgoaiNgu
        {
            get
            {
                return _TrinhDoNgoaiNgu;
            }
            set
            {
                SetPropertyValue("TrinhDoNgoaiNgu", ref _TrinhDoNgoaiNgu, value);
            }
        }

        [ModelDefault("Caption", "Lý luận chính trị")]
        public LyLuanChinhTri LyLuanChinhTri
        {
            get
            {
                return _LyLuanChinhTri;
            }
            set
            {
                SetPropertyValue("LyLuanChinhTri", ref _LyLuanChinhTri, value);
            }
        }

        [ModelDefault("Caption", "Danh hiệu được phong")]
        public DanhHieuDuocPhong DanhHieuDuocPhong
        {
            get
            {
                return _DanhHieuDuocPhong;
            }
            set
            {
                SetPropertyValue("DanhHieuDuocPhong", ref _DanhHieuDuocPhong, value);
            }
        }
        #endregion
        
        
        #endregion

        #region DangVien
        //---------------------------Thông tin cơ bản------------------------------
        private ToChucDang _ToChucDang;
        private ToChucDang _ChiBoDang;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private ChucVuDang _ChucVuDang;
        private DateTime _NgayBoNhiem;
        private DateTime _NgayHetNhiemKy;
        //
        private string _SoLyLich;
        private string _SoTheDang;
        private DateTime _NgayCapThe;
        private string _NoiCapThe;
        
        
        //---------------------------Ngày vào Đảng----------------------------------
        //--Đoàn--
        private DateTime _NgayVaoDoan;
        private DateTime _NgayNhapNgu;
        private DateTime _NgayXuatNgu;
        //--Dự bị--
        private DateTime _NgayDuBi;
        private string _TaiChiBo1;
        private string _NguoiGioiThieu1;
        private string _DonVi1;
        private string _NguoiGioiThieu2;
        private string _DonVi2;
        //--Chính thức--
        private DateTime _NgayVaoDangChinhThuc;
        private string _TaiChiBo2;
        //--Quyết định chính thức
        private DateTime _NgayQuyetDinhChinhThuc;
        private string _DonVi3;
        //--Ngày được khôi phục đảng tịch
        private DateTime _NgayKhoiPhucDangTich;
        private string _ChiBo3;
        //Ngày được miễn công tác và SHĐ
        private DateTime _NgayMienSHD;

        //--Số Huy hiệu Đảng kiểu string
        private string _HuyHieu30Nam;
        private string _HuyHieu40Nam;
        private string _HuyHieu50Nam;
        private string _HuyHieu55Nam;
        private string _HuyHieu60Nam;
        private string _HuyHieu65Nam;
        private string _HuyHieu70Nam;
        private string _HuyHieu75Nam;
        private string _HuyHieu80Nam;
        private string _HuyHieu85Nam;
        private string _HuyHieu90Nam;
        
        //-------------------Hoàn cảnh kinh tế của bản thân và gia đình---------------
        private int _TongThuNhap;
        private int _ThuNhapBinhQuan;
        //Nhà ở
        private string _NhaDuocCap;
        private decimal _DienTichNhaDuocCap;
        private string _NhaTuMua;
        private decimal _DienTichNhaTuMua;
        //Đất ở
        private decimal _DienTichDatDuocCap;
        private decimal _DienTichDatTuMua;
        //Hoạt động kinh tế
        private string _HoatDongKinhTe;
        private decimal _DienTichTrangTrai;
        private int _SoLaoDong;
        //Tài sản có giá trị cao (50 triệu đồng trở lên)
        private string _TaiSanGiaTriCao;
        private int _TongGiaTriTaiSan;
        
        //--------------------Lịch sử bản thân----------------------
        //Bị bắt, bị tù (ngày, tháng, năm; chính quyền xử lý; hình thức xử lý; nơi thi hành)
        private string _TuDay;
        //Làm việc chế độ cũ
        private string _CheDoCu;
          

        /* ----------------Ngày vào Đảng không hiểu-------------
        private string _NgayVaoDangChinhThuc2;
        private string _NguoiGioiThieu21;
        private string _DonVi21;
        private string _ChiBo21;
        private string _NguoiGioiThieu22;
        private string _DonVi22;
        private string _NgayVaoDang2;
        private string _ChiBo22;

        [ModelDefault("Caption", "Ngày vào Đảng lần 2")]
        public string NgayVaoDang2
        {
            get
            {
                return _NgayVaoDang2;
            }
            set
            {
                SetPropertyValue("NgayVaoDang2", ref _NgayVaoDang2, value);
            }
        }

        [ModelDefault("Caption", "Chi bộ 21")]
        public string ChiBo21
        {
            get
            {
                return _ChiBo21;
            }
            set
            {
                SetPropertyValue("ChiBo21", ref _ChiBo21, value);
            }
        }

        [ModelDefault("Caption", "Người giới thiệu 21")]
        public string NguoiGioiThieu21
        {
            get
            {
                return _NguoiGioiThieu21;
            }
            set
            {
                SetPropertyValue("NguoiGioiThieu21", ref _NguoiGioiThieu21, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ, đơn vị 21")]
        public string DonVi21
        {
            get
            {
                return _DonVi21;
            }
            set
            {
                SetPropertyValue("DonVi21", ref _DonVi21, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào Đảng chính thức lần 2")]
        public string NgayVaoDangChinhThuc2
        {
            get
            {
                return _NgayVaoDangChinhThuc2;
            }
            set
            {
                SetPropertyValue("NgayVaoDangChinhThuc2", ref _NgayVaoDangChinhThuc2, value);
            }
        }

        [ModelDefault("Caption", "Chi bộ 22")]
        public string ChiBo22
        {
            get
            {
                return _ChiBo22;
            }
            set
            {
                SetPropertyValue("ChiBo22", ref _ChiBo22, value);
            }
        }

        [ModelDefault("Caption", "Người giới thiệu 22")]
        public string NguoiGioiThieu22
        {
            get
            {
                return _NguoiGioiThieu22;
            }
            set
            {
                SetPropertyValue("NguoiGioiThieu22", ref _NguoiGioiThieu22, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ, đơn vị 22")]
        public string DonVi22
        {
            get
            {
                return _DonVi22;
            }
            set
            {
                SetPropertyValue("DonVi22", ref _DonVi22, value);
            }
        }
        */

        /* ------------Tổ chức đảng nhiều cấp-------------------
        private string _DangBo;
        private string _DangBoTinh;
        private string _DangBoHuyen;
        private string _DangBoCoSo;
        private string _DangBoBoPhan;
        private string _ChiBo;

        [ModelDefault("Caption", "Đảng bộ")]
        public string DangBo
        {
            get
            {
                return _DangBo;
            }
            set
            {
                SetPropertyValue("DangBo", ref _DangBo, value);
            }
        }

        [ModelDefault("Caption", "Đảng bộ tỉnh")]
        public string DangBoTinh
        {
            get
            {
                return _DangBoTinh;
            }
            set
            {
                SetPropertyValue("DangBoTinh", ref _DangBoTinh, value);
            }
        }

        [ModelDefault("Caption", "Đảng bộ huyện")]
        public string DangBoHuyen
        {
            get
            {
                return _DangBoHuyen;
            }
            set
            {
                SetPropertyValue("DangBoHuyen", ref _DangBoHuyen, value);
            }
        }

        [ModelDefault("Caption", "Đảng bộ cơ sở")]
        public string DangBoCoSo
        {
            get
            {
                return _DangBoCoSo;
            }
            set
            {
                SetPropertyValue("DangBoCoSo", ref _DangBoCoSo, value);
            }
        }

        [ModelDefault("Caption", "Đảng bộ bộ phận")]
        public string DangBoBoPhan
        {
            get
            {
                return _DangBoBoPhan;
            }
            set
            {
                SetPropertyValue("DangBoBoPhan", ref _DangBoBoPhan, value);
            }
        }

        [ModelDefault("Caption", "Chi bộ")]
        public string ChiBo
        {
            get
            {
                return _ChiBo;
            }
            set
            {
                SetPropertyValue("ChiBo", ref _ChiBo, value);
            }
        }
        */


        #region Thông tin cơ bản
        [ModelDefault("Caption", "Tổ chức Đảng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Association("ToChucDang-DangVienList")]
        public ToChucDang ToChucDang
        {
            get
            {
                return _ToChucDang;
            }
            set
            {
                SetPropertyValue("ToChucDang", ref _ToChucDang, value);
            }
        }

        [ModelDefault("Caption", "Chi bộ Đảng")]
        public ToChucDang ChiBoDang
        {
            get
            {
                return _ChiBoDang;
            }
            set
            {
                SetPropertyValue("ChiBoDang", ref _ChiBoDang, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading && ThongTinNhanVien == null)
                {
                    UpdateNVList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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
                    if (BoPhan == null
                    || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;

                    //
                    AfterNhanVienChanged();
                }

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
                //chưa nên làm vì sau này nên quản lý dữ liệu theo nhiệm kỳ công tác
                //if (!IsLoading && ThongTinNhanVien != null)
                //{
                //    if (value != null && value.HSPCChucVu > ThongTinNhanVien.NhanVienThongTinLuong.HSPCLanhDao)
                //        ThongTinNhanVien.NhanVienThongTinLuong.HSPCLanhDao = value.HSPCChucVu;
                //    else
                //        ThongTinNhanVien.NhanVienThongTinLuong.HSPCLanhDao = 0;
                //}
                if (!IsLoading && ThongTinNhanVien != null)
                {
                    if (value != null && value.HSPCChucVuDang > 0)
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuDang = value.HSPCChucVuDang;
                }
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm")]
        public DateTime NgayBoNhiem
        {
            get
            {
                return _NgayBoNhiem;
            }
            set
            {
                SetPropertyValue("NgayBoNhiem", ref _NgayBoNhiem, value);
            }
        }
        [ModelDefault("Caption", "Ngày hết nhiệm kỳ")]
        public DateTime NgayHetNhiemKy
        {
            get
            {
                return _NgayHetNhiemKy;
            }
            set
            {
                SetPropertyValue("NgayHetNhiemKy", ref _NgayHetNhiemKy, value);
            }
        }
        [ModelDefault("Caption", "Số lý lịch")]
        public string SoLyLich
        {
            get
            {
                return _SoLyLich;
            }
            set
            {
                SetPropertyValue("SoLyLich", ref _SoLyLich, value);
            }
        }

        [ModelDefault("Caption", "Số thẻ Đảng")]
        public string SoTheDang
        {
            get
            {
                return _SoTheDang;
            }
            set
            {
                SetPropertyValue("SoTheDang", ref _SoTheDang, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp thẻ")]
        public DateTime NgayCapThe
        {
            get
            {
                return _NgayCapThe;
            }
            set
            {
                SetPropertyValue("NgayCapThe", ref _NgayCapThe, value);
            }
        }

        [ModelDefault("Caption", "Nơi cấp thẻ")]
        public string NoiCapThe
        {
            get
            {
                return _NoiCapThe;
            }
            set
            {
                SetPropertyValue("NoiCapThe", ref _NoiCapThe, value);
            }
        }
        #endregion

        #region Ngày vào Đảng
        [ModelDefault("Caption", "Ngày vào Đoàn")]
        public DateTime NgayVaoDoan
        {
            get
            {
                return _NgayVaoDoan;
            }
            set
            {
                SetPropertyValue("NgayVaoDoan", ref _NgayVaoDoan, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào nhập ngũ")]
        public DateTime NgayNhapNgu
        {
            get
            {
                return _NgayNhapNgu;
            }
            set
            {
                SetPropertyValue("NgayNhapNgu", ref _NgayNhapNgu, value);
            }
        }

        [ModelDefault("Caption", "Ngày xuất ngũ")]
        public DateTime NgayXuatNgu
        {
            get
            {
                return _NgayXuatNgu;
            }
            set
            {
                SetPropertyValue("NgayXuatNgu", ref _NgayXuatNgu, value);
            }
        }

        
        [ModelDefault("Caption", "Ngày vào Đảng")]
        public DateTime NgayDuBi
        {
            get
            {
                return _NgayDuBi;
            }
            set
            {
                SetPropertyValue("NgayDuBi", ref _NgayDuBi, value);
            }
        }

        [ModelDefault("Caption", "Tại chi bộ 1")]
        public string TaiChiBo1
        {
            get
            {
                return _TaiChiBo1;
            }
            set
            {
                SetPropertyValue("TaiChiBo1", ref _TaiChiBo1, value);
            }
        }

        [ModelDefault("Caption", "Người giới thiệu 1")]
        public string NguoiGioiThieu1
        {
            get
            {
                return _NguoiGioiThieu1;
            }
            set
            {
                SetPropertyValue("NguoiGioiThieu1", ref _NguoiGioiThieu1, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ, đơn vị 1")]
        public string DonVi1
        {
            get
            {
                return _DonVi1;
            }
            set
            {
                SetPropertyValue("DonVi1", ref _DonVi1, value);
            }
        }

        [ModelDefault("Caption", "Người giới thiệu 2")]
        public string NguoiGioiThieu2
        {
            get
            {
                return _NguoiGioiThieu2;
            }
            set
            {
                SetPropertyValue("NguoiGioiThieu2", ref _NguoiGioiThieu2, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ, đơn vị 2")]
        public string DonVi2
        {
            get
            {
                return _DonVi2;
            }
            set
            {
                SetPropertyValue("DonVi2", ref _DonVi2, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào Đảng chính thức")]
        public DateTime NgayVaoDangChinhThuc
        {
            get
            {
                return _NgayVaoDangChinhThuc;
            }
            set
            {
                SetPropertyValue("NgayVaoDangChinhThuc", ref _NgayVaoDangChinhThuc, value);
            }
        }

        [ModelDefault("Caption", "Tại chi bộ 2")]
        public string TaiChiBo2
        {
            get
            {
                return _TaiChiBo2;
            }
            set
            {
                SetPropertyValue("TaiChiBo2", ref _TaiChiBo2, value);
            }
        }

        [ModelDefault("Caption", "Ngày quyết định chính thức")]
        public DateTime NgayQuyetDinhChinhThuc
        {
            get
            {
                return _NgayQuyetDinhChinhThuc;
            }
            set
            {
                SetPropertyValue("NgayQuyetDinhChinhThuc", ref _NgayQuyetDinhChinhThuc, value);
            }
        }

        [ModelDefault("Caption", "Tại chi bộ 3")]
        public string DonVi3
        {
            get
            {
                return _DonVi3;
            }
            set
            {
                SetPropertyValue("DonVi3", ref _DonVi3, value);
            }
        }

        [ModelDefault("Caption", "Ngày khôi phục Đảng tịch")]
        public DateTime NgayKhoiPhucDangTich
        {
            get
            {
                return _NgayKhoiPhucDangTich;
            }
            set
            {
                SetPropertyValue("NgayKhoiPhucDangTich", ref _NgayKhoiPhucDangTich, value);
            }
        }

        [ModelDefault("Caption", "Tại chi bộ 4")]
        public string ChiBo3
        {
            get
            {
                return _ChiBo3;
            }
            set
            {
                SetPropertyValue("ChiBo3", ref _ChiBo3, value);
            }
        }

        [ModelDefault("Caption", "Ngày miễn CT và SHĐ")]
        public DateTime NgayMienSHD
        {
            get
            {
                return _NgayMienSHD;
            }
            set
            {
                SetPropertyValue("NgayMienSHD", ref _NgayMienSHD, value);
            }
        }

        #endregion

        #region Số huy hiệu Đảng
        [ModelDefault("Caption", "Số huy hiệu Đảng 30 năm")]
        public string HuyHieu30Nam
        {
            get
            {
                return _HuyHieu30Nam;
            }
            set
            {
                SetPropertyValue("HuyHieu30Nam", ref _HuyHieu30Nam, value);
            }
        }

        [ModelDefault("Caption", "Số huy hiệu Đảng 40 năm")]
        public string HuyHieu40Nam
        {
            get
            {
                return _HuyHieu40Nam;
            }
            set
            {
                SetPropertyValue("HuyHieu40Nam", ref _HuyHieu40Nam, value);
            }
        }

        [ModelDefault("Caption", "Số huy hiệu Đảng 50 năm")]
        public string HuyHieu50Nam
        {
            get
            {
                return _HuyHieu50Nam;
            }
            set
            {
                SetPropertyValue("HuyHieu50Nam", ref _HuyHieu50Nam, value);
            }
        }

        [ModelDefault("Caption", "Số huy hiệu Đảng 55 năm")]
        public string HuyHieu55Nam
        {
            get
            {
                return _HuyHieu55Nam;
            }
            set
            {
                SetPropertyValue("HuyHieu50Nam", ref _HuyHieu55Nam, value);
            }
        }

        [ModelDefault("Caption", "Số huy hiệu Đảng 60 năm")]
        public string HuyHieu60Nam
        {
            get
            {
                return _HuyHieu60Nam;
            }
            set
            {
                SetPropertyValue("HuyHieu60Nam", ref _HuyHieu60Nam, value);
            }
        }

        [ModelDefault("Caption", "Số huy hiệu Đảng 65 năm")]
        public string HuyHieu65Nam
        {
            get
            {
                return _HuyHieu65Nam;
            }
            set
            {
                SetPropertyValue("HuyHieu65Nam", ref _HuyHieu65Nam, value);
            }
        }

        [ModelDefault("Caption", "Số huy hiệu Đảng 70 năm")]
        public string HuyHieu70Nam
        {
            get
            {
                return _HuyHieu70Nam;
            }
            set
            {
                SetPropertyValue("HuyHieu70Nam", ref _HuyHieu70Nam, value);
            }
        }

        [ModelDefault("Caption", "Số huy hiệu Đảng 75 năm")]
        public string HuyHieu75Nam
        {
            get
            {
                return _HuyHieu75Nam;
            }
            set
            {
                SetPropertyValue("HuyHieu75Nam", ref _HuyHieu75Nam, value);
            }
        }

        [ModelDefault("Caption", "Số huy hiệu Đảng 80 năm")]
        public string HuyHieu80Nam
        {
            get
            {
                return _HuyHieu80Nam;
            }
            set
            {
                SetPropertyValue("HuyHieu80Nam", ref _HuyHieu80Nam, value);
            }
        }

        [ModelDefault("Caption", "Số huy hiệu Đảng 85 năm")]
        public string HuyHieu85Nam
        {
            get
            {
                return _HuyHieu85Nam;
            }
            set
            {
                SetPropertyValue("HuyHieu85Nam", ref _HuyHieu85Nam, value);
            }
        }

        [ModelDefault("Caption", "Số huy hiệu Đảng 90 năm")]
        public string HuyHieu90Nam
        {
            get
            {
                return _HuyHieu90Nam;
            }
            set
            {
                SetPropertyValue("HuyHieu90Nam", ref _HuyHieu90Nam, value);
            }
        }
        #endregion

        #region lịch sử bản thân
        [Size(500)]
        [ModelDefault("Caption", "Tù đày")]
        public string TuDay
        {
            get
            {
                return _TuDay;
            }
            set
            {
                SetPropertyValue("TuDay", ref _TuDay, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Làm việc ở chế độ cũ")]
        public string CheDoCu
        {
            get
            {
                return _CheDoCu;
            }
            set
            {
                SetPropertyValue("CheDoCu", ref _CheDoCu, value);
            }
        }
        #endregion

        #region Hoàn cảnh kinh tế

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tổng thu nhập")]
        public int TongThuNhap
        {
            get
            {
                return _TongThuNhap;
            }
            set
            {
                SetPropertyValue("TongThuNhap", ref _TongThuNhap, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thu nhập bình quân")]
        public int ThuNhapBinhQuan
        {
            get
            {
                return _ThuNhapBinhQuan;
            }
            set
            {
                SetPropertyValue("ThuNhapBinhQuan", ref _ThuNhapBinhQuan, value);
            }
        }

        [ModelDefault("Caption", "Nhà được cấp")]
        public string NhaDuocCap
        {
            get
            {
                return _NhaDuocCap;
            }
            set
            {
                SetPropertyValue("NhaDuocCap", ref _NhaDuocCap, value);
            }
        }

        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("Caption", "Diện tích nhà được cấp")]
        public decimal DienTichNhaDuocCap
        {
            get
            {
                return _DienTichNhaDuocCap;
            }
            set
            {
                SetPropertyValue("DienTichNhaDuocCap", ref _DienTichNhaDuocCap, value);
            }
        }

        [ModelDefault("Caption", "Nhà tự mua")]
        public string NhaTuMua
        {
            get
            {
                return _NhaTuMua;
            }
            set
            {
                SetPropertyValue("NhaTuMua", ref _NhaTuMua, value);
            }
        }

        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("Caption", "Diện tích nhà tự mua")]
        public decimal DienTichNhaTuMua
        {
            get
            {
                return _DienTichNhaTuMua;
            }
            set
            {
                SetPropertyValue("DienTichNhaTuMua", ref _DienTichNhaTuMua, value);
            }
        }

        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("Caption", "Diện tích đất được cấp")]
        public decimal DienTichDatDuocCap
        {
            get
            {
                return _DienTichDatDuocCap;
            }
            set
            {
                SetPropertyValue("DienTichDatDuocCap", ref _DienTichDatDuocCap, value);
            }
        }
        
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("Caption", "Diện tích đất tự mua")]
        public decimal DienTichDatTuMua
        {
            get
            {
                return _DienTichDatTuMua;
            }
            set
            {
                SetPropertyValue("DienTichDatTuMua", ref _DienTichDatTuMua, value);
            }
        }

        [ModelDefault("Caption", "Hoạt động kinh tế")]
        public string HoatDongKinhTe
        {
            get
            {
                return _HoatDongKinhTe;
            }
            set
            {
                SetPropertyValue("HoatDongKinhTe", ref _HoatDongKinhTe, value);
            }
        }

        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("Caption", "Diện tích trang trại")]
        public decimal DienTichTrangTrai
        {
            get
            {
                return _DienTichTrangTrai;
            }
            set
            {
                SetPropertyValue("DienTichTrangTrai", ref _DienTichTrangTrai, value);
            }
        }

        [ModelDefault("Caption", "Số lao động thuê mướn")]
        public int SoLaoDong
        {
            get
            {
                return _SoLaoDong;
            }
            set
            {
                SetPropertyValue("SoLaoDong", ref _SoLaoDong, value);
            }
        }
        
        [Size(500)]
        [ModelDefault("Caption", "Tài sản giá trị lớn")]
        public string TaiSanGiaTriCao
        {
            get
            {
                return _TaiSanGiaTriCao;
            }
            set
            {
                SetPropertyValue("TaiSanGiaTriCao", ref _TaiSanGiaTriCao, value);
            }
        }

        [Size(500)]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Giá trị tài sản")]
        public int TongGiaTriTaiSan
        {
            get
            {
                return _TongGiaTriTaiSan;
            }
            set
            {
                SetPropertyValue("TongGiaTriTaiSan", ref _TongGiaTriTaiSan, value);
            }
        }
        #endregion


        [Aggregated]
        [Association("DangVien-ListQuanHeGiaDinh")]
        [ModelDefault("Caption", "Quan hệ gia đình")]
        public XPCollection<QuanHeGiaDinh> ListQuanHeGiaDinh //{ get; set; }
        {
            get
            {
                return GetCollection<QuanHeGiaDinh>("ListQuanHeGiaDinh");
            }
        }

        [Aggregated]
        [Association("DangVien-ListChucVuDangKiemNhiem")]
        [ModelDefault("Caption", "Chức vụ kiêm nhiệm")]
        public XPCollection<ChucVuDangKiemNhiem> ListChucVuDangKiemNhiem
        {
            get
            {
                return GetCollection<ChucVuDangKiemNhiem>("ListChucVuDangKiemNhiem");
            }
        }
        #endregion

        #region Tình trạng để theo dõi danh sách Đảng viên từng năm
        private TinhTrangDangVien _TinhTrangDangVien;
        
        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrangDangVien TinhTrangDangVien
        {
            get
            {
                return _TinhTrangDangVien;
            }
            set
            {
                SetPropertyValue("TinhTrangDangVien", ref _TinhTrangDangVien, value);
            }
        }

        private DateTime _NgayVaoToChucDanh;

        [ModelDefault("Caption", "Ngày vào TCĐ")]
        public DateTime NgayVaoToChucDanh
        {
            get
            {
                return _NgayVaoToChucDanh;
            }
            set
            {
                SetPropertyValue("NgayVaoToChucDanh", ref _NgayVaoToChucDanh, value);
            }
        }

        private DateTime _NgayRaKhoiToChucDanh;

        [ModelDefault("Caption", "Ngày ra khỏi TCĐ")]
        public DateTime NgayRaKhoiToChucDanh
        {
            get
            {
                return _NgayRaKhoiToChucDanh;
            }
            set
            {
                SetPropertyValue("NgayRaKhoiToChucDanh", ref _NgayRaKhoiToChucDanh, value);
            }
        }
        #endregion

        [NonPersistent]
        [Browsable(false)]
        public bool IsSave { get; set; }


        public DangVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //if (!String.IsNullOrEmpty(DangBo))
            //{
            //    NguoiSuDung user = HamDungChung.CurrentUser();
            //    if (user != null && user.ThongTinTruong != null)
            //        DangBo = "Đảng bộ " + user.ThongTinTruong.TenBoPhan;
            //}

            NoiSinh = new DiaChi(Session);
            QueQuan = new DiaChi(Session);
            DiaChiThuongTru = new DiaChi(Session);
            NoiOHienNay = new DiaChi(Session);
            GioiTinh = GioiTinhEnum.Nam;
            DanToc = Session.FindObject<DanToc>(CriteriaOperator.Parse("TenDanToc like ?", "%Kinh%"));
            TonGiao = Session.FindObject<TonGiao>(CriteriaOperator.Parse("TenTonGiao like ?", "Không"));
            TinhTrangDangVien = Session.FindObject<TinhTrangDangVien>(CriteriaOperator.Parse("TenTinhTrang like ?", "Kết nạp"));
            if (TinhTrangDangVien == null)
            {
                TinhTrangDangVien = new TinhTrangDangVien(Session) { TenTinhTrang = "Kết nạp", MaQuanLy = "KN", KhongThuocToChucDang = false };
            }
            UpdateNVList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            
            UpdateNVList();
            
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        ITreeNode ICategorizedItem.Category
        {
            get
            {
                return ToChucDang;
            }
            set
            {
                ToChucDang = (ToChucDang)value;
            }
        }

        [Browsable(false)]
        [NonPersistent()]
        public ToChucDang Category
        {
            get
            {
                return ToChucDang;
            }
            set
            {
                ToChucDang = value;
            }
        }

        protected override void OnDeleting()
        {
            //xóa check là đảng viên bên hồ sơ
            ThongTinNhanVien.LaDangVien = false;

            base.OnDeleting();
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                if (!(ThongTinNhanVien == null))
                //check là đảng viên bên hồ sơ
                ThongTinNhanVien.LaDangVien = true;
                ThongTinNhanVien.NgayVaoDangDuBi = NgayDuBi;
                ThongTinNhanVien.NgayVaoDangChinhThuc = NgayVaoDangChinhThuc;
                
            }
        }

        protected void AfterNhanVienChanged()
        {
            Ho = ThongTinNhanVien.Ho;
            Ten = ThongTinNhanVien.Ten;
            if (!String.IsNullOrEmpty(ThongTinNhanVien.TenGoiKhac))
                TenGoiKhac = ThongTinNhanVien.TenGoiKhac;
            GioiTinh = ThongTinNhanVien.GioiTinh;
            if (ThongTinNhanVien.NgaySinh != null)
                NgaySinh = ThongTinNhanVien.NgaySinh;
            //NoiSinh

            if (ThongTinNhanVien.NoiSinh != null)
            {
                if (ThongTinNhanVien.NoiSinh.QuocGia != null)
                    NoiSinh.QuocGia = ThongTinNhanVien.NoiSinh.QuocGia;
                if (ThongTinNhanVien.NoiSinh.TinhThanh != null)
                    NoiSinh.TinhThanh = ThongTinNhanVien.NoiSinh.TinhThanh;
                if (ThongTinNhanVien.NoiSinh.QuanHuyen != null)
                    NoiSinh.QuanHuyen = ThongTinNhanVien.NoiSinh.QuanHuyen;
                if (ThongTinNhanVien.NoiSinh.XaPhuong != null)
                    NoiSinh.XaPhuong = ThongTinNhanVien.NoiSinh.XaPhuong;
                if (ThongTinNhanVien.NoiSinh.SoNha != null)
                    NoiSinh.SoNha = ThongTinNhanVien.NoiSinh.SoNha;
            }
            //---CMND
            if (!String.IsNullOrEmpty(ThongTinNhanVien.CMND))
                CMND = ThongTinNhanVien.CMND;
            if (ThongTinNhanVien.NgayCap != null)
                NgayCap = ThongTinNhanVien.NgayCap;
            if (ThongTinNhanVien.NoiCap != null)
                NoiCap = ThongTinNhanVien.NoiCap;
            //QueQuan
            if (ThongTinNhanVien.QueQuan != null)
            {
                if (ThongTinNhanVien.QueQuan.QuocGia != null)
                    QueQuan.QuocGia = ThongTinNhanVien.QueQuan.QuocGia;
                if (ThongTinNhanVien.QueQuan.TinhThanh != null)
                    QueQuan.TinhThanh = ThongTinNhanVien.QueQuan.TinhThanh;
                if (ThongTinNhanVien.QueQuan.QuanHuyen != null)
                    QueQuan.QuanHuyen = ThongTinNhanVien.QueQuan.QuanHuyen;
                if (ThongTinNhanVien.QueQuan.XaPhuong != null)
                    QueQuan.XaPhuong = ThongTinNhanVien.QueQuan.XaPhuong;
                if (ThongTinNhanVien.QueQuan.SoNha != null)
                    QueQuan.SoNha = ThongTinNhanVien.QueQuan.SoNha;
            }
            //DiaChiThuongTru
            if (ThongTinNhanVien.DiaChiThuongTru != null)
            {
                if (ThongTinNhanVien.DiaChiThuongTru.QuocGia != null)
                    DiaChiThuongTru.QuocGia = ThongTinNhanVien.DiaChiThuongTru.QuocGia;
                if (ThongTinNhanVien.DiaChiThuongTru.TinhThanh != null)
                    DiaChiThuongTru.TinhThanh = ThongTinNhanVien.DiaChiThuongTru.TinhThanh;
                if (ThongTinNhanVien.DiaChiThuongTru.QuanHuyen != null)
                    DiaChiThuongTru.QuanHuyen = ThongTinNhanVien.DiaChiThuongTru.QuanHuyen;
                if (ThongTinNhanVien.DiaChiThuongTru.XaPhuong != null)
                    DiaChiThuongTru.XaPhuong = ThongTinNhanVien.DiaChiThuongTru.XaPhuong;
                if (ThongTinNhanVien.DiaChiThuongTru.SoNha != null)
                    DiaChiThuongTru.SoNha = ThongTinNhanVien.DiaChiThuongTru.SoNha;
            }
            //NoiOHienNay
            if (ThongTinNhanVien.NoiOHienNay != null)
            {
                if (ThongTinNhanVien.NoiOHienNay.QuocGia != null)
                    NoiOHienNay.QuocGia = ThongTinNhanVien.NoiOHienNay.QuocGia;
                if (ThongTinNhanVien.NoiOHienNay.TinhThanh != null)
                    NoiOHienNay.TinhThanh = ThongTinNhanVien.NoiOHienNay.TinhThanh;
                if (ThongTinNhanVien.NoiOHienNay.QuanHuyen != null)
                    NoiOHienNay.QuanHuyen = ThongTinNhanVien.NoiOHienNay.QuanHuyen;
                if (ThongTinNhanVien.NoiOHienNay.XaPhuong != null)
                    NoiOHienNay.XaPhuong = ThongTinNhanVien.NoiOHienNay.XaPhuong;
                if (ThongTinNhanVien.NoiOHienNay.SoNha != null)
                    NoiOHienNay.SoNha = ThongTinNhanVien.NoiOHienNay.SoNha;
            }

            if (ThongTinNhanVien.DanToc != null)
                DanToc = ThongTinNhanVien.DanToc;
            if (ThongTinNhanVien.TonGiao != null)
                TonGiao = ThongTinNhanVien.TonGiao;

            //--------------------------Thành phần xuất thân----------------------
            CongViecChinhDangLam = ThongTinNhanVien.ChucVu != null ?
                                    ThongTinNhanVien.ChucVu.TenChucVu :
                                        ThongTinNhanVien.ChucDanh != null ?
                                        ThongTinNhanVien.ChucDanh.TenChucDanh : "";
            DonViTuyenDungCongChuc = Session.GetObjectByKey<BoPhan>(HamDungChung.ThongTinTruong(Session).Oid).TenBoPhan;
            if (ThongTinNhanVien.NgayTuyenDung != null)
                NgayTuyenDungCongChuc = ThongTinNhanVien.NgayTuyenDung;
            //Thành phần gia đình
            if (ThongTinNhanVien.ThanhPhanXuatThan != null)
                ThanhPhanXuatThan = ThongTinNhanVien.ThanhPhanXuatThan;
            //Gia đình liệt sỹ / Gia Đình có công với CM
            if (ThongTinNhanVien.UuTienGiaDinh != null)
                UuTienGiaDinh = ThongTinNhanVien.UuTienGiaDinh;
            //Thương binh loại
            if (ThongTinNhanVien.UuTienBanThan != null)
                UuTienBanThan = ThongTinNhanVien.UuTienBanThan;
            if (ThongTinNhanVien.TinhTrangSucKhoe != null)
                TinhTrangSucKhoe = ThongTinNhanVien.TinhTrangSucKhoe;

            //-------------------------Trình độ chuyên môn------------------------
            if (ThongTinNhanVien.NhanVienTrinhDo.TrinhDoVanHoa != null)
                HocVanPhoThong = ThongTinNhanVien.NhanVienTrinhDo.TrinhDoVanHoa;
            if (ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null)
                HocVi = ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon;
            if (ThongTinNhanVien.NhanVienTrinhDo.HocHam != null)
                HocHam = ThongTinNhanVien.NhanVienTrinhDo.HocHam;
            if (ThongTinNhanVien.NhanVienTrinhDo.ChuyenMonDaoTao != null)
                ChuyenMonNghiepVu = ThongTinNhanVien.NhanVienTrinhDo.ChuyenMonDaoTao.NganhDaoTao;
            if (ThongTinNhanVien.NhanVienTrinhDo.LyLuanChinhTri != null)
                LyLuanChinhTri = ThongTinNhanVien.NhanVienTrinhDo.LyLuanChinhTri;
            if (ThongTinNhanVien.NhanVienTrinhDo.NgoaiNgu != null)
                NgoaiNgu = ThongTinNhanVien.NhanVienTrinhDo.NgoaiNgu;
            if (ThongTinNhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu != null)
                TrinhDoNgoaiNgu = ThongTinNhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu;
            //---Danh hiệu được phong
            if (ThongTinNhanVien.NhanVienTrinhDo.DanhHieuCaoNhat != null)
                DanhHieuDuocPhong = ThongTinNhanVien.NhanVienTrinhDo.DanhHieuCaoNhat;

            //-------------------------Lấy quan hệ gia đình -----------------------
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien.Oid=?", ThongTinNhanVien.Oid);
            XPCollection<QuanHeGiaDinh> dsQuanHeGiaDinh = new XPCollection<QuanHeGiaDinh>(Session);
            dsQuanHeGiaDinh.Criteria = filter;

            foreach (QuanHeGiaDinh item in dsQuanHeGiaDinh)
            {
                item.DangVien = this;
            }
        }

        protected void AfterGioiTinhChanged()
        {
            if (GioiTinh == GioiTinhEnum.Nam)
                HinhAnh = global::PSC_HRM.Module.Properties.Resources.male;
            else
                HinhAnh = global::PSC_HRM.Module.Properties.Resources.female;
        }

    }

}
