using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.Xpo.Metadata;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Data.SqlClient;
//
namespace PSC_HRM.Module.HoSo
{
    [ImageName("BO_Resume")]
    [ModelDefault("Caption", "Nhân viên")]
    [ModelDefault("EditorTypeName", "PSC_HRM.Module.Win.Editors.CustomCategorizedListEditor")]
    [Appearance("Hide_NgayNghiViec", TargetItems = "NgayNghiViec", Visibility = ViewItemVisibility.Hide, Criteria = "!TinhTrang.KhongConCongTacTaiTruong")]
    [Appearance("Khoa_MaQuanLy", TargetItems = "MaQuanLy", Enabled = false, Criteria = "MaTruong = 'DNU'")]
    [RuleCombinationOfPropertiesIsUnique("HoSo.Unique3", DefaultContexts.Save, "LoaiHoSo;CMND", "Nhân viên đã tồn tại trong hệ thống. Liên hệ quản trị hệ thống HRM")]
    [RuleCombinationOfPropertiesIsUnique("HoSo.Unique4", DefaultContexts.Save, "LoaiHoSo;SoHoChieu", "Nhân viên đã tồn tại trong hệ thống. Liên hệ quản trị hệ thống HRM")]
    [Appearance("HidePhanVien_HVNH", TargetItems = "PhanVien", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'HVNH'")]
    public class NhanVien : HoSo, IBoPhan, ICategorizedItem, IThongTinTruong
    {
        private DateTime _NgayVaoCoQuan;
        private ChucDanh _ChucDanh;
        private bool _BangCapDaKiemDuyet;
        private BoPhan _BoPhan;
        private DateTime _NgayVaoNganhGiaoDuc;
        private DateTime _NgayVaoNganhNganHang;
        private ThanhPhanXuatThan _ThanhPhanGiaDinh;
        private UuTienGiaDinh _UuTienGiaDinh;
        private UuTienBanThan _UuTienBanThan;
        private CongViec _CongViecHienNay;
        private HopDong.HopDong _HopDongHienTai;
        private DateTime _TuNgayHD;
        private NhanVienThongTinLuong _NhanVienThongTinLuong;
        private NhanVienTrinhDo _NhanVienTrinhDo;
        private DateTime _NgayTuyenDung;
        private string _DonViTuyenDung;
        private string _CongViecTuyenDung;
        private CongViec _CongViecDuocGiao;
        private TinhTrang _TinhTrang;
        private ThongTinTruong _ThongTinTruong;
        private BoPhan _BoPhanTinhLuong;
        private DateTime _NgayNghiViec;
        private bool _LaDangVien;
        private BoPhan _BoPhanCu;
        private decimal _SoThangKhongTinhPhep;
        private LoaiPhanVien _PhanVien;

        [ModelDefault("Caption", "Ngày nghỉ việc")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TinhTrang.KhongConCongTacTaiTruong=True")]
        public DateTime NgayNghiViec
        {
            get
            {
                return _NgayNghiViec;
            }
            set
            {
                SetPropertyValue("NgayNghiViec", ref _NgayNghiViec, value);
            }
        }

        [ModelDefault("Caption", " Là đảng viên")]
        public bool LaDangVien
        {
            get
            {
                return _LaDangVien;
            }
            set
            {
                SetPropertyValue("LaDangVien", ref _LaDangVien, value);
            }
        }

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanh ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
            }
        }

        [ModelDefault("Caption", "Bằng cấp đã kiểm duyệt")]
        public bool BangCapDaKiemDuyet
        {
            get
            {
                return _BangCapDaKiemDuyet;
            }
            set
            {
                SetPropertyValue("BangCapDaKiemDuyet", ref _BangCapDaKiemDuyet, value);
            }
        }

        [Delayed]
        [ModelDefault("Caption", "Hình ảnh")]
        [Size(SizeAttribute.Unlimited)]
        [ValueConverter(typeof(ImageValueConverter))]
        public System.Drawing.Image HinhAnh
        {
            get
            {
                return GetDelayedPropertyValue<System.Drawing.Image>("HinhAnh");
            }
            set
            {
                if (value != null)
                    SetDelayedPropertyValue<System.Drawing.Image>("HinhAnh", new System.Drawing.Bitmap(value, new System.Drawing.Size(100, 120)));
                else
                    SetDelayedPropertyValue<System.Drawing.Image>("HinhAnh", value);

            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [Association("BoPhan-ListNhanVien")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if(!IsLoading && value != null)
                {
                    if (TruongConfig.MaTruong.Equals("CYD"))
                        AfterBoPhanChanged();
                    if(TruongConfig.MaTruong.Equals("DNU") && (MaQuanLy == null|| MaQuanLy == ""))
                        Tao_MaGiangVien();
                    //if (TruongConfig.MaTruong.Equals("UEL"))
                    //    Tao_MaGiangVien_UEL();
                    
                }
            }
        }

        public void Tao_MaGiangVien()
        {
            object MaGV = null;
            SqlParameter[] param = new SqlParameter[1]; /*Số parameter trên Store Procedure*/
            param[0] = new SqlParameter("@BoPhan", BoPhan.Oid);
            MaGV = DataProvider.GetValueFromDatabase("spd_DNU_HoSo_TaoMa", System.Data.CommandType.StoredProcedure, param);
            if(MaGV!=null)
            {
                MaQuanLy = MaGV.ToString();
            }
        }
        //public void Tao_MaGiangVien_UEL()
        //{
        //    object MaGV = null;
        //    SqlParameter[] param = new SqlParameter[1]; /*Số parameter trên Store Procedure*/
        //    param[0] = new SqlParameter("@BoPhan", BoPhan.Oid);
        //    MaGV = DataProvider.GetValueFromDatabase("spd_TaoMaQuanLyTheoDV", System.Data.CommandType.StoredProcedure, param);
        //    if (MaGV != null)
        //    {
        //        MaQuanLy = MaGV.ToString();
        //    }
        //}

        [ModelDefault("Caption", "Đơn vị tính lương")]
        public BoPhan BoPhanTinhLuong
        {
            get
            {
                return _BoPhanTinhLuong;
            }
            set
            {
                SetPropertyValue("BoPhanTinhLuong", ref _BoPhanTinhLuong, value);
            }
        }

        [ModelDefault("Caption", "Thành phần xuất thân")]
        public ThanhPhanXuatThan ThanhPhanXuatThan
        {
            get
            {
                return _ThanhPhanGiaDinh;
            }
            set
            {
                SetPropertyValue("ThanhPhanXuatThan", ref _ThanhPhanGiaDinh, value);
            }
        }

        [ModelDefault("Caption", "Ưu tiên gia đình")]
        public UuTienGiaDinh UuTienGiaDinh
        {
            get
            {
                return _UuTienGiaDinh;
            }
            set
            {
                SetPropertyValue("UuTienGiaDinh", ref _UuTienGiaDinh, value);
            }
        }

        [ModelDefault("Caption", "Ưu tiên bản thân")]
        public UuTienBanThan UuTienBanThan
        {
            get
            {
                return _UuTienBanThan;
            }
            set
            {
                SetPropertyValue("UuTienBanThan", ref _UuTienBanThan, value);
            }
        }

        [ModelDefault("Caption", "Công việc hiện nay")]
        public CongViec CongViecHienNay
        {
            get
            {
                return _CongViecHienNay;
            }
            set
            {
                SetPropertyValue("CongViecHienNay", ref _CongViecHienNay, value);
            }
        }

        [ModelDefault("Caption", "Hợp đồng hiện tại")]
        [DataSourceProperty("ListHopDong")]
        
        public HopDong.HopDong HopDongHienTai
        {
            get
            {
                return _HopDongHienTai;
            }
            set
            {
                SetPropertyValue("HopDongHienTai", ref _HopDongHienTai, value);
            }
        }
        [ModelDefault("Caption", "Từ ngày (Hợp đồng)")]
        public DateTime TuNgayHD
        {
            get
            {
                return _TuNgayHD;
            }
            set
            {
                SetPropertyValue("TuNgayHD", ref _TuNgayHD, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào ngành GD")]
        public DateTime NgayVaoNganhGiaoDuc
        {
            get
            {
                return _NgayVaoNganhGiaoDuc;
            }
            set
            {
                SetPropertyValue("NgayVaoNganhGiaoDuc", ref _NgayVaoNganhGiaoDuc, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào ngành NH")]
        public DateTime NgayVaoNganhNganHang
        {
            get
            {
                return _NgayVaoNganhNganHang;
            }
            set
            {
                SetPropertyValue("NgayVaoNganhNganHang", ref _NgayVaoNganhNganHang, value);
            }
        }

        [ModelDefault("Caption", "Ngày tuyển dụng")]
        public DateTime NgayTuyenDung
        {
            get
            {
                return _NgayTuyenDung;
            }
            set
            {
                SetPropertyValue("NgayTuyenDung", ref _NgayTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị tuyển dụng")]
        public string DonViTuyenDung
        {
            get
            {
                return _DonViTuyenDung;
            }
            set
            {
                SetPropertyValue("DonViTuyenDung", ref _DonViTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "Công việc tuyển dụng")]
        public string CongViecTuyenDung
        {
            get
            {
                return _CongViecTuyenDung;
            }
            set
            {
                SetPropertyValue("CongViecTuyenDung", ref _CongViecTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "Công việc được giao")]
        public CongViec CongViecDuocGiao
        {
            get
            {
                return _CongViecDuocGiao;
            }
            set
            {
                SetPropertyValue("CongViecDuocGiao", ref _CongViecDuocGiao, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào cơ quan")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayVaoCoQuan
        {
            get
            {
                return _NgayVaoCoQuan;
            }
            set
            {
                SetPropertyValue("NgayVaoCoQuan", ref _NgayVaoCoQuan, value);
                if (!IsLoading)
                {
                    if (TruongConfig.MaTruong == "DNU")
                    {
                        if (BoPhan != null && NgayVaoCoQuan != DateTime.MinValue)
                        {
                            //Tao_MaGiangVien();
                        }
                    }
                }
            }
        }

        [ModelDefault("Caption", "Thông tin lương")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public NhanVienThongTinLuong NhanVienThongTinLuong
        {
            get
            {
                return _NhanVienThongTinLuong;
            }
            set
            {
                SetPropertyValue("NhanVienThongTinLuong", ref _NhanVienThongTinLuong, value);
            }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public NhanVienTrinhDo NhanVienTrinhDo
        {
            get
            {
                return _NhanVienTrinhDo;
            }
            set
            {
                SetPropertyValue("NhanVienTrinhDo", ref _NhanVienTrinhDo, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
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

        [Aggregated]
        [ModelDefault("Caption", "Tài khoản ngân hàng")]
        [Association("NhanVien-ListTaiKhoanNganHang")]
        public XPCollection<TaiKhoanNganHang> ListTaiKhoanNganHang
        {
            get
            {
                return GetCollection<TaiKhoanNganHang>("ListTaiKhoanNganHang");
            }
        }

        [Persistent]
        [ModelDefault("Caption", "Đơn vị cũ")]
        public BoPhan BoPhanCu
        {
            get
            {
                return _BoPhanCu;
            }
            set
            {
                SetPropertyValue("BoPhanCu", ref _BoPhanCu, value);
            }
        }

        [ModelDefault("Caption", "Số tháng không tính phép")]
        public decimal SoThangKhongTinhPhep
        {
            get
            {
                return _SoThangKhongTinhPhep;
            }
            set
            {
                SetPropertyValue("SoThangKhongTinhPhep", ref _SoThangKhongTinhPhep, value);
            }
        }
        
        [Aggregated]
        [ModelDefault("AllowNew", "False")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Hợp đồng lao động")]
        public XPCollection<HopDong.HopDong> ListHopDong
        {
            get
            {
                return new XPCollection<HopDong.HopDong>(Session, CriteriaOperator.Parse("NhanVien=?", Oid));
            }
        }

        //[NonPersistent]
        //[Browsable(false)]
        //private string MaTruong { get; set; }

        public NhanVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NhanVienTrinhDo = new NhanVienTrinhDo(Session);
            NhanVienThongTinLuong = new NhanVienThongTinLuong(Session);
            TinhTrang = Session.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc"));
            //Lấy mã trường hiện tại dùng để phân quyền
            MaTruong = TruongConfig.MaTruong;
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
        }

        public void OnLoadedNhanVien()
        {
            //Lấy mã trường hiện tại dùng để phân quyền
            MaTruong = TruongConfig.MaTruong;
        }

        ITreeNode ICategorizedItem.Category
        {
            get
            {
                return BoPhan;
            }
            set
            {
                BoPhan = (BoPhan)value;
            }
        }

        [Browsable(false)]
        [NonPersistent()]
        public BoPhan Category
        {
            get
            {
                return BoPhan;
            }
            set
            {
                BoPhan = value;
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Thông tin trường")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân viện")]
        public LoaiPhanVien PhanVien
        {
            get
            {
                return _PhanVien;
            }
            set
            {
                SetPropertyValue("PhanVien", ref _PhanVien, value);
            }
        }

        protected override void AfterGioiTinhChanged()
        {
            //if (GioiTinh == GioiTinhEnum.Nam)
            //    HinhAnh = global::PSC_HRM.Module.Properties.Resources.male;
            //else
            //    HinhAnh = global::PSC_HRM.Module.Properties.Resources.female;
        }
    }

}
