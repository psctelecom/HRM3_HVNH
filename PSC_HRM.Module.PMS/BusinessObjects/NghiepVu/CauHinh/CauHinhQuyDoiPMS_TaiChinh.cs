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


    [ModelDefault("IsCloneable", "True")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Cấu hình quy đổi PMS (Tài chính)")]

    //[Appearance("Hide_CongThuc_TinhTIen_QNU", TargetItems = "CongThucTinhTienThuLao_DotTamUng;CongThucTinhTienThuLao_DotTongKet;CongThucTinhGio_TinhThuLao",
    //                                                Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat <> 'QNU'")]

    [Appearance("Hide", TargetItems = "HocKy", Visibility = ViewItemVisibility.Hide)]
    public class CauHinhQuyDoiPMS_TaiChinh : ThongTinChungPMS
    {

        private string ExpressionType_TaiChinh
        {
            get
            {
                return "PSC_HRM.Module.PMS.NghiepVu.ChonGiaTriLapCongThucPMS_TaiChinh";
            }
        }

        #region QNU - Công thức
        private string _GioChuanQuyDoiCQ;
        private string _CongThuc_A2_ChinhQuy;

        private string _GioChuanQuyDoiKhongCQ;
        private string _CongThuc_A2_KhongChinhQuy;

        private string _GioChuanQuyDoiCaoHoc;
        private string _CongThuc_A2_CaoHoc;

        private string _TongGioQuyDoi;
        private string _TongGioQuyDoi_SauGiamTru;

        private string _CongThuc_TongTien;
        private string _CongThuc_TongTien_CaoHoc;
        private string _CongThuc_TongTien_DHCD;

        #endregion

        #region Đợt 1
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức Đợt 1_Chính quy")]
        public string GioChuanQuyDoiCQ
        {
            get { return _GioChuanQuyDoiCQ; }
            set { SetPropertyValue("GioChuanQuyDoiCQ", ref _GioChuanQuyDoiCQ, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức Đợt 1_Không chính quy")]
        public string GioChuanQuyDoiKhongCQ
        {
            get { return _GioChuanQuyDoiKhongCQ; }
            set { SetPropertyValue("GioChuanQuyDoiKhongCQ", ref _GioChuanQuyDoiKhongCQ, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức Đợt 1_Cao học")]
        public string GioChuanQuyDoiCaoHoc
        {
            get { return _GioChuanQuyDoiCaoHoc; }
            set { SetPropertyValue("GioChuanQuyDoiCaoHoc", ref _GioChuanQuyDoiCaoHoc, value); }
        }
        #endregion

        #region A2
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức Đợt 2_Chính quy")]
        public string CongThuc_A2_ChinhQuy
        {
            get { return _CongThuc_A2_ChinhQuy; }
            set { SetPropertyValue("CongThuc_A2_ChinhQuy", ref _CongThuc_A2_ChinhQuy, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức Đợt 2_Không chính quy")]
        public string CongThuc_A2_KhongChinhQuy
        {
            get { return _CongThuc_A2_KhongChinhQuy; }
            set { SetPropertyValue("CongThuc_A2_KhongChinhQuy", ref _CongThuc_A2_KhongChinhQuy, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức Đợt 2_Cao học")]
        public string CongThuc_A2_CaoHoc
        {
            get { return _CongThuc_A2_CaoHoc; }
            set { SetPropertyValue("CongThuc_A2_CaoHoc", ref _CongThuc_A2_CaoHoc, value); }
        }
        #endregion


        #region Tổng giờ -  Tính tiền
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức Tổng giờ")]
        public string TongGioQuyDoi
        {
            get { return _TongGioQuyDoi; }
            set { SetPropertyValue("TongGioQuyDoi", ref _TongGioQuyDoi, value); }
        }
        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức Tổng giờ sau giảm trừ")]
        public string TongGioQuyDoi_SauGiamTru
        {
            get { return _TongGioQuyDoi_SauGiamTru; }
            set { SetPropertyValue("TongGioQuyDoi_SauGiamTru", ref _TongGioQuyDoi_SauGiamTru, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức Tổng tiền")]
        public string CongThuc_TongTien
        {
            get { return _CongThuc_TongTien; }
            set { SetPropertyValue("CongThuc_TongTien", ref _CongThuc_TongTien, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức Tổng tiền cao học")]
        public string CongThuc_TongTien_CaoHoc
        {
            get { return _CongThuc_TongTien_CaoHoc; }
            set { SetPropertyValue("CongThuc_TongTien_CaoHoc", ref _CongThuc_TongTien_CaoHoc, value); }
        }

        [VisibleInListView(false)]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [ModelDefault("Caption", "Công thức Tổng tiền DHCD")]
        public string CongThuc_TongTien_DHCD
        {
            get { return _CongThuc_TongTien_DHCD; }
            set { SetPropertyValue("CongThuc_TongTien_DHCD", ref _CongThuc_TongTien_DHCD, value); }
        }
        #endregion
        public CauHinhQuyDoiPMS_TaiChinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}