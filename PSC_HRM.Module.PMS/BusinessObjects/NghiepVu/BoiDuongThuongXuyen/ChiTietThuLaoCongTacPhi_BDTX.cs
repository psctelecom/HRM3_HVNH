using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.CauHinh.HeSo;

namespace PSC_HRM.Module.PMS.NghiepVu.QuanLyBoiDuongThuongXuyen
{
    [ModelDefault("Caption", "Chi tiết thù lao công tác phí")]
    [DefaultProperty("ThongTin")]
    public class ChiTietThuLaoCongTacPhi_BDTX : BaseObject
    {
        private QuanLyBoiDuongThuongXuyen _QuanLyBoiDuongThuongXuyen;
        private DonViDaoTaoThuongXuyen _NoiGiangDay;
        private HocKy _HocKy;
        private ChuyenNganhDaoTao _NganhHocDT;
        private string _Khoa;
        private string _Lop;
        private string _HocKyNoiDay;
        private int _SiSo;
        private HocPhan_BoiDuongThuongXuyen _HocPhan;
        private decimal _SoDVHT_TinhChi;
        private int _SoTietLyThuyet;
        private int _SoTietThucHanh;
        private int _SoTietThaoLuan;
        private string _GhiChuHeSo;
        private NhanVien _NhanVien;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private decimal _DinhMucTienXe;
        private decimal _HeSoTienXe;
        private decimal _QuyDoiTienXe;
        private decimal _SoNgayLuuTru;
        private decimal _DonGiaLuuTru;
        private decimal _TongTienLuuTru;
        private decimal _TongTien;
        private string _GhiChuTong;
        private DateTime _TuNgayGDD;
        private DateTime _DenNgayGDD;

        [Association("QuanLyBoiDuongThuongXuyen-ListChiTietThuLaoCongTacPhi_BDTX")]
        [ModelDefault("Caption", "Quản lý")]
        [Browsable(false)]
        public QuanLyBoiDuongThuongXuyen QuanLyBoiDuongThuongXuyen
        {
            get { return _QuanLyBoiDuongThuongXuyen; }
            set { SetPropertyValue("QuanLyBoiDuongThuongXuyen", ref _QuanLyBoiDuongThuongXuyen, value); }
        }

        [ModelDefault("Caption", "Nơi giảng dạy")]
        public DonViDaoTaoThuongXuyen NoiGiangDay
        {
            get { return _NoiGiangDay; }
            set
            {
                SetPropertyValue("NoiGiangDay", ref _NoiGiangDay, value);
            }
        }
        [ModelDefault("Caption", "Học kỳ")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }

        [ModelDefault("Caption", "Ngành học")]
        public ChuyenNganhDaoTao NganhHocDT
        {
            get { return _NganhHocDT; }
            set { SetPropertyValue("NganhHocDT", ref _NganhHocDT, value); }
        }

        [ModelDefault("Caption", "Khóa học")]
        public string Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }

        [ModelDefault("Caption", "Lớp")]
        public string Lop
        {
            get { return _Lop; }
            set { SetPropertyValue("Lop", ref _Lop, value); }
        }

        [ModelDefault("Caption", "Học kỳ nơi dạy")]
        public string HocKyNoiDay
        {
            get { return _HocKyNoiDay; }
            set { SetPropertyValue("HocKyNoiDay", ref _HocKyNoiDay, value); }
        }

        [ModelDefault("Caption", "Sỉ số")]
        public int SiSo
        {
            get { return _SiSo; }
            set { SetPropertyValue("SiSo", ref _SiSo, value); }
        }

        [ModelDefault("Caption", "Học phần")]
        [DataSourceProperty("NganhHoc.ListHocPhan_BoiDuongThuongXuyen")]
        public HocPhan_BoiDuongThuongXuyen HocPhan
        {
            get { return _HocPhan; }
            set { SetPropertyValue("HocPhan", ref _HocPhan, value); }
        }

        [ModelDefault("Caption", "Số DVHT")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        public decimal SoDVHT_TinhChi
        {
            get { return _SoDVHT_TinhChi; }
            set
            {
                SetPropertyValue("SoDVHT_TinhChi", ref _SoDVHT_TinhChi, value);
            }
        }

        [ModelDefault("Caption", "Số tiết lý thuyết")]
        public int SoTietLyThuyet
        {
            get { return _SoTietLyThuyet; }
            set { SetPropertyValue("SoTietLyThuyet", ref _SoTietLyThuyet, value); }
        }

        [ModelDefault("Caption", "Số tiết thực hành")]
        public int SoTietThucHanh
        {
            get { return _SoTietThucHanh; }
            set { SetPropertyValue("SoTietThucHanh", ref _SoTietThucHanh, value); }
        }

        [ModelDefault("Caption", "Số tiết thảo luận")]
        public int SoTietThaoLuan
        {
            get { return _SoTietThaoLuan; }
            set { SetPropertyValue("SoTietThaoLuan", ref _SoTietThaoLuan, value); }
        }

        [ModelDefault("Caption", "Ghi chú hệ số")]
        public string GhiChuHeSo
        {
            get { return _GhiChuHeSo; }
            set { SetPropertyValue("GhiChuHeSo", ref _GhiChuHeSo, value); }
        }

        [ModelDefault("Caption", "Nhân viên")]
        [ModelDefault("AllowEdit", "false")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [ImmediatePostData]
        public DateTime TuNgay
        {
            get { return _TuNgay; }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if(value != null)
                {
                    TuNgayGDD = TuNgay.AddDays(1);
                }
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [ImmediatePostData]
        public DateTime DenNgay
        {
            get { return _DenNgay; }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (value != null)
                {
                    DenNgayGDD = DenNgay.AddDays(1);
                }
            }
        }

        [ModelDefault("Caption", "Định mức tiền xe")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        public decimal DinhMucTienXe
        {
            get { return _DinhMucTienXe; }
            set
            {
                SetPropertyValue("DinhMucTienXe", ref _DinhMucTienXe, value);
                if(value != 0)
                {
                    QuyDoiTienXe = HeSoTienXe * value;
                }
            }
        }

        [ModelDefault("Caption", "Hệ số tiền xe")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        public decimal HeSoTienXe
        {
            get { return _HeSoTienXe; }
            set
            {
                SetPropertyValue("HeSoTienXe", ref _HeSoTienXe, value);
                if (value != 0)
                {
                    QuyDoiTienXe = DinhMucTienXe * value;
                }
            }
        }

        [ModelDefault("Caption", "Quy đổi tiền xe")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        public decimal QuyDoiTienXe
        {
            get { return _QuyDoiTienXe; }
            set
            {
                SetPropertyValue("QuyDoiTienXe", ref _QuyDoiTienXe, value);
                if(value != 0)
                {
                    TongTien = TongTienLuuTru + value;
                }
            }
        }

        [ModelDefault("Caption", "Số ngày lưu trú")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ImmediatePostData]
        public decimal SoNgayLuuTru
        {
            get { return _SoNgayLuuTru; }
            set
            {
                SetPropertyValue("SoNgayLuuTru", ref _SoNgayLuuTru, value);
                if (DonGiaLuuTru != 0 && value != 0)
                {
                    TongTienLuuTru = value * DonGiaLuuTru;
                }
            }
        }
        [ModelDefault("Caption", "Đơn giá lưu trữ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        [Browsable(false)]
        public decimal DonGiaLuuTru
        {
            get { return _DonGiaLuuTru; }
            set
            {
                SetPropertyValue("DonGiaLuuTru", ref _DonGiaLuuTru, value);
                if (SoNgayLuuTru != 0 && value != 0)
                {
                    TongTienLuuTru = value * SoNgayLuuTru;
                }
            }
        }
        [ModelDefault("Caption", "Tổng tiền lưu trữ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        public decimal TongTienLuuTru
        {
            get { return _TongTienLuuTru; }
            set
            {
                SetPropertyValue("TongTienLuuTru", ref _TongTienLuuTru, value);
                if (value != 0)
                {
                    TongTien = QuyDoiTienXe + value;
                }
            }
        }

        [ModelDefault("Caption", "Tổng tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongTien
        {
            get { return _TongTien; }
            set
            {
                SetPropertyValue("TongTien", ref _TongTien, value);              
            }
        }

        [ModelDefault("Caption", "Ghi chú tổng")]
        public string GhiChuTong
        {
            get { return _GhiChuTong; }
            set { SetPropertyValue("GhiChuTong", ref _GhiChuTong, value); }
        }


        [ModelDefault("Caption", "Từ ngày GDD")]
        public DateTime TuNgayGDD
        {
            get { return _TuNgayGDD; }
            set { SetPropertyValue("TuNgayGDD", ref _TuNgayGDD, value); }
        }

        [ModelDefault("Caption", "Đến ngày GDD")]
        public DateTime DenNgayGDD
        {
            get { return _DenNgayGDD; }
            set { SetPropertyValue("DenNgayGDD", ref _DenNgayGDD, value); }
        }

        public ChiTietThuLaoCongTacPhi_BDTX(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}