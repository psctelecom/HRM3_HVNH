using System;
using System.Collections;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.HopDong
{
    public class Non_HopDongKiemGiang : Non_HopDong
    {
        [System.ComponentModel.DisplayName("Học kỳ")]
        public string HocKy { get; set; }
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }
        [System.ComponentModel.DisplayName("Mã số thuế trường")]
        public string MaSoThueTruong { get; set; }
        [System.ComponentModel.DisplayName("Số tài khoản trường")]
        public string SoTaiKhoanTruong { get; set; }
        [System.ComponentModel.DisplayName("Ngân hàng trường")]
        public string NganHangTruong { get; set; }
        [System.ComponentModel.DisplayName("Mã số thuế người lao động")]
        public string MaSoThueNguoiLaoDong { get; set; }
        [System.ComponentModel.DisplayName("Số tài khoản người lao động")]
        public string SoTaiKhoanNguoiLaoDong { get; set; }
        [System.ComponentModel.DisplayName("Ngân hàng người lao động")]
        public string NganHangNguoiLaoDong { get; set; }
        [System.ComponentModel.DisplayName("Môn dạy kiêm giảng")]
        public string MonDayKiemGiang { get; set; }
        [System.ComponentModel.DisplayName("Tổng tiền")]
        public string TongTien { get; set; }
       
        [System.ComponentModel.DisplayName("Tổng tiền bằng chữ")]
        public string TongTienBangChu { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị công tác")]
        public string DonViCongTac { get; set; }
        [System.ComponentModel.DisplayName("Công việc hiện nay")]
        public string CongViecHienNay { get; set; }
        [System.ComponentModel.DisplayName("Số tiền một tiết")]
        public string SoTien1Tiet { get; set; }
        [System.ComponentModel.DisplayName("Số tiền một tiết bằng chữ")]
        public string SoTien1TietBangChu { get; set; } 
        [System.ComponentModel.DisplayName("Từ ngày date")]
        public string TuNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày date")]
        public string DenNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Lớp")]
        public string Lop { get; set; }
        [System.ComponentModel.DisplayName("Số Tiết Lý Thuyết")]
        public string SoTietLT { get; set; }
        [System.ComponentModel.DisplayName("Số Tiết Thực Hành")]
        public string SoTietTH { get; set; }
        [System.ComponentModel.DisplayName("Sĩ số")]
        public string SiSo { get; set; }
        [System.ComponentModel.DisplayName("Ngành")]
        public string Nganh { get; set; }
        [System.ComponentModel.DisplayName("Quyền cao nhất đơn vị")]
        public string QuyenCaoNhatDonVi { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ nhân viên")]
        public string ChucVuNhanVien { get; set; }
    }
}
