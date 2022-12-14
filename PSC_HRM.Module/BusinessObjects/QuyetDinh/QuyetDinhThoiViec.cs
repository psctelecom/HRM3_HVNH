using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.QuyetDinhService;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thôi việc")]
    [Appearance("QuyetDinhThoiViec.HideUFM", TargetItems = "TinhTroCapTuNgay;TinhTroCapDenNgay;MucLuongHienHuong;SoNamTroCap;SoTienTroCap;HeSoLuong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UFM'")]
    [Appearance("QuyetDinhThoiViec.HideUEL", TargetItems = "TinhTroCapTuNgay;TinhTroCapDenNgay;MucLuongHienHuong;SonamTroCap;SoTienTroCap;HeSoLuong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UEL'")]
    [Appearance("QuyetDinhThoiViec.HideQNU", TargetItems = "ThoiHanBanGiaoCongViec;NoiCuTruSauKhiThoiViec", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'QNU'")]
    [Appearance("QuyetDinhThoiViec.HideNEU", TargetItems = "ThoiHanBanGiaoCongViec;NoiCuTruSauKhiThoiViec;TinhTroCapTuNgay;TinhTroCapDenNgay;MucLuongHienHuong;SoNamTroCap;SoTienTroCap;HeSoLuong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'NEU'")]

    public class QuyetDinhThoiViec : QuyetDinhCaNhan
    {
        private TinhTrang _TinhTrang;
        private DateTime _NgayPhatSinhBienDong;
        private DateTime _ThoiHanBanGiaoCongViec;
        private DateTime _NgayXinNghi;
        private DateTime _NghiViecTuNgay;
        private DateTime _TraLuongDenHetNgay;
        private string _LyDo;
        private LoaiQuyetDinhThoiViec _HinhThucThoiViec;
        private DiaChi _NoiCuTruSauKhiThoiViec;

        //QNU
        private DateTime _TinhTroCapTuNgay;
        private DateTime _TinhTroCapDenNgay;
        private decimal _MucLuongHienHuong;
        private decimal _SoNamTroCap;
        private decimal _SoTienTroCap;

        [ModelDefault("Caption", "Hình thức thôi việc")]
        //[RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "MaTruong = 'UTE'")]
        public LoaiQuyetDinhThoiViec HinhThucThoiViec
        {
            get
            {
                return _HinhThucThoiViec;
            }
            set
            {
                SetPropertyValue("HinhThucThoiViec", ref _HinhThucThoiViec, value);
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

        [ModelDefault("Caption", "Ngày xin nghỉ")]       
        public DateTime NgayXinNghi
        {
            get
            {
                return _NgayXinNghi;
            }
            set
            {
                SetPropertyValue("NgayXinNghi", ref _NgayXinNghi, value);                
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nghỉ việc từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NghiViecTuNgay
        {
            get
            {
                return _NghiViecTuNgay;
            }
            set
            {
                SetPropertyValue("NghiViecTuNgay", ref _NghiViecTuNgay, value);
                if (!IsLoading && value != DateTime.MinValue)
                {

                    if (TruongConfig.MaTruong.Equals("QNU"))
                    {
                        NgayPhatSinhBienDong = value;
                        TraLuongDenHetNgay = value.AddDays(-1);
                        TinhTroCapDenNgay = new DateTime(2008, 12, 31);
                    }
                    else
                    {

                        NgayPhatSinhBienDong = value;
                        TraLuongDenHetNgay = value.AddDays(-1);
                        TinhTroCapDenNgay = value.AddDays(-1);
                    }
                }
            }
        }

        [ModelDefault("Caption", "Trả lương đến hết ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TraLuongDenHetNgay
        {
            get
            {
                return _TraLuongDenHetNgay;
            }
            set
            {
                SetPropertyValue("TraLuongDenHetNgay", ref _TraLuongDenHetNgay, value);
            }
        }

        [ModelDefault("Caption", "Thời hạn bàn giao công việc")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public DateTime ThoiHanBanGiaoCongViec
        {
            get
            {
                return _ThoiHanBanGiaoCongViec;
            }
            set
            {
                SetPropertyValue("ThoiHanBanGiaoCongViec", ref _ThoiHanBanGiaoCongViec, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Nơi cư trú sau khi nghỉ")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi NoiCuTruSauKhiThoiViec
        {
            get
            {
                return _NoiCuTruSauKhiThoiViec;
            }
            set
            {
                SetPropertyValue("NoiCuTruSauKhiThoiViec", ref _NoiCuTruSauKhiThoiViec, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tính trợ cấp từ ngày")]        
        public DateTime TinhTroCapTuNgay
        {
            get
            {
                return _TinhTroCapTuNgay;
            }
            set
            {
                SetPropertyValue("TinhTroCapTuNgay", ref _TinhTroCapTuNgay, value);
                if (!IsLoading && ThongTinTruong != null && MaTruong.Equals("QNU"))
                {
                    // SoNamTroCap = Math.Round((decimal)(TinhTroCapDenNgay.Subtract(TinhTroCapTuNgay).Days / 365.2425), 1);
                    decimal a = Math.Round((decimal)(TinhTroCapDenNgay.Subtract(TinhTroCapTuNgay).Days / 365.2425), 1);
                    decimal b = Math.Round((decimal)(TinhTroCapDenNgay.Subtract(TinhTroCapTuNgay).Days / 365.2425), 0);
                    if ((a - b) > 0.5m)
                    {
                        SoNamTroCap = b + 1;
                    }
                    if ((a - b) <= 0.5m && (a - b) > 0)
                    {
                        SoNamTroCap = b + 0.5m;
                    }
                    MucLuongHienHuong = ((ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong + ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNien) + ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu) * ThongTinTruong.ThongTinChung.LuongCoBan;
                    SoTienTroCap = MucLuongHienHuong * ThongTinTruong.ThongTinChung.MucTroCapThoiViec * SoNamTroCap;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tính trợ cấp đến ngày")]
        public DateTime TinhTroCapDenNgay
        {
            get
            {
                return _TinhTroCapDenNgay;
            }
            set
            {
                SetPropertyValue("TinhTroCapDenNgay", ref _TinhTroCapDenNgay, value);
                if (!IsLoading && ThongTinTruong != null && MaTruong.Equals("QNU"))
                {
                    decimal a = Math.Round((decimal)(TinhTroCapDenNgay.Subtract(TinhTroCapTuNgay).Days / 365.2425), 1);
                    decimal b = Math.Round((decimal)(TinhTroCapDenNgay.Subtract(TinhTroCapTuNgay).Days / 365.2425), 0);
                    if ((a - b) > 0.5m)
                    {
                        SoNamTroCap = b + 1;
                    }
                    if ((a - b) <= 0.5m && (a - b) > 0)
                    {
                        SoNamTroCap = b + 0.5m;
                    }
                    MucLuongHienHuong = ((ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong * ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNien) + ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu) * ThongTinTruong.ThongTinChung.LuongCoBan;
                    SoTienTroCap = MucLuongHienHuong * ThongTinTruong.ThongTinChung.MucTroCapThoiViec * SoNamTroCap;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Mức lương hiện hưởng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal MucLuongHienHuong
        {
            get
            {
                return _MucLuongHienHuong;
            }
            set
            {
                SetPropertyValue("MucLuongHienHuong", ref _MucLuongHienHuong, value);
                if (!IsLoading && ThongTinTruong != null && MaTruong.Equals("QNU"))
                {
                    // SoNamTroCap = Math.Round((decimal)(TinhTroCapDenNgay.Subtract(TinhTroCapTuNgay).Days / 365.2425), 2);
                    decimal a = Math.Round((decimal)(TinhTroCapDenNgay.Subtract(TinhTroCapTuNgay).Days / 365.2425), 1);
                    decimal b = Math.Round((decimal)(TinhTroCapDenNgay.Subtract(TinhTroCapTuNgay).Days / 365.2425), 0);
                    if ((a - b) > 0.5m)
                    {
                        SoNamTroCap = b + 1;
                    }
                    if ((a - b) <= 0.5m && (a - b) > 0)
                    {
                        SoNamTroCap = b + 0.5m;
                    }

                    SoTienTroCap = MucLuongHienHuong * ThongTinTruong.ThongTinChung.MucTroCapThoiViec * SoNamTroCap;
                }
            }
        }
       
        [ImmediatePostData]
        [ModelDefault("Caption", "Số năm trợ cấp")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoNamTroCap
        {
            get
            {
                return _SoNamTroCap;
            }
            set
            {
                SetPropertyValue("SoNamTroCap", ref _SoNamTroCap, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số tiền trợ cấp")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTienTroCap
        {
            get
            {
                return _SoTienTroCap;
            }
            set
            {
                SetPropertyValue("SoTienTroCap", ref _SoTienTroCap, value);
            }
        }

        [Size(500)]
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

        //Lưu vết
        [Browsable(false)]
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

        public QuyetDinhThoiViec(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhThoiViec;
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định thôi việc"));
        }

        protected override void AfterNhanVienChanged()
        {
            TinhTrang = ThongTinNhanVien.TinhTrang;
            TinhTroCapTuNgay = ThongTinNhanVien.NgayVaoCoQuan;          
        }

        protected override void OnLoaded()
        {
            base.OnLoading();

            if (GiayToHoSo == null)
            {
                GiayToList = ThongTinNhanVien.ListGiayToHoSo;
                if (GiayToList.Count > 0 && SoQuyetDinh != null)
                {
                    GiayToList.Criteria = CriteriaOperator.Parse("GiayTo like ? and SoGiayTo = ?", "Quyết định", SoQuyetDinh);
                    if (GiayToList.Count > 0)
                        GiayToHoSo = Session.FindObject<GiayToHoSo>(CriteriaOperator.Parse("Oid = ?", GiayToList[0].Oid));
                }
            }

            MaTruong = TruongConfig.MaTruong;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            SystemContainer.Resolver<IQuyetDinhThoiViecService>("QDThoiViec" + TruongConfig.MaTruong).Save(Session, this);
        }

        protected override void OnDeleting()
        {
            SystemContainer.Resolver<IQuyetDinhThoiViecService>("QDThoiViec" + TruongConfig.MaTruong).Delete(Session, this);

            base.OnDeleting();
        }
    }
}