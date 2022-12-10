using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ChotThongTinTinhLuong
{
    [ImageName("BO_TroCap")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Thông tin tính truy lĩnh")]
    [Appearance("ThongTinTinhTruyLinh.LuongHeSo", TargetItems = "LuongKhoan", Enabled = false, Criteria = "PhanLoai=0")]
    [Appearance("ThongTinTinhTruyLinh.LuongKhoan", TargetItems = "NgachLuong;BacLuong;HeSoLuong", Enabled = false, Criteria = "PhanLoai=1 or PhanLoai=2")]
    public class ThongTinTinhTruyLinh : BaseObject
    {
        private BangChotThongTinTinhLuong _BangChotThongTinTinhLuong;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private TinhTrang _TinhTrang;
        private TrangThaiThamGiaBaoHiemEnum _TrangThaiThamGiaBaoHiem;
        private string _GhiChu;
        private decimal _PhuCapTrachNhiemCongViec;
        private decimal _ThamNienCongTac;
        private decimal _HSPCChucVu3;
        private decimal _HSPCChucVu2;
        private decimal _HSPCChucVu1;
        private decimal _PhuCapTangThem;
        private decimal _PhuCapTienXang;
        private decimal _PhuCapDienThoai;
        private decimal _HSPCChucVuCongDoan;
        private decimal _PhuCapTienAn;
        private decimal _HSPCChucVuDoan;
        private decimal _HSPCChucVuDang;
        private int _PhuCapThuHut;
        private decimal _HSPCThiDua;
        private decimal _HSPCChuyenMon;
        private decimal _HSPCUuDai;
        private ThongTinLuongEnum _PhanLoai;
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuong;
        private decimal _HeSoLuong;
        private bool _KhongCuTru;
        private int _VuotKhung;
        private decimal _HSPCVuotKhung;
        private decimal _ThamNien;
        private decimal _HSPCThamNien;
        private decimal _HSPCChucVu;
        private decimal _HSPCDocHai;
        private decimal _HSPCTrachNhiem;
        private decimal _HSPCKhuVuc;
        private decimal _HSPCKiemNhiem;
        private decimal _HSPCLuuDong;
        private decimal _HSPCKhac;
        private decimal _ChenhLechBaoLuuLuong;
        private int _PhuCapUuDai;
        private int _PhuCapDacThu;
        private int _PhuCapDacBiet;
        private decimal _LuongKhoan;
        private int _SoNguoiPhuThuoc;
        private int _SoThangGiamTru;
        private bool _Huong85PhanTramLuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng chốt thông tin tính lương")]
        [Association("BangChotThongTinTinhLuong-ListThongTinTinhTruyLinh")]
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
                if (!IsLoading && value != null
                    && (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid))
                    BoPhan = value.BoPhan;
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

        #region Lương ngạch bậc
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
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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
                    if (!Huong85PhanTramLuong)
                    {
                        HSPCVuotKhung = Math.Round(HeSoLuong * VuotKhung / 100, 4, MidpointRounding.AwayFromZero);
                        HSPCUuDai = Math.Round((HeSoLuong + HSPCChucVu + HSPCVuotKhung) * PhuCapUuDai / 100, 4, MidpointRounding.AwayFromZero);
                        HSPCThamNien = Math.Round((HeSoLuong + HSPCChucVu + HSPCVuotKhung) * ThamNien / 100, 4, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        HSPCVuotKhung = Math.Round((HeSoLuong * 85 / 100) * VuotKhung / 100, 4, MidpointRounding.AwayFromZero);
                        HSPCUuDai = Math.Round(((HeSoLuong * 85 / 100) + HSPCChucVu + HSPCVuotKhung) * PhuCapUuDai / 100, 4, MidpointRounding.AwayFromZero);
                        HSPCThamNien = Math.Round(((HeSoLuong * 85 / 100) + HSPCChucVu + HSPCVuotKhung) * ThamNien / 100, 4, MidpointRounding.AwayFromZero);
                    }
                }
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
        #endregion

        #region Lương khoán
        [ModelDefault("Caption", "Lương khoán")]
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
        #endregion

        #region Phụ cấp nhà nước
        [ImmediatePostData]
        [ModelDefault("Caption", "HSPC chức vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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
                    if (!Huong85PhanTramLuong)
                    {
                        HSPCVuotKhung = Math.Round(HeSoLuong * VuotKhung / 100, 4, MidpointRounding.AwayFromZero);
                        HSPCUuDai = Math.Round((HeSoLuong + HSPCChucVu + HSPCVuotKhung) * PhuCapUuDai / 100, 4, MidpointRounding.AwayFromZero);
                        HSPCThamNien = Math.Round((HeSoLuong + HSPCChucVu + HSPCVuotKhung) * ThamNien / 100, 4, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        HSPCVuotKhung = Math.Round((HeSoLuong * 85 / 100) * VuotKhung / 100, 4, MidpointRounding.AwayFromZero);
                        HSPCUuDai = Math.Round(((HeSoLuong * 85 / 100) + HSPCChucVu + HSPCVuotKhung) * PhuCapUuDai / 100, 4, MidpointRounding.AwayFromZero);
                        HSPCThamNien = Math.Round(((HeSoLuong * 85 / 100) + HSPCChucVu + HSPCVuotKhung) * ThamNien / 100, 4, MidpointRounding.AwayFromZero);
                    }
                }
            }
        }

        [ModelDefault("Caption", "HSPC độc hại")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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

        [ModelDefault("Caption", "HSPC khu vực")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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

        [ModelDefault("Caption", "HSPC kiêm nhiệm")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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

        [ModelDefault("Caption", "HSPC lưu động")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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

        [ModelDefault("Caption", "HSPC ưu đãi")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
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
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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
                    if (!Huong85PhanTramLuong)
                    {
                        HSPCUuDai = Math.Round((HeSoLuong + HSPCChucVu + HSPCVuotKhung) * PhuCapUuDai / 100, 4, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        HSPCUuDai = Math.Round(((HeSoLuong * 85 / 100) + HSPCChucVu + HSPCVuotKhung) * PhuCapUuDai / 100, 4, MidpointRounding.AwayFromZero);
                    }
                }
            }
        }

        [ModelDefault("Caption", "% PC đặc thù")]
        public int PhuCapDacThu
        {
            get
            {
                return _PhuCapDacThu;
            }
            set
            {
                SetPropertyValue("PhuCapDacThu", ref _PhuCapDacThu, value);
            }
        }

        [ModelDefault("Caption", "% PC đặc biệt")]
        public int PhuCapDacBiet
        {
            get
            {
                return _PhuCapDacBiet;
            }
            set
            {
                SetPropertyValue("PhuCapDacBiet", ref _PhuCapDacBiet, value);
            }
        }

        [ModelDefault("Caption", "Chênh lệch lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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
                if (!IsLoading)
                {
                    if (!Huong85PhanTramLuong)
                    {
                        HSPCVuotKhung = Math.Round(HeSoLuong * VuotKhung / 100, 4, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        HSPCVuotKhung = Math.Round((HeSoLuong * 85 / 100) * VuotKhung / 100, 4, MidpointRounding.AwayFromZero);
                    }
                }
            }
        }

        // HSPC vượt khung = Hệ số lương * % vượt khung
        [ImmediatePostData]
        [ModelDefault("Caption", "HSPC vượt khung")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
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
                        HSPCThamNien = Math.Round((HeSoLuong + HSPCChucVu + HSPCVuotKhung) * ThamNien / 100, 4, MidpointRounding.AwayFromZero);
                        HSPCUuDai = Math.Round((HeSoLuong + HSPCChucVu + HSPCVuotKhung) * PhuCapUuDai / 100, 4, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        HSPCThamNien = Math.Round(((HeSoLuong * 85 / 100) + HSPCChucVu + HSPCVuotKhung) * ThamNien / 100, 4, MidpointRounding.AwayFromZero);
                        HSPCUuDai = Math.Round(((HeSoLuong * 85 / 100) + HSPCChucVu + HSPCVuotKhung) * PhuCapUuDai / 100, 4, MidpointRounding.AwayFromZero);
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
                        HSPCThamNien = Math.Round((HeSoLuong + HSPCChucVu + HSPCVuotKhung) * ThamNien / 100, 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        HSPCThamNien = Math.Round(((HeSoLuong * 85 / 100) + HSPCChucVu + HSPCVuotKhung) * ThamNien / 100, 2, MidpointRounding.AwayFromZero);
                    }
                }
            }
        }

        //HSPC thâm niên = (hệ số lương + phụ cấp chức vụ + HSPC vượt khung) * % thâm niên
        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
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
        #endregion

        #region Phụ cấp trường
        [ModelDefault("Caption", "HSPC Quản lý")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVu1
        {
            get
            {
                return _HSPCChucVu1;
            }
            set
            {
                SetPropertyValue("HSPCChucVu1", ref _HSPCChucVu1, value);
            }
        }

        [ModelDefault("Caption", "HSPC Kiêm nhiệm 1")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVu2
        {
            get
            {
                return _HSPCChucVu2;
            }
            set
            {
                SetPropertyValue("HSPCChucVu2", ref _HSPCChucVu2, value);
            }
        }

        [ModelDefault("Caption", "HSPC Kiêm nhiệm 2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVu3
        {
            get
            {
                return _HSPCChucVu3;
            }
            set
            {
                SetPropertyValue("HSPCChucVu3", ref _HSPCChucVu3, value);
            }
        }

        [ModelDefault("Caption", "HSPC CV Đảng")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
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
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
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
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
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

        [ModelDefault("Caption", "HSPC thi đua")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCThiDua
        {
            get
            {
                return _HSPCThiDua;
            }
            set
            {
                SetPropertyValue("HSPCThiDua", ref _HSPCThiDua, value);
            }
        }

        [ModelDefault("Caption", "HSPC chuyên môn")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Thâm niên công tác")]
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

        [ModelDefault("Caption", "PCTN công việc KN")]
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
        #endregion

        #region Thuế TNCN
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
        #endregion

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

        public ThongTinTinhTruyLinh(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<HoSo.ThongTinNhanVien>(Session);

            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

    }
}
