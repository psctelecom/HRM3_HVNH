using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietThongBaoGiaHanTapSuDetail : Non_MergeItem
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
        [System.ComponentModel.DisplayName("Thời gian gia hạn")]
        public string ThoiGianGiaHan { get; set; }
        [System.ComponentModel.DisplayName("Ngày được gia hạn")]
        public string NgayDuocGiaHan { get; set; }
        [System.ComponentModel.DisplayName("Ngày bắt đầu tập sự")]
        public string NgayBatDauTapSu { get; set; }
        [System.ComponentModel.DisplayName("Ngày kết thúc tập sự")]
        public string NgayKetThucTapSu { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
    }
}
