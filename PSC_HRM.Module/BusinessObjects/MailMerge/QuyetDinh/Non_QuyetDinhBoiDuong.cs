using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhBoiDuong : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Quốc gia")]
        public string QuocGia { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị tổ chức")]
        public string DonViToChuc { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị tổ chức theo chương trình")]
        public string DonViToChucTheoChuongTrinh { get; set; }
        [System.ComponentModel.DisplayName("Địa điểm")]
        public string DiaDiem { get; set; }
        [System.ComponentModel.DisplayName("Địa điểm theo chương trình")]
        public string DiaDiemTheoChuongTrinh { get; set; }
        [System.ComponentModel.DisplayName("Nội dung bồi dưỡng")]
        public string NoiDungBoiDuong { get; set; }
        [System.ComponentModel.DisplayName("Nguồn kinh phí")]
        public string NguonKinhPhi { get; set; }
        [System.ComponentModel.DisplayName("Trường hỗ trợ")]
        public string TruongHoTro { get; set; }
        [System.ComponentModel.DisplayName("Trường đào tạo")]
        public string TruongDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Chứng chỉ")]
        public string ChungChi { get; set; }
        [System.ComponentModel.DisplayName("Số công văn")]
        public string SoCongVan { get; set; }
        [System.ComponentModel.DisplayName("Chương trình bồi dưỡng")]
        public string ChuongTrinhBoiDuong { get; set; }
        [System.ComponentModel.DisplayName("Nơi bồi dưỡng")]
        public string NoiBoiDuong { get; set; }
        [System.ComponentModel.DisplayName("Loại hình bồi dưỡng")]
        public string LoaiHinhBoiDuong { get; set; }
        [System.ComponentModel.DisplayName("Thời gian")]
        public string ThoiGian { get; set; }
        [System.ComponentModel.DisplayName("Số quyết định điều chỉnh")]
        public string SoQuyetDinhDieuChinh { get; set; }
        [System.ComponentModel.DisplayName("Ngày quyết định điều chỉnh")]
        public string NgayQuyetDinhDieuChinh { get; set; }
        [System.ComponentModel.DisplayName("Số tiền")]
        public string SoTien { get; set; }
        [System.ComponentModel.DisplayName("Số tiền bằng chữ")]
        public string SoTienBangChu { get; set; }
        [System.ComponentModel.DisplayName("Ngày khai giảng")]
        public string NgayKhaiGiang { get; set; }

        public Non_QuyetDinhBoiDuong()
        {
            Master = new List<Non_ChiTietQuyetDinhBoiDuongMaster>();
            Detail = new List<Non_ChiTietQuyetDinhBoiDuongDetail>();
        }
    }
}
