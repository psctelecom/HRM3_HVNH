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
    [ModelDefault("Caption", "Chi tiết cấu hình quy đổi PMS(QC94)")]

    public class ChiTietCauHinhQuyDoiPMS_VHU : BaseObject
    {
        private DanhMucLoaiKhongPhanThoiKhoaBieu _DanhMucLoaiKhongPhanThoiKhoaBieu;
        private string _CongThucQuyGioChuan;
        private decimal _DonGia;
        private CauHinhQuyDoiPMS_VHU _CauHinhQuyDoiPMS_VHU;

        [ModelDefault("Caption", "Cấu hình")]
        [Association("CauHinhQuyDoiPMS_VHU-ListChiTietCauHinhQuyDoiPMS_VHU")]
        [Browsable(false)]
        public CauHinhQuyDoiPMS_VHU CauHinhQuyDoiPMS_VHU
        {
            get { return _CauHinhQuyDoiPMS_VHU; }
            set { SetPropertyValue("CauHinhQuyDoiPMS_VHU", ref _CauHinhQuyDoiPMS_VHU, value); }
        }

        [ModelDefault("Caption", "Danh mục")]
        public DanhMucLoaiKhongPhanThoiKhoaBieu DanhMucLoaiKhongPhanThoiKhoaBieu
        {
            get { return _DanhMucLoaiKhongPhanThoiKhoaBieu; }
            set { SetPropertyValue("DanhMucLoaiKhongPhanThoiKhoaBieu", ref _DanhMucLoaiKhongPhanThoiKhoaBieu, value); }
        }

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set { SetPropertyValue("DonGia", ref _DonGia, value); }
        }
        
        private string ExpressionType
        {
            get
            {
                return "PSC_HRM.Module.PMS.NghiepVu.ChonGiaTriLapCongThucPMS";
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức quy đổi")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucQuyGioChuan
        {
            get
            {
                return _CongThucQuyGioChuan;
            }
            set
            {
                SetPropertyValue("CongThucQuyGioChuan", ref _CongThucQuyGioChuan, value);
            }
        }
        

        public ChiTietCauHinhQuyDoiPMS_VHU(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
 
    }
}

