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
    [ModelDefault("Caption", "Chọn giá trị PMS - Ngoài giảng dạy")]
    [NonPersistent]

    public class ChonGiaTriLapCongThucPMS_NgoaiGiangDay : BaseObject
    {
        //Chỉ dùng để lập công thức
        //======================================================
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Kê khai")]
        public ChiTietKeKhai_CacHoatDong_ThoiKhoaBieu ChiTietKeKhai_CacHoatDong_ThoiKhoaBieu { get; set; }


        public ChonGiaTriLapCongThucPMS_NgoaiGiangDay(Session session) : base(session) { }
    }

}
