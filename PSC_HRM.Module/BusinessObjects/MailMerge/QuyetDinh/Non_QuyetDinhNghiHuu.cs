using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhNghiHuu : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Ngày sinh")]
        public string NgaySinh { get; set; }
        [System.ComponentModel.DisplayName("Ngày ký")]
        public string NgayKy { get; set; }
        [System.ComponentModel.DisplayName("Nơi sinh")]
        public string NoiSinh { get; set; }
        [System.ComponentModel.DisplayName("Nơi sinh (tỉnh/tp)")]
        public string NoiSinh_TinhTP { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [System.ComponentModel.DisplayName("Số sổ bảo hiểm xã hội")]
        public string SoSoBHXH { get; set; }
        [System.ComponentModel.DisplayName("Nghỉ việc từ ngày")]
        public string NghiViecTuNgay { get; set; }
        [System.ComponentModel.DisplayName("Nơi cư trú sau khi nghỉ việc")]
        public string NoiCuTruSauKhiNghiViec { get; set; }
        [System.ComponentModel.DisplayName("Quận")]
        public string NoiCuTruSauKhiNghiViec_Quan { get; set; }
        [System.ComponentModel.DisplayName("Tỉnh")]
        public string NoiCuTruSauKhiNghiViec_Tinh { get; set; }
        //
        [System.ComponentModel.DisplayName("Tài khoản ngân hàng")]
        public string TKNganHang { get; set; }
        [System.ComponentModel.DisplayName("Ngân hàng")]
        public string NganHang { get; set; }
        [System.ComponentModel.DisplayName("Nơi khám BHYT ")]
        public string NoiKhamBHYT { get; set; }
    }
}
