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
using PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.UEL;

namespace PSC_HRM.Module.PMS.NghiepVu
{
    [ImageName("BO_BangLuong")]
    [ModelDefault("Caption", "Chọn giá trị PMS(UEL)")]
    [NonPersistent]

    public class ChonGiaTriLapCongThucPMS_UEL : BaseObject
    {
        //Chỉ dùng để lập công thức
        //======================================================
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Khối lượng giảng dạy")]
        public ChiTietKhoiLuongGiangDay_UEL ChiTietKhoiLuongGiangDay_UEL { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Dữ liệu lập công thức")]
        public CotChonChauHinhQiuDoi CotChonChauHinhQiuDoi { get; set; }

        public ChonGiaTriLapCongThucPMS_UEL(Session session) : base(session) { }
    }

}
