using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhTiepNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Ngày xin tiếp nhận")]
        public string NgayXinTiepNhan { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày QĐ tiếp nhận")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Số QĐ nghỉ không hưởng lương")]
        public string SoQDNghiKhongLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngày QĐ nghỉ không hưởng lương")]
        public string NgayNghiKhongLuong { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày QĐ nghỉ không hưởng lương")]
        public string TuNgayNghiKhongLuong { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày QĐ nghỉ không hưởng lương")]
        public string DenNgayNghiKhongLuong { get; set; }
        [System.ComponentModel.DisplayName("Số tháng nghỉ không hưởng lương")]
        public string SoThang { get; set; }
    }
}
