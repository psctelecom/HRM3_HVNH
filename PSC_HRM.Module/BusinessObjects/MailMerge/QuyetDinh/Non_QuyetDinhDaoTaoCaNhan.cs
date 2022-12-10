using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhDaoTaoCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Hình thức đào tạo")]
        public string HinhThucDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Quốc gia")]
        public string QuocGia { get; set; }
        [System.ComponentModel.DisplayName("Trường đào tạo của QĐ")]
        public string TruongDaoTaoQD { get; set; }
        [System.ComponentModel.DisplayName("Ngành đào tạo của QĐ")]
        public string NganhDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Mã ngành đào tạo của QĐ")]
        public string MaNganhDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Chuyên ngành đào tạo của QĐ")]
        public string ChuyenMonDaoTaoQD { get; set; }
        [System.ComponentModel.DisplayName("Tên khóa đào tạo")]
        public string TenKhoaDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Trình độ đào tạo của QĐ")]
        public string TrinhDoChuyenMonQD { get; set; }
        [System.ComponentModel.DisplayName("Nguồn kinh phí")]
        public string NguonKinhPhi { get; set; }
        [System.ComponentModel.DisplayName("Trường hổ trợ")]
        public string TruongHoTro { get; set; }
        [System.ComponentModel.DisplayName("Thời gian (Khóa đào tạo)")]
        public string KhoaDaoTao { get; set; }
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
        [System.ComponentModel.DisplayName("Từ tháng")]
        public string TuThang { get; set; }
        [System.ComponentModel.DisplayName("Đến tháng")]
        public string DenThang { get; set; }
        [System.ComponentModel.DisplayName("Ngày xin đi")]
        public string NgayXinDi { get; set; }
        [System.ComponentModel.DisplayName("Họ tên TCCB")]
        public string HoTenTCCB { get; set; }
        [System.ComponentModel.DisplayName("SDT TCCB")]
        public string SDTTCCB { get; set; }
        [System.ComponentModel.DisplayName("Địa chỉ TCCB")]
        public string DiachiTCCB { get; set; }
        [System.ComponentModel.DisplayName("Học hàm TCCB")]
        public string HocHamTCCB { get; set; }
        [System.ComponentModel.DisplayName("Học vị TCCB")]
        public string HocViTCCB { get; set; }
        [System.ComponentModel.DisplayName("Ghi chú")]
        public string GhiChu { get; set; }
        [System.ComponentModel.DisplayName("Số tiền")]
        public string SoTien { get; set; }
        [System.ComponentModel.DisplayName("Số tiền bằng chữ")]
        public string SoTienBangChu { get; set; }
        [System.ComponentModel.DisplayName("Ngày khai giảng")]
        public string NgayKhaiGiang { get; set; }
        [System.ComponentModel.DisplayName("Số công văn")]
        public string SoCongVan { get; set; }  
    }
}
