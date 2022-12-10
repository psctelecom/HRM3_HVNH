using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    public class Report_PMS_ThanhToanThuLaoGiangDay_HopDong_Non : BaseObject
    {
        private bool _Chon;
        private string _MaGV;
        private string _DonVi;
        private string _HoTen;
        private string _ChucDanh;
        private string _HocHam;
        private string _HocVi;
        private string _LoaiGV;
        private string _MaHocPhan;
        private string _TenHocPhan;
        private string _LoaiHocPhan;
        private int _SiSo;
        private int _SoTinhChi;
        private decimal _SoTietThucDay;
        private decimal _TietQuyDoi;
        private decimal _DonGia;
        private decimal _ThanhTienChiTiet;
        private string _TenBoPhan;
        private string _TenNamhoc;
        private string _TenHocKy;
        private decimal _SoTietLT;
        private decimal _SoTietTH;
        private decimal _SoTietKhac;
        private string _ThoiGiangDay;
        private string _DiaDiemDay;

        [ModelDefault("Caption", "Chọn")]
        [ModelDefault("AllowEdit", "True")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }

        [ModelDefault("Caption","Mã giảng viên")]
        [ModelDefault("AllowEdit", "False")]
        public string MaGV
        {
            get { return _MaGV; }
            set { SetPropertyValue("MaGV", ref _MaGV, value); }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [ModelDefault("AllowEdit", "False")]
        public string DonVi
        {
            get { return _DonVi; }
            set { SetPropertyValue("DonVi", ref _DonVi, value); }
        }

        [ModelDefault("Caption", "Họ tên")]
        [ModelDefault("AllowEdit", "False")]
        public string HoTen
        {
            get { return _HoTen; }
            set { SetPropertyValue("HoTen", ref _HoTen, value); }
        }

        [ModelDefault("Caption", "Chức danh")]
        [ModelDefault("AllowEdit", "False")]
        public string ChucDanh
        {
            get { return _ChucDanh; }
            set { SetPropertyValue("ChucDanh,", ref _ChucDanh, value); }
        }

        [ModelDefault("Caption", "Học Hàm")]
        [ModelDefault("AllowEdit", "False")]
        public string HocHam
        {
            get { return _HocHam; }
            set { SetPropertyValue("HocHam", ref _HocHam, value); }
        }

        [ModelDefault("Caption", "Học vị")]
        [ModelDefault("AllowEdit", "False")]
        public string HocVi
        {
            get { return _HocVi; }
            set { SetPropertyValue("HocVi", ref _HocVi, value); }
        }

        [ModelDefault("Caption", "Loại giảng vên")]
        [ModelDefault("AllowEdit", "False")]
        public string LoaiGV
        {
            get { return _LoaiGV; }
            set { SetPropertyValue("LoaiGV", ref _LoaiGV, value); }
        }

        [ModelDefault("Caption", "Mã học phần")]
        [ModelDefault("AllowEdit", "False")]
        public string MaHocPhan
        {
            get { return _MaHocPhan; }
            set { SetPropertyValue("MaHocPhan", ref _MaHocPhan, value); }
        }

        [ModelDefault("Caption", "Tên học phần")]
        [ModelDefault("AllowEdit", "False")]
        public string TenHocPhan
        {
            get { return _TenHocPhan; }
            set { SetPropertyValue("TenHocPhan", ref _TenHocPhan, value); }
        }

        [ModelDefault("Caption", "Loai học phần")]
        [ModelDefault("AllowEdit", "False")]
        public string LoaiHocPhan
        {
            get { return _LoaiHocPhan; }
            set { SetPropertyValue("LoaiHocPhan", ref _LoaiHocPhan, value); }
        }

        [ModelDefault("Caption", "Sỉ số")]
        [ModelDefault("AllowEdit", "False")]
        public int SiSo
        {
            get { return _SiSo; }
            set { SetPropertyValue("SiSo", ref _SiSo, value); }
        }

        [ModelDefault("Caption", "Số TC")]
        [ModelDefault("AllowEdit", "False")]
        public int SoTinhChi
        {
            get { return _SoTinhChi; }
            set { SetPropertyValue("SoTinhChi", ref _SoTinhChi, value); }
        }

        [ModelDefault("Caption", "Số tiết thực dạy")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoTietThucDay
        {
            get
            {
                return _SoTietThucDay;
            }
            set
            {
                SetPropertyValue("SoTietThucDay", ref _SoTietThucDay, value);
            }
        }

        [ModelDefault("Caption", "Tiết quy đổi")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TietQuyDoi
        {
            get
            {
                return _TietQuyDoi;
            }
            set
            {
                SetPropertyValue("TietQuyDoi", ref _TietQuyDoi, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DonGia
        {
            get
            {
                return _DonGia;
            }
            set
            {
                SetPropertyValue("DonGia", ref _DonGia, value);
            }
        }

        [ModelDefault("Caption", "Thành tiền chi tiết")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ThanhTienChiTiet
        {
            get
            {
                return _ThanhTienChiTiet;
            }
            set
            {
                SetPropertyValue("ThanhTienChiTiet", ref _ThanhTienChiTiet, value);
            }
        }

        [ModelDefault("Caption", "Tên bộ phận")]
        [ModelDefault("AllowEdit", "False")]
        public string TenBoPhan
        {
            get { return _TenBoPhan; }
            set { SetPropertyValue("TenBoPhan", ref _TenBoPhan, value); }
        }

        [ModelDefault("Caption", "Tên Năm Học")]
        [Browsable(false)]
        [ModelDefault("AllowEdit", "False")]
        public string TenNamhoc
        {
            get { return _TenNamhoc; }
            set { SetPropertyValue("TenNamhoc", ref _TenNamhoc, value); }
        }
        [ModelDefault("Caption", "Tên Học Kỳ")]
        [Browsable(false)]
        [ModelDefault("AllowEdit", "False")]
        public string TenHocKy
        {
            get { return _TenHocKy; }
            set { SetPropertyValue("TenHocKy", ref _TenHocKy, value); }
        }

        [ModelDefault("Caption", "Số tiết lý thuyết")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoTietLT
        {
            get
            {
                return _SoTietLT;
            }
            set
            {
                SetPropertyValue("SoTietLT", ref _SoTietLT, value);
            }
        }

        [ModelDefault("Caption", "Số tiết thực hành")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoTietTH
        {
            get
            {
                return _SoTietTH;
            }
            set
            {
                SetPropertyValue("SoTietTH", ref _SoTietTH, value);
            }
        }

        [ModelDefault("Caption", "Số tiết khác")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoTietKhac
        {
            get
            {
                return _SoTietKhac;
            }
            set
            {
                SetPropertyValue("SoTietKhac", ref _SoTietKhac, value);
            }
        }

        [ModelDefault("Caption", "Thời gian dạy")]
        [ModelDefault("AllowEdit", "False")]
        public string ThoiGiangDay
        {
            get { return _ThoiGiangDay; }
            set { SetPropertyValue("ThoiGiangDay", ref _ThoiGiangDay, value); }
        }

        [ModelDefault("Caption", "Địa điểm dạy")]
        [ModelDefault("AllowEdit", "False")]
        public string DiaDiemDay
        {
            get { return _DiaDiemDay; }
            set { SetPropertyValue("DiaDiemDay", ref _DiaDiemDay, value); }
        }
        public Report_PMS_ThanhToanThuLaoGiangDay_HopDong_Non(Session session)
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