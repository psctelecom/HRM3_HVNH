using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.TaoMaQuanLy;
using System.Data.SqlClient;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.TuyenDung
{
    [ImageName("BO_Resume")]
    [DefaultProperty("HoTen")]
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Ứng viên")]
    public class UngVien : HoSo.HoSo
    {
        private DateTime _NgayDuTuyen;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private ChuyenMonDaoTao _ChuyenMonDaoTao;
        private TruongDaoTao _TruongDaoTao;
        private HinhThucDaoTao _HinhThucDaoTao;
        private int _NamTotNghiep;
        private ChuongTrinhHoc _ChuongTrinhHoc;
        private TrinhDoTinHoc _TrinhDoTinHoc;
        private NgoaiNgu _NgoaiNgu;
        private TrinhDoNgoaiNgu _TrinhDoNgoaiNgu;
        private bool _SinhVienGiuLaiTruong;
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private NhuCauTuyenDung _NhuCauTuyenDung;
        private string _CoQuanCu;
        private bool _ChuyenCongTac;
        private string _CMND_UngVien;
        private string _SoBaoDanh;
        private string _KinhNghiem;
        private int _ChieuCao;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        [Association("QuanLyTuyenDung-ListUngVien")]
        public QuanLyTuyenDung QuanLyTuyenDung
        {
            get
            {
                return _QuanLyTuyenDung;
            }
            set
            {
                SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "Số báo danh")]
        [ModelDefault("AllowEdit", "False")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string SoBaoDanh
        {
            get
            {
                return _SoBaoDanh;
            }
            set
            {
                SetPropertyValue("SoBaoDanh", ref _SoBaoDanh, value);
            }
        }

        [ModelDefault("Caption", "Ngày dự tuyển")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayDuTuyen
        {
            get
            {
                return _NgayDuTuyen;
            }
            set
            {
                SetPropertyValue("NgayDuTuyen", ref _NgayDuTuyen, value);
            }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        [ModelDefault("AllowEdit", "False")]
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

        [ModelDefault("Caption", "Sinh viên giữ lại trường")]
        public bool SinhVienGiuLaiTruong
        {
            get
            {
                return _SinhVienGiuLaiTruong;
            }
            set
            {
                SetPropertyValue("SinhVienGiuLaiTruong", ref _SinhVienGiuLaiTruong, value);
            }
        }

        [ModelDefault("Caption", "Cơ quan cũ")]
        public string CoQuanCu
        {
            get
            {
                return _CoQuanCu;
            }
            set
            {
                SetPropertyValue("CoQuanCu", ref _CoQuanCu, value);
            }
        }

        [ModelDefault("Caption", "Số chứng minh nhân dân")]
        public string CMND_UngVien
        {
            get
            {
                return _CMND_UngVien;
            }
            set
            {
                SetPropertyValue("CMND_UngVien", ref _CMND_UngVien, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Kinh nghiệm")]
        public string KinhNghiem
        {
            get
            {
                return _KinhNghiem;
            }
            set
            {
                SetPropertyValue("KinhNghiem", ref _KinhNghiem, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Vị trí ứng tuyển")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QuanLyTuyenDung.ListNhuCauTuyenDung")]
        public NhuCauTuyenDung NhuCauTuyenDung
        {
            get
            {
                return _NhuCauTuyenDung;
            }
            set
            {
                SetPropertyValue("NhuCauTuyenDung", ref _NhuCauTuyenDung, value);
                if (!IsLoading 
                    && value != null 
                    && QuanLyTuyenDung != null
                    && string.IsNullOrWhiteSpace(MaQuanLy) && !MaTruong.Equals("IUH"))
                {
                    if (NhuCauTuyenDung.ViTriTuyenDung.TenViTriTuyenDung.Contains("giảng viên"))
                    {
                        SoBaoDanh = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.SoBaoDanhGiangVien);
                    }
                    else
                    {
                        SoBaoDanh = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.SoBaoDanhNhanVien);
                    }
                }
            }
        }

        [Appearance("ChuyenCongTac_IUH", TargetItems = "ChuyenCongTac", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'LUH'")]
        [ModelDefault("Caption", "Chuyển công tác")]
        public bool ChuyenCongTac
        {
            get
            {
                return _ChuyenCongTac;
            }
            set
            {
                SetPropertyValue("ChuyenCongTac", ref _ChuyenCongTac, value);
            }
        }

        [ModelDefault("Caption", "Chiều cao (Cm)")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int ChieuCao
        {
            get
            {
                return _ChieuCao;
            }
            set
            {
                SetPropertyValue("ChieuCao", ref _ChieuCao, value);
            }
        }

        //[Browsable(false)]
        //[NonPersistent]
        //public string MaTruong
        //{
        //    get;
        //    set;
        //}

        public UngVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GioiTinh = GioiTinhEnum.Nam;
            //
            MaTruong = TruongConfig.MaTruong;
            //
            LoaiHoSo = LoaiHoSoEnum.UngVien;
        }
    }

}
