using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhGiaHanThinhGiang : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Thời gian gia hạn")]
        public string ThoiGianGiaHan { get; set; }
        [System.ComponentModel.DisplayName("Tập sự đến ngày")]
        public string TapSuDenNgay { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
        [System.ComponentModel.DisplayName("Nhân viên")]
        public string NhanVien { get; set; }
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
        [System.ComponentModel.DisplayName("Nơi Cấp")]
        public string NoiCap { get; set; }
        [System.ComponentModel.DisplayName("Cấp Ngày")]
        public string CapNgay { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ cao của đơn vị")]
        public string ChucVuDonVi { get; set; }
    }
}
