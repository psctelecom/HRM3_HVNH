using System;

namespace PSC_HRM.Module.MailMerge.HopDong
{
    public class Non_ThuMoiNhanViec : Non_HopDongCaNhan
    {
        [System.ComponentModel.DisplayName("Ngày bắt đầu đóng bảo hiểm")]
        public string NgayBatDauDongBaoHiem { get; set; }
        [System.ComponentModel.DisplayName("Thưởng hiệu quả theo tháng")]
        public string LuongGop { get; set; }
        [System.ComponentModel.DisplayName("Mức lương")]
        public string MucLuong { get; set; }
        [System.ComponentModel.DisplayName("Hưởng 85 phần trăm mức lương")]
        public string Huong85PhanTramMucLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngày hợp đồng")]
        public string NgayHopDong { get; set; }
        [System.ComponentModel.DisplayName("Thời gian thử việc")]
        public string ThoiGianThuViec { get; set; }
        [System.ComponentModel.DisplayName("Tổng thu nhập")]
        public string TongThuNhap { get; set; }
        [System.ComponentModel.DisplayName("Lương thử việc")]
        public string LuongThuViec { get; set; }

    }
}
