using System;

namespace PSC_HRM.Module.MailMerge.HopDong
{
    public class Non_HopDongLaoDong : Non_HopDongCaNhan
    {
        [System.ComponentModel.DisplayName("Hình thức thanh toán")]
        public string HinhThucThanhToan { get; set; }
        [System.ComponentModel.DisplayName("Mã ngạch")]
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
        [System.ComponentModel.DisplayName("Tập sự từ ngày")]
        public string TapSuTuNgay { get; set; }
        [System.ComponentModel.DisplayName("Tập sự đến ngày")]
        public string TapSuDenNgay { get; set; }
        [System.ComponentModel.DisplayName("Nghề nghiệp trước khi tuyển dụng")]
        public string NgheNghiepTruocKhiDuocTuyen { get; set; }
        //QNU
        [System.ComponentModel.DisplayName("Tiền lương")]
        public string TienLuong { get; set; }
        [System.ComponentModel.DisplayName("Tiền lương bằng chữ")]
        public string TienLuongBangChu { get; set; }
        [System.ComponentModel.DisplayName("Phụ cấp tiền ăn")]
        public string PhuCapTienAn { get; set; }
        [System.ComponentModel.DisplayName("Phụ cấp độc hại")]
        public string PhuCapDocHai { get; set; }
        [System.ComponentModel.DisplayName("Mã học hàm người ký")]
        public string MaHocHamNguoiKy { get; set; }
        [System.ComponentModel.DisplayName("Mã trình độ người ký")]
        public string MaTrinhDoNguoiKy { get; set; }
    }
}
