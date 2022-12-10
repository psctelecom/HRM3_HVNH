using System;



namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhBoNhiemChucVuDoan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Chức vụ đoàn cũ")]
        public string ChucVuDoanCu { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ đoàn mới")]
        public string ChucVuDoanMoi { get; set; }
        [System.ComponentModel.DisplayName("HSPC chức vụ đoàn mới")]
        public string HSPCChucVuDoanMoi { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng HSPC chức vụ đoàn mới")]
        public string NgayHuongHSPCChucVuDoanMoi { get; set; }
        [System.ComponentModel.DisplayName("Năm nhiệm kỳ")]
        public string NamNhiemKy { get; set; }
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }
        [System.ComponentModel.DisplayName("Ngày Ký")]
        public string NgayKy { get; set; }

    }

}
