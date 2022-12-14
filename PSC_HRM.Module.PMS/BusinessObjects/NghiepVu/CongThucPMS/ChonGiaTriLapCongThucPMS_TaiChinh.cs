using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.TapDieuKien;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using PSC_HRM.Module.PMS.NghiepVu.ThanhToan;
using PSC_HRM.Module.PMS.NghiepVu.SauDaiHoc;
using PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang;
using PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao;
using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.TaiChinh;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.ThanhToan;

namespace PSC_HRM.Module.PMS.NghiepVu
{
    [ImageName("BO_BangLuong")]
    [ModelDefault("Caption", "Chọn giá trị PMS - tài chính")]
    [NonPersistent]

    public class ChonGiaTriLapCongThucPMS_TaiChinh : BaseObject
    {
        //Chỉ dùng để lập công thức
        //======================================================
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Thông tin bảng chốt")]
        public ThongTinBangChot ThongTinBangChot { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Thanh toán vượt định mức")]
        public ThanhToanVuotDinhMuc ThanhToanVuotDinhMuc { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Cấu hình")]
        public CauHinhQuyDoiPMS_TaiChinh CauHinhQuyDoiPMS_TaiChinh { get; set; }

        public ChonGiaTriLapCongThucPMS_TaiChinh(Session session) : base(session) { }
    }

}
