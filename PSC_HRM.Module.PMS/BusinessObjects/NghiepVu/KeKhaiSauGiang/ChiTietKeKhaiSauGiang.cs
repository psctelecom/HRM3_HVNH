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
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang
{
    [ModelDefault("Caption", "Chi tiết kê khai sau giảng")]
    [Appearance("ToMauTongGio", TargetItems = "SLChamThiHetHocPhan;SLThucTapNgheCLC;SLCDTN;SLKhoaLuanTN;SLCaCoiThi", BackColor = "Aquamarine", FontColor = "Red")]
    public class ChiTietKeKhaiSauGiang : BaseObject
    {
        #region key
        private QuanLyKeKhaiSauGiang _QuanLyKeKhaiSauGiang;
        [Association("QuanLyKeKhaiSauGiang-ListChiTietKeKhaiSauGiang")]
        [ModelDefault("Caption", "Quản lý")]
        [Browsable(false)]
        public QuanLyKeKhaiSauGiang QuanLyKeKhaiSauGiang
        {
            get
            {
                return _QuanLyKeKhaiSauGiang;
            }
            set
            {
                SetPropertyValue("QuanLyKeKhaiSauGiang", ref _QuanLyKeKhaiSauGiang, value);             
            }
        }
        #endregion
        #region Khai báo
        private NhanVien _NhanVien;
        private decimal _TongGio;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;
        #region Chấm thi
        private decimal _ChamThiHetHocPhan;
        private decimal _ChamThiHetHocPhanVanDap;
        private decimal _ChamThiHetHocPhanTieuLuan;
        private decimal _ChamThiTotNghiep;
        private decimal _ChamThiThucTapCLC;
        private decimal _ChamCDTN;
        private decimal _ChamBaoVeKLTN;
        private decimal _ChamBaoVeKLTNCLCTV;
        private decimal _ChamBaoVeKLTNCLCTA;
        #endregion
        #region Hướng dẫn
        private decimal _HDCLCThamQuanThucTe;
        private decimal _HDVietCDTNSVCuoiKhoa;
        private decimal _HDDeTaiLuanVanCaoHoc;
        private decimal _HDThucTeNgeNghiepCLC;
        private decimal _HDCDTN;
        private decimal _HDKLTN;
        private decimal _HDKLTNCLCTV;
        private decimal _HDKLTNCLCTA;
        private decimal _GiaiDapHeVLVH;
        private decimal _HeThongOnThiCuoiKhoa;
        private decimal _PhuDaoSinhVienNuocNgoai;
        #endregion
        #region Ôn thi cuối khóa
        private decimal _SoanDeThi1HocPhan;
        private decimal _BoSungNganHangCauHoi;
        private decimal _RaDeThiTotNghiep;
        private decimal _RaDeHetHocPhanSDH;
        private decimal _PhuTrachCaThi;
        #endregion
        #region KhaiBaoAn
        private int _SLChamThiHetHocPhan;
        private int _SLChamThiHetHocPhanVanDap;
        private int _SLChamThiHetHocPhanTieuLuan;
        private int _SLThucTapNgheCLC;
        private int _SLCDTN;
        private int _SLHDCDTN;
        private int _SLKhoaLuanTN;
        private int _SLKhoaLuanTNCLCTV;
        private int _SLKhoaLuanTNCLCTA;
        private int _SLHDKhoaLuanTN;
        private int _SLHDKhoaLuanTNCLCTV;
        private int _SLHDKhoaLuanTNCLCTA;
        private int _SLCaCoiThi;
        private int _SLHDThucTeNgeNghiepCLC;

        private decimal _SLChamThiTN;
        private decimal _SLHDSVThamQuanThucTe;
        private decimal _SLHDVietCDTN;
        private decimal _SLHDDeTaiLuanVan;
        private decimal _SLGiaiDapThacMac;
        private decimal _SLHeThongHoa_OnThi;
        private decimal _SLSoanDeThi;
        private decimal _SLBoSungNganHangCauHoi;
        private decimal _SLRaDeTotNghiep;
        private decimal _SLRaDeThiHetHocPhan;
        private decimal _SLPhuDaoSinhVienNuocNgoai;
        private string _GhiChu;

        #endregion
        #endregion

        #region Chi tiết khai báo
        [ModelDefault("Caption", "Giảng viên")]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }
        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get
            {
                return _BacDaoTao;
            }
            set
            {
                SetPropertyValue("BacDaoTao", ref _BacDaoTao, value);
            }
        }
        [ModelDefault("Caption", "Loại hình đào tạo")]
        public HeDaoTao HeDaoTao
        {
            get
            {
                return _HeDaoTao;
            }
            set
            {
                SetPropertyValue("HeDaoTao", ref _HeDaoTao, value);
            }
        }
        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGio
        {
            get
            {
                return _TongGio;
            }
            set
            {
                SetPropertyValue("TongGio", ref _TongGio, value);
            }
        }
        #region Chấm thi
        [ModelDefault("Caption", "Chấm thi hết học phần tự luận")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ChamThiHetHocPhan
        {
            get
            {
                return _ChamThiHetHocPhan;
            }
            set
            {
                SetPropertyValue("ChamThiHetHocPhan", ref _ChamThiHetHocPhan, value);
            }
        }

        [ModelDefault("Caption", "Chấm thi hết học phần vấn đáp")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ChamThiHetHocPhanVanDap
        {
            get
            {
                return _ChamThiHetHocPhanVanDap;
            }
            set
            {
                SetPropertyValue("ChamThiHetHocPhanVanDap", ref _ChamThiHetHocPhanVanDap, value);
            }
        }

        [ModelDefault("Caption", "Chấm thi hết học phần tiểu luận")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ChamThiHetHocPhanTieuLuan
        {
            get
            {
                return _ChamThiHetHocPhanTieuLuan;
            }
            set
            {
                SetPropertyValue("ChamThiHetHocPhanTieuLuan", ref _ChamThiHetHocPhanTieuLuan, value);
            }
        }
        [ModelDefault("Caption", "Chấm thi tốt nghiệp")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ChamThiTotNghiep
        {
            get
            {
                return _ChamThiTotNghiep;
            }
            set
            {
                SetPropertyValue("ChamThiTotNghiep", ref _ChamThiTotNghiep, value);
            }
        }
        [ModelDefault("Caption", "Chấm thực tập nghề nghiệp hệ CLC")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ChamThiThucTapCLC
        {
            get
            {
                return _ChamThiThucTapCLC;
            }
            set
            {
                SetPropertyValue("ChamThiThucTapCLC", ref _ChamThiThucTapCLC, value);
            }
        }
        [ModelDefault("Caption", "Chấm CĐTN")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ChamCDTN
        {
            get
            {
                return _ChamCDTN;
            }
            set
            {
                SetPropertyValue("ChamCDTN", ref _ChamCDTN, value);
            }
        }
        [ModelDefault("Caption", "Chấm bảo vệ KLTN")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ChamBaoVeKLTN
        {
            get
            {
                return _ChamBaoVeKLTN;
            }
            set
            {
                SetPropertyValue("ChamBaoVeKLTN", ref _ChamBaoVeKLTN, value);
            }
        }
        [ModelDefault("Caption", "Chấm bảo vệ KLTN CLC TV")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ChamBaoVeKLTNCLCTV
        {
            get
            {
                return _ChamBaoVeKLTNCLCTV;
            }
            set
            {
                SetPropertyValue("ChamBaoVeKLTNCLCTV", ref _ChamBaoVeKLTNCLCTV, value);
            }
        }
        [ModelDefault("Caption", "Chấm bảo vệ KLTN CLC TA")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ChamBaoVeKLTNCLCTA
        {
            get
            {
                return _ChamBaoVeKLTNCLCTA;
            }
            set
            {
                SetPropertyValue("ChamBaoVeKLTNCLCTA", ref _ChamBaoVeKLTNCLCTA, value);
            }
        }
        #endregion
        #region Hướng dẫn
        [ModelDefault("Caption", "HD hệ CLC tham quan thực tế ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HDCLCThamQuanThucTe
        {
            get
            {
                return _HDCLCThamQuanThucTe;
            }
            set
            {
                SetPropertyValue("HDCLCThamQuanThucTe", ref _HDCLCThamQuanThucTe, value);
            }
        }
        [ModelDefault("Caption", "HD Viết CĐTN cuối khóa (buổi)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HDVietCDTNSVCuoiKhoa
        {
            get
            {
                return _HDVietCDTNSVCuoiKhoa;
            }
            set
            {
                SetPropertyValue("HDVietCDTNSVCuoiKhoa", ref _HDVietCDTNSVCuoiKhoa, value);
            }
        }
        [ModelDefault("Caption", "HD đề tài luận văn học viện cao học (buổi)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HDDeTaiLuanVanCaoHoc
        {
            get
            {
                return _HDDeTaiLuanVanCaoHoc;
            }
            set
            {
                SetPropertyValue("HDDeTaiLuanVanCaoHoc", ref _HDDeTaiLuanVanCaoHoc, value);
            }
        }

        [ModelDefault("Caption", "HD thực tế nghề nghiệp CLC")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HDThucTeNgeNghiepCLC
        {
            get
            {
                return _HDThucTeNgeNghiepCLC;
            }
            set
            {
                SetPropertyValue("HDThucTeNgeNghiepCLC", ref _HDThucTeNgeNghiepCLC, value);
            }
        }
        [ModelDefault("Caption", "Hướng dẫn CĐTN (quyển)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HDCDTN
        {
            get
            {
                return _HDCDTN;
            }
            set
            {
                SetPropertyValue("HDCDTN", ref _HDCDTN, value);
            }
        }
        [ModelDefault("Caption", "Hướng dẫn KLTN (quyển)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HDKLTN
        {
            get
            {
                return _HDKLTN;
            }
            set
            {
                SetPropertyValue("HDKLTN", ref _HDKLTN, value);
            }
        }
        [ModelDefault("Caption", "Hướng dẫn KLTN CLC TV (quyển)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HDKLTNCLCTV
        {
            get
            {
                return _HDKLTNCLCTV;
            }
            set
            {
                SetPropertyValue("HDKLTNCLCTV", ref _HDKLTNCLCTV, value);
            }
        }
        [ModelDefault("Caption", "Hướng dẫn KLTN CLC TA (quyển)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HDKLTNCLCTA
        {
            get
            {
                return _HDKLTNCLCTA;
            }
            set
            {
                SetPropertyValue("HDKLTNCLCTA", ref _HDKLTNCLCTA, value);
            }
        }
        [ModelDefault("Caption", "Giải đáp thắc mắc hệ VLVH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GiaiDapHeVLVH
        {
            get
            {
                return _GiaiDapHeVLVH;
            }
            set
            {
                SetPropertyValue("GiaiDapHeVLVH", ref _GiaiDapHeVLVH, value);
            }
        }
        [ModelDefault("Caption", "Hệ thống hóa và ôn thi cuối khóa")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeThongOnThiCuoiKhoa
        {
            get
            {
                return _HeThongOnThiCuoiKhoa;
            }
            set
            {
                SetPropertyValue("HeThongOnThiCuoiKhoa", ref _HeThongOnThiCuoiKhoa, value);
            }
        }
        #endregion
        #region Ôn thi cuối khóa
        [ModelDefault("Caption", "Soạn bộ đề thi cho 01 học phần")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoanDeThi1HocPhan
        {
            get
            {
                return _SoanDeThi1HocPhan;
            }
            set
            {
                SetPropertyValue("SoanDeThi1HocPhan", ref _SoanDeThi1HocPhan, value);
            }
        }
        [ModelDefault("Caption", "Bổ sung ngân hàng câu hỏi, đề thi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal BoSungNganHangCauHoi
        {
            get
            {
                return _BoSungNganHangCauHoi;
            }
            set
            {
                SetPropertyValue("BoSungNganHangCauHoi", ref _BoSungNganHangCauHoi, value);
            }
        }
        [ModelDefault("Caption", "Ra đề thi tốt nghiệp")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal RaDeThiTotNghiep
        {
            get
            {
                return _RaDeThiTotNghiep;
            }
            set
            {
                SetPropertyValue("RaDeThiTotNghiep", ref _RaDeThiTotNghiep, value);
            }
        }
        [ModelDefault("Caption", "Ra đề thi hết học phần đào tạo SĐH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal RaDeHetHocPhanSDH
        {
            get
            {
                return _RaDeHetHocPhanSDH;
            }
            set
            {
                SetPropertyValue("RaDeHetHocPhanSDH", ref _RaDeHetHocPhanSDH, value);
            }
        }
        [ModelDefault("Caption", "Phụ trách ca thi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PhuTrachCaThi
        {
            get
            {
                return _PhuTrachCaThi;
            }
            set
            {
                SetPropertyValue("PhuTrachCaThi", ref _PhuTrachCaThi, value);
            }
        }
        [ModelDefault("Caption", "Phụ đạo cho sinh viên nước ngoài")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PhuDaoChoSinhVienNuocNgoai
        {
            get
            {
                return _PhuDaoSinhVienNuocNgoai;
            }
            set
            {
                SetPropertyValue("PhuDaoSinhVienNuocNgoai", ref _PhuDaoSinhVienNuocNgoai, value);
            }
        }
        #endregion
        #endregion
        #region Dữ liệu ẩn
        [ModelDefault("Caption", "SL chấm thi hết HP tự luận")]
        //[Browsable(false)]
        public int SLChamThiHetHocPhan {
            get
            {
                return _SLChamThiHetHocPhan;
            }
            set
            {
                SetPropertyValue("SLChamThiHetHocPhan", ref _SLChamThiHetHocPhan, value);
            }
        }

        [ModelDefault("Caption", "SL chấm thi hết HP vấn đáp")]
        //[Browsable(false)]
        public int SLChamThiHetHocPhanVanDap
        {
            get
            {
                return _SLChamThiHetHocPhanVanDap;
            }
            set
            {
                SetPropertyValue("SLChamThiHetHocPhanVanDap", ref _SLChamThiHetHocPhanVanDap, value);
            }
        }


        [ModelDefault("Caption", "SL chấm thi hết HP tiểu luận")]
        //[Browsable(false)]
        public int SLChamThiHetHocPhanTieuLuan
        {
            get
            {
                return _SLChamThiHetHocPhanTieuLuan;
            }
            set
            {
                SetPropertyValue("SLChamThiHetHocPhanTieuLuan", ref _SLChamThiHetHocPhanTieuLuan, value);
            }
        }

        [ModelDefault("Caption", "SL thực tập nghề CLC")]
        //[Browsable(false)]
        public int SLThucTapNgheCLC
        {
            get
            {
                return _SLThucTapNgheCLC;
            }
            set
            {
                SetPropertyValue("SLThucTapNgheCLC", ref _SLThucTapNgheCLC, value);
            }
        }

        [ModelDefault("Caption", "SL CĐTN")]
        //[Browsable(false)]
        public int SLCDTN
        {
            get
            {
                return _SLCDTN;
            }
            set
            {
                SetPropertyValue("SLCDTN", ref _SLCDTN, value);
            }
        }

        [ModelDefault("Caption", "SL HD CĐTN")]
        //[Browsable(false)]
        public int SLHDCDTN
        {
            get
            {
                return _SLHDCDTN;
            }
            set
            {
                SetPropertyValue("SLHDCDTN", ref _SLHDCDTN, value);
            }
        }

        [ModelDefault("Caption", "SL KLTN đại trà")]
        //[Browsable(false)]
        public int SLKhoaLuanTN
        {
            get
            {
                return _SLKhoaLuanTN;
            }
            set
            {
                SetPropertyValue("SLKhoaLuanTN", ref _SLKhoaLuanTN, value);
            }
        }

        [ModelDefault("Caption", "SL KLTN CLC TV")]
        //[Browsable(false)]
        public int SLKhoaLuanTNCLCTV
        {
            get
            {
                return _SLKhoaLuanTNCLCTV;
            }
            set
            {
                SetPropertyValue("SLKhoaLuanTNCLCTV", ref _SLKhoaLuanTNCLCTV, value);
            }
        }

        [ModelDefault("Caption", "SL KLTN CLCL TA")]
        //[Browsable(false)]
        public int SLKhoaLuanTNCLCTA
        {
            get
            {
                return _SLKhoaLuanTNCLCTA;
            }
            set
            {
                SetPropertyValue("SLKhoaLuanTNCLCTA", ref _SLKhoaLuanTNCLCTA, value);
            }
        }

        [ModelDefault("Caption", "SL HD KLTN đại trà")]
        //[Browsable(false)]
        public int SLHDKhoaLuanTN
        {
            get
            {
                return _SLHDKhoaLuanTN;
            }
            set
            {
                SetPropertyValue("SLHDKhoaLuanTN", ref _SLHDKhoaLuanTN, value);
            }
        }

        [ModelDefault("Caption", "SL HD KLTN CLC TV")]
        //[Browsable(false)]
        public int SLHDKhoaLuanTNCLCTV
        {
            get
            {
                return _SLHDKhoaLuanTNCLCTV;
            }
            set
            {
                SetPropertyValue("SLHDKhoaLuanTNCLCTV", ref _SLHDKhoaLuanTNCLCTV, value);
            }
        }

        [ModelDefault("Caption", "SL HD KLTN CLC TA")]
        //[Browsable(false)]
        public int SLHDKhoaLuanTNCLCTA
        {
            get
            {
                return _SLHDKhoaLuanTNCLCTA;
            }
            set
            {
                SetPropertyValue("SLHDKhoaLuanTNCLCTA", ref _SLHDKhoaLuanTNCLCTA, value);
            }
        }

        [ModelDefault("Caption", "SL ca coi thi")]
        //[Browsable(false)]
        public int SLCaCoiThi
        {
            get
            {
                return _SLCaCoiThi;
            }
            set
            {
                SetPropertyValue("SLCaCoiThi", ref _SLCaCoiThi, value);
            }
        }

        [ModelDefault("Caption", "SL HD thực tế nghề nghiệp CLC")]
        //[Browsable(false)]
        public int SLHDThucTeNgeNghiepCLC
        {
            get
            {
                return _SLHDThucTeNgeNghiepCLC;
            }
            set
            {
                SetPropertyValue("SLHDThucTeNgeNghiepCLC", ref _SLHDThucTeNgeNghiepCLC, value);
            }
        }


        [ModelDefault("Caption", "SL Chấm thi tốt nghiệp")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SLChamThiTN
        {
            get
            {
                return _SLChamThiTN;
            }
            set
            {
                SetPropertyValue("SLChamThiTN", ref _SLChamThiTN, value);
            }
        }

        [ModelDefault("Caption", "SL Hướng dẫn sinh viên hệ CLC tham quan thực tế")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SLHDSVThamQuanThucTe
        {
            get
            {
                return _SLHDSVThamQuanThucTe;
            }
            set
            {
                SetPropertyValue("SLHDSVThamQuanThucTe", ref _SLHDSVThamQuanThucTe, value);
            }
        }
        [ModelDefault("Caption", "SL Hướng dẫn viết CĐTN cho sinh viên cuối khóa (buổi)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SLHDVietCDTN
        {
            get
            {
                return _SLHDVietCDTN;
            }
            set
            {
                SetPropertyValue("SLHDVietCDTN", ref _SLHDVietCDTN, value);
            }
        }
        [ModelDefault("Caption", "SL Hướng dẫn đề tài luận văn cho học viện cao học (buổi)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SLHDDeTaiLuanVan
        {
            get
            {
                return _SLHDDeTaiLuanVan;
            }
            set
            {
                SetPropertyValue("SLHDDeTaiLuanVan", ref _SLHDDeTaiLuanVan, value);
            }
        }
        [ModelDefault("Caption", "SL Giải đáp thắc mắc cho sinh viên hệ VLVH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SLGiaiDapThacMac
        {
            get
            {
                return _SLGiaiDapThacMac;
            }
            set
            {
                SetPropertyValue("SLGiaiDapThacMac", ref _SLGiaiDapThacMac, value);
            }
        }
        [ModelDefault("Caption", "SL Hệ thống hóa và ôn thi cuối khóa")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SLHeThongHoa_OnThi
        {
            get
            {
                return _SLHeThongHoa_OnThi;
            }
            set
            {
                SetPropertyValue("SLHeThongHoa_OnThi", ref _SLHeThongHoa_OnThi, value);
            }
        }
        [ModelDefault("Caption", "SL Soạn bộ đề thi cho 01 học phần")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SLSoanDeThi
        {
            get
            {
                return _SLSoanDeThi;
            }
            set
            {
                SetPropertyValue("SLSoanDeThi", ref _SLSoanDeThi, value);
            }
        }
        [ModelDefault("Caption", "SL Bổ sung ngân hàng câu hỏi, đề thi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SLBoSungNganHangCauHoi
        {
            get
            {
                return _SLBoSungNganHangCauHoi;
            }
            set
            {
                SetPropertyValue("SLBoSungNganHangCauHoi", ref _SLBoSungNganHangCauHoi, value);
            }
        }
        [ModelDefault("Caption", "SL Ra đề thi tốt nghiệp")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SLRaDeTotNghiep
        {
            get
            {
                return _SLRaDeTotNghiep;
            }
            set
            {
                SetPropertyValue("SLRaDeTotNghiep", ref _SLRaDeTotNghiep, value);
            }
        }
        [ModelDefault("Caption", "SL Ra đề thi hết học phần đào tạo SĐH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SLRaDeThiHetHocPhan
        {
            get
            {
                return _SLRaDeThiHetHocPhan;
            }
            set
            {
                SetPropertyValue("SLRaDeThiHetHocPhan", ref _SLRaDeThiHetHocPhan, value);
            }
        }
        [ModelDefault("Caption", "SL phụ đạo sinh viên nước ngoài")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SLPhuDaoSinhVienNuocNgoai
        {
            get
            {
                return _SLPhuDaoSinhVienNuocNgoai;
            }
            set
            {
                SetPropertyValue("SLPhuDaoSinhVienNuocNgoai", ref _SLPhuDaoSinhVienNuocNgoai, value);
            }
        }
        #endregion

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

        public ChiTietKeKhaiSauGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}