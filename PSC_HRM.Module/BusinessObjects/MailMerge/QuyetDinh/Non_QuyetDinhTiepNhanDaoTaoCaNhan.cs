using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhTiepNhanDaoTaoCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Hình thức đào tạo")]
        public string HinhThucDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Quốc gia")]
        public string QuocGia { get; set; }
        [System.ComponentModel.DisplayName("Trường đào tạo")]
        public string TruongDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Ngành đào tạo")]
        public string NganhDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Chuyên ngành đào tạo")]
        public string ChuyenNganhDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Trình độ đào tạo")]
        public string TrinhDoChuyenMon { get; set; }
        [System.ComponentModel.DisplayName("Nguồn kinh phí")]
        public string NguonKinhPhi { get; set; }
        [System.ComponentModel.DisplayName("Trường hổ trợ")]
        public string TruongHoTro { get; set; }
        [System.ComponentModel.DisplayName("Khóa đào tạo")]
        public string KhoaDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày (date)")]
        public string TuNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày đào tạo")]
        public string TuNgayDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày đào tạo")]
        public string DenNgayDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Đến tháng đào tạo")]
        public string DenThangDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Ngày trờ về trường")]
        public string NgayTroVeTruong { get; set; }
        [System.ComponentModel.DisplayName("Thời gian đào tạo")]
        public string ThoiGianDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Năm đào tạo")]
        public string NamDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Số quyết định đào tạo")]
        public string SoQuyetDinhDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Số quyết định gia hạn đào tạo")]
        public string SoQuyetDinhGiaHanDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Ngày ký gia hạn")]
        public string NgayKyGH { get; set; }
    }
}
