using System;

namespace PSC_HRM.Module.MailMerge.HopDong
{
    public class Non_HopDongLamViec : Non_HopDongCaNhan
    {
        [System.ComponentModel.DisplayName("Hình thức thanh toán")]
        public string HinhThucThanhToan { get; set; }
        [System.ComponentModel.DisplayName("Mã ngạch lương")]
        public string MaNgach { get; set; }
        [System.ComponentModel.DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Bậc lương")]
        public string BacLuong { get; set; }
        [System.ComponentModel.DisplayName("Hệ số lương")]
        public string HeSoLuong { get; set; }
        [System.ComponentModel.DisplayName("Mức lương được hưởng")]
        public string MucLuongDuocHuong { get; set; }
        [System.ComponentModel.DisplayName("Thời gian xét nâng bậc lương")]
        public string ThoiGianXetNangBacLuong { get; set; }
        [System.ComponentModel.DisplayName("Số quyết định")]
        public string SoQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Ngày ký quyết định")]
        public string NgayKyQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Năm tuyển dụng")]
        public string NamTuyenDung { get; set; }
        [System.ComponentModel.DisplayName("Tập sự từ ngày")]
        public string TapSuTuNgay { get; set; }
        [System.ComponentModel.DisplayName("Tập sự đến ngày")]
        public string TapSuDenNgay { get; set; }
        [System.ComponentModel.DisplayName("Nghề nghiệp trước khi tuyển dụng")]
        public string NgheNghiepTruocKhiDuocTuyen { get; set; }
        [System.ComponentModel.DisplayName("Ngày bắt đầu đóng bảo hiểm")]
        public string NgayBatDauDongBaoHiem { get; set; }
        [System.ComponentModel.DisplayName("Mức lương")]
        public string MucLuong { get; set; }
        [System.ComponentModel.DisplayName("Thưởng hiệu quả theo tháng")]
        public string LuongGop { get; set; }
        [System.ComponentModel.DisplayName("Mã học hàm người ký")]
        public string MaHocHamNguoiKy { get; set; }
        [System.ComponentModel.DisplayName("Mã trình độ người ký")]
        public string MaTrinhDoNguoiKy { get; set; }
    }
}
