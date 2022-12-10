using System;



namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhBoNhiem : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Chức vụ cũ")]
        public string ChucVuCu { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị mới")]
        public string DonViMoi { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ mới")]
        public string ChucVu { get; set; }
        [System.ComponentModel.DisplayName("HSPC chức vụ mới")]
        public string HSPCChucVuMoi { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng HSPC chức vụ")]
        public string NgayHuongHSPCChucVu { get; set; }
        [System.ComponentModel.DisplayName("Ngày hết nhiệm kỳ")]
        public string NgayHetNhiemKy { get; set; }
        [System.ComponentModel.DisplayName("Số năm nhiệm kỳ")]
        public string SoNamNhiemKy { get; set; }
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }
        [System.ComponentModel.DisplayName("Ngày Ký")]
        public string NgayKy { get; set; }
        [System.ComponentModel.DisplayName("Năm nhiệm kỳ")]
        public string NamNhiemKy { get; set; }

    }

}
