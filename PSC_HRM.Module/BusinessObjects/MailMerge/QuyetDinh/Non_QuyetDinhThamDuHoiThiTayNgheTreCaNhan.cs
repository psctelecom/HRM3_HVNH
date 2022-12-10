using DevExpress.Xpo;
using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhThamDuHoiThiTayNgheTreCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Số công văn")]
        public string SoCongVan { get; set; }
        [System.ComponentModel.DisplayName("Địa điểm")]
        public string DiaDiem { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị tổ chức")]
        public string DonViToChuc { get; set; }
        [System.ComponentModel.DisplayName("Nguồn kinh phí")]
        public string NguonKinhPhi { get; set; }
        [System.ComponentModel.DisplayName("Trường hỗ trợ")]
        public string TruongHoTro { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
        [System.ComponentModel.DisplayName("Vị trí")]
        public string ViTri { get; set; }
        [System.ComponentModel.DisplayName("Nghề tham gia")]
        public string NgheThamGia { get; set; }
    }
}
