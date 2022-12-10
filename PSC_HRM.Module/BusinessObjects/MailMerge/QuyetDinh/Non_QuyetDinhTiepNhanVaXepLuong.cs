using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhTiepNhanVaXepLuong : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Mã ngạch")]
        public string MaNgach { get; set; }
        [System.ComponentModel.DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Bậc lương")]
        public string BacLuong { get; set; }
        [System.ComponentModel.DisplayName("Hệ số lương")]
        public string HeSoLuong { get; set; }
        [System.ComponentModel.DisplayName("Vượt khung")]
        public string VuotKhung { get; set; }
        [System.ComponentModel.DisplayName("Mốc nâng lương")]
        public string MocNangLuong { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị cũ")]
        public string DonViCu { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị Mới")]
        public string DonViMoi { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Ngày Ký")]
        public string NgayKy { get; set; }

    }
}
