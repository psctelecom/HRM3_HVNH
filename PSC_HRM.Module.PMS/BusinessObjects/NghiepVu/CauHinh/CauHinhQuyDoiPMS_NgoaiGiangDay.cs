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
    [ModelDefault("Caption", "Cấu hình quy đổi PMS(Ngoài giảng dạy)")]
    [Appearance("Hide_NEU", TargetItems = "KyTinhPMS;HocKy"
            , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.MaQuanLy = 'NEU'")]
    public class CauHinhQuyDoiPMS_NgoaiGiangDay : ThongTinChungPMS
    {
        private string _CongThucSoBaiKiemTra;
        private string _CongThucSoBaiTapLon;
        private string _CongThucSoBaiTieuLuan;
        private string _CongThucSoDeAnTotNghiep;
        private string _CongThucSoChuyenDeTotNghiep;
        private string _CongThucSoBaiThi;
        private string _CongThucSoDeRaDe;
        private string _CongThucSoHDKhac;
        private string _CongThucSoSlotHoc;
        private string _CongThucSoTraLoiCauHoiTrenHeThongHocTap;
        private string _CongThucSoTruyCapLopHoc;

        private string _CongThucTongGio;

        #region Công thức
        private string ExpressionType
        {
            get
            {
                return "PSC_HRM.Module.PMS.NghiepVu.ChonGiaTriLapCongThucPMS_NgoaiGiangDay";
            }
        }
        [ModelDefault("Caption", "Tính số bài KT")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucSoBaiKiemTra
        {
            get
            {
                return _CongThucSoBaiKiemTra;
            }
            set
            {
                SetPropertyValue("CongThucSoBaiKiemTra", ref _CongThucSoBaiKiemTra, value);
            }
        }
        [ModelDefault("Caption", "Tính số bài tập lớn")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucSoBaiTapLon
        {
            get
            {
                return _CongThucSoBaiTapLon;
            }
            set
            {
                SetPropertyValue("CongThucSoBaiTapLon", ref _CongThucSoBaiTapLon, value);
            }
        }

        [ModelDefault("Caption", "Công thức quy đổi bài tiểu luận")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucSoBaiTieuLuan
        {
            get
            {
                return _CongThucSoBaiTieuLuan;
            }
            set
            {
                SetPropertyValue("CongThucSoBaiTieuLuan", ref _CongThucSoBaiTieuLuan, value);
            }
        }
        [ModelDefault("Caption", "Công thức quy đổi số đề án TN")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucSoDeAnTotNghiep
        {
            get
            {
                return _CongThucSoDeAnTotNghiep;
            }
            set
            {
                SetPropertyValue("CongThucSoDeAnTotNghiep", ref _CongThucSoDeAnTotNghiep, value);
            }
        }
        [ModelDefault("Caption", "Công thức quy đổi số chuyên đề TN")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucSoChuyenDeTotNghiep
        {
            get
            {
                return _CongThucSoChuyenDeTotNghiep;
            }
            set
            {
                SetPropertyValue("CongThucSoChuyenDeTotNghiep", ref _CongThucSoChuyenDeTotNghiep, value);
            }
        }



        [ModelDefault("Caption", "Công thức quy đổi số bài thi")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucSoBaiThi
        {
            get
            {
                return _CongThucSoBaiThi;
            }
            set
            {
                SetPropertyValue("CongThucSoBaiThi", ref _CongThucSoBaiThi, value);
            }
        }
        [ModelDefault("Caption", "Công thức quy đổi số đề ra đề")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucSoDeRaDe
        {
            get
            {
                return _CongThucSoDeRaDe;
            }
            set
            {
                SetPropertyValue("CongThucSoDeRaDe", ref _CongThucSoDeRaDe, value);
            }
        }
        [ModelDefault("Caption", "Công thức  quy đổi HD khác")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucSoHDKhac
        {
            get
            {
                return _CongThucSoHDKhac;
            }
            set
            {
                SetPropertyValue("CongThucSoHDKhac", ref _CongThucSoHDKhac, value);
            }
        }
        [ModelDefault("Caption", "Công thức  quy đổi số slot học")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucSoSlotHoc
        {
            get
            {
                return _CongThucSoSlotHoc;
            }
            set
            {
                SetPropertyValue("CongThucSoSlotHoc", ref _CongThucSoSlotHoc, value);
            }
        }
        [ModelDefault("Caption", "Công thức quy đổi trả lời câu hỏi")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucSoTraLoiCauHoiTrenHeThongHocTap
        {
            get
            {
                return _CongThucSoTraLoiCauHoiTrenHeThongHocTap;
            }
            set
            {
                SetPropertyValue("CongThucSoTraLoiCauHoiTrenHeThongHocTap", ref _CongThucSoTraLoiCauHoiTrenHeThongHocTap, value);
            }
        }
        [ModelDefault("Caption", "Công thức quy đổi truy cập lớp học")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucSoTruyCapLopHoc
        {
            get
            {
                return _CongThucSoTruyCapLopHoc;
            }
            set
            {
                SetPropertyValue("CongThucSoTruyCapLopHoc", ref _CongThucSoTruyCapLopHoc, value);
            }
        }

        [ModelDefault("Caption", "Công thức quy đổi tổng giờ")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucTongGio
        {
            get
            {
                return _CongThucTongGio;
            }
            set
            {
                SetPropertyValue("CongThucTongGio", ref _CongThucTongGio, value);
            }
        }
        #endregion
        public CauHinhQuyDoiPMS_NgoaiGiangDay(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}