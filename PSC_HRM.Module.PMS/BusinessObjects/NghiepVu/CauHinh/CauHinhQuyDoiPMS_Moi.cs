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
    [DefaultProperty("Caption")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Cấu hình quy đổi PMS(Mới)")]
    [Appearance("Hide_NEU", TargetItems = "KyTinhPMS;HocKy"
            , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.MaQuanLy = 'NEU'")]
    [Appearance("Hide_HUFLIT", TargetItems = "KyTinhPMS;HeDaoTao;CongThucTinhTongTiet;CongThucTinhTongHeSo;CongThucQuyDoiGio;CongThucQuyDoiGio_LyThuyet;CongThucQuyDoiGio_ThucHanh"
            , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.MaQuanLy = 'HUFLIT'")]
    public class CauHinhQuyDoiPMS_Moi : ThongTinChungPMS
    {
        private HeDaoTao _HeDaoTao;

        [ModelDefault("Caption", "Hệ đào tạo")]
        [VisibleInListView(false)]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set { _HeDaoTao = value; }
        }
        
        private string _CongThucTinhTongTiet;
        private string _CongThucTinhTongHeSo;
        private string _CongThucQuyDoiGio;
        private string _CongThucQuyDoiGio_LyThuyet;
        private string _CongThucQuyDoiGio_ThucHanh;
        private string _CongThucTinhSoTietThanhToan;
        private string _CongThucTinhTienVuotGio;
        private decimal _DonGiaCoHuu;
        private decimal _DonGiaThinhGiang;

       
        #region Công thức
        private string ExpressionType
        {
            get
            {
                return "PSC_HRM.Module.PMS.NghiepVu.ChonGiaTriLapCongThucPMS";
            }
        }
        [ModelDefault("Caption", "Tính tổng tiết")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucTinhTongTiet
        {
            get
            {
                return _CongThucTinhTongTiet;
            }
            set
            {
                SetPropertyValue("CongThucTinhTongTiet", ref _CongThucTinhTongTiet, value);
            }
        }
        [ModelDefault("Caption", "Tính tổng hệ số")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucTinhTongHeSo
        {
            get
            {
                return _CongThucTinhTongHeSo;
            }
            set
            {
                SetPropertyValue("CongThucTinhTongHeSo", ref _CongThucTinhTongHeSo, value);
            }
        }

        [ModelDefault("Caption", "Công thức quy đổi giờ chuẩn")]        
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoiGio
        {
            get
            {
                return _CongThucQuyDoiGio;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoiGio", ref _CongThucQuyDoiGio, value);
            }
        }
        [ModelDefault("Caption", "Công thức quy đổi giờ LT")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoiGio_LyThuyet
        {
            get
            {
                return _CongThucQuyDoiGio_LyThuyet;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoiGio_LyThuyet", ref _CongThucQuyDoiGio_LyThuyet, value);
            }
        }
        [ModelDefault("Caption", "Công thức quy đổi giờ TH")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoiGio_ThucHanh
        {
            get
            {
                return _CongThucQuyDoiGio_ThucHanh;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoiGio_ThucHanh", ref _CongThucQuyDoiGio_ThucHanh, value);
            }
        }
        [ModelDefault("Caption", "Công thức tính số tiết vượt giờ")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucTinhSoTietThanhToan
        {
            get
            {
                return _CongThucTinhSoTietThanhToan;
            }
            set
            {
                SetPropertyValue("CongThucTinhSoTietThanhToan", ref _CongThucTinhSoTietThanhToan, value);
            }
        }
        [ModelDefault("Caption", "Công thức tính tiền vượt giờ")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucTinhTienVuotGio
        {
            get
            {
                return _CongThucTinhTienVuotGio;
            }
            set
            {
                SetPropertyValue("CongThucTinhTienVuotGio", ref _CongThucTinhTienVuotGio, value);
            }
        }
        
        #endregion
        [ModelDefault("Caption", "Đơn giá (Cơ hữu)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        public decimal DonGiaCoHuu
        {
            get { return _DonGiaCoHuu; }
            set { SetPropertyValue("DonGiaCoHuu", ref _DonGiaCoHuu, value); }
        }
        [ModelDefault("Caption", "Đơn giá (Thỉnh giảng)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        public decimal DonGiaThinhGiang
        {
            get { return _DonGiaThinhGiang; }
            set { SetPropertyValue("DonGiaThinhGiang", ref _DonGiaThinhGiang, value); }
        }
        public CauHinhQuyDoiPMS_Moi(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongThucQuyDoiGio = "";
            CongThucTinhTongHeSo = "";
            CongThucTinhTongTiet = "";
        }
    }
}