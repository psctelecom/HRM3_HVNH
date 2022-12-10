using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
namespace PSC_HRM.Module.HoSo
{
    [ImageName("BO_Resume")]
    [DefaultProperty("TrinhDoChuyenMon")]
    [ModelDefault("Caption", "Thông tin trình độ")]
    public class NhanVienTrinhDo : BaseObject
    {
        //Học hàm
        private HocHam _HocHam;
        private int _NamCongNhanHocHam;
        private DateTime _NgayCongNhanHocHam;
        //Học vị
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private ChuyenMonDaoTao _ChuyenMonDaoTao;
        private TruongDaoTao _TruongDaoTao;
        private HinhThucDaoTao _HinhThucDaoTao;
        private int _NamTotNghiep;
        private DateTime _NgayCapBang;
        //Trình độ văn hóa 12/12
        private TrinhDoVanHoa _TrinhDoVanHoa;
        //Đang theo học
        private ChuongTrinhHoc _ChuongTrinhHoc;
        private QuocGia _QuocGiaHoc;

        private TrinhDoTinHoc _TrinhDoTinHoc;
        private NgoaiNgu _NgoaiNgu;
        private TrinhDoNgoaiNgu _TrinhDoNgoaiNgu;

        private DanhHieuDuocPhong _DanhHieuCaoNhat;
        private DateTime _NgayPhongDanhHieu;
        private LyLuanChinhTri _LyLuanChinhTri;
        private QuanLyGiaoDuc _QuanLyGiaoDuc;
        private QuanLyNhaNuoc _QuanLyNhaNuoc;
        private QuanLyKinhTe _QuanLyKinhTe;
        private DateTime _NgayCongTac;
        private DateTime _NgayHuongCheDo;
        
        //GTVT
        private TrinhDoSuPham _TrinhDoSuPham;        

        [ModelDefault("Caption", "Trình độ văn hóa")]
        public TrinhDoVanHoa TrinhDoVanHoa
        {
            get
            {
                return _TrinhDoVanHoa;
            }
            set
            {
                SetPropertyValue("TrinhDoVanHoa", ref _TrinhDoVanHoa, value);
            }
        }

        [ImmediatePostData]
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

        //toán, lý, hóa, sinh, cntt,...
        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        [ModelDefault("AllowEdit", "False")]
        public ChuyenMonDaoTao ChuyenMonDaoTao
        {
            get
            {
                return _ChuyenMonDaoTao;
            }
            set
            {
                SetPropertyValue("ChuyenMonDaoTao", ref _ChuyenMonDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Trường đào tạo")]
        [ModelDefault("AllowEdit", "False")]
        public TruongDaoTao TruongDaoTao
        {
            get
            {
                return _TruongDaoTao;
            }
            set
            {
                SetPropertyValue("TruongDaoTao", ref _TruongDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Hình thức đào tạo")]
        [ModelDefault("AllowEdit", "False")]
        public HinhThucDaoTao HinhThucDaoTao
        {
            get
            {
                return _HinhThucDaoTao;
            }
            set
            {
                SetPropertyValue("HinhThucDaoTao", ref _HinhThucDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Năm tốt nghiệp")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamTotNghiep
        {
            get
            {
                return _NamTotNghiep;
            }
            set
            {
                SetPropertyValue("NamTotNghiep", ref _NamTotNghiep, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp bằng")]
        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayCapBang
        {
            get
            {
                return _NgayCapBang;
            }
            set
            {
                SetPropertyValue("NgayCapBang", ref _NgayCapBang, value);
            }
        }

        [ModelDefault("Caption", "Ngày công tác")]
        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayCongTac
        {
            get
            {
                return _NgayCongTac;
            }
            set
            {
                SetPropertyValue("NgayCongTac", ref _NgayCongTac, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng chế độ")]
        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayHuongCheDo
        {
            get
            {
                return _NgayHuongCheDo;
            }
            set
            {
                SetPropertyValue("NgayHuongCheDo", ref _NgayHuongCheDo, value);
            }
        }

        [ModelDefault("Caption", "Hiện đang theo học")]
        public ChuongTrinhHoc ChuongTrinhHoc
        {
            get
            {
                return _ChuongTrinhHoc;
            }
            set
            {
                SetPropertyValue("ChuongTrinhHoc", ref _ChuongTrinhHoc, value);
            }
        }

        [ModelDefault("Caption", "Quốc gia học")]
        public QuocGia QuocGiaHoc
        {
            get
            {
                return _QuocGiaHoc;
            }
            set
            {
                SetPropertyValue("QuocGiaHoc", ref _QuocGiaHoc, value);
            }
        }

        [ModelDefault("Caption", "Trình độ tin học")]
        public TrinhDoTinHoc TrinhDoTinHoc
        {
            get
            {
                return _TrinhDoTinHoc;
            }
            set
            {
                SetPropertyValue("TrinhDoTinHoc", ref _TrinhDoTinHoc, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
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

        [ModelDefault("AllowEdit", "False")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Học hàm (*)")]
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

        [ModelDefault("Caption", "Năm công nhận")]
        [ImmediatePostData]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamCongNhanHocHam
        {
            get
            {
                return _NamCongNhanHocHam;
            }
            set
            {
                SetPropertyValue("NamCongNhanHocHam", ref _NamCongNhanHocHam, value);
            }
        }

        [ModelDefault("Caption","Ngày công nhận")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayCongNhanHocHam
        {
            get
            {
                return _NgayCongNhanHocHam;
            }
            set
            {
                SetPropertyValue("NgayCongNhanHocHam", ref _NgayCongNhanHocHam, value);
            }
        }

        [ModelDefault("Caption", "Danh hiệu được phong")]
        public DanhHieuDuocPhong DanhHieuCaoNhat
        {
            get
            {
                return _DanhHieuCaoNhat;
            }
            set
            {
                SetPropertyValue("DanhHieuCaoNhat", ref _DanhHieuCaoNhat, value);
            }
        }

        [ModelDefault("Caption", "Ngày công nhận danh hiệu")]
        public DateTime NgayPhongDanhHieu
        {
            get
            {
                return _NgayPhongDanhHieu;
            }
            set
            {
                SetPropertyValue("NgayPhongDanhHieu", ref _NgayPhongDanhHieu, value);
                if (!IsLoading)
                {
                    NamCongNhanHocHam = value.Year;
                }
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

        [ModelDefault("Caption", "Quản lý giáo dục")]
        public QuanLyGiaoDuc QuanLyGiaoDuc
        {
            get
            {
                return _QuanLyGiaoDuc;
            }
            set
            {
                SetPropertyValue("QuanLyGiaoDuc", ref _QuanLyGiaoDuc, value);
            }
        }

        [ModelDefault("Caption", "Quản lý Nhà nước")]
        public QuanLyNhaNuoc QuanLyNhaNuoc
        {
            get
            {
                return _QuanLyNhaNuoc;
            }
            set
            {
                SetPropertyValue("QuanLyNhaNuoc", ref _QuanLyNhaNuoc, value);
            }
        }

        [ModelDefault("Caption", "Quản lý kinh tế")]
        public QuanLyKinhTe QuanLyKinhTe
        {
            get
            {
                return _QuanLyKinhTe;
            }
            set
            {
                SetPropertyValue("QuanLyKinhTe", ref _QuanLyKinhTe, value);
            }
        }

        [ModelDefault("Caption", "Trình độ sư phạm")]
        public TrinhDoSuPham TrinhDoSuPham
        {
            get
            {
                return _TrinhDoSuPham;
            }
            set
            {
                SetPropertyValue("TrinhDoSuPham", ref _TrinhDoSuPham, value);
            }
        }        

        public NhanVienTrinhDo(Session session) : base(session) { }
    }

}
