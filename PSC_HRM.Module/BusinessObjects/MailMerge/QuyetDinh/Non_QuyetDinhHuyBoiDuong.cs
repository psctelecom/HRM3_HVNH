using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhHuyBoiDuong : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Hình thức đào tạo")]
        public string HinhThucDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Quốc gia")]
        public string QuocGia { get; set; }
        [System.ComponentModel.DisplayName("Trường bồi dưỡng")]
        public string TruongBoiDuong { get; set; }
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
        [System.ComponentModel.DisplayName("Từ ngày QĐ bồi dưỡng")]
        public string TuNgayQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày QĐ bồi dưỡng")]
        public string DenNgayQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Số QĐ bồi dưỡng")]
        public string SoQDBD { get; set; }
        [System.ComponentModel.DisplayName("Ngày ký QĐ bồi dưỡng (date)")]
        public string NgayKyQDBDDate { get; set; }
        [System.ComponentModel.DisplayName("Cơ quan ký QĐ bồi dưỡng")]
        public string CoQuanKyQDBD { get; set; }
        [System.ComponentModel.DisplayName("Chương trinh bồi dưỡng")]
        public string CTBoiDuong { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị tổ chức bồi dưỡng")]
        public string DVTCBD { get; set; }
        [System.ComponentModel.DisplayName("Nơi bồi dưỡng")]
        public string NoiBD { get; set; }
        [System.ComponentModel.DisplayName("Nội dung bồi dưỡng")]
        public string NoiDungBD { get; set; }
    }
}
