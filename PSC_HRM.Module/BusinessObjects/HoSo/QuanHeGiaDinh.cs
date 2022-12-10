using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.DoanDang;
namespace PSC_HRM.Module.HoSo
{
    [ImageName("BO_GiaDinh")]
    [DefaultProperty("HoTenNguoiThan")]
    [ModelDefault("Caption", "Quan hệ gia đình")]
    [Appearance("QuanHeGiaDinh", TargetItems = "NuocCuTru;NamDiCu", Enabled = false, Criteria = "PhanLoai=1")]

    [Appearance("Hide_GTVT", TargetItems = "PhanLoai;NuocCuTru;NamDiCu;QueQuanText;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'GTVT'")]
    [Appearance("Hide_BUH", TargetItems = "PhanLoai;NuocCuTru;NamDiCu;QueQuanText;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'BUH'")]
    [Appearance("Hide_IUH", TargetItems = "NoiSinh;CMND;SoHoChieu;MaSoThue;LoaiGiamTruGiaCanh;GiayKhaiSinh_So;GiayKhaiSinh_QuyenSo;GiayKhaiSinh_NoiDangKy;PhuThuoc;LienHeKhanCap;DienThoaiDiDong;NgaySinhFull;QueQuanText;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='IUH'")]
    [Appearance("Hide_UTE", TargetItems = "NoiSinh;CMND;SoHoChieu;MaSoThue;LoaiGiamTruGiaCanh;GiayKhaiSinh_So;GiayKhaiSinh_QuyenSo;GiayKhaiSinh_NoiDangKy;PhuThuoc;LienHeKhanCap;DienThoaiDiDong;NgaySinhFull;QueQuanText;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UTE'")]
    [Appearance("Hide_LUH", TargetItems = "NoiSinh;CMND;SoHoChieu;MaSoThue;LoaiGiamTruGiaCanh;GiayKhaiSinh_So;GiayKhaiSinh_QuyenSo;GiayKhaiSinh_NoiDangKy;PhuThuoc;LienHeKhanCap;DienThoaiDiDong;NgaySinhFull;QueQuanText;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'LUH'")]
    [Appearance("Hide_DLU", TargetItems = "NoiSinh;CMND;SoHoChieu;MaSoThue;LoaiGiamTruGiaCanh;GiayKhaiSinh_So;GiayKhaiSinh_QuyenSo;GiayKhaiSinh_NoiDangKy;PhuThuoc;LienHeKhanCap;DienThoaiDiDong;NgaySinhFull;QueQuanText;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'DLU'")]
    [Appearance("Hide_HBU", TargetItems = "NoiSinh;CMND;SoHoChieu;MaSoThue;LoaiGiamTruGiaCanh;GiayKhaiSinh_So;GiayKhaiSinh_QuyenSo;GiayKhaiSinh_NoiDangKy;PhuThuoc;NgaySinh;QueQuanText;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'HBU'")]
    [Appearance("Hide_QNU", TargetItems = "QueQuan;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'QNU'")]

    
    public class QuanHeGiaDinh : BaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private DangVien _DangVien;

        //----------------Bảng kê người phụ thuộc---------------- 
        private string _HoTenNguoiThan;
        private DateTime _NgaySinhFull;
        private GioiTinhEnum _GioiTinh;
        private string _NoiSinh;//
        private DanToc _DanToc;
        private QuocGia _QuocTich;
        private string _CMND;//
        private string _SoHoChieu;//

        private string _MaSoThue;//

        private QuanHe _QuanHe;
        private LoaiGiamTruGiaCanh _LoaiGiamTruGiaCanh;//

        //Thông tin trên giấy khai sinh của người phụ thuộc (Nếu người phụ thuộc không có MST, CMND và Hộ chiếu)
        private string _GiayKhaiSinh_So;//
        private string _GiayKhaiSinh_QuyenSo;//
        private DiaChi _GiayKhaiSinh_NoiDangKy;//

        private bool _PhuThuoc;//

        //--------------------------------------
        private int _NgaySinh;
        private TinhTrangEnum _TinhTrang;
        private TinhThanh _QueQuan;
        private string _QueQuanText;//QNU
        private TonGiao _TonGiao;
        
        private Trong_NgoaiNuocEnum _PhanLoai;
        private QuocGia _NuocCuTru;
        private int _NamDiCu;
        private string _NgheNghiepHienTai;
        private string _NoiLamViec;
        private string _DienThoaiDiDong;
        private bool _LienHeKhanCap;

        //------------------------------------------
        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("ThongTinNhanVien-ListQuanHeGiaDinh")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "DangVien")]
        [Association("DangVien-ListQuanHeGiaDinh")]
        public DangVien DangVien
        {
            get
            {
                return _DangVien;
            }
            set
            {
                SetPropertyValue("DangVien", ref _DangVien, value);
                if (value != null && !IsLoading)
                {
                    ThongTinNhanVien = value.ThongTinNhanVien;
                }
            }
        }

        [ModelDefault("Caption", "Họ tên người thân")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string HoTenNguoiThan
        {
            get
            {
                return _HoTenNguoiThan;
            }
            set
            {
                SetPropertyValue("HoTenNguoiThan", ref _HoTenNguoiThan, value);
            }
        }

        [ModelDefault("Caption", "Ngày sinh")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime NgaySinhFull
        {
            get
            {
                return _NgaySinhFull;
            }
            set
            {
                SetPropertyValue("NgaySinhFull", ref _NgaySinhFull, value);
            }
        }

        [ModelDefault("Caption", "Nơi sinh")]
        public string NoiSinh
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

        [ModelDefault("Caption", "Mã số thuế")]
        public string MaSoThue
        {
            get
            {
                return _MaSoThue;
            }
            set
            {
                SetPropertyValue("MaSoThue", ref _MaSoThue, value);
            }
        }

        [ModelDefault("Caption", "Quốc tịch")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuocGia QuocTich
        {
            get
            {
                return _QuocTich;
            }
            set
            {
                SetPropertyValue("QuocTich", ref _QuocTich, value);
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

        [ModelDefault("Caption", "Số hộ chiếu")]
        public string SoHoChieu
        {
            get
            {
                return _SoHoChieu;
            }
            set
            {
                SetPropertyValue("SoHoChieu", ref _SoHoChieu, value);
            }
        }

        [ModelDefault("Caption", "Quan hệ")]
        public QuanHe QuanHe
        {
            get
            {
                return _QuanHe;
            }
            set
            {
                SetPropertyValue("QuanHe", ref _QuanHe, value);
            }
        }

        [ModelDefault("Caption", "Tên gia cảnh")]
        [RuleRequiredField(DefaultContexts.Save,TargetCriteria="PhuThuoc = True")]
        public LoaiGiamTruGiaCanh LoaiGiamTruGiaCanh
        {
            get
            {
                return _LoaiGiamTruGiaCanh;
            }
            set
            {
                SetPropertyValue("LoaiGiamTruGiaCanh", ref _LoaiGiamTruGiaCanh, value);
            }
        }

        [ModelDefault("Caption", "Số (GKS)")]
        public string GiayKhaiSinh_So
        {
            get
            {
                return _GiayKhaiSinh_So;
            }
            set
            {
                SetPropertyValue("GiayKhaiSinh_So", ref _GiayKhaiSinh_So, value);
            }
        }

        [ModelDefault("Caption", "Quyển số (GKS)")]
        public string GiayKhaiSinh_QuyenSo
        {
            get
            {
                return _GiayKhaiSinh_QuyenSo;
            }
            set
            {
                SetPropertyValue("GiayKhaiSinh_So", ref _GiayKhaiSinh_QuyenSo, value);
            }
        }

        [ModelDefault("Caption", "Nơi đăng ký (GKS)")]
        public DiaChi GiayKhaiSinh_NoiDangKy
        {
            get
            {
                return _GiayKhaiSinh_NoiDangKy;
            }
            set
            {
                SetPropertyValue("GiayKhaiSinh_NoiDangKy", ref _GiayKhaiSinh_NoiDangKy, value);
            }
        }

        [ModelDefault("Caption", "Phụ thuộc")]
        public bool PhuThuoc
        {
            get
            {
                return _PhuThuoc;
            }
            set
            {
                SetPropertyValue("PhuThuoc", ref _PhuThuoc, value);
            }
        }

        [ModelDefault("Caption", "Liên hệ khẩn cấp")]
        public bool LienHeKhanCap
        {
            get
            {
                return _LienHeKhanCap;
            }
            set
            {
                SetPropertyValue("LienHeKhanCap", ref _LienHeKhanCap, value);
            }
        }

        [ModelDefault("Caption", "Điện thoại di động")]
        public string DienThoaiDiDong
        {
            get
            {
                return _DienThoaiDiDong;
            }
            set
            {
                SetPropertyValue("DienThoaiDiDong", ref _DienThoaiDiDong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại")]
        public Trong_NgoaiNuocEnum PhanLoai
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
                    if (PhanLoai == Trong_NgoaiNuocEnum.NgoaiNuoc)
                        QuocTich = null;
                    else
                        QuocTich = HamDungChung.GetCurrentQuocGia(Session);
                }

            }
        }

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
            }
        }

        [ModelDefault("Caption", "Năm sinh")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        public int NgaySinh
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
        
        [ModelDefault("Caption", "Quê quán")]
        public TinhThanh QueQuan
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

        [ModelDefault("Caption", "Quê quán ")]
        public string QueQuanText
        {
            get
            {
                return _QueQuanText;
            }
            set
            {
                SetPropertyValue("QueQuanText", ref _QueQuanText, value);
            }
        }

        [ModelDefault("Caption", "Nơi ở hiện nay")]
        public string NoiOHienNay { get; set; }

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

        [ModelDefault("Caption", "Nước cư trú")]
        public QuocGia NuocCuTru
        {
            get
            {
                return _NuocCuTru;
            }
            set
            {
                SetPropertyValue("NuocCuTru", ref _NuocCuTru, value);
            }
        }

        [ModelDefault("Caption", "Năm di cư")]
        public int NamDiCu
        {
            get
            {
                return _NamDiCu;
            }
            set
            {
                SetPropertyValue("NamDiCu", ref _NamDiCu, value);
            }
        }

        [ModelDefault("Caption", "Nghề nghiệp")]
        public string NgheNghiepHienTai
        {
            get
            {
                return _NgheNghiepHienTai;
            }
            set
            {
                SetPropertyValue("NgheNghiepHienTai", ref _NgheNghiepHienTai, value);
            }
        }

        [ModelDefault("Caption", "Nơi làm việc")]
        public string NoiLamViec
        {
            get
            {
                return _NoiLamViec;
            }
            set
            {
                SetPropertyValue("NoiLamViec", ref _NoiLamViec, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrangEnum TinhTrang
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

        [NonPersistent]
        [Browsable(false)]
        private string MaTruong { get; set; }

        public QuanHeGiaDinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;

            QuocTich = HamDungChung.GetCurrentQuocGia(Session);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            //Lấy mã trường hiện tại dùng để phân quyền
            MaTruong = TruongConfig.MaTruong;

        }

    }
}
