using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhDieuChinhThoiGianDiDaoTao : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Nguồn kinh phí")]
        public string NguonKinhPhi { get; set; }
        [System.ComponentModel.DisplayName("Trường hổ trợ")]
        public string TruongHoTro { get; set; }
        [System.ComponentModel.DisplayName("Quốc gia")]
        public string QuocGia { get; set; }
        [System.ComponentModel.DisplayName("Hộ chiếu")]
        public string HoChieu { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày điều chỉnh")]
        public string TuNgayDC { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày điều chỉnh")]
        public string DenNgayDC { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
        [System.ComponentModel.DisplayName("Số công văn")]
        public string SoCongVan { get; set; }
        [System.ComponentModel.DisplayName("Địa điểm")]
        public string DiaDiem { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị tổ chức")]
        public string DonViToChuc { get; set; }
        [System.ComponentModel.DisplayName("Số quyết định cũ")]
        public string SoQuyetDinhCu { get; set; }
        [System.ComponentModel.DisplayName("Ngày hiệu lực cũ")]
        public string NgayHieuLucCu { get; set; }

        public Non_QuyetDinhDieuChinhThoiGianDiDaoTao()
        {
            Master = new List<Non_ChiTietQuyetDinhDieuChinhThoiGianDiDaoTaoMaster>();
            Detail = new List<Non_ChiTietQuyetDinhDieuChinhThoiGianDiDaoTaoDetail>();
        }
    }
}
