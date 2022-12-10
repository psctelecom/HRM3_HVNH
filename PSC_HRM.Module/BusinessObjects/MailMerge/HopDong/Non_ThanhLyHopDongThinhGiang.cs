using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.HopDong
{
    public class Non_ThanhLyHopDongThinhGiang : Non_HopDong
    {
        [System.ComponentModel.DisplayName("Số tài khoản")]
        public string SoTaiKhoan { get; set; }
        [System.ComponentModel.DisplayName("Ngân hàng")]
        public string NganHang { get; set; }
        [System.ComponentModel.DisplayName("Số hợp đồng thỉnh giảng")]
        public string SoHopDongThinhGiang { get; set; }
        [System.ComponentModel.DisplayName("Ngày ký hợp đồng thỉnh giảng")]
        public string NgayKyHopDongThinhGiang { get; set; }
        [System.ComponentModel.DisplayName("Môn dạy")]
        public string MonDay { get; set; }
        [System.ComponentModel.DisplayName("Lớp")]
        public string Lop { get; set; }
        [System.ComponentModel.DisplayName("Số tiết")]
        public string SoTiet { get; set; }
        [System.ComponentModel.DisplayName("Số tiền trên tiết")]
        public string SoTienTrenTiet { get; set; }
        [System.ComponentModel.DisplayName("Tổng tiền")]
        public string TongTien { get; set; }
        [System.ComponentModel.DisplayName("Tổng tiền bằng chữ")]
        public string TongTienBangChu { get; set; }
        [System.ComponentModel.DisplayName("Học vị")]
        public string HocVi { get; set; }
    }
}
