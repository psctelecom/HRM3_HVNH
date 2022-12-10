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


namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Loại môn học")]
    [DefaultProperty("TenLoaiMonHoc")]
    public class LoaiMonHoc : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiMonHoc;
        private string _CongThucQuyDoiGio;
        private string _CongThucQuyDoiGioSauDinhMuc;
        private decimal _DoUuTien;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên loại môn học")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiMonHoc
        {
            get { return _TenLoaiMonHoc; }
            set { SetPropertyValue("TenLoaiMonHoc", ref _TenLoaiMonHoc, value); }
        }

        private string ExpressionType
        {
            get
            {
                return "PSC_HRM.Module.PMS.NghiepVu.ChonGiaTriLapCongThucPMS";
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

        [ModelDefault("Caption", "Công thức quy đổi giờ sau trừ định mức")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyDoiGioSauDinhMuc
        {
            get
            {
                return _CongThucQuyDoiGioSauDinhMuc;
            }
            set
            {
                SetPropertyValue("CongThucQuyDoiGioSauDinhMuc", ref _CongThucQuyDoiGioSauDinhMuc, value);
            }
        }

        [ModelDefault("Caption", "Độ ưu tiên")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal DoUuTien
        {
            get { return _DoUuTien; }
            set { SetPropertyValue("DoUuTien", ref _DoUuTien, value); }
        }
        public LoaiMonHoc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            DoUuTien = 1;
        }
    }
}
