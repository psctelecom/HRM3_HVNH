 using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhDiCongTacCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Số công văn/phiếu trình")]
        public string SoCongVan { get; set; }
        [System.ComponentModel.DisplayName("Địa điểm")]
        public string DiaDiem { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị tổ chức")]
        public string DonViToChuc { get; set; }
        [System.ComponentModel.DisplayName("Nguồn kinh phí")]
        public string NguonKinhPhi { get; set; }
        [System.ComponentModel.DisplayName("Trường hổ trợ")]
        public string TruongHoTro { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày (Date)")]
        public string TuNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày (Date)")]
        public string DenNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
        [System.ComponentModel.DisplayName("Ngày ký")]
        public string NgayKy { get; set; }
        [System.ComponentModel.DisplayName("Ngày xin đi/mời")]
        public string NgayXinDi { get; set; }
        [System.ComponentModel.DisplayName("Ngày xin đi/mời (date)")]
        public string NgayXinDiDate { get; set; }
        [System.ComponentModel.DisplayName("Vị trí")]
        public string ViTri { get; set; }
        [System.ComponentModel.DisplayName("Quốc gia")]
        public string QuocGia { get; set; }
        [System.ComponentModel.DisplayName("Thời gian")]
        public string ThoiGian { get; set; }
        [System.ComponentModel.DisplayName("Ghi chú TG")]
        public string GhiChuTG { get; set; }
    }
}
