using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.GiayTo;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using DevExpress.Xpo.DB;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.TaoMaQuanLy;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace PSC_HRM.Module.HopDong
{
    [ImageName("BO_Constract")]
    [ModelDefault("AllowNew", "False")]
    [DefaultProperty("SoHopDongFull")]
    [ModelDefault("Caption", "Hợp đồng")]
    [Appearance("HopDong", TargetItems = "NhanVien", Enabled = false, Criteria = "NhanVien is not null")]
    
    [Appearance("HopDong.NguoiKyTrongTruong", TargetItems = "TenNguoiKy", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoaiNguoiKy=0")]
    [Appearance("HopDong.NguoiKyNgoaiTruong", TargetItems = "NguoiKy", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoaiNguoiKy=2")]
    [Appearance("HopDong.NguoiKyKhongDangTaiChuc", TargetItems = "NguoiKy", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoaiNguoiKy=1")]
    [Appearance("HopDong.TruongRaQuyetDinh", TargetItems = "TenCoQuan", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiCoQuan=0")]
    [Appearance("HopDong.CoQuanKhacRaQuyetDinh", TargetItems = "ThongTinTruong", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiCoQuan=1 or LoaiCoQuan=2")]
    
    public class HopDong : TruongBaseObject, IBoPhan
    {
        //dùng cho hdtg
        [Browsable(false)]
        public static string NamHoc { get; private set; }
        [Browsable(false)]
        public static string HocKy { get; private set; }

        private string _CanCu;
        private QuanLyHopDongThinhGiang _QuanLyHopDongThinhGiang;
        private bool _HopDongCu;
        private string _LoaiHopDong;
        private QuocGia _QuocTich;
        private ThongTinTruong _ThongTinTruong;
        private string _ChucDanhChuyenMon;
        private GiayToHoSo _GiayToHoSo;
        private ChucVu _ChucVuNguoiKy;
        private NguoiKyEnum _PhanLoaiNguoiKy = NguoiKyEnum.DangTaiChuc;
        private ThongTinNhanVien _NguoiKy;
        private BoPhan _BoPhan;
        private string _DienThoai;
        private string _DiaChi;
        private NhanVien _NhanVien;
        private string _SoHopDong;
        private DateTime _NgayKy;
        private string _GhiChu;
        private DateTime _NgaySinh;
        private DiaChi _NoiSinh;
        private string _CMND;
        private DateTime _NgayCap;
        private TinhThanh _NoiCap;
        private string _NoiLamViec;
        private int _DaCapNhat; // Thảo thêm

        //BUH
        private LoaiCoQuanEnum _LoaiCoQuan;
        private string _TenNguoiKy;
        private string _TenCoQuan;

        //Dùng để biết job có cập nhật chưa
        [Browsable(false)]
        public int DaCapNhat
        {
            get
            {
                return _DaCapNhat;
            }
            set
            {
                SetPropertyValue("DaCapNhat", ref _DaCapNhat, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại cơ quan")]
        public LoaiCoQuanEnum LoaiCoQuan
        {
            get
            {
                return _LoaiCoQuan;
            }
            set
            {
                SetPropertyValue("LoaiCoQuan", ref _LoaiCoQuan, value);
                UpdateChucVuList();
                if (!IsLoading)
                {
                    ChucVuNguoiKy = null;
                    if (value == LoaiCoQuanEnum.CoQuanKhac)
                    { PhanLoaiNguoiKy = NguoiKyEnum.NgoaiTruong; }
                    else
                    { PhanLoaiNguoiKy = NguoiKyEnum.DangTaiChuc; }
                }
            }
        }
        
        [ModelDefault("Caption", "Tên cơ quan")]
        public string TenCoQuan
        {
            get
            {
                return _TenCoQuan;
            }
            set
            {
                SetPropertyValue("TenCoQuan", ref _TenCoQuan, value);
            }
        }

        [ModelDefault("Caption", "Tên người ký")]
        public string TenNguoiKy
        {
            get
            {
                return _TenNguoiKy;
            }
            set
            {
                SetPropertyValue("TenNguoiKy", ref _TenNguoiKy, value);
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Số hợp đồng")]
        public string SoHopDongFull { get; set; }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Quản lý hợp đồng thỉnh giảng")]
        [Association("QuanLyHopDongThinhGiang-ListHopDong")]
        public QuanLyHopDongThinhGiang QuanLyHopDongThinhGiang
        {
            get
            {
                return _QuanLyHopDongThinhGiang;
            }
            set
            {
                SetPropertyValue("QuanLyHopDongThinhGiang", ref _QuanLyHopDongThinhGiang, value);
                if (value != null)
                {
                    if (!IsLoading)
						TaoSoHopDong();

                    if (value.NamHoc != null)
                        NamHoc = value.NamHoc.TenNamHoc;
                    if (value.HocKy != null)
                        HocKy = value.HocKy.MaQuanLy;
                }
            }
        }

        [ImmediatePostData]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Loại hợp đồng")]
        public string LoaiHopDong
        {
            get
            {
                return _LoaiHopDong;
            }
            set
            {
                SetPropertyValue("LoaiHopDong", ref _LoaiHopDong, value);
                if (!IsLoading)
                    TaoTrichYeu();
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
                if (!IsLoading && value != null)
                {
                    DienThoai = value.DienThoai;
                    if (value.DiaChi != null)
                        DiaChi = value.DiaChi.FullDiaChi;
                }
            }
        }

        [ModelDefault("Caption", "Điện thoại cơ quan")]
        public string DienThoai
        {
            get
            {
                return _DienThoai;
            }
            set
            {
                SetPropertyValue("DienThoaiCongTy", ref _DienThoai, value);
            }
        }

        [Size(200)]
        [ModelDefault("Caption", "Địa chỉ cơ quan")]
        public string DiaChi
        {
            get
            {
                return _DiaChi;
            }
            set
            {
                SetPropertyValue("DiaChi", ref _DiaChi, value);
            }
        }

        [ModelDefault("Caption", "Số hợp đồng")]
        public string SoHopDong
        {
            get
            {
                return _SoHopDong;
            }
            set
            {
                SetPropertyValue("SoHopDong", ref _SoHopDong, value);
                if (!IsLoading && value != null && GiayToHoSo != null)
                    GiayToHoSo.SoGiayTo = value;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày ký")]
        public DateTime NgayKy
        {
            get
            {
                return _NgayKy;
            }
            set
            {
                SetPropertyValue("NgayKy", ref _NgayKy, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    if (GiayToHoSo != null)
                    {
                        GiayToHoSo.NgayBanHanh = value;
                        //thảo thêm
                        DanhMuc.GiayTo giayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "%hợp đồng%"));
                        if (giayTo != null)
                        {
                            GiayToHoSo.GiayTo = giayTo;
                        }
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại người ký")]
        public NguoiKyEnum PhanLoaiNguoiKy
        {
            get
            {
                return _PhanLoaiNguoiKy;
            }
            set
            {
                SetPropertyValue("PhanLoaiNguoiKy", ref _PhanLoaiNguoiKy, value);
                if (!IsLoading && ChucVuNguoiKy != null)
                {
                    UpdateNguoiKyList();
                    NguoiKy = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ người ký")]
        [DataSourceProperty("ChucVuList")]
        public ChucVu ChucVuNguoiKy
        {
            get
            {
                return _ChucVuNguoiKy;
            }
            set
            {
                SetPropertyValue("ChucVuNguoiKy", ref _ChucVuNguoiKy, value);
                if (!IsLoading)
                {
                    UpdateNguoiKyList();
                    NguoiKy = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Người ký")]
        [DataSourceProperty("NguoiKyList")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "PhanLoaiNguoiKy=0")]
        public ThongTinNhanVien NguoiKy
        {
            get
            {
                return _NguoiKy;
            }
            set
            {
                SetPropertyValue("NguoiKy", ref _NguoiKy, value);
            }
        }

        [ImmediatePostData]
        //[DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Người lao động")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading && value != null)
                {
                    //cập nhật hợp đồng mới nhất
                    UpdateHopDongHienTai();

                    //cập nhật nơi làm việc
                    UpdateNoiLamViec();

                    //xử lý khi nhân viên thay đổi
                    AfterNhanVienChanged();

                    //lưu trữ giấy tờ                    
                    if (GiayToHoSo != null)
                    {
                        GiayToHoSo.HoSo = value;
                        //thảo thêm
                        DanhMuc.GiayTo giayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "%hợp đồng%"));
                        if (giayTo != null)
                        {
                            GiayToHoSo.GiayTo = giayTo;
                        }
                    }
                    CMND = value.CMND;
                    NgaySinh = value.NgaySinh;
                    NoiSinh = value.NoiSinh;
                    NgayCap = value.NgayCap;
                    NoiCap = value.NoiCap;

                    UpdateGiayToList();
                }
            }
        }

        [ModelDefault("Caption", "Quốc tịch")]
        public QuocGia QuocTich
        {
            get
            {
                return _QuocTich;
            }
            set
            {
                SetPropertyValue("QuocTich", ref _QuocTich, value);
            }
        }

        [ModelDefault("Caption", "Ngày sinh")]
        public DateTime NgaySinh
        {
            get
            {
                return _NgaySinh;
            }
            set
            {
                SetPropertyValue("NgaySinh", ref _NgaySinh, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Nơi sinh")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi NoiSinh
        {
            get
            {
                return _NoiSinh;
            }
            set
            {
                SetPropertyValue("NoiSinh", ref _NoiSinh, value);
            }
        }

        [ModelDefault("Caption", "Số CMND")]
        public string CMND
        {
            get
            {
                return _CMND;
            }
            set
            {
                SetPropertyValue("CMND", ref _CMND, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp")]
        public DateTime NgayCap
        {
            get
            {
                return _NgayCap;
            }
            set
            {
                SetPropertyValue("NgayCap", ref _NgayCap, value);
            }
        }

        [ModelDefault("Caption", "Nơi cấp")]
        public TinhThanh NoiCap
        {
            get
            {
                return _NoiCap;
            }
            set
            {
                SetPropertyValue("NoiCap", ref _NoiCap, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức danh chuyên môn")]
        public string ChucDanhChuyenMon
        {
            get
            {
                return _ChucDanhChuyenMon;
            }
            set
            {
                SetPropertyValue("ChucDanhChuyenMon", ref _ChucDanhChuyenMon, value);
                if (!IsLoading)
                    TaoTrichYeu();
            }
        }

        [ModelDefault("Caption", "Bộ phận")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Nơi làm việc")]
        public string NoiLamViec
        {
            get
            {
                return _NoiLamViec;
            }
            set
            {
                SetPropertyValue("NoiLamViec", ref _NoiLamViec, value);
            }
        }

        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Hợp đồng đã hết hiệu lực")]
        public bool HopDongCu
        {
            get
            {
                return _HopDongCu;
            }
            set
            {
                SetPropertyValue("HopDongCu", ref _HopDongCu, value);
                if (!IsLoading && value)
                    Session.Save(this);
            }
        }

        [ModelDefault("Caption", "Căn cứ")]
        public string CanCu
        {
            get
            {
                return _CanCu;
            }
            set
            {
                SetPropertyValue("CanCu", ref _CanCu, value);
            }
        }

        [Size(200)]
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

        [ModelDefault("Caption", "Lưu trữ hợp đồng")]
        //[ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        [DataSourceProperty("GiayToList", DataSourcePropertyIsNullMode.SelectAll)]
        public GiayToHoSo GiayToHoSo
        {
            get
            {
                return _GiayToHoSo;
            }
            set
            {
                SetPropertyValue("GiayToHoSo", ref _GiayToHoSo, value);
                if (!IsLoading && value != null && NhanVien != null)
                    XemGiayToHoSo();
                
            }
        }

        [Browsable(false)]
        public XPCollection<GiayToHoSo> GiayToList { get; set; }
        public HopDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            //Chức vụ người ký mặc định Hiệu trưởng
            ChucVuNguoiKy = Session.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", "Hiệu trưởng%"));
            NguoiKy = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucVu.TenChucVu like ? and TinhTrang.TenTinhTrang like ? and ThongTinTruong=?", "Hiệu trưởng", "Đang làm việc", ThongTinTruong.Oid));
            NgayKy = HamDungChung.GetServerTime();

            UpdateChucVuList();
            UpdateNguoiKyList();  
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //Thiết lập thông tin hợp đồng
                if (!HopDongCu && NhanVien != null && NhanVien.HopDongHienTai != null)
                {
                    if (NhanVien.HopDongHienTai != null && !NhanVien.HopDongHienTai.HopDongCu)
                    {
                        Session.Save(NhanVien.HopDongHienTai);
                    }
                    NhanVien.HopDongHienTai = this;
                }
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoading();

            UpdateChucVuList();
            //UpdateNguoiKyList();
            UpdateGiayToList();
            //
            if (!string.IsNullOrEmpty(this.SoHopDong))
            {
                this.SoHopDongFull = SoHopDong;
            }
        }

        protected override void OnDeleting()
        {
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }

            if (!IsSaving)
            {
                //Thiết lập thông tin hợp đồng
                if (NhanVien != null && NhanVien.HopDongHienTai == this)
                {
                    XPCollection<HopDong> ListHopDong = new XPCollection<HopDong>(Session, 
                                                                                  CriteriaOperator.Parse("NhanVien =? and Oid != ?",
                                                                                                            NhanVien.Oid,
                                                                                                            this.Oid));
                    ListHopDong.Sorting.Add(new SortProperty("NgayKy", SortingDirection.Descending));
                    if(ListHopDong.Count > 0)
                        NhanVien.HopDongHienTai = ListHopDong[0];
                }
            }

            base.OnDeleting();

           
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NguoiKyList { get; set; }

        [Browsable(false)]
        public XPCollection<ChucVu> ChucVuList { get; set; }

        [Browsable(false)]
        public XPCollection<NhanVien> NVList { get; set; }

        //Cập nhật danh sách người ký
        public void UpdateNguoiKyList()
        {
            //if (NguoiKyList == null)
            //    NguoiKyList = new XPCollection<ThongTinNhanVien>(Session);
            //NguoiKyList.Criteria = HamDungChung.GetNguoiKyTenCriteria(PhanLoaiNguoiKy, ChucVuNguoiKy);
            if (NguoiKyList == null)
                NguoiKyList = new XPCollection<HoSo.ThongTinNhanVien>(Session, false);
            else
                NguoiKyList.Reload();
            if (ChucVuNguoiKy != null)
            {
                CriteriaOperator filter = HamDungChung.GetNguoiKyTenCriteria(PhanLoaiNguoiKy, ChucVuNguoiKy);
                XPCollection<HoSo.ThongTinNhanVien> ds = new XPCollection<ThongTinNhanVien>(Session, filter);
                foreach (HoSo.ThongTinNhanVien item in ds)
                {
                    NguoiKyList.Add(item);
                }
            }
        }

        //Cập nhật danh sách chức vụ
        private void UpdateChucVuList()
        {
            if (ChucVuList == null)
                ChucVuList = new XPCollection<ChucVu>(Session);

            ChucVuList.Criteria = CriteriaOperator.Parse("PhanLoai=2 or PhanLoai=0 or PhanLoai is null");
        }

        //Cập nhật hợp đồng mới nhất
        private void UpdateHopDongHienTai()
        {
            if (NhanVien.HopDongHienTai != null &&
                       NhanVien.HopDongHienTai != this &&
                       NhanVien.HopDongHienTai.NgayKy < NgayKy)
                NhanVien.HopDongHienTai = this;
            if (NhanVien.HopDongHienTai == null)
            {
                NhanVien.HopDongHienTai = this;
            }
        }

        //Cập nhật nơi làm việc
        // - Nếu là trường hoặc phòng ban thì giữ nguyên
        // - Nếu là bộ môn thì phải chuyển thành trường
        private void UpdateNoiLamViec()
        {
            if (NhanVien.BoPhan.LoaiBoPhan == LoaiBoPhanEnum.BoMonTrucThuocKhoa)
                BoPhan = NhanVien.BoPhan.BoPhanCha;
            else
                BoPhan = NhanVien.BoPhan;
        }

        private void UpdateGiayToList()
        {
            //if (GiayToList == null)
            //    GiayToList = new XPCollection<GiayToHoSo>(Session);

            if (NhanVien != null)
                GiayToList = NhanVien.ListGiayToHoSo;
                //GiayToList.Criteria = CriteriaOperator.Parse("HoSo=? and GiayTo.TenGiayTo like ?", NhanVien.Oid, "%Hợp đồng%");
        }

        /// <summary>
        /// Tạo số hợp đồng tự động
        /// </summary>
        protected virtual void TaoSoHopDong() { }

        /// <summary>
        /// Tạo trích yếu cho giấy tờ hồ sơ
        /// </summary>
        protected virtual void TaoTrichYeu() { }

        /// <summary>
        /// Xảy ra sau khi dữ liệu nhân viên thay đổi
        /// </summary>
        protected virtual void AfterNhanVienChanged() {}

        private void XemGiayToHoSo()
        {
            using (DialogUtil.AutoWait())
            {
                try
                {
                    byte[] data = FptProvider.DownloadFile(GiayToHoSo.DuongDanFile, HamDungChung.CauHinhChung.Username, HamDungChung.CauHinhChung.Password);
                    if (data != null)
                    {
                        string strTenFile = "TempFile.pdf";
                        //Lưu file vào thư mục bin\Debug
                        HamDungChung.SaveFilePDF(data, strTenFile);
                        //Đọc file pdf
                        frmGiayToViewer viewer = new frmGiayToViewer("TempFile.pdf");
                        viewer.ShowDialog();
                    }
                    else
                        XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch
                {
                    XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }

}
