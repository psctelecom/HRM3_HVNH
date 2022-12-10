using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhNangLuongDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [System.ComponentModel.DisplayName("Năm sinh")]
        public string NamSinh { get; set; }
        [System.ComponentModel.DisplayName("Năm sinh nữ")]
        public string NamSinhNu { get; set; }
        [System.ComponentModel.DisplayName("Năm sinh nam")]
        public string NamSinhNam { get; set; }
        [System.ComponentModel.DisplayName("Trình độ chuyên môn")]
        public string TrinhDoChuyenMon { get; set; }
        [System.ComponentModel.DisplayName("Loại nâng lương")]
        public string LoaiNangLuong { get; set; }
        [System.ComponentModel.DisplayName("Mã ngạch lương")]
        public string MaNgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Bậc lương cũ")]
        public string BacLuongCu { get; set; }
        [System.ComponentModel.DisplayName("Hệ số lương cũ")]
        public string HeSoLuongCu { get; set; }
        [System.ComponentModel.DisplayName("Vượt khung cũ")]
        public string VuotKhungCu { get; set; }
        [System.ComponentModel.DisplayName("Bậc lương mới")]
        public string BacLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Hệ số lương mới")]
        public string HeSoLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Vượt khung mới")]
        public string VuotKhungMoi { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng lương")]
        public string NgayHuongLuong { get; set; }
        [System.ComponentModel.DisplayName("Mốc nâng lương mới")]
        public string MocNangLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Mốc nâng lương cũ")]
        public string MocNangLuongCu { get; set; }
        //
        //NEU          
        [System.ComponentModel.DisplayName("Đơn vị chủ quản")]
        public string DonViChuQuan { get; set; }
        [System.ComponentModel.DisplayName("Tên trường viết hoa")]
        public string TenTruongVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Tên trường viết thường")]
        public string TenTruongVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Tên trường viết tắt")]
        public string TenTruongVietTat { get; set; }
        [System.ComponentModel.DisplayName("Số quyết định")]
        public string SoQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Số phiếu trình")]
        public string SoPhieuTrinh { get; set; }
        [System.ComponentModel.DisplayName("Ngày phiếu trình")]
        public string NgayPhieuTrinh { get; set; }

        [System.ComponentModel.DisplayName("Ngày quyết định")]
        public string NgayQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Năm quyết định")]
        public string NamQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Quý quyết định")]
        public string QuyQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Ngày hiệu lực")]
        public string NgayHieuLuc { get; set; }
        [System.ComponentModel.DisplayName("Ngày quyết định (Date)")]
        public string NgayQuyetDinhDate { get; set; }
        [System.ComponentModel.DisplayName("Ngày hiệu lực (Date)")]
        public string NgayHieuLucDate { get; set; }
        [System.ComponentModel.DisplayName("Căn cứ")]
        public string CanCu { get; set; }
        [System.ComponentModel.DisplayName("Về việc")]
        public string NoiDung { get; set; }
        [System.ComponentModel.DisplayName("Chức danh người ký")]
        public string ChucDanhNguoiKy { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ người ký")]
        public string ChucVuNguoiKy { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ người ký viết thường")]
        public string ChucVuNguoiKyVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Người ký")]
        public string NguoiKy { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng người ký viết thường")]
        public string DanhXungNguoiKyVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng người ký viết hoa")]
        public string DanhXungNguoiKyVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Ghi chú")]
        public string GhiChu { get; set; }
        [System.ComponentModel.DisplayName("Chức danh người ký bản sao")]
        public string ChucDanhNguoiKyBanSao { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ người ký bản sao")]
        public string ChucVuNguoiKyBanSao { get; set; }
        [System.ComponentModel.DisplayName("Người ký bản sao")]
        public string NguoiKyBanSao { get; set; }      
        [System.ComponentModel.DisplayName("Số bản sao")]
        public string SoBanSao { get; set; }
        [System.ComponentModel.DisplayName("Ngày họp hội đồng lương (Date)")]
        public string NgayHopHoiDongLuongDate { get; set; }
        [System.ComponentModel.DisplayName("Số lượng cán bộ nâng lương thường xuyên")]
        public string SoLuongNangThuongXuyen { get; set; }
        [System.ComponentModel.DisplayName("Số lượng cán bộ nâng lương trước nghỉ hưu")]
        public string SoLuongNangTruocNghiHuu { get; set; }
        [System.ComponentModel.DisplayName("Số lượng cán bộ nâng lương trước hạn")]
        public string SoLuongNangTruocHan { get; set; }
        [System.ComponentModel.DisplayName("Số lượng cán bộ nâng vượt khung")]
        public string SoLuongNangVuotKhung { get; set; }
    }
}
