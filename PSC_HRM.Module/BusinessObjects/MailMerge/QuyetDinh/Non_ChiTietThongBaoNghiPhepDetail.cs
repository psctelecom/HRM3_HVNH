using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietThongBaoNghiPhepDetail : Non_MergeItem
    {
        [System.ComponentModel.DisplayName("STT")]
        public string STT { get; set; }
        [System.ComponentModel.DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [System.ComponentModel.DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [System.ComponentModel.DisplayName("Mã ngạch")]
        public string MaNgach { get; set; }
        [System.ComponentModel.DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Thời gian nghỉ phép")]
        public string ThoiGianNghiPhep { get; set; }
        [System.ComponentModel.DisplayName("Ngày xin nghỉ phép")]
        public string NgayDuocNghiPhep { get; set; }
        [System.ComponentModel.DisplayName("Ngày bắt đầu nghỉ phép")]
        public string NgayBatDauNghiPhep { get; set; }
        [System.ComponentModel.DisplayName("Ngày kết thúc nghỉ phép")]
        public string NgayKetThucNghiPhep { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
    }
}
