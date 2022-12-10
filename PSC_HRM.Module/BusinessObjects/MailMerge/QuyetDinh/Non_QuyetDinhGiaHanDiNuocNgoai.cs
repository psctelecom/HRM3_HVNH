using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhGiaHanDiNuocNgoai : Non_QuyetDinhNhanVien
    {
        
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Tại quốc gia")]
        public string TaiQuocGia { get; set; }
        [System.ComponentModel.DisplayName("Trường hỗ trợ")]
        public string TruongHoTro { get; set; }
        [System.ComponentModel.DisplayName("Nguồn kinh phí")]
        public string NguonKinhPhi { get; set; }
        [System.ComponentModel.DisplayName("Địa điểm")]
        public string DiaDiem { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
    }
}
