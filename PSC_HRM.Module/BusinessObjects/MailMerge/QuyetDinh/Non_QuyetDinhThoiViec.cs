using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhThoiViec : Non_QuyetDinhNhanVien
    {        
        [System.ComponentModel.DisplayName("Ngày xin nghỉ")]
        public string NgayXinNghi { get; set; }
        [System.ComponentModel.DisplayName("Nghỉ việc từ ngày")]
        public string NghiViecTuNgay { get; set; }
        [System.ComponentModel.DisplayName("Trả lương đến ngày")]
        public string TraLuongDenNgay { get; set; }
        [System.ComponentModel.DisplayName("Thời hạn bàn giao công việc")]
        public string ThoiHanBanGiaoCongViec { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
        [System.ComponentModel.DisplayName("Tính trợ cấp từ ngày")]
        public string TinhTroCapTuNgay { get; set; }
        [System.ComponentModel.DisplayName("Tính trợ cấp đến ngày")]
        public string TinhTroCapDenNgay { get; set; }
        [System.ComponentModel.DisplayName("Tính trợ cấp từ ngày (tháng)")]
        public string TinhTroCapTuNgayMonth { get; set; }
        [System.ComponentModel.DisplayName("Tính trợ cấp đến ngày (tháng)")]
        public string TinhTroCapDenNgayMonth { get; set; }
        [System.ComponentModel.DisplayName("Mức lương hiện hưởng")]
        public string MucLuongHienHuong { get; set; }
        [System.ComponentModel.DisplayName("Số năm trợ cấp")]
        public string SoNamTroCap { get; set; }
        [System.ComponentModel.DisplayName("Số tiền trợ cấp")]
        public string SoTienTroCap { get; set; }
        [System.ComponentModel.DisplayName("Số tiền trợ cấp bằng chữ")]
        public string SoTienTroCapBangChu { get; set; }
        //
        [System.ComponentModel.DisplayName("Tài khoản ngân hàng")]
        public string TKNganHang { get; set; }
        [System.ComponentModel.DisplayName("Ngân hàng")]
        public string NganHang { get; set; }
        [System.ComponentModel.DisplayName("Hệ số lương")]
        public string HeSoLuong { get; set; }
        [System.ComponentModel.DisplayName("Bậc lương")]
        public string BacLuong { get; set; }
        [System.ComponentModel.DisplayName("Số sổ BHXH")]
        public string SoBHXH { get; set; }
    }
}
