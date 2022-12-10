using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuaTrinh
{
    [ImageName("BO_QuaTrinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Diễn biến lương và phụ cấp")]
    [RuleCombinationOfPropertiesIsUnique("DienBienLuong.Identifier", DefaultContexts.Save, "ThongTinNhanVien,QuyetDinh", "Quyết định nâng lương đã tồn tại trong hệ thống")]

    public class DienBienLuong : TruongBaseObject
    {
        private decimal _PhuCapDienThoai;
        private decimal _PhuCapUuDaiCoHuu;
        private decimal _PhuCapTienAn;
        private decimal _PhuCapChucVu3;
        private decimal _PhuCapChucVu2;
        private decimal _PhuCapChucVu1;
        private int _PhuCapThuHut;
        private decimal _HSPCChucVuTruong;
        private decimal _HSPCThiDua;
        private decimal _HSPCHocVi;
        private decimal _HSPCUuDai;
        private int _PhuCapUuDai;
        private decimal _HSPCVuotKhung;
        private decimal _HSPCThamNien;
        private decimal _ThamNien;
        private QuyetDinh.QuyetDinh _QuyetDinh;       
        private ThongTinNhanVien _ThongTinNhanVien;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuong;
        private decimal _HeSoLuong;
        private decimal _ChenhLechBaoLuuLuong;
        private bool _Huong85PhanTramLuong;
        private int _VuotKhung;
        private decimal _HSPCChucVu;
        private decimal _HSPCTrachNhiem;
        private decimal _HSPCKiemNhiem;
        private decimal _HSPCLuuDong;
        private decimal _HSPCDocHai;
        private decimal _HSPCKhuVuc;
        private decimal _HSPCKhac;
        private int _PhuCapDacBiet;
        private int _PhuCapDacThu;
        private string _LyDo;
        private bool _NangLuongTruocHan;
        private decimal _TienTroCapChucVu;
        private decimal _TienTroCapKiemNhiem;
       
        private decimal _MucLuong; //NEU
        private decimal _ThuongHieuQuaTheoThang;
        private decimal _PhuCapTienXang;
        private decimal _PhuCapTrachNhiemCongViec;
        //BUH
        private decimal _SoThangKhongTinhThamNien;
        private int _PhuCapKiemNhiem;
        private decimal _PhuCapLaiXe;
        private decimal _HSPCChucVuBaoLuu;
        private ChucVu _ChucVu;
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;
        
        public DienBienLuong(Session session) : base(session) { }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
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
                        NgachLuong = value.NhanVienThongTinLuong.NgachLuong;
                        BacLuong = value.NhanVienThongTinLuong.BacLuong;
                        HeSoLuong = value.NhanVienThongTinLuong.HeSoLuong;
                        VuotKhung = value.NhanVienThongTinLuong.VuotKhung;
                        ThamNien = value.NhanVienThongTinLuong.ThamNien;            //Chưa có trong createDienBienLuong
                        Huong85PhanTramLuong = value.NhanVienThongTinLuong.Huong85PhanTramLuong;
                        HSPCChucVu = value.NhanVienThongTinLuong.HSPCChucVu;
                        HSPCDocHai = value.NhanVienThongTinLuong.HSPCDocHai;        //Chưa có trong createDienBienLuong
                        HSPCKhac = value.NhanVienThongTinLuong.HSPCKhac;            //Chưa có trong createDienBienLuong
                        PhuCapThuHut = value.NhanVienThongTinLuong.PhuCapThuHut;    //Chưa có trong createDienBienLuong
                        HSPCTrachNhiem = value.NhanVienThongTinLuong.HSPCTrachNhiem;
                        PhuCapUuDai = value.NhanVienThongTinLuong.PhuCapUuDai;      //Chưa có trong createDienBienLuong
                        HSPCKiemNhiem = value.NhanVienThongTinLuong.HSPCKiemNhiem;                      
                        PhuCapTienAn = value.NhanVienThongTinLuong.PhuCapTienAn;     //Chưa có trong createDienBienLuong                        
                }
            }
        }

        //[ImmediatePostData]
        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinh.QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
                if (!IsLoading && value != null)
                {
                    //LyDo = value.NoiDung;
                    SoQuyetDinh = value.SoQuyetDinh;
                    NgayQuyetDinh = value.NgayQuyetDinh;
                }
            }
        }

        [ModelDefault("Caption", "Số quyết định")]
        public string SoQuyetDinh
        {
            get
            {
                return _SoQuyetDinh;
            }
            set
            {
                SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày quyết định")]
        public DateTime NgayQuyetDinh
        {
            get
            {
                return _NgayQuyetDinh;
            }
            set
            {
                SetPropertyValue("NgayQuyetDinh", ref _NgayQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        #region Lương - Phụ cấp nhà nước
        [ModelDefault("Caption", "Ngạch lương")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong = 'LUH'")]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
            }
        }

        [DataSourceProperty("NgachLuong.ListBacLuong")]
        [ModelDefault("Caption", "Bậc lương")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong = 'LUH'")]
        [ImmediatePostData()]
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
            }
        }

        [ModelDefault("Caption", "Hưởng 85% mức lương")]
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

        [ModelDefault("Caption", "HSPC Chức vụ")]
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

        [ModelDefault("Caption", "HSPC Trách nhiệm")]
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

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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
            }
        }

        [ModelDefault("Caption", "HSPC ưu đãi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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

        [ModelDefault("Caption", "Phụ cấp thu hút")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
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

        [ModelDefault("Caption", "Chênh lệch bảo lưu lương")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
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

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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
            }
        }

        [ModelDefault("Caption", "HSPC vượt khung")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCVuotKhung
        {
            get
            {
                return _HSPCVuotKhung;
            }
            set
            {
                SetPropertyValue("HSPCVuotKhung", ref _HSPCVuotKhung, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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
            }
        }

        [ModelDefault("Caption", "HSPC thâm niên")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
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

        [ModelDefault("Caption", "Nâng lương trước hạn")]
        public bool NangLuongTruocHan
        {
            get
            {
                return _NangLuongTruocHan;
            }
            set
            {
                SetPropertyValue("NangLuongTruocHan", ref _NangLuongTruocHan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "% PC Kiêm nhiệm")]
        public int PhuCapKiemNhiem
        {
            get
            {
                return _PhuCapKiemNhiem;
            }
            set
            {
                SetPropertyValue("PhuCapKiemNhiem", ref _PhuCapKiemNhiem, value);
            }
        }
        #endregion

        #region phụ cấp trường
        [ModelDefault("Caption", "Phụ cấp tiền ăn")]
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

        [ModelDefault("Caption", "Phụ cấp điện thoại")]
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

        [ModelDefault("Caption", "HSPC chức vụ (trường)")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVuTruong
        {
            get
            {
                return _HSPCChucVuTruong;
            }
            set
            {
                SetPropertyValue("HSPCChucVuTruong", ref _HSPCChucVuTruong, value);
            }
        }

        [ModelDefault("Caption", "HSPC học vị")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCHocVi
        {
            get
            {
                return _HSPCHocVi;
            }
            set
            {
                SetPropertyValue("HSPCHocVi", ref _HSPCHocVi, value);
            }
        }

        #endregion
        
        [Size(8000)]
        [ModelDefault("Caption", "Lý do")]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }
                
        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (ThongTinNhanVien.NhanVien != null)
                ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(ThongTinNhanVien.NhanVien.Oid);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (QuyetDinh != null)
            {
                //LyDo = value.NoiDung;
                SoQuyetDinh = QuyetDinh.SoQuyetDinh;
                NgayQuyetDinh = QuyetDinh.NgayQuyetDinh;
            }
        }
    }

}
