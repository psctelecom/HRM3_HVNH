using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhTiepNhanDiNuocNgoaiCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Quốc gia")]
        public string QuocGia { get; set; }
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
        [System.ComponentModel.DisplayName("Mốc nâng lương lần sau")]
        public string MocNangLuongLanSau { get; set; }
        [System.ComponentModel.DisplayName("Mốc nâng lương")]
        public string MocNangLuong { get; set; }
        [System.ComponentModel.DisplayName("Mốc nâng lương điều chỉnh")]
        public string MocNangLuongDieuChinh { get; set; }
        [System.ComponentModel.DisplayName("Số quyết định cũ")]
        public string SoQuyetDinhCu { get; set; }
        [System.ComponentModel.DisplayName("Ngày hiệu lực cũ")]
        public string NgayhieuLucCu { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày cũ")]
        public string TuNgayCu { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày cũ")]
        public string DenNgayCu { get; set; }
    }
}
