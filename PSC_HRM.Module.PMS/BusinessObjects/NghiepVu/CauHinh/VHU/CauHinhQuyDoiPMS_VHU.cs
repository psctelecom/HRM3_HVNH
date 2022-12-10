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
    //[ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Cấu hình quy đổi PMS(QC94)")]

    public class CauHinhQuyDoiPMS_VHU : BaseObject
    {
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

        private string _CongThucQuyDoiLyThuyet;
        private string _CongThucQuyDoiThucHanh;
        private string _CongThucQuyDoiDoAn;
        private string _CongThucQuyDoiLyThuyet_ThucHanh;
        private string _CongThucQuyDoiLyThuyetCLC;
        private string _CongThucQuyDoiThucTap;
        private string _CongThucQuyDoiLuanAn;
        private string _CongThucQuyDoiLyThuyetNguoiNuocNgoai;

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
        [Browsable(false)]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

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

        [ModelDefault("Caption", "Đơn giá (GV Cơ hữu)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        [Browsable(false)]
        public decimal DonGiaCoHuu
        {
            get { return _DonGiaCoHuu; }
            set { SetPropertyValue("DonGiaCoHuu", ref _DonGiaCoHuu, value); }
        }
        [ModelDefault("Caption", "Đơn giá (GV thỉnh giảng)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        [Browsable(false)]
        public decimal DonGiaThinhGiang
        {
            get { return _DonGiaThinhGiang; }
            set { SetPropertyValue("DonGiaThinhGiang", ref _DonGiaThinhGiang, value); }
        }
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
        [Browsable(false)]
        public decimal HeSo_ThaoLuan
        {
            get { return _HeSo_ThaoLuan; }
            set { SetPropertyValue("HeSo_ThaoLuan", ref _HeSo_ThaoLuan, value); }
        }

        [ModelDefault("Caption", "Hệ số đồ án")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [Browsable(false)]
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
        [Browsable(false)]
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
        [Browsable(false)]
        public int SoBaiTNTH_GioChuan
        {
            get { return _SoBaiTNTH_GioChuan; }
            set { SetPropertyValue("SoBaiTNTH_GioChuan", ref _SoBaiTNTH_GioChuan, value); }
        }

        [ModelDefault("Caption", "Số giờ chuẩn")]
        //[RuleRange("SoGioChuan", DefaultContexts.Save, 0.00, 10000, "Số giờ chuẩn > 0")]
        [VisibleInListView(false)]
        [Browsable(false)]
        //[Browsable(false)]
        public int SoGioChuan
        {
            get { return _SoGioChuan; }
            set { SetPropertyValue("SoGioChuan", ref _SoGioChuan, value); }
        }

        [ModelDefault("Caption", "Hệ số giảng dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [Browsable(false)]
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

        [Aggregated]
        [Association("CauHinhQuyDoiPMS_VHU-ListChiTietCauHinhQuyDoiPMS_VHU")]
        [ModelDefault("Caption", "Công thức theo loại")]
        public XPCollection<ChiTietCauHinhQuyDoiPMS_VHU> ListChiTietCauHinhQuyDoiPMS_VHU
        {
            get
            {
                return GetCollection<ChiTietCauHinhQuyDoiPMS_VHU>("ListChiTietCauHinhQuyDoiPMS_VHU");
            }
        }

        public CauHinhQuyDoiPMS_VHU(Session session) : base(session) { }

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

        }
    }
}

