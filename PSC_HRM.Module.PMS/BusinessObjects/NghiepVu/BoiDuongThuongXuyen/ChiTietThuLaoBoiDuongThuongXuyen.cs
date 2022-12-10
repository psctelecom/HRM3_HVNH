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
    [ModelDefault("Caption", "Chi tiết bồi dưỡng thường xuyên")]
    [DefaultProperty("ThongTin")]
    public class ChiTietThuLaoBoiDuongThuongXuyen : BaseObject
    {
        private QuanLyBoiDuongThuongXuyen _QuanLyBoiDuongThuongXuyen;
        private DonViDaoTaoThuongXuyen _NoiGiangDay;
        private HocKy _HocKy;
        private ChuyenNganhDaoTao _NganhHocDT;
        private string _Khoa;
        private string _Lop;
        private string _HocKyNoiDay;
        private int _SiSo;
        private int _SoNhom;
        private HocPhan_BoiDuongThuongXuyen _HocPhan;
        private decimal _SoDVHT_TinhChi;
        private int _SoTietLyThuyet;
        private int _SoTietThucHanh;
        private int _SoTietThaoLuan;
        private string _GhiChu;
        private NhanVien _NhanVien;
        private decimal _SoGioTamUng;
        private string _PhuongThucDaoTao;
        private string _HinhThucThi;
        private int _SoBaiThucHanh;
        private decimal _HeSoLopDongK1;
        private decimal _HeSoTinhChiK2;
        private decimal _HeSoGDK5;
        private decimal _TongGioLyThuyetA2;
        private decimal _HeSoThucHanh;
        private decimal _QuyDoiTietThucHanh;
        private decimal _QuyDoiBTL;
        private decimal _QuyDoiDA_TT_TH;
        private decimal _ThucTap_ThiNghiem;
        private decimal _RaDe;
        private decimal _ChamBaiThiThucHanh;
        private decimal _TongGioKhacA1;
        private decimal _TongGio;


        [Association("QuanLyBoiDuongThuongXuyen-ListChiTietThuLaoBoiDuongThuongXuyen")]
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
                if(!IsLoading && value != null)
                {
                    HeSoGDK5 = value.HeSoCoSo.HeSo_CoSo;
                }
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

        [ModelDefault("Caption", "Số nhóm")]
        public int SoNhom
        {
            get { return _SoNhom; }
            set { SetPropertyValue("SoNhom", ref _SoNhom, value); }
        }

        [ModelDefault("Caption", "Học phần")]
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
                if (!IsLoading && value != 0)
                {
                    SoGioTamUng = value * 15;
                }
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

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Nhân viên")]
        [ModelDefault("AllowEdit", "false")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("Caption", "Số giờ tạm ứng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioTamUng
        {
            get { return _SoGioTamUng; }
            set { SetPropertyValue("SoGioTamUng", ref _SoGioTamUng, value); }
        }

        [ModelDefault("Caption", "Phương thức đào tạo")]
        public string PhuongThucDaoTao
        {
            get { return _PhuongThucDaoTao; }
            set { SetPropertyValue("PhuongThucDaoTao", ref _PhuongThucDaoTao, value); }
        }

        [ModelDefault("Caption", "Hình thức thi")]
        public string HinhThucThi
        {
            get { return _HinhThucThi; }
            set { SetPropertyValue("HinhThucThi", ref _HinhThucThi, value); }
        }

        [ModelDefault("Caption", "Số bài thực hành")]
        public int SoBaiThucHanh
        {
            get { return _SoBaiThucHanh; }
            set { SetPropertyValue("SoBaiThucHanh", ref _SoBaiThucHanh, value); }
        }

        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("Caption", "Hệ số lớp đông K1")]
        public decimal HeSoLopDongK1
        {
            get { return _HeSoLopDongK1; }
            set { SetPropertyValue("HeSoLopDongK1", ref _HeSoLopDongK1, value); }
        }

        [ModelDefault("Caption", "Hệ số đào tạo K2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal HeSoTinhChiK2
        {
            get { return _HeSoTinhChiK2; }
            set { SetPropertyValue("HeSoTinhChiK2", ref _HeSoTinhChiK2, value); }
        }

        [ModelDefault("Caption", "Hệ số giáo dục K5")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal HeSoGDK5
        {
            get { return _HeSoGDK5; }
            set { SetPropertyValue("HeSoGDK5", ref _HeSoGDK5, value); }
        }

        [ModelDefault("Caption", "Tổng giờ lý thuyết A2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [ImmediatePostData]
        public decimal TongGioLyThuyetA2
        {
            get { return _TongGioLyThuyetA2; }
            set
            {
                SetPropertyValue("TongGioLyThuyetA2", ref _TongGioLyThuyetA2, value);
                if(!IsLoading && value != 0 && TongGioKhacA1 != 0)
                {
                    TongGio = TongGioKhacA1 + TongGioLyThuyetA2;
                }
            }
        }

        [ModelDefault("Caption", "Hệ số thực hành")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal HeSoThucHanh
        {
            get { return _HeSoThucHanh; }
            set { SetPropertyValue("HeSoThucHanh", ref _HeSoThucHanh, value); }
        }

        [ModelDefault("Caption", "Số tiết TH x hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal QuyDoiTietThucHanh
        {
            get { return _QuyDoiTietThucHanh; }
            set { SetPropertyValue("QuyDoiTietThucHanh", ref _QuyDoiTietThucHanh, value); }
        }

        [ModelDefault("Caption", "BTL")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal QuyDoiBTL
        {
            get { return _QuyDoiBTL; }
            set { SetPropertyValue("QuyDoiBTL", ref _QuyDoiBTL, value); }
        }

        [ModelDefault("Caption", "ĐA, TT, TH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal QuyDoiDA_TT_TH
        {
            get { return _QuyDoiDA_TT_TH; }
            set { SetPropertyValue("QuyDoiDA_TT_TH", ref _QuyDoiDA_TT_TH, value); }
        }

        [ModelDefault("Caption", "TT, TN")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal ThucTap_ThiNghiem
        {
            get { return _ThucTap_ThiNghiem; }
            set { SetPropertyValue("ThucTap_ThiNghiem", ref _ThucTap_ThiNghiem, value); }
        }

        [ModelDefault("Caption", "Ra đề")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal RaDe
        {
            get { return _RaDe; }
            set { SetPropertyValue("RaDe", ref _RaDe, value); }
        }
      
        [ModelDefault("Caption", "Chấm bài thi HP")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal ChamBaiThiThucHanh
        {
            get { return _ChamBaiThiThucHanh; }
            set { SetPropertyValue("ChamBaiThiThucHanh", ref _ChamBaiThiThucHanh, value); }
        }

        [ModelDefault("Caption", "Tổng giờ khác A1")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [ImmediatePostData]
        public decimal TongGioKhacA1
        {
            get { return _TongGioKhacA1; }
            set
            {
                SetPropertyValue("NoiGiangDay", ref _TongGioKhacA1, value);
                if (!IsLoading && value != 0 && TongGioLyThuyetA2 != 0)
                {
                    TongGio = TongGioKhacA1 + TongGioLyThuyetA2;
                }
            }
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
        public ChiTietThuLaoBoiDuongThuongXuyen(Session session)
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