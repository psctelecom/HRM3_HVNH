using System;
namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhNhanVien : Non_QuyetDinh
    {
        //HoSo
        [System.ComponentModel.DisplayName("Nhân viên")]
        public string NhanVien { get; set; }
        [System.ComponentModel.DisplayName(" Chức danh nhân viên")]
        public string ChucDanhNhanVien { get; set; }
        [System.ComponentModel.DisplayName("Nhân viên viết hoa")]
        public string NhanVienVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng nhân viên viết thường")]
        public string DanhXungVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng nhân viên viết hoa")]
        public string DanhXungVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Ngày sinh")]
        public string NgaySinh { get; set; }
        [System.ComponentModel.DisplayName("Ngày sinh (Date)")]
        public string NgaySinhDate { get; set; }
        [System.ComponentModel.DisplayName("Số CMND")]
        public string SoCMND { get; set; }
        [System.ComponentModel.DisplayName("Nơi sinh")]
        public string NoiSinh { get; set; }
        [System.ComponentModel.DisplayName("Địa chỉ thường trú")]
        public string DiaChiThuongTru { get; set; }
        [System.ComponentModel.DisplayName("Số điện thoại")]
        public string DienThoaiDiDong { get; set; }
        [System.ComponentModel.DisplayName("Email")]
        public string Email { get; set; }
        
        //NhanVienTrinhDo
        [System.ComponentModel.DisplayName("Trình độ chuyên môn")]
        public string TrinhDoChuyenMon { get; set; }
        [System.ComponentModel.DisplayName("Chuyên môn đào tạo")]
        public string ChuyenMonDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Chuyên ngành đào tạo")]
        public string ChuyenNganhDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Trường đào tạo")]
        public string TruongDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Năm tốt nghiệp")]
        public string NamTotNghiep { get; set; }
        [System.ComponentModel.DisplayName("Học hàm")]
        public string HocHam { get; set; }

        //NhanVien
        [System.ComponentModel.DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [System.ComponentModel.DisplayName("Tại bộ môn")]
        public string TaiBoMon { get; set; }
        [System.ComponentModel.DisplayName("Tên viết tắt đơn vị")]
        public string TenVietTatDonVi { get; set; }
        [System.ComponentModel.DisplayName("Mã đơn vị")]
        public string MaDonVi { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ nhân viên viết hoa")]
        public string ChucVuNhanVienVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ nhân viên viết thường")]
        public string ChucVuNhanVienVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ trưởng đơn vị")]
        public string ChucVuTruongDonVi { get; set; }
        [System.ComponentModel.DisplayName("Chức danh nhân viên viết hoa")]
        public string ChucDanh { get; set; }
        [System.ComponentModel.DisplayName("Chức danh nhân viên viết thường")]
        public string ChucDanhVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Loại nhân viên")]
        public string LoaiNhanVien { get; set; }
        [System.ComponentModel.DisplayName("Ngày vào cơ quan")]
        public string NgayVaoCoQuan { get; set; }

        //ThongTinNhanVien
        [System.ComponentModel.DisplayName("Số hiệu công chức")]
        public string SoHieuCongChuc { get; set; }
        [System.ComponentModel.DisplayName("Số hồ sơ")]
        public string SoHoSo { get; set; }

        //NhanVienThongTinLuong
        [System.ComponentModel.DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Bậc lương")]
        public string BacLuong { get; set; }
        [System.ComponentModel.DisplayName("Hệ số lương")]
        public string HeSoLuong { get; set; }
        [System.ComponentModel.DisplayName("Mã ngạch lương")]
        public string MaNgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngày bổ nhiệm ngạch")]
        public string NgayBoNhiemNgach { get; set; }
        [System.ComponentModel.DisplayName("Mốc nâng lương")]
        public string MocNangLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng lương")]
        public string NgayHuongLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngạch lương mới")]
        public string NgachLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Bậc lương mới")]
        public string BacLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Hệ số lương mới")]
        public string HeSoLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Mã ngạch lương mới")]
        public string MaNgachLuongMoi { get; set; }
    }

}
