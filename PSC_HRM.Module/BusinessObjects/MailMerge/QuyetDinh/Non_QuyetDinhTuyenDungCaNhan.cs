using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhTuyenDungCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Nhóm ngạch lương")]
        public string NhomNgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Mã ngạch")]
        public string MaNgach { get; set; }
        [System.ComponentModel.DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Bậc lương")]
        public string BacLuong { get; set; }
        [System.ComponentModel.DisplayName("Hệ số lương")]
        public string HeSoLuong { get; set; }
        [System.ComponentModel.DisplayName("Hưởng 85% mức lương")]
        public string Huong85PhanTramMucLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng lương")]
        public string NgayHuongLuong { get; set; }
        [System.ComponentModel.DisplayName("Thời gian tập sự")]
        public string ThoiGianTapSu { get; set; }
        [System.ComponentModel.DisplayName("Đợt tuyển dụng")]
        public int DotTuyenDung { get; set; }
        [System.ComponentModel.DisplayName("Năm tuyển dụng")]
        public string NamTuyenDung { get; set; }
        [System.ComponentModel.DisplayName("Năm sinh")]
        public string NamSinh { get; set; }
    }
}
