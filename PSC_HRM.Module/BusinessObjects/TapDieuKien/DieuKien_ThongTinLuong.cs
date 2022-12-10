using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.TapDieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Thông tin lương")]
    public class DieuKien_ThongTinLuong : BaseObject
    {
        [ModelDefault("Caption", "Phân loại")]
        public ThongTinLuongEnum PhanLoai { get; set; }

        [ModelDefault("Caption", "Nhóm ngạch lương")]
        public NhomNgachLuong NhomNgachLuong { get; set; }

        [ModelDefault("Caption", "Ngạch lương")]
        public NgachLuong NgachLuong { get; set; }

        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch")]
        public DateTime NgayBoNhiemNgach { get; set; }

        //ngay huong luong ghi tren quyet dinh
        [ModelDefault("Caption", "Ngày hưởng lương")]
        public DateTime NgayHuongLuong { get; set; }

        [ModelDefault("Caption", "Bậc lương")]
        public BacLuong BacLuong { get; set; }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("DisplayFormat","N2")]
        [ModelDefault("EditMask","N2")]
        public Double HeSoLuong { get; set; }

        [ModelDefault("Caption", "Không cư trú")]
        public bool KhongCuTru { get; set; }

        [ModelDefault("Caption", "Mã số thuế")]
        public string MaSoThue { get; set; }

        [ModelDefault("Caption", "Cơ quan thuế")]
        public CoQuanThue CoQuanThue { get; set; }

        [ModelDefault("Caption", "% vượt khung")]
        public int VuotKhung { get; set; }

        [ModelDefault("Caption", "HSPC vượt khung")]
        [ModelDefault("DisplayFormat", "N3")]
        [ModelDefault("EditMask", "N3")]
        public Double HSPCVuotKhung { get; set; }

        [ModelDefault("Caption", "HSPC chức vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCChucVu { get; set; }
        [ModelDefault("Caption", "HSPC chức vụ bảo lưu")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCChucVuBaoLuu { get; set; }

        [ModelDefault("Caption", "HSPC phục vụ đào tạo")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCPhucVuDaoTao { get; set; }

        [ModelDefault("Caption", "Ngày hưởng PCCV")]
        public DateTime NgayHuongHSPCChucVu { get; set; }

        [ModelDefault("Caption", "HSPC độc hại")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCDocHai { get; set; }

        [ModelDefault("Caption", "HSPC trách nhiệm")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCTrachNhiem { get; set; }

        [ModelDefault("Caption", "HSPC trách nhiệm 1")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCTrachNhiem1 { get; set; }

        [ModelDefault("Caption", "HSPC trách nhiệm 2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCTrachNhiem2 { get; set; }

        [ModelDefault("Caption", "HSPC trách nhiệm 3")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCTrachNhiem3 { get; set; }

        [ModelDefault("Caption", "HSPC trách nhiệm 4")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCTrachNhiem4 { get; set; }

        [ModelDefault("Caption", "HSPC trách nhiệm 5")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCTrachNhiem5 { get; set; }

        [ModelDefault("Caption", "HSPC trách nhiệm 6")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCTrachNhiem6 { get; set; }

        [ModelDefault("Caption", "HSPC khu vực")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCKhuVuc { get; set; }

        [ModelDefault("Caption", "HSPC kiêm nhiệm")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCKiemNhiem { get; set; }

        [ModelDefault("Caption", "HSPC lưu động")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCLuuDong { get; set; }

        [ModelDefault("Caption", "Tỉ lệ tăng thêm")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public Double TiLeTangThem { get; set; }

        [ModelDefault("Caption", "HSL tăng thêm")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.SpinEditor")]
        public Double HSLTangThem { get; set; }

        [ModelDefault("Caption", "HS tăng thêm theo TN (C)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSLTangThemTheoThamNien { get; set; }

        [ModelDefault("Caption", "HSPC khác")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCKhac { get; set; }

        [ModelDefault("Caption", "% PC ưu đãi")]
        public int PhuCapUuDai { get; set; }

        [ModelDefault("Caption", "% PC đặc thù")]
        public int PhuCapDacThu { get; set; }

        [ModelDefault("Caption", "% PC đặc biệt")]
        public int PhuCapDacBiet { get; set; }

        [ModelDefault("Caption", "PC Thu hút")]
        public int PhuCapThuHut { get; set; }

        [ModelDefault("Caption", "% Thâm niên")]
        public int ThamNien { get; set; }

        [ModelDefault("Caption", "HSPC Thâm niên")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.SpinEditor")]
        public Double HSPCThamNien { get; set; }

        [ModelDefault("Caption", "PC quản lý")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCChucVu1 { get; set; }

        [ModelDefault("Caption", "PC kiêm nhiệm 1")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCChucVu2 { get; set; }

        [ModelDefault("Caption", "PC kiêm nhiệm 2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCChucVu3 { get; set; }

        [ModelDefault("Caption", "HSPC chuyên môn")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public Double HSPCChuyenMon { get; set; }

        [ModelDefault("Caption", "HSPCCV Đảng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCChucVuDang { get; set; }

        [ModelDefault("Caption", "HSPCCV Đoàn")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCChucVuDoan { get; set; }

        [ModelDefault("Caption", "HSPCCV Công Đoàn")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCChucVuCongDoan { get; set; }       

        [ModelDefault("Caption", "Phụ cấp tiền ăn")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public Double PhuCapTienAn { get; set; }

        [ModelDefault("Caption", "Phụ cấp điện thoại")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public Double PhuCapDienThoai { get; set; }

        [ModelDefault("Caption", "Số lít xăng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public Double PhuCapTienXang { get; set; }

        [ModelDefault("Caption", "PC tăng thêm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public Double PhuCapTangThem { get; set; }

        [ModelDefault("Caption", "Thâm niên công tác")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public Double ThamNienCongTac { get; set; }

        [ModelDefault("Caption", "PC kiêm nhiệm công việc")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public Double HSPhuCapTrachNhiemCongViec { get; set; }

        [ModelDefault("Caption", "Phụ cấp ưu đãi (cơ hữu)")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public Double PhuCapUuDaiCoHuu { get; set; }

        [ModelDefault("Caption", "HSPC Khối hành chính")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCKhoiHanhChinh { get; set; }

        [ModelDefault("Caption", "% PC thâm niên HC")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public Double PhanTramThamNienHC { get; set; }

        [ModelDefault("Caption", "HSPC thâm niên HC")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCThamNienHC { get; set; }

        [ModelDefault("Caption", "Lương khoán")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public Double LuongKhoan { get; set; }

        [ModelDefault("Caption", "Số người phụ thuộc")]
        public int SoNguoiPhuThuoc { get; set; }

        [ModelDefault("Caption", "Số ngày tính TNTT")]
        public int SoNgayTinhTNTT { get; set; }

        [ModelDefault("Caption", "Số tháng giảm trừ")]
        public int SoThangGiamTru { get; set; }

        [ModelDefault("Caption", "Mốc nâng lương")]
        public DateTime MocNangLuong { get; set; }

        [ModelDefault("Caption", "Hưởng 85% mức lương")]
        public bool Huong85PhanTramLuong { get; set; }

        [ModelDefault("Caption", "Tính lương")]
        public bool TinhLuong { get; set; }

        [ModelDefault("Caption", "Không đóng bảo hiểm")]
        public bool KhongDongBaoHiem { get; set; }

        [ModelDefault("Caption", "Không tham gia công đoàn")]
        public bool KhongThamGiaCongDoan { get; set; }

        [ModelDefault("Caption", "Bằng đã kiểm duyệt")]
        public bool BangCapDaKiemDuyet { get; set; }

        [ModelDefault("Caption", "Được hưởng HSPC Chuyên viên")]
        public bool DuocHuongHSPCChuyenVien { get; set; }

        [ModelDefault("Caption", "HSPC lãnh đạo")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCLanhDao { get; set; }

        [ModelDefault("Caption", "HSPC kiêm nhiệm trong trường")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCKiemNhiemTrongTruong { get; set; }

        [ModelDefault("Caption", "HSPC ưu đãi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCUuDai { get; set; }

        [ModelDefault("Caption", "HSPC trách nhiệm trong trường")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCTracNhiemTruong { get; set; }       

        [ModelDefault("Caption", "HSCV tăng thêm")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("EditMask", "N4")]
        public Double HeSoChucVuTangThem { get; set; }

        [ModelDefault("Caption", "Số tháng thanh toán TNTT")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double SoThangThanhToan { get; set; }       

        [ModelDefault("Caption", "Số tháng thanh toán TNQL")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double SoThangThanhToanTNQL { get; set; }

        [ModelDefault("Caption", "Số tháng thanh toán DTCV")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double SoThangThanhToanDTCV { get; set; }

        [ModelDefault("Caption", "Số tháng thanh toán PVDT")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double SoThangThanhToanPVDT { get; set; }

        public DieuKien_ThongTinLuong(Session session) : base(session) { }
    }

}
