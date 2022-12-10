using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhKhenThuongCaNhanDetail : Non_MergeItem
    {
        [System.ComponentModel.DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [System.ComponentModel.DisplayName("Nhân viên")]
        public string NhanVien { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [System.ComponentModel.DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ")]
        public string Chucvu { get; set; }
    }
}
