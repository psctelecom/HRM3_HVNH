using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.HopDong
{
    [ImageName("BO_HopDong")]
    [DefaultProperty("NhanVien")]
    [ModelDefault("Caption", "Đề nghị mời giảng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyDeNghiMoiGiang;NhanVien")]
    [Appearance("TinhTienTheoDonGiaCLC_True", TargetItems = "DonGiaCLC", Visibility = ViewItemVisibility.Hide, Criteria = "TinhTienTheoDonGiaCLC = False")]
    [Appearance("TinhTienTheoDonGiaCLC_False", TargetItems = "DonGia", Visibility = ViewItemVisibility.Hide, Criteria = "TinhTienTheoDonGiaCLC = True")]
    [Appearance("TheoDeNghiKhac_True", TargetItems = "DonGiaCLC;DonGia", Enabled = true, Criteria = "TheoDeNghiKhac = True")]
    [Appearance("TheoDeNghiKhac_False", TargetItems = "DonGiaCLC;DonGia", Enabled = false, Criteria = "TheoDeNghiKhac = False")]
    [Appearance("DeNghiMoiGiang", TargetItems = "LoaiHopDong;NgayLap;NhanVien;NgaySinh;HocHam;HocVi;DonGia;DonGiaCLC;TongSoTiet", Enabled = false, Criteria = "DongY")]
    [Appearance("Edit", TargetItems = "DongY", Enabled = false, Criteria = "!AcountType")]
    public class DeNghiMoiGiang : BaseObject, IBoPhan
    {
        private bool _Chon;
        private TaoHopDongThinhGiangEnum _LoaiHopDong;
        private QuanLyDeNghiMoiGiang _QuanLyDeNghiMoiGiang;
        private DateTime _NgayLap;
        private BoPhan _BoPhan;
        private string _NoiLamViec;
        private NhanVien _NhanVien;
        private DateTime _NgaySinh;
        private HocHam _HocHam;
        private HocVi _HocVi;
        private decimal _DonGia;
        private bool _DongY;
        private bool _LapHopDong;
        private bool _TinhTienTheoDonGiaCLC;
        private bool _TheoDeNghiKhac;
        private decimal _DonGiaCLC;
        private int _TongSoTiet;
        private decimal _DonGiaDB;

        [ModelDefault("Caption", "Chọn")]
        [NonPersistent]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ModelDefault("Caption", "Loại hợp đồng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public TaoHopDongThinhGiangEnum LoaiHopDong
        {
            get
            {
                return _LoaiHopDong;
            }
            set
            {
                SetPropertyValue("LoaiHopDong", ref _LoaiHopDong, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý đề nghị mời giảng")]
        [Association("QuanLyDeNghiMoiGiang-ListDeNghiMoiGiang")]
        public QuanLyDeNghiMoiGiang QuanLyDeNghiMoiGiang
        {
            get
            {
                return _QuanLyDeNghiMoiGiang;
            }
            set
            {
                SetPropertyValue("QuanLyDeNghiMoiGiang", ref _QuanLyDeNghiMoiGiang, value);
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
            }
        }


        [ModelDefault("Caption", "Bộ phận")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
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
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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
                    #region 
                    //if (string.IsNullOrEmpty(((GiangVienThinhGiang)NhanVien).DonViCongTac))
                    //{
                    //    DialogUtil.ShowInfo(string.Format("[{0}] - chưa có thông tin đơn vị công tác.", NhanVien.HoTen));
                    //    value = null;
                    //    BoPhan = null;
                    //    NoiLamViec = string.Empty;
                    //    HocHam = null;
                    //    HocVi = null;
                    //    DonGia = 0;
                    //    DonGiaCLC = 0;

                    //    return;
                    //}
                    //else
                    //{
                    //    NoiLamViec = ((GiangVienThinhGiang)NhanVien).DonViCongTac;
                    //}
                    //if (((GiangVienThinhGiang)NhanVien).NhanVienTrinhDo.HocHam == null && ((GiangVienThinhGiang)NhanVien).HocVi == null)
                    //{
                    //    DialogUtil.ShowInfo(string.Format("[{0}] - chưa có thông tin học hạm, học vị.", NhanVien.HoTen));
                    //    value = null;
                    //    BoPhan = null;
                    //    NoiLamViec = string.Empty;
                    //    HocHam = null;
                    //    HocVi = null;
                    //    DonGia = 0;
                    //    DonGiaCLC = 0;

                    //    return;
                    //}
                    //if (((GiangVienThinhGiang)NhanVien).NgaySinh == null)
                    //{
                    //    DialogUtil.ShowInfo(string.Format("[{0}] - chưa có ngày sinh.", NhanVien.HoTen));
                    //    value = null;
                    //    BoPhan = null;
                    //    NoiLamViec = string.Empty;
                    //    HocHam = null;
                    //    HocVi = null;
                    //    DonGia = 0;
                    //    DonGiaCLC = 0;

                    //    return;
                    //}
                    //if (string.IsNullOrEmpty(((GiangVienThinhGiang)NhanVien).CMND))
                    //{
                    //    DialogUtil.ShowInfo(string.Format("[{0}] - chưa có chứng minh.", NhanVien.HoTen));
                    //    value = null;
                    //    BoPhan = null;
                    //    NoiLamViec = string.Empty;
                    //    HocHam = null;
                    //    HocVi = null;
                    //    DonGia = 0;
                    //    DonGiaCLC = 0;

                    //    return;
                    //}
                    //if (string.IsNullOrEmpty(((GiangVienThinhGiang)NhanVien).DienThoaiDiDong) && string.IsNullOrEmpty(((GiangVienThinhGiang)NhanVien).DienThoaiNhaRieng))
                    //{
                    //    DialogUtil.ShowInfo(string.Format("[{0}] - chưa có điện thoại.", NhanVien.HoTen));
                    //    value = null;
                    //    BoPhan = null;
                    //    NoiLamViec = string.Empty;
                    //    HocHam = null;
                    //    HocVi = null;
                    //    DonGia = 0;
                    //    DonGiaCLC = 0;

                    //    return;
                    //}
                    //if (string.IsNullOrEmpty(((GiangVienThinhGiang)NhanVien).NhanVienThongTinLuong.MaSoThue))
                    //{
                    //    DialogUtil.ShowInfo(string.Format("[{0}] - chưa có mã số thuế.", NhanVien.HoTen));
                    //    value = null;
                    //    BoPhan = null;
                    //    NoiLamViec = string.Empty;
                    //    HocHam = null;
                    //    HocVi = null;
                    //    DonGia = 0;
                    //    DonGiaCLC = 0;

                    //    return;
                    //}
                    //TaiKhoanNganHang taiKhoanNganHang = Session.FindObject<TaiKhoanNganHang>(CriteriaOperator.Parse("NhanVien=?", NhanVien.Oid));
                    //if (taiKhoanNganHang == null || (taiKhoanNganHang != null && (string.IsNullOrEmpty(taiKhoanNganHang.SoTaiKhoan) || taiKhoanNganHang.NganHang == null)))
                    //{
                    //    DialogUtil.ShowInfo(string.Format("[{0}] - chưa có tài khoản ngân hàng.", NhanVien.HoTen));
                    //    value = null;
                    //    BoPhan = null;
                    //    NoiLamViec = string.Empty;
                    //    HocHam = null;
                    //    HocVi = null;
                    //    DonGia = 0;
                    //    DonGiaCLC = 0;

                    //    return;
                    //}
                    #endregion

                    TaiKhoanNganHang taiKhoanNganHang = Session.FindObject<TaiKhoanNganHang>(CriteriaOperator.Parse("NhanVien=?", NhanVien.Oid));

                    if (
                        string.IsNullOrEmpty(((GiangVienThinhGiang)NhanVien).DonViCongTac)
                        || (((GiangVienThinhGiang)NhanVien).NhanVienTrinhDo.HocHam == null && ((GiangVienThinhGiang)NhanVien).HocVi == null) 
                        ||((NhanVien).NgaySinh == null)
                        || (string.IsNullOrEmpty((((GiangVienThinhGiang)NhanVien).CMND))
                        || (string.IsNullOrEmpty(((GiangVienThinhGiang)NhanVien).DienThoaiDiDong) && string.IsNullOrEmpty(((GiangVienThinhGiang)NhanVien).DienThoaiNhaRieng))
                        || (string.IsNullOrEmpty(((GiangVienThinhGiang)NhanVien).NhanVienThongTinLuong.MaSoThue))
                        || (taiKhoanNganHang == null || (taiKhoanNganHang != null && (string.IsNullOrEmpty(taiKhoanNganHang.SoTaiKhoan) || taiKhoanNganHang.NganHang == null))))
                        )
                    {
                        string ChuoiThongBao = "Chưa có: ";
                        if(string.IsNullOrEmpty(((GiangVienThinhGiang)NhanVien).DonViCongTac))
                            ChuoiThongBao += "\nThông tin đơn vị công tác";
                        if((((GiangVienThinhGiang)NhanVien).NhanVienTrinhDo.HocHam == null && ((GiangVienThinhGiang)NhanVien).HocVi == null))
                             ChuoiThongBao += "\nThông tin học hạm, học vị";
                        if (((GiangVienThinhGiang)NhanVien).NgaySinh == null)
                            ChuoiThongBao += "\nNgày sinh";
                        if(string.IsNullOrEmpty(((GiangVienThinhGiang)NhanVien).CMND)) 
                            ChuoiThongBao += "\nChứng minh";
                        if(string.IsNullOrEmpty(((GiangVienThinhGiang)NhanVien).DienThoaiDiDong) && string.IsNullOrEmpty(((GiangVienThinhGiang)NhanVien).DienThoaiNhaRieng))
                           ChuoiThongBao += "\nĐiện thoại";
                        if(string.IsNullOrEmpty(((GiangVienThinhGiang)NhanVien).NhanVienThongTinLuong.MaSoThue))
                           ChuoiThongBao += "\nMã số thuế";
                        if(taiKhoanNganHang ==null || (taiKhoanNganHang!= null && (string.IsNullOrEmpty(taiKhoanNganHang.SoTaiKhoan) || taiKhoanNganHang.NganHang == null)))
                           ChuoiThongBao += "\nTài khoản ngân hàng";

                        DialogUtil.ShowInfo(string.Format("[{0}] - " + ChuoiThongBao, NhanVien.HoTen));
                        value = null;
                        BoPhan = null;
                        NoiLamViec = string.Empty;
                        HocHam = null;
                        HocVi = null;
                        DonGia = 0;
                        DonGiaCLC = 0;
                        //
                        return;
                    }
                    else
                    {
                        NoiLamViec = ((GiangVienThinhGiang)NhanVien).DonViCongTac;
                    }

                    //Lấy thông tin cơ bản của cán bộ
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    //
                    if (NhanVien.NhanVienTrinhDo != null)
                    {
                        HocHam = NhanVien.NhanVienTrinhDo.HocHam;
                    }
                    //
                    NgaySinh = NhanVien.NgaySinh;
                    //
                    if ((NhanVien as GiangVienThinhGiang) != null)
                    {
                        HocVi = (NhanVien as GiangVienThinhGiang).HocVi;
                    }
                    //
                   
                    DonGia = HamDungChung.GetSoTien1Tiet(NhanVien,Session);
                    DonGiaCLC = HamDungChung.GetSoTienChatLuongCao(NhanVien, Session);
                    
                }
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

        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get
            {
                return _HocHam;
            }
            set
            {
                SetPropertyValue("HocHam", ref _HocHam, value);
            }
        }

        [ModelDefault("Caption", "Học vị")]
        public HocVi HocVi
        {
            get
            {
                return _HocVi;
            }
            set
            {
                SetPropertyValue("HocVi", ref _HocVi, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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

        [ModelDefault("Caption", "Đơn giá CLC")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal DonGiaCLC
        {
            get
            {
                return _DonGiaCLC;
            }
            set
            {
                SetPropertyValue("DonGiaCLC", ref _DonGiaCLC, value);
            }
        }

        [ModelDefault("Caption", "Tính tiền theo đơn giá CLC")]
        [ImmediatePostData]
        public bool TinhTienTheoDonGiaCLC
        {
            get
            {
                return _TinhTienTheoDonGiaCLC;
            }
            set
            {
                SetPropertyValue("TinhTienTheoDonGiaCLC", ref _TinhTienTheoDonGiaCLC, value);
            }
        }

        [ModelDefault("Caption", "Tổng số tiết")]
        [NonPersistent]
        public int TongSoTiet
        {
            get
            {

                return _TongSoTiet;
            }
            set
            {

                SetPropertyValue("TongSoTiet", ref _TongSoTiet, value);
            }
        }


        [ModelDefault("Caption", "Theo đề nghị khác")]
        [ImmediatePostData]
        public bool TheoDeNghiKhac
        {
            get
            {
                return _TheoDeNghiKhac;
            }
            set
            {
                SetPropertyValue("TheoDeNghiKhac", ref _TheoDeNghiKhac, value);
            }
        }

        [ModelDefault("Caption", "Đồng ý")]
        public bool DongY
        {
            get
            {
                return _DongY;
            }
            set
            {
                SetPropertyValue("DongY", ref _DongY, value);
            }
        }

        [ModelDefault("Caption", "Lập hợp đồng")]
        [ModelDefault("AllowEdit", "False")]
        public bool LapHopDong
        {
            get
            {
                return _LapHopDong;
            }
            set
            {
                SetPropertyValue("LapHopDong", ref _LapHopDong, value);
            }
        }

        [Aggregated]
        [ImmediatePostData]
        [ModelDefault("Caption", "Danh sách môn")]
        [Association("DeNghiMoiGiang-ListChiTietDeNghiMoiGiang")]
        public XPCollection<ChiTietDeNghiMoiGiang> ListChiTietDeNghiMoiGiang
        {
            get
            {
                return GetCollection<ChiTietDeNghiMoiGiang>("ListChiTietDeNghiMoiGiang");

            }
        }

        [ModelDefault("Caption", "Đơn giá đặc biệt")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal DonGiaDB
        {
            get
            {
                return _DonGiaDB;
            }
            set
            {
                SetPropertyValue("DonGiaDB", ref _DonGiaDB, value);
            }
        }
      
        [NonPersistent]
        [Browsable(false)]
        private bool AcountType { get; set; }

        public DeNghiMoiGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;
            NgayLap = HamDungChung.GetServerTime();
            //
            UpdateNhanVienList();
            //
            if (HamDungChung.AcountType.Equals("ToChuc"))
            {
                AcountType = true;
            }
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateNhanVienList();
            MaTruong = TruongConfig.MaTruong;
            //
            if (HamDungChung.AcountType.Equals("ToChuc"))
            {
                AcountType = true;
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();

        }
        [Browsable(false)]
        public XPCollection<NhanVien> NVList { get; set; }

        protected void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<NhanVien>(Session, HamDungChung.GetCriteriaGiangVienThinhGiang(Session));
        }

        [NonPersistent]
        [Browsable(false)]
        private string MaTruong { get; set; }

    }
}
