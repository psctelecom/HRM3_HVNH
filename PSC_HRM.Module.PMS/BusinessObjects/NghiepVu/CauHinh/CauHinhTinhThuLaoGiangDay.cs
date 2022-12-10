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
    [ModelDefault("Caption", "Cấu hình tính thù lao giảng dạy")]
    [ModelDefault("IsCloneable", "True")]
    [Appearance("Hide_HUFLIT", TargetItems = "KyTinhPMS;"
            , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.MaQuanLy = 'HUFLIT'")]
    public class CauHinhTinhThuLaoGiangDay : ThongTinChungPMS
    {
        [ModelDefault("Caption", "ThongTin")]
        public string ThongTin
        {
            get
            {
                if (TruongConfig.MaTruong == "HUFLIT")
                    return String.Format("{0} {1} {2}", ThongTinTruong.TenBoPhan, NamHoc != null ? "- Năm học: " + NamHoc.TenNamHoc : "", HocKy != null ? "- " + HocKy.TenHocKy : "");
                else
                    return String.Format("{0} {1}", ThongTinTruong.TenBoPhan, NamHoc != null ? " - Năm học: " + NamHoc.TenNamHoc : ""); ;

            }
        }

        private string _CongThucTinhTien_KhongThue;
        private string _CongThucTinhTien_Thue;
        private string _CongThucTinhTien_TongTien;

       
        private string ExpressionType
        {
            get
            {
                return "PSC_HRM.Module.PMS.NghiepVu.ChonGiaTriLapCongThucPMS";
            }
        }
        [ModelDefault("Caption", "Tiền chịu thuế")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        [Size(500)]
        public string CongThucTinhTien_KhongThue
        {
            get
            {
                return _CongThucTinhTien_KhongThue;
            }
            set
            {
                SetPropertyValue("CongThucTinhTien_KhongThue", ref _CongThucTinhTien_KhongThue, value);
            }
        }
        [ModelDefault("Caption", "Tiền không chịu thuế")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        [Size(500)]
        public string CongThucTinhTien_Thue
        {
            get
            {
                return _CongThucTinhTien_Thue;
            }
            set
            {
                SetPropertyValue("CongThucTinhTien_Thue", ref _CongThucTinhTien_Thue, value);
            }
        }

        [ModelDefault("Caption", "Tổng tiền")]        
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        [Size(500)]
        public string CongThucTinhTien_TongTien
        {
            get
            {
                return _CongThucTinhTien_TongTien;
            }
            set
            {
                SetPropertyValue("CongThucTinhTien_TongTien", ref _CongThucTinhTien_TongTien, value);
            }
        }
        public CauHinhTinhThuLaoGiangDay(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}