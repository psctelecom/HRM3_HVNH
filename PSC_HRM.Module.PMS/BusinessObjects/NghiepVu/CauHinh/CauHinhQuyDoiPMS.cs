using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.NonPersistent;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.CauHinh;
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.PMS.NghiepVu
{


    //[ModelDefault("IsCloneable", "True")]
    [DefaultProperty("Caption")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Cấu hình quy đổi PMS")]
    [Appearance("Hide_CongThuc_QNU", TargetItems = "CongThucQuyDoi_RaDe;CongThucQuyDoi_ChamGK;CongThucQuyDoi_ChamTuLuan;"                                                 
                                                    + "CongThucQuyDoi_ChamTieuLuan;CongThucQuyDoi_ChamVanDap;CongThucQuyDoi_CoiThi;"
                                                    +"CongThucQuyDoi_HDLV;CongThucQuyDoi_TongHocVien",
                                                    Visibility = ViewItemVisibility.Hide,Criteria = "BacDaoTao.MaQuanLy <> 'SDH'")]

    [Appearance("Hide_CongThuc_HVNH", TargetItems = "CongThucQuyDoi_RaDe;CongThucQuyDoi_ChamGK;CongThucQuyDoi_ChamTuLuan;"
                                                    + "CongThucQuyDoi_ChamTieuLuan;CongThucQuyDoi_ChamVanDap;CongThucQuyDoi_CoiThi;"
                                                    + "CongThucQuyDoi_HDLV;CongThucQuyDoi_TongHocVien;"/*QNU*/
                                                    + "CongThucQuyDoiThucHanh;"
                                                    + "CongThucQuyDoiDoAn;"
                                                    + "CongThucQuyDoiBaiTapLon;"
                                                    + "CongThucQuyDoiLyThuyet_ThucHanh;"
		                                            + "CongThucQuyDoiLyThuyetCLC;"
		                                            + "CongThucQuyDoiThucTap;"
		                                            + "CongThucQuyDoiLuanAn;"
		                                            + "CongThucQuyDoiLyThuyetNguoiNuocNgoai;"
                                                    + "TongGioTNTH_DA_BTL;TongGioLyThuyetThaoLuan;"
                                                    + "HeSo_ThaoLuan;HeSo_DoAn;"
                                                    + "HeSo_BTL;HeSo_ChamThi;CongThucQuyDoiTongTietThucHien;CongThucQuyDoiSoTietDinhMuc;CongThucQuyDoiSoTietHopDong;CongThucQuyDoiSoTietPhuTroi;"
                                                    + "SoBaiTNTH_GioChuan;HeSo_GiangDay;BacDaoTao"
                                                    , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'NHH'")]
    [Appearance("Hide_CongThucKhac_HVNH", TargetItems = "CongThucQuyDoi_HDThucTeNgheNghiepCLC;CongThucQuyDoi_ChamThiHetHocPhanVanDap;CongThucQuyDoi_ChamThiHetHocPhanTieuLuan;"
                                                    + "CongThucQuyDoi_ChamBaoVeKLTNCLCTV;CongThucQuyDoi_ChamBaoVeKLTNCLCTA;CongThucQuyDoi_HDKLTNCLCTV;"                                              
                                                    + "CongThucQuyDoi_HDKLTNCLCTA"
                                                    , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat != 'NHH'")]
    [Appearance("Hide_CongThuc", TargetItems = "CongThucQuyDoi_ChamThiHetHocPhan;"
                                                    + "CongThucQuyDoi_ChamThucTapNgheNghiepCLC;"
                                                    + "CongThucQuyDoi_ChamCDTN;"
                                                    + "CongThucQuyDoi_ChamBaoVeKLTN;"
                                                    + "CongThucQuyDoi_HuongDanCDTN;"
                                                    + "CongThucQuyDoi_HDKLTN;"
                                                    + "CongThucQuyDoiLyThuyet_ThucHanh;"
                                                    + "CongThucQuyDoiLyThuyetCLC;"
                                                    + "CongThucQuyDoiThucTap;"
                                                    + "CongThucQuyDoiLuanAn;"
                                                    + "CongThucQuyDoiLyThuyetNguoiNuocNgoai;"
                                                    + "CongThucQuyDoi_PhuTrachCaThi;"
                                                    + "CongThucQuyDoi_ChamThiTN;"
                                                     + "CongThucQuyDoi_HDSVThamQuanThucTe;"
                                                     + "CongThucQuyDoi_HDVietCDTN;"
                                                     + "CongThucQuyDoi_HDDeTaiLuanVan;"
                                                     + "CongThucQuyDoi_GiaiDapThacMac;"
                                                     + "CongThucQuyDoi_HeThongHoa_OnThi;"
                                                     + "CongThucQuyDoi_SoanDeThi;"
                                                     + "CongThucQuyDoi_BoSungNganHangCauHoi;"
                                                     + "CongThucQuyDoi_RaDeTotNghiep;CongThucQuyDoiTongTietThucHien;CongThucQuyDoiSoTietDinhMuc;CongThucQuyDoiSoTietHopDong;CongThucQuyDoiSoTietPhuTroi;"
                                                     + "CongThucQuyDoi_RaDeThiHetHocPhan;SoGioChuan"/*HVNH*/
                                                    , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'QNU'")]
    [Appearance("Hide_CongThuc_DNU", TargetItems = "CongThucQuyDoi_ChamThiHetHocPhan;"
                                                    + "CongThucQuyDoi_ChamThucTapNgheNghiepCLC;"
                                                    + "CongThucQuyDoi_ChamCDTN;"
                                                    + "CongThucQuyDoi_ChamBaoVeKLTN;"
                                                    + "CongThucQuyDoi_HuongDanCDTN;"
                                                    + "CongThucQuyDoi_HDKLTN;"
                                                    + "CongThucQuyDoi_PhuTrachCaThi;"
                                                    + "CongThucQuyDoi_ChamThiTN;"
                                                     + "CongThucQuyDoi_HDSVThamQuanThucTe;"
                                                     + "CongThucQuyDoi_HDVietCDTN;"
                                                     + "CongThucQuyDoi_HDDeTaiLuanVan;"
                                                     + "CongThucQuyDoi_GiaiDapThacMac;"
                                                        + "CongThucQuyDoiLyThuyet_ThucHanh;"
                                                    + "CongThucQuyDoiLyThuyetCLC;"
                                                    + "CongThucQuyDoiThucTap;"
                                                    + "CongThucQuyDoiLuanAn;"
                                                    + "CongThucQuyDoiLyThuyetNguoiNuocNgoai;"
                                                     + "CongThucQuyDoi_HeThongHoa_OnThi;"
                                                     + "CongThucQuyDoi_SoanDeThi;"
                                                     + "CongThucQuyDoi_BoSungNganHangCauHoi;"
                                                     + "CongThucQuyDoi_RaDeTotNghiep;"
                                                     + "CongThucQuyDoi_RaDeThiHetHocPhan;"
                                                     + "HeSo_ThaoLuan;"
                                                     + "HeSo_DoAn;"
                                                     + "HeSo_BTL;"
                                                     + "SoBaiTNTH_GioChuan;"
                                                     + "HeSo_GiangDay;"
                                                     + "CongThucQuyDoiThaoLuan;"
                                                     + "CongThucQuyDoiChamBai;"
                                                     + "CongThucQuyDoiDoAn;"
                                                     + "CongThucQuyDoiBaiTapLon;"
                                                     + "TongGioTNTH_DA_BTL;"
                                                     + "TongGioQuyDoi"
                                                    , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'DNU'")]
    [Appearance("Hide_CongThuc_VHU", TargetItems = "CongThucQuyDoiThaoLuan;"
                                                   + "CongThucQuyDoiChamBai;"
                                                   + "CongThucQuyDoiBaiTapLon;"
                                                   + "TongGioLyThuyetThaoLuan;"
                                                   + "TongGioTNTH_DA_BTL;"
                                                   + "TongGioQuyDoi;"
                                                   + "CongThucQuyDoi_RaDe;"
                                                   + "CongThucQuyDoi_ChamGK;"
                                                   + "CongThucQuyDoi_ChamTuLuan;"
                                                   + "CongThucQuyDoi_ChamTieuLuan;"
                                                   + "CongThucQuyDoi_ChamVanDap;"
                                                   + "CongThucQuyDoi_CoiThi;"
                                                   + "CongThucQuyDoi_HDLV;"
                                                   + "CongThucQuyDoi_TongHocVien;"
                                                   + "HeSo_ThaoLuan;"
                                                   + "HeSo_DoAn;"
                                                   + "HeSo_BTL;"
                                                   + "HeSo_ChamThi;"
                                                   + "SoBaiTNTH_GioChuan;"
                                                   + "SoGioChuan;"
                                                   + "HeSo_GiangDay;"
                                                   + "CongThucQuyDoi_ChamThiHetHocPhan;"
                                                   + "CongThucQuyDoi_ChamThucTapNgheNghiepCLC;"
                                                   + "CongThucQuyDoi_ChamCDTN;"
                                                   + "CongThucQuyDoi_ChamBaoVeKLTN;"
                                                   + "CongThucQuyDoi_HuongDanCDTN;"
                                                   + "CongThucQuyDoi_HDKLTN;"
                                                   + "CongThucQuyDoi_PhuTrachCaThi;"
                                                   + "CongThucQuyDoi_ChamThiTN;"
                                                   + "CongThucQuyDoi_HDSVThamQuanThucTe;"
                                                   + "CongThucQuyDoi_HDVietCDTN;"
                                                   + "CongThucQuyDoi_HDDeTaiLuanVan;"
                                                   + "CongThucQuyDoi_GiaiDapThacMac;"
                                                   + "CongThucQuyDoi_HeThongHoa_OnThi;"
                                                   + "CongThucQuyDoi_SoanDeThi;"
                                                   + "CongThucQuyDoi_BoSungNganHangCauHoi;"
                                                   + "CongThucQuyDoi_RaDeTotNghiep;"
                                                   + "CongThucQuyDoi_RaDeThiHetHocPhan;"
                                                   + "CongThucQuyDoiTongTietThucHien;"
                                                   + "CongThucQuyDoiSoTietDinhMuc;"
                                                   + "CongThucQuyDoiSoTietHopDong;"
                                                   + "CongThucQuyDoiSoTietPhuTroi"
                                                    , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'VHU'")]
    [Appearance("Hide_CongThuc_UFM", TargetItems = "CongThucQuyDoiThaoLuan;"
                                                   + "CongThucQuyDoiChamBai;"
                                                   + "CongThucQuyDoiBaiTapLon;"
                                                   + "TongGioLyThuyetThaoLuan;"
                                                   + "TongGioTNTH_DA_BTL;"
                                                   + "TongGioQuyDoi;"
                                                   + "CongThucQuyDoi_RaDe;"
                                                   + "CongThucQuyDoi_ChamGK;"
                                                   + "CongThucQuyDoi_ChamTuLuan;"
                                                   + "CongThucQuyDoi_ChamTieuLuan;"
                                                   + "CongThucQuyDoi_ChamVanDap;"
                                                   + "CongThucQuyDoi_CoiThi;"
                                                   + "CongThucQuyDoi_HDLV;"
                                                   + "CongThucQuyDoi_TongHocVien;"
                                                   + "HeSo_ThaoLuan;"
                                                   + "HeSo_DoAn;"
                                                   + "HeSo_BTL;"
                                                   + "HeSo_ChamThi;"
                                                   + "SoBaiTNTH_GioChuan;"
                                                   + "SoGioChuan;"
                                                   + "HeSo_GiangDay;"
                                                   + "CongThucQuyDoi_ChamThiHetHocPhan;"
                                                   + "CongThucQuyDoi_ChamThucTapNgheNghiepCLC;"
                                                   + "CongThucQuyDoi_ChamCDTN;"
                                                   + "CongThucQuyDoi_ChamBaoVeKLTN;"
                                                   + "CongThucQuyDoi_HuongDanCDTN;"
                                                   + "CongThucQuyDoi_HDKLTN;"
                                                   + "CongThucQuyDoi_PhuTrachCaThi;"
                                                   + "CongThucQuyDoi_ChamThiTN;"
                                                   + "CongThucQuyDoi_HDSVThamQuanThucTe;"
                                                   + "CongThucQuyDoi_HDVietCDTN;"
                                                   + "CongThucQuyDoi_HDDeTaiLuanVan;"
                                                   + "CongThucQuyDoi_GiaiDapThacMac;"
                                                   + "CongThucQuyDoi_HeThongHoa_OnThi;"
                                                   + "CongThucQuyDoi_SoanDeThi;"
                                                   + "CongThucQuyDoi_BoSungNganHangCauHoi;"
                                                   + "CongThucQuyDoi_RaDeTotNghiep;"
                                                   + "CongThucQuyDoi_RaDeThiHetHocPhan;"
                                                   + "CongThucQuyDoiTongTietThucHien;"
                                                   + "CongThucQuyDoiSoTietDinhMuc;"
                                                   + "CongThucQuyDoiSoTietHopDong;"
                                                   + "CongThucQuyDoiSoTietPhuTroi;"                                                    
                                                   + "CongThucQuyDoiDoAn;"
                                                   + "CongThucQuyDoiLyThuyet_ThucHanh;"
                                                   + "CongThucQuyDoiLyThuyetCLC;"
                                                   + "CongThucQuyDoiThucTap;"
                                                   + "CongThucQuyDoiLuanAn;"
                                                   + "CongThucQuyDoiLyThuyetNguoiNuocNgoai"
                                                    , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'UFM'")]


    [Appearance("Hide_CongThuc_TinhTIen_QNU", TargetItems = "CongThucTinhTienThuLao_DotTamUng;CongThucTinhTienThuLao_DotTongKet;CongThucTinhGio_TinhThuLao",
                                                    Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat <> 'QNU'")]

    public class CauHinhQuyDoiPMS : BaseObject
    {
        #region KhaiBao
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;

        private BacDaoTao _BacDaoTao;
        private decimal _DonGiaCoHuu;
        private decimal _DonGiaThinhGiang;

        private string _CongThucTinhHeSoPMS;
        private bool _NgungApDung;
        private decimal _HeSo_ThaoLuan;
        private decimal _HeSo_DoAn;
        private decimal _HeSo_BTL;
        private decimal _HeSo_ChamThi;
        private int _SoBaiTNTH_GioChuan;
        private int _SoGioChuan;


        private decimal _HeSo_GiangDay;
        private decimal _DonGiaThanhToanVuotMuc;

        #region Công thức
        private string _CongThucQuyDoiLyThuyet;
        private string _CongThucQuyDoiThucHanh;
        private string _CongThucQuyDoiThaoLuan;
        private string _CongThucQuyDoiChamBai;
        private string _CongThucQuyDoiDoAn;
        private string _CongThucQuyDoiBaiTapLon;
        private string _TongGioLyThuyetThaoLuan;
        private string _TongGioTNTH_DA_BTL;
        private string _TongGioQuyDoi;
        //DNU
        private string _CongThucQuyDoiTongTietThucHien;
        private string _CongThucQuyDoiSoTietDinhMuc;
        private string _CongThucQuyDoiSoTietHopDong;
        private string _CongThucQuyDoiSoTietPhuTroi;
        #region VHU
        private string _CongThucQuyDoiLyThuyet_ThucHanh;
        private string _CongThucQuyDoiLyThuyetCLC;
        private string _CongThucQuyDoiThucTap;
        private string _CongThucQuyDoiLuanAn;
        private string _CongThucQuyDoiLyThuyetNguoiNuocNgoai;
        #endregion
        #endregion

        #region SDH
        private string _CongThucQuyDoi_RaDe;
        private string _CongThucQuyDoi_ChamGK;
        private string _CongThucQuyDoi_ChamTuLuan;
        private string _CongThucQuyDoi_ChamTieuLuan;
        private string _CongThucQuyDoi_ChamVanDap;
        private string _CongThucQuyDoi_CoiThi;
        private string _CongThucQuyDoi_HDLV;
        private string _CongThucQuyDoi_TongHocVien;
        #endregion

        #region Kê khai sau giảng
        private string _CongThucQuyDoi_ChamThiHetHocPhan;
        private string _CongThucQuyDoi_ChamThiHetHocPhanVanDap;//
        private string _CongThucQuyDoi_ChamThiHetHocPhanTieuLuan;//
        private string _CongThucQuyDoi_ChamThucTapNgheNghiepCLC;
        private string _CongThucQuyDoi_ChamCDTN;
        private string _CongThucQuyDoi_ChamBaoVeKLTN;
        private string _CongThucQuyDoi_ChamBaoVeKLTNCLCTV;//
        private string _CongThucQuyDoi_ChamBaoVeKLTNCLCTA;//
        private string _CongThucQuyDoi_HuongDanCDTN;
        private string _CongThucQuyDoi_HDKLTN;
        private string _CongThucQuyDoi_HDKLTNCLCTV;//
        private string _CongThucQuyDoi_HDKLTNCLCTA;//
        private string _CongThucQuyDoi_PhuTrachCaThi;
        private string _CongThucQuyDoi_HDThucTeNgheNghiepCLC;//
        //Kê khai
        private string _CongThucQuyDoi_ChamThiTN;
        private string _CongThucQuyDoi_HDSVThamQuanThucTe;
        private string _CongThucQuyDoi_HDVietCDTN;
        private string _CongThucQuyDoi_HDDeTaiLuanVan;
        private string _CongThucQuyDoi_GiaiDapThacMac;
        private string _CongThucQuyDoi_HeThongHoa_OnThi;
        private string _CongThucQuyDoi_SoanDeThi;
        private string _CongThucQuyDoi_BoSungNganHangCauHoi;
        private string _CongThucQuyDoi_RaDeTotNghiep;
        private string _CongThucQuyDoi_RaDeThiHetHocPhan;
        #endregion
        #endregion

        #region Thông tin
        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "False")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[VisibleInListView(false)]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }
        [ModelDefault("Caption", "Năm học")]
        //[VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading)
                    updateHocKyList();
            }
        }

        [ModelDefault("Caption", "Đơn giá thanh toán vượt mức")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        public decimal DonGiaThanhToanVuotMuc
        {
            get { return _DonGiaThanhToanVuotMuc; }
            set { SetPropertyValue("DonGiaThanhToanVuotMuc", ref _DonGiaThanhToanVuotMuc, value); }
        }

        [ModelDefault("Caption", "Học kỳ")]

        [DataSourceProperty("HocKyList", DataSourcePropertyIsNullMode.SelectAll)]
        [VisibleInListView(false)]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }
        [Browsable(false)]
        public XPCollection<HocKy> HocKyList { get; set; }
        public void updateHocKyList()
        {
            HocKyList = new XPCollection<HocKy>(Session);
            HocKyList.Criteria = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
            SortingCollection sortHK = new SortingCollection();
            sortHK.Add(new SortProperty("TuNgay", DevExpress.Xpo.DB.SortingDirection.Ascending));
            HocKyList.Sorting = sortHK;
            OnChanged("HocKyList");
        }
        [ModelDefault("Caption", "Bậc đào tạo")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        #endregion

        #region Công thức
        private string ExpressionType
        {
            get
            {
                return "PSC_HRM.Module.PMS.NghiepVu.ChonGiaTriLapCongThucPMS";
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức hệ số PMS")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucTinhHeSoPMS
        {
            get
            {
                return _CongThucTinhHeSoPMS;
            }
            set
            {
                SetPropertyValue("CongThucTinhHeSoPMS", ref _CongThucTinhHeSoPMS, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức quy đổi lý thuyết")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoiLyThuyet
        {
            get
            {
                return _CongThucQuyDoiLyThuyet;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoiLyThuyet", ref _CongThucQuyDoiLyThuyet, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức quy đổi thực hành")]
       // [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoiThucHanh
        {
            get
            {
                return _CongThucQuyDoiThucHanh;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoiThucHanh", ref _CongThucQuyDoiThucHanh, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức quy đổi thảo luận")]
       // [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoiThaoLuan
        {
            get
            {
                return _CongThucQuyDoiThaoLuan;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoiThaoLuan", ref _CongThucQuyDoiThaoLuan, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức quy đổi chấm bài")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoiChamBai
        {
            get
            {
                return _CongThucQuyDoiChamBai;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoiChamBai", ref _CongThucQuyDoiChamBai, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức quy đổi đồ án")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoiDoAn
        {
            get
            {
                return _CongThucQuyDoiDoAn;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoiDoAn", ref _CongThucQuyDoiDoAn, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức quy đổi BTL")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoiBaiTapLon
        {
            get
            {
                return _CongThucQuyDoiBaiTapLon;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoiBaiTapLon", ref _CongThucQuyDoiBaiTapLon, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức Tổng giờ Lý thuyết thảo luận")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string TongGioLyThuyetThaoLuan
        {
            get
            {
                return _TongGioLyThuyetThaoLuan;
            }
            set
            {
                SetPropertyValue("TongGioLyThuyetThaoLuan", ref _TongGioLyThuyetThaoLuan, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức TH_DA_BTL")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string TongGioTNTH_DA_BTL
        {
            get
            {
                return _TongGioTNTH_DA_BTL;
            }
            set
            {
                SetPropertyValue("TongGioTNTH_DA_BTL", ref _TongGioTNTH_DA_BTL, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tổng giờ quy đổi")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string TongGioQuyDoi
        {
            get
            {
                return _TongGioQuyDoi;
            }
            set
            {
                SetPropertyValue("TongGioQuyDoi", ref _TongGioQuyDoi, value);
            }
        }
        #endregion

        #region CT Sau DH

        [Size(-1)]
        [ModelDefault("Caption", "Công thức quy đổi - Ra đề")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoi_RaDe
        {
            get
            {
                return _CongThucQuyDoi_RaDe;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoi_RaDe", ref _CongThucQuyDoi_RaDe, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức quy đổi - Chấm GK")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoi_ChamGK
        {
            get
            {
                return _CongThucQuyDoi_ChamGK;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoi_ChamGK", ref _CongThucQuyDoi_ChamGK, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức quy đổi - Chấm tự luận")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoi_ChamTuLuan
        {
            get
            {
                return _CongThucQuyDoi_ChamTuLuan;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoi_ChamTuLuan", ref _CongThucQuyDoi_ChamTuLuan, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức quy đổi - Chấm Tiểu luận")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoi_ChamTieuLuan
        {
            get
            {
                return _CongThucQuyDoi_ChamTieuLuan;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoi_ChamTieuLuan", ref _CongThucQuyDoi_ChamTieuLuan, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức quy đổi - Vấn đáp")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoi_ChamVanDap
        {
            get
            {
                return _CongThucQuyDoi_ChamVanDap;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoi_ChamVanDap", ref _CongThucQuyDoi_ChamVanDap, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức quy đổi - Coi thi")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoi_CoiThi
        {
            get
            {
                return _CongThucQuyDoi_CoiThi;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoi_CoiThi", ref _CongThucQuyDoi_CoiThi, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức quy đổi - HDLV")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoi_HDLV
        {
            get
            {
                return _CongThucQuyDoi_HDLV;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoi_HDLV", ref _CongThucQuyDoi_HDLV, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức quy đổi - Tổng SV")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoi_TongHocVien
        {
            get
            {
                return _CongThucQuyDoi_TongHocVien;
            }
            set
            {
                SetPropertyValue("_CongThucQuyDoi_TongHocVien", ref _CongThucQuyDoi_TongHocVien, value);
            }
        }
        #endregion


        #region QNU - Tính tiền tài chính
        private string _CongThucTinhGio_TinhThuLao;
        private string _CongThucTinhTienThuLao_DotTamUng;
        private string _CongThucTinhTienThuLao_DotTongKet;
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức tính giờ (Tính thù lao)")]
        public string CongThucTinhGio_TinhThuLao
        {
            get { return _CongThucTinhGio_TinhThuLao; }
            set { SetPropertyValue("CongThucTinhGio_TinhThuLao", ref _CongThucTinhGio_TinhThuLao, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Tính tiền thu lao (Tạm ứng)")]
        public string CongThucTinhTienThuLao_DotTamUng
        {
            get { return _CongThucTinhTienThuLao_DotTamUng; }
            set { SetPropertyValue("CongThucTinhTienThuLao_DotTamUng", ref _CongThucTinhTienThuLao_DotTamUng, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Tính tiền thu lao (Tổng kết)")]
        public string CongThucTinhTienThuLao_DotTongKet
        {
            get { return _CongThucTinhTienThuLao_DotTongKet; }
            set { SetPropertyValue("CongThucTinhTienThuLao_DotTongKet", ref _CongThucTinhTienThuLao_DotTongKet, value); }
        }
        #endregion

        [ModelDefault("Caption", "Đơn giá (GV Cơ hữu)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        public decimal DonGiaCoHuu
        {
            get { return _DonGiaCoHuu; }
            set { SetPropertyValue("DonGiaCoHuu", ref _DonGiaCoHuu, value); }
        }
        [ModelDefault("Caption", "Đơn giá (GV thỉnh giảng)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        public decimal DonGiaThinhGiang
        {
            get { return _DonGiaThinhGiang; }
            set { SetPropertyValue("DonGiaThinhGiang", ref _DonGiaThinhGiang, value); }
        }
        #region Hệ số/ Số tiền/ Số giờ
        [ModelDefault("Caption", "Ngừng áp dụng")]
        public bool NgungApDung
        {
            get
            {
                return _NgungApDung;
            }
            set
            {
                SetPropertyValue("NgungApDung", ref _NgungApDung, value);
            }
        }
        [ModelDefault("Caption", "Hệ số thảo luận")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("HeSo_ThaoLuan", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        [VisibleInListView(false)]
        public decimal HeSo_ThaoLuan
        {
            get { return _HeSo_ThaoLuan; }
            set { SetPropertyValue("HeSo_ThaoLuan", ref _HeSo_ThaoLuan, value); }
        }

        [ModelDefault("Caption", "Hệ số đồ án")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("HeSo_DoAn", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        [VisibleInListView(false)]
        public decimal HeSo_DoAn
        {
            get { return _HeSo_DoAn; }
            set { SetPropertyValue("HeSo_DoAn", ref _HeSo_DoAn, value); }
        }

        [ModelDefault("Caption", "Hệ số bài tập lớn")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("HeSo_BTL", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        [VisibleInListView(false)]
        public decimal HeSo_BTL
        {
            get { return _HeSo_BTL; }
            set { SetPropertyValue("HeSo_BTL", ref _HeSo_BTL, value); }
        }

        [ModelDefault("Caption", "Hệ số bài chấm thi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("HeSo_ChamThi", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        [Browsable(false)]
        public decimal HeSo_ChamThi
        {
            get { return _HeSo_ChamThi; }
            set { SetPropertyValue("HeSo_ChamThi", ref _HeSo_ChamThi, value); }
        }

        [ModelDefault("Caption", "Số bài TNTH / Giờ chuẩn")]
        //[RuleRange("SoBaiTNTH_GioChuan", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        [VisibleInListView(false)]
        public int SoBaiTNTH_GioChuan
        {
            get { return _SoBaiTNTH_GioChuan; }
            set { SetPropertyValue("SoBaiTNTH_GioChuan", ref _SoBaiTNTH_GioChuan, value); }
        }

        [ModelDefault("Caption", "Số giờ chuẩn")]
        //[RuleRange("SoGioChuan", DefaultContexts.Save, 0.00, 10000, "Số giờ chuẩn > 0")]
        [VisibleInListView(false)]
        //[Browsable(false)]
        public int SoGioChuan
        {
            get { return _SoGioChuan; }
            set { SetPropertyValue("SoGioChuan", ref _SoGioChuan, value); }
        }

        [ModelDefault("Caption", "Hệ số giảng dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("HeSo_GiangDay", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        [VisibleInListView(false)]
        public decimal HeSo_GiangDay
        {
            get { return _HeSo_GiangDay; }
            set { SetPropertyValue("HeSo_GiangDay", ref _HeSo_GiangDay, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức chấm thi hết học phần ")]
        public string CongThucQuyDoi_ChamThiHetHocPhan
        {
            get { return _CongThucQuyDoi_ChamThiHetHocPhan; }
            set { SetPropertyValue("CongThucQuyDoi_ChamThiHetHocPhan", ref _CongThucQuyDoi_ChamThiHetHocPhan, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức chấm thi hết học phần vấn đáp")]
        public string CongThucQuyDoi_ChamThiHetHocPhanVanDap
        {
            get { return _CongThucQuyDoi_ChamThiHetHocPhanVanDap; }
            set { SetPropertyValue("CongThucQuyDoi_ChamThiHetHocPhanVanDap", ref _CongThucQuyDoi_ChamThiHetHocPhanVanDap, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức chấm thi hết học phần tiểu luận")]
        public string CongThucQuyDoi_ChamThiHetHocPhanTieuLuan
        {
            get { return _CongThucQuyDoi_ChamThiHetHocPhanTieuLuan; }
            set { SetPropertyValue("CongThucQuyDoi_ChamThiHetHocPhanTieuLuan", ref _CongThucQuyDoi_ChamThiHetHocPhanTieuLuan, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức chấm thực tập tốt nghiệp CLC")]
        public string CongThucQuyDoi_ChamThucTapNgheNghiepCLC
        {
            get { return _CongThucQuyDoi_ChamThucTapNgheNghiepCLC; }
            set { SetPropertyValue("CongThucQuyDoi_ChamThucTapNgheNghiepCLC", ref _CongThucQuyDoi_ChamThucTapNgheNghiepCLC, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức chấm CDTN")]
        public string CongThucQuyDoi_ChamCDTN
        {
            get { return _CongThucQuyDoi_ChamCDTN; }
            set { SetPropertyValue("CongThucQuyDoi_ChamCDTN", ref _CongThucQuyDoi_ChamCDTN, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức chấm Bảo Vệ KLTN")]
        public string CongThucQuyDoi_ChamBaoVeKLTN {
            get { return _CongThucQuyDoi_ChamBaoVeKLTN; }
            set { SetPropertyValue("CongThucQuyDoi_ChamBaoVeKLTN", ref _CongThucQuyDoi_ChamBaoVeKLTN, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức chấm Bảo Vệ KLTN CLC TV")]
        public string CongThucQuyDoi_ChamBaoVeKLTNCLCTV
        {
            get { return _CongThucQuyDoi_ChamBaoVeKLTNCLCTV; }
            set { SetPropertyValue("CongThucQuyDoi_ChamBaoVeKLTNCLCTV", ref _CongThucQuyDoi_ChamBaoVeKLTNCLCTV, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức chấm Bảo Vệ KLTN CLC TA")]
        public string CongThucQuyDoi_ChamBaoVeKLTNCLCTA
        {
            get { return _CongThucQuyDoi_ChamBaoVeKLTNCLCTA; }
            set { SetPropertyValue("CongThucQuyDoi_ChamBaoVeKLTNCLCTA", ref _CongThucQuyDoi_ChamBaoVeKLTNCLCTA, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức hướng Dẫn CDTN")]
        public string CongThucQuyDoi_HuongDanCDTN {
            get { return _CongThucQuyDoi_HuongDanCDTN; }
            set { SetPropertyValue("CongThucQuyDoi_HuongDanCDTN", ref _CongThucQuyDoi_HuongDanCDTN, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức hướng Dẫn KLTN")]
        public string CongThucQuyDoi_HDKLTN {
            get { return _CongThucQuyDoi_HDKLTN; }
            set { SetPropertyValue("CongThucQuyDoi_HDKLTN", ref _CongThucQuyDoi_HDKLTN, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức hướng Dẫn KLTN CLC TV")]
        public string CongThucQuyDoi_HDKLTNCLCTV
        {
            get { return _CongThucQuyDoi_HDKLTNCLCTV; }
            set { SetPropertyValue("CongThucQuyDoi_HDKLTNCLCTV", ref _CongThucQuyDoi_HDKLTNCLCTV, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức hướng Dẫn KLTN CLC TA")]
        public string CongThucQuyDoi_HDKLTNCLCTA
        {
            get { return _CongThucQuyDoi_HDKLTNCLCTA; }
            set { SetPropertyValue("CongThucQuyDoi_HDKLTNCLCTA", ref _CongThucQuyDoi_HDKLTNCLCTA, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức hướng Dẫn thực tế NN CLC")]
        public string CongThucQuyDoi_HDThucTeNgheNghiepCLC
        {
            get { return _CongThucQuyDoi_HDThucTeNgheNghiepCLC; }
            set { SetPropertyValue("CongThucQuyDoi_HDThucTeNgheNghiepCLC", ref _CongThucQuyDoi_HDThucTeNgheNghiepCLC, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức phụ đạo sinh viên nước ngoài")]
        public string CongThucQuyDoi_PhuTrachCaThi {
            get { return _CongThucQuyDoi_PhuTrachCaThi; }
            set { SetPropertyValue("CongThucQuyDoi_PhuTrachCaThi", ref _CongThucQuyDoi_PhuTrachCaThi, value); }
        }
        #endregion
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Chấm thi tốt nghiệp")]
        public string CongThucQuyDoi_ChamThiTN
        {
            get { return _CongThucQuyDoi_ChamThiTN; }
            set { SetPropertyValue("CongThucQuyDoi_ChamThiTN", ref _CongThucQuyDoi_ChamThiTN, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "HD sinh viên  tham quan thực tế")]
        public string CongThucQuyDoi_HDSVThamQuanThucTe
        {
            get { return _CongThucQuyDoi_HDSVThamQuanThucTe; }
            set { SetPropertyValue("CongThucQuyDoi_HDSVThamQuanThucTe", ref _CongThucQuyDoi_HDSVThamQuanThucTe, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "HD viết CDTN")]
        public string CongThucQuyDoi_HDVietCDTN
        {
            get { return _CongThucQuyDoi_HDVietCDTN; }
            set { SetPropertyValue("CongThucQuyDoi_HDVietCDTN", ref _CongThucQuyDoi_HDVietCDTN, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "HD đề tài luận văn")]
        public string CongThucQuyDoi_HDDeTaiLuanVan
        {
            get { return _CongThucQuyDoi_HDDeTaiLuanVan; }
            set { SetPropertyValue("CongThucQuyDoi_HDDeTaiLuanVan", ref _CongThucQuyDoi_HDDeTaiLuanVan, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Giải đáp thắc mắc")]
        public string CongThucQuyDoi_GiaiDapThacMac
        {
            get { return _CongThucQuyDoi_GiaiDapThacMac; }
            set { SetPropertyValue("CongThucQuyDoi_GiaiDapThacMac", ref _CongThucQuyDoi_GiaiDapThacMac, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Hệ thống hóa và ôn thi cuối khóa")]
        public string CongThucQuyDoi_HeThongHoa_OnThi
        {
            get { return _CongThucQuyDoi_HeThongHoa_OnThi; }
            set { SetPropertyValue("CongThucQuyDoi_HeThongHoa_OnThi", ref _CongThucQuyDoi_HeThongHoa_OnThi, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Soạn bộ đề thi")]
        public string CongThucQuyDoi_SoanDeThi
        {
            get { return _CongThucQuyDoi_SoanDeThi; }
            set { SetPropertyValue("CongThucQuyDoi_SoanDeThi", ref _CongThucQuyDoi_SoanDeThi, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Bổ sung ngân hàng câu hỏi, đề thi")]
        public string CongThucQuyDoi_BoSungNganHangCauHoi
        {
            get { return _CongThucQuyDoi_BoSungNganHangCauHoi; }
            set { SetPropertyValue("CongThucQuyDoi_BoSungNganHangCauHoi", ref _CongThucQuyDoi_BoSungNganHangCauHoi, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Ra đề thi tốt nghiệp")]
        public string CongThucQuyDoi_RaDeTotNghiep
        {
            get { return _CongThucQuyDoi_RaDeTotNghiep; }
            set { SetPropertyValue("CongThucQuyDoi_RaDeTotNghiep", ref _CongThucQuyDoi_RaDeTotNghiep, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Ra đề thi hết học phần")]
        public string CongThucQuyDoi_RaDeThiHetHocPhan
        {
            get { return _CongThucQuyDoi_RaDeThiHetHocPhan; }
            set { SetPropertyValue("CongThucQuyDoi_RaDeThiHetHocPhan", ref _CongThucQuyDoi_RaDeThiHetHocPhan, value); }
        }
        //DNU
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Tổng tiết thực hiện")]
        public string CongThucQuyDoiTongTietThucHien
        {
            get { return _CongThucQuyDoiTongTietThucHien; }
            set { SetPropertyValue("CongThucQuyDoiTongTietThucHien", ref _CongThucQuyDoiTongTietThucHien, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Số tiết định mức")]
        public string CongThucQuyDoiSoTietDinhMuc
        {
            get { return _CongThucQuyDoiSoTietDinhMuc; }
            set { SetPropertyValue("CongThucQuyDoiSoTietDinhMuc", ref _CongThucQuyDoiSoTietDinhMuc, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Số tiết hợp đồng")]
        public string CongThucQuyDoiSoTietHopDong
        {
            get { return _CongThucQuyDoiSoTietHopDong; }
            set { SetPropertyValue("CongThucQuyDoiSoTietHopDong", ref _CongThucQuyDoiSoTietHopDong, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Số tiết phụ trội")]
        public string CongThucQuyDoiSoTietPhuTroi
        {
            get { return _CongThucQuyDoiSoTietPhuTroi; }
            set { SetPropertyValue("CongThucQuyDoiSoTietPhuTroi", ref _CongThucQuyDoiSoTietPhuTroi, value); }
        }
        #region VHU
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức quy đổi lý thuyết thực hành")]
        public string CongThucQuyDoiLyThuyet_ThucHanh
        {
            get { return _CongThucQuyDoiLyThuyet_ThucHanh; }
            set { SetPropertyValue("CongThucQuyDoiLyThuyet_ThucHanh", ref _CongThucQuyDoiLyThuyet_ThucHanh, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức quy đổi lý thuyết CLC")]
        public string CongThucQuyDoiLyThuyetCLC
        {
            get { return _CongThucQuyDoiLyThuyetCLC; }
            set { SetPropertyValue("CongThucQuyDoiLyThuyetCLC", ref _CongThucQuyDoiLyThuyetCLC, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức quy đổi thực tập")]
        public string CongThucQuyDoiThucTap
        {
            get { return _CongThucQuyDoiThucTap; }
            set { SetPropertyValue("CongThucQuyDoiThucTap", ref _CongThucQuyDoiThucTap, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức quy đổi luận án")]
        public string CongThucQuyDoiLuanAn
        {
            get { return _CongThucQuyDoiLuanAn; }
            set { SetPropertyValue("CongThucQuyDoiLuanAn", ref _CongThucQuyDoiLuanAn, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức quy đổi lý thuyết người nước ngoài")]
        public string CongThucQuyDoiLyThuyetNguoiNuocNgoai
        {
            get { return _CongThucQuyDoiLyThuyetNguoiNuocNgoai; }
            set { SetPropertyValue("CongThucQuyDoiLyThuyetNguoiNuocNgoai", ref _CongThucQuyDoiLyThuyetNguoiNuocNgoai, value); }
        }
        #endregion
        #region kê khai sau giảng

        #endregion
        public CauHinhQuyDoiPMS(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            CauHinhChung chc = Session.FindObject<CauHinhChung>(CriteriaOperator.Parse("ThongTinTruong =?", ThongTinTruong.Oid));
            if (chc != null)
                SoGioChuan = chc.SoGioChuan;
        }
        protected override void OnSaving()
        {
            base.OnSaving();
            if (SoGioChuan == 0 && NamHoc != null)
            {
                CauHinhChung chc = Session.FindObject<CauHinhChung>(CriteriaOperator.Parse("NamHoc =?", NamHoc.Oid));
                if (chc != null)
                    SoGioChuan = chc.SoGioChuan;
            }
            if (CongThucQuyDoiLyThuyet == string.Empty)
                CongThucQuyDoiLyThuyet = "0";

            if (CongThucQuyDoiThucHanh == string.Empty)
                CongThucQuyDoiThucHanh = "0";

            if (CongThucQuyDoiThaoLuan == string.Empty)
                CongThucQuyDoiThaoLuan = "0";

            if (CongThucQuyDoiChamBai == string.Empty)
                CongThucQuyDoiChamBai = "0";

            if (CongThucQuyDoiDoAn == string.Empty)
                CongThucQuyDoiDoAn = "0";

            if (CongThucQuyDoiBaiTapLon == string.Empty)
                CongThucQuyDoiBaiTapLon = "0";

            if (TongGioLyThuyetThaoLuan == string.Empty)
                TongGioLyThuyetThaoLuan = "0";

            if (TongGioTNTH_DA_BTL == string.Empty)
                TongGioTNTH_DA_BTL = "0";

            if (TongGioQuyDoi == string.Empty)
                TongGioQuyDoi = "0";
        }
    }
}