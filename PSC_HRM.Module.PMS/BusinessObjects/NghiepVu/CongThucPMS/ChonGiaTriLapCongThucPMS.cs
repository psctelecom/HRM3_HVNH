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

namespace PSC_HRM.Module.PMS.NghiepVu
{
    [ImageName("BO_BangLuong")]
    [ModelDefault("Caption", "Chọn giá trị PMS")]
    [NonPersistent]

    public class ChonGiaTriLapCongThucPMS : BaseObject
    {
        //Chỉ dùng để lập công thức
        //======================================================
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Hệ số lớp đông")]
        public HeSoLopDong HeSoLopDong { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Hệ số lương")]
        public HeSoLuong HeSoLuong { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Hệ số chức danh")]
        public HeSoChucDanh HeSoChucDanh { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Hệ số ngoài giờ")]
        public HeSoGiangDay_NgoaiGio HeSoGiangDay_NgoaiGio { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Hệ số cơ sở")]
        public HeSoCoSo HeSoCoSo { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Hệ số Tín chỉ")]
        public HeSoTinChi HeSoTinChi { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Khối lượng giảng dạy")]
        public ChiTietKhoiLuongGiangDay KhoiLuongGiangDay { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Cấu hình quy đổi PMS")]
        public CauHinhQuyDoiPMS CauHinhQuyDoiPMS { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Thông tin bảng chốt")]
        public ThongTinBangChot ThongTinBangChot { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Chi tiết sau đại học")]
        public ChiTietKhoiLuongSauDaiHoc ChiTietKhoiLuongSauDaiHoc { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Chi tiết kê khai sau giảng")]
        public ChiTietKeKhaiSauGiang ChiTietKeKhaiSauGiang { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Nhân viên giờ giảng")]
        public NhanVien_GioGiang NhanVien_GioGiang { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Tính thù lao")]
        public Temp_TinhThuLao Temp_TinhThuLao { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Quản lý hệ số")]
        public QuanLyHeSo QuanLyHeSo { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Chi tiết khối lượng giảng dạy (Mới)")]
        public ChiTietKhoiLuongGiangDay_Moi ChiTietKhoiLuongGiangDay_Moi { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Thời khóa biểu (Mới)")]
        public ChiTiet_ThoiKhoaBieu ChiTiet_ThoiKhoaBieu { get; set; }
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Thông tin chung")]
        public ChiTietThongTinChungPMS ChiTietThongTinChungPMS { get; set; }
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Thông tin tính thù lao")]
        public ThongTinBangChotThuLao ThongTinBangChotThuLao { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Kê khai hoạt động khác")]
        public ChiTietKeKhaiHuongDanKhac ChiTietKeKhaiHuongDanKhac { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Bảng thanh toán giờ")]
        public BangThanhToanGio BangThanhToanGio { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Chốt thù lao")]
        public ChiTietChotThuLao ChiTietChotThuLao { get; set; }

        public ChonGiaTriLapCongThucPMS(Session session) : base(session) { }
    }

}
