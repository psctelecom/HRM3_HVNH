using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HopDong;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.HoSo
{
    [ImageName("BO_Resume")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Thông tin lương")]
    [Appearance("HideHSLuong_VLU", TargetItems = "NhanVienThongTinLuong.HeSoLuong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'VLU'")]

    public class NhanVienThongTinLuong : TruongBaseObject
    {
        private bool _KhongTinhLuong;
        //
        private DateTime _NgayHuongHSPCThamNien;
        private DateTime _NgayHuongHSPCChuyenMon;
        private DateTime _NgayHuongHSPCTracNhiem;
        //
        private bool _DuocHuongHSPCChuyenVien;
        private CuTruEnum _CuTru;

        ////VLU
        //private decimal _P1MucLuongDongBHXH;
        //private decimal _P3ThuNhapTangThem;

        //UEL
        private decimal _HSPCKiemNhiemTrongTruong;
        private decimal _HSPCThamNienTrongTruong;
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
        //UEL
        private decimal _HSPCPhucVuDaoTao;
        //
        private ThongTinLuongEnum _PhanLoai;
        private NgachLuong _NgachLuong; //VLU không dùng
        private DateTime _NgayBoNhiemNgach;
        private DateTime _NgayHuongLuong;
        private BacLuong _BacLuong;
        private decimal _HeSoLuong;
        private DateTime _MocNangLuong;
        private DateTime _MocNangLuongLanSau;
        private string _LyDoDieuChinh;
        private DateTime _MocNangLuongDieuChinh;
        private bool _Huong85PhanTramLuong;
        private decimal _LuongKhoan;

        private DateTime _NgayCapNhatThamNienCongTac;
        private decimal _ThamNienCongTac;
        private string _ThangNamTNCongTac;
        private decimal _PhuCapTrachNhiemCongViec;
        private decimal _PhuCapTangThem;
        private decimal _PhuCapTienXang;
        private decimal _PhuCapDienThoai;
        private decimal _PhuCapTienAn;        
        private DateTime _NgayHuongThamNien;
        private decimal _HSPCChucVuCongDoan;
        private decimal _HSPCChucVuDoan;
        private decimal _HSPCChucVuDang;
        private int _PhuCapThuHut;
        private decimal _HSPCChuyenMon;
        private decimal _HSPCUuDai;
        private int _PhuCapUuDai;
        private decimal _HSPCVuotKhung;
        private int _VuotKhung;
        private decimal _ThamNien;
        private decimal _HSPCThamNien;
        private decimal _HSPCChucVu;
        private DateTime _NgayHuongHSPCChucVu;
        private decimal _HSPCDocHai;
        private int _PhuCapDocHai;
        private decimal _HSPCTrachNhiem;
        private decimal _HSPCKiemNhiem;
        private decimal _HSPCKhac;       
        private int _PhuCapKhac;
        private decimal _HSLTangThem;
        private decimal _ChenhLechBaoLuuLuong;

        private bool _KhongDongBaoHiem;
        private bool _KhongThamGiaCongDoan;
        private bool _KhongDongBHXH;
        private bool _KhongDongBHYT;
        private bool _KhongDongBHTN;
        private bool _KhongTinhTienAnTrua;

        //QNU
        private decimal _HSPCTracNhiemTruong;
        private decimal _PhuCapTienDocHai;
        private decimal _HSPCChucVuBaoLuu;
        private DateTime _NgayHetHanHuongHSPChucVuBaoLuu;
        private decimal _TienTroCap;
        private decimal _KhoanTienNgoaiGio;
        private decimal _KhoanPhuongTien;

        //NEU
        private decimal _HSChung;
        private decimal _HSHoanThanhCV;
        private decimal _HSPCGiangDay;
        private DateTime _NgayHuongThamNienHC;
        private decimal _PhanTramThamNienHC;
        private decimal _HSPCThamNienHC;
        private decimal _PhanTramKhoiHC;
        private decimal _HSPCKhoiHanhChinh;
        //
        private int _SoNguoiPhuThuoc;
        private int _SoThangGiamTru;
        private bool _KhongCuTru;
        private CoQuanThue _CoQuanThue;
        private string _MaSoThue;
        private DateTime _NgayCapMST;
        private int _PhuongThucTinhThue;
        private bool _TinhThueTNCNMacDinh;
        private bool _LuongNET;
        private bool _IsVLU = false;

        public NhanVienThongTinLuong(Session session)
            : base(session)
        {
            
        }
        //[ModelDefault("EditMask", "N0")]
        //[ModelDefault("DisplayFormat", "N0")]
        //[RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("Caption", "Mức lương đóng BHXH (P1)")]
        //[ModelDefault("EditMask", "N0")]
        //[ModelDefault("DisplayFormat", "N0")]
        //public decimal P1MucLuongDongBHXH
        //{
        //    get
        //    {
        //        return _P1MucLuongDongBHXH;
        //    }
        //    set
        //    {
        //        SetPropertyValue("P1MucLuongDongBHXH", ref _P1MucLuongDongBHXH, value);
        //    }
        //}
        //[ModelDefault("EditMask", "N0")]
        //[ModelDefault("DisplayFormat", "N0")]
        //[RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("Caption", "Thu nhập tăng thêm (P3)")]
        //[ModelDefault("EditMask", "N0")]
        //[ModelDefault("DisplayFormat", "N0")]
        //public decimal P3ThuNhapTangThem
        //{
        //    get
        //    {
        //        return _P3ThuNhapTangThem;
        //    }
        //    set
        //    {
        //        SetPropertyValue("P3ThuNhapTangThem", ref _P3ThuNhapTangThem, value);
        //    }
        //}


        [ModelDefault("Caption", "Ngày cấp mã số thuế")]
        public DateTime NgayCapMST
        {
            get
            {
                return _NgayCapMST;
            }
            set
            {
                SetPropertyValue("NgayCapMST", ref _NgayCapMST, value);
            }
        }
        [NonPersistent]
        [Browsable(false)]
        [ModelDefault("Caption", "Role")]
        public bool IsVLU
        {
            get
            {
                return _IsVLU;
            }
            set
            {
                SetPropertyValue("IsVLU", ref _IsVLU, value);
            }
        }

        [ModelDefault("Caption", "Phần trăm tính thuế TNCN (%)")]
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
                    {
                        LuongKhoan = 0;
                    }
                    else if (value == ThongTinLuongEnum.LuongKhoanCoBHXH
                        || value == ThongTinLuongEnum.LuongKhoanKhongBHXH
                        || value == ThongTinLuongEnum.LuongKhoanNhanVienCongNhat)
                    {
                        NgachLuong = null;
                        BacLuong = null;
                        HeSoLuong = 0;
                        NgayHuongLuong = DateTime.MinValue;
                        MocNangLuong = DateTime.MinValue;
                        MocNangLuongLanSau = DateTime.MinValue;
                    }
                    else
                    {
                        NgayHuongLuong = DateTime.MinValue;
                        MocNangLuong = DateTime.MinValue;
                        MocNangLuongLanSau = DateTime.MinValue;
                    }
                }
            }
        }

        [Browsable(false)]
        public string Caption
        {
            get
            {
                if (PhanLoai == ThongTinLuongEnum.LuongHeSo)
                    return ObjectFormatter.Format("Ngạch {NgachLuong.TenNgachLuong} ({BacLuong.TenBacLuong}), hệ số: {HeSoLuong:N4}", this);
                else
                    return ObjectFormatter.Format("Lương khoán: {LuongKhoan:N0}", this);
            }
        }


        [ModelDefault("Caption", "Không tính lương")]
        public bool KhongTinhLuong
        {
            get
            {
                return _KhongTinhLuong;
            }
            set
            {
                SetPropertyValue("KhongTinhLuong", ref _KhongTinhLuong, value);
            }
        }

        [ImmediatePostData]
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
                if (!IsLoading)
                {
                    BacLuong = null;
                }
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch")]
        public DateTime NgayBoNhiemNgach
        {
            get
            {
                return _NgayBoNhiemNgach;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemNgach", ref _NgayBoNhiemNgach, value);
            }
        }

        //ngay huong luong ghi tren quyet dinh
        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày hưởng lương")]
        public DateTime NgayHuongLuong
        {
            get
            {
                return _NgayHuongLuong;
            }
            set
            {
                SetPropertyValue("NgayHuongLuong", ref _NgayHuongLuong, value);
                //if (!IsLoading && value != null)
                //{
                //    MocNangLuong = NgayHuongLuong;
                //}
            }
        }

        [ImmediatePostData()]
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
                    VuotKhung = 0;
                    HSPCVuotKhung = 0;
                }
            }
        }

        [ImmediatePostData]
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
                if (!IsLoading)
                {
                    //
                    if (!Huong85PhanTramLuong)
                    {
                        HSPCVuotKhung = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCVuotKhung);
                        HSPCUuDai = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCUuDai);
                        HSPCThamNien = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNien);
                        HSPCThamNienHC = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNienHanhChinh);
                        HSPCKhoiHanhChinh = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhoiHanhChinh);
                        //HSPCDocHai = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCDocHai);
                        //HSPCKhac = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhac);
                        //HoSoHelper.CapNhatHSLTangThem(Session, this, null);
                    }
                    else
                    {
                        HSPCVuotKhung = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCVuotKhung) * 85) / 100;
                        HSPCUuDai = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCUuDai) * 85) / 100;
                        HSPCThamNien = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNien);
                        HSPCThamNienHC = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNienHanhChinh) * 85) / 100;
                        HSPCKhoiHanhChinh = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhoiHanhChinh) * 85) / 100;
                        //HSPCDocHai = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCDocHai) * 85) / 100;
                        //HSPCKhac = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhac) * 85) / 100;
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Mốc nâng lương")]
        public DateTime MocNangLuong
        {
            get
            {
                return _MocNangLuong;
            }
            set
            {
                SetPropertyValue("MocNangLuong", ref _MocNangLuong, value);
                if (!IsLoading && value != DateTime.MinValue && NgachLuong != null)
                {
                    //chưa vượt khung
                    if (VuotKhung == 0)
                        MocNangLuongLanSau = MocNangLuong.AddMonths(NgachLuong.ThoiGianNangBac);
                    //vượt khung thi mỗi năm vượt khung một lần
                    else
                        MocNangLuongLanSau = MocNangLuong.AddMonths(12);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Mốc nâng lương điều chỉnh")]
        public DateTime MocNangLuongDieuChinh
        {
            get
            {
                return _MocNangLuongDieuChinh;
            }
            set
            {
                SetPropertyValue("MocNangLuongDieuChinh", ref _MocNangLuongDieuChinh, value);
                if (!IsLoading)
                {
                    if (MocNangLuongDieuChinh != DateTime.MinValue
                        && MocNangLuongDieuChinh > MocNangLuong)
                    {
                        if (VuotKhung == 0)
                            MocNangLuongLanSau = MocNangLuongDieuChinh.AddMonths(NgachLuong.ThoiGianNangBac);
                        else
                            MocNangLuongLanSau = MocNangLuongDieuChinh.AddMonths(12);
                    }
                    else if (MocNangLuong != DateTime.MinValue)
                    {
                        //chưa vượt khung
                        if (VuotKhung == 0)
                            MocNangLuongLanSau = MocNangLuong.AddMonths(NgachLuong.ThoiGianNangBac);
                        //vượt khung thi mỗi năm vượt khung một lần
                        else
                            MocNangLuongLanSau = MocNangLuong.AddMonths(12);
                    }
                }
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Lý do điều chỉnh")]
        public string LyDoDieuChinh
        {
            get
            {
                return _LyDoDieuChinh;
            }
            set
            {
                SetPropertyValue("LyDoDieuChinh", ref _LyDoDieuChinh, value);
            }
        }

        //cái này dùng để tính mốc nâng lương lần sau
        //[Browsable(false)]
        [ModelDefault("Caption", "Mốc nâng lương lần sau")]
        //[ModelDefault("AllowEdit", "False")]
        public DateTime MocNangLuongLanSau
        {
            get
            {
                return _MocNangLuongLanSau;
            }
            set
            {
                SetPropertyValue("MocNangLuongLanSau", ref _MocNangLuongLanSau, value);
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
                if (!IsLoading)
                {
                    //
                    if (!Huong85PhanTramLuong)
                    {
                        HSPCVuotKhung = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCVuotKhung);
                        HSPCUuDai = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCUuDai);
                        HSPCThamNien = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNien);
                        HSPCThamNienHC = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNienHanhChinh);
                        HSPCKhoiHanhChinh = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhoiHanhChinh);
                        //HSPCDocHai = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCDocHai);
                        //HSPCKhac = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhac);
                        //HoSoHelper.CapNhatHSLTangThem(Session, this, null);
                    }
                    else
                    {
                        HSPCVuotKhung = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCVuotKhung) * 85) / 100;
                        HSPCUuDai = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCUuDai) * 85) / 100;
                        HSPCThamNien = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNien);
                        HSPCThamNienHC = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNienHanhChinh) * 85) / 100;
                        HSPCKhoiHanhChinh = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhoiHanhChinh) * 85) / 100;
                        //HSPCDocHai = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCDocHai) * 85) / 100;
                        //HSPCKhac = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhac) * 85) / 100;
                    }
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

        [ModelDefault("Caption", "Không đóng công đoàn")]
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
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.SpinEditor")]
        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        public decimal HSPCChucVu
        {
            get
            {
                return _HSPCChucVu;
            }
            set
            {
                SetPropertyValue("HSPCChucVu", ref _HSPCChucVu, value);
                if (!IsLoading)
                {
                    CauHinh.CauHinhChung chc = HamDungChung.CauHinhChung;
                    if (chc != null)
                    {
                        //
                        if (!Huong85PhanTramLuong)
                        {
                            HSPCVuotKhung = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCVuotKhung);
                            HSPCUuDai = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCUuDai);
                            HSPCThamNien = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNien);
                            HSPCThamNienHC = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNienHanhChinh);
                            HSPCKhoiHanhChinh = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhoiHanhChinh);
                            //HSPCDocHai = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCDocHai);
                            //HSPCKhac = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhac);
                            //HoSoHelper.CapNhatHSLTangThem(Session, this, null);
                        }
                        else
                        {
                            HSPCVuotKhung = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCVuotKhung) * 85) / 100;
                            HSPCUuDai = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCUuDai) * 85) / 100;
                            HSPCThamNien = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNien);
                            HSPCThamNienHC = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNienHanhChinh) * 85) / 100;
                            HSPCKhoiHanhChinh = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhoiHanhChinh) * 85) / 100;
                            //HSPCDocHai = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCDocHai) * 85) / 100;
                            //HSPCKhac = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhac) * 85) / 100;
                        }
                    }
                }
            }
        }

        [ModelDefault("Caption", "Ngày hưởng HSPCCV")]
        public DateTime NgayHuongHSPCChucVu
        {
            get
            {
                return _NgayHuongHSPCChucVu;
            }
            set
            {
                SetPropertyValue("NgayHuongHSPCChucVu", ref _NgayHuongHSPCChucVu, value);
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
       
        [ModelDefault("Caption", "HSPC trách nhiệm trường")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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
                if (!IsLoading)
                {
                    if(!Huong85PhanTramLuong)
                        HSPCUuDai = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCUuDai);
                    else
                        HSPCUuDai = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCUuDai) / 100 * 85;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "HS khác trường")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
        public int PhuCapKhac
        {
            get
            {
                return _PhuCapKhac;
            }
            set
            {
                SetPropertyValue("PhuCapKhac", ref _PhuCapKhac, value);
                if (!IsLoading)
                {
                    if (!Huong85PhanTramLuong)
                    {
                        HSPCKhac = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhac);
                    }
                    else
                    {
                        HSPCKhac = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhac) * 85) / 100;
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "% PC độc hại")]
        public int PhuCapDocHai
        {
            get
            {
                return _PhuCapDocHai;
            }
            set
            {
                SetPropertyValue("PhuCapDocHai", ref _PhuCapDocHai, value);
                if (!IsLoading)
                {
                    if (!Huong85PhanTramLuong)
                    {
                        HSPCDocHai = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCDocHai);
                    }
                    else
                    {

                        HSPCDocHai = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCDocHai) * 85) / 100;
                    }
                }
            }
        }

        [ModelDefault("Caption", "Chênh lệch lương")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
        public decimal ChenhLechBaoLuuLuong
        {
            get
            {
                return _ChenhLechBaoLuuLuong;
            }
            set
            {
                SetPropertyValue("ChenhLechBaoLuuLuong", ref _ChenhLechBaoLuuLuong, value);
            }
        }

        [ImmediatePostData]
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
                ////Cập nhật mốc nâng lương lần sau
                //chưa vượt khung
                if (!IsLoading && NgachLuong != null && MocNangLuong != DateTime.MinValue)
                {
                    if (value == 0)
                        MocNangLuongLanSau = MocNangLuong.AddMonths(NgachLuong.ThoiGianNangBac);
                    //vượt khung thi mỗi năm vượt khung một lần
                    else
                        MocNangLuongLanSau = MocNangLuong.AddMonths(12);
                }

                ///Cập nhật hệ số
                if (!IsLoading)
                {
                    if (!Huong85PhanTramLuong )
                    {
                        HSPCVuotKhung = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCVuotKhung);
                    }
                    else
                    {
                        HSPCVuotKhung = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCVuotKhung) * 85) / 100;
                    }
                }
            }
        }

        // HSPC vượt khung = Hệ số lương * % vượt khung
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
                if (!IsLoading)
                {
                    if (!Huong85PhanTramLuong)
                    {
                        HSPCUuDai = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCUuDai);
                        HSPCThamNien = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNien);
                        HSPCThamNienHC = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNienHanhChinh);
                        HSPCKhoiHanhChinh = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhoiHanhChinh);
                    }
                    else
                    {
                        HSPCThamNien = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNien) * 85) / 100;
                        HSPCUuDai = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCUuDai) * 85) / 100;
                        HSPCThamNienHC = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNienHanhChinh) * 85) / 100;
                        HSPCKhoiHanhChinh = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhoiHanhChinh) * 85) / 100;
                    }
                }
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
                if (!IsLoading)
                {
                    if (!Huong85PhanTramLuong)
                    {
                        HSPCThamNien = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNien);
                    }
                    else
                    {
                        HSPCThamNien = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNien) * 85) / 100;
                    }
                }
            }
        }

        //HSPC thâm niên = (hệ số lương + phụ cấp chức vụ + HSPC vượt khung) * % thâm niên
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
        [ImmediatePostData]
        [ModelDefault("Caption", "HSPCCV bảo lưu")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.SpinEditor")]
        public decimal HSPCChucVuBaoLuu
        {
            get
            {
                return _HSPCChucVuBaoLuu;
            }
            set
            {
                SetPropertyValue("HSPCChucVuBaoLuu", ref _HSPCChucVuBaoLuu, value);
                if (!IsLoading)
                {
                    if (!Huong85PhanTramLuong)
                    {
                        HSPCThamNien = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNien);
                    }
                    else
                    {
                        HSPCThamNien = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNien) * 85) / 100;
                    }
                }
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
                if (!IsLoading)
                    NgayCapNhatThamNienCongTac = HamDungChung.GetServerTime();
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Ngày cập nhật thâm niên công tác")]
        public DateTime NgayCapNhatThamNienCongTac
        {
            get
            {
                return _NgayCapNhatThamNienCongTac;
            }
            set
            {
                SetPropertyValue("NgayCapNhatThamNienCongTac", ref _NgayCapNhatThamNienCongTac, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp trách nhiệm trường")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal PhuCapTrachNhiemCongViec
        {
            get
            {
                return _PhuCapTrachNhiemCongViec;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiemCongViec", ref _PhuCapTrachNhiemCongViec, value);
            }
        }

        [ModelDefault("Caption", "HSL tăng thêm")]
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
                if (!IsLoading)
                {
                    SoThangGiamTru = SoNguoiPhuThuoc * 12;
                }
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

        [ModelDefault("Caption", "Mã số thuế (*)")]
        [RuleUniqueValue("", DefaultContexts.Save, TargetCriteria = "MaTruong != 'QNU'")]
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

        [ModelDefault("Caption", "Cơ quan thuế")]
        public CoQuanThue CoQuanThue
        {
            get
            {
                return _CoQuanThue;
            }
            set
            {
                SetPropertyValue("CoQuanThue", ref _CoQuanThue, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Được hưởng HSPC chuyên viên")]
        public bool DuocHuongHSPCChuyenVien
        {
            get
            {
                return _DuocHuongHSPCChuyenVien;
            }
            set
            {
                if (!IsLoading && value)
                {
                    ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("NhanVienThongTinLuong=?", Oid));
                    if (nhanVien != null && nhanVien.HopDongHienTai != null)
                    {
                        if (nhanVien.HopDongHienTai is HopDong_Khoan)
                        {
                            if (XtraMessageBox.Show("Đây là nhân viên hợp đồng khoán. Bạn có chắc chắn nhân viên này được hưởng phụ cấp chuyên viên?", "Thông báo", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                                SetPropertyValue("DuocHuongHSPCChuyenVien", ref _DuocHuongHSPCChuyenVien, value);
                        }
                        else
                            SetPropertyValue("DuocHuongHSPCChuyenVien", ref _DuocHuongHSPCChuyenVien, value);
                    }
                    else
                        SetPropertyValue("DuocHuongHSPCChuyenVien", ref _DuocHuongHSPCChuyenVien, value);
                }
                else
                    SetPropertyValue("DuocHuongHSPCChuyenVien", ref _DuocHuongHSPCChuyenVien, value);
            }
        }

        /// <summary>
        ///Tính tất cả hệ số theo công thức đã lập 
        /// </summary>
        private decimal TinhHeSoTheoCongThuc(string congThucTinhHeSo)
        {
            decimal result = 0;
            if (congThucTinhHeSo != null)
            {
                if (congThucTinhHeSo.ToLower().Contains("round"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace(",", "&");
                }
                #region Repacle tất cả hệ số
                if (congThucTinhHeSo.Contains("[HeSoLuong]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[HeSoLuong]", Convert.ToString(HeSoLuong));
                }
                if (congThucTinhHeSo.Contains("[HSPCVuotKhung]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[HSPCVuotKhung]", Convert.ToString(HSPCVuotKhung));
                }
                if (congThucTinhHeSo.Contains("[HSPCUuDai]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[HSPCUuDai]", Convert.ToString(HSPCUuDai));
                }
                if (congThucTinhHeSo.Contains("[HSPCKhac]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[HSPCKhac]", Convert.ToString(HSPCKhac));
                }
                if (congThucTinhHeSo.Contains("[HSPCDocHai]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[HSPCDocHai]", Convert.ToString(HSPCDocHai));
                }
                if (congThucTinhHeSo.Contains("[HSPCThamNien]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[HSPCThamNien]", Convert.ToString(HSPCThamNien));
                }
                if (congThucTinhHeSo.Contains("[HSPCChucVu]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[HSPCChucVu]", Convert.ToString(HSPCChucVu));
                }
                if (congThucTinhHeSo.Contains("[HSPCChucVuBaoLuu]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[HSPCChucVuBaoLuu]", Convert.ToString(HSPCChucVuBaoLuu));
                }
                if (congThucTinhHeSo.Contains("[HSPCChucVuDoan]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[HSPCChucVuDoan]", Convert.ToString(HSPCChucVuDoan));
                }
                if (congThucTinhHeSo.Contains("[HSPCKhac]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[HSPCKhac]", Convert.ToString(HSPCKhac));
                }
                if (congThucTinhHeSo.Contains("[HSPCThamNienHC]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[HSPCThamNienHC]", Convert.ToString(HSPCThamNienHC));
                }
                if (congThucTinhHeSo.Contains("[HSPCKhoiHanhChinh]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[HSPCKhoiHanhChinh]", Convert.ToString(HSPCKhoiHanhChinh));
                }
                #endregion
                //
                #region Repalce tất cả phần trăm
                if (congThucTinhHeSo.Contains("[VuotKhung]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[VuotKhung]", Convert.ToString(VuotKhung));
                }
                if (congThucTinhHeSo.Contains("[ThamNien]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[ThamNien]", Convert.ToString(ThamNien));
                }
                if (congThucTinhHeSo.Contains("[PhuCapUuDai]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[PhuCapUuDai]", Convert.ToString(PhuCapUuDai));
                }
                if (congThucTinhHeSo.Contains("[PhuCapKhac]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[PhuCapKhac]", Convert.ToString(PhuCapKhac));
                }
                if (congThucTinhHeSo.Contains("[PhuCapDocHai]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[PhuCapDocHai]", Convert.ToString(PhuCapDocHai));
                }
                if (congThucTinhHeSo.Contains("[PhanTramThamNienHC]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[PhanTramThamNienHC]", Convert.ToString(PhanTramThamNienHC));
                }
                if (congThucTinhHeSo.Contains("[PhanTramKhoiHC]"))
                {
                    congThucTinhHeSo = congThucTinhHeSo.Replace("[PhanTramKhoiHC]", Convert.ToString(PhanTramKhoiHC));
                }
                #endregion
                //
                //result = Math.Round(HamDungChung.GetHeSo(congThucTinhHeSo.Replace(",", ".")), 4, MidpointRounding.AwayFromZero);
                string congThuc = congThucTinhHeSo.Replace(",", ".");
                result = Math.Round(HamDungChung.GetHeSo(congThuc.Replace("&", ",")), 4, MidpointRounding.AwayFromZero);
            }
            else
                result = 0;
            return result;
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tính lương NET")]
        public bool LuongNET
        {
            get
            {
                return _LuongNET;
            }
            set
            {
                SetPropertyValue("LuongNET", ref _LuongNET, value);
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

        [ModelDefault("Caption", "HSPC lãnh đạo")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
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

        [ModelDefault("Caption", "% phầm trăm phụ cấp đặc biệt")]
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

        [ModelDefault("Caption", "HSPC đặc thù")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
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

        [ModelDefault("Caption", "HSPC lưu động")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
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
        
        [ModelDefault("Caption", "HSPC thâm niên trường")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
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

        [ModelDefault("Caption", "HSPC phục vụ đào tạo")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
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

        [ModelDefault("Caption", "HSPC kiêm nhiệm trường")]
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

        [ModelDefault("Caption", "HSPC khu vục")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
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

        [ModelDefault("Caption", "HS chung")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
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

        [ModelDefault("Caption", "HSPC giảng dạy")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
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

        [ModelDefault("Caption", "HS hoàn thành CV")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
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

        [ModelDefault("Caption", "Cư trú")]
        public CuTruEnum CuTru
        {
            get
            {
                return _CuTru;
            }
            set
            {
                SetPropertyValue("CuTru", ref _CuTru, value);
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

        [ModelDefault("Caption", "Ngày hưởng HSPC chuyên môn")]
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

        [ModelDefault("Caption", "Ngày hưởng HSPC thâm niên")]
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

        [ModelDefault("Caption", "Ngày hưởng HSPC Thâm niên HC")]
        public DateTime NgayHuongThamNienHC
        {
            get
            {
                return _NgayHuongThamNienHC;
            }
            set
            {
                SetPropertyValue("NgayHuongThamNienHC", ref _NgayHuongThamNienHC, value);
            }
        }

        [ImmediatePostData]
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
                if (!IsLoading)
                {
                    if (!Huong85PhanTramLuong)
                    {
                        HSPCThamNienHC = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNienHanhChinh);
                    }
                    else
                    {
                        HSPCThamNienHC = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCThamNienHanhChinh) * 85) / 100;
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "HSPC Thâm niên HC")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.SpinEditor")]
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

        [ImmediatePostData]
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
                if (!IsLoading)
                {
                    if (!Huong85PhanTramLuong)
                    {
                        HSPCKhoiHanhChinh = TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhoiHanhChinh);
                    }
                    else
                    {
                        HSPCKhoiHanhChinh = (TinhHeSoTheoCongThuc(HamDungChung.CauHinhChung.CauHinhThongTinLuong.CongThucTinhHSPCKhoiHanhChinh) * 85) / 100;
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "HSPC khối hành chính")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.SpinEditor")]
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

        private void GetValueOfThamNienAndNgayTinhThamNien(string thangNamThamNien)
        {
            DataTable dt = new DataTable();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ThangNamThamNien", thangNamThamNien);

            SqlCommand cmd = DataProvider.GetCommand("spd_TinhGiaTriThongTinThamNienMoi", CommandType.StoredProcedure, param);
            cmd.Connection = DataProvider.GetConnection();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    this.ThamNien = item["ThamNien"] != null ? Convert.ToInt32(item["ThamNien"].ToString()) : 0;
                    ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("NhanVienThongTinLuong=?", this.Oid));
                    if (nhanVien != null)
                    {
                        nhanVien.NgayTinhThamNienNhaGiao = Convert.ToDateTime(item["NgayTinhThamNien"].ToString());
                    }
                }
            }
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //if(TruongConfig.MaTruong.Equals("VLU"))
            //    IsVLU = true;
            //else
            //    IsVLU = false;
            //
            //Cập nhật lại Mốc nâng lương lần sau
            if (MocNangLuongDieuChinh != DateTime.MinValue
                        && MocNangLuongDieuChinh > MocNangLuong)
            {
                if (VuotKhung == 0)
                    MocNangLuongLanSau = MocNangLuongDieuChinh.AddMonths(NgachLuong.ThoiGianNangBac);
                else
                    MocNangLuongLanSau = MocNangLuongDieuChinh.AddMonths(12);
            }
            else if (MocNangLuong != DateTime.MinValue)
            {
                //chưa vượt khung
                if (VuotKhung == 0)
                {
                    if (NgachLuong != null)
                     MocNangLuongLanSau = MocNangLuong.AddMonths(NgachLuong.ThoiGianNangBac);
                }
                //vượt khung thi mỗi năm vượt khung một lần
                else
                    MocNangLuongLanSau = MocNangLuong.AddMonths(12);
            }
             
        }

        protected override void OnLoading()
        {
            base.OnLoading();
            if (TruongConfig.MaTruong.Equals("VLU"))
                IsVLU = true;
            else
                IsVLU = false;
        }
    }
}
