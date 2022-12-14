using System;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.BoNhiem;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Collections.Generic;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định bổ nhiệm")]
    //[Appearance("QuyetDinhBoNhiem.NgoaiTruong",TargetItems = "BoPhanCu;BoPhanMoi",Visibility = ViewItemVisibility.Hide,Criteria =  "CoQuanRaQuyetDinh=1")]
    //[Appearance("QuyetDinhBoNhiem.TrongTruong", TargetItems = "BoPhan1;BoPhanMoi1", Visibility = ViewItemVisibility.Hide, Criteria = "CoQuanRaQuyetDinh=0")]
    [Appearance("Hide_NEU", TargetItems = "HSPCTrachNhiemTruong;NgayHuongHSPCTrachNhiemTruong;HSPCChuyenMon;NgayHuongHSPCChuyenMon;HSPCThamNienTruong;HSPCLanhDaoMoi;HSPCLanhDaoCu;HSPCKiemNhiemTrongTruongCu;HSPCKiemNhiemTrongTruongMoi", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'NEU'")]
    [Appearance("Hide_KhacQNU", TargetItems = "NamNhiemKy;TaiBoMon", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'QNU'")]
    [Appearance("Hide_KhacNEU", TargetItems = "BoPhanMoiText;LoaiBoNhiem", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'NEU'")]
  
    public class QuyetDinhBoNhiem : QuyetDinhCaNhan
    {
        private decimal _HSPCChucVu1Moi;
        private decimal _HSPCChucVu1Cu;
        private bool _HetHieuLuc;
        private CongViec _CongViecCu;
        private LoaiNhanSu _LoaiNhanSuCu;
        private DateTime _NgayPhatSinhBienDong;
        private ChucVu _ChucVuCu;
        private ChucVu _ChucVuMoi;
        private decimal _HSPCChucVuCu;
        private decimal _HSPCChucVuMoi;
        private DateTime _NgayHuongHeSoCu;
        private DateTime _NgayThoiHuongHeSoCu;
        private DateTime _NgayHuongHeSoMoi;
        private DateTime _NgayHetNhiemKy;
        private bool _QuyetDinhMoi;
        private ChucVu _ChucVuCaoNhatDaQua;
        private BoPhan _BoPhanMoi;
        private string _BoPhanMoiText;
        private NamHoc _NamHoc;
        private int _NhiemKy;
        //
        private bool _DuocHuongPhuCapChuyenVien;
        private bool _DuocHuongPhuCapChuyenVienCu;
        private DateTime _NgayBoNhiemCu;
        private DateTime _NgayHuongHSPCChuyenMon;
        private DateTime _NgayHuongHSPCChuyenMonCu;
        private decimal _HSPCTrachNhiemTruong;
        private decimal _HSPCTrachNhiemTruongCu;
        private DateTime _NgayHuongHSPCTrachNhiemTruong;
        private DateTime _NgayHuongHSPCTrachNhiemTruongCu;
        private DateTime _NgayThoiHuongHSPCTrachNhiemTruong;//QNU
        private decimal _HSPCThamNienTruong;
        private decimal _HSPCThamNienTruongCu;
        private decimal _HSPCChuyenMon;
        private decimal _HSPCChuyenMonCu;
        private decimal _HSPCLanhDaoMoi;
        private decimal _HSPCLanhDaoCu;
        private DateTime _NgayBoNhiem;
        private decimal _HSPCKiemNhiemTrongTruongCu;
        private decimal _HSPCKiemNhiemTrongTruongMoi;
        //QNU
        private string _NamNhiemKy;
        private BoPhan _TaiBoMon;
        //NEU
        private LoaiBoNhiem _LoaiBoNhiem;

        [ModelDefault("Caption", "Năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }
        
        [Browsable(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Ngày phát sinh biến động")]
        public DateTime NgayPhatSinhBienDong
        {
            get
            {
                return _NgayPhatSinhBienDong;
            }
            set
            {
                SetPropertyValue("NgayPhatSinhBienDong", ref _NgayPhatSinhBienDong, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ cũ")]
        public ChucVu ChucVuCu
        {
            get
            {
                return _ChucVuCu;
            }
            set
            {
                SetPropertyValue("ChucVuCu", ref _ChucVuCu, value);
            }
        }

        [ModelDefault("Caption", "HSPC chức vụ cũ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVuCu
        {
            get
            {
                return _HSPCChucVuCu;
            }
            set
            {
                SetPropertyValue("HSPCChucVuCu", ref _HSPCChucVuCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng HSPC chức vụ cũ")]
        public DateTime NgayHuongHeSoCu
        {
            get
            {
                return _NgayHuongHeSoCu;
            }
            set
            {
                SetPropertyValue("NgayHuongHeSoCu", ref _NgayHuongHeSoCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày thôi hưởng HSPC chức vụ cũ")]
        public DateTime NgayThoiHuongHeSoCu
        {
            get
            {
                return _NgayThoiHuongHeSoCu;
            }
            set
            {
                SetPropertyValue("NgayThoiHuongHeSoCu", ref _NgayThoiHuongHeSoCu, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị mới")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhanMoi
        {
            get
            {
                return _BoPhanMoi;
            }
            set
            {
                SetPropertyValue("BoPhanMoi", ref _BoPhanMoi, value);
                if (!IsLoading)
                {
                    if (value != null)
                        BoPhanMoiText = value.TenBoPhan;
                    else
                        BoPhanMoiText = string.Empty;
                }
            }
        }
        [ModelDefault("Caption", "Tại bộ môn")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public BoPhan TaiBoMon
        {
            get
            {
                return _TaiBoMon;
            }
            set
            {
                SetPropertyValue("TaiBoMon", ref _TaiBoMon, value);
            }
        }
        [ModelDefault("Caption", "Loại bổ nhiệm")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public LoaiBoNhiem LoaiBoNhiem
        {
            get
            {
                return _LoaiBoNhiem;
            }
            set
            {
                SetPropertyValue("LoaiBoNhiem", ref _LoaiBoNhiem, value);
            }
        }
        [ModelDefault("Caption", "Đơn vị mới")]
        public string BoPhanMoiText
        {
            get
            {
                return _BoPhanMoiText;
            }
            set
            {
                SetPropertyValue("BoPhanMoiText", ref _BoPhanMoiText, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVu ChucVuMoi
        {
            get
            {
                return _ChucVuMoi;
            }
            set
            {
                SetPropertyValue("ChucVuMoi", ref _ChucVuMoi, value);
                if (!IsLoading && value != null)
                {
                    XuLy();
                }
            }
        }

        [ModelDefault("Caption", "HSPC chức vụ mới")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVuMoi
        {
            get
            {
                return _HSPCChucVuMoi;
            }
            set
            {
                SetPropertyValue("HSPCChucVuMoi", ref _HSPCChucVuMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày hưởng HSPC chức vụ mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongHeSoMoi
        {
            get
            {
                return _NgayHuongHeSoMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongHeSoMoi", ref _NgayHuongHeSoMoi, value);
                if (!IsLoading && NhiemKy > 0 && value != DateTime.MinValue)
                { 
                    NgayHetNhiemKy = NgayHuongHeSoMoi.AddYears(NhiemKy).AddDays(-1);
                    NgayPhatSinhBienDong = value;
                }                   
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số năm nhiệm kỳ")]
        public int NhiemKy
        {
            get
            {
                return _NhiemKy;
            }
            set
            {
                SetPropertyValue("NhiemKy", ref _NhiemKy, value);
                if (!IsLoading && NhiemKy > 0 && NgayHieuLuc != DateTime.MinValue)
                    NgayHetNhiemKy = NgayHuongHeSoMoi.AddYears(NhiemKy).AddDays(-1);
            }
        }

        [ModelDefault("Caption", "Năm nhiệm kỳ")]
        public string NamNhiemKy
        {
            get
            {
                return _NamNhiemKy;
            }
            set
            {
                SetPropertyValue("NamNhiemKy", ref _NamNhiemKy, value);
               
            }
        }

        [ModelDefault("Caption", "Ngày hết nhiệm kỳ")]
        public DateTime NgayHetNhiemKy
        {
            get
            {
                return _NgayHetNhiemKy;
            }
            set
            {
                SetPropertyValue("NgayHetNhiemKy", ref _NgayHetNhiemKy, value);
            }
        }

        [ModelDefault("Caption", "Quyết định còn hiệu lực")]
        public bool QuyetDinhMoi
        {
            get
            {
                return _QuyetDinhMoi;
            }
            set
            {
                SetPropertyValue("QuyetDinhMoi", ref _QuyetDinhMoi, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Loại nhân sự cũ")]
        public LoaiNhanSu LoaiNhanSuCu
        {
            get
            {
                return _LoaiNhanSuCu;
            }
            set
            {
                SetPropertyValue("LoaiNhanSuCu", ref _LoaiNhanSuCu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Công việc cũ")]
        public CongViec CongViecCu
        {
            get
            {
                return _CongViecCu;
            }
            set
            {
                SetPropertyValue("CongViecCu", ref _CongViecCu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Chức vụ cao nhất đã qua")]
        public ChucVu ChucVuCaoNhatDaQua
        {
            get
            {
                return _ChucVuCaoNhatDaQua;
            }
            set
            {
                SetPropertyValue("ChucVuCaoNhatDaQua", ref _ChucVuCaoNhatDaQua, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "HSPC quản lý")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVu1Cu
        {
            get
            {
                return _HSPCChucVu1Cu;
            }
            set
            {
                SetPropertyValue("HSPCChucVu1Cu", ref _HSPCChucVu1Cu, value);
            }
        }

        [ModelDefault("Caption", "HSPC quản lý")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVu1Moi
        {
            get
            {
                return _HSPCChucVu1Moi;
            }
            set
            {
                SetPropertyValue("HSPCChucVu1Moi", ref _HSPCChucVu1Moi, value);
            }
        }

        [Browsable(false)]
        public bool HetHieuLuc
        {
            get
            {
                return _HetHieuLuc;
            }
            set
            {
                SetPropertyValue("HetHieuLuc", ref _HetHieuLuc, value);
            }
        }

        [ModelDefault("Caption", "Được hưởng phụ cấp chuyên viên")]
        public bool DuocHuongPhuCapChuyenVien
        {
            get
            {
                return _DuocHuongPhuCapChuyenVien;
            }
            set
            {
                SetPropertyValue("DuocHuongPhuCapChuyenVien", ref _DuocHuongPhuCapChuyenVien, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Được hưởng phụ cấp chuyên viên cũ")]
        public bool DuocHuongPhuCapChuyenVienCu
        {
            get
            {
                return _DuocHuongPhuCapChuyenVienCu;
            }
            set
            {
                SetPropertyValue("DuocHuongPhuCapChuyenVienCu", ref _DuocHuongPhuCapChuyenVienCu, value);
            }
        }
        [ModelDefault("Caption", "Ngày bổ nhiệm cũ")]
        public DateTime NgayBoNhiemCu
        {
            get
            {
                return _NgayBoNhiemCu;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemCu", ref _NgayBoNhiemCu, value);
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
        [Browsable(false)]
        [ModelDefault("Caption", "Ngày hưởng HSPC chuyên môn cũ ")]
        public DateTime NgayHuongHSPCChuyenMonCu
        {
            get
            {
                return _NgayHuongHSPCChuyenMonCu;
            }
            set
            {
                SetPropertyValue("NgayHuongHSPCChuyenMonCu", ref _NgayHuongHSPCChuyenMonCu, value);
            }
        }

        [ModelDefault("Caption", "HSPC trách nhiệm ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCTrachNhiemTruong
        {
            get
            {
                return _HSPCTrachNhiemTruong;
            }
            set
            {
                SetPropertyValue("HSPCTrachNhiemTruong", ref _HSPCTrachNhiemTruong, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "HSPC trách nhiệm cũ ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCTrachNhiemTruongCu
        {
            get
            {
                return _HSPCTrachNhiemTruongCu;
            }
            set
            {
                SetPropertyValue("HSPCTrachNhiemTruongCu", ref _HSPCTrachNhiemTruongCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng HSPC trách nhiệm ")]
        public DateTime NgayHuongHSPCTrachNhiemTruong
        {
            get
            {
                return _NgayHuongHSPCTrachNhiemTruong;
            }
            set
            {
                SetPropertyValue("NgayHuongHSPCTrachNhiemTruong", ref _NgayHuongHSPCTrachNhiemTruong, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Ngày hưởng HSPC trách nhiệm cũ ")]
        public DateTime NgayHuongHSPCTrachNhiemTruongCu
        {
            get
            {
                return _NgayHuongHSPCTrachNhiemTruongCu;
            }
            set
            {
                SetPropertyValue("NgayHuongHSPCTrachNhiemTruongCu", ref _NgayHuongHSPCTrachNhiemTruongCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày thôi hưởng HSPC trách nhiệm ")]
        public DateTime NgayThoiHuongHSPCTrachNhiemTruong
        {
            get
            {
                return _NgayThoiHuongHSPCTrachNhiemTruong;
            }
            set
            {
                SetPropertyValue("NgayThoiHuongHSPCTrachNhiemTruong", ref _NgayThoiHuongHSPCTrachNhiemTruong, value);
            }
        }

        [ModelDefault("Caption", "HSPC thâm niên trường")]
        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        public decimal HSPCThamNienTruong
        {
            get
            {
                return _HSPCThamNienTruong;
            }
            set
            {
                SetPropertyValue("HSPCThamNienTruong", ref _HSPCThamNienTruong, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "HSPC thâm niên trường cũ")]
        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        public decimal HSPCThamNienTruongCu
        {
            get
            {
                return _HSPCThamNienTruongCu;
            }
            set
            {
                SetPropertyValue("HSPCThamNienTruongCu", ref _HSPCThamNienTruongCu, value);
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
        [Browsable(false)]
        [ModelDefault("Caption", "HSPC chuyên môn cũ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChuyenMonCu
        {
            get
            {
                return _HSPCChuyenMonCu;
            }
            set
            {
                SetPropertyValue("HSPCChuyenMonCu", ref _HSPCChuyenMonCu, value);
            }
        }

        [ModelDefault("Caption", "HSPC lãnh đạo mới")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCLanhDaoMoi
        {
            get
            {
                return _HSPCLanhDaoMoi;
            }
            set
            {
                SetPropertyValue("HSPCLanhDaoMoi", ref _HSPCLanhDaoMoi, value);
            }
        }

        [ModelDefault("Caption", "HSPC lãnh đạo cũ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCLanhDaoCu
        {
            get
            {
                return _HSPCLanhDaoCu;
            }
            set
            {
                SetPropertyValue("HSPCLanhDaoCu", ref _HSPCLanhDaoCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm")]
        public DateTime NgayBoNhiem
        {
            get
            {
                return _NgayBoNhiem;
            }
            set
            {
                SetPropertyValue("NgayBoNhiem", ref _NgayBoNhiem, value);
            }
        }

        [ModelDefault("Caption", "HSPC kiêm nhiệm (trường) cũ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCKiemNhiemTrongTruongCu
        {
            get
            {
                return _HSPCKiemNhiemTrongTruongCu;
            }
            set
            {
                SetPropertyValue("HSPCKiemNhiemTrongTruongCu", ref _HSPCKiemNhiemTrongTruongCu, value);
            }
        }


        [ModelDefault("Caption", "HSPC kiêm nhiệm (trường) mới")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCKiemNhiemTrongTruongMoi
        {
            get
            {
                return _HSPCKiemNhiemTrongTruongMoi;
            }
            set
            {
                SetPropertyValue("HSPCKiemNhiemTrongTruongMoi", ref _HSPCKiemNhiemTrongTruongMoi, value);
            }
        }

        public QuyetDinhBoNhiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhBoNhiem;

            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định bổ nhiệm"));
        }

        protected override void QuyetDinhChanged()
        {
            if (NgayHieuLuc != DateTime.MinValue)
                NgayPhatSinhBienDong = NgayHieuLuc;
        }

        protected override void AfterNhanVienChanged()
        {
            ChucVuCu = ThongTinNhanVien.ChucVu;
            HSPCChucVuCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu;
            HSPCLanhDaoCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCLanhDao;
            //
            NgayBoNhiemCu = ThongTinNhanVien.NgayBoNhiem;
            NgayHuongHeSoCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCChucVu;
            ChucVuCaoNhatDaQua = ThongTinNhanVien.ChucVuCoQuanCaoNhat;
            DuocHuongPhuCapChuyenVienCu = ThongTinNhanVien.NhanVienThongTinLuong.DuocHuongHSPCChuyenVien;
            DuocHuongPhuCapChuyenVien = ThongTinNhanVien.NhanVienThongTinLuong.DuocHuongHSPCChuyenVien;
            HSPCChuyenMonCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCChuyenMon;
            HSPCChuyenMon = ThongTinNhanVien.NhanVienThongTinLuong.HSPCChuyenMon;
            NgayHuongHSPCChuyenMonCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCChuyenMon;
            HSPCThamNienTruongCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNienTrongTruong;
            HSPCThamNienTruong = ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNienTrongTruong;
            HSPCTrachNhiemTruongCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCTrachNhiem;
            HSPCTrachNhiemTruong = ThongTinNhanVien.NhanVienThongTinLuong.HSPCTrachNhiem; ;
            NgayHuongHSPCTrachNhiemTruongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem;

            if (GiayToHoSo != null)
                GiayToHoSo.HoSo = ThongTinNhanVien;

            LoaiNhanSuCu = ThongTinNhanVien.LoaiNhanSu;
            CongViecCu = ThongTinNhanVien.CongViecHienNay;
        }

        protected override void OnLoaded()
        {
            base.OnLoading();
            //if (GiayToHoSo == null)
            //{
            //    GiayToList = ThongTinNhanVien.ListGiayToHoSo;
            //    if (GiayToList.Count > 0 && SoQuyetDinh != null)
            //    {
            //        GiayToList.Criteria = CriteriaOperator.Parse("GiayTo like ? and SoGiayTo = ?", "Quyết định bổ nhiệm", SoQuyetDinh);
            //        if (GiayToList.Count > 0)
            //            GiayToHoSo = Session.FindObject<GiayToHoSo>(CriteriaOperator.Parse("Oid = ?", GiayToList[0].Oid));
            //    }
            //}
            MaTruong = TruongConfig.MaTruong;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            if(TruongConfig.MaTruong.Equals("QNU"))
            { 
                if (!IsDeleted)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                        ThongTinNhanVien.Oid);
                    HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);

                    //cho phép lập quyết định bổ nhiệm lại mà không cần lập quyết định miễn nhiệm trước đó.
                    //để tránh có 2 dòng trong quản lý biến động (1 dòng biến động giảm mức đóng, 1 dòng biến động tăng mức đóng)
                    if (QuyetDinhMoi
                        && !HetHieuLuc
                        //&& ChucVuCu != ChucVuMoi
                        )
                    {
                        ThongTinNhanVien.BoPhan = BoPhanMoi;
                        ThongTinNhanVien.ChucVu = ChucVuMoi;
                        ThongTinNhanVien.NgayBoNhiem = NgayHieuLuc;
                       // ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = HSPCChucVuMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCChucVu = NgayHuongHeSoMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCLanhDao = HSPCLanhDaoMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.DuocHuongHSPCChuyenVien = false;
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChuyenMon = 0;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCChuyenMon = DateTime.MinValue;
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNienTrongTruong = 0;
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = HSPCTrachNhiemTruong;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem = NgayHuongHSPCTrachNhiemTruong;

                        if (HSPCChucVuCu > HSPCChucVuMoi)
                        {
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu = HSPCChucVuCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = 0;
                            ThongTinNhanVien.NhanVienThongTinLuong.NgayHetHanHuongHSPChucVuBaoLuu = NgayThoiHuongHeSoCu;
                        }
                        else
                        {
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu =HSPCChucVuMoi;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu = 0;
                            ThongTinNhanVien.NhanVienThongTinLuong.NgayHetHanHuongHSPChucVuBaoLuu = DateTime.MinValue;
                        }

                        //chi phat sinh bien dong khi nhan vien do tham gia bao hiem
                        if (hoSoBaoHiem != null
                            && NgayPhatSinhBienDong != DateTime.MinValue)
                        {
                            //biến động thay đổi mức đóng
                            BienDongHelper.CreateBienDongThayDoiLuong(Session, this, NgayPhatSinhBienDong,
                                ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong, HSPCChucVuMoi,
                                ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung,
                                ThongTinNhanVien.NhanVienThongTinLuong.ThamNien,
                                ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac,
                                ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong);

                            //Biến động thay đổi chức danh
                            BienDongHelper.CreateBienDongThayDoiChucDanh(Session, this, NgayPhatSinhBienDong, string.Format("{0} {1}", ChucVuMoi.TenChucVu, BoPhan.TenBoPhan));
                        }

                        //Lần bổ nhiệm
                        object count = Session.Evaluate<QuaTrinhBoNhiem>(CriteriaOperator.Parse("Count()"),
                            CriteriaOperator.Parse("ThongTinNhanVien=? and ChucVu=? and QuyetDinh!=?",
                            ThongTinNhanVien, ChucVuMoi, this));
                        if (count != null)
                            ThongTinNhanVien.LanBoNhiemChucVu = (int)count + 1;
                        else
                            ThongTinNhanVien.LanBoNhiemChucVu = 1;

                        //cần thay đổi thông tin chức vụ cao nhất đã qua ở đây
                        if (ChucVuCaoNhatDaQua == null ||
                            (ChucVuCaoNhatDaQua.HSPCChucVu < ChucVuMoi.HSPCChucVu))
                            ThongTinNhanVien.ChucVuCoQuanCaoNhat = ChucVuMoi;

                        //loại nhân sự, công việc
                        HoSoHelper.SetLoaiNhanSu(Session, ThongTinNhanVien);
                    
                        //Làm quyết định bổ nhiệm trước đó hết hiệu lực
                        filter = CriteriaOperator.Parse("ThongTinNhanVien=? And Oid!=?", ThongTinNhanVien, this.Oid);
                        SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                        XPCollection<QuyetDinhBoNhiem> quyetDinhCuList = new XPCollection<QuyetDinhBoNhiem>(Session, filter, sort);
                        quyetDinhCuList.TopReturnedObjects = 1;
                        if (quyetDinhCuList.Count == 1)
                            quyetDinhCuList[0].QuyetDinhMoi = false;
                    }

                    //quá trình bổ nhiệm chức vụ
                    QuaTrinhHelper.CreateQuaTrinhBoNhiem(Session, this, ChucVuMoi, HSPCChucVuMoi, NgayHuongHeSoMoi);

                    //quá trình công tác
                    //chỉ tạo quá trình công tác, khi nào miễn nhiệm thì điền thông tin đến năm vào
                    QuaTrinhHelper.CreateQuaTrinhCongTac(Session, this, string.Format("{0} {1}", ChucVuMoi.TenChucVu, BoPhanMoi.TenBoPhan));

                    //Cập nhật đến ngày của diễn biến lương
                    if (ChucVuCu != null)
                    {
                        filter = CriteriaOperator.Parse("ThongTinNhanVien=? and ChucVuMoi=?", ThongTinNhanVien, ChucVuCu);
                        using (XPCollection<QuyetDinhBoNhiem> quyetDinhList = new XPCollection<QuyetDinhBoNhiem>(Session, filter))
                        {
                            quyetDinhList.TopReturnedObjects = 1;
                            if (quyetDinhList.Count == 1)
                            {
                                QuaTrinhHelper.UpdateDienBienLuong(Session, this, ThongTinNhanVien, NgayHuongHeSoMoi);
                            }
                        }
                    }

                    //Tạo mới diễn biến lương
                    if (NgayHuongHeSoMoi != DateTime.MinValue)
                    {
                        QuaTrinhHelper.CreateDienBienLuong(Session, this, ThongTinNhanVien, NgayHuongHeSoMoi, null);
                    }

                    //Bảo hiểm xã hội
                    if (hoSoBaoHiem != null &&
                        NgayHuongHeSoMoi != DateTime.MinValue)
                    {
                        QuaTrinhHelper.CreateQuaTrinhThamGiaBHXH(Session, hoSoBaoHiem, this, NgayHuongHeSoMoi);
                    }

                    //quản lý bổ nhiệm
                    BoNhiemHelper.CreateBoNhiem(Session, this, ChucVuMoi, false);

                    //luu tru giay to ho so can bo huong dan
                    //GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                    //GiayToHoSo.SoGiayTo = SoQuyetDinh;
                    //GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                    //GiayToHoSo.TrichYeu = NoiDung;

                }
        }
            else
            {
                if (!IsDeleted)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                        ThongTinNhanVien.Oid);
                    HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);

                    //cho phép lập quyết định bổ nhiệm lại mà không cần lập quyết định miễn nhiệm trước đó.
                    //để tránh có 2 dòng trong quản lý biến động (1 dòng biến động giảm mức đóng, 1 dòng biến động tăng mức đóng)
                    if (QuyetDinhMoi
                        && !HetHieuLuc
                        //&& ChucVuCu != ChucVuMoi
                        )
                    {
                        ThongTinNhanVien.BoPhan = BoPhanMoi;
                        ThongTinNhanVien.ChucVu = ChucVuMoi;
                        ThongTinNhanVien.NgayBoNhiem = NgayHieuLuc;
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = HSPCChucVuMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCChucVu = NgayHuongHeSoMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCLanhDao = HSPCLanhDaoMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.DuocHuongHSPCChuyenVien = DuocHuongPhuCapChuyenVien;
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChuyenMon = HSPCChuyenMon;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCChuyenMon = NgayHuongHSPCChuyenMonCu;
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNienTrongTruong = HSPCThamNienTruong;
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = HSPCTrachNhiemTruong;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem = NgayHuongHSPCTrachNhiemTruong;

                        //chi phat sinh bien dong khi nhan vien do tham gia bao hiem
                        if (hoSoBaoHiem != null
                            && NgayPhatSinhBienDong != DateTime.MinValue)
                        {
                            //biến động thay đổi mức đóng
                            BienDongHelper.CreateBienDongThayDoiLuong(Session, this, NgayPhatSinhBienDong,
                                ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong, HSPCChucVuMoi,
                                ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung,
                                ThongTinNhanVien.NhanVienThongTinLuong.ThamNien,
                                ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac,
                                ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong);

                            //Biến động thay đổi chức danh
                            BienDongHelper.CreateBienDongThayDoiChucDanh(Session, this, NgayPhatSinhBienDong, string.Format("{0} {1}", ChucVuMoi.TenChucVu, BoPhan.TenBoPhan));
                        }

                        //Lần bổ nhiệm
                        object count = Session.Evaluate<QuaTrinhBoNhiem>(CriteriaOperator.Parse("Count()"),
                            CriteriaOperator.Parse("ThongTinNhanVien=? and ChucVu=? and QuyetDinh!=?",
                            ThongTinNhanVien, ChucVuMoi, this));
                        if (count != null)
                            ThongTinNhanVien.LanBoNhiemChucVu = (int)count + 1;
                        else
                            ThongTinNhanVien.LanBoNhiemChucVu = 1;

                        //cần thay đổi thông tin chức vụ cao nhất đã qua ở đây
                        if (ChucVuCaoNhatDaQua == null ||
                            (ChucVuCaoNhatDaQua.HSPCChucVu < ChucVuMoi.HSPCChucVu))
                            ThongTinNhanVien.ChucVuCoQuanCaoNhat = ChucVuMoi;

                        //loại nhân sự, công việc
                        HoSoHelper.SetLoaiNhanSu(Session, ThongTinNhanVien);

                        //Làm quyết định bổ nhiệm trước đó hết hiệu lực
                        filter = CriteriaOperator.Parse("ThongTinNhanVien=? And Oid!=?", ThongTinNhanVien, this.Oid);
                        SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                        XPCollection<QuyetDinhBoNhiem> quyetDinhCuList = new XPCollection<QuyetDinhBoNhiem>(Session, filter, sort);
                        quyetDinhCuList.TopReturnedObjects = 1;
                        if (quyetDinhCuList.Count == 1)
                            quyetDinhCuList[0].QuyetDinhMoi = false;
                    }

                    //quá trình bổ nhiệm chức vụ
                    QuaTrinhHelper.CreateQuaTrinhBoNhiem(Session, this, ChucVuMoi, HSPCChucVuMoi, NgayHuongHeSoMoi);

                    //quá trình công tác
                    //chỉ tạo quá trình công tác, khi nào miễn nhiệm thì điền thông tin đến năm vào
                    QuaTrinhHelper.CreateQuaTrinhCongTac(Session, this, string.Format("{0} {1}", ChucVuMoi.TenChucVu, BoPhanMoi.TenBoPhan));

                    //Cập nhật đến ngày của diễn biến lương
                    if (ChucVuCu != null)
                    {
                        filter = CriteriaOperator.Parse("ThongTinNhanVien=? and ChucVuMoi=?", ThongTinNhanVien, ChucVuCu);
                        using (XPCollection<QuyetDinhBoNhiem> quyetDinhList = new XPCollection<QuyetDinhBoNhiem>(Session, filter))
                        {
                            quyetDinhList.TopReturnedObjects = 1;
                            if (quyetDinhList.Count == 1)
                            {
                                QuaTrinhHelper.UpdateDienBienLuong(Session, this, ThongTinNhanVien, NgayHuongHeSoMoi);
                            }
                        }
                    }

                    //Tạo mới diễn biến lương
                    if (NgayHuongHeSoMoi != DateTime.MinValue)
                    {
                        QuaTrinhHelper.CreateDienBienLuong(Session, this, ThongTinNhanVien, NgayHuongHeSoMoi, null);
                    }

                    //Bảo hiểm xã hội
                        if (hoSoBaoHiem != null &&
                            NgayHuongHeSoMoi != DateTime.MinValue)
                        {
                            QuaTrinhHelper.CreateQuaTrinhThamGiaBHXH(Session, hoSoBaoHiem, this, NgayHuongHeSoMoi);
                        }

                    //quản lý bổ nhiệm
                    BoNhiemHelper.CreateBoNhiem(Session, this, ChucVuMoi, false);

                    //luu tru giay to ho so can bo huong dan
                    //GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                    //GiayToHoSo.SoGiayTo = SoQuyetDinh;
                    //GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                    //GiayToHoSo.TrichYeu = NoiDung;

                }
            }

        }

        protected override void OnDeleting()
        {
            if (QuyetDinhMoi)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                using (XPCollection<QuyetDinhBoNhiem> quyetdinh = new XPCollection<QuyetDinhBoNhiem>(Session, filter, sort))
                {
                    quyetdinh.TopReturnedObjects = 1;
                    if (quyetdinh.Count > 0)
                    {
                        if (quyetdinh[0] == This && QuyetDinhMoi)
                        {
                            //lần bổ nhiệm
                            object count = Session.Evaluate<QuaTrinhBoNhiem>(CriteriaOperator.Parse("Count()"),
                                CriteriaOperator.Parse("ThongTinNhanVien=? and ChucVu=? and QuyetDinh!=?",
                                ThongTinNhanVien, ChucVuCu, this));
                            if (count != null && (int)count > 0)
                                ThongTinNhanVien.LanBoNhiemChucVu = (int)count;
                            else
                                ThongTinNhanVien.LanBoNhiemChucVu = 0;

                            ThongTinNhanVien.LoaiNhanSu = LoaiNhanSuCu;
                            ThongTinNhanVien.CongViecHienNay = CongViecCu;
                            ThongTinNhanVien.ChucVu = ChucVuCu;
                            ThongTinNhanVien.NgayBoNhiem = NgayBoNhiemCu;
                            ThongTinNhanVien.ChucVuCoQuanCaoNhat = ChucVuCaoNhatDaQua;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = HSPCChucVuCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCLanhDao = HSPCLanhDaoCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCChucVu = NgayHuongHeSoCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCKiemNhiemTrongTruong = HSPCKiemNhiemTrongTruongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.DuocHuongHSPCChuyenVien = DuocHuongPhuCapChuyenVienCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCChuyenMon = HSPCChuyenMonCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCChuyenMon = NgayHuongHSPCChuyenMonCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNienTrongTruong = HSPCThamNienTruongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = HSPCTrachNhiemTruongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem = NgayHuongHSPCTrachNhiemTruongCu;
                        }
                    }
                }

                //Làm quyết định bổ nhiệm trước đó có hiệu lực lại
                filter = CriteriaOperator.Parse("ThongTinNhanVien=? And Oid !=?",ThongTinNhanVien,this.Oid);
                sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                XPCollection<QuyetDinhBoNhiem> quyetDinhCuList = new XPCollection<QuyetDinhBoNhiem>(Session, filter, sort);
                quyetDinhCuList.TopReturnedObjects = 1;
                if (quyetDinhCuList.Count == 1)
                    quyetDinhCuList[0].QuyetDinhMoi = true;

                //bộ phận
                if (BoPhanMoi != null)
                {
                    if (CoQuanRaQuyetDinh == CoQuanRaQuyetDinhEnum.TruongRaQuyetDinh)
                        ThongTinNhanVien.BoPhan = BoPhan;
                    else
                        ThongTinNhanVien.BoPhan = BoPhanMoi;
                }

            }

            //xoa qua trinh bo nhiem
            QuaTrinhHelper.DeleteQuaTrinhNhanVien<QuaTrinhBoNhiem>(Session, this);

            //xoa qua trinh tham gia bao hiem xa hoi
            if (NgayHuongHeSoMoi != DateTime.MinValue)
                QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhThamGiaBHXH>(Session, CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=? and TuNam=?", ThongTinNhanVien, NgayHuongHeSoMoi));

            //xoa qua trinh cong tac
            QuaTrinhHelper.DeleteQuaTrinhHoSo<QuaTrinhCongTac>(Session, ThongTinNhanVien, this);

            if (NgayPhatSinhBienDong != DateTime.MinValue)
            {
                //xóa biến động thay doi muc dong
                BienDongHelper.DeleteBienDong<BienDong_ThayDoiLuong>(Session, ThongTinNhanVien, NgayPhatSinhBienDong);

                //xoa bien dong thay doi chuc danh
                BienDongHelper.DeleteBienDong<BienDong_ThayDoiChucDanh>(Session, ThongTinNhanVien, NgayPhatSinhBienDong);
            }

            //xoa dien bien luong
            QuaTrinhHelper.DeleteQuaTrinhNhanVien<DienBienLuong>(Session, this);

            //xóa quản lý bổ nhiệm
            BoNhiemHelper.DeleteBoNhiem<ChiTietBoNhiem>(Session, this);

            //xoa giay to
            if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);

            base.OnDeleting();
        }

        private void XuLy()
        {
            HSPCChucVuMoi = ChucVuMoi.HSPCChucVu;
            //
            HSPCLanhDaoMoi = 0;
            HSPCKiemNhiemTrongTruongMoi = 0;

            decimal hspcldChucVuKiemNhiem = 0;
            decimal hspcldChucVuChinh = 0;

            hspcldChucVuChinh = ChucVuMoi.HSPCQuanLy;
            if (ThongTinNhanVien.ChucVuKiemNhiem != null)
            {
                hspcldChucVuKiemNhiem = ThongTinNhanVien.ChucVuKiemNhiem.HSPCQuanLy;
            }


            ///dua vo danh sach
            List<decimal> hspcChucVu = new List<decimal>();
            if (hspcldChucVuChinh > 0)
                hspcChucVu.Add(hspcldChucVuChinh);
            if (hspcldChucVuKiemNhiem > 0)
                hspcChucVu.Add(hspcldChucVuKiemNhiem);
         

            if (hspcChucVu.Count > 0)
            {
                if (hspcChucVu.Count == 1)
                {
                    HSPCLanhDaoMoi = hspcChucVu[0];
                }
                else
                {
                    int index = hspcChucVu.Count - 1;
                    hspcChucVu.Sort((a, b) => decimal.Compare(a, b));

                    HSPCLanhDaoMoi = hspcChucVu[index];
                    HSPCKiemNhiemTrongTruongMoi = hspcChucVu[index - 1] * 0.1m;
                }
            }

        }
    }
}
