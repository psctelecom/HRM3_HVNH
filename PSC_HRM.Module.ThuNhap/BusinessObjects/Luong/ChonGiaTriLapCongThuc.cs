using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.ChotThongTinTinhLuong;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.ChamCong;

namespace PSC_HRM.Module.ThuNhap.Luong
{
    [ImageName("BO_BangLuong")]
    [ModelDefault("Caption", "Chọn giá trị")]
    [NonPersistent]
     public class ChonGiaTriLapCongThuc : BaseObject
    {
        //Chỉ dùng để lập công thức
        //======================================================
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Kỳ tính lương")]
        public KyTinhLuong KyTinhLuong { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Thông tin lương")]
        public ThongTinTinhLuong ThongTinLuong { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Chấm công nhân viên")]
        public ChiTietChamCongNhanVien ChiTietChamCongNhanVien { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Chấm công khác")]
        public ChiTietChamCongKhac ChiTietChamCongKhac { get; set; }
        //======================================================

        public ChonGiaTriLapCongThuc(Session session) : base(session) { }
    }
}
