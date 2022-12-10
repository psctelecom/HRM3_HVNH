using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhThanhLapHoiDongXetDenBuDaoTaoDetail : Non_MergeItem
    {
        [System.ComponentModel.DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [System.ComponentModel.DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [System.ComponentModel.DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [System.ComponentModel.DisplayName("Chức danh hội đồng tuyển dụng")]
        public string ChucDanhHoiDongTuyenDung { get; set; }
        [System.ComponentModel.DisplayName("Vai trò")]
        public string VaiTro { get; set; }
    }
}
