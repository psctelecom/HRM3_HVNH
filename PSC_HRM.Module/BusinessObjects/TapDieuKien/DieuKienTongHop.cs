using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.ChotThongTinTinhLuong;

namespace PSC_HRM.Module.TapDieuKien
{
    [NonPersistent]
    [DefaultProperty("HoTen")]
    [ModelDefault("Caption", "Thông tin nhân viên")]
    public class DieuKienTongHop : BaseObject
    {
        [ModelDefault("Caption", "Sơ yếu lý lịch 1")]
        public DieuKien_HoSo HoSo { get; set; }

        [ModelDefault("Caption", "Sơ yếu lý lịch 2")]
        public DieuKien_NhanVien NhanVien { get; set; }

        [ModelDefault("Caption", "Số hiệu công chức")]
        public string SoHieuCongChuc { get; set; }

        [ModelDefault("Caption", "Số hồ sơ")]
        public string SoHoSo { get; set; }

        [ModelDefault("Caption", "Chức vụ")]
        [DataSourceCriteria("PhanLoai=0 or PhanLoai=2")]
        public ChucVu ChucVu { get; set; }

        [ModelDefault("Caption", "Lần bổ nhiệm chức vụ")]
        public int LanBoNhiemChucVu { get; set; }

        [ModelDefault("Caption", "Ngày bổ nhiệm")]
        public DateTime NgayBoNhiem { get; set; }

        [ModelDefault("Caption", "Chức vụ kiêm nhiệm")]
        public ChucVu ChucVuKiemNhiem { get; set; }

        [ModelDefault("Caption", "Điện thoại cơ quan")]
        public string DienThoaiCoQuan { get; set; }

        [ModelDefault("Caption", "Biên chế")]
        public bool BienChe { get; set; }

        [ModelDefault("Caption", "Tham gia giảng dạy")]
        public bool ThamGiaGiangDay { get; set; }

        [ModelDefault("Caption", "Loại hợp đồng")]
        public LoaiNhanVien LoaiNhanVien { get; set; }

        [ModelDefault("Caption", "Loại nhân sự")]
        public LoaiNhanSu LoaiNhanSu { get; set; }

        [ModelDefault("Caption", "Thông tin lương")]
        public DieuKien_ThongTinLuong NhanVienThongTinLuong { get; set; }

        [ModelDefault("Caption", "Trình độ nhân viên")]
        public DieuKien_TrinhDo NhanVienTrinhDo { get; set; }

        [ModelDefault("Caption", "Hợp đồng lao động")]
        public DieuKien_HopDong HopDong { get; set; }

        [ModelDefault("Caption", "Đảng viên")]
        public DieuKien_DangVien DangVien { get; set; }

        [ModelDefault("Caption", "Đoàn viên")]
        public DieuKien_DoanVien DoanVien { get; set; }

        [ModelDefault("Caption", "Đoàn thể")]
        public DieuKien_DoanThe DoanThe { get; set; }

        [ModelDefault("Caption", "Bảo hiểm")]
        public DieuKien_BaoHiem HoSoBaoHiem { get; set; }

        [ModelDefault("Caption", "Kỷ luật")]
        public DieuKien_KyLuat QuyetDinhKyLuat { get; set; }

        [ModelDefault("Caption", "Khen thưởng")]
        public DieuKien_KhenThuong QuyetDinhKhenThuong { get; set; }

        [ModelDefault("Caption", "Đánh giá cán bộ")]
        public DieuKien_DanhGiaCanBo ChiTietChamCongNhanVien { get; set; }

        public DieuKienTongHop(Session session) : base(session) { }
    }

}
