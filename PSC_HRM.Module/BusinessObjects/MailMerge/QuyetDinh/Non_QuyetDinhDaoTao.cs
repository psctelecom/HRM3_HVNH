using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhDaoTao : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Hình thức đào tạo")]
        public string HinhThucDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Quốc gia")]
        public string QuocGia { get; set; }
        [System.ComponentModel.DisplayName("Trường đào tạo")]
        public string TruongDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Ngành đào tạo")]
        public string NganhDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Trình độ đào tạo")]
        public string TrinhDoChuyenMon { get; set; }
        [System.ComponentModel.DisplayName("Tên khóa đào tạo")]
        public string TenKhoaDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Nguồn kinh phí")]
        public string NguonKinhPhi { get; set; }
        [System.ComponentModel.DisplayName("Trường hỗ trợ")]
        public string TruongHoTro { get; set; }
        [System.ComponentModel.DisplayName("Thời gian")]
        public string KhoaDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Từ tháng")]
        public string TuThang { get; set; }
        [System.ComponentModel.DisplayName("Đến tháng")]
        public string DenThang { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày (date)")]
        public string TuNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày (date)")]
        public string DenNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Thời gian đào tạo")]
        public string ThoiGianDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Nước đào tạo")]
        public string NuocDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Ghi chú")]
        public string GhiChu { get; set; }
        [System.ComponentModel.DisplayName("Số công văn")]
        public string SoCongVan { get; set; }
        [System.ComponentModel.DisplayName("Số tiền")]
        public string SoTien { get; set; }
        [System.ComponentModel.DisplayName("Số tiền bằng chữ")]
        public string SoTienBangChu { get; set; }
        [System.ComponentModel.DisplayName("Ngày khai giảng")]
        public string NgayKhaiGiang { get; set; }
        [System.ComponentModel.DisplayName("Ngày xin đi")]
        public string NgayXinDi { get; set; }
        public Non_QuyetDinhDaoTao()
        {
            Master = new List<Non_ChiTietQuyetDinhDaoTaoMaster>();
            Detail = new List<Non_ChiTietQuyetDinhDaoTaoDetail>();
        }
    }
}
