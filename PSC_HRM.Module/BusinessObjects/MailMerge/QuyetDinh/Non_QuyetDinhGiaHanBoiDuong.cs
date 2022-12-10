using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhGiaHanBoiDuong : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Hình thức đào tạo")]
        public string HinhThucDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Quốc gia")]
        public string QuocGia { get; set; }
        [System.ComponentModel.DisplayName("Trường đào tạo")]
        public string TruongDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Chuyên ngành đào tạo")]
        public string ChuyenMonDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Trình độ đào tạo")]
        public string TrinhDoChuyenMon { get; set; }
        [System.ComponentModel.DisplayName("Nguồn kinh phí")]
        public string NguonKinhPhi { get; set; }
        [System.ComponentModel.DisplayName("Trường hổ trợ")]
        public string TruongHoTro { get; set; }
        [System.ComponentModel.DisplayName("Khóa đào tạo")]
        public string KhoaDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Thời gian gia hạn")]
        public string ThoiGianGiaHan { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày (Date)")]
        public string TuNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày (Date)")]
        public string DenNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày QĐ đào tạo")]
        public string TuNgayQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày QĐ đào tạo")]
        public string DenNgayQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Số QĐ đào tạo")]
        public string SoQDDT { get; set; }
        [System.ComponentModel.DisplayName("Ngày ký QĐ đào tạo (date)")]
        public string NgayKyQDDTDate { get; set; }
        [System.ComponentModel.DisplayName("Cơ quan ký QĐ đào tạo")]
        public string CoQuanKyQDDT { get; set; }
        [System.ComponentModel.DisplayName("Chương trinh bồi dưỡng")]
        public string CTBoiDuong { get; set; }
    }
}
